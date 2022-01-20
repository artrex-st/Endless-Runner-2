using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameMode Configuration", menuName = "Config/GameMode")]
public class GameModeConfig : ScriptableObject
{
    [Header("Multipliers")]
    public float initialSpeed, maximumSpeed, timeToMaximumSpeed, baseScoreMultiplier = 1, reloadGameDelay = 3;
    [Range(0,9f)] public float timerToStartRun;
    [Range(1,100f)] public float scoreByDistanceValue;

    public GameModeConfig(float initialSpeed, float maximumSpeed, float timeToMaximumSpeed, float baseScoreMultiplier, float reloadGameDelay, float timerToStartRun, float scoreByDistanceValue)
    {
        this.initialSpeed = initialSpeed;
        this.maximumSpeed = maximumSpeed;
        this.timeToMaximumSpeed = timeToMaximumSpeed;
        this.baseScoreMultiplier = baseScoreMultiplier;
        this.reloadGameDelay = reloadGameDelay;
        this.timerToStartRun = timerToStartRun;
        this.scoreByDistanceValue = scoreByDistanceValue;
    }
}
