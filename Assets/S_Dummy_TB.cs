using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Dummy_TB : MonoBehaviour, S_Enemies_MA
{
    [SerializeField] float maxHealth;
    [ShowNonSerializedField] float health;

    [Required]
    [SerializeField] GameObject freeText;

    GameObject art;

    public void Attack(float damage)
    {
        //dummy cant attack :(
    }

    public void Hurt(float damage)
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
    void Start()
    {
        health = maxHealth;
        art = transform.GetChild(0).gameObject;
    }

    private void Update()
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
