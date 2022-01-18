using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameMode Configuration", menuName = "GameMode Configuration")]
public class GameModeConfig : ScriptableObject
{
    [Header("Multipliers")]
    public float initialSpeed, maximumSpeed, timeToMaximumSpeed, baseScoreMultiplier = 1, reloadGameDelay = 3;
    [Range(0,9f)] public float timerToStartRun;
    [Range(1,100f)] public float scoreValueSpeed;
    // 
    // private float _InitialSpeed => initialSpeed;
    // private float _MaximumSpeed => maximumSpeed;
    // private float _TimeToMaximumSpeed => timeToMaximumSpeed;
    // private float _BaseScoreMultiplier => baseScoreMultiplier;
    // private float _ReloadGameDelay => reloadGameDelay;
    // private float _TimerToStartRun => timerToStartRun;
    // private float _ScoreperValueSpeed => scoreValueSpeed;    
}
