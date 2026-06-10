using System;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    [Header("Debug Settings")]
    [Tooltip("Eğer işaretliyse, oyun her başladığında eski kayıtları siler ve kasayı sıfırlar.")]
    public bool resetSaveOnStart = true; 

    public Action<int> OnTotalGoldChanged;

    private int totalGold = 0;

    private void Start()
    {
        if (resetSaveOnStart)
        {
            PlayerPrefs.DeleteKey("TotalGold");
            PlayerPrefs.Save();
        }

        totalGold = PlayerPrefs.GetInt("TotalGold", 0);

        OnTotalGoldChanged?.Invoke(totalGold);
    }

    public int GetTotalGold()
    {
        return totalGold;
    }

    public void AddGold(int amount)
    {
        totalGold += amount;
        PlayerPrefs.SetInt("TotalGold", totalGold);
        PlayerPrefs.Save();

        OnTotalGoldChanged?.Invoke(totalGold);
    }

    public bool ConsumeGold(int amount)
    {
        if (totalGold >= amount)
        {
            totalGold -= amount;
            PlayerPrefs.SetInt("TotalGold", totalGold);
            PlayerPrefs.Save();

            OnTotalGoldChanged?.Invoke(totalGold);
            return true;
        }
        return false;
    }
}