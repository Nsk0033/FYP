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
		public bool abilityF;
		public bool chargedAttack;
		
		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
		private float flt_LeftClickAttackTimer;
		
		private void FixedUpdate()
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
		}

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
		
		public void OnAbilityF(InputValue value)
		{
			AbilityFInput(value.isPressed);
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
		
		public void AbilityFInput(bool newAbilityFState)
		{
			abilityF = newAbilityFState;
		}
		
		public void ChargedAttackInput(bool newChargedAttackState)
		{
			chargedAttack = newChargedAttackState;
			
			Debug.Log("Charged Attack: " + chargedAttack);
		}
		
		
		
		private void LeftClickAttackTimer()
		{
			if (flt_LeftClickAttackTimer >= 0)
			{
				flt_LeftClickAttackTimer -= Time.fixedDeltaTime;
			}
		}
	}
	
}