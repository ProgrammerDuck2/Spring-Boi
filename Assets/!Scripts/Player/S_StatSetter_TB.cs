using NaughtyAttributes;
using UnityEngine;

public class S_StatSetter_TB : MonoBehaviour
{
    [Required]
    [Expandable]
    public S_StatsObject_TB Stats;

    void Awake()
    {
        S_Stats_MA.maxHealth = Stats.maxHealth;
        S_Stats_MA.playerHealth = Stats.playerHealth;
        S_Stats_MA.Damage = Stats.Damage;
        S_Stats_MA.JumpPower = Stats.JumpPower;
        S_Stats_MA.Speed = Stats.Speed;
        S_Stats_MA.HandLaunchSpeed = Stats.HandLaunchSpeed;
        S_Stats_MA.HandLaunchReach = Stats.HandLaunchReach;
        S_Stats_MA.HandGrabRadius = Stats.HandGrabRadius;
        S_Stats_MA.AimAssistRadius = Stats.AimAssistRadius;
        S_Stats_MA.MaxVelocity = Stats.MaxVelcity;
        S_Stats_MA.AerialMaxVelocity = Stats.AerialMaxVelocity;
    }
}