using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    
    public float speed = 1f;

    public string tagToCompareEnemy = "Enemy";
    public string tagToCompareEnd = "EndLine";

    public GameObject endScreen;

    public TextMeshPro uiTextPowerUp;

    public bool invencible = false;

    private bool _canRun;
    private Vector3 _pos;

    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 5;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }



    // Update is called once per frame
    void Update()
    {
        if (!_canRun) return; // Se _canRun for false nao faz mas nada que esta abaixo do codigo de Update

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCompareEnemy)
        {
            if(!invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEATH);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCompareEnd)
        {
            if (!invencible)
            {
                EndGame();
                //SetInvencible(false);
            }            
        }
    }

    private void MoveBack()
    {
        this.transform.position -= transform.forward * 0.5f;
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)//Se eu nao passar nada sera Idle
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN,_currentSpeed / _baseSpeedToAnimation);
    }

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float animationDuration)
    {
        var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;
    }

    public void ResetHeight()
    {
        var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
}
