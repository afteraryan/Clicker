using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text LeadboardText;
    [SerializeField] Button button;
    [SerializeField] Button btn_LeaderBoard;
    [SerializeField] Button btn_Restart;
    [SerializeField] float maxTime = 15f;
    public PlayfabManager PlayfabManager;
    int scoreAmount = 0;
    float timerLeft;

    private void Awake()
    {
        timerLeft = maxTime;
        gameOverText.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        btn_LeaderBoard.gameObject.SetActive(false);
        btn_Restart.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (timerLeft <= 0f)
        {
            return;
        }

        timerLeft -= Time.deltaTime;

        if (timerLeft <= 0f)
        {
            timerLeft = 0f;
            gameOverText.gameObject.SetActive(true);
            button.gameObject.SetActive(false);
            btn_LeaderBoard.gameObject.SetActive(true);
            btn_Restart.gameObject.SetActive(true);
            PlayfabManager.SendLeaderboard(scoreAmount);
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = timerLeft < 0 ? "Time: " + timerLeft.ToString("F1") : "Time: " + Mathf.CeilToInt(timerLeft).ToString("F0");
    }

    public void OnButtonClick()
    {
        scoreAmount++;
        UpdateText();
    }

    private void UpdateText()
    {
        score.text = "Score: " + scoreAmount.ToString();
    }

    public void GetLeaderBoard()
    {
        //PlayfabManager.SendLeaderboard(scoreAmount);
        PlayfabManager.GetLeaderboardRequest();
        LeadboardText.gameObject.SetActive(true);
        btn_LeaderBoard.gameObject.SetActive(false);
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
