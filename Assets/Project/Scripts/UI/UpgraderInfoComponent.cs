using UnityEngine;
using TMPro;

public class UpgraderInfoComponent : MonoBehaviour
{
        [SerializeField] private RaceController controller; 
        [SerializeField] private CycleComponent cycle;
        [SerializeField] private int number;
        [SerializeField] private bool isElement;
        [SerializeField] private TMP_Text countText, bonusText;

        private void Update()
        {
                if (controller.RaceData.IsRace == 1) cycle = controller.Cycle;
                if (cycle != null)
                {
                        if (!isElement)
                        {
                                countText.text = cycle.CycleData._BaseDictionaries[number].Count.ToString();
                                switch (number)
                                {
                                        case 0: bonusText.text = "+" + (cycle.CycleData.maxHP - (int)cycle.RaceData.MaxHp); break;
                                        case 1: bonusText.text = "+" + (int)(100 * (cycle.CycleData.heal - 0)) + "%"; break;
                                        case 2: bonusText.text = "+" + (cycle.CycleData.dayBase - 1); break;
                                        case 3: bonusText.text = "+" + (cycle.CycleData.cycleTime - 10); break;
                                        case 4: bonusText.text = "-" + (int)(100 * (1 - cycle.CycleData.costFactor)) + "%"; break;
                                }
                        }
                        else
                        {
                                countText.text = cycle.CycleData._ElDictionaries[number].Count.ToString();
                                bonusText.text =  "-" + 100 *(1 - cycle.CycleData._ElDictionaries[number].ElementFactor) + "%";
                        }
                }
        }
}