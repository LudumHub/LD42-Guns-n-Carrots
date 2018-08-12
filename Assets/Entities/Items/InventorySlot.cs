using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler
{
    [SerializeField] private Image myImage;

    public event Action PointerEnter;
    public event Action PointerExit;
    public event Action PointerDown;

    public Vector2Int Position { get; set; }
    public Item Item { get; set; }
    public InventoryItemView ItemView { get; set; }

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PointerEnter != null) PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PointerExit != null) PointerExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown != null) PointerDown();
    }

    public void HighlightInvalid()
    {
        myImage.color = Color.red;
    }

    public void ResetHighlight()
    {
        myImage.color = Color.white;
    }

    public void HighlightHover()
    {
        myImage.color = Color.yellow;
    }
}