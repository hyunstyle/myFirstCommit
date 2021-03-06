﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lifeController : MonoBehaviour
{

    

    private static lifeController instance;
    public static lifeController Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<lifeController>();
            }
            return lifeController.instance;
        }
    }

    public int life;

    public GameObject[] healthBar;

    public Text remainedGerm;
    public int remainedGermNumber;

    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public CanvasGroup mainCanvasGroup;

    public ParticleSystem bombEffect;
    public ParticleSystem clearEffect;
    private float gameOverTime;
    private bool isGameOver;
    private bool isEffectEnd;
    public bool isCleared;

    // 각 stage별 넘어야하는 score
    public int[] stage;
     
    // 현재 Scene의 index
    public int currentNum;

    // 슬라이드 효과 애니메이션
    public GameObject slideDown;
    public GameObject slideUp;

    // 점수 올라가는 effect
    public ParticleSystem plusEffect;

    public static int score;

    //public GameObject scoreNum;
    //public Animator amt;
    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        score = 0;

        // 각 stage별 목표치
        stage = new int[5];
        stage[0] = 10;
        stage[1] = 30;
        stage[2] = 60;
        stage[3] = 100;

        // 현재 Scene의 index 저장
        currentNum = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("current index: " + currentNum);
        //Debug.Log("current goal: " + stage[currentNum]);

        // stage별 점수 초기화
        //remainedGermNumber = stage[currentNum - 1];
        remainedGerm.text = remainedGermNumber.ToString();

        isGameOver = false;
        isEffectEnd = false;
        isCleared = false;

        //amt = scoreNum.GetComponent<Animator>();
        //amt.enabled = false;
    }

    private void NewMethod()
    {
        stage[0] = 0;
    }

    // Update is called once per frame
    void Update ()
    {
		if(life == 0)
        {
            

            // Game Over 됬을 때 explosion 실행
            if (!isGameOver)
            {
                //gameOverTime = Time.time;
                bombEffect.gameObject.SetActive(true);
                StartCoroutine(turnOnGameOverCanvas());
                isGameOver = true;
                //gameOverCanvas.SetActive(true);
                //score = remainedGermNumber;

            }
            else
            {
                // explosion이 끝나면 LeaderBoard로 이동
                //if(!isEffectEnd && gameOverTime + 1.5f < Time.time)
                //{
                //    bombEffect.gameObject.SetActive(false);
                //    mainCanvas.SetActive(false);
                    

                //    isEffectEnd = true;
                //    SceneManager.LoadScene("LeaderBoard");
                //}
            }
        }

        if(isCleared)
        {   
            StartCoroutine(nextLevel());
            //StartCoroutine(DoFade());
            isCleared = false;
        }
	}

    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(1.0f);

        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName.Contains("1"))
        {
            SceneManager.LoadScene(2);
        }
        else if (sceneName.Contains("2"))
        {
            SceneManager.LoadScene(3);
        }
        else if (sceneName.Contains("3"))
        {
            SceneManager.LoadScene(4);
        }
        else if (sceneName.Contains("4"))
        {
            SceneManager.LoadScene(5);
        }
        else if (sceneName.Contains("5"))
        {

        }
    }

    IEnumerator DoFade()
    {

        while (mainCanvasGroup.alpha > 0)
        {
            mainCanvasGroup.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }

        mainCanvasGroup.interactable = false;

        yield return null;
    }

    IEnumerator turnOnGameOverCanvas()
    {
        yield return new WaitForSeconds(1.5f);
        gameOverCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);

        score = remainedGermNumber;
        bombEffect.gameObject.SetActive(false);
        mainCanvas.SetActive(false);

        SceneManager.LoadScene("LeaderBoard");
    }
}
