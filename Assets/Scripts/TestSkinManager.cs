using UnityEngine;

public class TestSkinManager : MonoBehaviour
{
    void Start()
    {
       
        CurrencyManager.Instance.AddCurrency(50);

      
        bool buy1 = SkinManager.Instance.TryBuySkin("blue");
        Debug.Log("Blue Skin purchase successful？ " + buy1);

       
        SkinManager.Instance.SelectSkin("blue");
        Debug.Log("current skin：" + SkinManager.Instance.SelectedSkin.displayName);

        
        bool buy2 = SkinManager.Instance.TryBuySkin("gold");
        Debug.Log("Gold Skin purchase successful？ " + buy2);

        bool buy3 = SkinManager.Instance.TryBuySkin("purple");
        Debug.Log("Purple Skin purchase successful？ " + buy3);
    }
}