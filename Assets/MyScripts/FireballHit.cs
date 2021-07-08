using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vCharacterController
{
    public class FireballHit : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<vThirdPersonController>() != null)
            {
                other.GetComponentInParent<vThirdPersonController>().Damaged(1);
                Destroy(gameObject);
                Debug.Log("hit player");
            }
            
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
