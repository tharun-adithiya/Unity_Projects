using System.Collections;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    private Transform _targetTransform;
    [SerializeField]private GameObject _fallingObject;
    [SerializeField]Rigidbody _ballRb;
    [SerializeField] private float _fallSpeed;
    private Vector3 _objectMotion;
    [SerializeField] private Transform _spawnArea;
    public ObjectMovement movement;
    public GridGenerator m_generator;
    private Vector3 randomPosition;
    [SerializeField]private float m_spawnRate=0.5f;
    [SerializeField] private float m_spawnHeight = 25f;


    void Start()
    {
        
        _targetTransform=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(SpawnObstacle(m_spawnRate));
    }

    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        randomPosition = new Vector3(m_generator.randomPosX, m_spawnHeight, m_generator.randomPosZ);

    }
    private IEnumerator SpawnObstacle(float spawnRate)
    {
        while (_targetTransform != null)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(_fallingObject, randomPosition, _fallingObject.transform.rotation);
            //Debug.Log("Xpos:" + randomPosition.x + ", Zpos:" + randomPosition.z);
        }
        
    }
}
