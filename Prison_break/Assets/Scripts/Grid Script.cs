using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridScript
{
    //Dimensions for the map, intialized at constructor
    private int width;
    private int height;
    private float cellSize;
    //matrix for the map, again initialized at constructor
    private int[,] gridArray;

    //text mesh array for the text within the grid
    private TextMesh[,] debugText;
    public GridScript(int w, int h, float cs)
    {
        width = w;
        height = h;
        cellSize = cs;
        //initialize the width and height via constructor params, now create the grid with them
        gridArray = new int[width, height];
        debugText = new TextMesh[width, height];

        //Debug.Log(width + " " + height);

        //Giving the map some textures to be seen on the scene. This will then now allow it to be interactable. Since
        //I need to add some kind of texture to every element, we need a nested loop to modify every tile in the grid.
        for(int i = 0; i < gridArray.GetLength(0);i++)
        {
            for(int j = 0; j < gridArray.GetLength(1);j++)
            {
                debugText[i,j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(),null, GetWorldPositions(i,j) + new Vector3(cellSize,cellSize) * 0.5f,20,Color.white,TextAnchor.MiddleCenter );
                //drawing lines to visually see bounds. Need to be able to see gizmos during runtime in order for this to work
                Debug.DrawLine(GetWorldPositions(i, j), GetWorldPositions(i, j + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPositions(i, j), GetWorldPositions(i + 1, j), Color.white, 100f);



            }
        }
        //the lines in the loop do not close off the top and right borders of the grid. These two lines basically draw the top and right border
        //closing it off completing the grid structure.
        Debug.DrawLine(GetWorldPositions(0,height), GetWorldPositions(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPositions(width,0), GetWorldPositions(width,height), Color.white, 100f);

        //testing the setvalue function
        SetValue(4, 2, 69);


    }
    //using premade packages, just scales the positions of the world by the cellsize made during construction
    private Vector3 GetWorldPositions(int x, int y)
    {
        return new Vector3(x, y) * cellSize;

    }


    private void getXY(Vector3 wp, out int x, out int y)
    {
        x = Mathf.FloorToInt(wp.x / cellSize);
        y = Mathf.FloorToInt(wp.y / cellSize);
    }
    //you can interact with the grid to set certain values to it, making for barriers or having certain values be walls/doors/items/gaurds
    public void SetValue(int x, int y, int value)
    {
        //checks the bounds of x,y sent in to make sure it is within the bounds of the grid
        if( x>=0 && y>=0 && x< width && y < height)
        {
            //now set the value to the proper grid position
            gridArray[x, y] = value;
            debugText[x, y].text = gridArray[x, y].ToString();
        }


    }


    //different declaration of setvalue, with now taking in a world position object instead of stand alone ints x and y,
    //which in turn calls the other set value function as we are extracting the adjusted x,y values
    public void SetValue(Vector3 wp, int val)
    {
        int xPos;
        int yPos;
        getXY(wp, out xPos, out yPos);
        SetValue(xPos, yPos, val);


    }

}
