using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03
    }

    public List<ArtSetup> artSetup;

    public ArtSetup GetSetupByType(ArtType artType)
    {
       return artSetup.Find(i => i.artType == artType);
    }

}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
