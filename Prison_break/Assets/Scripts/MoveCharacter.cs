using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{


    public float playerSpeed = 0.1f;
    public Vector3 myPos;
    //public Transform myPlay;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        //pos.z = 0;
        //pos.rotation
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            //pos.y += playerSpeed;
            body.MovePosition(new Vector2(pos.x, pos.y + playerSpeed));
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            //pos.x -= playerSpeed;
            body.MovePosition(new Vector2(pos.x - playerSpeed, pos.y));
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            
            //pos.y -= playerSpeed;
            body.MovePosition(new Vector2(pos.x, pos.y - playerSpeed));
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //pos.x += playerSpeed;
            body.MovePosition(new Vector2(pos.x + playerSpeed, pos.y));
        }

        // transform.position = pos;

    }



    //for collisions
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Item Obtained");
        Destroy(coll.gameObject);

    }
}
