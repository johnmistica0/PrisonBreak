using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{

    public float playerSpeed = 0.001f;
    public LayerMask mask = 1 << 8;
    BoxCollider2D boxCollider;
    private void Start(){
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.edgeRadius = 1;
    
        boxCollider.size = new Vector2(1,1);
    }
    
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
    }
}
