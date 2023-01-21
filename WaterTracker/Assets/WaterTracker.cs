using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

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

    public Dictionary<string, string> jsonObj;

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
        
        // A correct website page.
        StartCoroutine(GetRequest("http://20.90.118.142:7071/api/get-water-usage?user_id=1"));
    }
    
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string jsonString = webRequest.downloadHandler.text;
                    Debug.Log(pages[page] + ":\nReceived: " + jsonString);
                    jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                    TodaysWaterUsage = (float)Math.Round(Convert.ToDouble(jsonObj["today"])/1000, 2);
                    break;
            }
        }
    }

    private void Update()
    {
        if (CurrentTabId != 1) return;
        tmpWaterUsage1.text = TodaysWaterUsage.ToString();
        tmpWaterUsage2.text = TodaysWaterUsage.ToString();
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

public class WaterUsageObject
{
    public int user_id;
    public Dictionary<string, double> water_usage;
}