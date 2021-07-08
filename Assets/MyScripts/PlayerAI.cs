using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    Vector3 target;
    NavMeshAgent agent;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
 
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    public void Die()
    {
        anim.SetBool("Dead", true);
        StartCoroutine(wait());
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.SetDestination(hit.point);
            }
      }
        anim.SetFloat("forward", agent.velocity.magnitude);
      if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
            
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }

}


