using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // parm config 
    [SerializeField] AudioClip breakSound;// var of type AudioClip to hook the clip in the assets
    [SerializeField] GameObject blockSparklesVFX;// var of type GameObject (all the game objects in unity ) to hook the particules file 
    [SerializeField] int timesHit = 0;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] float speedDown = 0.2f;
    float newYPos = 0;

    // state 


    // cached reference 




    private void Start()
    {
        CountBreakableBlocks();
         
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            FindObjectOfType<Level>().CountBlocks();// at the start of the scene, this line will be called for each block, 
                                                    //coz this script is attached
                                                    // to the block game object ( to each block ), and with each call it will increment and give 
                                                    // the number of blocks in total. 

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // we are creating a build in method that'll be called when any collider 
                                                           // collide or touch the collider of this game object (Block in this case)
                                                           // the collision parameter of type collision2d return the object that get in touch 
                                                           // with this game object (Block), it helps a lot to know what to do next
    {

        if (tag == "Breakable")
        {
            HandleHit();

        }

    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length+1; // max hits a block can take must be limited by the nbr of the sprites the block has
                                           // we can always duplicate and add the nbr of sprites of block for a higher number of max hits 
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();

        }

    }

    private void ShowNextHitSprite()
    {

        int spriteIndex = timesHit - 1;// sprite index is the index we will use in the sprite array (hitSprite) to decide what sprite 
                                       // we want to display when the block takes a hit 
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("the error is on " + gameObject.name);
        }

    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);// This function create an audio source but 
                                                                                // automatically dispose of it once the clip finish playing
                                                                                // if we use the normal sound function when the cube destroyed
                                                                                // the sound will stop , so this is better
                                                                                // and we must put it before the game object is destroyed
        FindObjectOfType<Level>().BlockDestroyed();

        FindObjectOfType<GameSession>().AddScoreWithBlock();

        Destroy(gameObject);// this line will destroy the gameObject that this script is attached to, in this case the Block

        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()// a methode to instantiate the particles effects 
    {

        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        // we instantiate the serialized fiel GameObject , in the position and same rotation of the block
        // and we assign this instantiation to a variable

        Destroy(sparkles, 2); // this destroy the instantiated GameObject after 2 seconds 
 
    }


    private void Update()
    {
        newYPos = this.transform.position.y- speedDown*Time.deltaTime;
        this.transform.position = new Vector3 (this.transform.position.x,newYPos,0);
        

    }

}
