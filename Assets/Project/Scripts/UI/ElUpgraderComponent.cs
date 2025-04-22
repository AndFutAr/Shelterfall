using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ElUpgraderComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CycleComponent cycle;
    private Dictionary<Int32, ElementUpgrader> _elementUpgraders;
    private ElementUpgrader _elUpgrader;
    
    [SerializeField] private int defenderNum;
    [SerializeField] private GameObject UpgraderInfo;
    [SerializeField] private TMP_Text textCost, textProcent;
    [SerializeField] private bool isClicked = false;
    public bool IsClicked => isClicked;

    public void SetUpgraders(CycleComponent _cycle)
    {
        cycle = _cycle;
        _elUpgrader = cycle.CycleData._ElDictionaries[defenderNum - 1];
        isClicked = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isClicked)
        {
            UpgraderInfo.SetActive(true);
            textCost.text = (_elUpgrader.Price * cycle.CycleData.costFactor).ToString();
            if (_elUpgrader.Count == 0) textProcent.text = "на 15%";
            else textProcent.text = "на 10%";
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClicked && cycle.gameObject.GetComponent<PepperStorage>().PepperCount >= _elUpgrader.Price)
        {
            isClicked = true;
            _elUpgrader.IncreaseUpgrader();
            cycle.transform.parent.GetComponent<RaceController>().ui.CloseRerollElement();
        }
    }

    public void OnPointerExit(PointerEventData eventData) => UpgraderInfo.SetActive(false);
}