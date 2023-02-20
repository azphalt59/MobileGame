using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance;
    public List<GameObject> objectsTrigger;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    public void DisableTrigger()
    {
        foreach (GameObject item in objectsTrigger)
        {
            item.SetActive(false);
        }
        objectsTrigger.Clear();
    }
}
