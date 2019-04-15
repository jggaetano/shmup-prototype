using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Transform powerUpPanel;
    public Text livesText;
    public Text playerText;
    public Text scoreText;
    public Text hiscoreText;

    static int lives = 2;
    int score;
    static int hiscore;

    int currentPowerUp;
    Color highlightedColor = new Color(0, 1, 0);
    Color normalColor = new Color(1, 1, 1);

    Dictionary<PowerUPType, int> powerUpTracker;

    void Start() {
        currentPowerUp = -1;
        powerUpTracker = new Dictionary<PowerUPType, int>();
        powerUpTracker.Add(PowerUPType.MISSLES, 2);
        powerUpTracker.Add(PowerUPType.EXTRABLASTERS, 4);
        powerUpTracker.Add(PowerUPType.SHIELD, 1);
        score = 0;
        hiscore = PlayerPrefs.GetInt("HIGHSCORE");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PlayerPrefs.SetInt("HIGHSCORE", hiscore);
            Application.Quit();
        }

        if (Input.GetButtonDown("Select") && CanSelect()) {
            PowerUpPlayer();
        }

        livesText.text = lives.ToString();
        scoreText.text = score.ToString("0000000");
        hiscoreText.text = hiscore.ToString("HI  0000000");
    }

    public void AdvancePowerUpChoice() {
        UnHighlightOption();
        currentPowerUp++;
        if (currentPowerUp == powerUpPanel.childCount)
            currentPowerUp = 0;
        HighlightOption();
    }

    void ResetOption() {
        UnHighlightOption();
        currentPowerUp = -1;
    }

    //void AddOption(PowerUPType powerUp) {
    //    for (int i = 0; i < powerUpPanel.childCount; i++) {
    //        string powerUpName = powerUpPanel.GetChild(i).name.ToUpper();
    //        if ((PowerUPType)Enum.Parse(typeof(PowerUPType), powerUpName) == powerUp)
    //            powerUpPanel.GetChild(i).GetComponentInChildren<Text>().gameObject.SetActive(true);
    //    }
    //}

    void RemoveOption(PowerUPType powerUp) {
        for (int i = 0; i < powerUpPanel.childCount; i++) {
            string powerUpName = powerUpPanel.GetChild(i).name.ToUpper();
            if ((PowerUPType)Enum.Parse(typeof(PowerUPType), powerUpName) == powerUp)
                powerUpPanel.GetChild(i).GetComponentInChildren<Text>().gameObject.SetActive(false);
        }
    }

    bool CanSelect() {
        if (currentPowerUp < 0)
            return false;

        string powerUpName = powerUpPanel.GetChild(currentPowerUp).name.ToUpper();
        try {
            PowerUPType powerUp = (PowerUPType)Enum.Parse(typeof(PowerUPType), powerUpName);
            if (powerUpTracker.ContainsKey(powerUp)) {
                if (powerUpTracker[powerUp] == 0)
                    return false;
            }
        }
        catch {
            return false;
        }

        return true;
    }

    void HighlightOption() {
        if (currentPowerUp < 0)
            return;

        powerUpPanel.GetChild(currentPowerUp).GetComponent<Image>().color = highlightedColor;
    }

    void UnHighlightOption() {
        if (currentPowerUp < 0)
            return;

        powerUpPanel.GetChild(currentPowerUp).GetComponent<Image>().color = normalColor;
    }

    void PowerUpPlayer() {

        if (currentPowerUp < 0)
            return;

        string powerUpName = powerUpPanel.GetChild(currentPowerUp).name.ToUpper();
        PowerUPType powerUp = (PowerUPType)Enum.Parse(typeof(PowerUPType), powerUpName);
        switch (powerUp) {
            case PowerUPType.SPEEDUP:
                FindObjectOfType<Player>().speed += 1.5f;
                break;
            case PowerUPType.MISSLES:
                FindObjectOfType<Player>().AddLauncher();
                break;
            case PowerUPType.EXTRABLASTERS:
                FindObjectOfType<Player>().AddBlaster();
                break;
            case PowerUPType.SHIELD:
                FindObjectOfType<Player>().ShieldsUp();
                break;
            default:
                break;
        }

        if (powerUpTracker.ContainsKey(powerUp)) {
            powerUpTracker[powerUp]--;
            if (powerUpTracker[powerUp] == 0)
                RemoveOption(powerUp);
        }

        ResetOption();
    }

    public void AddPoints(int value) {
        score += value;
    }

    public void ShieldDown() {
        if (powerUpTracker.ContainsKey(PowerUPType.SHIELD) == false)
            return;
        powerUpTracker[PowerUPType.SHIELD] = 1;
        powerUpPanel.FindChild("Shield/Text").gameObject.SetActive(true);       
    }


}
