using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("HorizontalMovement")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private Direction _direction;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private DirectionalCameraPair _cameras;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private bool _isGrounded;
    private Vector2 _movement;
    private AnimationType _currentAnimationType;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _isGrounded = _collider.IsTouchingLayers(_groundLayer);
    }

    private void Update()
    {
        _isGrounded = _collider.IsTouchingLayers(_groundLayer);

        UpdateAnimations();
    }

    public void MoveHorizontally(float direction)
    {
        _movement.x = direction;
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
        if ((_direction == Direction.Right && direction < 0)
            || (_direction == Direction.Left && direction > 0))
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;

        foreach (var camera in _cameras.DirectionCameras)
        {
            camera.Value.enabled = camera.Key == _direction;
        }
    }

    private void PlayAnimation(AnimationType animationType, bool active)
    {
        if (!active)
        {
            if(_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
            {
                return;
            }

            _currentAnimationType = AnimationType.Idle;
            PlayAnimation(_currentAnimationType);
            return;
        }

        if(_currentAnimationType >= animationType)
        {
            return;
        }

        _currentAnimationType = animationType;
        PlayAnimation(_currentAnimationType);
    }

    private void PlayAnimation(AnimationType animationType)
    {
        _animator.SetInteger(nameof(AnimationType), (int)animationType);
    }

    private void UpdateAnimations()
    {
        PlayAnimation(AnimationType.Idle, true);
        PlayAnimation(AnimationType.Run, _movement.magnitude > 0);
        PlayAnimation(AnimationType.Jump, !_isGrounded);
    }
}