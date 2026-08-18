using UnityEngine;


[System.Serializable]
public class Skin
{
    public string id;          
    public string displayName; 
    public int price;          
    public bool isUnlocked;    
    public bool isSelected;    

    public Sprite[] animationFrames; 

    public Skin(string id, string displayName, int price, bool isUnlocked = false, bool isSelected = false)
    {
        this.id = id;
        this.displayName = displayName;
        this.price = price;
        this.isUnlocked = isUnlocked;
        this.isSelected = isSelected;
    }
}