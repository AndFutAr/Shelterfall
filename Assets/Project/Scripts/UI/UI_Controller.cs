using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public RaceController controller;
    public CycleTimeReader timeReader;
    public CycleComponent cycle;
    public Camera camera, startCamera;

    [SerializeField] private GameObject startMenu, gameMenu, setMenu, progressMenu, raceOverMenu, lastMenu, enchantmentMenu;

    private bool isEnchant = false, isUsed = false;
    [SerializeField] private GameObject dayUI, eveningUI, nightUI, windowUI, baseEnchantsMenu, elEnchantsMenu;
    [SerializeField] private GameObject dayIcon, eveningIcon, nightIcon;
    [SerializeField] private Vector3 lastIconPos, curIconPos, nextIconPos;
    [SerializeField] private GameObject animDayPrefab, animEveningPrefab, animNightPrefab;
    private Animation animDay, animEvening, animNight;
    [SerializeField] private CinemachineSmoothPath _path;
    
    [SerializeField] private GameObject location, postProgression, pepppers0;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private Slider raceScore;
    [SerializeField] private GameObject ExitButton;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioSource[] sounds;
    public float SoundVolume => soundSlider.value;
    
    [SerializeField] private TMP_Text ResultDaysText, ResultPeppersText, RewardFragmentsText;
    private int fragmentFactor = 1;
    
    [SerializeField] private GameObject PeCoinInc;
    
    [SerializeField] private GameObject rerollBase, rerollElement, getCardsButton;
    [SerializeField] private TMP_Text bidDisText, bidSizeText;
    [SerializeField] private GameObject wheel;
    [SerializeField] private Animation baseWheelAnim, bossWheelAnim;
    [SerializeField] private AnimationClip[] baseWheelAnimClips, bossWheelAnimClips;
    private int bidSize = 0;
    private bool isBid = false;
    
    [SerializeField] private GameObject rollDis, rerollDis, makeBidBut, notMakeBidBut;
    [SerializeField] private GameObject bidButtons;
    [SerializeField] private GameObject[] _nightResults = new GameObject[10];
    int disasterNum = 10;

    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text dayText, dayTimeText;
    [SerializeField] private GameObject lightning;
    
    [SerializeField] private GameObject ProgressionIcon;
    [SerializeField] private TMP_Text fragmentText, allFragText;
    
    public GameObject Canvas() => canvas;

    public void SetUI(CycleComponent _cycle) => cycle = _cycle;    
    private void OnEnable()
    {
        timeReader.OnDayStart += StartDay;
        timeReader.OnEveningStart += StartEvening;
        timeReader.OnNightStart += StartNight;

        timeReader.OnRacingClose += RaceOver;
    }
    private void Start()
    {
        soundSlider.value = controller.RaceData.SoundVolume;
        
        animDay = Instantiate(animDayPrefab, transform).GetComponent<Animation>();
        animDay.gameObject.GetComponent<CinemachineDollyCart>().m_Path = _path;
        animEvening = Instantiate(animEveningPrefab, transform).GetComponent<Animation>();
        animEvening.gameObject.GetComponent<CinemachineDollyCart>().m_Path = _path;
        animNight = Instantiate(animNightPrefab, transform).GetComponent<Animation>();
        animNight.gameObject.GetComponent<CinemachineDollyCart>().m_Path = _path;
        
        startMenu.SetActive(true);
        gameMenu.SetActive(false);
        startCamera.gameObject.SetActive(true);
        camera.gameObject.SetActive(false);
        raceScore.value = controller.RaceData.BestRace / 50.0f;
    }

    private void Update()
    {
        bestScore.text = controller.RaceData.BestRace.ToString();
        fragmentText.text = controller.RaceData.PepperFragments.ToString();
        for(int i = 0; i < 5; i++) sounds[i].volume = soundSlider.value;
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

    public void OpenEnchants(GameObject button)
    {
        if (!isUsed) StartCoroutine(Enchantments(button));
    }
    IEnumerator Enchantments(GameObject button)
    {
        isUsed = true;
        if (!isEnchant)
        {
            enchantmentMenu.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            windowUI.transform.DOLocalMove(new Vector3(0, 1080, 0), 0.5f);
            dayUI.transform.DOLocalMove(new Vector3(0, -1080, 0), 0.5f);
            eveningUI.transform.DOLocalMove(new Vector3(0, -1080, 0), 0.5f);
            nightUI.transform.DOLocalMove(new Vector3(0, -1080, 0), 0.5f);
            yield return new WaitForSeconds(0.5f);
            baseEnchantsMenu.transform.DOLocalMove(new Vector3(-420, 0, 0), 0.5f);
            elEnchantsMenu.transform.DOLocalMove(new Vector3(410, 0, 0), 0.5f);
            button.transform.DOLocalMove(new Vector3(-570, -460, 0), 0.5f);
            button.transform.localScale = new Vector3(-1f, 1f, 1f);
            yield return new WaitForSeconds(0.5f);
            isEnchant = true;
            isUsed = false;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            button.transform.localScale = new Vector3(1f, 1f, 1f);
            button.transform.DOLocalMove(new Vector3(-840, -460, 0), 0.5f);
            baseEnchantsMenu.transform.DOLocalMove(new Vector3(-960, 0, 0), 0.5f);
            elEnchantsMenu.transform.DOLocalMove(new Vector3(960, 0, 0), 0.5f);
            yield return new WaitForSeconds(0.5f);
            enchantmentMenu.SetActive(false);
            windowUI.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            dayUI.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            eveningUI.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            nightUI.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            yield return new WaitForSeconds(0.5f);
            isEnchant = false;
            isUsed = false;
        }
    }
    public void OpenSet(GameObject _lastMenu)
    {
        pepppers0.SetActive(true);
        setMenu.SetActive(true);
        startMenu.SetActive(false);
        gameMenu.SetActive(false);
        lastMenu = _lastMenu;
        if(lastMenu == startMenu) ExitButton.SetActive(false);
        else ExitButton.SetActive(true);
    }
    public void OpenProgress()
    {
        fragmentText.text = controller.RaceData.PepperFragments.ToString();
        postProgression.SetActive(true);
        location.SetActive(false);
        progressMenu.SetActive(true);
        startMenu.SetActive(false);
        lastMenu = startMenu;
        ProgressionIcon.SetActive(false);
    }
    public void Back()
    {
        pepppers0.SetActive(false);
        location.SetActive(true);
        postProgression.SetActive(false);
        setMenu.SetActive(false);
        progressMenu.SetActive(false);
        lastMenu.SetActive(true);
        controller.SaveRace();
    }
    public void StartGame() => StartCoroutine(GameStart());
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.1f);
        gameMenu.SetActive(true);
        dayUI.SetActive(false);
        eveningUI.SetActive(false);
        nightUI.SetActive(false);
        gameMenu.transform.localPosition = new Vector3(0, 1080, 0);
        startMenu.transform.DOLocalMove(new Vector3(0, -1080, 0), 1f);
        gameMenu.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
        startCamera.transform.DOLocalMove(camera.transform.position, 1f);
        startCamera.transform.DORotate(camera.transform.rotation.eulerAngles, 1f);
        yield return new WaitForSeconds(1f);
        startMenu.SetActive(false);
        startCamera.gameObject.SetActive(false);
        camera.gameObject.SetActive(true); 
        timeReader.InverseRace();
    }
    public void IncreasePepCoin(float count, Vector3 mousePosition)
    {
        GameObject PeCoin = Instantiate(PeCoinInc, mousePosition, PeCoinInc.transform.rotation);
        PeCoin.transform.GetComponent<TextMeshPro>().text = "+" + count;
        PeCoin.transform.parent = canvas.transform;
        PeCoin.transform.position = mousePosition;
    }

    public void RaceOver()
    {
        if(!isEnchant) if(!isUsed) StartCoroutine(FinalRace());
        ResultDaysText.text = cycle.CycleData.cycleNum.ToString();
        ResultPeppersText.text = ((int)cycle.CycleData.allPepper).ToString();
        RewardFragmentsText.text =
            (fragmentFactor * (int)(cycle.CycleData.cycleNum * 5 + cycle.CycleData.allPepper * 0.1f)).ToString();
        allFragText.text = controller.RaceData.PepperFragments + " (+" +  RewardFragmentsText.text + ") осколков";
    }
    IEnumerator FinalRace()
    {
        isEnchant = true;
        isUsed = true;
        yield return new WaitForSeconds(0.1f);
        gameMenu.transform.DOLocalMove(new Vector3(0, 1080, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameMenu.SetActive(false);
        raceOverMenu.SetActive(true);
        raceOverMenu.transform.localPosition = new Vector3(0, 1080, 0);
        raceOverMenu.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isUsed = false;
    }
    public void DoubleReward(GameObject button)
    {
        button.SetActive(false);
        fragmentFactor = 2;
        RewardFragmentsText.text =
            (fragmentFactor * (int)(cycle.CycleData.cycleNum * 5 + cycle.CycleData.allPepper * 0.1f)).ToString();
        allFragText.text = controller.RaceData.PepperFragments + " (+" +  RewardFragmentsText.text + ") осколков";
    }
    public void CloseGame() => controller.LoseRace(fragmentFactor);
    public void ReturnInGame(GameObject buttonsMenu)
    {
        if (controller.RaceData.PepperFragments >= 50)
        {
            controller.RaceData.PepperFragments -= 50;
            StartCoroutine(ReturnGame(buttonsMenu));
        }
    }
    IEnumerator ReturnGame(GameObject buttonsMenu)
    {
        yield return new WaitForSeconds(0.1f);
        isEnchant = false;
        cycle.GetComponent<Shelter>().SetShelter(cycle.CycleData.maxHP, cycle.CycleData.maxHP / 2);
        gameMenu.SetActive(true);
        raceOverMenu.transform.DOLocalMove(new Vector3(0, -1080, 0), 0.5f);
        gameMenu.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
        for(int i = 0; i <= 1; i++) buttonsMenu.transform.GetChild(i).gameObject.SetActive(false);
        buttonsMenu.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        raceOverMenu.SetActive(false);
    }

    public void RaceExit()
    {
        controller.SaveRace();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private int t = -1;

    public void ChooseUpgrade(int type)
    {
        t = type;
        if(t == -1) ProgressionIcon.SetActive(false);
        else ProgressionIcon.SetActive(true);
    }
    public void BuyUpgrade()
    {
        if (t != -1)
        {
            ProgressionIcon.SetActive(false);
            controller.UpgradeProgress(t);
        }
    }

    public void StartDay() => StartCoroutine(DayStart());
    public void StartEvening() => StartCoroutine(EveningStart());
    public void StartNight() => StartCoroutine(NightStart());
    IEnumerator DayStart()
    {
        windowUI.SetActive(true);
        lightning.transform.rotation = Quaternion.Euler(-90, 30, 0);
        for(int i = 0; i < 10; i++) _nightResults[i].SetActive(false);
        nightUI.SetActive(false);
        
        camera.transform.SetParent(animDay.transform);
        animNight.GetComponent<CinemachineDollyCart>().m_Position = 2;
        animDay.Play();
        lightning.transform.DORotate(new Vector3(90, 30, 0), 2f);
        nightIcon.transform.DOLocalMove(lastIconPos, 0.5f);
        dayIcon.transform.DOLocalMove(curIconPos, 0.5f);
        eveningIcon.transform.DOLocalMove(nextIconPos, 0.5f);
        
        yield return new WaitForSeconds(2f);
        dayText.text = (cycle.CycleData.cycleNum + 1).ToString();
        dayTimeText.text = "День";
        dayUI.SetActive(true);
            
        bidSize = 0;
        bidSizeText.text = bidSize.ToString();
        bidDisText.text = "-";
        wheel.transform.rotation = Quaternion.Euler(-90, 90, 90);
        if ((cycle.CycleData.cycleNum + 1) % 5 == 0 && cycle.CycleData.cycleNum >= 4)
            wheel.transform.rotation = Quaternion.Euler(-90, 90, -90);
    }
    IEnumerator EveningStart()
    {
        lightning.transform.rotation = Quaternion.Euler(90, 30, 0);
        dayUI.SetActive(false);
        
        camera.transform.SetParent(animEvening.transform);
        animDay.GetComponent<CinemachineDollyCart>().m_Position = 5;
        animEvening.Play();
        lightning.transform.DORotate(new Vector3(50, 30, 0), 2f);
        dayIcon.transform.DOLocalMove(lastIconPos, 0.5f);
        eveningIcon.transform.DOLocalMove(curIconPos, 0.5f);
        nightIcon.transform.DOLocalMove(nextIconPos, 0.5f);
        
        yield return new WaitForSeconds(2f);
        dayTimeText.text = "Вечер";
        eveningUI.SetActive(true);
        getCardsButton.SetActive(true);
    }
    IEnumerator NightStart()
    {
        lightning.transform.rotation = Quaternion.Euler(50, 30, 0);
        rerollBase.SetActive(false);
        rerollElement.SetActive(false);
        eveningUI.SetActive(false);
        
        camera.transform.SetParent(animNight.transform);
        animEvening.GetComponent<CinemachineDollyCart>().m_Position = 0;
        animNight.Play();
        lightning.transform.DORotate(new Vector3(-90, 30, 0), 2f);
        eveningIcon.transform.DOLocalMove(lastIconPos, 0.5f);
        nightIcon.transform.DOLocalMove(curIconPos, 0.5f);
        dayIcon.transform.DOLocalMove(nextIconPos, 0.5f);
        
        yield return new WaitForSeconds(2f);
        dayTimeText.text = "Ночь";
        nightUI.SetActive(true);
        for(int i = 0; i < 5; i++) bidButtons.SetActive(true);
        makeBidBut.SetActive(true);
        notMakeBidBut.SetActive(true);
        rollDis.SetActive(false);
        rerollDis.SetActive(false);
        camera.transform.localRotation = Quaternion.Euler(25, 90, 0);
    }
    
    public void GoToNight() => timeReader.GoNight();

    public void GoToDay()
    {
        timeReader.GoDay();
    }

    public void IncreaseBid(int num)
    {
        if (num == 0) bidSize = 0;
        if (cycle.GetComponent<PepperStorage>().PepperCount >= bidSize + num)
        {
            bidSize += num;
            bidSizeText.text = bidSize.ToString();
        }
    }
    public void ChooseBid(int type, string name)
    {
        if (cycle != null && cycle.transform.GetChild(0) != null)
        {
            cycle.transform.GetChild(0).GetComponent<NightOperator>().ChooseBid(type);
            bidDisText.text = name;
            isBid = true;
        }
    }
    public void MakeBid()
    {
        if (bidSize != 0 && isBid == true)
        {
            makeBidBut.SetActive(false);
            notMakeBidBut.SetActive(false);
            rollDis.SetActive(true);
            cycle.transform.GetChild(0).GetComponent<NightOperator>().Bid(bidSize);
            for (int i = 0; i < 5; i++) bidButtons.SetActive(false);
        }
    }
    public void NotMakeBid()
    {
        makeBidBut.SetActive(false);
        notMakeBidBut.SetActive(false);
        rollDis.SetActive(true);
        for(int i = 0; i < 5; i++) bidButtons.SetActive(false);
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

        /*baseWheelAnim.Play(baseWheelAnimClips[disasterNum].name);
        bossWheelAnim.Play(bossWheelAnimClips[disasterNum].name);*/
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
        isUsed = true;
        windowUI.SetActive(false);
        yield return new WaitForSeconds(4f);
        if (cycle.CycleData.cycleNum % 5 != 0)
        {
            _nightResults[disasterNum].SetActive(true);
            _nightResults[disasterNum].transform.GetChild(0).GetComponent<TMP_Text>().text = 
                cycle.transform.GetChild(0).GetComponent<NightOperator>().DiffHP.ToString();
            _nightResults[disasterNum].transform.GetChild(1).GetComponent<TMP_Text>().text = 
                            cycle.transform.GetChild(0).GetComponent<NightOperator>().DiffPeppers.ToString();
        }
        else
        {
            _nightResults[disasterNum + 5].SetActive(true);
            _nightResults[disasterNum + 5].transform.GetChild(0).GetComponent<TMP_Text>().text = 
                cycle.transform.GetChild(0).GetComponent<NightOperator>().DiffHP.ToString();
            _nightResults[disasterNum + 5].transform.GetChild(1).GetComponent<TMP_Text>().text = 
                cycle.transform.GetChild(0).GetComponent<NightOperator>().DiffPeppers.ToString();
        }
        isUsed = false;
    }
}