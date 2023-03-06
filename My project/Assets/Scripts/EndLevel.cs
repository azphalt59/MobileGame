using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            if (GameManager.Instance.haveCheese)
            {
                AudioManager.Instance.Play("Win");
                GameManager.Instance.LoseCon.PlayerWin();
            }
        }
    }
}
