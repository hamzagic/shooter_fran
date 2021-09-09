using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    public GameObject player;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        
        //if (gameOver)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        Instantiate(player, Vector3.zero, Quaternion.identity);
        //        gameOver = false;
        //        _uiManager.ResetCounter();

        //    }
        //}
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
