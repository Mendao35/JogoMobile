using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public new ParticleSystem particleSystem;
    public float timeToHide = 3f;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;


    private void Awake()
    {
        //particleSystem = GetComponent<ParticleSystem>();
        /*if(particleSystem != null)
        {
            particleSystem.transform.SetParent(null);
        }*/
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        if(graphicItem != null)
        {
            graphicItem.SetActive(false);
        }
        Invoke(nameof(HideObject), timeToHide);
        OnCollect();

    }
        

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if(particleSystem != null)
        {
            particleSystem.Play();
        }

        if(audioSource != null)
        {
            audioSource.Play();
        }

    }

}
