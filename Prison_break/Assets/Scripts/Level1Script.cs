using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridScript grid = new GridScript(1, "./Assets/Maps/level1.csv");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
