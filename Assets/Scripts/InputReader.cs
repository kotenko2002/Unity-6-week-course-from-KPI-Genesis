using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;

    private float _direction;

    void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow))
        {
            _playerEntity.Jump();
        }
    }

    private void FixedUpdate()
    {
        _playerEntity.MoveHorizontally(_direction);
    }
}