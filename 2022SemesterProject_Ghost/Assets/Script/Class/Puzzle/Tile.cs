using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    TextMeshProUGUI textNumeric;
    [SerializeField]
    private PuzzleManager puzzleManager;
    private Vector3 correctPosition;

    public bool IsCorrected { private set; get; } = false;

    private int numeric;
    public int Numeric
    {
        set
        {
            numeric = value;
            textNumeric.text = numeric.ToString();
        }
        get => numeric;
    }
    public void Setup(int hideNumeric, int numeric)
    {
        textNumeric = GetComponentInChildren<TextMeshProUGUI>();
        textNumeric.enabled = false;
        Numeric = numeric;
        if(Numeric == hideNumeric)
        {
            GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
    }
    public void SetCorrectPosition()
    {
        correctPosition = GetComponent<RectTransform>().localPosition;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance.puzzleClearArray[GameManager.Instance.puzzleArrayNum])
        {
            puzzleManager.IsMoveTile(this);
        }
    }
    public void OnMoveTo(Vector3 end)
    {
        StartCoroutine("MoveTo", end);
    }

    private IEnumerator MoveTo(Vector3 end)
    {
        float current = 0;
        float percent = 0;
        float moveTime = 0.1f;
        Vector3 start = GetComponent<RectTransform>().localPosition;
        while(percent<1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            GetComponent<RectTransform>().localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        IsCorrected = correctPosition == GetComponent<RectTransform>().localPosition ? true : false;

        puzzleManager.IsGameOver();
    }
    public void MakeCorrectTrue()
    {
        IsCorrected = true;
    }
}
