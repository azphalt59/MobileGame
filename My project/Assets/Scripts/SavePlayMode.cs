using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class SavePlayMode : MonoBehaviour
{
    public bool SaveWhenPlayMode = false;
    // This function is called when the script is loaded or a value is changed in the Inspector (in edit mode).
    private void OnValidate()
    {
        // Check if we're in play mode and the scene has been modified
        if (EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode && GUI.changed)
        {
            Debug.Log("modifs");
            // Save the changes to the scene
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }

    // This function is called when the application is stopped
    private void OnApplicationQuit()
    {
        Debug.Log("QUIT");
        // Check if we're in play mode and the scene has been modified
        if (EditorApplication.isPlaying)
        {
            //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            // Save the changes to the scene
            EditorSceneManager.SaveOpenScenes();
            Debug.Log("Save la scène");
        }
    }
}
