using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Invector.vCharacterController
{
    public class MyAI : MonoBehaviour
    {
        public GameObject firePoint;
        public GameObject fireball;
        public float fireballSpeed;
        public float attackDistance = 1f;
        public HPBar healthBar;
        public int maxHP;
        public int currentHP;
        public GameObject hpUI;
        public float jumpSpeed;

        private float defaultSpeed;

        NavMeshAgent agent;
        private Transform target;
        Animator anim;
        GameObject player;
        List<Rigidbody> allRbs;
        
        // Start is called before the first frame update
        void Start()
        {

            currentHP = maxHP;
            healthBar.SetMaxHealth(maxHP);

            allRbs = new List<Rigidbody>();
            allRbs.AddRange(GetComponentsInChildren<Rigidbody>());
            player = GameObject.FindGameObjectWithTag("Player");
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player").transform;

            defaultSpeed = agent.speed;

            StartCoroutine(SetTarget());

            foreach (Rigidbody rb in allRbs)
            {
                rb.isKinematic = true;
            }


        }

        public void HasSight()
        {
            StartCoroutine(SetTarget());
        }
        public void Die()
        {
            foreach (Rigidbody rb in allRbs)
            {
                rb.isKinematic = false;
            }
            anim.SetBool("isDead", true);
            //anim.enabled = false;
            agent.isStopped = true;
            Destroy(hpUI);
            gameObject.tag = "goblin";
        }

        // Update is called once per frame

        void Update()
        {

            if (agent.isOnOffMeshLink)
            {
                //agent.speed = 1f;
                anim.SetBool("isJumping", true);
            }
            else
            {
                //agent.speed = 3f;
                anim.SetBool("isJumping", false);
            }
                

            if(player.GetComponent<vThirdPersonController>() != null)
            {
                if (player.GetComponent<vThirdPersonController>().currentHP <= 0)
                {
                    agent.isStopped = true;
                    anim.SetBool("isPlayerDead", true);
                }
                   

            }
    
           // anim.speed = agent.velocity.magnitude;

            //if(player.GetComponent<Animator>().)

            if (Vector3.Distance(player.transform.position, transform.position) < attackDistance)
            {
                anim.SetBool("isAttacking", true);
                if (gameObject.name == "shaman")
                    agent.speed = 0.1f;
                
            }
            else
            {
                agent.speed = defaultSpeed;
                anim.SetBool("isAttacking", false);
            }

        }

        public void Damaged(int dmg)
        {
            
            currentHP -= dmg;
            healthBar.SetHealth(currentHP);
            if(currentHP <= 0)
            {
                Die();
            }
            else if(currentHP == 1)
            {
                anim.SetBool("isWounded", true);
                agent.speed /= 2;
            }
        }

        IEnumerator SetTarget()
        {
                yield return new WaitForSeconds(1f);
                agent.SetDestination(target.position);
                StartCoroutine(SetTarget());
            
            

        }

        private void CastFireball()
        {
            GameObject bull = Instantiate(fireball, firePoint.transform.position, firePoint.transform.rotation) as GameObject;

            Rigidbody bullRigidbody = bull.GetComponent<Rigidbody>();
            bullRigidbody.velocity = (player.transform.position + new Vector3(0,0.5f,0) - firePoint.transform.position).normalized * fireballSpeed;
           // Debug.Log("CastFireball");
        }
    }
}
