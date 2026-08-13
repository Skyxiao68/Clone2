using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform player; 

    public enum CameraMode
    {
        FollowPlayer,
        AutoScroll
    }

    public CameraMode mode = CameraMode.FollowPlayer;

    public float xOffset = 5f; 
    public float autoScrollSpeed = 3f; 
    public float smoothTime = 0.2f; 

    public bool followPlayerY = true;
    public float yoffset = 2f;
    public float smoothTimeY = 0.3f; 
    public float minY = -5f; 
    public float maxY = 5f; 

    public float zPosition = -10f; 

    private float velocityX = 0f;
    private float velocityY = 0f;
    private float currentX; 
    private float currentY; 


    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned in CameraFollow script.");
            return;
        }

        currentX = transform.position.x;
        currentY = transform.position.y;

    }

    void LateUpdate()
    {
        if (player == null)
        {
            return; 
        }

        float playerX; 

        if (mode == CameraMode.AutoScroll)
        {
            playerX = currentX + autoScrollSpeed * Time.deltaTime;

        } 
        else
        {
            playerX = player.position.x + xOffset;
            if (playerX < currentX)
            {
                playerX = currentX; 
            }
        }

        currentX = Mathf.SmoothDamp(currentX, playerX, ref velocityX, smoothTime);

        if (followPlayerY)
        {
            float targetY = player.position.y + yoffset;
            targetY = Mathf.Clamp(targetY, minY, maxY);
            currentY = Mathf.SmoothDamp(currentY, targetY, ref velocityY, smoothTimeY);
        }

        transform.position = new Vector3(currentX, currentY, zPosition); 

       
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 targetPosition = new Vector3(player.position.x + xOffset, player.position.y + yoffset, zPosition);
            Gizmos.DrawWireCube(targetPosition, new Vector3(1,1,0));
        }
    }
}
