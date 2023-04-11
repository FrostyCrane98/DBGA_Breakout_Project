using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score = 0;
    public int Combo = 0;
    public int Lives = 3;
    public UIController UIController;
    public int InitialLives = 3;

    public Ball Ball;
    public Vector3 BallResetPosition;

    public GameObject ExplosionPrefab;
    public GameObject BlockContainerPrefab;
    GameObject BlockContainerInstance;

    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }

    public void Start()
    {
        UIController.UpdateComboCounter(Combo);
        UIController.UpdateScoreText(Score);
        UIController.UpdateLives(Lives);
        PauseGame();
    }

    public void AddCombo()
    {
        Combo ++;

        UIController.UpdateComboCounter(Combo);

    }

    public void ResetCombo()
    {
        Combo = 0;
        UIController.UpdateComboCounter(Combo);
    }
    public void AddScore(int _value)
    {
        if (Combo == 0)
        {
            Score += _value;
        }
        else
        {
            Score += _value * Combo;
        }
        UIController.UpdateScoreText(Score);
    }

    public void BallLost()
    {
        //reset position of the ball
        Ball.transform.position = BallResetPosition;
        Vector3 currentVelocity = Ball.Velocity;
        Ball.enabled = false;
        currentVelocity.y = Mathf.Abs(currentVelocity.y);
        Ball.Velocity = currentVelocity;       


        //lose a life
        Lives--;
        UIController.UpdateLives(Lives);
        if (Lives < 0)
        {
            GameOver();
        }

        //reset combo counter
        ResetCombo();
    }

    void GameOver()
    {
        UIController.ShowGameOver();
        isPlaying = false;
        PauseGame();
    }

    public void StartGame()
    {
        isPlaying = true;
        ResetGame();
        UnpauseGame();
    }

    void ResetGame()
    {
        Lives = InitialLives;
        Score = 0;
        Combo = 0;
        UIController.UpdateScoreText(Score);
        UIController.UpdateLives(Lives);
        UIController.HideStartGamePanel();
        UIController.HideGameOver();
        Destroy(BlockContainerInstance);
        BlockContainerInstance = Instantiate(BlockContainerPrefab);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (isPlaying)
        {
            UIController.ShowPausePanel();
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        UIController.HidePausePanel();
    }

    public void QuitGame()
    {
        isPlaying = false;
        PauseGame();
        UIController.HideGameOver();
        UIController.HidePausePanel();
        UIController.ShowStartGamePanel();

        Ball.transform.position = BallResetPosition;
    }
}
