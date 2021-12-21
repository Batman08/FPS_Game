using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*Private Constant Variables*/
    //Mouse Variables
    private const float MOUSE_SENSITIVITY = 500f;
    private const string MOUSE_X_AXIS_NAME = "Mouse X";
    private const string MOUSE_Y_AXIS_NAME = "Mouse Y";

    //Movement variables
    private const string MOVEMENT_X_AXIS_NAME = "Horizontal";
    private const string MOVEMENT_Z_AXIS_NAME = "Vertical";


    /*Public Variables*/
    public LayerMask GroundLayerMask;


    /*Private Variables*/
    private CharacterController _characterController;
    private Vector3 _velocity;
    private Transform _groundCheck;

    private float _xRotation = 0f;
    private float _movementSpeed = 12f;
    private float _gravity = -75;/*-19.62f,-9.81f*/
    private float _groundDistance = 0.4f;
    private float _jumpHeight = 3f;

    private bool _isGrounded;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _groundCheck = GetComponentInChildren<Transform>().GetChild(2);
        MouseLockState();
    }

    private CursorLockMode MouseLockState()
    {
        return Cursor.lockState = CursorLockMode.Locked;
    }

    public void LookAround(Transform cameraTranform)
    {
        float mouseX = Input.GetAxis(MOUSE_X_AXIS_NAME) * MOUSE_SENSITIVITY * Time.deltaTime;
        float mouseY = Input.GetAxis(MOUSE_Y_AXIS_NAME) * MOUSE_SENSITIVITY * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        cameraTranform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void Movement(Transform playerTransform)
    {
        CheckIfGrounded();

        float x = Input.GetAxis(MOVEMENT_X_AXIS_NAME);
        float z = Input.GetAxis(MOVEMENT_Z_AXIS_NAME);

        Vector3 move = playerTransform.right * x + playerTransform.forward * z;
        _characterController.Move(move * _movementSpeed * Time.deltaTime);

        JumpMechanic();

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void JumpMechanic()
    {
        bool hasJumpButtonBeenPressed = Input.GetKeyDown(KeyCode.Space) && _isGrounded;
        if (hasJumpButtonBeenPressed)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
    private void CheckIfGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, GroundLayerMask);

        bool keepPlayerAboveGround = _isGrounded && _velocity.y < 0;
        if (keepPlayerAboveGround)
        {
            _velocity.y = -2f;
        }
    }
}
