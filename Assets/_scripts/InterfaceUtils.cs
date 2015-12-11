using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceUtils : MonoBehaviour {

	public static List<T> GetInterfaces<T>(GameObject objectToSearch) {
		List<T> results = new List<T>();
		foreach(MonoBehaviour mb in objectToSearch.GetComponents<MonoBehaviour>()) {
			if(mb is T) {
				results.Add((T)((System.Object) mb));
			}
		}
		return results;
	}
	
	public static T GetInterface<T>(GameObject objectToSearch) {
		foreach(MonoBehaviour mb in objectToSearch.GetComponents<MonoBehaviour>()) {
			if(mb is T) {
				return (T)((System.Object) mb);
			}
		}
		return default(T);
	}
}
