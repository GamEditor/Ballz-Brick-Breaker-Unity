using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static Coins Instance;
    public int m_Coins;
    public Text m_CoinsText;
    

    private void Awake() {
        Debug.Log("Awake the COINS");
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        Debug.Log("Start Coins");
        m_Coins = PlayerPrefs.GetInt("coins");
        m_CoinsText.text = m_Coins.ToString();
    }

    public void UpdateCoins() {
        Debug.Log("Update coins to coins in PlayerPrefs");
          //  m_CoinsAfterGame += m_Coins;
            m_CoinsText.text = m_Coins.ToString();
            PlayerPrefs.SetInt("coins", m_Coins);
    }
    
    // Add coin and UpdateCoins
    public void AddCoin() {
        m_Coins++;
        Debug.Log("+ 1 Coin, total = "+ m_Coins);
        m_CoinsText.text = m_Coins.ToString();
        UpdateCoins();
    }

    public bool IsPurchisable(int price) { // checks if coins are enough to buy something
            return (m_Coins >= price);
    }
}