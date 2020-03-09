using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : Singleton<StartGameManager>
{
    /****************************************************/
    /***************** Basic Properties *****************/
    /****************************************************/
    [SerializeField]
    private GameObject MainMenu = null;

    [SerializeField]
    private GameObject StoryMenu = null;

    [SerializeField]
    private GameObject ControlsMenu = null;

    [SerializeField]
    private GameObject Instructions = null;

    // Start is called before the first frame update
    void Start()
    {
        StoryMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        Instructions.SetActive(false);
        MainMenu.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Switch to the Main scene
    public void BeginGame()
    {
        SceneManager.LoadScene("Main");
    }

    // Open the Story menu
    public void GoToStory()
    {
        MainMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        Instructions.SetActive(false);
        StoryMenu.SetActive(true);
    }

    // Open the Controls menu
    public void GoToControls()
    {
        MainMenu.SetActive(false);
        StoryMenu.SetActive(false);
        Instructions.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    // Open the Instructions
    public void GoToInstructions()
    {
        MainMenu.SetActive(false);
        StoryMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        Instructions.SetActive(true);        
    }
}
