using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy_control : MonoBehaviour
{
    //public Transform Player;
    CHAracter_States states;
    Animator anim;
    NavMeshAgent agent;
    [SerializeField] float attackRadius = 5;
    bool canAttack = true;
    float attackcooldown = 2f;
    AudioSource damageSound;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        states=GetComponent<CHAracter_States>();
        damageSound = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float distance =  Vector3.Distance (transform.position, level_manager.instance.player.position);
        if (distance < attackRadius)
        {
            agent.SetDestination(level_manager.instance.player.position);
            anim.SetFloat("speed",agent.velocity.magnitude);
            if (distance <= agent.stoppingDistance)
            {
                if (canAttack)
                {
                    StartCoroutine(cooldown());
                    anim.SetTrigger("attack");
                }
            }
        }
    }
    IEnumerator cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackcooldown);
        canAttack = true;
    }

    //player damage enemy

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("die");
          
            states.Changehealth(-other.GetComponentInParent<CHAracter_States>().power);
            anim.SetTrigger("damage");
            damageSound.Play();




        }
    }

   
    // enemy damage palyer
    public void DamagePlayer()
    {
        level_manager.instance.player.GetComponent<CHAracter_States>().Changehealth(-states.power);
       

    }

    
    


}
