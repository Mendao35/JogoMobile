using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPeaceBaseSetup : ScriptableObject
{
    public ArtManager.ArtType artType;

    [Header("Levels Peaces")]
    public List<LevelPeaceBase> levelPeacesList;
    public List<LevelPeaceBase> levelPeacesStartList;
    public List<LevelPeaceBase> levelPeacesEndtList;

    public int peacesNumber = 5;
    public int peacesNumberStart = 2;
    public int peacesNumberEnd = 1;
}

