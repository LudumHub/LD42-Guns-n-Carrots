using System.Collections;
using UnityEngine;

public class InventoryItemView : MonoBehaviour
{
    public Item Item { get; set; }

    public void StartDragging()
    {
        StartCoroutine(FollowMouse());
    }

    private IEnumerator FollowMouse()
    {
        while (Input.GetMouseButton(0))
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 10.0f;
            transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            yield return new WaitForEndOfFrame();
        }

        var inventory = FindObjectOfType<Inventory>();
        if (!inventory.AcceptDraggedItem(this))
        {
            StartCoroutine(MoveToDropPoint());
        }
    }

    public void DropAway()
    {
        StartCoroutine(MoveToDropPoint());
    }

    private IEnumerator MoveToDropPoint()
    {
        var dropPoint = FindObjectOfType<DropArea>().GetRandomPoint();
        dropPoint.z = transform.position.z;
        while (Vector3.Distance(dropPoint, transform.position) > 1)
        {
            const float speed = 1;
            transform.position = Vector3.MoveTowards(transform.position, dropPoint, speed);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(SwitchToWorldView());
    }

    private IEnumerator SwitchToWorldView()
    {
        while (transform.localScale.magnitude > Mathf.Epsilon)
        {
            const float shrinkingSpeed = 0.3f;
            var currentScale = transform.localScale.magnitude;
            var maxScale = Mathf.Clamp(currentScale - shrinkingSpeed, 0, currentScale);
            transform.localScale = Vector3.ClampMagnitude(transform.localScale, maxScale);
            yield return new WaitForEndOfFrame();
        }

        var worldView = FindObjectOfType<ItemFactory>().CreateWorldItem(Item);
        worldView.gameObject.AddComponent<MouseDropMotion>();
        worldView.transform.position = transform.position;
        Destroy(gameObject);
    }
}