using System.Collections;
using UnityEngine;

public class IncreaseScore : MonoBehaviour
{

    public float f = 1f;
    private float failXposition;
    private bool collected = false;

    public GameObject flower; 

    private GameObject player; 

    
    
    private void Start()
    {
        failXposition = transform.position.x + f;  


    }

    
    
    
    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); 
        }


        Transform playerPos = player.transform;

        if (collected)
        {
            return; 
        }

        if (playerPos.position.x > failXposition)
        {
            OnMissed();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collected)
        {
            return; 
        }
        
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collected = true;

            if (Score.Instance != null)
            {
                Score.Instance.AddScore(); 
            }
            else
            {
                Debug.LogWarning("Score instance is not found. Make sure the Score script is attached to a GameObject in the scene.");
            }

            Destroy(gameObject);

            Destroy(flower, 0.5f);
             
        }
    }

   

    private void OnMissed()
    {
        if (collected)
        {
            return; 
        }


        collected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver(); 
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 gizmoPos = new Vector3(failXposition, transform.position.y, transform.position.z);
        Gizmos.DrawLine(gizmoPos + Vector3.up * 5f, gizmoPos + Vector3.down * 5f);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 gizmoPos2 = new Vector3(f, transform.position.y, transform.position.z);
        Gizmos.DrawLine(gizmoPos2 + Vector3.up * 5f, gizmoPos2 + Vector3.down * 5f);
    }
}
