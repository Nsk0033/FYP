using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI.Example;

namespace EmeraldAI
{
    //This script will automatically be added to player targets. You can customize the DamagePlayerStandard function
    //or create your own. Ensure that it will be called within the SendPlayerDamage function. This allows users to customize
    //how player damage is received and applied without having to modify any main system scripts. The EmeraldComponent can
    //be used for added functionality such as only allowing blocking if the received AI is using the Melee Weapon Type.
    public class EmeraldAIPlayerDamage : MonoBehaviour
    {
        public List<string> ActiveEffects = new List<string>();
        public bool IsDead = false;

        private void Awake()
        {
            if (PlayerHealth.instance == null)
            {
                Debug.LogError("PlayerHealth singleton instance is not found!");
            }
        }

        public void SendPlayerDamage(int DamageAmount, Transform Target, EmeraldAISystem EmeraldComponent, bool CriticalHit = false)
        {
            Debug.Log("Player Get Attacked");
            // The standard damage function that sends damage to the Emerald AI demo player
            DamagePlayerCustom(DamageAmount);

            // Creates damage text on the player's position, if enabled.
            CombatTextSystem.Instance.CreateCombatText(DamageAmount, transform.position, CriticalHit, false, true);
        }

        void DamagePlayerCustom(int DamageAmount)
        {
            Debug.Log("Player Damage Checking");
            if (PlayerHealth.instance != null)
            {
                PlayerHealth.instance.DamagePlayer(DamageAmount);
                Debug.Log("Player Damage Successfully");
                if (PlayerHealth.instance.CurrentHealth <= 0)
                {
                    IsDead = true;
                }
            }
            else
            {
                Debug.LogError("PlayerHealth singleton instance is null in DamagePlayerCustom.");
            }
        }
		
        /*void DamagePlayerStandard(int DamageAmount)
        {
            if (GetComponent<EmeraldAIPlayerHealth>() != null)
            {
                EmeraldAIPlayerHealth PlayerHealth = GetComponent<EmeraldAIPlayerHealth>();
                PlayerHealth.DamagePlayer(DamageAmount);
				Debug.Log("Player Damage Successfully");
                if (PlayerHealth.CurrentHealth <= 0)
                {
                    IsDead = true;
                }
            }
        }*/

        /*
        void DamageRFPS(int DamageAmount, Transform Target)
        {
            if (GetComponent<FPSPlayer>() != null)
            {
                GetComponent<FPSPlayer>().ApplyDamage((float)DamageAmount, Target, true);
            }
        }
        */

        /*
        void DamageInvectorPlayer (int DamageAmount, Transform Target)
        {
            if (GetComponent<Invector.vCharacterController.vCharacter>())
            {
                var PlayerInput = GetComponent<Invector.vCharacterController.vMeleeCombatInput>();

                if (!PlayerInput.blockInput.GetButton())
                {
                    var _Damage = new Invector.vDamage(DamageAmount);
                    _Damage.sender = Target;
                    _Damage.hitPosition = Target.position;
                    GetComponent<Invector.vCharacterController.vCharacter>().TakeDamage(_Damage);
                }
            }
        }
        */

        /*
        void DamageUFPSPlayer(int DamageAmount)
        {
            if (GetComponent<vp_FPPlayerDamageHandler>())
            {
                GetComponent<vp_FPPlayerDamageHandler>().Damage((float)DamageAmount);
            }
        }
        */
    }
}
