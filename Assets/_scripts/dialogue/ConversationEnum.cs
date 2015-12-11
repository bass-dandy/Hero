using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class ConversationEnum : IEnumerator {
	
	private XmlNodeList lines;
	int position = -1;

	
	public ConversationEnum(XmlNodeList lines) {
		this.lines = lines;
	}
	
	public bool MoveNext() {
		position++;
		return (position < lines.Count);
	}
	
	public void Reset() {
		position = -1;
	}
	
	object IEnumerator.Current {
		get {
			return Current;
		}
	}
	
	public Line Current {
		get {
			if(position < lines.Count) {
				return new Line(lines[position]);
			} else {
				throw new InvalidOperationException();
			}
		}
	}
}
