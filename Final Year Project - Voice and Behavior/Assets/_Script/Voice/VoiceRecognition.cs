using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace StarterAssets
{
	public class VoiceRecognition : MonoBehaviour
	{
		private KeywordRecognizer keywordRecognizer;
		private Dictionary<string, System.Action> actions = new Dictionary<string, System.Action>();
		
		private StarterAssetsInputs starterAssetsInputs;
		private ThirdPersonShooterController thirdPersonShooterController;
		
		
		private void Awake()
		{
			
			
			starterAssetsInputs = GetComponent<StarterAssetsInputs>();
			thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
			
		}
		
		void Start()
		{
			//actions.Add("forward", Forward);
			
			/*
			actions.Add("lift", Up);
			actions.Add("action", Up);
			
			
			actions.Add("down", Down);
			actions.Add("don't", Down);
			actions.Add("dow", Down);
			actions.Add("done", Down);
			actions.Add("dal", Down);
			actions.Add("da", Down);
			actions.Add("skill", Down);
			
			
			actions.Add("back", Back);
			actions.Add("item", Back);*/
			
			actions.Add("attack", MeleeAttack);
			actions.Add("shoot", MeleeAttack);
			actions.Add("charge", ChargedAttack);
			actions.Add("charged", ChargedAttack);
			actions.Add("heavy", ChargedAttack);
			actions.Add("jump", Jump);
			actions.Add("up", Jump);
			
			actions.Add("interact", Interact);
			actions.Add("hi", Interact);
			actions.Add("hello", Interact);
			actions.Add("talk", Interact);
			actions.Add("next", Interact);
			actions.Add("hey", Interact);
			
			actions.Add("limit", Limit);
			actions.Add("ultimate", Limit);
			actions.Add("ulti", Limit);
			actions.Add("q", Limit);
			
			actions.Add("e", SkillE);
			actions.Add("skill", SkillE);
			actions.Add("ability", SkillE);
			actions.Add("scale", SkillE);
			actions.Add("kill", SkillE);
			
			actions.Add("one", Skill1);
			actions.Add("fire", Skill1);
			actions.Add("flame", Skill1);
			actions.Add("blaze", Skill1);
			actions.Add("inferno", Skill1);
			actions.Add("burn", Skill1);
			
			actions.Add("two", Skill2);
			actions.Add("ice", Skill2);
			actions.Add("blizzard", Skill2);
			actions.Add("cold", Skill2);
			actions.Add("frost", Skill2);
			actions.Add("frozen", Skill2);
			
			actions.Add("three", Skill3);
			actions.Add("thunder", Skill3);
			actions.Add("lightning", Skill3);
			actions.Add("flash", Skill3);
			actions.Add("spark", Skill3);
			actions.Add("electrical", Skill3);
			actions.Add("electric", Skill3);
			
			actions.Add("dodge", Dodge);
			actions.Add("dog", Dodge);
			actions.Add("leap", Dodge);
			actions.Add("evade", Dodge);
			actions.Add("avoid", Dodge);
			actions.Add("lunge", Dodge);
			
			actions.Add("sword", Sword);
			actions.Add("word", Sword);
			actions.Add("sort", Sword);
			actions.Add("sot", Sword);
			actions.Add("knife", Sword);
			actions.Add("bow", Bow);
			actions.Add("boom", Bow);
			actions.Add("bowl", Bow);
			actions.Add("arrow", Bow);
			actions.Add("axe", Axe);
			actions.Add("ex", Axe);
			
			// Display the contents of the actions dictionary
			Debug.Log("Actions dictionary contents:");
			foreach (var kvp in actions)
			{
				Debug.Log($"Key: {kvp.Key}, Action: {kvp.Value.Method.Name}");
			}
			
			keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
			keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
			keywordRecognizer.Start();
		}
		
		private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
		{
			string[] words = speech.text.Split(' ');

			Debug.Log("Recognized Words:");
			foreach (string word in words)
			{
				Debug.Log($"Word: {word}");
			}

			foreach (string word in words)
			{
				if (actions.ContainsKey(word))
				{
					actions[word].Invoke();
				}
			}
		}

		
		private void Forward()
		{
			Debug.Log("Forward");
		}
		
		private void Back()
		{
			Debug.Log("Back");
		}
		
		private void Jump()
		{
			Debug.Log("Jump");
			starterAssetsInputs.JumpInput(true);
			
		}
		
		private void MeleeAttack()
		{
			Debug.Log("Melee");
			starterAssetsInputs.ShootInput(true);
		}
		
		private void ChargedAttack()
		{
			Debug.Log("ChargedAttack");
			starterAssetsInputs.ChargedAttackInput(true);
		}
		
		private void Interact()
		{
			Debug.Log("Interact");
			starterAssetsInputs.InteractInput(true);
		}
		
		private void Limit()
		{
			Debug.Log("Limit");
			starterAssetsInputs.SkillQInput(true);
		}
		
		private void SkillE()
		{
			Debug.Log("E");
			starterAssetsInputs.SkillEInput(true);
		}
		
		private void Skill1()
		{
			Debug.Log("1");
			starterAssetsInputs.Skill1Input(true);
		}
		
		private void Skill2()
		{
			Debug.Log("2");
			starterAssetsInputs.Skill2Input(true);
		}
		
		private void Skill3()
		{
			Debug.Log("3");
			starterAssetsInputs.Skill3Input(true);
		}
		
		private void Dodge()
		{
			Debug.Log("Dodge");
			starterAssetsInputs.DodgeInput(true);
		}
		
		private void Sword()
		{
			thirdPersonShooterController.CurrentWeaponIndex = 1;
		}
		
		private void Bow()
		{
			thirdPersonShooterController.CurrentWeaponIndex = 2;
		}
		
		private void Axe()
		{
			thirdPersonShooterController.CurrentWeaponIndex = 3;
		}
		
	}
}
