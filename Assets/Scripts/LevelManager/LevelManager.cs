using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    //public GameObject level;
    public List<GameObject> levels;

    
 
    /* SCRIPTABLE OBJECT
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

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);
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
