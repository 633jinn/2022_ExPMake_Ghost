using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneBtn : MonoBehaviour
{
    public void TouchStartBtn()
    {
        SceneManager.LoadScene("WaitingScene");
    }

    public void TouchLoadBtn()
    {
        //���� �ε��ؼ� ��������
    }

    public void TouchQuitBtn()
    {
        Application.Quit();
    }
}
