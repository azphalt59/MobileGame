using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicEnabler : MonoBehaviour
{
    [SerializeField] private GameObject magicalObject;

    private void OnEnable()
    {
        GridController.Instance.magicEnablers.Add(this);
    }
    private void OnMouseDown()
    {
        OnClick();
    }
    public void OnClick()
    {
        gameObject.SetActive(false);
        PlayerController.Instance.DisablePlayerTrigger();
        GridController.Instance.DisableMagicalsEnablers();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableTrigger();
        UseMagicScroll();
    }
    public void UseMagicScroll()
    {
        GameManager.Instance.MagicScrollCount--;
        magicalObject.transform.DOScale(0, GameManager.Instance.magicalDestructionDuration).OnComplete(MovePlayer);
    }
    public void MovePlayer()
    {
        PlayerController.Instance.PlayerMesh.transform.LookAt(PlayerController.Instance.gameObject.transform.position + magicalObject.transform.position);
        if (GridController.Instance.cats.Count > 0)
        {
            PlayerController.Instance.gameObject.transform.DOMove(magicalObject.transform.position, GameManager.Instance.movementDuration)
                .OnComplete(GridController.Instance.ExecuteCatsMovement);
                //.OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
        } 
        else
        {
            PlayerController.Instance.gameObject.transform.DOMove(magicalObject.transform.position, GameManager.Instance.movementDuration)
                .OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);

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
