using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEnabler : MonoBehaviour
{
    GameObject obj;
    GameObject player;
    public bool isActive = false;
    public int indexPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        obj = transform.parent.gameObject;
        player = PlayerController.Instance.gameObject;
    }
    private void OnEnable()
    {
        obj = transform.parent.gameObject;
        GridController.Instance.interactionEnablers.Add(this);
    }
    
    // Update is called once per frame
    private void OnMouseDown()
    {
        OnClick();
    }
    public void OnClick()
    {
        isActive = !isActive;
        if(isActive)
        {
            PlayerController.Instance.DisablePlayerTrigger();
            GridController.Instance.DisableTrigger();
            obj.GetComponent<MovableObject>().ShowTrigger(null);
            obj.GetComponent<MovableObject>().HideSideTrigger(indexPlayer);
        }
        else
        {
            //GridController.Instance.DisableInteractionEnablers();
            obj.GetComponent<MovableObject>().HideTrigger();
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
