using UnityEngine;
using System.Collections;
using System.Xml;

public class Conversation : IEnumerable {

	private XmlNode conversation;

	public Conversation(XmlNode c) {
		conversation = c;
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return (IEnumerator) GetEnumerator();
	}
	
	public ConversationEnum GetEnumerator() {
		return new ConversationEnum(conversation.ChildNodes);
	}
	
	public bool FreezePlayer() {
		XmlAttribute attr = conversation.Attributes["freeze_player"];
		if(attr == null) {
			return false;
		}
		return attr.Value.ToLower() == "true";
	}
}
