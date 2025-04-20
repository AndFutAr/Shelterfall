using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
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
    
    [SerializeField] private GameObject rerollBase, rerollElement, getCardsButton;
    [SerializeField] private GameObject rerollDis;

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
        cycle.transform.GetChild(0).GetComponent<EveningOperator>().RerollBase();
        if(cycle.transform.GetChild(0).GetComponent<EveningOperator>().TwistsBase <= 0) rerollBase.SetActive(false);
    }
    public void RerollElement()
    {
        cycle.transform.GetChild(0).GetComponent<EveningOperator>().RerollElement();
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
        bestScore.text = controller.RaceData.BestRace.ToString();
    }
    public void BuyUpgrade(int t) => controller.UpgradeProgress(t);

    public void StartDay() => StartCoroutine(DayStart());
    public void StartEvening() => StartCoroutine(EveningStart());
    public void StartNight() => StartCoroutine(NightStart());
    IEnumerator DayStart()
    {
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
        if (cycle.CycleData.cycleNum % 5 != 0 && cycle.RaceData.IsRerollDef == 1) StartCoroutine(RespinSpawn());
        if (cycle.CycleData.cycleNum % 5 == 0 && cycle.RaceData.IsRerollBoss == 1) StartCoroutine(RespinSpawn());
        
        animEvening.transform.GetComponent<CinemachineDollyCart>().m_Position = 0;
    }
    
    public void GoToNight() => timeReader.GoNight();
    public void GoToDay() => timeReader.GoDay();
    
    public void MakeBid(int type)
    {
        int chance = UnityEngine.Random.Range(0, 100);
        int disasterNum = chance / 20;
        cycle.transform.GetChild(0).GetComponent<NightOperator>().Bid(type, disasterNum);
    }
    public void NotMakeBid()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        int disasterNum = chance / 20;
        cycle.transform.GetChild(0).GetComponent<NightOperator>().SpinTheWheel(disasterNum);
    }
    public void RespinWheel()
    {
        if (cycle.RaceData.IsRerollDef == 1 && cycle.CycleData.cycleNum % 5 != 0)
        {
            NotMakeBid();
            cycle.RaceData.IsRerollDef = 0;
        }
        else if (cycle.RaceData.IsRerollBoss == 1 && cycle.CycleData.cycleNum % 5 == 0)
        {
            NotMakeBid();
            cycle.RaceData.IsRerollBoss = 0;
        }
    }
    IEnumerator RespinSpawn()
    { 
        rerollDis.SetActive(true);
        yield return new WaitForSeconds(2f);
        rerollDis.SetActive(false);
    }
}