using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof (Dialogue))]
public class DialogueEditor : Editor {

	static Dialogue d;
	[MenuItem("Dialogue/Create New Dialogue Object")]
	static void CreateDialogue() {
		string path = EditorUtility.SaveFilePanel ("Create New Dialogue Object", "Assets/Dialogue", "newDialogue", "asset");

		if (path == "")
			return;

		path = FileUtil.GetProjectRelativePath (path);

		d = CreateInstance<Dialogue> ();
		AssetDatabase.CreateAsset (d, path);
		AssetDatabase.SaveAssets ();
	}

	void OnEnable () {
		d = (Dialogue)target;
	}

	public override void OnInspectorGUI () {
		//d = (Dialogue)target;
		GUILayout.Label ("Dialogue");

		GUILayout.BeginVertical ();
		for (int i = 0; i < d.speakers.Count; i++) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("x", GUILayout.Width (30)))
				RemoveSpeaker (d.speakers [i]);
			GUILayout.Label ("Name", GUILayout.Width (40));
			d.speakers [i].name = GUILayout.TextArea (d.speakers [i].name, GUILayout.Width (100));

			GUILayout.BeginVertical ();
			for (int j = 0; j < d.speakers [i].lines.Count; j++) {
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("x", GUILayout.Width (30)))
					RemoveLine (d.speakers [i], d.speakers [i].lines[j]);
				GUILayout.Label ("Text", GUILayout.Width (40));
				d.speakers [i].lines [j].text = GUILayout.TextArea (d.speakers [i].lines [j].text, GUILayout.Width (250), GUILayout.MinHeight (80));
				GUILayout.BeginVertical ();
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Letters Per Second", GUILayout.Width (180));
				d.speakers [i].lines [j].lettersPerSecond = EditorGUILayout.FloatField (d.speakers [i].lines [j].lettersPerSecond, GUILayout.Width (30));
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Start at new row?", GUILayout.Width (180));
				d.speakers [i].lines [j].startAtNewLine = EditorGUILayout.Toggle (d.speakers [i].lines [j].startAtNewLine);
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Clear all after this line?", GUILayout.Width (180));
				d.speakers [i].lines [j].clearAllAfterDelay = EditorGUILayout.Toggle (d.speakers [i].lines [j].clearAllAfterDelay);
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Automatically go to next line?", GUILayout.Width (180));
				d.speakers [i].lines [j].automaticallyGoToNExtLine = EditorGUILayout.Toggle (d.speakers [i].lines [j].automaticallyGoToNExtLine);
				GUILayout.EndHorizontal ();
				if(d.speakers [i].lines [j].automaticallyGoToNExtLine) {
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Auto Delay", GUILayout.Width (180));
					d.speakers [i].lines [j].autoDelay = EditorGUILayout.FloatField (d.speakers [i].lines [j].autoDelay, GUILayout.Width (30));
					GUILayout.EndHorizontal ();
				}
				GUILayout.BeginHorizontal ();
				d.speakers [i].lines [j].face = (Sprite)EditorGUILayout.ObjectField (d.speakers [i].lines [j].face, typeof (Sprite), false);
				GUILayout.EndHorizontal ();

				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
			}
			if (GUILayout.Button ("Add Line", GUILayout.Width (180)))
				AddLine (d.speakers [i]);
			
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
			GUILayout.Space (10);
		}
		GUILayout.EndVertical ();

		if (d.speakers.Count <= 1) {
			GUILayout.Label ("Monologue?", GUILayout.Width (180));
			d.monologue = EditorGUILayout.Toggle (d.monologue);
		}

		if (d.monologue == false) {
			d.timesRead = 0;
			d.monologueReactivations = new List<Dialogue.Speaker> ();
		}

		if (d.speakers.Count < 1) {
			if (GUILayout.Button ("Add Speaker"))
				AddSpeaker ();
		} else if (d.speakers.Count >= 1 && !d.monologue || d.speakers.Count < 1 && d.monologue == true) {
			if (GUILayout.Button ("Add Speaker"))
				AddSpeaker ();
		} else if (d.monologue == true && d.speakers.Count == 1) {
			GUILayout.Space (10);
			for (int k = 0; k < d.monologueReactivations.Count; k++) {
				GUILayout.BeginVertical ();

				if (GUILayout.Button ("x", GUILayout.Width (30)))
					RemoveMonologueReactivation (d.monologueReactivations [k]);
				GUILayout.Label ("Reactivation nr " + (k + 1));

				GUILayout.BeginVertical ();
				for (int j = 0; j < d.monologueReactivations [k].lines.Count; j++) {
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button ("x", GUILayout.Width (30)))
						RemoveMonologueLine (d.monologueReactivations [k].lines[j], k);
					GUILayout.Label ("Text", GUILayout.Width (40));
					d.monologueReactivations [k].lines [j].text = GUILayout.TextArea (d.monologueReactivations [k].lines [j].text, GUILayout.Width (250), GUILayout.MinHeight (80));
					GUILayout.BeginVertical ();
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Letters Per Second", GUILayout.Width (180));
					d.monologueReactivations [k].lines [j].lettersPerSecond = EditorGUILayout.FloatField (d.monologueReactivations [k].lines [j].lettersPerSecond, GUILayout.Width (30));
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Start at new row?", GUILayout.Width (180));
					d.monologueReactivations [k].lines [j].startAtNewLine = EditorGUILayout.Toggle (d.monologueReactivations [k].lines [j].startAtNewLine);
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Clear all after this line?", GUILayout.Width (180));
					d.monologueReactivations [k].lines [j].clearAllAfterDelay = EditorGUILayout.Toggle (d.monologueReactivations [k].lines [j].clearAllAfterDelay);
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Automatically go to next line?", GUILayout.Width (180));
					d.monologueReactivations [k].lines [j].automaticallyGoToNExtLine = EditorGUILayout.Toggle (d.monologueReactivations [k].lines [j].automaticallyGoToNExtLine);
					GUILayout.EndHorizontal ();
					if(d.monologueReactivations [k].lines [j].automaticallyGoToNExtLine) {
						GUILayout.BeginHorizontal ();
						GUILayout.Label ("Auto Delay", GUILayout.Width (180));
						d.monologueReactivations [k].lines [j].autoDelay = EditorGUILayout.FloatField (d.monologueReactivations [k].lines [j].autoDelay, GUILayout.Width (30));
						GUILayout.EndHorizontal ();
					}

					GUILayout.EndVertical ();
					GUILayout.EndHorizontal ();
					GUILayout.Space (10);
				}
				if (GUILayout.Button ("Add Monologue Line", GUILayout.Width (180)))
					AddMonologueLine (d.speakers [0], k);

				GUILayout.EndVertical ();

				GUILayout.Space (10);
				GUILayout.EndVertical ();
			}
			if (GUILayout.Button ("Add Next Activation Monologue", GUILayout.Width (200)))
				AddMonologueReactivation ();
		}

		GUILayout.Space (180);
		base.OnInspectorGUI ();
	}

	void AddSpeaker () {
		d.speakers.Add (new Dialogue.Speaker ("", new List<Dialogue.Line> (), d));
	}

	void RemoveSpeaker(Dialogue.Speaker speaker) {
		d.speakers.Remove (speaker);
	}

	void AddMonologueLine(Dialogue.Speaker speaker, int id) {
		d.monologueReactivations [id].lines.Add (new Dialogue.Line("", 25f, false, false, speaker, false, 0f, null));
	}

	void RemoveMonologueLine (Dialogue.Line line, int id) {
		d.monologueReactivations [id].lines.Remove (line);
	}

	void AddLine (Dialogue.Speaker speaker) {
		speaker.lines.Add (new Dialogue.Line("", 25f, false, false, speaker, false, 0f, null));
	}

	void RemoveLine (Dialogue.Speaker speaker, Dialogue.Line line) {
		speaker.lines.Remove (line);
	}

	void AddMonologueReactivation () {
		d.monologueReactivations.Add (new Dialogue.Speaker (d.speakers[0].name, new List<Dialogue.Line> (), d));
	}

	void RemoveMonologueReactivation (Dialogue.Speaker speaker) {
		d.monologueReactivations.Remove (speaker);
	}
}
