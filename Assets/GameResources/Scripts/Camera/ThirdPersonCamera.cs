using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform Target;
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

        private void LateUpdate()
        {
            rotationDeltaHorizontal *= Sensivity;
            rotationDeltaVertical = ClampVerticalAngle(rotationDeltaVertical * Sensivity, -MaxVerticalAngle, MaxVerticalAngle);

            rotationHorizontal = Mathf.Repeat(rotationHorizontal + rotationDeltaHorizontal, 360f);
            rotationVertical += rotationDeltaVertical;

            Target.Rotate(Vector3.up, rotationDeltaHorizontal);
            transform.RotateAround(Target.position, transform.right, rotationDeltaVertical);
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

        private float ClampVerticalAngle(float angle, float minVerticalAngle, float maxVerticalAngle)
        {
            var upBound = maxVerticalAngle - rotationVertical;
            var downBound = minVerticalAngle - rotationVertical;

            return Mathf.Clamp(angle, downBound, upBound);
        }
    }
}
