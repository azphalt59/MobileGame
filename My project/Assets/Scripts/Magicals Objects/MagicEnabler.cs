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
           

}
