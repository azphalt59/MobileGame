using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTriggerMovement : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        float scale = Grid.Instance.SetPlayerSpeed();
        player = transform.parent.transform.parent.gameObject;
        transform.localPosition = new Vector3(transform.localPosition.x * scale, transform.localPosition.y, transform.localPosition.z * scale);
    }

#if UNITY_EDITOR
    private void OnMouseDown()
    {
        OnClick();
    }
#endif
    public void OnClick()
    {
        Vector3 dir = transform.localPosition;
        PlayerController.Instance.RotatePlayer(dir);

        if (GridController.Instance.cats.Count != 0)
        {
            PlayerController.Instance.PlayerAnimator.SetBool("Run", true);
            player.transform.DOMove(transform.position, GameManager.Instance.movementDuration).OnComplete(OnPlayerMoveComplete);
        }
        else
        {
            PlayerController.Instance.PlayerAnimator.SetBool("Run", true);
            player.transform.DOMove(transform.position, GameManager.Instance.movementDuration).OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
        }

        player.GetComponent<PlayerController>().DisablePlayerTrigger();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableMagicalsEnablers();
        GridController.Instance.DisableTrigger();
    }
    public void OnPlayerMoveComplete()
    {
        PlayerController.Instance.PlayerAnimator.SetBool("Run", false);
        GridController.Instance.ExecuteCatsMovement();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            gameObject.SetActive(false);
        }
        if(other.gameObject.GetComponent<MagicDestroyable>() != null)
        {
            if(GameManager.Instance.MagicScrollCount <= 0)
            {
            }
            else
            {
                other.gameObject.GetComponent<MagicDestroyable>().ActionEnabler.SetActive(true);
            }
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
