using UnityEngine;

public class PedalColliderMarker : MonoBehaviour
{
    public static bool HasTouchedPedal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HasTouchedPedal= true;
            Debug.Log("player has touched pedal");
        }
    }
}
