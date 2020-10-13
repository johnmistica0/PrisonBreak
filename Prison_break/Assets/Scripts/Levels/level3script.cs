using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3script : MonoBehaviour
{
    // Start is called before the first frame update
    public GridScript grid;
    void Start()
    {
        grid = new GridScript(1, "./Assets/Maps/level3.csv");

    }


    // Update is called once per frame
    void Update()
    {

    }
}
