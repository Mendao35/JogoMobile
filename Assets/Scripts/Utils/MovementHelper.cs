using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    public List<Transform> positionsList;

    public float duration = 1f;

    private int _index = 0;

    private void Start()
    {
        transform.position =  positionsList[0].transform.position; //a Posiçao inicio do gameobject é a primeira posiçao da lista
        NextIndex();
        StartCoroutine(StartMoviment());
    }

    private void NextIndex()
    {
        _index++;

        if (_index >= positionsList.Count) //Quando chega no ultimo item da lista zera e volta pro incio
        {
            _index = 0;            
        }
    }

    IEnumerator StartMoviment()
    {
        float time = 0;

        while (true)
        {
            var currentPosition = transform.position;

            while( time < duration) //Quando bater no tempo do duration para de executar ou vai executar outra coisa
            {
                transform.position = Vector3.Lerp(currentPosition, positionsList[_index].transform.position,(time/duration));
                
                time += Time.deltaTime; //Faz a contagem de tempo
                yield return null;
            }

            NextIndex();

            time = 0;

            yield return null; 
        }
    } 
}
