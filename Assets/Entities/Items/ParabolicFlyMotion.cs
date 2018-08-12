using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParabolicFlyMotion: PhysicsMotion
{
    private IEnumerator Start()
    {
        Rigidbody.AddForce(new Vector2(25, 40));
        var seconds = Random.value + 0.5f;
        yield return new WaitForSeconds(seconds);
        Rigidbody.velocity = Vector2.zero;
        gameObject.AddComponent<RoadSlideMotion>();
        Destroy(this);
    }
}