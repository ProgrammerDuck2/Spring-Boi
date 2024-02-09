using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PistonLong_MA : MonoBehaviour, S_Enemies_MA
{
    private Vector3 velocity;
    public bool Grounded;
    Vector3 groundCheckPos;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stickGroundLayer;
    [SerializeField] float GravityMultiplier = 3.5f;
    float MaxVelocity = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (Grounded)
        {
            velocity.y = Mathf.Sqrt(1 * 5 * -3f * Physics.gravity.y);
        }
    }

    public void Attack(float damage)
    {
        
    }

    public void Hurt(float damage)
    {
        
    }
    private void FixedUpdate()
    {
        groundCheckPos = transform.position - transform.up * 0.9f;

        if (Grounded != Physics.CheckSphere(groundCheckPos, 1 * 0.5f, groundLayer))
        {
            Collider[] ground = Physics.OverlapSphere(groundCheckPos, 1 * 0.5f, stickGroundLayer);

            Grounded = !Grounded;
            transform.parent = ground.Length >= 1 ? ground[0].transform : null;
        }
    }

    float t;
    void Gravity()
    {
        if (Grounded & velocity.y < 0)
        {
            velocity = Vector3.zero;
        }
        else
        {
            velocity.y += Physics.gravity.y * GravityMultiplier * Time.deltaTime;

            velocity = new Vector3(Mathf.Lerp(velocity.x, 0, t), velocity.y + Physics.gravity.y * GravityMultiplier * Time.deltaTime, Mathf.Lerp(velocity.z, 0, t));

            velocity = new Vector3(velocity.x, Mathf.Clamp(velocity.y, -MaxVelocity, MaxVelocity), velocity.z);

            t = 2f * Time.deltaTime;
        }

        //cc.Move(velocity * Time.deltaTime);
    }
}
