using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Configuration", menuName = "Config/PlayerControll")]
public class PlayerControlConfig : ScriptableObject
{
    public float horizontalSpeed = 15, laneDistanceX = 4, jumpDistanceZ = 5, jumpHeightY = 2, jumpLerpSpeed = 10, rollDistanceZ = 5;

    public PlayerControlConfig(float horizontalSpeed, float laneDistanceX, float jumpDistanceZ, float jumpHeightY, float jumpLerpSpeed, float rollDistanceZ)
    {
        this.horizontalSpeed = horizontalSpeed;
        this.laneDistanceX = laneDistanceX;
        this.jumpDistanceZ = jumpDistanceZ;
        this.jumpHeightY = jumpHeightY;
        this.jumpLerpSpeed = jumpLerpSpeed;
        this.rollDistanceZ = rollDistanceZ;
    }
}
