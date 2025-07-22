using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CollectiblesRotator : MonoBehaviour
{
    [SerializeField] private float m_rotateSpeed;
    [SerializeField] private List<GameObject> m_collectibles;
    private Vector3 m_collectibleRotation;

    private void Start()
    {
        m_collectibleRotation=new Vector3(0f,m_rotateSpeed,0f);
    }
    void Update()
    {
        foreach (var collectible in m_collectibles)
        {
            if (collectible != null)
            {
                collectible.transform.Rotate(m_collectibleRotation*Time.deltaTime);
            }
        }
    }
}
