using UnityEngine;

public class Movment : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    //[SerializeField] private float jumpForce = 12f;

    //[Header("Ground Check")]
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float checkRadius = 0.2f;
    //[SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //}
    }
    //не обязательно
    //void OnDrawGizmosSelected()
    //{
    //    if (groundCheck != null)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    //    }
    //}
}
