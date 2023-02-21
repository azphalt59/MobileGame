using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        player.transform.position = transform.position;
        player.GetComponent<PlayerController>().ResetPlayerTriggerMovement();
        GridController.Instance.DisableInteractionEnablers();
        GridController.Instance.DisableTrigger();
        GridController.Instance.ExecuteCatsMovement();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            //Debug.Log("un obstacle est détecté sur " + gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
