using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject ScreenTouchCanvas;
    [SerializeField]
    GameObject dialoguePrefab;
    [SerializeField]
    Text characterNameText;
    [SerializeField]
    Text dialogueText;
    [SerializeField]
    GameObject routeFirstParent;
    [SerializeField]
    Text routeFirstText;
    [SerializeField]
    GameObject routeSecondParent;
    [SerializeField]
    Text routeSecondText;
    [SerializeField]
    GameObject routeThirdParent;
    [SerializeField]
    Text routeThirdText;

    [SerializeField]
    DialogueWrapper dialogueWrapper;
    [SerializeField]
    string dialogueWrapperName;

    public JsonManager jsonManager;

    [HideInInspector]
    bool isDialogueEnd = false;
    bool isDialoguePrinting = false;
    int nowDialogueIndex;

    private void Start()
    {
        jsonManager = new JsonManager();
        dialogueWrapper = jsonManager.ResourceDataLoad<DialogueWrapper>(dialogueWrapperName);
        dialogueWrapper.Parse();
        Debug.Log(dialogueWrapper.dialogueArray[0].dialogueTypes.ToString());
        characterNameText.text = "";
        dialogueText.text = "";
        nowDialogueIndex = 0;
        StartDialogue();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //    SkipDialogue();
    }

    public void StartDialogue()
    {
        //씬이 바뀌고 자연스럽게 페이드인 -> 천천히 대화창이 나오게 설정
        StartCoroutine(PrintDialogue());
    }

    IEnumerator PrintDialogue()
    {
        isDialoguePrinting = true;
        Dialogue nowDialogue = dialogueWrapper.dialogueArray[nowDialogueIndex];

        characterNameText.text = nowDialogue.characterName;
        for (int i = 0; i < nowDialogue.dialogue.Length; i++)
        {
            dialogueText.text += nowDialogue.dialogue[i];
            yield return new WaitForSeconds(0.07f);
        }

        isDialoguePrinting = false;
        nowDialogueIndex++;
    }

    public void SkipDialogue()
    {
        Debug.Log("누르면 쭉 스킵하게 만들거임");
    }

    public void SpreadDialogue(Dialogue nowDialog)
    {
        //한번에 쭉 뿌림
        dialogueText.text = "";
        dialogueText.text = nowDialog.dialogue;
    }

    public void ContinueDialogue()
    {
        if (nowDialogueIndex < dialogueWrapper.dialogueArray.Length)
        {
            Dialogue nowDialogue = dialogueWrapper.dialogueArray[nowDialogueIndex];
            if (isDialoguePrinting)
            {
                SpreadDialogue(nowDialogue);
                StopAllCoroutines();
                isDialoguePrinting = false;
                nowDialogueIndex++;
            }

            else if (!isDialogueEnd && !isDialoguePrinting)
            {
                dialogueText.text = "";
                Debug.Log("뚯뚜루~");
                StartCoroutine(PrintDialogue());
            }


            if (nowDialogueIndex >= dialogueWrapper.dialogueArray.Length)
            {
                isDialogueEnd = true;
            }
        }

        if (isDialogueEnd) Debug.Log("대화 끄읏");
    }
}