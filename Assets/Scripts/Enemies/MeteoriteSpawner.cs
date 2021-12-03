using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy1;
    [SerializeField] private GameObject _enemy2;

    [Range(0f, 1f)]
    [SerializeField] float _enemy2SpawnFrequency;
    [SerializeField] float _enemy2SpawnScoreThreshold;


    [SerializeField] private float _enemyMinSpawnDelay;
    [SerializeField] private float _spawnDelayModifier;

    private float _spawnDelay;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _spawnDelay = CalculateSpawnDelay();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > _spawnDelay)
        {
            _spawnDelay = CalculateSpawnDelay();
            //Debug.Log(_spawnDelay);

            SpawnEnemy();

            float enemies = FindObjectsOfType<EnemyHealth>().Length;
            GameController.Instance.IncreasePlayerScore(enemies);

            _timer = 0;
        }
    }

    private void SpawnEnemy()
    {
        if (Random.Range(0f,1f) < _enemy2SpawnFrequency && GameController.Instance.PlayerScore > _enemy2SpawnScoreThreshold)
        {
            Instantiate(_enemy2, transform.position, Quaternion.identity, gameObject.transform);
        }
        else
        {
            Instantiate(_enemy1, transform.position, Quaternion.identity, gameObject.transform);
        }

    }

    private float CalculateSpawnDelay()
    {
        float delay = _enemyMinSpawnDelay + _spawnDelayModifier;
        _spawnDelayModifier *= 0.9f;

        return delay;
    }
}
