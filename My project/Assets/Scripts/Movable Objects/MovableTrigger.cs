using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTrigger : MonoBehaviour
{
    GameObject obj;
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
        PlayerController.Instance.gameObject.transform.position = obj.transform.position;
        obj.transform.position = transform.position;
        obj.GetComponent<MovableObject>().HideTrigger();
    }
    
}
