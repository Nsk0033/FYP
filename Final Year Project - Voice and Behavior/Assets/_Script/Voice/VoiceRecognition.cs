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
		
		
		private void Awake()
		{
			
			
			starterAssetsInputs = GetComponent<StarterAssetsInputs>();
			
			
		}
		
		void Start()
		{
			actions.Add("forward", Forward);
			
			actions.Add("up", Up);
			actions.Add("lift", Up);
			actions.Add("action", Up);
			actions.Add("jump", Up);
			
			actions.Add("down", Down);
			actions.Add("don't", Down);
			actions.Add("dow", Down);
			actions.Add("done", Down);
			actions.Add("dal", Down);
			actions.Add("da", Down);
			actions.Add("skill", Down);
			actions.Add("attack", Down);
			
			actions.Add("back", Back);
			actions.Add("item", Back);
			
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
		
		private void Up()
		{
			Debug.Log("Up");
			starterAssetsInputs.JumpInput(true);
			
		}
		
		private void Down()
		{
			Debug.Log("Down");
			starterAssetsInputs.ShootInput(true);
		}
	}
}
