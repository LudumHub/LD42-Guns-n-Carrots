using UnityEngine;

public class PhysicsMotion : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}