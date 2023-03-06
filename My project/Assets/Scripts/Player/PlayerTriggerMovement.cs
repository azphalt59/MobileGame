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

    private void OnMouseDown()
    {
        OnClick();
    }
    public void OnClick()
    {
        Vector3 dir = transform.localPosition;
        Debug.Log(dir);
        PlayerController.Instance.RotatePlayer(dir);

        if (GridController.Instance.cats.Count != 0)
        {
            player.transform.DOMove(transform.position, GameManager.Instance.movementDuration).OnComplete(OnPlayerMoveComplete);
        }
        else
        {
            player.transform.DOMove(transform.position, GameManager.Instance.movementDuration).OnComplete(PlayerController.Instance.ResetPlayerTriggerMovement);
        }

        player.GetComponent<PlayerController>().DisablePlayerTrigger();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableMagicalsEnablers();
        GridController.Instance.DisableTrigger();
    }
    public void OnPlayerMoveComplete()
    {
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
