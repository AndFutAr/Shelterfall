using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelterView : MonoBehaviour
{
    Shelter _shelter;
    [SerializeField] private TMP_Text textHP; 

    
    public void GetShelter(Shelter shelter) => _shelter = shelter;
    private void Update()
    {
        if (_shelter != null)
            { textHP.text = _shelter.HP.ToString(); }
    }
}
