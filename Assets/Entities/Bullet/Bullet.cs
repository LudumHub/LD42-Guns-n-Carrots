using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float Damage = 1;
    public float Speed = 3f;
    public float Lifetime = 5f;

    Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public bool isRifle = false;

    private IEnumerator Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.up * Speed;

        yield return new WaitForSeconds(Lifetime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bulletUnpassable")
            || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("Hit", this);
            rigidBody.velocity = Vector2.zero;
            spriteRenderer.enabled = false;
        }
        else if (collision.gameObject.CompareTag("Mirror"))
            collision.gameObject.SendMessage("PlayParticles", transform.position);
        else
            gameObject.SetActive(false);
    }
}