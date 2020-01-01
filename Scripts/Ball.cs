/*using System;
using System.Collections;
using System.Collections.Generic;*/

using UnityEngine;
// note that we are only using UnityEnigne here, 2el library yalli menkoun 3m nesta5dema betkoun mdaweye, baynama l mesh 3m nesta5dema 
// betkoun metfiye 



public class Ball : MonoBehaviour
{

    // configuration parameters

    [SerializeField] Paddle paddle1; // we are serializing a variable of type paddle ( the paddle game object )
                                     // in the inspector we can assign the paddle to it
    [SerializeField] float xPush;// this control the force of the ball at start in x direction
    [SerializeField] float yPush;// this control the force of the ball at start in y direction
    [SerializeField] AudioClip[] ballSounds;// this create an array of type AudioClip, we'll assign audio to it 
    [SerializeField] float randomFactorX;// this variable created to make the ball go randomly on x axis when it stuck
    [SerializeField] float randomFactorY;// this variable created to make the ball go randomly on y axis when it stuck
    [SerializeField] float shurikenRotation = 8;
    // State 

    Vector2 paddleToBallVector; // variable of type vector2 we use it as a distance vector in x and y 
    bool hasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position; // this variable of type vector2 hold the distance 
                                                                              // between the paddle and the ball at the beginning


    }

    void Update()
    {

        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();

        }

        this.transform.Rotate(0, 0, shurikenRotation);



    }


    private void LaunchOnMouseClick()
    {

        if (Input.GetMouseButtonDown(0))
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }

    }


    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);// this variable of type vector hold 
                                                                                                    // the x and y of the paddle in the game

        transform.position = paddlePos + paddleToBallVector; // this line make the ball position = the paddle position + distance between 
                                                             // the 2 at the start , so that the ball will always follow the paddle

    }


    private void OnCollisionEnter2D(Collision2D collision) // we are creating a build in method that'll be called when any collider 
                                                           // collide or touch the collider of this game object (Ball in this case)
                                                           // the collision parameter of type collision2d return the object that get in touch 
                                                           // with this game object (Ball), it helps a lot to know what to do next
    {

        Vector2 velocityTweakX = new Vector2(Random.Range(0f, randomFactorX), 0f);
        Vector2 velocityTweakY = new Vector2(0, Random.Range(0f, randomFactorY));

        if (hasStarted == true)
        {

            // AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)]; // this line to play random clip at colliding
            // GetComponent<AudioSource>().PlayOneShot(clip);


            if (collision.gameObject.name == "Paddle")
            {
                GetComponent<AudioSource>().PlayOneShot(ballSounds[1]);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(ballSounds[0]);
            }

            BallDontStuckXY(velocityTweakX, velocityTweakY);

        }
          
    }


    private void BallDontStuckXY(Vector2 velocityTweakX, Vector2 velocityTweakY)
    {
        // ball must only change direction when it stuck , not randomly with each hit 
        // for now the ball change direction with each hit randomly but slightly if the parameters were small
        GetComponent<Rigidbody2D>().velocity += velocityTweakX;
        GetComponent<Rigidbody2D>().velocity += velocityTweakY;
    }

}
