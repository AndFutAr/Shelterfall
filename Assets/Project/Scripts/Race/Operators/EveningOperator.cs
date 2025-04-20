using System.Collections;
using System.Collections.Generic;
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
    
    [SerializeField] private Vector3 base1Pos = new Vector3(-400, 0, 0), base2Pos = new Vector3(-25, 0, 0), elementPos = new Vector3(400, 0, 0);
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
        RerollBase();
        RerollElement();
    }
    public void RerollBase()
    {
        if (twistsBase > 0)
        {
            twistsBase--;
            storage.SpendPepper(priceBase);
            priceBase *= 2;
            Destroy(upgraderBase1);
            Destroy(upgraderBase2);
            
            Base1Ind = Random.Range(0, 60) / 10;
            Base2Ind = Random.Range(0, 60) / 10;

            switch (Base1Ind)
            {
                case 0: baseUpgrader1 = cycle.CycleData.qualityMaterials; upgraderBase1 = Instantiate(baseUpgraders[0], base1Pos, Quaternion.identity); break;
                case 1: baseUpgrader1 = cycle.CycleData.repairRobots; upgraderBase1 = Instantiate(baseUpgraders[1], base1Pos, Quaternion.identity); break;
                case 2: baseUpgrader1 = cycle.CycleData.boer; upgraderBase1 = Instantiate(baseUpgraders[2], base1Pos, Quaternion.identity); break;
                case 3: baseUpgrader1 = cycle.CycleData.artificialSun; upgraderBase1 = Instantiate(baseUpgraders[3], base1Pos, Quaternion.identity); break;
                case 4: baseUpgrader1 = cycle.CycleData.rightPeople; upgraderBase1 = Instantiate(baseUpgraders[4], base1Pos, Quaternion.identity); break;
                case 5: baseUpgrader1 = cycle.CycleData.majorRepairs; upgraderBase1 = Instantiate(baseUpgraders[5], base1Pos, Quaternion.identity); break;
            }
            switch (Base2Ind)
            {
                case 0: baseUpgrader2 = cycle.CycleData.qualityMaterials; upgraderBase2 = Instantiate(baseUpgraders[0], base2Pos, Quaternion.identity); break;
                case 1: baseUpgrader2 = cycle.CycleData.repairRobots; upgraderBase2 = Instantiate(baseUpgraders[1], base2Pos, Quaternion.identity); break;
                case 2: baseUpgrader2 = cycle.CycleData.boer; upgraderBase2 = Instantiate(baseUpgraders[2], base2Pos, Quaternion.identity); break;
                case 3: baseUpgrader2 = cycle.CycleData.artificialSun; upgraderBase2 = Instantiate(baseUpgraders[3], base2Pos, Quaternion.identity); break;
                case 4: baseUpgrader2 = cycle.CycleData.rightPeople; upgraderBase2 = Instantiate(baseUpgraders[4], base2Pos, Quaternion.identity); break;
                case 5: baseUpgrader2 = cycle.CycleData.majorRepairs; upgraderBase2 = Instantiate(baseUpgraders[5], base2Pos, Quaternion.identity); break;
            }

            upgraderBase1.transform.SetParent(Canvas.transform.GetChild(2).GetChild(3));
            upgraderBase2.transform.SetParent(Canvas.transform.GetChild(2).GetChild(3));
            
            upgraderBase1.transform.localPosition = base1Pos;
            upgraderBase2.transform.localPosition = base2Pos;
            
            upgraderBase1.GetComponent<BaseUpgraderComponent>().SetUpgraders(cycle);
            upgraderBase2.GetComponent<BaseUpgraderComponent>().SetUpgraders(cycle);
        }
    }
    public void RerollElement()
    {
        if (twistsElement > 0)
        {
            twistsElement--;
            storage.SpendPepper(priceElement);
            priceElement *= 2;
            Destroy(upgraderElement);
            
            ElementInd = Random.Range(0, 50) / 10;

            switch (ElementInd)
            {
                case 0: elementUpgrader = cycle.CycleData.meteorDefender; upgraderElement = Instantiate(elementUpgraders[0], elementPos, Quaternion.identity); break;
                case 1: elementUpgrader = cycle.CycleData.earthDefender; upgraderElement = Instantiate(elementUpgraders[1], elementPos, Quaternion.identity); break;
                case 2: elementUpgrader = cycle.CycleData.energyDefender; upgraderElement = Instantiate(elementUpgraders[2], elementPos, Quaternion.identity); break;
                case 3: elementUpgrader = cycle.CycleData.cyberDefender; upgraderElement = Instantiate(elementUpgraders[3], elementPos, Quaternion.identity); break;
                case 4: elementUpgrader = cycle.CycleData.fogDefender; upgraderElement = Instantiate(elementUpgraders[4], elementPos, Quaternion.identity); break;
            }
        }
        
        upgraderElement.transform.SetParent(Canvas.transform.GetChild(2).GetChild(3));
        upgraderElement.transform.localPosition = elementPos;
        
        upgraderElement.GetComponent<ElUpgraderComponent>().SetUpgraders(cycle);
    }
}
