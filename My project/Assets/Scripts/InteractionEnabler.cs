using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEnabler : MonoBehaviour
{
    GameObject obj;
    GameObject player;
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
        GridController.Instance.DisableTrigger();
        obj.GetComponent<MovableObject>().ShowTrigger(null);
        
    }
}
