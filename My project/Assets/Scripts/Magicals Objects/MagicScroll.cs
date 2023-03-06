using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScroll : MonoBehaviour, IPickable
{
    public void Pick()
    {
        GameManager.Instance.MagicScrollCount++;
        AudioManager.Instance.Play("PaperPickUp");
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            Pick();
            Destroy(this.gameObject);
        }
    }
}
