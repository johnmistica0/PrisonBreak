using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FailureSequence : MonoBehaviour
{
    public void onTryAgainClick(){
        SceneManager.LoadScene("Level1Scene");

    }
    public void onExitClick(){
        SceneManager.LoadScene("MenuScene");
    }
}
