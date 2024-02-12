using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
[System.Serializable]
public class S_StatsInterface_TB : ScriptableObject
{
    public float playerHealth;
    public float maxHealth = 100;
    public float Damage = 10;
    public float JumpPower = 1;
    public Vector2 Speed = new Vector2(4.5f, 16); //X = Walkspeed, Y = Runspeed

    //Hand stats
    public float HandLaunchSpeed = 3;
    public float HandLaunchReach = 30;

    public float HandGrabRadius = .2f;
}
