using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridManager : MonoBehaviour
{
    [SerializeField] private float rows;
    [SerializeField] private float cols;
    [SerializeField] private float caseSize;
    [SerializeField] private GameObject gridParent;
    public List<Vector3> Pos;
    public List<GameObject> Cases;
    public int playerStart;

    public static GridManager Instance;

    public GameObject tileBase;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = new Vector3(caseSize * 0.5f, 0, caseSize * 0.5f);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 position = offset + new Vector3(caseSize * row, 0, caseSize * col);
                Pos.Add(position);
                GameObject tile = Instantiate(tileBase, position, Quaternion.identity);
                tile.GetComponent<Tile>().gridIndex = (int)((row * cols) + col);
                tile.GetComponent<Tile>().tileGround = tile;
                tile.GetComponent<Tile>().triggerMovement = tile.transform.GetChild(0).gameObject;
                tile.transform.parent = gridParent.transform;
                tile.transform.localScale *= caseSize;
                Cases.Add(tile);
            }
        }
        PlayerMovement.Instance.SetPosition(Pos[playerStart], playerStart);
        PlayerMovement.Instance.FindMovementPossibilities();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public float GetCaseSize()
    {
        return caseSize;
    }
    public void GetMovementCase(int currentPlayerPos)
    {
        
        // Top
        if(currentPlayerPos - (int)cols >= 0)  
            Cases[currentPlayerPos - (int)cols].GetComponent<Tile>().triggerMovement.SetActive(true);
        // Bot
        if (currentPlayerPos + (int)cols <= rows*cols-1)
            Cases[currentPlayerPos + (int)cols].GetComponent<Tile>().triggerMovement.SetActive(true);
        // Left
        if (currentPlayerPos%cols != 0)
            Cases[currentPlayerPos - 1].GetComponent<Tile>().triggerMovement.SetActive(true);
        // Right
        if (currentPlayerPos%cols != cols-1)
            Cases[currentPlayerPos + 1].GetComponent<Tile>().triggerMovement.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < rows+1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(0,0, caseSize*i), new Vector3(caseSize * cols, 0, caseSize * i));
        }
        for (int i = 0; i < cols+1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(caseSize * i,0,0), new Vector3(caseSize * i, 0, caseSize * rows));
        }
    }
}
