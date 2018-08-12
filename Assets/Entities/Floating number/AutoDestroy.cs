using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float timer = 2f;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
	}
	
}
