using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class CatMovement : MonoBehaviour
{
    public List<Vector3> CatPattern;
    public List<Vector3> ReversePath;
    public bool reverse = false;
    public int mvtIndex = 0;
    public void Start()
    {
        GridController.Instance.cats.Add(this);
    }
    public void ExecutePath()
    {
        Vector3 posTarget = Vector3.zero;
        Vector3 newPos = Vector3.zero;
        if (reverse && ReversePath.Count == 0)
        {
            reverse = false;
            mvtIndex = 0;
        }
        if (reverse)
        {
            newPos = ReversePath[0];
        }
        else
        {
            newPos = CatPattern[mvtIndex];
        }
        if (Physics.CheckSphere(transform.position + (newPos * Grid.Instance.SetPlayerSpeed()), 0.2f))
        {
            Collider[] col = Physics.OverlapSphere(transform.position + (newPos * Grid.Instance.SetPlayerSpeed()), 0.2f);
            bool itsPlayer = false;
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].gameObject.GetComponent<PlayerController>() != null)
                {
                    itsPlayer = true;
                    Debug.Log("C le player");
                    ExecuteMove(posTarget);
                    break;
                }
            }

            if (itsPlayer) 
                return;

            Debug.Log("Il y a un obstacle");
            reverse = !reverse;
            mvtIndex = 0;

            if (reverse)
            {

            }
            else
                ReversePath.Clear();

            ExecuteReversePath();

        }
        else
        {
            Debug.Log("pas d'obstacle");

        }
       
            
        ExecuteMove(posTarget);

    }
    public void ExecuteReversePath()
    {
        ReversePath.Reverse();
    }
    public void ExecuteMove(Vector3 posTarget)
    {

        if (reverse)
        {
            posTarget = transform.position + (ReversePath[0] * Grid.Instance.SetPlayerSpeed());
            transform.DOMove(posTarget, GameManager.Instance.movementDuration).OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
            ReversePath.RemoveAt(0);
            
        }
        else
        {
            posTarget = transform.position + (CatPattern[mvtIndex] * Grid.Instance.SetPlayerSpeed());
            transform.DOMove(posTarget, GameManager.Instance.movementDuration).OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
            ReversePath.Add(-CatPattern[mvtIndex]);
        }
        mvtIndex++;
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
        Gizmos.color = Color.red;
        if (reverse)
        {
            if (ReversePath.Count == 0)
                return;
            Gizmos.DrawSphere(transform.position + (ReversePath[0] * Grid.Instance.SetPlayerSpeed()), 0.2f);
        }
           
        else
            Gizmos.DrawSphere(transform.position + (CatPattern[0] * Grid.Instance.SetPlayerSpeed()), 0.2f);
    }
}
