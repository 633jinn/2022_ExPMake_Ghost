using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //해당 소품을 클릭하면 퍼즐로 이동하도록 설정
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    GameObject soul;
    [SerializeField]
    GameObject dialoguePrefab;
    string dialogueName;

    void Start()
    {
        dialogueName = "RandomDialogue";
        SetPuzzleStory();
    }

    public void TalkWithSoul()
    {
        if (GameManager.Instance.isTalkTIme == true)
        {
            SetRandomDialogue();
            dialogueManager.LoadDialogue();
        }
        else
        {
            Debug.Log("zzz");
        }
    }

    void SetRandomDialogue()
    {
        int randomNum = Random.Range(1, 12);
        GameManager.Instance.setDialogueName = dialogueName + randomNum.ToString();
        Debug.Log(dialogueName + randomNum);
    }
    public void SetPuzzleStory()
    {
        if (GameManager.Instance.puzzleDialogue == true)
        {
            GameManager.Instance.setDialogueName = GameManager.Instance.beforeSetDialogueName;
            dialogueManager.LoadDialogue();
            GameManager.Instance.puzzleDialogue = false;
        }
        
    }
}
