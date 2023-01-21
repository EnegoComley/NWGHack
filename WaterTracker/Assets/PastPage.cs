using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class PastPage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmpToday, tmpYesterday, tmpThisWeek, tmpLastWeek, tmpThisMonth, tmpLastMonth;
    private void OnEnable()
    {
        WaterUsageObject waterUsageObject = new WaterUsageObject();
        tmpToday.text = Mathf.RoundToInt((float)waterUsageObject.water_usage["today"]/1000).ToString();
        tmpYesterday.text = Mathf.RoundToInt((float)waterUsageObject.water_usage["yesterday"]/1000).ToString();
        tmpThisWeek.text = Mathf.RoundToInt((float)waterUsageObject.water_usage["this_week"]/1000).ToString();
        tmpLastWeek.text = Mathf.RoundToInt((float)waterUsageObject.water_usage["last_week"]/1000).ToString();
        tmpThisMonth.text = Mathf.RoundToInt((float)waterUsageObject.water_usage["this_month"]/1000).ToString();
        tmpLastMonth.text = Mathf.RoundToInt((float)waterUsageObject.water_usage["last_month"]/1000).ToString();
    }

    private void OnDisable()
    {
       
    }
    
    private class WaterUsageObject
    {
        public int user_id;
        public Dictionary<string, double> water_usage;
            
        public WaterUsageObject()
        {
            int user_id = 1;
            water_usage = new Dictionary<string, double>()
            {
                { "today", 290069.3214111328 },
                { "yesterday", 502683.1423187256 },
                { "this_week", 2404583.258911133 },
                { "this_month", 7861082.977661133 },
                { "last_week", 2701560.65625 },
                { "last_month", 12363257.21875 }
            };
        }
    }
}
