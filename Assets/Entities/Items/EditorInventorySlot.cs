using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EditorInventorySlot : MonoBehaviour
{
    public bool IsItem;
    public bool IsRoot;

    private void Update()
    {
        var image = GetComponent<Image>();
        if (IsRoot)
        {
            image.color = Color.red;
        }
        else if (IsItem)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
        }
    }
}