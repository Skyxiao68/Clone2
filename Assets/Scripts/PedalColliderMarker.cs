using UnityEngine;

public class PedalColliderMarker : MonoBehaviour
{
    public static bool HasTouchedPedal = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HasTouchedPedal = true;
            IncreaseScore.ResetBonus();
            Debug.Log("Player touched pedal, bonus reset.");
        }
    }
}