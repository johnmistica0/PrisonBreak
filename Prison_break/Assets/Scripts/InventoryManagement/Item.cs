
using UnityEngine;
using UnityEngine.UI;
//Item that is attached to the  item object so it can distinguish its type
public class Item : MonoBehaviour{
        Inventory.ItemTypes itemType;
        bool key;

        void onStart(){
            itemType = Inventory.ItemTypes.Empty;
            key = false;
            
        }

        public void SetItemType(Inventory.ItemTypes itemType){
            this.itemType = itemType;

        }
        public Inventory.ItemTypes GetItemType(){
            return itemType;
        }
        //check if the item is a key if so then can be used to open door
        public bool isKey(){
            return key;
        }

        
}