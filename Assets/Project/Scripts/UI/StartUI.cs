using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    void Start()
    {
        StartCoroutine(RotateWheel());
    }

    IEnumerator RotateWheel()
    {
        while (true)
        {
            wheel.transform.DORotateQuaternion(Quaternion.Euler(0f, 0, -1179f), 10f);
            yield return new WaitForSeconds(11f);
            wheel.transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 179f), 10f);
            yield return new WaitForSeconds(10f);
        }
    }
}
