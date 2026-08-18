using UnityEngine;

public class TestSkinData : MonoBehaviour
{
    private void Start()
    {
        foreach (var skin in SkinDatabase.AllSkins)
        {
            Debug.Log($"Skin ID: {skin.id}, Display Name: {skin.displayName}, Price: {skin.price}, Is Unlocked: {skin.isUnlocked}, Is Selected: {skin.isSelected}");
        }



    }




}