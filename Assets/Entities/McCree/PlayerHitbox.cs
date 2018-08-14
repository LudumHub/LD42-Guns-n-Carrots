using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private bool wasHitRecently;

    public void Hit(Bullet bullet)
    {
        if (!wasHitRecently)
        {
            wasHitRecently = true;
            FindObjectOfType<Inventory>().DropCarrot();
            StartCoroutine(FlashRed());
        }
    }

    private IEnumerator FlashRed()
    {
        SetSpritesColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        SetSpritesColor(Color.white);
        yield return new WaitForSeconds(0.2f);
        SetSpritesColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        SetSpritesColor(Color.white);
        yield return new WaitForSeconds(0.2f);
        SetSpritesColor(Color.red);
        yield return new WaitForSeconds(0.2f);
        SetSpritesColor(Color.white);
        wasHitRecently = false;
    }

    private void SetSpritesColor(Color color)
    {
        foreach (var sprite in GetSprites())
        {
            sprite.color = color;
        }
    }

    private IEnumerable<SpriteRenderer> GetSprites()
    {
        yield return GetComponent<SpriteRenderer>();
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            yield return spriteRenderer;
        }
    }
}