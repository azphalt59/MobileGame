using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class CatMovement : MonoBehaviour
{
    public Animator CatAnimator;
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
            if(mvtIndex == CatPattern.Count)
            {
                mvtIndex = 0;
            }
            newPos = CatPattern[mvtIndex];
        }
        if (Physics.CheckSphere(transform.position + (newPos * Grid.Instance.SetPlayerSpeed()), 0.2f))
        {
            Collider[] col = Physics.OverlapSphere(transform.position + (newPos * Grid.Instance.SetPlayerSpeed()), 0.2f);
            bool itsPlayer = false;
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].gameObject.GetComponent<PlayerController>() != null || col[i].gameObject.GetComponent<Cheese>() != null)
                {
                    itsPlayer = true;
                    ExecuteMove(posTarget);
                    break;
                }
            }

            if (itsPlayer) 
                return;

            
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
            transform.LookAt(posTarget);
            CatAnimator.SetBool("Run", true);
            transform.DOMove(posTarget, GameManager.Instance.movementDuration).OnComplete(OnCompleteMovement);
            ReversePath.RemoveAt(0);
            
        }
        else
        {
            CatAnimator.SetBool("Run", true);
            posTarget = transform.position + (CatPattern[mvtIndex] * Grid.Instance.SetPlayerSpeed());
            transform.LookAt(posTarget);
            transform.DOMove(posTarget, GameManager.Instance.movementDuration).OnComplete(OnCompleteMovement);
            ReversePath.Add(-CatPattern[mvtIndex]);
        }
        mvtIndex++;
    }

    public void OnCompleteMovement()
    {
        CatAnimator.SetBool("Run", false);
        PlayerController.Instance.ResetPlayerTriggerMovement();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerDectector>() != null)
        {
            other.gameObject.GetComponent<PlayerDectector>().triggerLinkToThis.SetActive(false);
            other.gameObject.SetActive(false);
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            AudioManager.Instance.Play("CatCatch");
            AudioManager.Instance.Play("Fail");
            GameManager.Instance.LoseCon.PlayerLose();
        }
    }
#if UNITY_EDITOR
    //private void OnDrawGizmos()
    //{
    //    Vector3 currentPos = transform.position;
    //    Gizmos.color = Color.green;
    //    for (int i = 0; i < CatPattern.Count; i++)
    //    {
    //        if (CatPattern.Count == 0) return;
    //        Gizmos.DrawLine(currentPos, currentPos + CatPattern[i] * Grid.Instance.SetPlayerSpeed());
    //        currentPos += CatPattern[i] * Grid.Instance.SetPlayerSpeed();
    //    }
    //    Gizmos.color = Color.red;
    //    if (reverse)
    //    {
    //        if (ReversePath.Count == 0)
    //            return;
    //        Gizmos.DrawSphere(transform.position + (ReversePath[0] * Grid.Instance.SetPlayerSpeed()), 0.2f);
    //    }

    //    else
    //        Gizmos.DrawSphere(transform.position + (CatPattern[0] * Grid.Instance.SetPlayerSpeed()), 0.2f);
    //}
#endif
}
