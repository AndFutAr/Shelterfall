using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CycleTimeReader: MonoBehaviour
{
    [SerializeField] private bool isDay = false, isEvening = false, isNight = false, isRacing = false;
    private float _dayTime = 10f;
    
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
        if(isRacing) isRacing = false;
        else
        {
            isRacing = true;
            StartCoroutine(GoDay());
        }
    }
    public void GoNight()
    {
        isEvening = false;
        isNight = true;
    }

    public void FinalNight() => StartCoroutine(GoDay());
    IEnumerator GoDay()
    {
        while (true)
        {
            isNight = false;
            isDay = true;
            yield return new WaitForSeconds(_dayTime);
            isDay = false;
            isEvening = true;
        }
    }
}
