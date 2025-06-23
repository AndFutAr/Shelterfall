using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PeCoinCycle : MonoBehaviour
{
    private float lifeTime = 0.4f;
    private void Start() => StartCoroutine(Cycle());
    IEnumerator Cycle()
    {
        while (true)
        {
            transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + 200, transform.localPosition.z), lifeTime);
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}
