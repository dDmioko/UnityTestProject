using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] private float Sensivity = 0.1f;
        [SerializeField] private float MaxVerticalAngle = 85f;

        private float rotationVertical;
        private float rotationHorizontal;

        private float rotationDeltaVertical;
        private float rotationDeltaHorizontal;

        private PlayerActions inputActions;

        private void OnEnable()
        {
            inputActions = new PlayerActions();

            inputActions.Walking.Enable();
            inputActions.Walking.CameraMovement.performed += OnMovement;
            inputActions.Walking.CameraMovement.canceled += OnEndMovement;
        }

        private void OnDisable()
        {
            inputActions.Walking.Disable();
            inputActions.Walking.CameraMovement.performed -= OnMovement;
            inputActions.Walking.CameraMovement.canceled -= OnEndMovement;
        }

        void Update()
        {
            rotationHorizontal = Mathf.Repeat(rotationHorizontal + rotationDeltaHorizontal * Sensivity, 360f);
            rotationVertical = Mathf.Clamp(rotationVertical + rotationDeltaVertical * Sensivity, -MaxVerticalAngle, MaxVerticalAngle);
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(rotationVertical, rotationHorizontal, 0);
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            rotationDeltaHorizontal = input.x;
            rotationDeltaVertical = input.y;
        }

        private void OnEndMovement(InputAction.CallbackContext context)
        {
            rotationDeltaHorizontal = 0;
            rotationDeltaVertical = 0;
        }
    }
}
