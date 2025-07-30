using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class AsteroidRainStarter : MonoBehaviour
{
    [SerializeField] private GameObject m_objectPool;
    [SerializeField] private GameObject m_objectFall;
    [SerializeField] private ObjectPooler m_poolObjMovement;
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private Transform m_differentTarget;
    [SerializeField] private ObjectFall m_objectMover;
    [HideInInspector]public bool m_isOutsideOfSafeArea=false;
    [SerializeField] private BoxCollider m_AsteroidTriggerer;
    [SerializeField] private UIManager m_uiManager;

    private void Start()
    {
        m_poolObjMovement.m_targetToFollow = m_differentTarget;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody") && !m_isOutsideOfSafeArea)
        {
           
            m_isOutsideOfSafeArea = true;
            m_poolObjMovement.m_targetToFollow = m_playerTransform;
            m_uiManager.OnPassSafeZone(m_isOutsideOfSafeArea);
            
            
            Debug.Log("SpawnHeight set to 250");
        }
        else if (other.gameObject.CompareTag("PlayerBody") && m_isOutsideOfSafeArea)
        {
            Debug.Log("HeightSet to Zero");
            
            m_isOutsideOfSafeArea = false;
            m_poolObjMovement.m_targetToFollow = m_differentTarget;
       
            m_uiManager.OnPassSafeZone(m_isOutsideOfSafeArea);

        }

    }

}
