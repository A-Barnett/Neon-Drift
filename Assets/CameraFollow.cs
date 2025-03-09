using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public float minX, maxY, maxX,minY;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public Transform target; // The player object to follow
    void LateUpdate()
    {
        // Position the camera behind the player
        Vector3 desiredPosition = target.position + offset;
        // Clamp the camera position so it does not go out of bounds
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        desiredPosition.z = -10;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
