using Assets.Scripts.Movement.Controller;
using Assets.Scripts.Movement.Data;
using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private HorizontalMovementData _horizontalMovementData;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private DirectionalCameraPair _cameras;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private bool _isGrounded;
    private AnimationType _currentAnimationType;

    private HorizontalMover _horizontalMover;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _horizontalMover = new HorizontalMover(_rigidbody, _horizontalMovementData);

        _isGrounded = _collider.IsTouchingLayers(_groundLayer);
    }

    private void Update()
    {
        _isGrounded = _collider.IsTouchingLayers(_groundLayer);

        UpdateAnimations();
        UpdateCameras();
    }

    private void UpdateCameras()
    {
        foreach (var camera in _cameras.DirectionCameras)
        {
            camera.Value.enabled = camera.Key == _horizontalMover.Direction;
        }
    }

    public void MoveHorizontally(float direction) => _horizontalMover.MoveHorizontally(direction);

    public void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }

        _isGrounded = true;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
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
        PlayAnimation(AnimationType.Run, _horizontalMover.IsMoving);
        PlayAnimation(AnimationType.Jump, !_isGrounded);
    }
}