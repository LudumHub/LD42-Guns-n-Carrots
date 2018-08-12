using System.Collections;
using UnityEngine;

public class MouseDropMotion : PhysicsMotion
{
    private IEnumerator Start()
    {
        Rigidbody.AddForce(new Vector2(0, 10));
        yield return new WaitForSeconds(0.5f);
        Rigidbody.velocity = Vector2.zero;
        gameObject.AddComponent<RoadSlideMotion>();
        Destroy(this);
    }
}