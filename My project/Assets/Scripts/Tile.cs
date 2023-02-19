using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject tileGround;
    public GameObject triggerMovement;
    public int gridIndex;
    public bool isFree = true;
    public Material freeMat;

    private void Update()
    {
        if (!isFree) tileGround.GetComponent<MeshRenderer>().material = GridManager.Instance.obstacleMat;
        else tileGround.GetComponent<MeshRenderer>().material = freeMat;
    }
}
