using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // cofiguration parameters
    [SerializeField] int screenWidthInUnits;
    [SerializeField] float minX;
    [SerializeField] GameObject shirukenSparklesVFX;// var of type GameObject (all the game objects in unity ) to hook the particules file 
    [SerializeField] float maxX;
    float mousePosInUnits;

    // cached refrence 

    GameSession gameSession;// FindObject method is heavy and better not be used in the update method that's why we cached it in a var
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();// FindObject method is heavy and better not be used in the update method
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
      void Update()
    {
       

        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);// vector2 is a type of variable that specify the x and y 
                                                                                    // we need to create a variable and assign it to a new 
                                                                                    // vector2 , transform.position.x bet5alli l x metl ma hiye

        paddlePos.x = Mathf.Clamp(GetXPose(), minX, maxX);// mathf.clamp limit the value of a variable


        transform.position = paddlePos; // this line change the position of the paddle game object in unity to the value of the variable
                                        // paddlePos , this is coz all this script is assigne and attached to the paddle game object
                                        // so the transform.position change the position value in the inspector of this game object


    }


    private float GetXPose()
    {
        if (gameSession.IsAutoPlayEnabled() == true)
        {
            return (ball.transform.position.x);
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;// mousePosition.x return x of the mouse on screen 
                                                                             // screen.width is the width of the game screen only
                                                                             // screenWidthInUnits is a serializedField that we enter in unity
                                                                             // and in our case = 16 ( camera size = 6 => height = 12 and 
                                                                             // and width = 12*4/3 = 16 ) 
                                                                             // so we got the position of the mouse in the game screen in units
                                                                             // between 0 and 16
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {


        GameObject sparkles = Instantiate(shirukenSparklesVFX, collision.transform.position, collision.transform.rotation);
        // we instantiate the serialized fiel GameObject , in the position and same rotation of the block
        // and we assign this instantiation to a variable

        Destroy(sparkles, 0.2f); // this destroy the instantiated GameObject after 2 seconds 
    }

    private void TriggerSparklesVFX()// a methode to instantiate the particles effects 
    {

        

    }
}
