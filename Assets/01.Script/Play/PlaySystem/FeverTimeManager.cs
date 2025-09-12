using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeverTimeManager : MonoBehaviour
{
    public Slider feverTimeSlider;
    public TMP_Text feverTimeText;
    public float feverTime = 0f;
    public float feverTimeMax = 10f;
    
    private bool mbFeverTime = false;
    
    ClampedFloat feverGaugeClamped;
    
    private void Awake()
    {
        feverGaugeClamped = new ClampedFloat(0, 100, 0);
        feverGaugeClamped.Events.OnValueChanged += UpdateFiverGaugeBar;
        InGameEventHandler.Instance.GyroShakeHandler += GainFeverGauge;
    }

    public void GainFeverGauge(float value)
    {
        if (mbFeverTime == false)
        {
            feverGaugeClamped.Increase(value);

            if (!feverGaugeClamped.IsMax) return;
            mbFeverTime = true;
            StartCoroutine(StartFever());
        }
    }

    public void UpdateFiverGaugeBar(float _, float __)
    {
        feverTimeSlider.value = feverGaugeClamped.Ratio;
    }

    public IEnumerator StartFever()
    {
        feverTimeText.gameObject.SetActive(true);
        StartCoroutine(EnSleep(() => feverTimeText.gameObject.SetActive(false)));
        
        while (feverTime < feverTimeMax)
        {
            float t = Mathf.Clamp01(feverTime / feverTimeMax);
            feverGaugeClamped.Lerp(t);
            
            feverTime += Time.deltaTime;
        }
        
        mbFeverTime = false;
        yield return null;
    }

    public IEnumerator EnSleep(Action callback)
    {
        yield return new WaitForSeconds(1f);
        callback?.Invoke();
    }
}