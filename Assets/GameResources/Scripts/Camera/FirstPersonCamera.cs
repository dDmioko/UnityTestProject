using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] private float Sensivity = 5f;
        [SerializeField] private float MaxVerticalAngle = 85f;

        private float rotationVertical;
        private float rotationHorizontal;

        void Update()
        {
            //rotationHorizontal += Input.GetAxis("Mouse X") * Sensivity;
            //rotationVertical -= Input.GetAxis("Mouse Y") * Sensivity;

            rotationHorizontal = Mathf.Repeat(rotationHorizontal, 360f);
            rotationVertical = Mathf.Clamp(rotationVertical, -MaxVerticalAngle, MaxVerticalAngle);
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(rotationVertical, rotationHorizontal, 0);
        }
    }
}
