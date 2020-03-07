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
    private float NightTime = 20;
    // Pause flag
    private bool pause = false;
    // Day/Night cycle flag
    private bool isNight = false;
    
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    // Set the fired projectiles under a parent
    [SerializeField]
    private Transform projectileParent = null;
    // Set the spawned monsters under a parent
    [SerializeField]
    private Transform monsterParent = null;
    // Game win pop-up
    [SerializeField]
    private GameObject gameWin = null;
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
    
    // Filter used for night effect
    [SerializeField]
    private GameObject nightFilter = null; 

    // Shop object
    [SerializeField]
    private GameObject shop = null; 
    // Shop menu
    [SerializeField]
    private GameObject shopMenu = null;

    // Fire rate upgrade cost and display
    private float upFRCost;
    [SerializeField]
    private TMP_Text FRCostTxt = null;

    // Max health upgrade cost and display
    private float upHPCost;
    [SerializeField]
    private TMP_Text HPCostTxt = null;

    // Attack power upgrade cost and display
    private float upAttackCost;
    [SerializeField]
    private TMP_Text AttackCostTxt = null;

    // Price increase rate
    [SerializeField]
    private float priceIncRate = 1.5f;

    // Current round (year) and display
    private int round = 1;
    [SerializeField]
    private TMP_Text roundTxt = null; 
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


    public float UpFRCost
    {
        get
        {
            return upFRCost;
        }
        set
        {
            this.upFRCost = value;
            this.FRCostTxt.text = "$" + value.ToString();
        }
    }

    public float UpHPCost
    {
        get
        {
            return upHPCost;
        }
        set
        {
            this.upHPCost = value;
            this.HPCostTxt.text = "$" + value.ToString();
        }
    }

    public float UpAttackCost
    {
        get
        {
            return upAttackCost;
        }
        set
        {
            this.upAttackCost = value;
            this.AttackCostTxt.text = "$" + value.ToString();
        }
    }

    public int Round
    {
        get
        {
            return round;
        }
        set
        {
            this.round = value;
            this.roundTxt.text = "Year: " + value.ToString();
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
        Round = 1;
        TechCash = 100;
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
        UpHPCost = shopMenu.GetComponent<Shop>().UpMaxHPCost;
        UpFRCost = shopMenu.GetComponent<Shop>().UpMaxFRCost;
        UpAttackCost = shopMenu.GetComponent<Shop>().UpAttackCost;
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
                Round +=1;
                TimeLeft = DayTime;
                nightFilter.SetActive(false);
                shop.SetActive(true);
                Player.Instance.Health = Player.Instance.MaxHealth;
                Player.Instance.PlayerHealthBar.SetMaxHealth(Player.Instance.MaxHealth);
            }
        }
        TimeLeft -= Time.fixedDeltaTime;
        // Win (Graduate from MIT) if round>4
        if (round > 4)
        {
            Time.timeScale = 0;
            gameWin.SetActive(true);
        }
        // Game over if player health goes to 0
        if (Player.Instance.Health <=0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
        // Clear collectibles at day
        if (!isNight)
        {
            Destroy(GameObject.FindGameObjectWithTag("FreeFood"));
        }
    }

    // Upgrade player maximum health
    public void UpMaxHP()
    {
        int cost = shopMenu.GetComponent<Shop>().UpMaxHPCost;
        if (TechCash >= cost)
        {
            Player.Instance.MaxHealth += 1;
            Player.Instance.Health = Player.Instance.MaxHealth;
            // Update health bar display
            Player.Instance.PlayerHealthBar.SetMaxHealth(Player.Instance.MaxHealth);
            // Update max health display
            PlayerMaxHealth = Player.Instance.MaxHealth;
            // Update cash and display
            TechCash -= cost;
            // Increase the cost of the powerup and update display
            shopMenu.GetComponent<Shop>().UpMaxHPCost = Mathf.RoundToInt((float)(cost * priceIncRate));
            UpHPCost = shopMenu.GetComponent<Shop>().UpMaxHPCost;
        }
    }

    // Upgrade player fire rate
    public void UpMaxFR()
    {
        int cost = shopMenu.GetComponent<Shop>().UpMaxFRCost;
        if (TechCash >= cost)
        {
            Player.Instance.FireRate += 1;
            // Update fire rate display
            PlayerFireRate = Player.Instance.FireRate;
            // Update cash and display
            TechCash -= cost;
            // Increase the cost of the powerup and update display
            shopMenu.GetComponent<Shop>().UpMaxFRCost = Mathf.RoundToInt((float)(cost * priceIncRate));
            UpFRCost = shopMenu.GetComponent<Shop>().UpMaxFRCost;
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
    public void QuitGame()
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
