using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class S_EnemyFight : S_Enemies_MA
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private GameObject hand;

    private float punchLenght = 5;
    float punchSpeed = 1f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        hand = rightHand;
    }

    public override IEnumerator Attack(float damage)
    {
        int whichHand = Random.Range(0, 2);
        if (whichHand == 0)
        {
            hand = leftHand;
        }
        else if (whichHand == 1)
        {
            hand = rightHand;
        }

        yield return StartCoroutine(Ready(attackRate - attackRate/2));

        float timer = 0;
        float value = 0;
        
        while (timer < attackRate / 4)
        {
            hand.transform.localScale = new Vector3(
                Mathf.Lerp(.5f, punchLenght, value), 
                hand.transform.localScale.y, 
                hand.transform.localScale.z
                );

            value += Time.deltaTime * (attackRange);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(base.Attack(damage));

        yield return StartCoroutine(Reset(attackRate / 12));
    }

    public IEnumerator Ready(float timeToReady)
    {
        float timer = 0;
        float value = 0;

        while (timer < timeToReady)
        {
            hand.transform.localScale = new Vector3(
                Mathf.Lerp(1, .5f, value),
                hand.transform.localScale.y,
                hand.transform.localScale.z
                );
            timer += Time.deltaTime;
            value += Time.deltaTime * (1 / timeToReady);

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator Reset(float timeToReset)
    {
        float timer = 0;
        float value = 0;

        while (timer < timeToReset)
        {
            hand.transform.localScale = new Vector3(
                Mathf.Lerp(punchLenght, 1, value),
                hand.transform.localScale.y,
                hand.transform.localScale.z
                );
            timer += Time.deltaTime;
            value += Time.deltaTime * (1 / timeToReset);

            yield return new WaitForEndOfFrame();
        }
    }

}
