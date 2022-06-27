using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class EnergyController : MonoBehaviour
{
    public static EnergyController Instance;
    public Energy Energy {private set; get;}

    private float currentTime = 0;

    [SerializeField] private Text energyText;

    private void Awake() {
        Debug.Log("Awake the EnergyController");
        Instance = this;
        Energy = new Energy();
        ShowEnergy();
    }

    void Update()
    {
        if (Energy.CurrentEnergy == Energy.defaultEnergy) {
            return;
        }
        if (currentTime >= Energy.energyRefreshentTime) {
            Energy.ChangeCurrentEnergy(Energy.CurrentEnergy + 1);
            currentTime = 0;
            ShowEnergy();
        }
        else {
            currentTime += Time.deltaTime;
        }
    }

    public void ShowEnergy(){
         energyText.text = $"{Energy.defaultEnergy} / {Energy.CurrentEnergy}";
    }
}