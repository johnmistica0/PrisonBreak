using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void OnSubmit()
    {
        string sceneString = "Level" + MoveCharacter.levelKey.ToString() + "Scene";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: sceneString);
    }
}
