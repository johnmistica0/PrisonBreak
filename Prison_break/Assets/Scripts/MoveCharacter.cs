using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCharacter : MonoBehaviour
{


    public float playerSpeed = 0.1f;
    public Vector3 myPos;
    //public Transform myPlay;
    private Rigidbody2D body;
    static int levelKey = 1;
    GameObject inventory;
    Inventory inventoryScript;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Invent");
        inventoryScript = inventory.GetComponent<Inventory>();
        body.freezeRotation = true;
        Vector3 startingPos = new Vector3(1, 1, 0);
        transform.position = startingPos;
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
        if(coll.gameObject.name == "Item")
        {

            Debug.Log("Item Obtained");
            Item item = coll.gameObject.GetComponent<Item>();
            
            Debug.Log(item.GetItemType());
            inventoryScript.addItemToInventory(item);
            Destroy(coll.gameObject);
            
        }
        else if(coll.gameObject.name == "Door")
        {
            
            levelKey++;
            string sceneString = "Level" + levelKey.ToString() + "Scene";
            Debug.Log(levelKey);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: sceneString);
            
        }
        else if (coll.gameObject.name == "NPC")
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1Scene");

        }



    }
}
