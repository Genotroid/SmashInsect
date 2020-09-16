using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnArea : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private GameObject _spawnerPool;

    private float _colliderSizeX;
    private float _lastSpawnTime = 0f;

    public event UnityAction<Insect> InsectSpawned;

    public GameObject SpawnerPool => _spawnerPool;

    private void Start()
    {
        _colliderSizeX = GetComponent<BoxCollider2D>().size.x / 2;
    }

    private void Update()
    {
        _lastSpawnTime += Time.deltaTime;
        if(_lastSpawnTime >= _spawnDelay && _spawnerPool.transform.childCount > 0)
        {
            int randomInsectIndex = Random.Range(0, _spawnerPool.transform.childCount);
            if (TryGetInsect(out Transform insect, randomInsectIndex))
            {
                _lastSpawnTime = 0;
                insect.gameObject.SetActive(true);
            }
        }
    }

    private bool TryGetInsect(out Transform result, int index)
    {
        result = _spawnerPool.transform.GetChild(index);

        return result.gameObject.activeSelf != true;
    }

    public void SetInsectList(InsectList[] insectList)
    {
        foreach(InsectList item in insectList)
        {
            for(int i = 0; i < item.Count; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-_colliderSizeX, _colliderSizeX), transform.position.y, transform.position.z);
                Insect insect = Instantiate(item.Insect, spawnPoint, item.Insect.gameObject.transform.rotation, _spawnerPool.transform);
                InsectSpawned?.Invoke(insect);
                insect.gameObject.SetActive(false);
            }
        }
    }
}

