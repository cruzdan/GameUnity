using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] BrickManager man;

    public float initialSpeedX = 7.5f;
    public float initialSpeedY = 7.5f;
    public float maxSpeedX = 30;
    public float maxSpeedY = 30;

    public float speedX;
    public float speedY;
    private Rigidbody2D rb;

    float sizeY = 5f;
    float firstX;
    float firstY;
    float endX;
    float endY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firstX = -10f + transform.localScale.x / 2;
        endX = 3.95f - transform.localScale.x / 2;
        firstY = sizeY - transform.localScale.y / 2;
        endY = -sizeY + transform.localScale.y / 2;
        initBall();
    }

    public void initBall()
    {
        speedX = -initialSpeedX;
        speedY = initialSpeedY * 4;
        transform.position = new Vector2(-3f, -3.5f);
    }
    // Update is called once per frame
    void Update()
    {
        float nextPositionX = rb.position.x + speedX * Time.deltaTime;
        float nextPositionY = rb.position.y + speedY * Time.deltaTime;
        if (nextPositionX < firstX)
        {
            speedX = Mathf.Abs(speedX);
        }
        else if (nextPositionX > endX)
        {
            speedX = -Mathf.Abs(speedX);
        }

        if (nextPositionY > firstY)
        {
            speedY = -Mathf.Abs(speedY);
        }
        else if (nextPositionY < endY)
        {
            man.RestartGame();
            //speedY = Mathf.Abs(speedY);
        }
        Vector2 pos = new Vector2(rb.position.x + speedX * Time.deltaTime, rb.position.y + speedY * Time.deltaTime);
        rb.MovePosition(pos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Brick"))
        {

            float ballY = transform.position.y + transform.localScale.y / 2;
            float brickY = collision.transform.position.y + collision.transform.localScale.y / 2;
            float ballX = transform.position.x - transform.localScale.x / 2;
            float brickX = collision.transform.position.x - collision.transform.localScale.x / 2;
            Vector2 pos;
            if (speedY < 0 && ballY > brickY)
            {
                speedY = -speedY;
                pos = new Vector2(rb.position.x, rb.position.y + speedY * Time.deltaTime);
                rb.MovePosition(pos);
            }
            else if (speedY >= 0 && ballY - transform.localScale.y < brickY - collision.transform.localScale.y)
            {
                speedY = -speedY;
                pos = new Vector2(rb.position.x, rb.position.y + speedY * Time.deltaTime);
                rb.MovePosition(pos);
            }
            if (speedX > 0 && ballX < brickX)
            {
                speedX = -speedX;
                pos = new Vector2(rb.position.x + speedX * Time.deltaTime, rb.position.y);
                rb.MovePosition(pos);
            }
            else if (speedX <= 0 && ballX + transform.localScale.x > brickX + collision.transform.localScale.x)
            {
                speedX = -speedX;
                pos = new Vector2(rb.position.x + speedX * Time.deltaTime, rb.position.y);
                rb.MovePosition(pos);
            }
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            float px = collision.gameObject.transform.position.x - collision.gameObject.transform.localScale.x / 2;
            float scaleX = collision.gameObject.transform.localScale.x;
            if (transform.position.x < px + scaleX / 3)
            {
                Debug.Log("Left");
                if (speedX - initialSpeedX / 2 >= -maxSpeedX)
                    speedX -= initialSpeedX / 2;
            }
            else if (transform.position.x > px + 2 * scaleX / 3)
            {
                Debug.Log("Right");
                if (speedX + initialSpeedX / 2 <= maxSpeedX)
                    speedX += initialSpeedX / 2;
            }
            speedY = Mathf.Abs(speedY);
        }
    }
}
