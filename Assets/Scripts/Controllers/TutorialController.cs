﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject touchscreenMenu;
    public GameObject scoreMenu;

    void Start() {
        if(!GameManager.instance.playerDied_GameRestarted && !GameManager.instance.tutorialDone) {
            startTutorial();
        }
        else {
            tutorialMenu.SetActive(false);
        }
    }

    public void startTutorial() {
        tutorialMenu.SetActive(true);
        touchscreenMenu.SetActive(false);
        GameManager.instance.tutorialDone = true;
    }
  
    public void show3RD_JoystickShootPause() {
        touchscreenMenu.SetActive(true);
        scoreMenu.SetActive(false);
    }

    public void show4TH() {
        touchscreenMenu.SetActive(false);
    }

    public void StartGame() {
        touchscreenMenu.SetActive(true);
        scoreMenu.SetActive(true);
        tutorialMenu.SetActive(false);
    }

}
