using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public RaceController controller;
    public CycleTimeReader timeReader;
    public CycleComponent cycle;
    public Camera camera, startCamera;

    [SerializeField] private GameObject startMenu, gameMenu, setMenu, progressMenu, lastMenu;
    [SerializeField] private GameObject dayUI, eveningUI, nightUI;
    [SerializeField] private Animation animDay, animEvening, animNight;

    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private Slider raceScore;
    
    [SerializeField] private GameObject rerollBase, rerollElement, getCardsButton;
    
    [SerializeField] private GameObject rollDis, rerollDis, makeBidBut, notMakeBidBut;
    [SerializeField] private GameObject[] bidButtons = new GameObject[5];
    [SerializeField] private GameObject[] _nightResults = new GameObject[10];
    int disasterNum = 10;

    [SerializeField] private GameObject canvas;
    public GameObject Canvas() => canvas;

    public void SetUI(CycleComponent _cycle)
    {
        cycle = _cycle;
    }
    private void OnEnable()
    {
        timeReader.OnDayStart += StartDay;
        timeReader.OnEveningStart += StartEvening;
        timeReader.OnNightStart += StartNight;

        timeReader.OnRacingClose += RaceOver;
    }
    private void Start()
    {
        startMenu.SetActive(true);
        gameMenu.SetActive(false);
        startCamera.gameObject.SetActive(true);
        camera.gameObject.SetActive(false);
        animDay.transform.GetComponent<CinemachineDollyCart>().m_Position = 0;
        raceScore.value = controller.RaceData.BestRace / 50.0f;
    }

    private void Update()
    {
        bestScore.text = controller.RaceData.BestRace.ToString();
    }

    public void GetUpgraders(GameObject but)
    {
        cycle.transform.GetChild(0).GetComponent<EveningOperator>().GetCards();
        getCardsButton = but;
        getCardsButton.SetActive(false);
        if(cycle.CycleData.eveningRange > 0) { rerollBase.SetActive(true); rerollElement.SetActive(true); }
    }
    public void RerollBase()
    {
        cycle.transform.GetChild(0).GetComponent<EveningOperator>().RerollBase(false);
        if(cycle.transform.GetChild(0).GetComponent<EveningOperator>().TwistsBase <= 0) rerollBase.SetActive(false);
    }
    public void RerollElement()
    {
        cycle.transform.GetChild(0).GetComponent<EveningOperator>().RerollElement(false);
        if(cycle.transform.GetChild(0).GetComponent<EveningOperator>().TwistsElement <= 0) rerollElement.SetActive(false);
    }
    public void CloseRerollBase() => rerollBase.SetActive(false);
    public void CloseRerollElement() => rerollElement.SetActive(false);
    
    public void OpenSet(GameObject _lastMenu)
    {
        setMenu.SetActive(true);
        startMenu.SetActive(false);
        gameMenu.SetActive(false);
        lastMenu = _lastMenu;
    }
    public void OpenProgress()
    {
        progressMenu.SetActive(true);
        startMenu.SetActive(false);
        lastMenu = startMenu;
    }
    public void Back()
    {
        setMenu.SetActive(false);
        progressMenu.SetActive(false);
        lastMenu.SetActive(true);
    }
    public void Exit()
    {
        setMenu.SetActive(false);   
        RaceOver();
        timeReader.InverseRace();
    }
    public void StartGame()
    {
        cycle.transform.parent.GetComponent<RaceController>().RaceData.IsRace = 1;
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        dayUI.SetActive(false);
        eveningUI.SetActive(false);
        nightUI.SetActive(false);
        startCamera.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);
        animDay.transform.GetComponent<CinemachineDollyCart>().m_Position = 0;
    }
    public void RaceOver()
    {
        gameMenu.SetActive(false);
        startMenu.SetActive(true);
        startCamera.gameObject.SetActive(true);
        camera.gameObject.SetActive(false);
        if (cycle.CycleData.cycleNum > controller.RaceData.BestRace) controller.RaceData.BestRace = cycle.CycleData.cycleNum;
        bestScore.text = controller.RaceData.BestRace.ToString();
        raceScore.value = controller.RaceData.BestRace / 50.0f;
    }
    public void BuyUpgrade(int t) => controller.UpgradeProgress(t);

    public void StartDay() => StartCoroutine(DayStart());
    public void StartEvening() => StartCoroutine(EveningStart());
    public void StartNight() => StartCoroutine(NightStart());
    IEnumerator DayStart()
    {
        for(int i = 0; i < 10; i++) _nightResults[i].SetActive(false);
        nightUI.SetActive(false);
        camera.transform.SetParent(animDay.transform);
        animDay.Play();
        yield return new WaitForSeconds(2f);
        dayUI.SetActive(true);
        animNight.transform.GetComponent<CinemachineDollyCart>().m_Position = 2;
    }
    IEnumerator EveningStart()
    {
        dayUI.SetActive(false);
        camera.transform.SetParent(animEvening.transform);
        animEvening.Play();
        yield return new WaitForSeconds(2f);
        eveningUI.SetActive(true);
        getCardsButton.SetActive(true);
        animDay.transform.GetComponent<CinemachineDollyCart>().m_Position = 5;
    }
    IEnumerator NightStart()
    {
        rerollBase.SetActive(false);
        rerollElement.SetActive(false);
        eveningUI.SetActive(false);
        
        camera.transform.SetParent(animNight.transform);
        animNight.Play();
        
        yield return new WaitForSeconds(2f);
        nightUI.SetActive(true);
        for(int i = 0; i < 5; i++) bidButtons[i].SetActive(true);
        makeBidBut.SetActive(true);
        notMakeBidBut.SetActive(true);
        rollDis.SetActive(false);
        rerollDis.SetActive(false);
        
        animEvening.transform.GetComponent<CinemachineDollyCart>().m_Position = 0;
    }
    
    public void GoToNight() => timeReader.GoNight();
    public void GoToDay() => timeReader.GoDay();
    
    public void ChooseBid(int type) => cycle.transform.GetChild(0).GetComponent<NightOperator>().ChooseBid(type);
    public void MakeBid()
    {
        makeBidBut.SetActive(false);
        notMakeBidBut.SetActive(false);
        rollDis.SetActive(true);
        cycle.transform.GetChild(0).GetComponent<NightOperator>().Bid();
        for(int i = 0; i < 5; i++) bidButtons[i].SetActive(false);
    }
    public void NotMakeBid()
    {
        makeBidBut.SetActive(false);
        notMakeBidBut.SetActive(false);
        rollDis.SetActive(true);
        for(int i = 0; i < 5; i++) bidButtons[i].SetActive(false);
    }
    public void StartWheel()
    {
        rollDis.SetActive(false);
        rerollDis.SetActive(false);
        int chance = UnityEngine.Random.Range(0, 100);
        disasterNum = chance / 20;
        while (disasterNum == cycle.CycleData.lastDisaster)
        {
            chance = UnityEngine.Random.Range(0, 100);
            disasterNum = chance / 20;
        }
        cycle.transform.GetChild(0).GetComponent<NightOperator>().SpinTheWheel(disasterNum);
        if (cycle.CycleData.cycleNum % 5 != 0 && cycle.RaceData.IsRerollDef == 1) StartCoroutine(RespinSpawn());
        else if (cycle.CycleData.cycleNum % 5 == 0 && cycle.RaceData.IsRerollBoss == 1) StartCoroutine(RespinSpawn());
        else StartCoroutine(NightResult(disasterNum));
    }
    public void RespinWheel()
    {
        StopCoroutine(NightResult(disasterNum));
        StopCoroutine(RespinSpawn());
        rerollDis.SetActive(false);
        if (cycle.RaceData.IsRerollDef == 1 && cycle.CycleData.cycleNum % 5 != 0)
        {
            cycle.RaceData.IsRerollDef = 0;
            StartWheel();
        }
        else if (cycle.RaceData.IsRerollBoss == 1 && cycle.CycleData.cycleNum % 5 == 0)
        {
            cycle.RaceData.IsRerollBoss = 0;
            StartWheel();
        }
    }
    IEnumerator RespinSpawn()
    {
        StartCoroutine(NightResult(disasterNum));
        yield return new WaitForSeconds(2f);
        rerollDis.SetActive(true);
        yield return new WaitForSeconds(2f);
        rerollDis.SetActive(false);
    }
    IEnumerator NightResult(int disasterNum)
    {
        yield return new WaitForSeconds(4f);
        if (cycle.CycleData.cycleNum % 5 != 0) _nightResults[disasterNum].SetActive(true);
        else _nightResults[disasterNum + 5].SetActive(true);
    }
}