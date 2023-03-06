using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheeseScrollUI : MonoBehaviour
{
    [SerializeField] private GameObject cheeseImage;
    [SerializeField] private TextMeshProUGUI scrollCountText;
    public void HaveCheese()
    {
        cheeseImage.SetActive(true);
    }

    private void Update()
    {
        scrollCountText.text = " " + GameManager.Instance.MagicScrollCount;
    }
}
