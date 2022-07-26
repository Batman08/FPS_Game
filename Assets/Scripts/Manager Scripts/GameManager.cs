using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [HideInInspector]
    public int RoundNumber;

    [SerializeField] private TextMeshProUGUI _roundCounterText;
    private TargetSpawner _targetSpawner;

    private void Awake()
    {
        gameManager = this;
        RoundNumber = 0;
    }

    private void Start()
    {
        _targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    private void Update()
    {
        //control target spawner
        
        RoundSystem();
    }

    private void RoundSystem()
    {
        if (_targetSpawner.ReadyToSpawn == true && _targetSpawner.transform.childCount <= 0)
        {
            RoundNumber += 1;
            _roundCounterText.text = $"Round: {RoundNumber}";
            _targetSpawner.ReadyToSpawn = false;
            _targetSpawner.ShouldSpawn = true;
        }
    }
}
