using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class DialogueReader : MonoBehaviour {

	public UnityEngine.UI.Text textBox;
	public UnityEngine.UI.Text nameTextBox;
	public UnityEngine.UI.Image image;
	public UnityEngine.UI.Image panel;
	public AudioClip letterSound;
	public AudioClip defaultSound;
	public bool isInDialogue = false;

	private AudioSource audioSource;

	public void Start() {
		panel.gameObject.SetActive (isInDialogue);
		audioSource = GetComponent<AudioSource> ();
	}

	public void ReadDialogue (Dialogue dialogue) {
		letterSound = defaultSound;
		StartCoroutine (StartDialogue(dialogue));
	}

	public void ReadDialogue (Dialogue dialogue, AudioClip sound) {
		letterSound = sound;
		StartCoroutine (StartDialogue(dialogue));
	}

	public IEnumerator StartDialogue(Dialogue dialogue) {
		textBox.text = "";
		isInDialogue = true;

		panel.gameObject.SetActive (isInDialogue);
		yield return StartCoroutine ("InitiateSpeakers", dialogue.speakers);
		if (dialogue.monologue && dialogue.monologueReactivations.Count > 0 && dialogue.timesRead < dialogue.monologueReactivations.Count) {
			dialogue.timesRead++;
		} else {
			dialogue.timesRead = dialogue.monologueReactivations.Count;
		}

		isInDialogue = false;
		panel.gameObject.SetActive (isInDialogue);
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

		if (speaker.dialogue.timesRead >= 1 && speaker.dialogue.monologue == true && speaker.dialogue.monologueReactivations.Count > 0) {
			foreach (Dialogue.Line line in speaker.dialogue.monologueReactivations[speaker.dialogue.timesRead-1].lines) {
				if (image != null && line.face != null)
					image.sprite = line.face;

				yield return StartCoroutine (SayLine(line, firstLine));
				if (line.clearAllAfterDelay) {
					textBox.text = "";
					firstLine = true;
				} else {
					firstLine = false;
				}
			}
		} else {
			foreach (Dialogue.Line line in speaker.lines) {
				if (image != null) {
					if (line.face != null) {
						image.sprite = line.face;
					} else {
						image.sprite = GetComponent<SpriteRenderer> ().sprite;
					}
				}
					
				
				yield return StartCoroutine (SayLine(line, firstLine));
				if (line.clearAllAfterDelay) {
					textBox.text = "";
					firstLine = true;
				} else {
					firstLine = false;
				}
			}
		}

	}

	public IEnumerator SayLine(Dialogue.Line line, bool firstLine) {
		if (line.startAtNewLine) {
			textBox.text += "\n* ";	
		} else if (firstLine) {
			textBox.text += "* ";
		}

		bool skip = false;

		/*
		float[] delays = new float[line.text.Split (" ".ToCharArray ()[0]).Length];
		for(int i = 0; i < line.text.Split (" ".ToCharArray ()[0]).Length; i++) {
			delays [i] = Random.Range (0f, 100f);
		}

		float sum = 0f;
		foreach (float n in delays) {
			sum += n;	
		}

		float sum2 = 0f;
		for (int i = 0; i < delays.Length; i++) {
			float n = delays [i];
			n /= sum;
			n *= (line.text.ToCharArray ().Length / line.lettersPerSecond);
			sum2 += n;
			delays [i] = n;
		}

		string tmp2 = "";
		foreach (float n in delays) {
			tmp2 += n + ", ";	
		}

		print ("Sum :" + sum + " Sum2: " + sum2 + " Aimed Sum: " + (line.text.ToCharArray ().Length / line.lettersPerSecond) + " Delays: " + tmp2);

		float timer = Time.time;
		int j = 0;
		foreach (string word in line.text.Split (" ".ToCharArray ()[0])) {
			string wordWithSpace = word + " ";
			foreach (char c in wordWithSpace.ToCharArray()) {
				textBox.text += c;
				if (skip == false) {
					if (c.ToString () != " ")
						audioSource.Play ();
					yield return new WaitForSeconds (delays [j] / wordWithSpace.Length);
				} 
			}
			j++;
		}
		textBox.text += " (Time: " + (Time.time - timer) + ", Aimed: " + (line.text.ToCharArray ().Length / line.lettersPerSecond) + ")";

		*/

		// /*

		foreach (string word in line.text.Split (" ".ToCharArray ()[0])) {
			string wordWithSpace = word + " ";

			foreach (char c in wordWithSpace.ToCharArray()) {
				textBox.text += c;
				if (skip == false) {
					if (c.ToString () != " ") {
						audioSource.Play ();
						yield return new WaitForSeconds ((line.text.ToCharArray ().Length / line.lettersPerSecond) / line.text.ToCharArray ().Length);
					}
				} 
			}
		}
		/*
		float secondsPerLetter = (line.text.ToCharArray ().Length / line.lettersPerSecond) / line.text.ToCharArray ().Length;
		foreach (char c in line.text.ToCharArray()) {
			textBox.text += c;
			if (skip == false) {
				if (c.ToString () != " ") {
					audioSource.Play ();
					yield return new WaitForSeconds (secondsPerLetter);
				} else {
					yield return new WaitForSeconds (secondsPerLetter * 5f);
				}
			} 
		}
		// */

		while (!Input.anyKeyDown && !line.automaticallyGoToNExtLine) {
			yield return null;
		}

		if (line.automaticallyGoToNExtLine) {
			yield return new WaitForSeconds (line.autoDelay);
		}
	}
}
