using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Mouse Variables
    private const float MOUSE_SENSITIVITY = 500f;
    private const string MOUSE_X_AXIS_NAME = "Mouse X";
    private const string MOUSE_Y_AXIS_NAME = "Mouse Y";

    //Movement variables
    private const string MOVEMENT_X_AXIS_NAME = "Horizontal";
    private const string MOVEMENT_Z_AXIS_NAME = "Vertical";


    //public Transform PlayerTransform; //todo: assign this to private in the future

    private CharacterController _characterController;
    private float _xRotation = 0f;
    private float _movementSpeed = 12f;
    private float _gravity = -9.81f;
    private Vector3 _velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        //SetPlayerTransform();

        MouseLockState();
    }

    private void Update()
    {
        //PlayerLookAround();
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
        float x = Input.GetAxis(MOVEMENT_X_AXIS_NAME);
        float z = Input.GetAxis(MOVEMENT_Z_AXIS_NAME);

        Vector3 move = playerTransform.right * x + playerTransform.forward * z;
        _characterController.Move(move * _movementSpeed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
