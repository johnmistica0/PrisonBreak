using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CodeMonkey.Utils;
using UnityEngine.AI;
using UnityEngine.UI;

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
    private GameObject[] invisibleWalls;
    private GameObject invisibleWallContainer;
    private GameObject floorContainer;
    private GameObject doorsContainer;
    private GameObject itemsContainer;
    private GameObject wallsContainer;
    private GameObject player;
    private GameObject[] npc;
    private int NPCnum;
    private int[] NPClocation;
    private Sprite[] itemSprites;
    private Sprite [] tileSprites;
    private Sprite[] doorSprites;
    private Sprite [] playerSprites;
    private Sprite[] moreTileSprites;
    private Dictionary<string, int> items = new Dictionary<string, int>();
    private Dictionary<string, int> tileColor = new Dictionary<string, int>();

    private ItemFactory itemFactory;
    

    public GridScript(float cs, string path)
    {
        MoveCharacter.usedKey = true;
        TextInput.doorKey = "";
        cellSize = cs;
        loadMapFile(path);
        //creates containers for objects
        createContainers();
        //loads sprites into game
        loadResources();
        itemFactory = new ItemFactory();

        player = createPlayerObject();

        for(int i = 0; i < npc.GetLength(0); i++)
            npc[i] = createNPCObject(NPClocation[i],NPClocation[i+1]);

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                objects[i, j] = createTileObject(gridArray[i, j], i, j);

            }
        }
        //creates invisible walls around the grid
        renderInvisibleWalls();

        // Create Camera
        createCameraObject();
        createTextField();
    }
    private void loadResources(){
        itemSprites = Resources.LoadAll<Sprite>("Items");

        tileSprites = Resources.LoadAll<Sprite>("Tiles");
        doorSprites = Resources.LoadAll<Sprite>("Door");
        playerSprites  = Resources.LoadAll<Sprite>("prison");
        moreTileSprites = Resources.LoadAll<Sprite>("Prison_A5");
    }

    private void createContainers(){
        floorContainer = new GameObject("FloorContainer");
        itemsContainer = new GameObject("ItemsContainer");
        doorsContainer = new GameObject("DoorsContainer");
        wallsContainer = new GameObject("WallsContainer");
    }


    //using premade packages, just scales the positions of the world by the cellsize made during construction
    private Vector3 GetWorldPositions(int x, int y)
    {
        return new Vector3(x, y) * cellSize;

    }

    private void createCameraObject()
    {
        GameObject camera = Camera.main.gameObject;
        camera.AddComponent<MoveCamera>();
        camera.GetComponent<MoveCamera>().player = player;
    }

    private GameObject createPlayerObject()
    {
        Texture2D tex = new Texture2D(100, 100);
        GameObject player = new GameObject("Player", typeof(SpriteRenderer));
        Transform transform = player.transform;

        //adds r2b2d and moveCharacter script to player GameObject
        player.AddComponent<MoveCharacter>();
        player.AddComponent<Rigidbody2D>();

        //Add a collider component to the player to detect collisions with walls, items, etc
        player.AddComponent<BoxCollider2D>();
        //set it to a variable to modify it
        BoxCollider2D b2d = player.GetComponent<BoxCollider2D>();
        //set the size of the colliders
        b2d.size = new Vector2(1.65f, 3.65f);
        b2d.offset = new Vector2(0, 1f);
        

        //also adding a polygon collider to make it more smooth
        //player.AddComponent<PolygonCollider2D>();


        //creates a new spriteRenderer for the player GameObject
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //set to 20 since need menus in front of it
        spriteRenderer.sortingOrder = 20;

        //does tranformations on sprites position and scale
        transform.SetParent(null, false);
        transform.localPosition = GetWorldPositions(0, 0);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        //sets rb2d gravity to 0
        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2d.interpolation = RigidbodyInterpolation2D.Extrapolate;
        rb2d.angularDrag = 0;
        rb2d.freezeRotation = true;
        //loads wizard sprite onto player
        
        spriteRenderer.sprite = (Sprite)playerSprites[30];

        return player;
    }

    private GameObject createNPCObject(int x, int y)
    {
        Texture2D tex = new Texture2D(100, 100);
        GameObject npc = new GameObject("NPC", typeof(SpriteRenderer));
        Transform transform = npc.transform;

        npc.AddComponent<Rigidbody2D>();
        npc.AddComponent<GuardPathing>();
        npc.GetComponent<GuardPathing>().player = player;
        npc.AddComponent<NavMeshAgent>();

        //Add a collider component to the player to detect collisions with walls, items, etc
        npc.AddComponent<BoxCollider2D>();
        //set it to a variable to modify it
        BoxCollider2D b2d = npc.GetComponent<BoxCollider2D>();
        //set the size of the colliders
        b2d.size = new Vector2(1.65f, 3.65f);
        b2d.offset = new Vector2(0, 1f);
        b2d.isTrigger = false;

        //does tranformations on sprites position and scale
        transform.SetParent(null, false);
        transform.localPosition = GetWorldPositions(x, y);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        //creates a new spriteRenderer for the player GameObject
        SpriteRenderer spriteRenderer = npc.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //set to 20 to render menu infront of it 
        spriteRenderer.sortingOrder = 20;

        //sets rb2d gravity to 0
        Rigidbody2D rb2d = npc.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2d.interpolation = RigidbodyInterpolation2D.Extrapolate;
        rb2d.angularDrag = 0;
        rb2d.freezeRotation = true;
        //loads inmate sprite onto player

        spriteRenderer.sprite = (Sprite)playerSprites[9];

        return npc;
    }

    private GameObject createTextField()
    {
        GameObject textField = new GameObject("InputField");
        textField.AddComponent<InputField>();
        Transform transform = textField.transform;
        transform.SetParent(null, false);
        transform.localPosition = GetWorldPositions(5, 5);

        return textField;
    }

    private void renderInvisibleWalls(){
        invisibleWallContainer = new GameObject("InvisibleWallContainer");
        //creates two walls per for loop, sets box one position under 0 and one at the height and width
        for(int i = 0; i < width; i++){
            GameObject invWall1 = new GameObject(i + ","+ -1,typeof(BoxCollider2D)); 
            GameObject invWall2 = new GameObject(i + "," + width, typeof(BoxCollider2D));
            Transform transform1 = invWall1.transform;
            Transform transform2 = invWall2.transform;
            transform1.SetParent(invisibleWallContainer.transform, false);
            transform2.SetParent(invisibleWallContainer.transform, false);
            transform1.localPosition = GetWorldPositions(i, -1);
            transform2.localPosition = GetWorldPositions(i, height);
        }
        for(int i = 0; i < height; i++){
            GameObject invWall1 = new GameObject(-1 + ","+ i,typeof(BoxCollider2D)); 
            GameObject invWall2 = new GameObject(height + 1 + "," + i, typeof(BoxCollider2D));
            Transform transform1 = invWall1.transform;
            Transform transform2 = invWall2.transform;
            transform1.SetParent(invisibleWallContainer.transform, false);
            transform2.SetParent(invisibleWallContainer.transform, false);
            transform1.localPosition = GetWorldPositions( -1, i);
            transform2.localPosition = GetWorldPositions( width, i);
        }

        

    }

    private GameObject createTileObject(int type, int x, int y){
        //Creates texture for tile also determines size
        Texture2D tex = new Texture2D(100, 100);
        GameObject tile = new GameObject(x + "," + y,typeof(SpriteRenderer));  
        Transform transform = tile.transform;
        //Sets the position of the object
        transform.localPosition = GetWorldPositions(x, y);
        //Creates a sprite renderer to render the object from the tile
        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(tex,new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        


        
        if(type == 0){//default grassy background
            transform.SetParent(floorContainer.transform, false);
            spriteRenderer.sprite = (Sprite) moreTileSprites [0];

            int customTile;
            if (tileColor.TryGetValue(x.ToString() + y.ToString(), out customTile))
            {
                spriteRenderer.sprite = (Sprite)moreTileSprites[customTile];
            }

        }
        else if(type == 1){//wall tiles
            transform.SetParent(wallsContainer.transform, false);
            spriteRenderer.sprite = (Sprite) tileSprites [4];
            //adding just the collider to the walls, no on trigger effects needed.
            tile.AddComponent<BoxCollider2D>();
            BoxCollider2D b2d = tile.GetComponent<BoxCollider2D>();
            b2d.size = new Vector2(1, 1);

            
        }else if(type == 2){//item tiles

            int itemType;

            if (items.TryGetValue(x.ToString() + y.ToString(), out itemType)) { }
            if(itemType == 4)
            {
                MoveCharacter.usedKey = false;
            }

            spriteRenderer.sprite = (Sprite)moreTileSprites[0];

            if (itemType != 4 || (itemType == 4 && TextInput.doorKey.Length == 0))//if itemType key but has no password requirement or if its not of itemType key
            {
                //Adding a duplicate layer beneath it, so that when collision for the item is detected,
                //we simply destroy that game object, revealing the default tile beneath
                GameObject tile2 = new GameObject(x + "," + y, typeof(SpriteRenderer));
                Transform transform2 = tile2.transform;
                transform2.SetParent(itemsContainer.transform, false);
                transform.SetParent(floorContainer.transform, false);
                transform2.localPosition = GetWorldPositions(x, y);
                SpriteRenderer spriteRenderer2 = tile2.GetComponent<SpriteRenderer>();
                spriteRenderer2.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                spriteRenderer2.sprite = (Sprite)moreTileSprites[0];
                //sorting order so that the item image renders over the grass block
                spriteRenderer.sortingOrder = 1;
                spriteRenderer2.sortingOrder = 0;

                //code for the item tile
                spriteRenderer.sprite = (Sprite)itemSprites[itemType];

                tile.AddComponent<BoxCollider2D>();

                BoxCollider2D b2d = tile.GetComponent<BoxCollider2D>();

                Item item = tile.AddComponent<Item>();

                // Spawn item based on type
                switch (itemType)
                {
                    case 0:
                        item.SetItemType((itemFactory.generateEmpty()));
                        break;
                    case 1:
                        item.SetItemType((itemFactory.generateAttackBoost()));
                        break;
                    case 2:
                        item.SetItemType((itemFactory.generateSpeedBoost()));
                        break;
                    case 3:
                        item.SetItemType((itemFactory.generateStealthBoost()));
                        break;
                    case 4:
                        item.SetItemType((itemFactory.generateKey()));
                        break;
                }
                tile.gameObject.name = "Item";

                b2d.isTrigger = true;
            }
            

        }
        else if(type == 3){//"door" tiles
            GameObject tile2 = new GameObject(x + "," + y, typeof(SpriteRenderer));
            Transform transform2 = tile2.transform;
            transform2.SetParent(itemsContainer.transform, false);
            transform.SetParent(floorContainer.transform, false);
            transform2.localPosition = GetWorldPositions(x, y);
            SpriteRenderer spriteRenderer2 = tile2.GetComponent<SpriteRenderer>();
            spriteRenderer2.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            spriteRenderer2.sprite = (Sprite)tileSprites[4];
            //sorting order so that the item image renders over the grass block
            spriteRenderer.sortingOrder = 1;
            spriteRenderer2.sortingOrder = 0;


            transform.SetParent(doorsContainer.transform, false);
            spriteRenderer.sprite = (Sprite)doorSprites[2];
            tile.AddComponent<BoxCollider2D>();
            BoxCollider2D b2d = tile.GetComponent<BoxCollider2D>();
            b2d.size = new Vector2(1, 1);
            b2d.isTrigger = true;

            tile.gameObject.name = "Door";
        }

        return tile;

    }

    
    private void loadMapFile(string path){
        var reader = new StreamReader(File.OpenRead(path));
        int counter = 0;

        // While there are lines to read
        while(!reader.EndOfStream){
            var line = reader.ReadLine();
            var list = line.Split(',');
            if (counter == 0)
            {
                width = int.Parse(list[0]);
                height = int.Parse(list[1]);
                //initialize array from file width and height the first entry in the file
                gridArray = new int[width, height];
                //Giving the map some textures to be seen on the scene. This will then now allow it to be interactable. Since
                //I need to add some kind of texture to every element, we need a nested loop to modify every tile in the grid.
                debugText = new TextMesh[width, height];
                objects = new GameObject[width, height];
            }
            else if (counter == 1)//loads NPC data from csv file
            {
                //number of NPCs on map
                NPCnum = int.Parse(list[0]);
                npc = new GameObject[NPCnum];
                NPClocation = new int[2 * NPCnum];
                for(int i = 1; i < NPCnum+1; i++)
                {
        
                    NPClocation[i - 1] = int.Parse(list[i + (i-1)]);
                    NPClocation[i] = int.Parse(list[i + 1 + (i - 1)]);

                    Debug.Log(NPClocation[i-1]);
                    Debug.Log(NPClocation[i]);
                }

            }
            else
            {
                //the rest of the entries are the types of tiles in the game 1 being wall 0 being walkable and the rest could be doors or items
                int x = int.Parse(list[0]);
                int y = int.Parse(list[1]);
                //checking if out of bounds from grid
                if (x < width && y < height)
                {
                    int tileType = int.Parse(list[2]);
                    gridArray[x, y] = tileType;
                    if(tileType == 2)
                    {
                        items.Add(x.ToString() + y.ToString(), int.Parse(list[3]));
                        if (int.Parse(list[3]) == 4)
                        {
                            Debug.Log(list[4]);
                            TextInput.doorKey = list[4];
                        }
                        
                    }
                    if(tileType == 0)
                    {
                        tileColor.Add(x.ToString() + y.ToString(), int.Parse(list[3]));
                        Debug.Log(int.Parse(list[3]));
                    }
                }
                else
                {
                    //exit from game since map is incorrect format
                }
            }
            counter++;
        }
    }
    public int getWidth(){
        return width;
    }
    public int getHeight(){
        return height;
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
