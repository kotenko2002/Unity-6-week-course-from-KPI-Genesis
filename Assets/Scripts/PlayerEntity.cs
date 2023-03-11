using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [Header("HorizontalMovement")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private bool _faceRight;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _isGrounded = _collider.IsTouchingLayers(groundLayer);
    }

    private void Update()
    {
        _isGrounded = _collider.IsTouchingLayers(groundLayer);
    }

    public void MoveHorizontally(float direction)
    {
        SetFaceDirection(direction);

        _rigidbody.velocity = new Vector2(direction * _horizontalSpeed, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }

        _isGrounded = true;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void SetFaceDirection(float direction)
    {
        if ((_faceRight && direction < 0) || (!_faceRight && direction > 0))
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        _faceRight = !_faceRight;
    }
}