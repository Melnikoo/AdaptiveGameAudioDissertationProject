using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Invector.vCharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        public HPBar healthBar;
        public StaminaBar staminaBar;
        FMOD.Studio.Bus MasterBus;
        public Transform height;
        
       
        [Range(1f, 10f)]
        public int maxHP;
        [Range(1f, 10f)]
        public int maxStamina;

        public int currentHP;
        public int currentStamina;

        private GameObject sword;
        private GameObject shield;

        void Start()
        {
            if (Time.timeScale != 1)
                Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
            height = gameObject.transform;

            sword = GameObject.FindGameObjectWithTag("Sword");
            shield = GameObject.FindGameObjectWithTag("Shield");

            currentHP = maxHP;
            healthBar.SetMaxHealth(maxHP);

            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);

            StartCoroutine(RecoverStamina());

        
        }

        void Update()
        {
            Debug.Log(height.position.y);
        }
  

        IEnumerator RecoverStamina()
        {
            while (true)
            {

                if (currentStamina == maxStamina)
                    yield return null;
                else
                { 
                    yield return new WaitForSeconds(5);
                    currentStamina += 1;
                    staminaBar.SetStamina(currentStamina);
                }
            }
        }

        /* public GameObject bullet;
         public GameObject bulletPos;
         public float bulletSpeed;*/
        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }

            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
            }
        }

        public virtual void Aim (bool value)
        {
            if (value)
            {
                isAiming = true;
              
                freeSpeed.walkSpeed -= 1;
                freeSpeed.runningSpeed -= 3;
                freeSpeed.sprintSpeed -= 5;
                strafeSpeed.walkSpeed -= 1;
                strafeSpeed.runningSpeed -= 3;
                strafeSpeed.sprintSpeed -= 5;

            }
            else if (!value)
            {
                
               if(strafeSpeed.walkSpeed != 2)
                {
                    freeSpeed.walkSpeed += 1;
                    freeSpeed.runningSpeed += 3;
                    freeSpeed.sprintSpeed += 5;
                    strafeSpeed.walkSpeed += 1;
                    strafeSpeed.runningSpeed += 3;
                    strafeSpeed.sprintSpeed += 5;
                }
                
                isAiming = false;
            }
        }


        public virtual void Damaged(int dmg)
        {
            if(isAiming)
            {
                currentStamina -= dmg;
                if (currentStamina > 10)
                    currentStamina = 10;
                staminaBar.SetStamina(currentStamina);
                if (currentStamina == 0)
                    isAiming = false;
            }
            else
            {
                 currentHP -= dmg;
                 healthBar.SetHealth(currentHP);
                 if (currentHP <= 0)
                     Die();
            }
            
        }

        public virtual void Die()
        {
            Debug.Log("Died");
            animator.SetBool("Dead", true);
            GetComponent<vThirdPersonInput>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene(0);

           /* if (isAiming)
                Aim(false);*/
        }

        public virtual void Strafe()
        {
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // trigger jump animations
            if (input.sqrMagnitude < 0.1f)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
        }
    }
}