using UnityEngine;
using UnityEngine.InputSystem; 

public class Player : MonoBehaviour
{
    public Rigidbody rb; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        print(ctx); 
        if (ctx.started)
        {
            

        }
    }
}
