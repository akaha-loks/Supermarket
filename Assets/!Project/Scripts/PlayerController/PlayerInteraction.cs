using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Button _dropButton;
    [SerializeField] private float _dropForce = 5;
    [SerializeField] private LayerMask _itemLayer;

    private Item currentItem;

    private void Start()
    {
        _dropButton.gameObject.SetActive(false);
        _dropButton.onClick.AddListener(DropItem);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                TryPickupItem(touch.position);
            }
        }
    }

    private void TryPickupItem(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f, _itemLayer))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item && currentItem == null)
            {
                currentItem = item;
                item.PickUp(_hand);
                _dropButton.gameObject.SetActive(true);
            }
        }
    }

    private void DropItem()
    {
        if (!currentItem) return;

        currentItem.Drop(Camera.main.transform.forward * _dropForce);
        currentItem = null;
        _dropButton.gameObject.SetActive(false);
    }
}