using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public static class S_Stats_MA
{
    public static float playerHealth;
    public static float maxHealth = 100;
    public static float Damage = 10;
    public static float JumpPower = 1;
    public static Vector2 Speed = new Vector2(4.5f, 16); //X = Walkspeed, Y = Runspeed
    public static float TurnSpeed = 15;

    public static Vector3 MaxVelocity;
    public static Vector3 AerialMaxVelocity;

    //Hand stats
    public static float HandLaunchSpeed = 3;
    public static float HandLaunchReach = 30;

    public static float HandGrabRadius = .2f;

    public static float AimAssistRadius = 1;

    //collectibles
    public static float maxOil = 100;
    public static float oilCollected;
}