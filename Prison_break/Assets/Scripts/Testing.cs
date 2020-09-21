using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


//this is a temporary testing file to test the behavior of my grid/map. Simply creates the object and attaches this script to 
// a different game object.
public class Testing : MonoBehaviour
{
    private GridScript gs;
    private void Start()
    {
        // gs = new GridScript(15, 15,5f); 
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            gs.SetValue(UtilsClass.GetMouseWorldPosition(), 69);
        }
    }
}
