using UnityEngine;

public class DoorMover : MonoBehaviour
{
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _posToOpen;
    [SerializeField] private Transform _posToClose;

    [SerializeField] private float _speed = 2f;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = _posToClose.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetPosition = _posToOpen.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetPosition = _posToClose.position;
        }
    }

    private void Update()
    {
        _door.position = Vector3.Lerp(_door.position, targetPosition, _speed * Time.deltaTime);
    }
}
