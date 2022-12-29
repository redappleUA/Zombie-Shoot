using Opsive.UltimateCharacterController.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] AudioClip hitAudio;
    [SerializeField] List<AudioClip> runAudio = new();

    private NavMeshAgent agent = null;
    private Transform target;
    private Animator anim;
    private Health m_health;
    private Health z_health;

    private float z_lastHealth;
    private readonly float[] attackAnim = { 0, .25f, .75f, 1 };
    private float timeOfLastAttack = 0;
    private bool hasStopped = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            
            if (!audioSource.isPlaying && Random.value > .5f && z_health.IsAlive())
            {
                audioSource.clip = runAudio[Random.Range(0, runAudio.Count)];
                audioSource.Play();
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

    public void Hit()
    {
        if(gameObject.GetComponent<Health>().IsAlive())
            target.GetComponent<Health>().Damage(10);
    }

    void TakeDamage()
    {
        transform.Find("FX_BloodSplat_01").Rotate(new Vector3(0, Random.Range(0, 360), 0));

        var blood = GetComponentInChildren<ParticleSystem>();
        blood.Play();

        audioSource.clip = hitAudio;
        if (!audioSource.isPlaying && z_health.IsAlive())
        {
            audioSource.Play();
        }

    }
}
