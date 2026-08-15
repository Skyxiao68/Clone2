using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public enum CameraMode
    {
        FollowPlayer,
        AutoScroll,
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
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                return;
            }
        }

        Vector3 playerPos = player.position;
        if (
            float.IsNaN(playerPos.x)
            || float.IsNaN(playerPos.y)
            || float.IsNaN(playerPos.z)
            || float.IsInfinity(playerPos.x)
            || float.IsInfinity(playerPos.y)
            || float.IsInfinity(playerPos.z)
        )
        {
            Debug.LogError($"Player positon is null！player.position = {playerPos}");
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

        if (float.IsNaN(currentX))
            currentX = transform.position.x;
        if (float.IsNaN(velocityX))
            velocityX = 0f;

        float newX = Mathf.SmoothDamp(currentX, playerX, ref velocityX, smoothTime);

        if (float.IsNaN(newX) || float.IsInfinity(newX))
        {
            Debug.LogError("SmoothDamp X have no effect keeping originals ");
            newX = currentX;
            velocityX = 0f;
        }

        currentX = newX;

        if (followPlayerY)
        {
            float targetY = playerPos.y + yoffset;
            targetY = Mathf.Clamp(targetY, minY, maxY);

            if (float.IsNaN(currentY))
                currentY = transform.position.y;
            if (float.IsNaN(velocityY))
                velocityY = 0f;

            float newY = Mathf.SmoothDamp(currentY, targetY, ref velocityY, smoothTimeY);
            if (float.IsNaN(newY) || float.IsInfinity(newY))
            {
                Debug.LogError("SmoothDamp Y have no to effect keeping originals ");
                newY = currentY;
                velocityY = 0f;
            }
            currentY = newY;
        }

        Vector3 newPosition = new Vector3(currentX, currentY, zPosition);
        if (float.IsNaN(newPosition.x) || float.IsNaN(newPosition.y) || float.IsNaN(newPosition.z))
        {
            Debug.LogError("camera postion contains NaN，cancel update ");
            return;
        }

        transform.position = newPosition;
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 targetPosition = new Vector3(
                player.position.x + xOffset,
                player.position.y + yoffset,
                zPosition
            );
            Gizmos.DrawWireCube(targetPosition, new Vector3(1, 1, 0));
        }
    }
}
