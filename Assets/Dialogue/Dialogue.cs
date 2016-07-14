using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue : ScriptableObject {

	public List<Speaker> speakers;

	[System.Serializable]
	public class Speaker{
		public string name;
		public List<Line> lines;

		public Speaker(string _name, List<Line> _lines) {
			name = _name;
			lines = _lines;
		}
	}

	[System.Serializable]
	public class Line{
		public string text;
		public float lettersPerSecond;
		public float delay;
		public bool clearAllAfterDelay;
		public bool startAtNewLine;

		public Line(string _text, float _lettersPerSecond, float _delay, bool _clearAllAfterDelay, bool _startAtNewLine) {
			text = _text;
			lettersPerSecond = _lettersPerSecond;
			delay = _delay;
			clearAllAfterDelay = _clearAllAfterDelay;
			startAtNewLine = _startAtNewLine;
		}
	}
}
