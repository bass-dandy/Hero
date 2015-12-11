using UnityEngine;
using System.Collections;
using System.Xml;

public class Line {

	private const float DEFAULT_DURATION = 2.0f;
	private XmlNode line;

	public Line(XmlNode n) {
		line = n;
	}
	
	public string Text() {
		return line.InnerText;
	}
	
	public string Speaker() {
		return line.Attributes["speaker"].Value;
	}
	
	public float Duration() {
		if(line.Attributes["duration"] == null) {
			return DEFAULT_DURATION;
		}
		return float.Parse(line.Attributes["duration"].Value);
	}
}
