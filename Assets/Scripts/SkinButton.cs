using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text priceText;
    public Button buyButton;
    public Button selectButton;
    public Image skinImage;


    private string skinID;
    private SkinShopUI shopUI;

    public void Setup(Skin skin, SkinShopUI ui)
    {
        skinID = skin.id;
        shopUI = ui;

        if(nameText == null || priceText == null || buyButton == null || selectButton == null)
        {
            Debug.LogError($"skinButton not fully assgined please check {gameObject.name} for all components");
            return; 
        }

        nameText.text = skin.displayName;

        if (skin.animationFrames != null && skin.animationFrames.Length > 0)
        {
            skinImage.sprite = skin.animationFrames[0];
            skinImage.gameObject.SetActive(true);
        }
        else
        {
            skinImage.enabled = false; 
            Debug.LogWarning($"skin {skinID} has no animation frames, please check the skin data");
        }

        if (skin.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);

            selectButton.interactable = !skin.isSelected;
            priceText.text = "";
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);

            priceText.text = skin.price.ToString();

            buyButton.interactable = (CurrencyManager.Instance != null && CurrencyManager.Instance.TotalCurrency >= skin.price);
        }
    }

    public void OnBuyClicked()
    {
        shopUI.OnBuyButtonClicked(skinID);
    }

    public void OnSelectClicked()
    {
        shopUI.OnSelectButtonClicked(skinID);
    }
}
