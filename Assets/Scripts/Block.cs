using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Hits = 1;
    public int ScoreValue = 100;

    GameController gameController;


    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void OnHit()
    {
        Hits--;

        if (Hits <= 0)
        {
            gameController.AddScore(ScoreValue);
            Destroy(gameObject);
        }
    }
    
}
