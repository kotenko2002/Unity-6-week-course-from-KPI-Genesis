using Assets.Scripts.Movement.Data;
using UnityEngine;

namespace Assets.Scripts.Movement.Controller
{
    public class Jumper
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly BoxCollider2D _collider;
        private readonly JumperData _jumperData;

        public bool IsGrounded => _collider.IsTouchingLayers(_jumperData.GroundLayer);

        public Jumper(Rigidbody2D rigidbody, BoxCollider2D collider, JumperData jumperData)
        {
            _rigidbody = rigidbody;
            _collider = collider;
            _jumperData = jumperData;
        }

        public void Jump()
        {
            if (!IsGrounded)
            {
                return;
            }

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumperData.JumpForce);
        }
    }
}
