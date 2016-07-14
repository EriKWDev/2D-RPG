using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class tmpDialogueEditor : Editor {

	[MenuItem("Dialogue/Create New Dialogue Object")]
	static void CreateDialogue() {
		string path = EditorUtility.SaveFilePanel ("Create New Dialogue Object", "Assets/Dialogue", "new_dialogue", "asset");

		if (path == "")
			return;

		path = FileUtil.GetProjectRelativePath (path);

		Dialogue d = CreateInstance<Dialogue> ();
		AssetDatabase.CreateAsset (d, path);
		AssetDatabase.SaveAssets ();
	}
}
