using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingSceneBtn : MonoBehaviour
{
    public void TouchDialogueLogBtn()
    {
        //�α� �����ּ�
    }

    public void TouchGalleryBtn()
    {
        SceneManager.LoadScene("GalleryScene");
    }

    public void TouchExitBtn()
    {
        //��ȥ�� ��� �־����ڽ��ϱ�? yes/no ���� �� �̵�
    }
}
