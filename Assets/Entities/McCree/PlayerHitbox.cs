using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private GameObject gunFather;

    public void Hit(Bullet bullet)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}