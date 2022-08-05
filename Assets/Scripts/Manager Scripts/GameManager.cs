using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [HideInInspector] public int NumberOfPlayers;
    [HideInInspector] public int RoundNumber;
     public float PlayerScore;

    [SerializeField] private TextMeshProUGUI _roundCounterText;
    [SerializeField] private bool _chooseRoundNumber = false;

    private TargetSpawner _targetSpawner;
    private float _pointsPerHit = 10;
    private float _pointsPerKill = 60;
    //private float _pointsPerKillWonderWeapon = 60;

    private void Awake()
    {
        gameManager = this;
        NumberOfPlayers = 1; //tod: adjust number of players
        RoundNumber = 0;
        PlayerScore = 0;
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

    private void SetTargetHealthIncremental()
    {
        if (RoundNumber > 1 && RoundNumber < 10)
        {
            _targetSpawner.TargetHealth += 150;
        }
        else if (RoundNumber >= 10)
        {
            float percentIncrease = (_targetSpawner.TargetHealth * 0.1f);
            _targetSpawner.TargetHealth += percentIncrease;
        }
    }

    public void IncrementPlayerScore(bool hasKilledTarget) {
        if (hasKilledTarget)
        {
            PlayerScore += _pointsPerKill;
        }
        else
        {
            PlayerScore += _pointsPerHit;
        }
    }
}
