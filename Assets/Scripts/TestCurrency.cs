using UnityEngine;

public class TestCurrency : MonoBehaviour
{
    void Start()
    {
        
        CurrencyManager.Instance.AddCurrency(100);

        
        bool success = CurrencyManager.Instance.SpendCurrency(50);
        Debug.Log("spend 50 success? " + success);

        bool fail = CurrencyManager.Instance.SpendCurrency(1000);
        Debug.Log("Spend 1000 success?" + fail);
    }
}