using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{

<<<<<<< HEAD
    public float playerSpeed = 0.001f;
    public LayerMask mask = 1 << 8;
    BoxCollider2D boxCollider;
    private void Start(){
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.edgeRadius = 1;
    
        boxCollider.size = new Vector2(1,1);
    }
    
=======
    public float playerSpeed = 0.1f;
    public Vector3 myPos;
    //public Transform myPlay;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
    }

>>>>>>> 14861aa117881ee7e5320058d89c29be0300d113
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

        transform.position = pos;

    }
<<<<<<< HEAD
    // private void OnTriggerEnter2D(Collider2D otherObject) {
    //     Vector3 pos = transform.position;
    //     Debug.Log("Cool1");
    //     pos.x += playerSpeed;
    //     transform.position = pos;
    //     if((mask.value) != otherObject.gameObject.layer){
        
    //     }
        
        
    // }
    private void OnCollisionEnter2D(Collision2D otherObject) {
        Debug.Log("Yesss");
        if((mask.value) != otherObject.gameObject.layer){
        
        }
=======



    //for collisions
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Item Obtained");
        Destroy(coll.gameObject);
>>>>>>> 14861aa117881ee7e5320058d89c29be0300d113
    }
}
