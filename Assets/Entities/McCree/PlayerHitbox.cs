using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private GameObject gunFather;

    public void Hit(Bullet bullet)
    {
        foreach (var gun in gunFather.GetComponentsInChildren<Gun>())
        {
            gun.ResetCooldown();
        }
    }
}