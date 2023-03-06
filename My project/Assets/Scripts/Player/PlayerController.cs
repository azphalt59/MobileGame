using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Animator PlayerAnimator;
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
    public GameObject PlayerMesh;
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

        PlayerAnimator.SetBool("Run", false);
        PlayerAnimator.SetBool("Push", false);
    }
  
    public void DisablePlayerTrigger()
    {
        foreach (PlayerTriggerMovement item in playerTriggerMovements)
        {
            item.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    public void RotatePlayer(Vector3 mvtTarget)
    {
        
        if(mvtTarget.z < 0)
        {
            PlayerMesh.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (mvtTarget.z > 0)
        {
            PlayerMesh.transform.eulerAngles = new Vector3(0, 270, 0);
        }
        if (mvtTarget.x < 0)
        {
            PlayerMesh.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (mvtTarget.x > 0)
        {
            PlayerMesh.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
