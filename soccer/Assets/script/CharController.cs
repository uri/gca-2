using UnityEngine;
using System.Collections;
 
 
public class CharController : MonoBehaviour {
 
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float gravityForce = 20.0f;
    public CharacterController controller;
    //the direction we are going to move
    private Vector3 dir;
	public playercam camera;
	
	public float swipeLength = 10.0f;
	public float swipeLengthRight = 50.0f;
	
	
	// Locals
	private float startX = -1.0f;
	private float draggingX = -1.0f;
	
	private float startXRight = -1.0f;
	private float draggingXRight = -1.0f;
 
    void Start()
    {
        //load a reference to our character controller component
        controller = GetComponent<CharacterController>();
    }
 
	/** To be used with physicsy stuff
	 */
	void FixedUpdate () {
 
        //is the user pressing left or right on the keyboard?
		Vector3 temp = camera.transform.right;
		float val = swipeHorizontalOnLeft();
        
		dir = new Vector3(val * temp.x, val * temp.y, val * temp.z);
 
        //transform direction to local space
        dir = transform.TransformDirection(dir) * speed;
 
        //apply gravity
        dir.y = controller.velocity.y - gravityForce * Time.deltaTime;
 
        //are we jumping?
        if (controller.isGrounded)
        {
            dir.y = tapOnRight();
        }
 
 
        //move our character
        controller.Move(dir * Time.deltaTime);
		
		// Rotate the camerea
		rotateCamera();
 
 
	}
	
	
	/** JUMP
	 */ 
	float tapOnRight () {
		float touchPosX = -1.0f;
		
		if (Input.touchCount > 1) {
			touchPosX = Input.GetTouch(1).position.x;	
		} 
		
		/*else if (Input.touchCount > 0) {
			touchPosX = Input.GetTouch(0).position.x;	
		}*/
		
		if (touchPosX == -1.0f) {
			return 0.0f;	
		}
		
		// Return a jump
		if (touchPosX > Screen.currentResolution.width / 2) {
			return jumpForce * gravityForce * Time.deltaTime; 
		}
		
		
		return 0.0f;
	}
	
	void rotateCamera() {
		swipeHorizontalOnRight();
	}
	
	void swipeHorizontalOnRight() {
		
		if (!controller.isGrounded) return;
		
		
		
		// Get the start position
		if (Input.touchCount > 0 ) {			
			Touch t = Input.GetTouch(0);	// Touch
			// If there is no touch get the start position
			if (startXRight == -1.0f) {
			
				// If the start position is on the left side of the screen.
				if (Input.GetTouch(0).position.x > Screen.currentResolution.width / 2 ) {
					startXRight = t.position.x;	
				}
			
			
			// We have a start position now
			} else {
				// Get the direction
				draggingXRight = Input.GetTouch(0).position.x;
				
				// Translate
				if ( Mathf.Abs(draggingXRight - startXRight) > swipeLengthRight && startXRight != -1) {
				
					if (draggingXRight - startXRight < -1) {
						
						camera.rotate_world_CCW();	
					} else {
						camera.rotate_world_CW();
					}
				
		
				}
			}
			
		} else {
			draggingXRight = -1.0f;
			startXRight = -1.0f;	
		}
		
	}
	
	
	float swipeHorizontalOnLeft() {
		// Get the start position
		if (Input.touchCount > 0 ) {			
			Touch t = Input.GetTouch(0);	// Touch
			// If there is no touch get the start position
			if (startX == -1.0f) {
			
				// If the start position is on the left side of the screen.
				if (Input.GetTouch(0).position.x < Screen.currentResolution.width / 2 ) {
					startX = t.position.x;	
					Debug.Log("Star posx: " + startX);
				}
			
			
			// We have a start position now
			} else {
				// Get the direction
				draggingX = Input.GetTouch(0).position.x;
				
				// Translate
				if ( Mathf.Abs(draggingX - startX) > swipeLength && startX != -1) {
				
					if (draggingX - startX < -1) {
						Debug.Log("Swiping On LEFT");
						return -1.0f;	
					} else {
						return 1.0f;
					}
				
		
				}
			}
			
		} else {
			draggingX = -1.0f;
			startX = -1.0f;	
		}
		
		return 0;
		
	}
 
	void OnTriggerEnter(Collider col) {
		
	}
 
}