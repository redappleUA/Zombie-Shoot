using Opsive.UltimateCharacterController.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Transform target;
    private Animator anim;
    private Health m_health;
    private Health z_health;

    private float z_lastHealth;
    private readonly float[] attackAnim = { 0, .25f, .75f, 1 };
    private float timeOfLastAttack = 0;
    private bool hasStopped = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        z_health = GetComponent<Health>();

        target = GameObject.FindWithTag("Player").transform;
        m_health = target.GetComponent<Health>();

        z_lastHealth = z_health.HealthValue;
    }

    void FixedUpdate()
    {
        MoveToTarget();

        if (z_health.HealthValue < z_lastHealth) TakeDamage();
        
        z_lastHealth = z_health.HealthValue;
    }

    private void MoveToTarget()
    {
        float stopLength = Vector3.Distance(target.position, transform.position);

        // Cast a ray from the AI's position in the direction it is facing
        Ray ray = new(transform.position, transform.forward);

        // Transform the ray from world space to local space
        ray = new Ray(transform.InverseTransformPoint(ray.origin), transform.InverseTransformDirection(ray.direction));

        if (Physics.Raycast(ray, out var hit))
        {
            Debug.DrawRay(transform.position, transform.forward);
            if (stopLength <= agent.stoppingDistance)
            {
                if (m_health.HealthValue != 0)
                {
                    if (!hasStopped)
                    {
                        timeOfLastAttack = Time.time;
                        hasStopped = true;
                    }

                    if (timeOfLastAttack + 2 <= Time.time)
                    {
                        if (hasStopped)
                            hasStopped = false;

                        if (hit.collider.CompareTag("Player"))
                            Attack();
                    }
                }
                else
                {
                    anim.SetFloat("Move", 0f, .2f, Time.fixedDeltaTime);
                }
            }
            else
            {
                agent.SetDestination(target.position);
                anim.SetFloat("Move", 1, .2f, Time.fixedDeltaTime);
            }
        }

    }

    void Attack()
    {
        //Random choose the punch animation
        anim.SetFloat("AttackIndex", attackAnim[Random.Range(0, attackAnim.Length)]);

        // set the attack trigger in the Animator
        anim.SetTrigger("Attack");
    }

    public void Hit() => target.GetComponent<Health>().Damage(10);

    void TakeDamage()
    {
        anim.SetTrigger("TakeDamage");
    }
}
