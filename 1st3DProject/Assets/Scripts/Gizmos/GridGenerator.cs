using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridGenerator : MonoBehaviour
{
    public int xGrid = 10;
    public int zGrid = 10;
    [HideInInspector] public float randomPosX;
    [HideInInspector] public float randomPosZ;

    private void Update()
    {
        randomPosX = Random.Range(-xGrid, xGrid);
        randomPosZ = Random.Range(-zGrid, zGrid);

    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;

        foreach (var point in GridDetails())
        {
            
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
