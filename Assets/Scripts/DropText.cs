using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This class controls the behvavior of the DropText Object
public class DropText : MonoBehaviour
{
    private Text text;
    public float fadeOutTime = 1f;
    public float velocity = 0.1f;

    private Vector3 frameShift;

    public string Text
    {
        get
        {
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        frameShift = new Vector3(0, 1, 0) * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            transform.position += frameShift;
            yield return null;
        }
        Destroy(gameObject, fadeOutTime);
    }
}