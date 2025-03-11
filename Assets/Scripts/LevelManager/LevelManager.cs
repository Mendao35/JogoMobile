using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    //public GameObject level;
    public List<GameObject> levels;    
 
    /* SCRIPTABLE OBJECT QUE ESTA PASSADNO ESSAS VARIAVEIS
    [Header("Levels Peaces")]
    public List<LevelPeaceBase> levelPeacesList;
    public List<LevelPeaceBase> levelPeacesStartList;
    public List<LevelPeaceBase> levelPeacesEndtList;

    public int peacesNumber = 5;
    public int peacesNumberStart = 2;
    public int peacesNumberEnd = 1;*/

    public List<LevelPeaceBaseSetup> levelPeaceBaseSetipList;

    [SerializeField] private int _index;
    public float timeBetweenPeaces = .3f;
    private GameObject _currentLevel;

    private List<LevelPeaceBase> _spawnedPeacesList = new List<LevelPeaceBase>();
    private LevelPeaceBaseSetup _currSetup;

    [Header("Sacle")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPeaces = .1f;
    public float scaleFactor = 1.2f; //Tamanho maximo antes de voltar ao normal
   // public Ease ease = Ease.OutBack;


    private void Awake()
    {
        //SpawnNextLevel();
        CreatLevelPeaces();
    }

    #region Level Criar Normal
    private void SpawnNextLevel()
    {
        if(_currentLevel != null)//Conferir se ele ja existe na Cena
        {
            Destroy(_currentLevel);
            _index++;

            if(_index >= levels.Count)//Se chegar ou passar do ultimo item da lista zera
            {
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(levels[_index], container); //Instancia o gameobject dentro do transform
        _currentLevel.transform.localPosition = Vector3.zero; //Zera a posiçao do Prefab para nao dar erro
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }
    #endregion

    #region Peaces

    private void CreatLevelPeaces()
    {
        //_spawnedPeacesList = new List<LevelPeaceBase>(); //Cria uma nova lista para estar zerada
        CleanSpawnedPeaces();

        if (_currSetup != null)
        {
            _index++;
            if (_index >= levelPeaceBaseSetipList.Count)
            {
                ResetLevelIndex();
            }
        }

        _currSetup = levelPeaceBaseSetipList[_index];

        //Cria as Peças
        for (int i = 0; i < _currSetup.peacesNumberStart; i++)
        {
            CreatePeaces(_currSetup.levelPeacesStartList);            
        }
        for (int i = 0; i < _currSetup.peacesNumber; i++)
        {
            CreatePeaces(_currSetup.levelPeacesList);            
        }

        for (int i = 0; i < _currSetup.peacesNumberEnd; i++)
        {
            CreatePeaces(_currSetup.levelPeacesEndtList);            
        }

        //ColorManager.Instance.ChangeColorByType(_currSetup.artType);

        StartCoroutine(ScalePeacesByTime());
    }

    IEnumerator ScalePeacesByTime()
    {
        foreach(var p in _spawnedPeacesList) //Percorre todos os itens da lista
        {
            p.transform.localScale = Vector3.one; //Bota as escalas dos itens pra 1
        }

        yield return null;

        for(int i = 0; i < _spawnedPeacesList.Count; i++)
        {
            StartCoroutine(AnimateScale(_spawnedPeacesList[i].transform, scaleDuration, scaleFactor));
            yield return new WaitForSeconds(scaleTimeBetweenPeaces);
            //_spawnedPeacesList[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            //yield return new WaitForSeconds(timeBetweenPeaces);
        }
    }

    IEnumerator AnimateScale(Transform obj, float duration, float maxScale)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = Vector3.one;
        Vector3 peakScale = Vector3.one * maxScale;

        // Fase de crescimento
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            obj.localScale = Vector3.Lerp(initialScale, peakScale, (elapsedTime / (duration / 2)));
            yield return null;
        }

        elapsedTime = 0f;

        // Fase de retorno ao tamanho normal
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            obj.localScale = Vector3.Lerp(peakScale, initialScale, (elapsedTime / (duration / 2)));
            yield return null;
        }

        obj.localScale = initialScale; // Garante o tamanho final correto
    }

    private void CreatePeaces(List<LevelPeaceBase> list)
    {
        var peace = list[(Random.Range(0, list.Count))];//Randomizendo dentro dos itens da lista
        var spawnedPeace = Instantiate(peace, container);

        if(_spawnedPeacesList.Count > 0) //Quer dizer que nao é a primeira peça
        {
            var lastPeace = _spawnedPeacesList[_spawnedPeacesList.Count - 1];//Pega a ultima peça da lista

            spawnedPeace.transform.position = lastPeace.endPeace.position;//Pega a posiçao da peça que acaou de spawnar e passa a posiçao
            
        }
        else
        {
            spawnedPeace.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spawnedPeace.GetComponentsInChildren<ArtPeace>())//Para cada peça que achar dentro do spawnedPeace fa;a isso
        {
            p.ChangePeace(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }

        _spawnedPeacesList.Add(spawnedPeace);//Adiciona na Lista a Peça criada
    }


    private void CleanSpawnedPeaces()
    {
        for(int i = _spawnedPeacesList.Count - 1; i >= 0 ; i--) //For de traz pra frente
        {
            Destroy(_spawnedPeacesList[i].gameObject);
        }

        _spawnedPeacesList.Clear();
    }

    /*IEnumerator CreateLevelPeacesCoroutine()
    {
        
    }*/


    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))//So para testar
        {
            CreatLevelPeaces();
        }
    }
}
