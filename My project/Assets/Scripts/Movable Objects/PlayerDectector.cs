using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDectector : MonoBehaviour
{
    GameObject obj;
    [SerializeField] public GameObject triggerLinkToThis;
    [SerializeField] private GameObject interactionEnabler;
    private void Start()
    {
        float scale = Grid.Instance.SetPlayerSpeed();
        obj = transform.parent.transform.parent.gameObject;
        obj.GetComponent<MovableObject>().DetectorsCollider.Add(GetComponent<Collider>());
        transform.localPosition = new Vector3(transform.localPosition.x * scale / obj.transform.localScale.x, transform.localPosition.y, transform.localPosition.z * scale / obj.transform.localScale.z);
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            interactionEnabler.SetActive(true);
        }
        if(other.gameObject.GetComponent<MovableObject>() != null || other.gameObject.GetComponent<MagicDestroyable>() != null || other.gameObject.GetComponent<CatMovement>())
        {
            obj.GetComponent<MovableObject>().HideThisTrigger(triggerLinkToThis);
        }
        
    }
}
