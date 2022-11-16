using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager2 : MonoBehaviour
{
    private static GameManager2 instance;
    public JsonManager jsonManager2;

    [HideInInspector]
    //�ð� ����
    public Text counterText, wakeupDateText, wakeupTimeText, wakeupText;
    [HideInInspector] public TimeSpan spare;
    [HideInInspector] public DateTime midnightTime, standardTime, now;



    private void Start()
    {
        jsonManager2 = new JsonManager();
    }

    void Update()
    {
        Time();
    }

    void Time()
    {
        string standard = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00"; // ���� ���� 8��
        standardTime = Convert.ToDateTime(standard); //���ؽð��� ���ó�¥ ���� 8�÷� ����
        now = DateTime.Now; //����ð�

        int result = DateTime.Compare(DateTime.Now, standardTime);
        if (result >= 0)
        {  // ���� 8�� ������ ���(��ȥ ��� ��)
            wakeupText.text = "��ħ �ð�";
            wakeupTimeText.text = "00��"; //����          
            wakeupDateText.text = now.AddDays(1).ToString("yyyy�� MM�� dd��"); // ���¥

            string midnight = now.AddDays(1).ToString("yyyy/MM/dd") + " 00:00"; // ����
            midnightTime = Convert.ToDateTime(midnight);
            spare = midnightTime - now; //����-����ð�
            counterText.text = "<color=#DC143C>" + spare.ToString(@"hh\:mm\:ss") + "</color>";
        }
        else
        { // ���� 8�� ����(��ȥ ��ħ ��)
            wakeupText.text = "��� �ð�";
            wakeupTimeText.text = "08��";

            spare = standardTime - now;
            wakeupDateText.text = standardTime.ToString("yyyy�� MM�� dd��");
            counterText.text = "<color=#32CD32>" + spare.ToString(@"hh\:mm\:ss") + "</color>";
        }
    }

}