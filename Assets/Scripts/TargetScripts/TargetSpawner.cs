using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public int RoundNumber = 1;
    public bool ShouldSpawn = true;
    public GameObject TargetObject;

    [SerializeField] private Transform[] _spawnLocations;

    private int _randomSpawnLocationIndex;

    private void Start()
    {

    }

    private void Update()
    {
        if (ShouldSpawn)
        {
            while (transform.childCount < NumberOfTargets())
            {
                _randomSpawnLocationIndex = Random.Range(0, _spawnLocations.Length);
                //Instantiate(TargetObject, transform);
                Instantiate(TargetObject, _spawnLocations[_randomSpawnLocationIndex].position, Quaternion.identity, transform);
                ShouldSpawn = false;
            }
        }

        Debug.Log(transform.childCount);
    }

    private double NumberOfTargets()
    {
        double preciseNumberOfTargets = 0.000058 * Mathf.Pow(RoundNumber, 3) + 0.074032 * Mathf.Pow(RoundNumber, 2) + 0.718119 * RoundNumber + 14.738699;
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