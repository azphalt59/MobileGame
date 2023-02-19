using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private Vector3 offset;
    public int currentGridPosition;
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
        offset = new Vector3(0, transform.localScale.y + GridManager.Instance.tileBase.transform.localScale.y * (GridManager.Instance.GetCaseSize()*0.5f), 0);
       
    }
    public void FindMovementPossibilities()
    {
        GridManager.Instance.GetMovementCase(currentGridPosition);
    }
    public void SetPosition(Vector3 worldPos, int gridPos)
    {
        // world pos
        transform.position = offset + worldPos;
        // grid pos
        currentGridPosition = gridPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
