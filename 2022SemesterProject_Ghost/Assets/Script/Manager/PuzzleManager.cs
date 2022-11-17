using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private Transform tilesParent;
    [SerializeField]
    private Button button;
    private List<Tile> tileList;//���� Ÿ�� ���� ����
    private Vector2Int puzzleSize = new Vector2Int(3, 3);

    private float neighborTileDistance = 320; //������ Ÿ�� ������ �Ÿ�
    public Vector3 EmptyTilePosition { set; get; }
   

    private IEnumerator Start()
    {
        tileList = new List<Tile>();

        SpawnTiles();

        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame();

        tileList.ForEach(x => x.SetCorrectPosition());

        Suffle();

    }

    private void SpawnTiles()
    {
        int num = 0;
        for (int y = 0; y < puzzleSize.y; ++y)
        {
            for (int x = 0; x < puzzleSize.x; ++x)
            {
                GameObject tileObject = transform.GetChild(num).gameObject;

                Tile tile = tileObject.GetComponent<Tile>();

                tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

                tileList.Add(tile);
                ++num;
            }
        }
    }

    public void Suffle()
    {
        int[] array = new int[transform.childCount];
        int arraysize = transform.childCount;
        while (CheckEntropy())
        {
            for (int i = 0; i < arraysize; ++i)
            {
                array[i] = transform.GetChild(i).GetComponent<Tile>().Numeric;//�ڽ��� ��ġ�� ����
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
    }
    public void IsMoveTile(Tile tile)//Ÿ�� �̵�
    {
        if (Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
        {
            Vector3 goalPosition = EmptyTilePosition;
            EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;
            tile.OnMoveTo(goalPosition);
        }
    }

    public void IsGameOver()//�˸��� ��ġ�� ������ ī��Ʈ, ��� ���� ��� GameClear ���
    {
        List<Tile> tiles = tileList.FindAll(x => x.IsCorrected == true);

        Debug.Log("Correct Count : " + tiles.Count);
        if (tiles.Count == puzzleSize.x * puzzleSize.y - 1)
        {
            Debug.Log("GameClear");
        }
    }
    private bool CheckEntropy()//entropy �˻�
    {
        int[] array = new int[transform.childCount];
        int arraysize = transform.childCount;
        for (int i = 0; i < arraysize; ++i)
        {
            array[i] = transform.GetChild(i).GetComponent<Tile>().Numeric;//�ڽ��� ��ġ�� ����
        }
        int entropy = 0;
        for (int i = 0; i < arraysize; ++i)
        {
            for (int j = i + 1; j < arraysize; ++j)
            {
                if (array[i] > array[j] && array[i] != 9) ++entropy;
            }
        }
        if (entropy % 2 != 0 || entropy == 0)//entropy�� Ȧ���� ��� suffle
            return true;
        else
            return false;
    }
}
