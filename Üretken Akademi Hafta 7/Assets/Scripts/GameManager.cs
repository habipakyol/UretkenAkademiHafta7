using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform startingPoint;

    public float lerpSpeed;
    public float highScore = 0;

    PlayerController playerControllerScript;

    public float score = 0;
    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI HighScoreText;

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        TotalScore.text = "SCORE: " + score;
        HighScoreText.text = "HIGHSCORE: " + highScore;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScript.gameover = false;
        StartCoroutine(PlayIntro());
    }

    void Update()
    {

        HighScoreText.text = "High Score: " + highScore.ToString();
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
        playerControllerScript.gameover = false;
    }

    public void AddScore(int value)
    {
        score += value;
        TotalScore.text = "SCORE: " + score;
        if (score >= highScore)
        {
            highScore = score;
            HighScoreText.text = "HIGHSCORE: " + highScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        else
        {
            HighScoreText.text = "HIGHSCORE: " + highScore;
        }

    }
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        playerControllerScript.gameover = false;
        playerControllerScript.doubleJumpUsed = false;
        playerControllerScript.isGrounded = true;
        playerControllerScript.JumpControl();
        playerControllerScript.restartGames.SetActive(false);

    }
}
