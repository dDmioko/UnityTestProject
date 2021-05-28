using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 0.2f;

    private float horizontal;
    private float vertical;

    private PlayerActions inputActions;

    private void OnEnable()
    {
        inputActions = new PlayerActions();

        inputActions.Enable();
        inputActions.Walking.Movement.performed += OnMovement;
        inputActions.Walking.Movement.canceled += OnEndMovement;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Walking.Movement.performed -= OnMovement;
        inputActions.Walking.Movement.canceled -= OnEndMovement;
    }

    private void FixedUpdate()
    {
        float x = MoveSpeed * horizontal;
        float z = MoveSpeed * vertical;

        transform.position += z * transform.forward;
        transform.position += x * transform.right;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        horizontal = input.x;
        vertical = input.y;
    }

    private void OnEndMovement(InputAction.CallbackContext context)
    {
        horizontal = 0;
        vertical = 0;
    }
}
