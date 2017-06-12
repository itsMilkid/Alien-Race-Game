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
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	[Header("UserInterface Output:")]
	[SerializeField] protected Text currentTime;
	[SerializeField] protected Text speed;
	[SerializeField] protected Text countDown;
	[SerializeField] protected Text roundTime;
	[SerializeField] protected GameObject powerUpIndicator;
	[SerializeField] protected GameObject gameOverPanel;

	
	protected Game game;
	protected PlayerController player;


	private void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		powerUpIndicator.SetActive(false);
		gameOverPanel.SetActive(false);
	}
	private void Update(){
		if(Game.started == false){
			//Countdown before game start
			countDown.text = Mathf.RoundToInt(game.gameStartCountdown).ToString();
			if(game.gameStartCountdown < 1){
				countDown.text = "GO!";
			}
		}
		//currentTime and speed texts
		if(Game.started == true){
			currentTime.text = player.currentTime.ToString();
			speed.text = (Mathf.RoundToInt(player.currentSpeed * 10)).ToString() + " KM/H";

			//Indication wheather a powerup is active or not
			if(player.powerUpActive == true){
				powerUpIndicator.SetActive(true);
			}

		}

		//Activating the game over panel and updating it's text when the game is over.
		if (Game.started == false && player.roundTime > 0){
			speed.gameObject.SetActive(false);
			gameOverPanel.SetActive(true);
			roundTime.text = "Your Time: " + player.roundTime.ToString() + " Sec!";
		}
	}
