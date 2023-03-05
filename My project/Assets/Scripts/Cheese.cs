using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour, IPickable
{
    public void Pick()
    {
        GameManager.Instance.haveCheese = true;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Pick();
            Destroy(this.gameObject);
        }
    }
}
