using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;

    [Header("Grid Settings")]
    [SerializeField] private float rows;
    [SerializeField] private float cols;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 gridOffset;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        //GenerateGrid();
    }
    public float SetPlayerSpeed()
    {
        return cellSize;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < cols; z++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x*cellSize, 0, z*cellSize) + new Vector3(cellSize*0.5f, 0, cellSize*0.5f);
                cube.transform.localScale = new Vector3(cellSize-(0.1f*cellSize), 1, cellSize-(0.1f * cellSize));
                cube.transform.parent = transform;

                
            }
        }

    }
#if UNITY_EDITOR
    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < rows + 1; i++)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(new Vector3(0, 0, cellSize * i) + gridOffset, new Vector3(cellSize * cols, 0, cellSize * i) + gridOffset);
    //    }
    //    for (int i = 0; i < cols + 1; i++)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(new Vector3(cellSize * i, 0, 0) + gridOffset, new Vector3(cellSize * i, 0, cellSize * rows) + gridOffset);
    //    }
    //}
#endif
}
