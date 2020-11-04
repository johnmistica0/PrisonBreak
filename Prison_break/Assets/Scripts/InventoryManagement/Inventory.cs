using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    private int selected;
    private int amountOfItems;
    private int maxInventory;
    private InventoryObject [] itemObjects;
    private GameObject currentInventory;
    private static Sprite[] inventorySprites;
    private static Sprite[] itemSprites;
    private Canvas canvas;
    GameObject player;
    MoveCharacter movement;
    public enum ItemTypes {
        Empty,
        AttackBoost,
        SpeedBoost,
        StealthBoost,
        Key,
    }
    //Loads item sprites and inventory sprites
    private void loadResources(){
        inventorySprites = Resources.LoadAll<Sprite>("Inventory");
        itemSprites = Resources.LoadAll<Sprite>("Items");
    }

    void Start(){
        maxInventory = 5;
        amountOfItems = 0;
        loadResources();
        //holds item objects
        currentInventory = new GameObject("CurrentInventory");
        itemObjects = new InventoryObject[maxInventory];
        canvas = currentInventory.AddComponent<Canvas>();
        currentInventory.AddComponent<CanvasScaler>();
        currentInventory.AddComponent<GraphicRaycaster>();
        //sets canvas over camera
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.sortingOrder = 20;   
        canvas.pixelPerfect = true;
        selected = 0;
        createInventoryArray();


    }
    private void createInventoryArray(){
        for(int i = 0; i < itemObjects.Length; i++){
            itemObjects[i] = new InventoryObject(i, new Vector2(-8.8F + i*1.4F, -4.1F));
            itemObjects[i].setParent(canvas.transform);
            if(i == selected){
                itemObjects[i].setSelected();
            }

        }
    }
    private void toggleSelected(){
        int nextSelected = (selected + 1) % maxInventory;
        itemObjects[selected].setUnSelected();
        itemObjects[nextSelected].setSelected();
        selected = nextSelected;
    }
    //choose selected based on keyboard number

    public void addItemToInventory(Item item){
        if(!checkIfFull()){
            for(int i = 0; i < itemObjects.Length; i++){
                if(itemObjects[i].GetItem().GetItemType() == ItemTypes.Empty){
                    itemObjects[i].setItem(item);
                    
                    amountOfItems++;
                    break; 
                }
            }

        }else{
            Debug.Log("Full");
            //do not allow to add and warn user
        }
    }
    public Item useItem(){
        Item item = itemObjects[selected].GetItem();
        //check random since no stealth boost detected
        //change player stats based on item;
        //send over objects as a package
        return item;
    }
    public void resetItem(){
        amountOfItems--;
        itemObjects[selected].reset();
    }
    private bool checkIfFull(){
        return (amountOfItems == maxInventory);        
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.I))
        {
            toggleSelected();   
        }
        if(Input.GetKeyDown(KeyCode.U)){
            ItemTypes boost = useItem().GetItemType();
            adjustPlayerStats(boost);
            resetItem();
        }
        
    }
    private void adjustPlayerStats(ItemTypes itemType){
        if(!player){
            player = GameObject.Find("Player");
            movement = player.GetComponent<MoveCharacter>();
        }
        //maybe implement a timer for each boost
        switch(itemType){
            case ItemTypes.AttackBoost:
                break;
            case ItemTypes.SpeedBoost:
                movement.playerSpeed = movement.playerSpeed+movement.playerSpeed*0.5F;
                break;
            case ItemTypes.StealthBoost:
                break;
            case ItemTypes.Key:
                print("used key");
                MoveCharacter.usedKey = true;
                break;

        }

    }
    //Each inventory container that holds an item sprite
    public class InventoryObject{
        Item item;
        GameObject inventoryObject;
        GameObject itemObject;
        Vector2 position;
        SpriteRenderer spriteInv;
        Button buttonInv;
        SpriteRenderer spriteItem;
        
        //Creates the Sprites and Positions the inventor
        public InventoryObject(int index, Vector2 position){
            //initialize item to empty;
            item  = new Item();
            item.SetItemType(ItemTypes.Empty);
            //inventory object position
            this.position = position;
            inventoryObject = new GameObject("Inventory: " + index);
            itemObject = new GameObject("Item :" + index);
            //sets positon to bottom left
            RectTransform rectTransform = inventoryObject.AddComponent<RectTransform>();
            rectTransform.localPosition = position;
            RectTransform rectTransform1 = itemObject.AddComponent<RectTransform>();
            rectTransform1.parent = rectTransform;
            rectTransform1.localPosition = new Vector3(0,0,0);
            //add item image to inventory cubes

            rectTransform.sizeDelta = new Vector2(1, 1);
            rectTransform.localScale = new Vector3(0.15F, 0.15F, 0.15F);
            rectTransform1.sizeDelta = new Vector2(1, 1);
            rectTransform1.localScale = new Vector3(6, 6, 0);
            spriteInv = inventoryObject.AddComponent<SpriteRenderer>();
            spriteItem = itemObject.AddComponent<SpriteRenderer>();
            buttonInv = inventoryObject.AddComponent<Button>();
            // spriteItem.sprite = (Sprite)inventorySprites[0];
            spriteInv.sprite = (Sprite)inventorySprites[0];
            spriteInv.sortingOrder = 21;
            spriteItem.sortingOrder = 22;
            
        }
        public void setParent(Transform parent){
            inventoryObject.transform.parent = parent;

        }
        public void setSelected(){
            spriteInv.color = new Color(255, 255, 255, 0.6F);
        }
        public void setUnSelected(){
            spriteInv.color = new Color(255, 255, 255, 1);
        }
        public void setItem(Item item){
            this.item = item;
            spriteItem.sprite = (Sprite) itemSprites[(int) item.GetItemType()];
        }
        public void reset(){
            this.item.SetItemType(ItemTypes.Empty);
            spriteItem.sprite = null;
        }
        public Item GetItem(){
            return item;
        }


    }
    

}