using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // this function means : when something pass through the LoseCollider game object which
        // this script is attached to it , what we wants to happened 

        SceneManager.LoadScene("Game Over");

    }


}
