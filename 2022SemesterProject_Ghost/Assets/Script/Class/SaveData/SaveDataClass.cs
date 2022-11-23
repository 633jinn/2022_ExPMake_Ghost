using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveDataClass
{
    ///���̺� ������ �ʿ��� ���
    ///     1. ó�� ������ �� �÷����ϰ� �ִ� ��
    ///     2. �ش� ��¥�� �ʼ� ���丮�� ���Ҵ°�?
    ///     3. �� ���񸶴� Ŭ��� �ߴ°�?
    ///     4. 
    ///     
    public bool isFirstPlay;
    public int startYear;
    public int startMonth;
    public int startDay;
    public int nowDay;
    public List<bool> isWatchDayStory;
    public List<bool> isClearPuzzle;

    public SaveDataClass()
    {
        isFirstPlay = true;
        startYear = DateTime.Now.Year;
        startMonth = DateTime.Now.Month;
        startDay = DateTime.Now.Day;
        nowDay = 1;
        isWatchDayStory = new List<bool>();
        for (int i = 0; i < 3; i++)
            isWatchDayStory.Add(false);
        isClearPuzzle = new List<bool>();
        for (int i = 0; i < 12; i++)
            isClearPuzzle.Add(false);
    }
}
