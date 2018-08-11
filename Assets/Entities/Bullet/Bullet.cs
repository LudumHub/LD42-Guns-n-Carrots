using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int Damage = 1;
    public float Speed = 3f;
    public float Lifetime = 5f;

    Rigidbody2D rigidBody;

    private IEnumerator Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.up * Speed;

        yield return new WaitForSeconds(Lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "bulletUnpassable") return;

        collision.SendMessage("Hit", Damage);
        rigidBody.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
