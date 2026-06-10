using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI; 

public class WheelManager : MonoBehaviour
{
    [Header("References")]
    public WheelSlice slicePrefab; 
    public Transform wheelSpinner;

    [Header("Settings")]
    public int totalSlices = 8;
    public float sliceRadius = 150f;

    [Header("Data Pools")]
    public List<RewardData> normalRewardsPool;
    public List<RewardData> superRewardsPool;
    public RewardData bombData;

    [Header("Spin Animation Settings")]
    public float spinDuration = 3.5f;
    public int spinRevolutions = 5;
    private bool isSpinning = false;

    [Header("Wheel Visuals (Images)")]
    public Image spinnerImage;
    public Image indicatorImage;

    [Header("Wheel Sprites")]
    public Sprite bronzeSpinner;
    public Sprite silverSpinner;
    public Sprite goldSpinner;

    [Header("Managers")]
    public ZoneManager zoneManager;
    public InventoryManager inventoryManager; 
    public UIManager uiManager;


    public Sprite bronzeIndicator;
    public Sprite silverIndicator;
    public Sprite goldIndicator;

    private List<WheelSlice> activeSlices = new List<WheelSlice>();


    public void GenerateWheel(int currentZone, ZoneType zoneType)
    {
        ClearWheel();

        UpdateWheelVisuals(zoneType);

        float angleStep = 360f / totalSlices;

        for (int i = 0; i < totalSlices; i++)
        {
            WheelSlice newSlice = Instantiate(slicePrefab, wheelSpinner);

            float angle = i * angleStep;
            float angleRad = angle * Mathf.Deg2Rad;

            Vector3 targetPos = new Vector3(Mathf.Sin(angleRad), Mathf.Cos(angleRad), 0) * sliceRadius;
            newSlice.transform.localPosition = targetPos;

            newSlice.transform.localRotation = Quaternion.Euler(0, 0, -angle);

            RewardData selectedData;

            if (zoneType == ZoneType.Normal && i == 0)
            {
                selectedData = bombData; 
            }
            else
            {
                List<RewardData> pool = (zoneType == ZoneType.Super) ? superRewardsPool : normalRewardsPool;
                selectedData = pool[Random.Range(0, pool.Count)];
            }

            newSlice.SetupSlice(selectedData, currentZone);
            activeSlices.Add(newSlice);
        }
    }

    private void UpdateWheelVisuals(ZoneType type)
    {
        if (spinnerImage == null || indicatorImage == null) return;

        switch (type)
        {
            case ZoneType.Normal:
                spinnerImage.sprite = bronzeSpinner;
                indicatorImage.sprite = bronzeIndicator;
                break;
            case ZoneType.Safe:
                spinnerImage.sprite = silverSpinner;
                indicatorImage.sprite = silverIndicator;
                break;
            case ZoneType.Super:
                spinnerImage.sprite = goldSpinner;
                indicatorImage.sprite = goldIndicator;
                break;
        }
    }

    public void SpinWheel()
    {
        if (isSpinning) return;
        isSpinning = true;

        int winningIndex = Random.Range(0, totalSlices);
        WheelSlice winningSlice = activeSlices[winningIndex];

        float angleStep = 360f / totalSlices;

        float targetAngle = winningIndex * angleStep;

        float finalRotation = targetAngle - (360f * spinRevolutions);

        if (AudioManager.Instance != null) AudioManager.Instance.StartSpinSound();


        wheelSpinner.DORotate(new Vector3(0, 0, finalRotation), spinDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCirc) 
            .OnComplete(() =>
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.StopSpinSound();
                }
                isSpinning = false;
                ProcessReward(winningSlice);
            });
    }

    private void ProcessReward(WheelSlice wonSlice)
    {
        if (wonSlice.sliceData.type == RewardType.Bomb)
        {
            if (uiManager != null)
            {
                uiManager.ShowBombPopup();
            }
        }
        else
        {
            RewardData data = wonSlice.sliceData;
            int currentZone = zoneManager.currentZone;
            ZoneType currentZoneType = zoneManager.GetZoneType(currentZone);

            float zoneProgression = 1f + (currentZone * data.zoneMultiplier);
            int finalAmount = Mathf.RoundToInt(data.baseAmount * zoneProgression);

            if (currentZoneType == ZoneType.Safe)
            {
                finalAmount = Mathf.RoundToInt(finalAmount * 2f);
            }
            else if (currentZoneType == ZoneType.Super)
            {
                finalAmount = Mathf.RoundToInt(finalAmount * 10f);
            }

            finalAmount = Mathf.Max(data.baseAmount, finalAmount); 

            if (inventoryManager != null) inventoryManager.AddReward(data, finalAmount);

            if (uiManager != null)
            {
                uiManager.ShowRewardPopup(data.icon, data.rewardName, finalAmount, () =>
                {
                    zoneManager.MoveToNextZone();
                });
            }
            else
            {
                zoneManager.MoveToNextZone();
            }
        }
    }

    private void ClearWheel()
    {
        foreach (var slice in activeSlices)
        {
            Destroy(slice.gameObject);
        }
        activeSlices.Clear();
    }
    private void OnEnable()
    {
        if (zoneManager != null)
        {
            zoneManager.OnZoneChanged += GenerateWheel;
        }
    }

    private void OnDisable()
    {
        if (zoneManager != null)
        {
            zoneManager.OnZoneChanged -= GenerateWheel;
        }
    }
}