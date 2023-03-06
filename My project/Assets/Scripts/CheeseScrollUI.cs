using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheeseScrollUI : MonoBehaviour
{
    [SerializeField] private GameObject cheeseImage;
    [SerializeField] private GameObject scrollImage;
    public void HaveCheese()
    {
        cheeseImage.SetActive(true);
    }

    private void Update()
    {
        if (GameManager.Instance.MagicScrollCount > 0)
        {
            scrollImage.SetActive(true);
        }
        else
            scrollImage.SetActive(false);
    }
}
