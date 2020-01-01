using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameSession : MonoBehaviour
{
    // param 
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 5;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    // State variable
    [SerializeField] int currentScore = 0;


    // Awake is excuted before start method, at the very bigenning
    private void Awake()
    {
        // we use Awake to use the singleton pattern approache, it's an approache to keep one gameobject throught the 
        // differents scene, in this case we need GameSession to stay from one scene to another to keep the same score of the game 
        // through the different scenes

        int gameStatusCount = FindObjectsOfType<GameSession>().Length;// note that we use objects with 's', to find all the objects

        if (gameStatusCount > 1)
        {

            gameObject.SetActive(false);// this line is needed to prevent bugs coz detroy() method is excuted last so we need to deactivate
            Destroy(gameObject); // so if there is more than one GameSession , we destroy the newer one, coz the awake for the old one 
                                 // would be long time excutioned 

        }
        else
        {
            DontDestroyOnLoad(gameObject); // this line 
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = currentScore.ToString();// we are assigning the value of currentScore variable to the serialized field variable
                                                 // called scoreText, which is attached to the textmesh pro game object that display the 
                                                 // score in the game, this is how you control the text content in a game 
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed; // this to control the speed of the game, so easy 
    }

    public void AddScoreWithBlock()
    {
        currentScore = currentScore + pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();

    }
    public void ResetGame()
    {
        Destroy(gameObject);

    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
