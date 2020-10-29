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
    public static bool usedKey = true;

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

        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            body.MovePosition(new Vector2(pos.x, pos.y + playerSpeed));
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            body.MovePosition(new Vector2(pos.x - playerSpeed, pos.y));
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            body.MovePosition(new Vector2(pos.x, pos.y - playerSpeed));
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            body.MovePosition(new Vector2(pos.x + playerSpeed, pos.y));
        }
        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            body.MovePosition(new Vector2(pos.x - playerSpeed, pos.y + playerSpeed));
        }
        if (Input.GetKey("w") && Input.GetKey("d"))
        {
            body.MovePosition(new Vector2(pos.x + playerSpeed, pos.y + playerSpeed));
        }
        if (Input.GetKey("s") && Input.GetKey("a"))
        {
            body.MovePosition(new Vector2(pos.x - playerSpeed, pos.y - playerSpeed));
        }
        if (Input.GetKey("s") && Input.GetKey("d"))
        {
            body.MovePosition(new Vector2(pos.x + playerSpeed, pos.y - playerSpeed));
        }



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
            if(usedKey == true)
            {
                levelKey++;
                string sceneString = "Level" + levelKey.ToString() + "Scene";
                Debug.Log(levelKey);
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: sceneString);
                GameObject door = GameObject.Find("Door");
            }
            else
            {
                print("Dont have key");
            }
            
        }
        else if (coll.gameObject.name == "NPC")
        {
            /*
            levelKey = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1Scene");
            */
            UnityEngine.SceneManagement.SceneManager.LoadScene("FailureScene");
        }



    }
}
