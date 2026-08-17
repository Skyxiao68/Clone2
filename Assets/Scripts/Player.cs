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
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 完全冻结旋转
    }

    // Update is called once per frame
    void Update()
    {
        if (float.IsNaN(transform.position.x) || float.IsNaN(transform.position.y))
        {
            Debug.LogError($"player position is unknown！position = {transform.position}");
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null && (float.IsNaN(rb.linearVelocity.x) || float.IsNaN(rb.linearVelocity.y)))
        {
            Debug.LogError($"player velocity is unknown！velocity = {rb.linearVelocity}");
        }

        if (GameManager.Instance == null || !GameManager.Instance.IsGameStarted)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsGameStarted)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // 设置水平速度
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && GameManager.Instance != null && GameManager.Instance.IsGameStarted)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocity);
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
