using System.Collections;
using UnityEngine;

public class IncreaseScore : MonoBehaviour
{

    public float f = 1f;
    public int normalScore = 1;
    public int bonusScore = 2; 
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

            SoundManager.Instance.PlayHoopSuccess();

            int scoreToAdd = normalScore;

            if (PedalColliderMarker.HasTouchedPedal)
            {
                scoreToAdd = normalScore;

                PedalColliderMarker.HasTouchedPedal = false; 
                Debug.Log("Player touched pedal add only 1 point"); 

            }
            else
            {
                scoreToAdd = bonusScore; 
                Debug.Log("Player did not touch pedal add bonus points"); 
            }

            if (Score.Instance != null)
            {
                Score.Instance.AddScore(scoreToAdd); 
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
