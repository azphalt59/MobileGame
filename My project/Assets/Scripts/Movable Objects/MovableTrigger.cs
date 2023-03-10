using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovableTrigger : MonoBehaviour
{
    GameObject obj;
    Vector3 newPlayerPos;
    public int TriggerIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        float scale = Grid.Instance.SetPlayerSpeed();
        obj = transform.parent.transform.parent.gameObject;
        transform.localPosition = new Vector3(transform.localPosition.x * scale / obj.transform.localScale.x, transform.localPosition.y, transform.localPosition.z * scale / obj.transform.localScale.z);
        gameObject.SetActive(false);
        for (int i = 0; i < obj.GetComponent<MovableObject>().MovableTriggers.Count; i++)
        {
            if(obj.GetComponent<MovableObject>().MovableTriggers[i].gameObject == this.gameObject)
            {
                TriggerIndex = i;
            }
        }
    }
#if UNITY_EDITOR
    private void OnMouseDown()
    {
        OnClick();
    }
#endif
    public void OnClick()
    {
        obj.GetComponent<MovableObject>().HideTrigger();
        GridController.Instance.DisableTrigger();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableMagicalsEnablers();
        PlayerController.Instance.DisablePlayerTrigger();
        
        if(obj.GetComponent<MovableObject>().InteractionEnabler.GetComponent<InteractionEnabler>().indexPlayer == TriggerIndex)
        {
            Vector3 objMvt = obj.transform.position - transform.position;
           
            //Debug.Log(transform.position - (obj.transform.position - transform.position) * 2);
            //Debug.Log(objMvt);

            PlayerController.Instance.RotatePlayer(obj.transform.position - PlayerController.Instance.gameObject.transform.position);
            PlayerController.Instance.PlayerMesh.transform.eulerAngles -= new Vector3(0,180,0);
            PlayerController.Instance.PlayerAnimator.SetBool("Run", true);
            PlayerController.Instance.gameObject.transform.DOMove(PlayerController.Instance.gameObject.transform.position - objMvt, GameManager.Instance.movementDuration).OnComplete(PullObject);
        }
        else
        {
            Vector3 newObjPos = transform.position;
            newPlayerPos = obj.transform.position;
            PlayerController.Instance.PlayerAnimator.SetBool("Push", true);
            AudioManager.Instance.Play("TireBloc");
            obj.transform.DOMove(newObjPos, GameManager.Instance.movementDuration).OnComplete(OnObjMoveComplete);
        }
        
        obj.GetComponent<MovableObject>().RefreshColliders();
    }
    private void PullObject()
    {
        PlayerController.Instance.PlayerMesh.transform.eulerAngles -= new Vector3(0, 180, 0);
        //if (PlayerController.Instance.PlayerAnimator.GetBool("Run") == true)
        //{
        //    PlayerController.Instance.PlayerAnimator.SetBool("Run", false);
        //}
        
        PlayerController.Instance.PlayerAnimator.SetBool("Push", true);
        AudioManager.Instance.Play("TireBloc");
        obj.transform.DOMove(transform.position, GameManager.Instance.movementDuration).OnComplete(OnPlayerMoveComplete);
    }
    private void OnObjMoveComplete()
    {
        if(PlayerController.Instance.PlayerAnimator.GetBool("Push") == true)
        {
            PlayerController.Instance.PlayerAnimator.SetBool("Push", false);
        }
        if (PlayerController.Instance.PlayerAnimator.GetBool("Run") == true)
        {
            PlayerController.Instance.PlayerAnimator.SetBool("Run", false);
        }

        PlayerController.Instance.PlayerAnimator.SetBool("Run", true);
        PlayerController.Instance.RotatePlayer(obj.transform.position - PlayerController.Instance.gameObject.transform.position);
        PlayerController.Instance.gameObject.transform.DOMove(newPlayerPos, GameManager.Instance.movementDuration).OnComplete(OnPlayerMoveComplete);
    }
    private void OnPlayerMoveComplete()
    {
        if (PlayerController.Instance.PlayerAnimator.GetBool("Push") == true)
        {
            PlayerController.Instance.PlayerAnimator.SetBool("Push", false);
        }
        if (PlayerController.Instance.PlayerAnimator.GetBool("Run") == true)
        {
            PlayerController.Instance.PlayerAnimator.SetBool("Run", false);
        }
        if (GridController.Instance.cats.Count > 0)
        {
            GridController.Instance.ExecuteCatsMovement();
        } 
        else
        {
            PlayerController.Instance.ResetPlayerTriggerMovement();
        }
            
    }
    public void CheckBehindCol()
    {
        if(TriggerIndex == 0)
        {

        }
    }
    private void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // L'objet a ?t? touch?
                    if (hit.collider.gameObject == gameObject)
                    {
                        OnClick();
                    }
                }
            }
        }

    }
    
}
