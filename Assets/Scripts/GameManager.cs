﻿using System.Collections;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{	
// GAME OBJECT VARS
	public GameObject player;
	public GameObject clonePlayer;

	public GameObject deathParticles;
	
// SCREEN SHAKE VARS
	public float shakeIntensityHurt;
	public float shakeDurationHurt;
	
// PLAYER HEALTH VARS
    public int healthValue;
	
// PLAYER AMMO VARS
	public int aoeNode;
	public float shieldNode;
	
// GAME MANAGER VARS
	public static GameManager instance {get; private set;}
	private float resetDelay = 1f;
	public static bool isPlayerWon = false;
	public static bool isPlayerDead = false;
	private bool gameOverCheck = false;
	private float score;
	
// TEXT VARS
	public Text gameOver;
	public Text youWon;
	
	void Start ()
	{
		gameOver.enabled = false;
		youWon.enabled = false;
		GameManagerCheck();
		SetUp();
	}
	
	void Update()
	{
		aoeNode = PlayerController.Instance.aoeNumber;

		shieldNode = PlayerController.Instance.coolDownTimerShield;
		
		if (isPlayerDead == true)
		{
			gameOver.enabled = true;
			Time.timeScale = .25f;
			gameOverCheck = true;
			Invoke("Reset", resetDelay);
		}

		if (isPlayerWon == true)
		{
			youWon.enabled = true;
			Time.timeScale = .25f;
			gameOverCheck = true;
			Invoke("Reset", resetDelay);
		}
	}
	
	void GameManagerCheck() 
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	
	public void SetUp()
	{	
		clonePlayer = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
	}

	public void PlayerHealth()
	{
		healthValue--;
		print("Player has been shot!");
		Camera.main.gameObject.GetComponent<CameraShake>().ShakeScreen(shakeIntensityHurt,shakeDurationHurt);
		
		if (healthValue <= 0)
		{
			print("Player has been killed!");
			Instantiate(deathParticles, clonePlayer.transform.position, Quaternion.identity);
			isPlayerDead = true;
			Destroy(clonePlayer);
		}
	}
	
	void Reset()
	{
		Time.timeScale = 1;
		if (gameOverCheck == true)
		{
			SceneManager.LoadScene(0);
			isPlayerDead = false;
			isPlayerWon = false;
			PlayerScore.playerScore = 0;
			PlayerController.Instance.RefillAmmo();
		}
//		else
//		{
//			SceneManager.LoadScene(index);
//		}
	}

	
}
