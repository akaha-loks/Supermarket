using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private bool _isMoving = false;
    private Transform _hand;

    private void Update()
    {
        if (_isMoving)
        {
            MoveToHand();
        }
    }

    public void PickUp(Transform hand)
    {
        _hand = hand;

        //Save current target position and rotation for a smooth transition
        _targetPosition = hand.position;
        _targetRotation = hand.rotation;

        // Stop physics and make an object with a subsidiary of a hand
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
        transform.SetParent(hand);
        _isMoving = true;
    }

    private void MoveToHand()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            _isMoving = false;
            transform.position = _targetPosition;
            transform.rotation = _targetRotation;
        }
    }

    public void Drop(Vector3 force)
    {
        transform.SetParent(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
