using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI;
using Game.Data;
using Game.Interfaces;
using Game.Core;
using Game.Utilities;
using Game.Zones;

namespace Game.Wheel
{
    [System.Serializable]
    public struct ZoneVisualConfig
    {
        public ZoneType zoneType;
        public Sprite spinnerSprite;
        public Sprite indicatorSprite;
    }

    public class WheelManager : MonoBehaviour, IWheelAnimator
    {
        [Header("References")]
        public WheelSlice slicePrefab; 
        public Transform wheelSpinner;

        [Header("Data Pools")]
        public List<RewardData> normalRewardsPool;
        public List<RewardData> superRewardsPool;
        public RewardData bombData;

        [Header("Wheel Visuals (Images)")]
        public Image spinnerImage;
        public Image indicatorImage;

        [Header("Wheel Visual Configurations")]
        public List<ZoneVisualConfig> zoneVisualConfigs = new List<ZoneVisualConfig>();

        private List<WheelSlice> activeSlices = new List<WheelSlice>();

        private void Awake()
        {
            ServiceLocator.Register<IWheelAnimator>(this);
        }

        private void Start()
        {
            var zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null)
            {
                zoneService.OnZoneChanged += GenerateWheel;
                GenerateWheel(zoneService.CurrentZone, zoneService.CurrentZoneType);
            }
        }

        private void OnDestroy()
        {
            var zoneService = ServiceLocator.Get<IZoneService>();
            if (zoneService != null)
            {
                zoneService.OnZoneChanged -= GenerateWheel;
            }
        }

        public void GenerateWheel(int currentZone, ZoneType zoneType)
        {
            if (wheelSpinner != null)
            {
                wheelSpinner.localEulerAngles = Vector3.zero;
            }
            ClearWheel();
            UpdateWheelVisuals(zoneType);

            float angleStep = 360f / WheelConstants.TOTAL_SLICES;

            for (int i = 0; i < WheelConstants.TOTAL_SLICES; i++)
            {
                WheelSlice newSlice = Instantiate(slicePrefab, wheelSpinner);

                float angle = i * angleStep;
                float angleRad = angle * Mathf.Deg2Rad;

                Vector3 targetPos = new Vector3(Mathf.Sin(angleRad), Mathf.Cos(angleRad), 0) * WheelConstants.SLICE_RADIUS;
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

            var config = zoneVisualConfigs.Find(c => c.zoneType == type);
            if (config.spinnerSprite != null)
            {
                spinnerImage.sprite = config.spinnerSprite;
                indicatorImage.sprite = config.indicatorSprite;
            }
        }

        public void SpinWheel(System.Action<RewardData> onComplete)
        {
            int winningIndex = Random.Range(0, WheelConstants.TOTAL_SLICES);
            WheelSlice winningSlice = activeSlices[winningIndex];

            float angleStep = 360f / WheelConstants.TOTAL_SLICES;
            float targetAngle = winningIndex * angleStep;
            float finalRotation = targetAngle - (360f * WheelConstants.SPIN_REVOLUTIONS);
            var audioService = ServiceLocator.Get<IAudioService>();
            if (audioService != null) audioService.StartSpinSound();

            wheelSpinner.DORotate(new Vector3(0, 0, finalRotation), WheelConstants.SPIN_DURATION, RotateMode.FastBeyond360)
                .SetEase(Ease.OutBack, 0.45f) 
                .OnComplete(() =>
                {
                    if (audioService != null) audioService.StopSpinSound();
                    onComplete?.Invoke(winningSlice.sliceData);
                });
        }

        private void ClearWheel()
        {
            foreach (var slice in activeSlices)
            {
                Destroy(slice.gameObject);
            }
            activeSlices.Clear();
        }
    }
}