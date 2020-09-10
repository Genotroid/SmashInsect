using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnArea : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private InsectList[] _insectList;

    private float _colliderSizeX;
    private float _lastSpawnTime = 0f;

    private void Start()
    {
        _colliderSizeX = GetComponent<BoxCollider2D>().size.x / 2;
    }

    private void Update()
    {
        _lastSpawnTime += Time.deltaTime;
        if(_lastSpawnTime >= _spawnDelay)
        {
            if(TrySpawnInsect(out Insect insect))
            {
                _lastSpawnTime = 0;
                Vector3 spawnPoint = new Vector3(Random.Range(-_colliderSizeX, _colliderSizeX), transform.position.y, transform.position.z);
                Instantiate(insect, spawnPoint, insect.gameObject.transform.rotation);
                //insect.gameObject.SetActive(true);
            }
        }
    }

    private bool TrySpawnInsect(out Insect result)
    {
        result = null;
        InsectList tempInsect = _insectList[Random.Range(0, _insectList.Length)];
        if (tempInsect.Count > 0)
        {
            result = tempInsect.Insect;
            tempInsect.EnableInsect();
        }

        return result != null;
    }
}

