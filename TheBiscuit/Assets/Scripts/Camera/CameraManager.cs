using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D m_confiner2D;
    [SerializeField] private List<GameObject> m_bounds = new List<GameObject>();
    private Dictionary<string,GameObject> m_boundsDictionary = new Dictionary<string,GameObject>();

    private void Start()
    {
        foreach (var obj in m_bounds)
        {
            m_boundsDictionary.Add(obj.name, obj);
            Debug.Log($"{obj.name} added to dictionary with value {obj}");
        }
    }
    private void OnEnable()
    {
        BoundsChecker.OnPassCamBounds += SwitchBounds;   
    }
    private void OnDisable()
    {
        BoundsChecker.OnPassCamBounds -= SwitchBounds;
    }
    public void SwitchBounds(string currentBounds)
    {
        if (m_boundsDictionary.TryGetValue(currentBounds, out GameObject boundsObject))
        {
            m_confiner2D.BoundingShape2D = boundsObject?.GetComponent<PolygonCollider2D>();
            Debug.Log($"Bounds changed to {currentBounds}");
        }
        else
        {
            Debug.Log($"Bounds {currentBounds} not found");
        }
    }

}
