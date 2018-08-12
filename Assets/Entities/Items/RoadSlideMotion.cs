using System.Collections;
using UnityEngine;

public class RoadSlideMotion : PhysicsMotion
{
    private Train train;

    protected override void Awake()
    {
        base.Awake();
        train = FindObjectOfType<Train>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            Rigidbody.MovePosition(transform.position + new Vector3(0, -1) * Time.deltaTime * train.Speed);
            yield return new WaitForEndOfFrame();
        }
    }
}