using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "Player/StatData", order = 1)]
[System.Serializable]
public class S_StatsObject_TB : ScriptableObject
{
    [Header("Player Stats")]
    [MinValue(1)]
    public float maxHealth = 100;
    [HideInInspector] public float playerHealth;
    [MinValue(1)]
    public float Damage = 10;
    [MinValue(1)]
    public float JumpPower = 1;
    [Space]
    [InfoBox("X = Walkspeed \nY = Runspeed")]
    public Vector2 Speed = new Vector2(4.5f, 16);

    public Vector3 MaxVelcity;
    public Vector3 AerialMaxVelocity;

    [Space]
    [HorizontalLine(color: EColor.Violet)]
    [Header("Hand Specific")]
    [MinValue(1)]
    public float HandLaunchSpeed = 3;
    [MinValue(1)]
    public float HandLaunchReach = 30;
    [MinValue(.01f)]
    public float HandGrabRadius = .2f;
    [MinValue(.5f)]
    public float AimAssistRadius = .5f;
}
