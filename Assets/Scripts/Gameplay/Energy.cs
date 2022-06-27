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

    public Energy() {
       CurrentEnergy = PlayerPrefs.GetInt("energy", defaultEnergy);
       Debug.Log("Current energy before CalculateEnergy -> " + CurrentEnergy);
       CalculateEnergy();
    }

    private void CalculateEnergy() {
        int currentTime = TimeController.GetCurrentTime();
        TimeController.LoadLastPlayTime();
        int differenceTime = currentTime - TimeController.LastPlayTime;
        CurrentEnergy += differenceTime / energyRefreshentTime;
        if (CurrentEnergy > defaultEnergy)
            ChangeCurrentEnergy(defaultEnergy);
        Debug.Log("Current energy After CalculateEnergy -> " + CurrentEnergy);
    }

    public void ChangeCurrentEnergy(int newEnergy) {
        CurrentEnergy = newEnergy;
        PlayerPrefs.SetInt("energy", CurrentEnergy);
    }
}