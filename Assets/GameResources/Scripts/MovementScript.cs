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
        float x = transform.position.x + (horizontal * MoveSpeed);
        float z = transform.position.z + (vertical * MoveSpeed);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
