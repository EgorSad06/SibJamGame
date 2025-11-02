using UnityEngine;

public class Movment : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Поворот объекта в направлении движения
        if (moveInput != 0)
        {
            FlipCharacter(moveInput);
        }
    }

    private void FlipCharacter(float direction)
    {
        // Если direction > 0 - двигаемся вправо, scale.x должен быть положительным
        // Если direction < 0 - двигаемся влево, scale.x должен быть отрицательным
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}