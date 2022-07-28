using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [HideInInspector]  public int RoundNumber;
    //[HideInInspector] public Target[] Targets;
    //[HideInInspector] public GameObject[] Targets;
    public int NumberOfPlayers;

    [SerializeField] private TextMeshProUGUI _roundCounterText;
    [SerializeField] private bool _chooseRoundNumber = false;

    private TargetSpawner _targetSpawner;

    private void Awake()
    {
        gameManager = this;
        NumberOfPlayers = 1; //added
        RoundNumber = 0;
    }

    private void Start()
    {
        _targetSpawner = FindObjectOfType<TargetSpawner>();
        _targetSpawner.ReadyToSpawn = true;
    }

    private void Update()
    {
        RoundSystem();
    }

    private bool IsRoundOver()
    {
        return _targetSpawner.ReadyToSpawn == true && _targetSpawner.transform.childCount <= 0 && _targetSpawner.TotalNumberSpawned == _targetSpawner.NumberOfTargets();
    }

    private void RoundSystem()
    {
        if (RoundNumber <= 0 || _chooseRoundNumber == true) //todo: remove _chooseRoundNumber when tiding up game
        {
            _chooseRoundNumber = false;
            SetRoundProperties();
        }
        else if (IsRoundOver() == true)
        {
            SetRoundProperties();
            SetTargetHealthIncremental();
        }

        //target starting health: 150
        //check if round > 1 & < 10 increase target health by 100
        //round 10 onwards health increases by 10%
        //round to whole number
    }

    private void SetRoundProperties()
    {
        RoundNumber += 1; // spawning targets total off by 2 targets    
        _roundCounterText.text = $"Round: {RoundNumber}";

        _targetSpawner.TotalNumberSpawned = 0;
        _targetSpawner.ReadyToSpawn = false;
        _targetSpawner.ShouldSpawn = true;
    }

    private void SetTargetHealthIncremental() {
        if (RoundNumber > 1 && RoundNumber < 10)
        {
            _targetSpawner.TargetHealth += 150;
        }
        else if (RoundNumber >= 10) {
            float percentIncrease = (_targetSpawner.TargetHealth * 0.1f);
            _targetSpawner.TargetHealth += percentIncrease;
        }
    }
}
