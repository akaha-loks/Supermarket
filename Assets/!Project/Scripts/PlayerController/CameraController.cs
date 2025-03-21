using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.1f, 5)]
    [SerializeField] private float _sensitivity = 2;
    [Range(0.1f, 5)]
    [SerializeField] private float _touchSensitivity = 1;

    [Range(15, 80)]
    [SerializeField] private float _maxYAngle;

    [Header("Screen Division Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _cameraAreaWidth = 0.5f;

    private float _rotationX = 0;
    private float _touchX = 0;
    private float _touchY = 0;

    private void Update()
    {
        CameraRotate();
    }

    private void CameraRotate()
    {
        CheckTouch();

        transform.parent.Rotate(Vector3.up * _touchX * _sensitivity); // Rotate character horizontally

        _rotationX -= _touchY * _sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -_maxYAngle, _maxYAngle); // Rotate vertically
        transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x > Screen.width * _cameraAreaWidth)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    _touchX = touch.deltaPosition.x * _touchSensitivity;
                    _touchY = touch.deltaPosition.y * _touchSensitivity;
                }
                else if (touch.phase == TouchPhase.Stationary)
                    ResetTouches();
            }
            else
                ResetTouches();
        }
        else
            ResetTouches();
    }

    private void ResetTouches()
    {
        _touchX = 0;
        _touchY = 0;
    }
}
