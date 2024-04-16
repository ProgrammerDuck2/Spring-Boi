using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface S_Enemies_MA
{
    //use on enemy scripts, MonoBehaviour, S_Enemies_MA
    public IEnumerator Attack(float damage);

    //call when player attacks an enemy
    public void Hurt(float damage, GameObject WhoDealtDamage);
}
