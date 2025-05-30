using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelterView : MonoBehaviour
{
    Shelter _shelter;
    [SerializeField] private TMP_Text textHP;
    [SerializeField] private Slider hp_slider;
    
    public void GetShelter(Shelter shelter) => _shelter = shelter;
    private void Update()
    {
        if (_shelter != null)
        {
            hp_slider.value = Mathf.Round(_shelter.HP / (float)(_shelter.MaxHP * 1.00) * 100) / 100;       
            textHP.text = _shelter.HP.ToString() + " / " + _shelter.MaxHP.ToString();
            if(Input.GetKeyDown(KeyCode.Q)) Debug.Log(Mathf.Round(_shelter.HP / (float)(_shelter.MaxHP * 1.00) * 100) / 100);
        }
    }
}
