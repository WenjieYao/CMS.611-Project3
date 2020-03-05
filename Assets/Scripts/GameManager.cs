using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/****************************************************/
// The Game Manager script is used for all game 
// control functionalities and record current
// game status
/****************************************************/
public class GameManager : Singleton<GameManager>
{
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Set the fired projectiles under a parent
    [SerializeField]
    private Transform projectileParent = null;
    // Set the spawned monsters under a parent
    [SerializeField]
    private Transform monsterParent = null;

    /****************************************************/
    // Public properties that corresponds to the private
    // properties above
    /****************************************************/
    public Transform ProjectileParent
    {
        get
        {
            return projectileParent;
        }
        set
        {
            this.projectileParent = value;
        }
    }

    public Transform MonsterParent
    {
        get
        {
            return monsterParent;
        }
        set
        {
            this.monsterParent = value;
        }
    }

    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
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
