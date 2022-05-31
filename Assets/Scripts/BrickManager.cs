using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text lifesText;
    [SerializeField] private Text shootText;


    public Ball ball;
    public PaddleMovement paddle;
    private GameObject brick;
    private int totalX = 14;
    private int actualBricks;
    private float firstX = -9.6f;

    //Game Menu
    public int level = 1;
    private int score = 0;
    private int lifes = 3;
    

    float brickWidth;
    float brickHeight;
    float freeSizeX;
    float freeSizeY;
    float initialBrickY;

    // Start is called before the first frame update
    void Start()
    {
        int totalY = level + Random.Range(0, 6);
        
        actualBricks = totalX * totalY;
        brickWidth = 13.95f / (totalX + 1);
        brickHeight = 10f * 0.01777f;
        freeSizeX = brickWidth / (totalX + 1);
        freeSizeY = 10f / 240f;
        initialBrickY = 1.9555f;

        CreateBricks(totalY);

        scoreText.text = score.ToString();
        levelText.text = level.ToString();
        lifesText.text = lifes.ToString();
    }

    public void DecrementActualBricks()
    {
        actualBricks--;
        score++;
        scoreText.text = score.ToString();
        if(actualBricks == 0)
        {
            NextLevel();
        }
    }

    public void CreateBricks(int totalY)
    {
        int pos = 0;
        for (int j = 0; j < totalY; j++)
        {
            for (int i = 0; i < totalX; i++)
            {
                pos = i + j * totalX;
                brick = Instantiate(brickPrefab) as GameObject;
                brick.transform.position = new Vector2(i * brickWidth + (i + 1) * freeSizeX + firstX,
                    j * brickHeight + (j + 1) * freeSizeY + initialBrickY);
                brick.transform.localScale = new Vector2(brickWidth, brickHeight);
            }
        }
    }

    public void NextLevel()
    {
        level++;
        levelText.text = level.ToString();
        ball.initBall();
        paddle.transform.position = new Vector2(-3f, -4f);
        int totalY = level + Random.Range(0, 6);
        actualBricks = totalX * totalY;
        CreateBricks(totalY);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
