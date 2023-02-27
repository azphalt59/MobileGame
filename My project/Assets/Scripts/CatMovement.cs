using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class CatMovement : MonoBehaviour
{
    public List<Vector3> CatPattern;
    public List<Vector3> ReversePath;

    public void Start()
    {
        GridController.Instance.cats.Add(this);
    }
    public void ExecutePath()
    {
        Vector3 posTarget = transform.position + (CatPattern[0] * Grid.Instance.SetPlayerSpeed());
        //transform.position += CatPattern[0] * Grid.Instance.SetPlayerSpeed();
        transform.DOMove(posTarget, GameManager.Instance.movementDuration).OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
        ReversePath.Add(-CatPattern[0]);
        CatPattern.Add(CatPattern[0]);
        CatPattern.RemoveAt(0);
    }
    public void ExecuteReversePath()
    {
        ReversePath.Reverse();
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnDrawGizmos()
    {
        Vector3 currentPos = transform.position;
        Gizmos.color = Color.green;
        for (int i = 0; i < CatPattern.Count; i++)
        {
            Gizmos.DrawLine(currentPos, currentPos + CatPattern[i] * Grid.Instance.SetPlayerSpeed());
            currentPos += CatPattern[i] * Grid.Instance.SetPlayerSpeed();
        }
    }
}
