using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject gameMenu;
    [SerializeField] GameObject pauseMenu;

    bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                ReturnButton();
            }
            else
            {
                Time.timeScale = 0f;
                gameMenu.SetActive(false);
                pauseMenu.SetActive(true);
                pause = !pause;
            }
        }
    }

    public void ReturnButton()
    {
        Time.timeScale = 1f;
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        pause = !pause;
    }
}
