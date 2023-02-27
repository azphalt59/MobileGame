using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float movementDuration = 2f;
    public float magicalDestructionDuration = 3f;
    public int MagicScrollCount = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    
    
    void Update()
    {
        
    }
}
