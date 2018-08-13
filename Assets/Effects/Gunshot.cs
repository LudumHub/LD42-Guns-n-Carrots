using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour {
    public Gun gunBehaviour;
	public void Shoot()
    {
        gunBehaviour.Shoot();
    }
}
