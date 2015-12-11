using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Task3Controller : MonoBehaviour {

	// Triggers
	[SerializeField] private EventTrigger arriveAtHome;
	[SerializeField] private EventTrigger enterHome;
	[SerializeField] private EventTrigger approachWife;
	[SerializeField] private EventTrigger exitHome;
	[SerializeField] private EventTrigger fleeScene;
	
	// Actors
	[SerializeField] private PlayerController player;
	[SerializeField] private KillableHuman innocent;
	[SerializeField] private KillableHuman wife;
	[SerializeField] private KillableHuman child;
	[SerializeField] private KillableHuman rivl;
	
	// Text boxes
	[SerializeField] private Text shadowText;
	[SerializeField] private Text innocentText;
	[SerializeField] private Text wifeText;
	[SerializeField] private Text childText;
	[SerializeField] private Text rivlText;
	
	// For fetching dialogue
	[SerializeField] private TextAsset dialogueFile;
	private Dialoguer dialogue;
	
	
	void Start () {
		dialogue = new Dialoguer(dialogueFile);  
		
		// Death events
		innocent.OnDeath += OnInnocentDeath;
		wife.OnDeath     += OnWifeDeath;
		child.OnDeath    += OnChildDeath;
		
		// Trigger events
		arriveAtHome.OnEnter  += OnHouseArrival;
		enterHome.OnEnter     += OnHouseEnter;
		exitHome.OnEnter      += OnHouseExit;
		approachWife.OnEnter  += OnApproachWife;
		fleeScene.OnEnter     += OnFleeScene;
		
		StartCoroutine(BeginConversation("shadow-intro"));
	}

	private IEnumerator BeginConversation(string id) {
		Conversation convo = dialogue.GetConversation(id);
		if(convo.FreezePlayer()) {
			player.Freeze();
		}
		foreach(Line line in convo) {
			Text speakerText;
			switch(line.Speaker().ToLower()) {
				case "shadow":
					speakerText = shadowText;
					break;
				case "innocent":
					speakerText = innocentText;
					break;
				case "wife":
					speakerText = wifeText;
					break;
				case "child":
					speakerText = childText;
					break;
				case "rivl":
					speakerText = rivlText;
					break;
				default: // shouldn't happen unless there's an XML error
					speakerText = shadowText;
					break;
			}
			if(speakerText != null) {
				speakerText.text = line.Text();
			}
			yield return new WaitForSeconds(line.Duration());
			
			// If text has been modified by another convo, cancel this one
			if(speakerText.text != line.Text()) {
				break;
			}
			if(speakerText != null) {
				speakerText.text = "";
			}
		}
		if(convo.FreezePlayer()) {
			player.Thaw();
		}
	}

	/******************************************************/
	/* Event callbacks ************************************/
	/******************************************************/

	void OnHouseArrival() {
		StartCoroutine(BeginConversation("shadow-arrival"));
		player.Cripple();
	}
	
	void OnHouseEnter() {
		StartCoroutine(BeginConversation("innocent-plea"));
	}

	void OnInnocentDeath() {
		wife.MoveX(0.5f, 3.0f);
		StartCoroutine(BeginConversation("shadow-kill"));
		exitHome.gameObject.SetActive(true);
		approachWife.gameObject.SetActive(true);
	}
	
	void OnWifeDeath() {
		StartCoroutine(BeginConversation("shadow-killed"));
	}
	
	void OnHouseExit() {
		child.gameObject.SetActive(true);
		StartCoroutine(BeginConversation("child"));
	}
	
	void OnApproachWife() {
		wife.MoveX(2.5f, 1.5f);
		StartCoroutine(BeginConversation("wife-warning"));
	}
	
	void OnChildDeath() {
		StartCoroutine(BeginConversation("shadow-found"));
		fleeScene.gameObject.SetActive(true);
		wife.OnDeath -= OnWifeDeath;
	}
	
	void OnFleeScene() {
		StartCoroutine(RivlConversation());
	}
	
	private IEnumerator RivlConversation() {
		rivl.gameObject.SetActive(true);
		
		rivl.MoveX(4.0f, 1.0f);
		
		IEnumerator convo = BeginConversation("rivl");
		while(convo.MoveNext()) {
			yield return convo.Current;
		}
		rivl.MoveX(-4.0f, 1.0f);
		
		convo = BeginConversation("shadow-end");
		while(convo.MoveNext()) {
			yield return convo.Current;
		}
		Application.LoadLevel(0);
	}
}
