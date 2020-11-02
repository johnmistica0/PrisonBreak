using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField textField;
    public Text textObj;
    public static string doorKey = "";
    private string input;

    public void OnSubmit()
    {
        input = textField.text;
        Debug.Log(input);
        textField.text = null;
        if(input == doorKey)
        {
            MoveCharacter.levelKey++;
            string sceneString = "Level" + MoveCharacter.levelKey.ToString() + "Scene";
            textObj.text = "Access Granted";
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: sceneString);
        }
        else
        {
            textObj.text = "Incorrect password";
        }
    }
    
}
