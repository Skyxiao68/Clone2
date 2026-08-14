using UnityEngine;
using System.Collections.Generic;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }

    // PlayerPrefs 键名
    private const string SELECTED_SKIN_KEY = "SelectedSkinID";
    private const string UNLOCKED_SKINS_KEY = "UnlockedSkins";

   
    public Skin SelectedSkin { get; private set; }

    void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
            
            LoadSkinData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

 
    private void LoadSkinData()
    {
    
        UnlockSkin("default", save: false);

    
        string unlockedIDs = PlayerPrefs.GetString(UNLOCKED_SKINS_KEY, "default");
        string[] ids = unlockedIDs.Split(',');
        foreach (string id in ids)
        {
            Skin skin = SkinDatabase.GetSkinByID(id);
            if (skin != null)
            {
                skin.isUnlocked = true;
            }
        }

    
        string selectedID = PlayerPrefs.GetString(SELECTED_SKIN_KEY, "default");
        SelectSkin(selectedID, save: false);
    }

    
    private void UnlockSkin(string skinID, bool save = true)
    {
        Skin skin = SkinDatabase.GetSkinByID(skinID);
        if (skin != null && !skin.isUnlocked)
        {
            skin.isUnlocked = true;
            if (save) SaveUnlockedSkins();
        }
    }

    // 尝试购买皮肤
    public bool TryBuySkin(string skinID)
    {
        Skin skin = SkinDatabase.GetSkinByID(skinID);
        if (skin == null || skin.isUnlocked)
        {
            Debug.Log("Skin already unlocked or not found: ");
            return false;
        }

        // 尝试花费货币
        if (CurrencyManager.Instance.SpendCurrency(skin.price))
        {
            UnlockSkin(skinID);
            Debug.Log($"Succsessfully buy skin：{skin.displayName}");
            return true;
        }
        else
        {
            Debug.Log("inufficient currency");
            return false;
        }
    }

    
    public void SelectSkin(string skinID, bool save = true)
    {
        Skin skin = SkinDatabase.GetSkinByID(skinID);
        if (skin == null || !skin.isUnlocked)
        {
            Debug.LogWarning($"Skin not unlocked, cannot select：{skinID}");
            return;
        }

        foreach (var s in SkinDatabase.AllSkins)
        {
            s.isSelected = false;
        }

        // 设置当前皮肤为选中
        skin.isSelected = true;
        SelectedSkin = skin;

        if (save)
        {
            PlayerPrefs.SetString(SELECTED_SKIN_KEY, skinID);
            PlayerPrefs.Save();
        }
    }

    
    private void SaveUnlockedSkins()
    {
        List<string> unlockedIDs = new List<string>();
        foreach (var skin in SkinDatabase.AllSkins)
        {
            if (skin.isUnlocked) unlockedIDs.Add(skin.id);
        }
        string joined = string.Join(",", unlockedIDs);
        PlayerPrefs.SetString(UNLOCKED_SKINS_KEY, joined);
        PlayerPrefs.Save();
    }
}