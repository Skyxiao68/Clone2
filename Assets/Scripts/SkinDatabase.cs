using System.Collections.Generic;
using UnityEngine;

public static class SkinDatabase
{
    public static List<Skin> AllSkins = new List<Skin>();

    static SkinDatabase()
    {
        AddSkin("default", "Default Skin", 0, true, true);
        AddSkin("blue", "Blue Skin", 20);
        AddSkin("green", "Green Skin", 40);
        AddSkin("purple", "Purple Skin", 60);
        AddSkin("gold", "Gold Skin", 80);
    }

    private static void AddSkin(
        string id,
        string displayName,
        int price,
        bool isUnlocked = false,
        bool isSelected = false
    )
    {
        Skin skin = new Skin(id, displayName, price, isUnlocked, isSelected);

        skin.animationFrames = Resources.LoadAll<Sprite>("Skins/" + id);
        AllSkins.Add(skin);
    }

    public static Skin GetSkinByID(string id)
    {
        return AllSkins.Find(skin => skin.id == id);
    }
}
