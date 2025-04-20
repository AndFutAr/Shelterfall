using System;
using UnityEngine;

public class PepperStorage : MonoBehaviour
{
    private float _pepperCount;
    public float PepperCount => (float)Math.Round(_pepperCount, 2);
    private float costFactor = 1;

    public Action<float> OnPepperEarned;
    public Action<float> OnPepperSpent;

    private void Awake()
    {
        _pepperCount = 0;
        costFactor = 1;
    }
    public void SetupPepper(float pepperCount)
    {
        _pepperCount = pepperCount;
    }
    public void GetFactor(float factorDiff) => costFactor = factorDiff;
    
    public void AddPepper(float range)
    {
        _pepperCount += range * transform.GetComponent<CycleComponent>().RaceData.PepperFactor;
        OnPepperEarned?.Invoke(PepperCount);
        transform.GetComponent<CycleComponent>().CycleData.allPepper += range * transform.GetComponent<CycleComponent>().RaceData.PepperFactor;
        transform.GetComponent<CycleComponent>().CycleData.lastPepper = _pepperCount;
    }
    public void SpendPepper(float range)
    {
        _pepperCount -= costFactor * range;
        OnPepperSpent?.Invoke(PepperCount);
    }

    public void TreftPepper(float range)
    {
        _pepperCount -= (int)(_pepperCount * range);
    }
}