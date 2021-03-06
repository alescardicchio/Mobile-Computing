﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    private Text scoreText;
    private Text lifeText;
    public GameObject GameOverUI;
    public GameObject TouchscreenUI;
    public GameObject Player;

    void Awake() {
        makeInstance();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
    }
    
    void OnEnable() {
        SceneManager.sceneLoaded += levelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= levelFinishedLoading;
    }

    void levelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if(!GameManager.instance.playerDied_GameRestarted && scene.name == "gameLvl1") { 
            // E' il primo avvio del gioco e ci troviamo nel livello iniziale, il giocatore avrà 0 diamanti e 3 vite.
            GameManager.instance.score = 0;
            GameManager.instance.lifeScore = 3;
        }
        scoreText.text = GameManager.instance.score.ToString();
        lifeText.text = GameManager.instance.lifeScore.ToString();
    }
    
    private void makeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    public void incrementScore() {
        GameManager.instance.score++;
        scoreText.text = GameManager.instance.score.ToString();
    }

    public void decrementLife() {
        GameManager.instance.lifeScore--;
        if (GameManager.instance.lifeScore >= 0) {
            lifeText.text = GameManager.instance.lifeScore.ToString();
        }
        StartCoroutine(playerDied());
    }

    IEnumerator playerDied() {
        yield return new WaitForSeconds(.96f);
        // Non abbiamo più vite, game over :
        if(GameManager.instance.lifeScore == 0) {
            FindObjectOfType<AudioManager>().StopPlaying("MusicaSottofondo");
            FindObjectOfType<AudioManager>().Play("GameOver");
            GameOverUI.SetActive(true);
            Player.SetActive(false);
            TouchscreenUI.SetActive(false);
            GameObject.Find("Scores").SetActive(false);
        } else {
            // Il player e' morto ma ha comunque delle vite rimanenti :
            GameManager.instance.playerDied_GameRestarted = true;
            GameManager.instance.score = 0;
            GameManager.instance.spiderScore = 0;
            GameManager.instance.trollScore = 0;
            GameManager.instance.winterScore = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }
    }
}
