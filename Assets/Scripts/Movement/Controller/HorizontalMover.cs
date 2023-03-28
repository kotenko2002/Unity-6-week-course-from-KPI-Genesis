using Assets.Scripts.Movement.Data;
using UnityEngine;

namespace Assets.Scripts.Movement.Controller
{
    public class HorizontalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly HorizontalMovementData _horizontalMovementData;

        private Vector2 _movement;

        public Direction Direction { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;

        public HorizontalMover(Rigidbody2D rigidbody, HorizontalMovementData horizontalMovementData)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
            _horizontalMovementData = horizontalMovementData;
        }

        public void MoveHorizontally(float direction)
        {
            _movement.x = direction;
            SetFaceDirection(direction);

            _rigidbody.velocity = new Vector2(direction * _horizontalMovementData.HorizontalSpeed, _rigidbody.velocity.y);
        }

        private void SetFaceDirection(float direction)
        {
            if ((Direction == Direction.Right && direction < 0)
                || (Direction == Direction.Left && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            _transform.Rotate(0, 180, 0);
            Direction = Direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
}
