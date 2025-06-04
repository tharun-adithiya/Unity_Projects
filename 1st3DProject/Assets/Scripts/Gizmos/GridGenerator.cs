using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridGenerator : MonoBehaviour
{
    public int xGrid = 10;
    public int zGrid = 10;
    [SerializeField] private GameObject cube;
    [HideInInspector] public float randomPosX;
    [HideInInspector] public float randomPosZ;
    [HideInInspector] public float randomPosXFinal;
    [HideInInspector] public float randomPosZFinal;
    private List<Vector3> m_WorldPosition;

    private void Start()
    {
        m_WorldPosition = new List<Vector3>();
    }
    private void Update()
    {
        randomPosX = Random.Range(-xGrid, xGrid);
        randomPosZ = Random.Range(-zGrid, zGrid);
        foreach (var point in GridDetails())
        {
            m_WorldPosition.Add(point);
        }
        //randomPosXFinal = Random.Range(m_WorldPosition[0].x, m_WorldPosition[m_WorldPosition.Count-1].x);
        //randomPosXFinal = Random.Range(m_WorldPosition[0].z, m_WorldPosition[m_WorldPosition.Count-1].z);
        //randomPosXFinal = Random.Range(m_WorldPosition[0].z, m_WorldPosition[m_WorldPosition.Count-1].z);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Xpos: " + randomPosX + ", Zpos: " + randomPosZ);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;

        foreach (var point in GridDetails())
        {
            //m_WorldPosition.Add(point);
            Gizmos.DrawWireCube(point, new Vector3(1, 0, 1));
        }
    }

    IEnumerable<Vector3> GridDetails()
    {
        for (int i = 0; i < xGrid; i++)
        {
            for (int j = 0; j < zGrid; j++)
            {
                // Convert local grid position to world space
                Vector3 localPosition = new Vector3(i, 0, j);
                Vector3 worldPosition = transform.TransformPoint(localPosition);
                yield return worldPosition;
            }
        }
    }
}
