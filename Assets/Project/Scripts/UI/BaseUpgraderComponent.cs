﻿using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BaseUpgraderComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CycleComponent cycle;
    private Dictionary<Int32, BaseUpgrader> _baseUpgraders;
    private BaseUpgrader _baseUpgrader;
    
    [SerializeField] private int defenderNum;
    [SerializeField] private GameObject UpgraderInfo;
    [SerializeField] private TMP_Text textCost, textProcent;
    [SerializeField] private string procent1, defProcent;
    [SerializeField] private bool isClicked;
    public bool IsClicked => isClicked;

    public void SetUpgraders(CycleComponent _cycle)
    {
        cycle = _cycle;
        _baseUpgrader = cycle.CycleData._BaseDictionaries[defenderNum - 1];
        isClicked = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            UpgraderInfo.SetActive(true);
            textCost.text = ((int)(_baseUpgrader.Price * cycle.CycleData.costFactor * 10)/10).ToString();
            if (_baseUpgrader.Count == 0) textProcent.text = procent1;
            else textProcent.text = defProcent;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClicked && cycle.gameObject.GetComponent<PepperStorage>().PepperCount >= _baseUpgrader.Price)
        {
            isClicked = true;
            Debug.Log(_baseUpgrader);
            _baseUpgrader.IncreaseUpgrader();
        }
    }
    public void OnPointerExit(PointerEventData eventData) => UpgraderInfo.SetActive(false);
}