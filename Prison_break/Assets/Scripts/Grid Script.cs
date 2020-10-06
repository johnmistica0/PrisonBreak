using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CodeMonkey.Utils;

public class GridScript
{
    //Dimensions for the map, intialized at constructor
    private int width;
    private int height;
    private float cellSize;
    //matrix for the map, again initialized at constructor
    private int[,] gridArray;
    public LayerMask walls;
    //text mesh array for the text within the grid
    private TextMesh[,] debugText;
    private GameObject[,] objects;
    private GameObject player;
    public GridScript(float cs, string path)
    {
        cellSize = cs;
        loadMapFile(path);
        player = createPlayerObject();
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                // debugText[i,j] = UtilsClass.CreateWorldText(gridArray[i, j].ToString(),null, GetWorldPositions(i,j) + new Vector3(cellSize,cellSize) * 0.5f,20,Color.white,TextAnchor.MiddleCenter );
                Debug.Log(i + " " + j);
                objects[i, j] = createTileObject(gridArray[i, j], i, j);


                //drawing lines to visually see bounds. Need to be able to see gizmos during runtime in order for this to work
                // Debug.DrawLine(GetWorldPositions(i, j), GetWorldPositions(i, j + 1), Color.white, 100f);
                // Debug.DrawLine(GetWorldPositions(i, j), GetWorldPositions(i + 1, j), Color.white, 100f);



            }
        }

        //the lines in the loop do not close off the top and right borders of the grid. These two lines basically draw the top and right border
        //closing it off completing the grid structure.
        Debug.DrawLine(GetWorldPositions(0, height), GetWorldPositions(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPositions(width, 0), GetWorldPositions(width, height), Color.white, 100f);

        //testing the setvalue function
        SetValue(4, 2, 69);


    }
    //using premade packages, just scales the positions of the world by the cellsize made during construction
    private Vector3 GetWorldPositions(int x, int y)
    {
        return new Vector3(x, y) * cellSize;

    }

    private GameObject createPlayerObject()
    {
        Texture2D tex = new Texture2D(100, 100);
        GameObject player = new GameObject("Player", typeof(SpriteRenderer));
        Transform transform = player.transform;

        //adds r2b2d and moveCharacter script to player GameObject
        player.AddComponent<MoveCharacter>();
        player.AddComponent<Rigidbody2D>();

        //does tranformations on sprites position and scale
        transform.SetParent(null, false);
        transform.localPosition = GetWorldPositions(0, 0);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //creates a new spriteRenderer for the player GameObject
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //set to 32767(max) - prevents player from clipper under any other sprite
        spriteRenderer.sortingOrder = 32767;

        //sets rb2d gravity to 0
        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;

        //loads wizard sprite onto player
        Sprite[] sprites;
        sprites = Resources.LoadAll<Sprite>("Fantasy");
        spriteRenderer.sprite = (Sprite)sprites[4];

        return player;
    }

    private GameObject createTileObject(int type, int x, int y){
        Texture2D tex = new Texture2D(100, 100);
        GameObject tile = new GameObject(x + ""+ "" + y,typeof(SpriteRenderer));  
        Transform transform = tile.transform;
        transform.SetParent(null, false);
        //Sets the position of the object
        transform.localPosition = GetWorldPositions(x, y);
        //Creates a sprite renderer to render the object from the tile
        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(tex,new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //tile.layer sets the layer of the object for this case layer 7 is non colliding and layer 8 is colliding
        Sprite [] sprites; 
        sprites = Resources.LoadAll<Sprite>("Tiles");


        
        if(type == 0){
            spriteRenderer.sprite = (Sprite) sprites [2];
            // spriteRenderer.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);

            tile.layer = 7;
        }else if(type == 1){
            spriteRenderer.sprite = (Sprite) sprites [3];
            tile.layer = 8;
        }else if(type == 2){
            spriteRenderer.color = new Color(0, 52, 209, 1.0f);
            tile.layer = 7;
        }else if(type == 3){
            spriteRenderer.color = new Color(255, 234, 0, 1.0f);
            tile.layer = 7;
        }

        return tile;

    }
    private void loadMapFile(string path){
        var reader = new StreamReader(File.OpenRead(path));
        int counter = 0;
        while(!reader.EndOfStream){
            var line = reader.ReadLine();
            var list = line.Split(','); 
            if(counter == 0){
                width = int.Parse(list[0]);
                height = int.Parse(list[1]);
                //initialize array from file width and height the first entry in the file
                gridArray = new int[width, height];
                //Giving the map some textures to be seen on the scene. This will then now allow it to be interactable. Since
                //I need to add some kind of texture to every element, we need a nested loop to modify every tile in the grid.
                debugText = new TextMesh[width, height];
                objects = new GameObject[width, height];
            }else{
                //the rest of the entries are the types of tiles in the game 1 being wall 0 being walkable and the rest could be doors or items
                int x = int.Parse(list[0]);
                int y = int.Parse(list[1]);
                //checking if out of bounds from grid
                if(x < width && y < height){
                    int tileType = int.Parse(list[2]);
                    gridArray[x,y] = tileType;
                    Debug.Log(gridArray[x,y]); 
                }else{
                    //exit from game since map is incorrect format
                }
            }
            counter++;
        }
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
            // debugText[x, y].text = gridArray[x, y].ToString();
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
