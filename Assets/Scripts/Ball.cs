using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject lastObjectHit;
    CircleCollider2D cc;
    public Vector2 Velocity = new Vector2(4, 4);
    public AudioClip WallHit;
    public AudioClip PaddleHit;
    public AudioClip BlockBreak;

    GameController gameController;


    private void Awake()
    {
        cc = GetComponent<CircleCollider2D>();
        gameController = FindObjectOfType<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Velocity * Time.deltaTime);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, cc.radius, Velocity, (Velocity * Time.deltaTime).magnitude);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != cc && hit.transform.gameObject != lastObjectHit)
            {
                lastObjectHit = hit.transform.gameObject;

                Velocity = Vector2.Reflect(Velocity, hit.normal);

                if (hit.transform.GetComponent<Paddle>())
                {
                    Velocity.y = Mathf.Abs(Velocity.y);
                    FindObjectOfType<AudioPlayer>().Play(PaddleHit);
                }

                else if (hit.transform.GetComponent<Block>())
                {
                    hit.transform.GetComponent<Block>().OnHit();
                    FindObjectOfType<AudioPlayer>().Play(BlockBreak);
                }
                else
                    FindObjectOfType<AudioPlayer>().Play(WallHit);
            }

            if (transform.position.y < -Camera.main.orthographicSize)
            {
                gameController.BallLost();
            }
        }
    }
}
