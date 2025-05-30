using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Operator : MonoBehaviour
{
    public CycleComponent cycle;
    public PepperStorage storage;
    public Shelter shelter;
    public UI_Controller ui_controller;

    private void Awake()
    {
        cycle = transform.parent.GetComponent<CycleComponent>();
        storage = transform.parent.GetComponent<PepperStorage>();
        shelter = transform.parent.GetComponent<Shelter>();
        ui_controller = transform.parent.parent.GetComponent<RaceController>().ui;
    }

    protected void RebootCycle()
    {
        cycle.SetDayRange(1);
        cycle.SetDayRange2(1);
        cycle.SetEveningRange(2);
        cycle.SetNightRange(1);
    }
}
