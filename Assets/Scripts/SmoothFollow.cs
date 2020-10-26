using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public Transform target;

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target) return;

        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var position = target.position;
        var wantedHeight = position.y + height;

        var cTransform = transform;
        var currentRotationAngle = cTransform.eulerAngles.y;
        var currentHeight = cTransform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;
    }
}