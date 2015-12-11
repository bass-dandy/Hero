using UnityEngine;
using System.Collections;
using System.Xml;

public class Dialoguer {

	private XmlDocument dialogue;

	public Dialoguer(TextAsset file) {
		dialogue = new XmlDocument();
		dialogue.LoadXml(file.text);
	}
	
	public Conversation GetConversation(string id) {
		return new Conversation(dialogue.DocumentElement.SelectSingleNode("//conversation[@id='" + id + "']"));
		//.doc.DocumentElement.SelectSingleNode("//UserList[@id='local']"));
	}
}
