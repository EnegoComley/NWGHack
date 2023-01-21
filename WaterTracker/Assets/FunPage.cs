using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FunPage : MonoBehaviour
{
    [SerializeField] private WaterTracker _waterTracker;
    [SerializeField] private TextMeshProUGUI tmpPercent, tmpLitres;

    private float kilolitresOfElephant = 4;
    private void OnEnable()
    {
        float l = Mathf.RoundToInt(_waterTracker.TodaysWaterUsage * 365f / 1000f);
        tmpLitres.text = l.ToString();
        tmpPercent.text = Math.Round(l / kilolitresOfElephant, 2).ToString();
    }
}
