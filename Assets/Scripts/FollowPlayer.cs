using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
   public Transform player; 
   public Vector3 offset = new Vector3(0, 2, 0);
   public bool smoothFollow = true;
   public float smoothSpeed = 0.125f;

   void LateUpdate()
   {
       if (player == null)
       {
           return; 
       }

       Vector3 desiredPosition = player.position + offset; 

       if (smoothFollow)
       {
           Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
           transform.position = smoothedPosition; 
       }
       else
       {
           transform.position = desiredPosition; 
       }
   } 
}
   
