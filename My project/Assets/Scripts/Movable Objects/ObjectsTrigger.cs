using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsTrigger : MonoBehaviour
{
   
    private void OnEnable()
    {
        if(GridController.Instance != null)
        GridController.Instance.objectsTrigger.Add(gameObject);
    }
}
