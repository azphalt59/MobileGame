using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        PlayerMovement.Instance.SetPosition(transform.parent.position, transform.parent.GetComponent<Tile>().gridIndex);
        
        for (int i = 0; i < GridManager.Instance.Cases.Count; i++)
        {
            GridManager.Instance.Cases[i].GetComponent<Tile>().triggerMovement.SetActive(false);
        }

        PlayerMovement.Instance.FindMovementPossibilities();
    }
}
