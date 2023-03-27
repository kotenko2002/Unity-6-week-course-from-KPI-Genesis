using Assets.Scripts.Core.Animation;
using Assets.Scripts.Movement.Controller;
using Assets.Scripts.Movement.Data;
using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private AnimatorController _animator;

    [SerializeField] private HorizontalMovementData _horizontalMovementData;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private DirectionalCameraPair _cameras;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private bool _isGrounded;

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
    
    private void UpdateAnimations()
    {
        _animator.PlayAnimation(AnimationType.Idle, true);
        _animator.PlayAnimation(AnimationType.Run, _horizontalMover.IsMoving);
        _animator.PlayAnimation(AnimationType.Jump, !_isGrounded);
    }

    private void UpdateCameras()
    {
        foreach (var camera in _cameras.DirectionCameras)
        {
            camera.Value.enabled = camera.Key == _horizontalMover.Direction;
        }
    }
}