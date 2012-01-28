using UnityEngine;
using System.Collections;

public class playercam : MonoBehaviour {
	
	//****************GLOBALS****************//
	private Transform parentTransform;
	private Transform childTransform;
	
	//INPUT
	private int startX = -1;
	private int currX = -1;
	public int SwipeLength = 50;
	//INPUT
	
	public int start_distance = 300;
	
	int turning;
	int turn_dir;
	int turn_count;
	public int turn_res =10;
	
	int tilting;
	int tilted;
	int tilt_count;
	public int tilt_res = 10;
	public int tilt_angle = 20;
	
	/*
	int zooming;
	int zoomed;
	int zoom_count;
	public int zoom_res = 5;
	public int zoom_dist = 400;
	 */
	public int frame_interval = 3;
	
	//****************INITIALIZATION****************//
	void Start () {
		parentTransform = transform.parent;
		childTransform = parentTransform.transform;
		
		transform.position = childTransform.position - new Vector3(0, 3, start_distance);
		print("X RESOLUTION: " + Screen.currentResolution.width);
		print("Y RESOLUTION: " + Screen.currentResolution.height);
		turning = 0;
		tilting = 0;
		//zooming = 0;
		turn_count = turn_res*frame_interval;
		tilt_count = tilt_res*frame_interval;
		//zoom_count = zoom_res;
	}
	

	//****************CAMERA FUNCTIONS****************//
	
	/*void zoom_update() {
		if(zooming == 1 && zoomed == 0) {
			if(zoom_count != 0){
				//if (zoom_count%frame_interval == 0)
					//print(this.GetComponent("size"));
			//		transform.Translate(-transform.parent.transform.forward * (zoom_dist/zoom_res));
				zoom_count--;
			}
			else {
				tilting = 1;
				turning = 0;
				zooming = 0;
				zoomed = 1;
				zoom_count = zoom_res;
			}
		}
		else if(zooming == 1 && zoomed == 1) {
			if(zoom_count != 0){
				//if (zoom_count%frame_interval == 0)
			//		transform.Translate(transform.parent.transform.forward * (zoom_dist/zoom_res));
				zoom_count--;
			}
			else {
				//is_rotating = 0;
				tilting = 0;
				turning = 0;
				zooming = 0;
				zoomed = 0;
				zoom_count = zoom_res;
			}
		}
	}
	*/
	void tilt_update() {
		if(tilting == 1 && tilted == 0) {
			if(tilt_count != 0){
				if(tilt_count%frame_interval==0)
					transform.RotateAround(childTransform.position, transform.right, tilt_angle/tilt_res);
				tilt_count--;
			}
			else {
				tilting = 0;
				turning = 1;
				tilted = 1;
				tilt_count = tilt_res*frame_interval;
			}
		}
		else if(tilting == 1 && tilted == 1) {
			if(tilt_count != 0){
				if(tilt_count%frame_interval==0)
					transform.RotateAround(childTransform.position, transform.right, -tilt_angle/tilt_res);
				tilt_count--;
			}
			else {
				//is_rotating = 0;
				turning = 0;
				tilting = 0;
				tilted = 0;
				//zooming = 1;
				tilt_count = tilt_res*frame_interval;
			}
		}
	}
	
	void turn_update() {
		if(turning == 1 && turn_dir == 1) {
			if(turn_count != 0){
				if(turn_count%frame_interval==0)
					transform.RotateAround(childTransform.position, childTransform.up, -90/turn_res);
				turn_count--;
			}
			else {
				turning = 0;
				tilting = 1;
				turn_count = turn_res*frame_interval;
			}
		}
		else if(turning == 1 && turn_dir == 0) {
			if(turn_count != 0){
				if(turn_count%frame_interval==0)
					transform.RotateAround(childTransform.position, childTransform.up, 90/turn_res);
				turn_count--;
			}
			else {
				turning = 0;
				tilting = 1;
				turn_count = turn_res*frame_interval;
			}
		}
	}
	
	void update_camera(){
		//if(is_rotating == 1){
			if(tilting == 1)
				tilt_update();
			else if(turning == 1)
				turn_update();
			//else if(zooming == 1)
			//	zoom_update();
		//}
	}
	
	public void rotate_world_CW(){
		turn_dir = 1;
		tilting = 1;
	}
	
	public void rotate_world_CCW(){
		turn_dir = 0;
		tilting = 1;
	}
	
	
	//****************INPUT****************//
	void check_swipe() {
	
		if (Input.touchCount > 0) {
		
			Touch t = Input.GetTouch(0);
			
			if (t.position.x > Screen.currentResolution.width / 2) {
				if (startX == -1) {
					startX = (int)t.position.x;	
				} else {
					currX = (int)t.position.x;
					Debug.Log("in curr pos");
					if (Mathf.Abs(currX	 - startX) > SwipeLength) {
						// Get the direction	
						if (currX - startX <= -1) {
							rotate_world_CW();
						} else {
							rotate_world_CCW();
						}
					}
					
				}
			}
			
		} else {
			startX = -1;
			currX = -1;
		}
	}
	
	
	//****************MAIN UPDATE****************//
	void Update () {
		
		//check_swipe();
		update_camera();
			
	}
}