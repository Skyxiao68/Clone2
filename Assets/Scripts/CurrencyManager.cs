using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance {get; private set;}

    private const string CURRENCY_KEY = "TotalCurrency";

    public int TotalCurrency {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            LoadCurrency(); 
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCurrency()
    {
        TotalCurrency = PlayerPrefs.GetInt(CURRENCY_KEY, 0);
    }


    public void AddCurrency(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        TotalCurrency += amount;
        PlayerPrefs.SetInt(CURRENCY_KEY, TotalCurrency);
        PlayerPrefs.Save(); 
        Debug.Log($"Currency added: {amount}, Total currency: {TotalCurrency}");

    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (TotalCurrency >= amount)
        {
            TotalCurrency -= amount;
            PlayerPrefs.SetInt(CURRENCY_KEY, TotalCurrency);
            PlayerPrefs.Save(); 
            Debug.Log($"Currency spent: {amount}, Total currency: {TotalCurrency}");
            return true;
        }
        else
        {
            Debug.Log("not enough currency to spend");
            return false; 
        }
    }

    public void ResetCurrency()
    {
        TotalCurrency = 0;
        PlayerPrefs.SetInt(CURRENCY_KEY, 0);
        PlayerPrefs.Save(); 
        Debug.Log("Currency reset to 0"); 
    }

}
