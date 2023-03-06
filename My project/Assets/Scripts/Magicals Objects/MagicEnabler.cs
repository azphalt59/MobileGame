using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicEnabler : MonoBehaviour
{
    [SerializeField] private GameObject magicalObject;
    [SerializeField] private ParticleSystem FireFx;

    private void OnEnable()
    {
        if(GridController.Instance != null)
            GridController.Instance.magicEnablers.Add(this);
    }
#if UNITY_EDITOR
    private void OnMouseDown()
    {
        OnClick();
    }
#endif
    public void OnClick()
    {
        gameObject.SetActive(false);
        PlayerController.Instance.DisablePlayerTrigger();
        GridController.Instance.DisableMagicalsEnablers();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableTrigger();
        UseMagicScroll();
        PlayerController.Instance.PlayerAnimator.SetBool("Push", true);
    }
    public void UseMagicScroll()
    {
        GameManager.Instance.MagicScrollCount--;
        PlayerController.Instance.RotatePlayer(magicalObject.transform.position - PlayerController.Instance.PlayerMesh.gameObject.transform.position);
        FireFx.Play();
        AudioManager.Instance.Play("DestructionBloc");
        magicalObject.transform.DOScale(0, GameManager.Instance.magicalDestructionDuration).OnComplete(MovePlayer);
    }
    public void MovePlayer()
    {
        PlayerController.Instance.PlayerAnimator.SetBool("Push", false);
        PlayerController.Instance.PlayerAnimator.SetBool("Run", true);
       
      
        if (GridController.Instance.cats.Count > 0)
        {
            PlayerController.Instance.gameObject.transform.DOMove(magicalObject.transform.position, GameManager.Instance.movementDuration)
                .OnComplete(OnMoveComplete);
                //.OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
        } 
        else
        {
            PlayerController.Instance.gameObject.transform.DOMove(magicalObject.transform.position, GameManager.Instance.movementDuration)
                .OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);

        }
    }

    public void OnMoveComplete()
    {
        PlayerController.Instance.PlayerAnimator.SetBool("Run", false);
        GridController.Instance.ExecuteCatsMovement();
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
