using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool aim;
        public bool shoot;
        public bool interact;
        public bool chargedAttack;
        public bool dodge;
        public bool questLogToggle;
        public bool skillQ;
        public bool skillE;
        public bool skill1;
        public bool skill2;
        public bool skill3;
        public bool pause;
        public bool map;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private float flt_LeftClickAttackTimer;
        private SkillUnlockManager skillUnlockManager;
        private ThirdPersonController thirdPersonController;

        private void Awake()
        {
            skillUnlockManager = FindObjectOfType<SkillUnlockManager>();
            thirdPersonController = GetComponent<ThirdPersonController>();

            if (skillUnlockManager == null)
            {
                Debug.LogError("SkillUnlockManager not found in the scene.");
            }

            if (thirdPersonController == null)
            {
                Debug.LogError("ThirdPersonController instance not found.");
            }
        }

        private void FixedUpdate()
        {
            //LeftClickAttackTimer();
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                MoveInput(value.Get<Vector2>());
            }
        }

        public void OnLook(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying && cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                JumpInput(value.isPressed);
            }
        }

        public void OnSprint(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                SprintInput(value.isPressed);
            }
        }

        public void OnAim(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                AimInput(value.isPressed);
            }
        }

        public void OnShoot(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                ShootInput(value.isPressed);
            }
        }

        public void OnChargedAttack(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                ChargedAttackInput(value.isPressed);
            }
        }

        public void OnInteract(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                InteractInput(value.isPressed);
            }
        }

        public void OnDodge(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                DodgeInput(value.isPressed);
            }
        }

        public void OnQuestLogToggle(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                questLogToggleInput(value.isPressed);
            }
        }

        public void OnSkillQ(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                SkillQInput(value.isPressed);
            }
        }

        public void OnSkillE(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                SkillEInput(value.isPressed);
            }
        }

        public void OnSkill1(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                if (skillUnlockManager.isSkill1Unlocked)
                {
                    Skill1Input(value.isPressed);
                }
                else
                {
                    Debug.Log("Skill 1 is not unlocked yet.");
                }
            }
        }

        public void OnSkill2(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                if (skillUnlockManager.isSkill2Unlocked)
                {
                    Skill2Input(value.isPressed);
                }
                else
                {
                    Debug.Log("Skill 2 is not unlocked yet.");
                }
            }
        }

        public void OnSkill3(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                if (skillUnlockManager.isSkill3Unlocked)
                {
                    Skill3Input(value.isPressed);
                }
                else
                {
                    Debug.Log("Skill 3 is not unlocked yet.");
                }
            }
        }

        public void OnPause(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                PauseInput(value.isPressed);
            }
        }

        public void OnMap(InputValue value)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                MapInput(value.isPressed);
            }
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void ShootInput(bool newShootState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                newShootState = true;
                shoot = newShootState;
                Debug.Log("Shoot: " + shoot);
            }
        }

        public void InteractInput(bool newInteractState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                interact = newInteractState;
            }
        }

        public void ChargedAttackInput(bool newChargedAttackState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                chargedAttack = newChargedAttackState;
                Debug.Log("Charged Attack: " + chargedAttack);
            }
        }

        public void DodgeInput(bool newDodgeState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                dodge = newDodgeState;
                Debug.Log("Dodge: " + dodge);
            }
        }

        public void questLogToggleInput(bool newQuestLogToggleState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                questLogToggle = newQuestLogToggleState;
                Debug.Log("questLogToggle: " + newQuestLogToggleState);
            }
        }

        public void SkillEInput(bool newSkillEState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                skillE = newSkillEState;
                Debug.Log("SkillE: " + skillE);
            }
        }

        public void SkillQInput(bool newSkillQState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                skillQ = newSkillQState;
                Debug.Log("SkillQ: " + skillQ);
            }
        }

        public void Skill1Input(bool newSkill1State)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                skill1 = newSkill1State;
                Debug.Log("Skill1: " + skill1);
            }
        }

        public void Skill2Input(bool newSkill2State)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                skill2 = newSkill2State;
                Debug.Log("Skill2: " + skill2);
            }
        }

        public void Skill3Input(bool newSkill3State)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                skill3 = newSkill3State;
                Debug.Log("Skill3: " + skill3);
            }
        }

        public void PauseInput(bool newPauseState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                pause = newPauseState;
            }
        }

        public void MapInput(bool newMapState)
        {
            if (!thirdPersonController.isDyingPlaying)
            {
                map = newMapState;
            }
        }

        private void LeftClickAttackTimer()
        {
            if (flt_LeftClickAttackTimer >= 0)
            {
                flt_LeftClickAttackTimer -= Time.fixedDeltaTime;
            }
        }

        public void ToggleMenuOnOff(bool isMenuOpen)
        {
            cursorLocked = !isMenuOpen;
            SetCursorState(cursorLocked);
        }

        public void ToggleMenu()
        {
            cursorLocked = !cursorLocked;
            SetCursorState(cursorLocked);
        }
    }
}
