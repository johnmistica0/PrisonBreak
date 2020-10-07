using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	// Contains the player object, set in Grid Script.cs
    public GameObject player;

    // Contains the Rigidbody2D of the camera
    private Rigidbody2D body;

    private void Start()
    {
    	// Get the body and freeze the rotaiton
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
    }

    // LateUpdate is called after Update each frame
    private void LateUpdate () 
    {
    	// After the player moves, move the camera to the players current position
        body.position = player.transform.position;
    }
}