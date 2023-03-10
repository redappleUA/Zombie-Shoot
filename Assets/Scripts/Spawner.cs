using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Traits;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting }

    [SerializeField] private List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountdown = 0;

    [SerializeField] private List<GameObject> zombies = new List<GameObject>();
    public List<Wave> waves = new ();

    public int killedZombies { get; private set; } = 0;
    private List<GameObject> spawnedZombies = new();
    public int currentWave { get; set; }
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
            Debug.Log("Spawning");
            if (state != SpawnState.Spawning)
            {
                if (currentWave < waves.Count) 
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

    void SpawnZombie()
    {
        var newZombie = Instantiate(zombies[Random.Range(0, zombies.Count)],
                spawners[Random.Range(0, spawners.Count)].transform.position, Quaternion.identity);
        newZombie.GetComponent<NavMeshAgent>().enabled = true;
        Debug.Log(currentWave + " spawned");
        spawnedZombies.Add(newZombie);
    }

    bool EnemiesAreDead()
    {
        for (int i = 0; i < spawnedZombies.Count; i++)
        {
            if (!spawnedZombies[i].GetComponent<Health>().IsAlive())
            {
                spawnedZombies.RemoveAt(i);
                killedZombies++;
                Debug.LogWarning("Dead");
            }
            else  return false;
        }
        spawnedZombies.Clear();
        return true;
    }

    private void ComleteWave()
    {   
        waveCountdown = timeBetweenWaves;
        killedZombies++;

        if (currentWave + 1 > waves.Count)
        {
            Debug.LogWarning("All waves comleted");
            waves.Clear();
        }
        else
        {
            state = SpawnState.Counting;
            Debug.LogWarning("Wave comleted");
            currentWave++;
        }
    }
}
