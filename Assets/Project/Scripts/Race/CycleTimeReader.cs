using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CycleTimeReader: MonoBehaviour
{
    [SerializeField] private bool isDay = false, isEvening = false, isNight = false, isRacing = false;
    private float _dayTime = 10f;
    public void ChangeDayTime(float dayTime) => _dayTime = dayTime;

    public Action OnDayStart, OnEveningStart, OnNightStart, OnRacingClose;
    
    public bool IsDay() => isDay;
    public bool IsEvening() => isEvening;
    public bool IsNight() => isNight;
    public bool IsRacing() => isRacing;

    private void Update()
    {
        if(!isRacing) isDay = isEvening = isNight = false;
    }
    
    public void InverseRace()
    {
        if (isRacing)
        {
            isRacing = false;
            StopCoroutine(GoToDay());
            StopCoroutine(GoToNight());
            OnRacingClose?.Invoke();
        }
        else
        {
            isRacing = true;
            StartCoroutine(GoToDay());
        }
    }

    public void GoNight() => StartCoroutine(GoToNight());
    IEnumerator GoToNight()
    {
        isEvening = false;
        OnNightStart?.Invoke();
        yield return new WaitForSeconds(2);
        isNight = true;
    }

    public void GoDay() => StartCoroutine(GoToDay());
    IEnumerator GoToDay()
    {
        while (true)
        {
            isNight = false;
            OnDayStart?.Invoke();
            yield return new WaitForSeconds(2);
            isDay = true;
            yield return new WaitForSeconds(_dayTime);
            isDay = false;
            OnEveningStart?.Invoke();
            yield return new WaitForSeconds(2);
            isEvening = true;
            break;
        }
    }
}
