using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Invector.vCharacterController
{


    public class DetectoPlayerAtHeight : MonoBehaviour
    {
        NavMeshAgent agent;
        GameObject player;
        vThirdPersonController controller;
        public float chaseHeight;
        bool beenStopped = false;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            controller = player.GetComponent<vThirdPersonController>();
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(controller.height.position.y <= chaseHeight && !beenStopped)
            {
                agent.isStopped = false;
                beenStopped = true;
            }
        }

       
    }
}
