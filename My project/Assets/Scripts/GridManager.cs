using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile
{
    public GameObject tileGround;
}
public class GridManager : MonoBehaviour
{
    [SerializeField] private float rows;
    [SerializeField] private float cols;
    [SerializeField] private float caseSize;
    [SerializeField] private GameObject gridParent;

    public GameObject tileBase;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = new Vector3(caseSize * 0.5f, 0, caseSize * 0.5f);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 position = offset + new Vector3(caseSize * row, 0, caseSize * col);
                GameObject tile = Instantiate(tileBase, position, Quaternion.identity);
                tile.transform.parent = gridParent.transform;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
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
