using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SkinShopUI : MonoBehaviour
{
    public TMP_Text currencyText;
    public Transform buttonContainer;
    public GameObject skinButtonPrefab;

    public PlayerVisual playerVisual;

    private void OnEnable()
    {
        RefreshShop();
    }

    public void RefreshShop()
    {
        if (buttonContainer == null)
        {
            Debug.LogError("Button Container not set!", this);
            return;
        }
        if (skinButtonPrefab == null)
        {
            Debug.LogError("Skin Button Prefab not set!", this);
            return; 
        }
        

        if (currencyText != null && CurrencyManager.Instance != null)
        {
            currencyText.text = "Currency: " + CurrencyManager.Instance.TotalCurrency.ToString();
        }

        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        if (SkinDatabase.AllSkins != null && skinButtonPrefab != null)
        {
            foreach (var skin in SkinDatabase.AllSkins)
            {
                GameObject buttonGO = Instantiate(skinButtonPrefab, buttonContainer);
                SkinButton button = buttonGO.GetComponent<SkinButton>();
                if (button != null)
                {
                    button.Setup(skin, this);
                }
            }
        }
    }

    public void OnBuyButtonClicked(string skinId)
    {
        
        bool success = SkinManager.Instance.TryBuySkin(skinId);

        if (success)
        {
            RefreshShop();
        }
        else
        {
            Debug.Log("Failed to buy skin, Not enough currency");
        }
    }

    public void OnSelectButtonClicked(string skinId)
    {
        SkinManager.Instance.SelectSkin(skinId);
        RefreshShop();

        if (playerVisual != null)
        {
            playerVisual.ApplySelectedSkin();
        }
    }
}
