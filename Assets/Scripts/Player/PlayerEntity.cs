using Assets.Scripts.Core.Animation;
using Assets.Scripts.Movement.Controller;
using Assets.Scripts.Movement.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private AnimatorController _animator;
    [SerializeField] private HorizontalMovementData _horizontalMovementData;
    [SerializeField] private JumperData _jumperData;
    [SerializeField] private DirectionalCameraPair _cameras;

    private Rigidbody2D _rigidbody;
    private HorizontalMover _horizontalMover;
    private Jumper _jumper;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _horizontalMover = new HorizontalMover(_rigidbody, _horizontalMovementData);
        _jumper = new Jumper(_rigidbody, GetComponent<BoxCollider2D>(), _jumperData);
    }

    private void Update()
    {
        UpdateAnimations();
        UpdateCameras();
    }

    public void MoveHorizontally(float direction) => _horizontalMover.MoveHorizontally(direction);

    public void Jump() => _jumper.Jump();
    
    private void UpdateAnimations()
    {
        _animator.PlayAnimation(AnimationType.Idle, true);
        _animator.PlayAnimation(AnimationType.Run, _horizontalMover.IsMoving);
        _animator.PlayAnimation(AnimationType.Jump, !_jumper.IsGrounded);
    }

    private void UpdateCameras()
    {
        foreach (var camera in _cameras.DirectionCameras)
        {
            camera.Value.enabled = camera.Key == _horizontalMover.Direction;
        }
    }
}