using UnityEngine;

public interface IPooledObject
{
    public void OnSpawnObject();
    public void Initialize(Transform target);
}
