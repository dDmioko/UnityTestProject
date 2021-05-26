using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform Target;
        [SerializeField]
        private float Sensivity = 5f;
        [SerializeField]
        private float MaxVerticalAngle = 85f;

        [SerializeField]
        private float rotationVertical;
        private float rotationHorizontal;

        [SerializeField]
        private float rotationDeltaVertical;
        private float rotationDeltaHorizontal;

        void Update()
        {
            rotationDeltaHorizontal = Input.GetAxis("Mouse X") * Sensivity;
            rotationDeltaVertical = Input.GetAxis("Mouse Y") * -Sensivity;
        }

        private void LateUpdate()
        {
            rotationDeltaVertical = ClampVerticalAngle(rotationDeltaVertical, -MaxVerticalAngle, MaxVerticalAngle);

            rotationHorizontal = Mathf.Repeat(rotationHorizontal + rotationDeltaHorizontal, 360f);
            rotationVertical += rotationDeltaVertical;

            Target.Rotate(Vector3.up, rotationDeltaHorizontal);
            transform.RotateAround(Target.position, transform.right, rotationDeltaVertical);
        }

        private float ClampVerticalAngle(float angle, float minVerticalAngle, float maxVerticalAngle)
        {
            var upBound = maxVerticalAngle - rotationVertical;
            var downBound = minVerticalAngle - rotationVertical;

            return Mathf.Clamp(angle, downBound, upBound);
        }
    }
}
