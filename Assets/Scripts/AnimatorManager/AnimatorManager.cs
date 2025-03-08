using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    //Script Sera adicionado dentro do player

    public Animator animator;
    public List<AnimatorSetup> animatorSetupList;

    public enum AnimationType //Enumerator com os tipos de animaçoes
    {
        IDLE,
        RUN,
        DEATH

    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f)
    {
        foreach(var animationSearch in animatorSetupList) //Procurar por todos os setups de Animaçao qual é igual ao AnimationType
        {
            if(animationSearch.type == type)
            {
                animator.SetTrigger(animationSearch.triggerString);
                animator.speed = animationSearch.speed * currentSpeedFactor;
                break; //Quando achar para
            }
        }
    }

    private void Update()
    {
      
    }

}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string triggerString;
    public float speed = 1f;

}
