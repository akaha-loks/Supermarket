using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5;
    private CharacterController _controller;

    [Header("Joystick")]
    [SerializeField] private Joystick _joystick;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = 0;
        float vertical = 0;

        horizontal = _joystick.Horizontal;
        vertical = _joystick.Vertical;

        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        _controller.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }
}
