using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class BtnManager : MonoBehaviour
{
    [SerializeField]
    GameObject fadeCanvas;

    //MainSceneBtn
    public void TouchStartBtn()
    {
        StartCoroutine(TouchStartBtnCoroutine());
    }

    IEnumerator TouchStartBtnCoroutine()
    {
        if (GameManager.Instance.saveData.isFirstPlay)
        {
            GameManager.Instance.saveData.isFirstPlay = false;
            //처음 시작한 날 저장
            GameManager.Instance.saveData.startYear = DateTime.Now.Year;
            GameManager.Instance.saveData.startMonth = DateTime.Now.Month;
            GameManager.Instance.saveData.startDay = DateTime.Now.Day;
            GameManager.Instance.setDialogueName = "Day1Encounter";

            UIFadeModule screenFadeModule = fadeCanvas.GetComponent<UIFadeModule>();
            screenFadeModule.ScreenFade(0, 1, 1);
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene("StoryScene");
        }
        else
        {
            UIFadeModule screenFadeModule = fadeCanvas.GetComponent<UIFadeModule>();
            screenFadeModule.ScreenFade(0, 1, 1);
            yield return new WaitForSeconds(1);
            //if(대화가 가능한 시간이 되었다면)
            SceneManager.LoadScene("WaitingScene");
        }
    }

    //WaitingSceneBtn
    public void TouchWaitingSceneSoulBtn()
    {
        if (GameManager.Instance.isTalkTIme == true)
        {
            //만약 그 날짜의 스토리를 보지 않았다면 스토리씬으로
            int nowDay = GameManager.Instance.saveData.nowDay;
            if (GameManager.Instance.saveData.isWatchDayStory[nowDay - 1] == false
                    && GameManager.Instance.isTalkTIme)
            {
                switch (nowDay)
                {
                    case 1:
                        GameManager.Instance.setDialogueName = "Day1Story";
                        SceneManager.LoadScene("StoryScene");
                        break;
                    case 2:
                        GameManager.Instance.setDialogueName = "Day2Story";
                        SceneManager.LoadScene("StoryScene");
                        break;
                    case 3:
                        GameManager.Instance.setDialogueName = "Day3Story";
                        SceneManager.LoadScene("StoryScene");
                        break;
                }
            }
            else
            {
                Debug.Log("Day" + nowDay + " 스토리 진행 끝남");
                SceneManager.LoadScene("RoomScene");
            }
        }
        else
        {
            //아니면
            SceneManager.LoadScene("RoomScene");
        }
    }

    public void TouchGalleryBtn()
    {
        SceneManager.LoadScene("GalleryScene");
    }

    public void TouchExitBtn()
    {
        //영혼과 잠시 멀어지겠습니까? yes/no 선택 후 이동 어디로?@@@@@@@@
    }

    //RoomSceneBtn
    public void TouchInteriorObjectBtnlight1()
    {
        GameManager.Instance.pastSceneName = SceneManager.GetActiveScene().name;
        int nowDay = GameManager.Instance.saveData.nowDay;
        bool[] array = GameManager.Instance.puzzleClearArray;
        GameManager.Instance.puzzleArrayNum = nowDay;
        if (nowDay == 1)
        {
            GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Rectangular";
            GameManager.Instance.beforeSetDialogueName = "Day1PastLifePuzzle2";
        }
        else
        {
            GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Sherlock";
            GameManager.Instance.beforeSetDialogueName = "Day2PastLifePuzzle";
        }
        SceneManager.LoadScene("PuzzleScene");
    }
    public void TouchInteriorObjectBtnlight2()
    {
        GameManager.Instance.pastSceneName = SceneManager.GetActiveScene().name;
        int nowDay = GameManager.Instance.saveData.nowDay;
        bool[] array = GameManager.Instance.puzzleClearArray;
        GameManager.Instance.puzzleArrayNum = GameManager.Instance.countCheck * (nowDay * 2 - 1) + 1;
        if (nowDay == 1)
        {
            if (GameManager.Instance.countCheck % 3 == 1)
            {
                GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Clover";
                GameManager.Instance.beforeSetDialogueName = "Day1StoryCloverPuzzle";
                GameManager.Instance.countCheck++;
            }
            else if (GameManager.Instance.countCheck % 3 == 2)
            {
                GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Leash";
                GameManager.Instance.beforeSetDialogueName = "Day1StoryCollarPuzzle";
                GameManager.Instance.countCheck++;
            }
            else
            {
                GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Soccer_ball";
                GameManager.Instance.beforeSetDialogueName = "Day1StoryFootballPuzzle";
                GameManager.Instance.countCheck = 1;
            }
        }
        else
        {
            if (GameManager.Instance.countCheck % 3 == 1)
            {
                GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Band";
                GameManager.Instance.beforeSetDialogueName = "Day2StoryBandPuzzle";
                GameManager.Instance.countCheck++;
            }
            else if (GameManager.Instance.countCheck % 3 == 2)
            {
                GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Cup";
                GameManager.Instance.beforeSetDialogueName = "Day2StoryCoffeePuzzle";
                GameManager.Instance.countCheck++;
            }
            else
            {
                GameManager.Instance.puzzleImage = "PuzzleImage/Puzzle_Wheel";
                GameManager.Instance.beforeSetDialogueName = "Day2StoryTirePuzzle";
                GameManager.Instance.countCheck = 1;
            }
        }
        SceneManager.LoadScene("PuzzleScene");
    }


    //GallerySceneBtn
    public void TouchHomeBtn()
    {
        SceneManager.LoadScene("WaitingScene");
    }

    public void TouchPuzzleBtn()
    {
        GameManager.Instance.pastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("PuzzleScene");
    }

    //PuzzleSceneBtn
    public void TouchPuzzleExitBtn()
    {
        switch(GameManager.Instance.pastSceneName)
        {
            case "RoomScene":
                SceneManager.LoadScene("RoomScene");
                break;
            case "GalleryScene":
                SceneManager.LoadScene("GalleryScene");
                break;
        }
    }

    //CustomizingSceneBtn
    public void TouchTempBtn()
    {
        GameManager.Instance.isCustomizingEnd = true;
        SceneManager.LoadScene("StoryScene");
    }
}
