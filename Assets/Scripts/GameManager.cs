using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/****************************************************/
// The Game Manager script is used for all game 
// control functionalities and record current
// game status
/****************************************************/
public class GameManager : Singleton<GameManager>
{
    /****************************************************/
    /************* Game Control Parameters **************/
    /****************************************************/
    // Day/Night time
    private float DayTime = 10;
    private float NightTime = 30;
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Set the fired projectiles under a parent
    [SerializeField]
    private Transform projectileParent = null;
    // Set the spawned monsters under a parent
    [SerializeField]
    private Transform monsterParent = null;
    // Game over pop-up
    [SerializeField]
    private GameObject gameOver = null;

    // Tech cash and the display
    private int techCash = 0;
    [SerializeField]
    private TMP_Text techCashTxt = null; 

    // Day/Night time left and display
    private float timeLeft = 0;
    [SerializeField]
    private TMP_Text timeLeftTxt = null; 

    // Day/Night cycle flag
    private bool isNight = true;
    
    // Filter used for night effect
    [SerializeField]
    private GameObject nightFilter = null; 
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

    public int TechCash
    {
        get
        {
            return techCash;
        }
        set
        {
            this.techCash = value;
            this.techCashTxt.text = "TechCash: " + value.ToString();
        }
    }

    public float TimeLeft
    {
        get
        {
            return timeLeft;
        }
        set
        {
            this.timeLeft = value;
            this.timeLeftTxt.text = "Time Left: " + value.ToString("F2");
        }
    }

    public bool IsNight
    {
        get
        {
            return isNight;
        }
        private set
        {
            this.isNight = value;
        }
    }
    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
    // Start is called before the first frame update
    void Start()
    {
        if (isNight)
        {
            TimeLeft = NightTime;
            nightFilter.SetActive(true);
        }
        else
        {
            TimeLeft = DayTime;
            nightFilter.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Change Day/Night when time runs out
        if (TimeLeft<=0)
        {
            isNight = !isNight;
            // Reset time left
            if (isNight)
            {
                TimeLeft = NightTime;
                nightFilter.SetActive(true);
            }
            else
            {
                TimeLeft = DayTime;
                nightFilter.SetActive(false);
                Player.Instance.Health = Player.Instance.MaxHealth;
                Player.Instance.PlayerHealthBar.SetMaxHealth(Player.Instance.MaxHealth);
            }
        }
        TimeLeft -= Time.fixedDeltaTime;
        // Game over if player health goes to 0
        if (Player.Instance.Health <=0)
            gameOver.SetActive(true);
        // Clear collectibles at day
        if (!isNight)
        {
            Destroy(GameObject.FindGameObjectWithTag("FreeFood"));
        }
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
