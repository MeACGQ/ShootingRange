using System;
using UnityEngine;

public class PlayerFPSCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity;

    PlayerInputAction action;

    [SerializeField] private GameObject CameraObject;

    Vector2 directon;
    float xRotation;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        action = new PlayerInputAction();
    }

    private void OnEnable()
    {
        action.Enable();

        action.PlayerMoves.Look.performed += ctx => directon = ctx.ReadValue<Vector2>();
        action.PlayerMoves.Look.canceled += ctx => directon = new Vector2(0, 0);
    }

    private void Update()
    {
        transform.Rotate(new Vector2(0f, directon.x) * sensitivity * Time.deltaTime);

        xRotation -= directon.y * sensitivity * Time.deltaTime;
        xRotation = Math.Clamp(xRotation, -80, 80);
        CameraObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
