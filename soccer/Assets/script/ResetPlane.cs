using UnityEngine;
using System.Collections;

public class ResetPlane : MonoBehaviour {
	
	public float DefPosX;
	public float DefPosY;
	public float DefPosZ;
	
	public CharController player;
	
	void OnTriggerEnter(Collider col) {
		if (col.tag	== "Player") {
			player.transform.position = new Vector3(DefPosX, DefPosY, DefPosZ);
		}
	}
}
