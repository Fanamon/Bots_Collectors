using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScreenEdgeMotion : MonoBehaviour
{
    [SerializeField][Range(0, 0.1f)] private float _edgeTolerance = 0.05f;

    public bool TryGetMoveDirectionByMousePositionAtScreenEdge(Vector3 cameraRight, Vector3 cameraForward,
        out Vector3 moveDirection)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        moveDirection = Vector3.zero;

        if (mousePosition.x < _edgeTolerance * Screen.width)
        {
            moveDirection += -cameraRight;
        }
        else if (mousePosition.x > (1f - _edgeTolerance) * Screen.width)
        {
            moveDirection += cameraRight;
        }

        if (mousePosition.y < _edgeTolerance * Screen.height)
        {
            moveDirection += -cameraForward;
        }
        else if (mousePosition.y > (1f - _edgeTolerance) * Screen.height)
        {
            moveDirection += cameraForward;
        }

        return moveDirection.Equals(Vector3.zero) == false;
    }
}
