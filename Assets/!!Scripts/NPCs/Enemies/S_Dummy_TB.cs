using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Dummy_TB : S_Enemies_MA
{
    [ShowNonSerializedField] float health;

    [Required]
    [SerializeField] GameObject freeText;

    GameObject art;

    public override IEnumerator Attack(float damage)
    {
        //dummy cant attack :(
        yield return null;
    }

    public override void Hurt(float damage, GameObject WhoDealtDamage)
    {
        health -= damage;

        Debug.Log(name + " lost: " + damage + " HP");

        StartCoroutine(TookDamage(damage));
    }

    IEnumerator TookDamage(float damage)
    {
        GameObject currentDmg = Instantiate(freeText, transform.position + transform.forward * 0.5f, transform.rotation);
        currentDmg.GetComponent<RectTransform>().forward = -currentDmg.GetComponent<RectTransform>().forward;
        currentDmg.transform.localScale = Vector3.one * 0.1f;
        currentDmg.GetComponent<TMP_Text>().text = damage.ToString();


        yield return new WaitForSeconds(.5f);

        Destroy(currentDmg);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        health = maxHealth;
        art = transform.GetChild(0).gameObject;
    }

    public override void Update()
    {
        if (health < 0)
        {
            art.SetActive(false);
        } 
        else if(!art.activeSelf)
        {
            art.SetActive(true);
        }

        if(health < maxHealth)
        {
            health += 5 * Time.deltaTime;
        }
    }
}
