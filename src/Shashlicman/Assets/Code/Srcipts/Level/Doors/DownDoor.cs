using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomDoorCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.doorSide = "down";
                other.transform.position = new Vector3(50, 0, 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
