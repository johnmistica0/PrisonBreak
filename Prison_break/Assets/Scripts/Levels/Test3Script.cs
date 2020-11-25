using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3Script : MonoBehaviour
{

    // Start is called before the first frame update
    public GridScript grid;
    void Start()
    {   
        grid = new GridScript(1, "./Assets/Maps/test3.csv");
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
