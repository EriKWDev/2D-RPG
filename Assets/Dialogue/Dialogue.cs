using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue : ScriptableObject {

	public int timesRead = 0;
	public bool monologue = false;

	public List<Speaker> monologueReactivations;

	public List<Speaker> speakers;

	[System.Serializable]
	public class Speaker{
		public Dialogue dialogue;
		public string name;
		public List<Line> lines;

		public Speaker(string _name, List<Line> _lines, Dialogue _dialogue) {
			name = _name;
			lines = _lines;
			dialogue = _dialogue;
		}
	}

	[System.Serializable]
	public class Line{
		public Speaker speaker;
		public string text;
		public float lettersPerSecond;
		public float autoDelay;
		public bool clearAllAfterDelay;
		public bool startAtNewLine;
		public bool automaticallyGoToNExtLine;
		public Sprite face;

		public Line(string _text, float _lettersPerSecond, bool _clearAllAfterDelay, bool _startAtNewLine, Speaker _speaker, bool _automaticallyGoToNextLine, float _autoDelay, Sprite _face) {
			text = _text;
			lettersPerSecond = _lettersPerSecond;
			clearAllAfterDelay = _clearAllAfterDelay;
			startAtNewLine = _startAtNewLine;
			speaker = _speaker;
			automaticallyGoToNExtLine = _automaticallyGoToNextLine;
			autoDelay = _autoDelay;
			face = _face;
		}
	}
}
