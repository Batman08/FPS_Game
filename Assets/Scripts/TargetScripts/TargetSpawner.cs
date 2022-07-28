using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [HideInInspector] public bool ShouldSpawn = false;
    [HideInInspector] public bool ReadyToSpawn = false;
    [HideInInspector] public int TotalNumberSpawned = 0;
    public float TargetHealth = 1000f;
    
    public GameObject TargetObject;

    [SerializeField] private Transform[] _spawnLocations;
    [SerializeField] private TextMeshProUGUI _targetCounterText;

    private int _randomSpawnLocationIndex; //added

    private void Update()
    {
        if (ShouldSpawn)
        {
            //while (transform.childCount < NumberOfTargets())
            while (TotalNumberSpawned < NumberOfTargets())
            {
                _randomSpawnLocationIndex = Random.Range(0, _spawnLocations.Length);
                Instantiate(TargetObject, _spawnLocations[_randomSpawnLocationIndex].position, Quaternion.identity, transform);
                TotalNumberSpawned++;
                ShouldSpawn = false;

                //if (transform.childCount < 24) //added
                //{
                //    _randomSpawnLocationIndex = Random.Range(0, _spawnLocations.Length);
                //    Instantiate(TargetObject, _spawnLocations[_randomSpawnLocationIndex].position, Quaternion.identity, transform);
                //    TotalNumberSpawned++;
                //    ShouldSpawn = false;
                //}
            }

            //GameManager.gameManager.Targets = FindObjectsOfType<Target>();
            ReadyToSpawn = true;
        }

        _targetCounterText.text = $"Target: {transform.childCount}";
    }

    public double NumberOfTargets()
    {
        double preciseNumberOfTargets = 0.000058 * Mathf.Pow(GameManager.gameManager.RoundNumber, 3) + 0.074032 * Mathf.Pow(GameManager.gameManager.RoundNumber, 2) + 0.718119 * GameManager.gameManager.RoundNumber + 14.738699;
        double numberOfTargetsRounded = Mathf.Round((float)preciseNumberOfTargets);
        Debug.Log(preciseNumberOfTargets);
        return numberOfTargetsRounded;
    }
}


/*
 
--R = current round number

1 player: 0,000058 * R^3 + 0,074032 * R^2 + 0,718119 *R + 14,738699

2 players: 0,000054 * R^3 + 0,169717 * R^2 + 0,541627 *R + 15,917041

3 players: 0,000169 * R^3 + 0,238079 * R^2 + 1,307276 *R + 21,291046

4 players: 0,000225 * R^3 + 0,314314 * R^2 + 1,835712 *R + 27,596132
 
 */