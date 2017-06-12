#region Copyright
//MIT License
//Copyright (c) 2017 , Milkid - Kristin Stock 

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Movement:")]
	[SerializeField] protected bool enableMobileControls;
	[SerializeField] protected float acceleration;
	[SerializeField] protected float deceleration;
	[SerializeField] protected float regularSpeed;
	[SerializeField] protected float slowedSpeed;

	[Header("PowerUp")]
	[SerializeField] protected float powerUpSpeed;
	[SerializeField] protected float powerUpTime;

	[Header("Wheels:")]
	[SerializeField] protected Transform wheelA;
	[SerializeField] protected Transform wheelB;
	[SerializeField] protected Transform wheelC;
	[SerializeField] protected Transform wheelD;

	[HideInInspector] public float currentSpeed;
	[HideInInspector] public float currentTime;
	[HideInInspector] public float roundTime;
	[HideInInspector] public bool powerUpActive;
	[HideInInspector] public bool roundStarted;

	protected float maxSpeed;

	protected RaycastHit hitInfo;

	protected bool gameStarted;

	protected float powerUpTimer;

	private void Start(){
		powerUpTimer = powerUpTime;
		currentTime = 0.0f;
	}

	private void Update(){
		if(gameStarted == true && roundStarted == true){
			currentTime += Time.deltaTime;
		}

		if(roundStarted == false){
			roundTime = currentTime;
		}

		if(powerUpActive == true){
			maxSpeed = powerUpSpeed;
		}
	}

	private void FixedUpdate(){
		if(Game.started == true){
			ForwardMovement();

			if(enableMobileControls == true){
				SteeringMobile();	
			} else {
				SteeringPC();
			}
		}
	}

	//Forward Movement suitable for PC and Mobile devices. Will start executing 
	//when game/round is started.
	private void ForwardMovement(){
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

		if(currentSpeed < maxSpeed){
			currentSpeed += acceleration * Time.deltaTime;
		} else if (currentSpeed > maxSpeed) {
			currentSpeed -= deceleration * Time.deltaTime;
		}
	}

	//Steering for mobile devices using the accelerator input.
	private void SteeringMobile(){
		if(Mathf.Abs(Input.acceleration.x) > 0.05f){
			transform.Rotate(new Vector3(0,Input.acceleration.x * 1.2f,0));
		} else {
			transform.Rotate(Vector3.zero);
		}
	}

	//Steering for PC using the arrow or "a" and "d" keys.
	private void SteeringPC(){
		if(Input.GetAxis("Horizontal") != 0){
			transform.Rotate(0,Input.GetAxis("Horizontal"),0);
		}
	}

	//Checking on which ground the Player currently is to adjust the speed accordingly.
	private void GroundCheck(){
		if(powerUpActive == false){
			if(Physics.Raycast(wheelA.position, Vector3.down, out hitInfo)){
				if(hitInfo.collider.gameObject.tag == "Road" ){
					maxSpeed = regularSpeed;
				} else {
					maxSpeed = slowedSpeed;
				}
			}

			if(Physics.Raycast(wheelB.position, Vector3.down, out hitInfo)){
				if(hitInfo.collider.gameObject.tag == "Road" ){
					maxSpeed = regularSpeed;
				} else {
					maxSpeed = slowedSpeed;
				}
			}

			if(Physics.Raycast(wheelC.position, Vector3.down, out hitInfo)){
				if(hitInfo.collider.gameObject.tag == "Road"){
					maxSpeed = regularSpeed;
				} else {
					maxSpeed = slowedSpeed;
				}
			}

			if(Physics.Raycast(wheelD.position, Vector3.down, out hitInfo)){
				if(hitInfo.collider.gameObject.tag == "Road"){
					maxSpeed = regularSpeed;
				} else {
					maxSpeed = slowedSpeed;
				}
			}
		}
	}

	private void RunPowerUpTimer(){
		powerUpTimer -= Time.deltaTime;

		if(powerUpTimer < 0){
			powerUpTimer = 0;
		}

		if(powerUpTimer > 0){
			return;
		}

		if(powerUpTimer == 0){
			powerUpTimer = powerUpTime;
			powerUpActive = false;
		}
	}
}
