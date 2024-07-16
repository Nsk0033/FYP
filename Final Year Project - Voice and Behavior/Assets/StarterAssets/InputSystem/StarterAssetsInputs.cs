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

        private void Awake()
        {
            skillUnlockManager = FindObjectOfType<SkillUnlockManager>();

            if (skillUnlockManager == null)
            {
                Debug.LogError("SkillUnlockManager not found in the scene.");
            }
        }
		
		/*private void FixedUpdate()
		{
			//LeftClickAttackTimer();
		}
		
		private void OnEnable()
		{
			//_playerControls.Gameplay.Enable();
		}

		private void OnDisable()
		{
			//_playerControls.Gameplay.Disable();
		}*/

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
		
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		
		public void OnChargedAttack(InputValue value)
		{
			ChargedAttackInput(value.isPressed);
		}
		
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
		
		public void OnDodge(InputValue value)
		{
			DodgeInput(value.isPressed);
		}
		
		public void OnQuestLogToggle(InputValue value)
		{
			questLogToggleInput(value.isPressed);
		}
		
		public void OnSkillQ(InputValue value)
		{
			SkillQInput(value.isPressed);
		}
		
		public void OnSkillE(InputValue value)
		{
			SkillEInput(value.isPressed);
		}
		
		public void OnSkill1(InputValue value)
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

        public void OnSkill2(InputValue value)
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

        public void OnSkill3(InputValue value)
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
		
		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}
		
		public void OnMap(InputValue value)
		{
			MapInput(value.isPressed);
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
			/*if (flt_LeftClickAttackTimer <= 0)
			{
				flt_LeftClickAttackTimer = 0.4f;
				return;
			}*/
			newShootState = true;
			shoot = newShootState;
			Debug.Log("Shoot: " + shoot);
		}
		
		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}
		
		public void ChargedAttackInput(bool newChargedAttackState)
		{
			chargedAttack = newChargedAttackState;
			
			Debug.Log("Charged Attack: " + chargedAttack);
		}
		
		public void DodgeInput(bool newDodgeState)
		{
			dodge = newDodgeState;
			
			Debug.Log("Dodge: " + dodge);
		}
		
		public void questLogToggleInput(bool newQuestLogToggleState)
		{
			questLogToggle = newQuestLogToggleState;
			
			Debug.Log("questLogToggle: " + newQuestLogToggleState);
		}
		
		public void SkillEInput(bool newSkillEState)
		{
			skillE = newSkillEState;
			
			Debug.Log("SkillE: " + skillE);
		}
		
		public void SkillQInput(bool newSkillQState)
		{
			skillQ = newSkillQState;
			
			Debug.Log("SkillQ: " + skillQ);
		}
		
		public void Skill1Input(bool newSkill1State)
		{
			skill1 = newSkill1State;
			
			Debug.Log("Skill1: " + skill1);
		}
		
		public void Skill2Input(bool newSkill2State)
		{
			skill2 = newSkill2State;
			
			Debug.Log("Skill2: " + skill2);
		}
		
		public void Skill3Input(bool newSkill3State)
		{
			skill3 = newSkill3State;
			
			Debug.Log("Skill3: " + skill3);
		}
		
		public void PauseInput(bool newPauseState)
		{
			pause = newPauseState;
		}
		
		public void MapInput(bool newMapState)
		{
			map = newMapState;
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