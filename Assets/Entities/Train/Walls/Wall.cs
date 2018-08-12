using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public float DamageScale = 1;
    public FloatingNumber textPrefub; 

    public void Hit(Bullet bulletScript)
    {
        var damage = bulletScript.Damage * DamageScale;

        GetComponentInParent<Train>().RecievedDamage(damage, bulletScript.transform.position);

        if (textPrefub != null)
        {
            var label = Instantiate(textPrefub, bulletScript.transform.position, Quaternion.identity);
            label.textMesh.text = "- " + damage;

            if (DamageScale < 1) label.textMesh.color = Color.gray;
            if (DamageScale > 1) label.textMesh.color = Color.red;
        }
    }
}
