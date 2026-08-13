using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  public Transform player; 

  public float xOffset = 0f; 

  public bool lockY = true; 
  public float fixedY = 0f;

  public bool useSmoothFollow = false;
  public float smoothSpeed = 10f; 

  private float initialY; 

  void Start()
  {
    initialY = transform.position.y;
    if (!lockY)
        {
            initialY = fixedY;
        }
  }

  void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned in FollowPlayer script.");
            return;
        }
    

        float playerX = player.position.x + xOffset;

        Vector3 newPos = transform.position;


        newPos.x = playerX;
        newPos.y = initialY; 

        if (useSmoothFollow)
        {

            newPos.x = Mathf.Lerp(transform.position.x, playerX, smoothSpeed * Time.deltaTime);
        }

        transform.position = newPos; 
   }

   void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 gizmoPos = new Vector3(player.position.x + xOffset, transform.position.y, transform.position.z);
        Gizmos.DrawWireSphere(gizmoPos, 0.5f);
    }
}
    