﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelMapController : MonoBehaviour 
{
    public Animator fstLvlAnim;
    public Animator sndLvlAnim;
    public Animator arrowsAnim;

    public void InitializeStars(int starsCollected) {
        if(starsCollected == 1) {
            GameManager.instance.lvl1Stars = 1;
            fstLvlAnim.SetTrigger("Set1Star");
        }
        if(starsCollected == 2) {
            GameManager.instance.lvl1Stars = 2;
            fstLvlAnim.SetTrigger("Set2Star");
        }
        if(starsCollected == 3) {
            GameManager.instance.lvl1Stars = 3;
            fstLvlAnim.SetTrigger("Set3Star");
        }
        StartCoroutine(UnlockArrow());
    }

    public IEnumerator UnlockArrow()
    {
        yield return new WaitForSeconds(3.5f);
        arrowsAnim.SetTrigger("SetArrow1");
        StartCoroutine(UnlockNextLevel());
    }
    
    public IEnumerator UnlockNextLevel() {
        yield return new WaitForSeconds(1.5f);    
        sndLvlAnim.SetTrigger("UnlockLvl2");
    }

    public void ForestWorldButton() {
        SceneManager.LoadScene("woodland");
    }
}
