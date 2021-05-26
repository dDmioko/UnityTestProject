using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 0.2f;

    private float horizontal;
    private float vertical;

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float coef = 0;
        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            coef = 1 / Mathf.Sqrt(vertical * vertical + horizontal * horizontal);
        }

        float x = MoveSpeed * coef * horizontal;
        float z = MoveSpeed * coef * vertical;

        transform.position += z * transform.forward;
        transform.position += x * transform.right;
    }
}
