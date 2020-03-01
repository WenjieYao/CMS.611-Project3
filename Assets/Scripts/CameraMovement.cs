using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************/
/**Camera Control Scprit, not used in current Project**/
/******************************************************/

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 5;

    private float xMax=100;
    private float yMin=-100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }
    private void GetInput()
	{
        if (Input.GetKey(KeyCode.UpArrow))
		{
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
		}
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10, 10), Mathf.Clamp(transform.position.y, -10, 10),-10);
    }
}
