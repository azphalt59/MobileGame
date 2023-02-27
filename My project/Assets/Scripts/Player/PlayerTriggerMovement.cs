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
        //player.transform.position = transform.position;
        player.transform.DOMove(transform.position, GameManager.Instance.movementDuration).OnComplete(OnPlayerMoveComplete);
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
}
