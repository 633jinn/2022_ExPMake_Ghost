using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    DialogueManager dialogueManager;
    [SerializeField]
    private Transform tilesParent;
    [SerializeField]
    private List<Tile> tileList;//생성 타일 정보 저장
    private Vector2Int puzzleSize = new Vector2Int(3, 3);

    private float neighborTileDistance = 320; //인접한 타일 사이의 거리
    public Vector3 EmptyTilePosition { set; get; }

    private Image img; //이미지 삽입
    public Sprite[] sprites;
    private IEnumerator Start()
    {
        tileList = new List<Tile>();

        SpawnTiles();

        SetPuzzle();
        if (ClearCheck())
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());
            yield return new WaitForEndOfFrame();

            tileList.ForEach(x => x.SetCorrectPosition());

            Suffle();
        }
    }

    private void SpawnTiles()
    {
        int num = 0;
        for (int y = 0; y < puzzleSize.y; ++y)
        {
            for (int x = 0; x < puzzleSize.x; ++x)
            {
                GameObject tileObject = tilesParent.GetChild(num).gameObject;

                Tile tile = tileObject.GetComponent<Tile>();
                
                tile.Setup(puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

                tileList.Add(tile);
                ++num;
            }
        }
    }
    public void Suffle()
    {
        int[] array = new int[tilesParent.childCount];
        int arraysize = tilesParent.childCount;
        while (CheckEntropy())
        {
            for (int i = 0; i < arraysize; ++i)
            {
                array[i] = tilesParent.GetChild(i).GetComponent<Tile>().Numeric;//자식이 배치된 순서
            }
            for (int i = 0; i < 9; i++)
            {
                if (array[i] != 9)
                {
                    var lastPos = tileList[i].transform.GetSiblingIndex();
                    int randomIndex = Random.Range(0, puzzleSize.x * puzzleSize.y - 1);
                    int tileNumeric = array[i];
                    tileList[i].transform.SetSiblingIndex(tileList[randomIndex].transform.GetSiblingIndex());
                    tileList[randomIndex].transform.SetSiblingIndex(tileList[lastPos].transform.GetSiblingIndex());
                    array[i] = array[randomIndex];
                    array[randomIndex] = array[tileNumeric];
                }
            }
        }
        EmptyTilePosition = tileList[tileList.Count - 1].GetComponent<RectTransform>().localPosition;
        for (int i = 0; i < 8; i++)
        {
            if (tilesParent.GetChild(i).GetComponent<Tile>().Numeric == i + 1)
                tileList[i].MakeCorrectTrue();
        }
    }
    public void IsMoveTile(Tile tile)//타일 이동
    {
        if (Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
        {
            Vector3 goalPosition = EmptyTilePosition;
            EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;
            tile.OnMoveTo(goalPosition);
        }
    }

    public void IsGameOver()//알맞은 위치의 퍼즐을 카운트, 모두 맞을 경우 GameClear 출력
    {
        List<Tile> tiles = tileList.FindAll(x => x.IsCorrected == true);

        Debug.Log("Correct Count : " + tiles.Count);
        if (tiles.Count == puzzleSize.x * puzzleSize.y - 1)
        {
            Debug.Log("GameClear");
            GameManager.Instance.puzzleClearArray[GameManager.Instance.puzzleArrayNum] = true;
            GameManager.Instance.puzzleDialogue = true;
            SceneManager.LoadScene("RoomScene");
        }
    }
    private bool CheckEntropy()//entropy 검사
    {
        int[] array = new int[tilesParent.childCount];
        int arraysize = tilesParent.childCount;
        for (int i = 0; i < arraysize; ++i)
        {
            array[i] = tilesParent.GetChild(i).GetComponent<Tile>().Numeric;//자식이 배치된 순서
        }
        int entropy = 0;
        for (int i = 0; i < arraysize; ++i)
        {
            for (int j = i + 1; j < arraysize; ++j)
            {
                if (array[i] > array[j] && array[i] != 9) ++entropy;
            }
        }
        if (entropy % 2 != 0 || entropy == 0)//entropy가 홀수면 계속 suffle
            return true;
        else
            return false;
    }
    public void SetPuzzle()
    {
        int num = 0;
        sprites = Resources.LoadAll<Sprite>(GameManager.Instance.puzzleImage);
        
        for (int y = 0; y < puzzleSize.y; ++y)
        {
            for (int x = 0; x < puzzleSize.x; ++x)
            {
                GameObject tileObject = tilesParent.GetChild(num).gameObject;
                img = tileObject.GetComponent<Image>();
                img.sprite = sprites[num];
                num++;
            }
        }
    }
    bool ClearCheck()
    {
        GameObject clearText = GameObject.Find("GameClear");
        if (GameManager.Instance.puzzleClearArray[GameManager.Instance.puzzleArrayNum])
        {
            clearText.GetComponent<UnityEngine.UI.Image>().enabled = true;
            clearText.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            //SceneManager.LoadScene("RoomScene");
            return false;
        }
        else
        {
            clearText.GetComponent<UnityEngine.UI.Image>().enabled = false;
            clearText.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            return true;
        }
    }
}
