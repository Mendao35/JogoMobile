using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))] //Significa que precisa ter um meshrenderer para o script funcionar
public class ColorChange : MonoBehaviour
{
   
    private float _duration = .2f;
    public MeshRenderer meshRenderer;
    public Color startColor = Color.white;

    private Color _corectColor;
    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        _corectColor = meshRenderer.materials[0].GetColor("_Color");
        lerpColor();
    }

    private void lerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(_corectColor, _duration).SetDelay(.2f);
    }
}
