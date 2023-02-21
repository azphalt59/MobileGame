using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public List<MovableTrigger> MovableTriggers;
    public List<PlayerDectector> PlayerDectectors;
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
}
