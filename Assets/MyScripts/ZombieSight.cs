using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Invector.vCharacterController
{
    public class ZombieSight : MonoBehaviour
    {
        MyAI zombie;
        [SerializeField]
        float distance;

        GameObject player;
        public Transform sightpoint;


        public bool canSee = false;

        [SerializeField]
        float chaseDistence = 5f;

        RaycastHit hit;
        Ray ray;
        Vector3 correction = new Vector3(0, 1f, 0);

        void Start()
        {
            zombie = GetComponent<MyAI>();
            player = GameObject.FindGameObjectWithTag("Player");
        }
        void Update()
        {
            ray = new Ray(sightpoint.position, player.transform.position + correction - sightpoint.position);
            distance = Vector3.Distance(sightpoint.position, player.transform.position);

            if (Physics.Raycast(ray, out hit, chaseDistence))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    canSee = true;
                    Debug.Log("can see");
                    Debug.DrawLine(sightpoint.position, player.transform.position + correction, Color.green);
                    zombie.HasSight();
                }
                else
                {

                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawLine(sightpoint.position, player.transform.position + correction, Color.red);

                }
            }
            else
            {

                Debug.Log("Cannot see");
                Debug.DrawLine(sightpoint.position, player.transform.position + correction, Color.red);
            }



            /*if (!Physics.Raycast(sightpoint.position, player.transform.position, chaseDistence))
            {
                Debug.DrawLine(sightpoint.position, player.transform.position, Color.red);
                CanSee = false;
                Debug.Log("CANT SEE");

            }
            else if (Physics.Raycast(sightpoint.position, player.transform.position, chaseDistence))
            {
                Debug.DrawLine(sightpoint.position, player.transform.position, Color.green);
                CanSee = true;
                Debug.Log("CAN SEE");
            }*/
        }
    }
}

