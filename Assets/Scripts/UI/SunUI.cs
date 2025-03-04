using System;
using TMPro;
using UnityEngine;

public class SunUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sunText;


    private void Start()
    {
        StageManager.Instance.GetSunManager().OnSunChanged += SunManager_OnSunChanged;
        UpdateSunText();
    }

    private void SunManager_OnSunChanged(object sender, EventArgs e)
    {
        UpdateSunText();
    }

    private void UpdateSunText()
    {
        sunText.text = StageManager.Instance.GetSunManager().GetSunCount().ToString();
    }
}
