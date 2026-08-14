using System.Collections.Generic;

public static class SkinDatabase
{
    public static List<Skin> AllSkins = new List<Skin>
    {
        new Skin(
            id: "default",
            displayName: "Default Skin",
            price: 0,
            isUnlocked: true,
            isSelected: true
        ),
        new Skin(id: "green", displayName: "Green Skin", price: 20),
        new Skin(id: "blue", displayName: "Blue Skin", price: 40),
        new Skin(id: "gold", displayName: "Gold Skin", price: 100),
        new Skin(id: "purple", displayName: "Purple Skin", price: 150)
    };

    public static Skin GetSkinByID(string id)
    {
        return AllSkins.Find(skin => skin.id == id);
    }
}
