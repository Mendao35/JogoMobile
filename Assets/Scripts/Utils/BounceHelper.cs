using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceHelper : MonoBehaviour
{
    [Header("Sacle")]
    public float scaleDuration = .2f;
    public float scaleBouce = 1.2f;
    public Ease ease = Ease.OutBack;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        transform.DOScale(scaleBouce, scaleDuration).SetEase(ease).SetLoops(3, LoopType.Yoyo);
    }
}
