using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class GuardPathing : MonoBehaviour
{

    private float waitTime;
    public float startWaitTime;

    public float speed = 12f;

    public Vector2 moveSpot;
    private Rigidbody2D body;

    public GameObject player;

    void Start()
    {
        waitTime = startWaitTime;
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
     
        moveSpot = new Vector2(Random.Range(1, 12), Random.Range(1, 12));
        

    }

   void Update()
    {


        body.MovePosition(Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime));
        if(Vector2.Distance(transform.position, player.transform.position) <= 3.0f)
        {
            moveSpot = player.transform.position;
        }
        if (Vector2.Distance(transform.position, moveSpot) <= 0.3f)
        {
            moveSpot = new Vector2(Random.Range(1, 12), Random.Range(1, 12));
        }


    }

    


}
