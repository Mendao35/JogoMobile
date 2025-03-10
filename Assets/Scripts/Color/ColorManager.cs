using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materialList;
    public List<ColorSetup> colorSetupList;

    public void ChangeColorByType(ArtManager.ArtType artType)
    {
        var setup =  colorSetupList.Find(i => i.artType == artType);

        for(int i = 0; i < materialList.Count; i++)//Percorre todos os dados da Lista
        {
            materialList[i].SetColor("_Color", setup.colorList[i]);
        }
    }
}

[System.Serializable]
public class ColorSetup
{
    public ArtManager.ArtType artType;
    public List<Color> colorList;
}
