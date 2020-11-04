using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class GuardPathing : MonoBehaviour
{

    private float waitTime;
    public float startWaitTime;

    public float speed = 7f;
    int counter = 0;
    public Vector2 moveSpot;
    private Rigidbody2D body;

    public GameObject player;

    int maxWidth = 15;
    int maxHeight = 15;
    void Start()
    {   
        waitTime = startWaitTime;
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        GameObject grid = GameObject.Find("Grid"    );
        moveSpot = new Vector2(Random.Range(1, maxHeight), Random.Range(1, maxWidth));
        

    }
    void isStuck(){
        moveSpot = new Vector2(Random.Range(1, maxHeight), Random.Range(1, maxWidth));
    }

   void Update()
    {
        counter++;

        body.MovePosition(Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime));
        if(Vector2.Distance(transform.position, player.transform.position) <= 3.0f)
        {
            moveSpot = player.transform.position;
        }
        if (Vector2.Distance(transform.position, moveSpot) <= 0.3f)
        {
            moveSpot = new Vector2(Random.Range(1, maxHeight), Random.Range(1, maxWidth));
        }
        if(Mathf.Abs(body.velocity.x+body.velocity.y) == 0 && counter > 100){
            isStuck();
            counter = 0;
        }


    }

    


}
