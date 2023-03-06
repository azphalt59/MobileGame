using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject winUI;
    public void PlayerLose()
    {
        loseUI.SetActive(true);
    }
    public void ResetLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Nextlevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex) + 1 <= SceneManager.sceneCountInBuildSettings+1)
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
        }
        else
        {
            Debug.Log((SceneManager.GetActiveScene().buildIndex) + 1 + " est l'id de la prochaine sc�ne");
            Debug.Log(SceneManager.sceneCountInBuildSettings + " est le nombre total de sc�nes");
        }
    }
    public void PlayerWin()
    {
        winUI.SetActive(true);
    }
}
