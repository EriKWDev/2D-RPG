using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class dialogueReader : MonoBehaviour {

	public Dialogue theDialogue;
	public UnityEngine.UI.Text textBox;
	public UnityEngine.UI.Text nameTextBox;
	public AudioClip letterSound;

	private AudioSource audioSource;

	public void Start() {
		audioSource = GetComponent<AudioSource> ();
		StartCoroutine (StartDialogue(theDialogue));
	}

	public IEnumerator StartDialogue(Dialogue dialogue) {
		textBox.text = "";
		yield return StartCoroutine ("InitiateSpeakers", dialogue.speakers);
		print ("Done");
	}

	public IEnumerator InitiateSpeakers(List<Dialogue.Speaker> speakers) {
		foreach (Dialogue.Speaker speaker in speakers) {
			yield return StartCoroutine ("InitiateSpeaker", speaker);
		}
	}

	public IEnumerator InitiateSpeaker(Dialogue.Speaker speaker) {
		if(nameTextBox != null)
			nameTextBox.text = speaker.name;
		audioSource.clip = letterSound;

		bool firstLine = true;
		foreach (Dialogue.Line line in speaker.lines) {
			yield return StartCoroutine (SayLine(line, firstLine));
			firstLine = false;
		}
	}

	public IEnumerator SayLine(Dialogue.Line line, bool firstLine) {
		if (line.startAtNewLine) {
			textBox.text += "\n* ";	
		} else if (firstLine) {
			textBox.text += "* ";
		}

		foreach (char c in line.text.ToCharArray()) {
			textBox.text += c;
			audioSource.Play ();
			yield return new WaitForSeconds ((line.text.ToCharArray ().Length / line.lettersPerSecond) / line.text.ToCharArray ().Length);
		}

		yield return new WaitForSeconds (line.delay);

		if (line.clearAllAfterDelay) {
			textBox.text = "";
		}
	}
}
