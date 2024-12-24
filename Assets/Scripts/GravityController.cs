using UnityEngine;

public class GravityController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // Physics.gravity = Vector3.left;
        //change gravity base on arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Physics.gravity = Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Physics.gravity = Vector3.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Physics.gravity = Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Physics.gravity = Vector3.right;
        }
    }
}
