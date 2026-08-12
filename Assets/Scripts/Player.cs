using UnityEngine;
using UnityEngine.InputSystem; 

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; 
    public float velocity = 1.5f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        print(ctx); 
        if (ctx.performed)
        {
            rb.linearVelocity = Vector2.up * velocity; ;

        }
    }
}
