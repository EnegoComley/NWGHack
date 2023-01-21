using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterTracker : MonoBehaviour
{
    public float TodaysWaterUsage;
    [SerializeField] private TextMeshProUGUI tmpWaterUsage1, tmpWaterUsage2;
    [SerializeField] private GameObject[] Pages;

    public int PastPageId = 0;
    public int TodayPageId = 1;
    public int FunPageId = 2;
    public int PreviousTabId = -1;
    public int CurrentTabId = -1;

    private void Awake()
    {
        foreach (GameObject page in Pages)
        {
            page.SetActive(false);
        }
    }

    private void Start()
    {
        SetTab(TodayPageId);
    }

    private void Update()
    {
        if (CurrentTabId != 1) return;
        print(CurrentTabId);
        tmpWaterUsage1.text = Mathf.RoundToInt(TodaysWaterUsage).ToString();
        tmpWaterUsage2.text = Mathf.RoundToInt(TodaysWaterUsage).ToString();
    }

    public void SetTab(int tabId)
    {
        PreviousTabId = CurrentTabId;
        CurrentTabId = tabId;
        
        if (PreviousTabId != -1)
            Pages[PreviousTabId].SetActive(false);
        Pages[CurrentTabId].SetActive(true);
    }
}
