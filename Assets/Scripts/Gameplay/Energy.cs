using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Energy
{
    public int defaultEnergy = 50;
    public int CurrentEnergy {private set; get;}
    public int energyRefreshentTime = 60;
    public int startGameEnergy = 10;
    private int lastPlayTime;

    public Energy() {
       CurrentEnergy = PlayerPrefs.GetInt("energy", defaultEnergy);
       Debug.Log("Current energy before CalculateEnergy -> " + CurrentEnergy);
       CalculateEnergy();
    }

    private void CalculateEnergy() {
        int currentTime = GetCurrentTime();
        LoadLastPlayTime();
        int differenceTime = lastPlayTime - currentTime;
        CurrentEnergy += differenceTime / energyRefreshentTime;
        if (CurrentEnergy > defaultEnergy)
            ChangeCurrentEnergy(defaultEnergy);
        Debug.Log("Current energy After CalculateEnergy -> " + CurrentEnergy);
    }

    public void ChangeCurrentEnergy(int newEnergy) {
        CurrentEnergy = newEnergy;
        PlayerPrefs.SetInt("energy", CurrentEnergy);
    }

    public int GetCurrentTime() {
        return DateTime.Now.Second;
    }

    public void SaveLastPlayTime() {
        PlayerPrefs.SetInt("last_play_time", GetCurrentTime());
    }
    
    public void LoadLastPlayTime() {
       lastPlayTime = PlayerPrefs.GetInt("last_play_time");
    }
}