using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Traits;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting }

    [SerializeField] private List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountdown = 0;

    [SerializeField] private List<GameObject> zombies = new List<GameObject>();
    [SerializeField] private Wave[] waves;

    private List<GameObject> spawnedZombie = new();
    private int currentWave;
    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if (!EnemiesAreDead()) return;
            else ComleteWave();
        }

        if(waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
            }

        }
        else 
            waveCountdown -= Time.deltaTime;
    }
    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.Spawning;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(wave.delay);
        }
        state = SpawnState.Waiting;
        yield break;
    }

#warning FIX
    void SpawnZombie()
    {
        var newZombie = Instantiate(zombies[Random.Range(0, zombies.Count)],
                spawners[Random.Range(0, spawners.Count)].transform.position, Quaternion.identity);
        Debug.Log(currentWave);
        spawnedZombie.Add(newZombie);
    }

    bool EnemiesAreDead()
    {
        int i = 0;
        foreach(var zombie in spawnedZombie)
        {
            if (zombie.GetComponent<Health>().IsAlive())
                i++;
            else return false;
        }
        return true;
    }

    private void ComleteWave()
    {   
        waveCountdown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length)
        {
            currentWave = 0;
            Debug.LogWarning("Wave comleted");
        }
        else currentWave++;
    }
}
