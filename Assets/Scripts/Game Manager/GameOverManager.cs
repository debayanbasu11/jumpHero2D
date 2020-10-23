using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    private GameObject gameOverPanel;
    private Animator gameOverAnim;

    private Button playAgainBtn, backBtn;

    private GameObject scoreText;
    private Text finalScore;

    void Awake(){
        MakeInstance();
        IniitalizeVariables();
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    public void GameOverShowPanel(){
        scoreText.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScore.text = "Score\n" + "" + ScoreManager.instance.GetScore();
        gameOverAnim.Play("FadeIn");
    }
    
    void IniitalizeVariables()
    {
          gameOverPanel = GameObject.Find("Game Over Panel Holder") as GameObject;
          gameOverAnim = gameOverPanel.GetComponent<Animator>();

          playAgainBtn = GameObject.Find("Play Again Button").GetComponent<Button>();
          backBtn = GameObject.Find("Back Button").GetComponent<Button>();

          playAgainBtn.onClick.AddListener(()=> PlayAgain());
          backBtn.onClick.AddListener(()=> BackToMenu());

          scoreText = GameObject.Find("Score Text");
          finalScore = GameObject.Find("Final Score").GetComponent<Text>();  

          gameOverPanel.SetActive(false);
    }

    public void PlayAgain(){
        SceneManager.LoadScene("Gameplay");
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
