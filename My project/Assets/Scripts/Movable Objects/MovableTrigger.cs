using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovableTrigger : MonoBehaviour
{
    GameObject obj;
    Vector3 newPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        float scale = Grid.Instance.SetPlayerSpeed();
        obj = transform.parent.transform.parent.gameObject;
        transform.localPosition = new Vector3(transform.localPosition.x * scale / obj.transform.localScale.x, transform.localPosition.y, transform.localPosition.z * scale / obj.transform.localScale.z);
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        OnClick();
    }
    public void OnClick()
    {
        obj.GetComponent<MovableObject>().HideTrigger();
        GridController.Instance.DisableTrigger();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableMagicalsEnablers();
        PlayerController.Instance.DisablePlayerTrigger();


        Vector3 newObjPos = transform.position;
        newPlayerPos = obj.transform.position;
        obj.transform.DOMove(newObjPos, GameManager.Instance.movementDuration).OnComplete(OnObjMoveComplete);


        obj.GetComponent<MovableObject>().RefreshColliders();
    }
    private void OnObjMoveComplete()
    {
        
        PlayerController.Instance.gameObject.transform.DOMove(newPlayerPos, GameManager.Instance.movementDuration).OnComplete(OnPlayerMoveComplete);
    }
    private void OnPlayerMoveComplete()
    {
        if (GridController.Instance.cats.Count > 0)
        {
            Debug.Log("ya des chats");
            GridController.Instance.ExecuteCatsMovement();
        } 
        else
        {
            Debug.Log("pas de chat");
            PlayerController.Instance.ResetPlayerTriggerMovement();
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
                    // L'objet a été touché
                    if (hit.collider.gameObject == gameObject)
                    {
                        OnClick();
                    }
                }
            }
        }

    }
}
