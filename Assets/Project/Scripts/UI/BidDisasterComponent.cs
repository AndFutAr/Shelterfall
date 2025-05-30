using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidDisasterComponent : MonoBehaviour
{
    public UI_Controller UIController;
    public  Camera camera;

    [SerializeField] private GameObject DisasterText;
    [SerializeField] private int number;
    [SerializeField] private string name;

    private void Update()
    {
        RaycastHit HallHit;
        Ray HallRay = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(HallRay, out HallHit))
        {
            if (HallHit.transform == this.transform)
            {
                DisasterText.SetActive(true);
                if(Input.GetMouseButtonDown(0))
                    UIController.ChooseBid(number, name);
            }
            else DisasterText.SetActive(false);
        }
        else DisasterText.SetActive(false);
    }
}
