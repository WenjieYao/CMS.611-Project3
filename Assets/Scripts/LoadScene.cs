using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image black;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Scenes/Starting", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        // Fading effect
        StartCoroutine(Fading());

    }

    // End game function
    public void EndGame()
    {
        Application.Quit();
    }

    // Fading function activated when change between scence
    IEnumerator Fading()
    {
        anim.SetBool("Fade",true);
        yield return new WaitUntil(()=>black.color.a==1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
