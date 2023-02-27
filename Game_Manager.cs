using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Game_Manager : Singleton<Game_Manager>
{
    private int lives;

    private int points = 0;
    

    public GameObject Heart_1;
    public GameObject Heart_2;
    public GameObject Heart_3;
    public Text Points_Counter;

    public GameObject Canvas_1;

    public GameObject Try_Again_Button;
    public GameObject Exit_Button;

    private void Start()
    {
        DontDestroyOnLoad(transform.root);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Heart_1);
        DontDestroyOnLoad(Heart_2);
        DontDestroyOnLoad(Heart_3);
        DontDestroyOnLoad(Canvas_1);
        New_Game();
    }

    public void New_Game()
    {
        Try_Again_Button.SetActive(false);
        Exit_Button.SetActive(false);
        lives = 3;
        points = 0;
        SceneManager.LoadScene(1);
        Heart_1.SetActive(true);
        Heart_2.SetActive(true);
        Heart_3.SetActive(true);
        Points_Counter.text = points.ToString();
    }

    public void Level_Complete()
    {
        Debug.Log("Mario uratował księżniczkę");
        Try_Again_Button.SetActive(true);
        Exit_Button.SetActive(true);
    }

    public void Level_Failed()
    {
        lives --; 

        if(Heart_1 && Heart_2 && Heart_3.activeSelf)
        {
            Heart_3.SetActive(false);
        }
        else if(Heart_1 && Heart_2.activeSelf)
        {
            Heart_2.SetActive(false);
        }
        else if(Heart_1.activeSelf)
        {
            Heart_1.SetActive(false);
        }
        Debug.Log("Mario zostaje zgnieciony przez beczkę i traci jedno życie");
        if(lives <= 0)
        {
            Try_Again_Button.SetActive(true);
            Exit_Button.SetActive(true);
        }
        else
        {   
            SceneManager.LoadScene(1);
        }
    }
    public void Exit_Game()
    {
        Application.Quit();
    }
    public void Get_100_Points()
    {
        points += 100;
        Points_Counter.text = points.ToString();
        Debug.Log("Mario dostaje 100 punktów za przeskoczenie nad beczką");
        // Debug.Log("Test");
    }
}
