using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{

    public float playerSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey("w") || Input.GetKey("up"))
        {
            pos.y += playerSpeed;
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            pos.x -= playerSpeed;
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            
            pos.y -= playerSpeed;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            pos.x += playerSpeed;
        }

        transform.position = pos;

    }
}
