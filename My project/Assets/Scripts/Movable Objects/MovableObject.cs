using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public List<MovableTrigger> MovableTriggers;
    public List<PlayerDectector> PlayerDectectors;
    public List<Collider> DetectorsCollider;
    public GameObject InteractionEnabler;
    public void ShowTrigger(GameObject triggeredtrigger)
    {
        foreach (MovableTrigger item in MovableTriggers)
        {
            if(item.gameObject != triggeredtrigger)
            item.gameObject.SetActive(true);

        }
        foreach (PlayerDectector item in PlayerDectectors)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void HideThisTrigger(GameObject go)
    {
        go.SetActive(false);
    }
    public void HideTrigger()
    {
        foreach (MovableTrigger item in MovableTriggers)
        {
                item.gameObject.SetActive(false);
        }
        foreach (PlayerDectector item in PlayerDectectors)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void HideSideTrigger(int indexTrigger)
    {
        if(indexTrigger == 0 || indexTrigger == 1)
        {
            MovableTriggers[2].gameObject.SetActive(false);
            MovableTriggers[3].gameObject.SetActive(false);
        }
        if (indexTrigger == 2 || indexTrigger == 3)
        {
            MovableTriggers[0].gameObject.SetActive(false);
            MovableTriggers[1].gameObject.SetActive(false);
        }

    }
    public void RefreshColliders()
    {
        foreach (Collider collider in DetectorsCollider)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
    }

}
