using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Function to end game
    public void ChangeScene()
    {
        //Debug.Log("LoadScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }

    // Function to restart a new game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
