using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Hits;
    public int HitScoreValue = 10;
    public int DestroyScoreValue = 40;

    GameController gameController;
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        BlockColors();
    }
    public void OnHit()
    {
        Hits--;
        gameController.AddScore(HitScoreValue);
        BlockColors();

        if (Hits <= 0)
        {
            gameController.AddScore(DestroyScoreValue);
            Destroy(Instantiate(gameController.ExplosionPrefab, transform.position, Quaternion.identity),1f);
            Destroy(gameObject);
            
        }
    }

    public void BlockColors()
    {
        if (Hits == 3)
        {
            spriteRenderer.color = Color.red;
        }
        else if (Hits == 2)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (Hits == 1)
        {
            spriteRenderer.color = Color.green;
        }
    }

}
