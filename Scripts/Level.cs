using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // parameters 
    [SerializeField] int breakableBlocks; // serialized for debugging purposes


    // cached reference
     


    private void Start()
    {
        
    }

    public void CountBlocks ()
    {
        breakableBlocks++;
         

    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        
        if (breakableBlocks<=0)
        {
            FindObjectOfType<SceneLoader>().LoadNextScene();

        }




    }

}
