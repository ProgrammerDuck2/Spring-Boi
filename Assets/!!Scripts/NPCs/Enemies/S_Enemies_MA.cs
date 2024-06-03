using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Fracture))]
[RequireComponent(typeof(Animator))]
public class S_Enemies_MA : MonoBehaviour
{
    public enum EnemyType
    {
        Roaming,
        OnSight,
        None
    }

    public EnemyType enemyType;

    [SerializeField] Animator enemyAnimator;

    public GameObject player { get; private set; }

    [HideInInspector] public float nextAttack = 0.0f;

    [Header("Stats")]
    public float maxHealth = 100;
    public float damage = 100;
    public float attackRate = 2f;
    public float sightRange = 15;
    public float attackRange = 10;
    [SerializeField] bool roaming = true;

    [Space(10)]
    public GameObject drop;

    //[Space(10)]
    //[ShowIf(nameof(roaming))]
    //[Required] public GameObject navCorner1;
    //[ShowIf(nameof(roaming))]
    //[Required] public GameObject navCorner2;

    [Space(10)]
    [HideInInspector] public NavMeshAgent navMeshAgent;

    [SerializeField] GameObject mapIcon;
    [SerializeField] List<Fracture> fractureArt;

    public virtual void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.destination = randomDestination();

        //if (navCorner1 == null || navCorner2 == null || !roaming) return;

        //Vector3 c1 = navCorner1.transform.position;
        //Vector3 c2 = navCorner2.transform.position;
        //navMeshAgent.destination = new Vector3(Random.Range(c1.x, c2.x), c1.y, Random.Range(c1.z, c2.z));
    }

    public virtual void Update()
    {
        if (maxHealth <= 0) return;
        //annimations

        switch (enemyType)
        {
            case EnemyType.Roaming:
                {
                    enemyAnimator.SetBool("IsWalking", false);

                    SetDestination();
                    PlayerFound();

                    break;
                }

            case EnemyType.OnSight:
                {
                    enemyAnimator.SetBool("IsWalking", false);
                    PlayerFound();
                    break;
                }

            case EnemyType.None:
                {
                    enemyAnimator.SetBool("IsWalking", false);
                    break;
                }
        }

        //if (navCorner1 == null || navCorner2 == null) return;

        if (attackRate <= nextAttack)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                StartCoroutine(Attack(damage));
            }
        }

        nextAttack += Time.deltaTime;
    }

    //use on enemy scripts, MonoBehaviour, S_Enemies_MA
    public virtual IEnumerator Attack(float damage)
    {
        nextAttack = 0;

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            player.GetComponent<S_PlayerHealth_MA>().Hurt(damage, gameObject);
        }

        yield return null;
    }

    //call when player attacks an enemy
    public virtual void Hurt(float damage, GameObject WhoDealtDamage)
    {
        maxHealth -= damage;

        if (maxHealth <= 0)
        {
            Die();
        }
    }
    [Button]
    public void Damage()
    {
        Hurt(50, null);
    }

    [Button("Kill")]
    public virtual void Die()
    {
        maxHealth = 0;
        navMeshAgent.enabled = false;

        foreach (var item in fractureArt)
        {
            item.CauseFracture();

            GameObject pieces = GameObject.Find(item.name + "Fragments");

            for (int i = 0; i < pieces.transform.childCount; i++)
            {
                GameObject piece = pieces.transform.GetChild(i).gameObject;

                piece.AddComponent<S_Pickupable_TB>();
                piece.tag = "Interactable";
                piece.gameObject.layer = 11;
                piece.GetComponent<Rigidbody>().AddForce(-(player.transform.position - transform.position).normalized * 1.5f, ForceMode.Impulse);
            }
        }

        Destroy(gameObject, 1);
    }

    private void OnDestroy()
    {
        mapIcon.SetActive(false);

        if (drop != null)
        {
            Instantiate(drop, transform.position + transform.up * 2, transform.rotation);
        }
    }

    protected Vector3 randomDestination()
    {
        return new Vector3(
            transform.position.x + Random.Range(-10, 10),
            transform.position.y,
            transform.position.z + Random.Range(-10, 10));
    }

    bool destinationReached()
    {
        return Vector3.Distance(transform.position, navMeshAgent.destination) < 1;
    }

    void SetDestination()
    {
        if (roaming && destinationReached())
        {
            navMeshAgent.destination = randomDestination();

            if (enemyAnimator != null)
                enemyAnimator.SetBool("IsWalking", true);
        }
    }

    public virtual void PlayerFound()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < sightRange)
        {
            transform.LookAt(player.transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            navMeshAgent.destination = player.transform.position - transform.forward * 1.5f;
            navMeshAgent.speed = 2;

            if(enemyAnimator != null)
                enemyAnimator.SetBool("IsWalking", true);
        }
    }
}

