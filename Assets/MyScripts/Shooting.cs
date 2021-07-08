using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace Invector.vCharacterController
{
    public class Shooting : MonoBehaviour
    {

        public AudioSource audio;
        

        public Camera cam;
        public vThirdPersonController cc;
        public KeyCode shootInput = KeyCode.Mouse0;
        public GameObject bullet;
        public GameObject gun;
        [SerializeField]
        private Transform FirePoint;
     
        [Range(1f, 50f)]
        public float bulletSpeed;

        [Range(0.1f, 2.0f)]
        public float fireRate;

        private float timer;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= fireRate)
            {
                if (cc.isAiming && Input.GetKeyDown(shootInput))
                {
                    /*GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
                    Rigidbody bullRigidbody = bull.GetComponent<Rigidbody>();

                    bullRigidbody.AddForce(Vector3.forward * bulletSpeed);*/

                    timer = 0f;
                    FireGun();

                }
            }
        }


        private void FireGun()
        {
            //Debug.Log("Shoot");
            // Debug.DrawRay(FirePoint.position, FirePoint.forward * 100, Color.red, 3f);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            Vector3 target;

            if (Physics.Raycast(ray, out hit))
            {

                //Debug.Log(hit.collider.gameObject.name);
                target = hit.point;
            }
            else
                target = ray.GetPoint(1000);

            GameObject bull = Instantiate(bullet, FirePoint.position, FirePoint.rotation) as GameObject;

            Rigidbody bullRigidbody = bull.GetComponent<Rigidbody>();
            bullRigidbody.velocity = (target - FirePoint.position).normalized * bulletSpeed;

            audio.Play();
        }
    }

}
