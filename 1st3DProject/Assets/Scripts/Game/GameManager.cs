using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private PlayerMovement m_playerMovement;
    [SerializeField]private ObjectFall m_objectFall;

    public  void OnInventoryFull()
    {
        m_playerMovement.playerSpeed = m_playerMovement.playerSpeed + 2.6f;
        Debug.Log($"Player Speed:{m_playerMovement.playerSpeed}");
        m_objectFall.spawnRate = 0.77f;
        m_objectFall.spawnHeight += 30f;
        Debug.Log($"SpawnRate:{m_objectFall.spawnRate}");
    }

    public void OnCollectMaterial()
    {
        Debug.Log($"Player Speed:{m_playerMovement.playerSpeed}");
        m_playerMovement.playerSpeed = m_playerMovement.playerSpeed + 0.5f;
        Debug.Log($"Player Speed:{m_playerMovement.playerSpeed}");
    }
}
