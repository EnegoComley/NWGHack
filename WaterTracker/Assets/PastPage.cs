using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class PastPage : MonoBehaviour
{
    [SerializeField] private WaterTracker _waterTracker;
    [SerializeField]
    private TextMeshProUGUI tmpToday, tmpYesterday, tmpThisWeek, tmpLastWeek, tmpThisMonth, tmpLastMonth;
    private void OnEnable()
    {
        tmpToday.text = Mathf.RoundToInt(_waterTracker.TodaysWaterUsage).ToString();
        tmpYesterday.text = Mathf.RoundToInt((float)Convert.ToDouble(_waterTracker.jsonObj["yesterday"])/1000).ToString();
        tmpThisWeek.text = Mathf.RoundToInt((float)Convert.ToDouble(_waterTracker.jsonObj["this_week"])/1000).ToString();
        tmpLastWeek.text = Mathf.RoundToInt((float)Convert.ToDouble(_waterTracker.jsonObj["last_week"])/1000).ToString();
        tmpThisMonth.text = Mathf.RoundToInt((float)Convert.ToDouble(_waterTracker.jsonObj["this_month"])/1000).ToString();
        tmpLastMonth.text = Mathf.RoundToInt((float)Convert.ToDouble(_waterTracker.jsonObj["last_month"])/1000).ToString();
    }
}
