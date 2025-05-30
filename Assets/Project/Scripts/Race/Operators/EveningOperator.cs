using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class EveningOperator : Operator
{
    [SerializeField] private int twistsBase = 1, twistsElement = 1;
    public int TwistsBase => twistsBase;
    public int TwistsElement => twistsElement;
    [SerializeField] private int Base1Ind, Base2Ind, ElementInd;
    
    private BaseUpgrader baseUpgrader1, baseUpgrader2;
    private ElementUpgrader elementUpgrader;
    private GameObject upgraderBase1, upgraderBase2, upgraderElement;
    private int priceBase = 10, priceElement = 10;
    
    [SerializeField] private Vector3 st1Pos = new Vector3(-330, 220, 0), st2Pos = new Vector3(-75, 220, 0), elstPos = new Vector3(200, 220, 0);
    [SerializeField] private Vector3 base1Pos = new Vector3(-330, 220, 0), base2Pos = new Vector3(-75, 220, 0), elementPos = new Vector3(200, 220, 0);
    [SerializeField] private GameObject[] baseUpgraders = new GameObject[6];
    [SerializeField] private GameObject[] elementUpgraders = new GameObject[5];

    [SerializeField] private GameObject Canvas;
    public void SetCanvas(GameObject _canvas) => Canvas = _canvas;

    public void DestroyCards()
    {
        Destroy(upgraderBase1);
        Destroy(upgraderBase2);
        Destroy(upgraderElement);
    }
    
    public void SetOperator(float range)
    {
        twistsBase = (int)range + 1;
        twistsElement = (int)range + 1;
    }
    void Start()
    {
        storage = transform.parent.GetComponent<PepperStorage>();
        shelter = transform.parent.GetComponent<Shelter>();

        priceBase = 10 + (cycle.CycleData.cycleNum - 1) * 2;
    }

    public void GetCards()
    {
        RerollBase(true);
        RerollElement(true);
    }
    public void RerollBase(bool first)
    {
        if (twistsBase > 0 && (storage.PepperCount >= priceBase || first))
        {
            twistsBase--;
            if (!first)
            {
                storage.SpendPepper(priceBase);
                priceBase *= 2;
                Destroy(upgraderBase1);
                Destroy(upgraderBase2);
            }
            
            Base1Ind = Random.Range(0, 60) / 10;
            Base2Ind = Random.Range(0, 60) / 10;

            baseUpgrader1 = cycle.CycleData._BaseDictionaries[Base1Ind];
            upgraderBase1 = Instantiate(baseUpgraders[Base1Ind], base1Pos, Quaternion.identity);
            
            baseUpgrader2 = cycle.CycleData._BaseDictionaries[Base2Ind];
            upgraderBase2 = Instantiate(baseUpgraders[Base2Ind], base2Pos, Quaternion.identity);

            upgraderBase1.transform.SetParent(Canvas.transform.GetChild(2).GetChild(4));
            upgraderBase2.transform.SetParent(Canvas.transform.GetChild(2).GetChild(4));
            
            upgraderBase1.transform.localPosition = st1Pos;
            upgraderBase2.transform.localPosition = st2Pos;
            upgraderBase1.transform.DOLocalMove(base1Pos, 0.5f);
            upgraderBase2.transform.DOLocalMove(base2Pos, 0.5f);
            
            upgraderBase1.GetComponent<BaseUpgraderComponent>().SetUpgraders(cycle);
            upgraderBase2.GetComponent<BaseUpgraderComponent>().SetUpgraders(cycle);
        }
    }
    public void RerollElement(bool first)
    {
        if (twistsElement > 0 && (storage.PepperCount >= priceElement || first))
        {
            twistsElement--;
            if (!first)
            {
                storage.SpendPepper(priceElement);
                priceElement *= 2;
                Destroy(upgraderElement);
            }

            ElementInd = Random.Range(0, 50) / 10;

            elementUpgrader = cycle.CycleData._ElDictionaries[ElementInd];
            upgraderElement = Instantiate(elementUpgraders[ElementInd], elementPos, Quaternion.identity);

            upgraderElement.transform.SetParent(Canvas.transform.GetChild(2).GetChild(4));
            upgraderElement.transform.localPosition = elstPos;
            upgraderElement.transform.DOLocalMove(elementPos, 0.5f);
            

            upgraderElement.GetComponent<ElUpgraderComponent>().SetUpgraders(cycle);
        }
    }
}
