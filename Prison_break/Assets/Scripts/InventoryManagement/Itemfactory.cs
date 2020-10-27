using System;
class ItemFactory{
    private Random rand;
    private int amountOfItems;
    //creates different items could be used to create at different rates
    public ItemFactory(){
        rand = new Random();
        amountOfItems = Enum.GetNames(typeof(Inventory.ItemTypes)).Length;
    }

    public Inventory.ItemTypes generateRandomItem(){
        return (Inventory.ItemTypes) generateRandomInt();
    }
    private int generateRandomInt(){
        return rand.Next(1,4);
    }
   

}