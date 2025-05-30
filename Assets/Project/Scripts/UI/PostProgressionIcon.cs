using System;
using TMPro;
using UnityEngine;

public class PostProgressionIcon : MonoBehaviour
{
    public Camera camera;
    [SerializeField] private RaceController controller;
    [SerializeField] private UI_Controller uiController;
    [SerializeField] private GameObject iconName;
    [SerializeField] private TMP_Text progressName, progressLevel, progressPrice, progressDescription;
    
    [SerializeField] private string nameProgress, descriptionProgress;
    [SerializeField] private int type;
    private Progress thisProgress;

    private void Start()
    {
        thisProgress = controller.RaceData._progressList[type];
    }

    private void Update()
    {
        RaycastHit HallHit;
        Ray HallRay = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(HallRay, out HallHit))
        {
            if (HallHit.transform == this.transform)
            {
                iconName.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(controller.RaceData._progressList[type]);
                    uiController.ChooseUpgrade(type);
                    progressName.text = nameProgress;
                    progressLevel.text = thisProgress.Count.ToString();
                    progressPrice.text = thisProgress.Price.ToString();
                    progressDescription.text = descriptionProgress;
                }
            }
            else iconName.SetActive(false);
            if (HallHit.transform.gameObject.layer == LayerMask.NameToLayer("PutCard"))
            {
                if(Input.GetMouseButtonDown(0)) uiController.ChooseUpgrade(-1);
            }
        }
        else
        {
            iconName.SetActive(false);
            if(Input.GetMouseButtonDown(0)) uiController.ChooseUpgrade(-1);
        }
    }
}