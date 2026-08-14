using UnityEngine;
using UnityEngine.InputSystem; 

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; 
    public float velocity = 1.5f; 
    public float speed = 0.5f; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    
        transform.position += Vector3.right * speed * Time.deltaTime; 
    }

    

    public void Jump(InputAction.CallbackContext ctx)
    {
         
        if (ctx.performed)
        {
            rb.linearVelocity = Vector2.up * velocity; ;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.GameOver(); 
        }
    }
}
