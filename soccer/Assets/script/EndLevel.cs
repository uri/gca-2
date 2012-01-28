using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {
	
	public int NextLevel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Application.LoadLevel(NextLevel);
		}
	}
}
