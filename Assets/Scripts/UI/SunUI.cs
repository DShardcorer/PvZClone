using System;
using TMPro;
using UnityEngine;

public class SunUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sunText;


    private void Start()
    {
        GameManager.Instance.GetSunManager().OnSunChanged += SunManager_OnSunChanged;
        UpdateSunText();
    }

    private void SunManager_OnSunChanged(object sender, EventArgs e)
    {
        UpdateSunText();
    }

    private void UpdateSunText()
    {
        sunText.text = GameManager.Instance.GetSunManager().GetSunCount().ToString();
    }
}
