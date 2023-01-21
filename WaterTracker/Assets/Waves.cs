using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private WaterTracker _waterTracker;
    
    [SerializeField] private float _speed, _magnitude;
    private RectTransform _rt;
    [SerializeField] private RectTransform _rtWaterUsage, _rtTodaysUsage;

    [SerializeField] private float _minPos, _maxPos;
    [SerializeField] private float _minWaterUsage, _maxWaterUsage;
    
    private float _startPos;
    private float _targetY;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_waterTracker.CurrentTabId == _waterTracker.TodayPageId)
        {
            _targetY = (-Screen.height / 2f + _minPos) +
                       (_maxPos - _minPos) *
                       ClampedPercentage(_minWaterUsage, _maxWaterUsage, _waterTracker.TodaysWaterUsage) +
                       Mathf.Sin(Time.time * _speed) * _magnitude;
            
            _rtWaterUsage.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            _rtTodaysUsage.position = new Vector3(Screen.width / 2f, Screen.height / 2f + 250f, 0f);
            _rtWaterUsage.gameObject.SetActive(true);
            _rtTodaysUsage.gameObject.SetActive(true);
        }
        else
        {
            _targetY = (-Screen.height / 2f + _minPos) +
                       Mathf.Sin(Time.time * _speed) * _magnitude;
            
            _rtWaterUsage.gameObject.SetActive(false);
            _rtTodaysUsage.gameObject.SetActive(false);
        }

        _rt.transform.position = new Vector3(Screen.width / 2f, _targetY, 0f);
    }

    private float ClampedPercentage(float min, float max, float val)
    {
        if (val <= min)
            return 0;

        if (val >= max)
            return 1;

        return (val - min) / (max - min);
    }
}
