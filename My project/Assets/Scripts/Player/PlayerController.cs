using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    private float speed;
    public List<PlayerTriggerMovement> playerTriggerMovements;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Grid.Instance.SetPlayerSpeed();
    }

    public void ResetPlayerTriggerMovement()
    {
        foreach (PlayerTriggerMovement item in playerTriggerMovements)
        {
            item.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
