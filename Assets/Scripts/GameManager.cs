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
    private float NightTime = 10;
    // Pause flag
    private bool pause = false;
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

    // Player fire rate and display
    private float playerFireRate = 0;
    [SerializeField]
    private TMP_Text playerFRTxt = null; 

    // Tech cash and the display
    private int playerMaxHealth = 0;
    [SerializeField]
    private TMP_Text playerMHTxt = null; 

    // Day/Night cycle flag
    private bool isNight = true;
    
    // Filter used for night effect
    [SerializeField]
    private GameObject nightFilter = null; 

    // Shop object
    [SerializeField]
    private GameObject shop = null; 
    // Shop menu
    [SerializeField]
    private GameObject shopMenu = null; 
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

    public float PlayerFireRate
    {
        get
        {
            return playerFireRate;
        }
        set
        {
            this.playerFireRate = value;
            this.playerFRTxt.text = "Fire Rate: " + value.ToString();
        }
    }

    public int PlayerMaxHealth
    {
        get
        {
            return playerMaxHealth;
        }
        set
        {
            this.playerMaxHealth = value;
            this.playerMHTxt.text = "Max HP:    " + value.ToString();
        }
    }

    public GameObject ShopMenu
    {
        get
        {
            return shopMenu;
        }
        set
        {
            this.shopMenu = value;
        }
    }
    /****************************************************/

    /****************************************************/
    /***************** Basic Functions ******************/
    /****************************************************/
    // Start is called before the first frame update
    void Start()
    {
        // Initialze display
        if (isNight)
        {
            TimeLeft = NightTime;
            nightFilter.SetActive(true);
            shop.SetActive(false);
        }
        else
        {
            TimeLeft = DayTime;
            nightFilter.SetActive(false);
            shop.SetActive(true);
        }
        PlayerFireRate = Player.Instance.FireRate;
        PlayerMaxHealth = Player.Instance.MaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if pause
         if (pause == false)
         {
             Time.timeScale = 1;
         }
         
         else 
         {
             Time.timeScale = 0;
         }
        // Change Day/Night when time runs out
        if (TimeLeft<=0)
        {
            isNight = !isNight;
            // Reset time left
            if (isNight)
            {
                TimeLeft = NightTime;
                nightFilter.SetActive(true);
                shop.SetActive(false);
                shopMenu.SetActive(false);
            }
            else
            {
                TimeLeft = DayTime;
                nightFilter.SetActive(false);
                shop.SetActive(true);
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

    // Upgrade player maximum health
    public void UpMaxHP()
    {
        if (TechCash>=2)
        {
            Player.Instance.MaxHealth += 1;
            Player.Instance.Health = Player.Instance.MaxHealth;
            // Update health bar display
            Player.Instance.PlayerHealthBar.SetMaxHealth(Player.Instance.MaxHealth);
            // Update max health display
            PlayerMaxHealth = Player.Instance.MaxHealth;
            // Update cash and display
            TechCash -= 2;
        }
    }

    // Upgrade player fire rate
    public void UpMaxFR()
    {
        if (TechCash>=4)
        {
            Player.Instance.FireRate += 1;
            // Update fire rate display
            PlayerFireRate = Player.Instance.FireRate;
            // Update cash and display
            TechCash -= 4;
        }
    }

    // Pause function and unpause
    public void PauseFun()
    {
        if(pause)
        {
            pause = false;
            Time.timeScale = 1;
        }
        else
            pause = true;
    }

    // Function to end game
    public void ChangeScene()
    {
        Time.timeScale = 1;
        //Debug.Log("LoadScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }

    // Function to restart a new game
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
