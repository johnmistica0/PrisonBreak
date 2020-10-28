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
    public Inventory.ItemTypes generateEmpty()
    {
        return (Inventory.ItemTypes)(0);
    }
    public Inventory.ItemTypes generateAttackBoost()
    {
        return (Inventory.ItemTypes)(1);
    }
    public Inventory.ItemTypes generateSpeedBoost()
    {
        return (Inventory.ItemTypes)(2);
    }
    public Inventory.ItemTypes generateStealthBoost()
    {
        return (Inventory.ItemTypes)(3);
    }
    public Inventory.ItemTypes generateKey()
    {
        return (Inventory.ItemTypes)(4);
    }
    private int generateRandomInt(){
        return rand.Next(1,4);
    }
   

}