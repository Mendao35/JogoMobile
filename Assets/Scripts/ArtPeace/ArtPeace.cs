using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtPeace : MonoBehaviour
{
    public GameObject currentArt;
    
   public void ChangePeace(GameObject peace)
    {
        if(currentArt != null)
        {
            Destroy(currentArt);
        }

        currentArt = Instantiate(peace, transform);
        //currentArt.transform.localPosition = Vector3.zero;

    }
}
