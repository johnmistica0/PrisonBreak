using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EscapeMenu: MonoBehaviour {
    bool enabledMenu;
    GameObject escMenuContainer;
    GameObject resumeButton;
    GameObject optionsButton;
    GameObject quitButton;
    //text mesh
    GameObject panel;
    Canvas pauseMenu;

    Button resumeBtnScript;
    Button optionsBtnScript;
    Button quitBtnScript;
    GameObject resumeText;
    GameObject optionsText;
    GameObject quitText;
    

    void Start(){

        enabledMenu = false;
        setContainer();
        setBackground();
        addButtons();
        escMenuContainer.SetActive(enabledMenu);
    }

    public void toggleActive(){
        if(enabledMenu){
            disable();
            Time.timeScale = 1.0F;

        }else{
            enable();
            Time.timeScale = 0;
        }
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
         {
                toggleActive();
                
         }
    }


    private void setContainer(){
        escMenuContainer = new GameObject("EscapeMenuContainer");
        pauseMenu = escMenuContainer.AddComponent<Canvas>();
        escMenuContainer.AddComponent<CanvasScaler>();
        escMenuContainer.AddComponent<GraphicRaycaster>();
        
        pauseMenu.renderMode = RenderMode.ScreenSpaceCamera;
        pauseMenu.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pauseMenu.sortingOrder = 24;   
        pauseMenu.pixelPerfect = true;
    }
    



    private void setBackground(){
        Color transparentBlack = new Color(0F,0F,0F,0.3F);
        Image background;
        RectTransform rectTransform;
        panel = new GameObject("Panel");
        panel.transform.parent = escMenuContainer.transform;
        background = panel.AddComponent<Image>();
        background.color = transparentBlack;
        background.sprite = null;
        rectTransform = background.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 100);
        rectTransform.localPosition = new Vector3(0, 0, 0);
    }
    private void resumeOnClick(){
        toggleActive();
        
    }
    private void optionsOnClick(){
        Debug.Log("Options");
    }
    private void quitOnClick(){
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1.0F;
    }
    


    private void addButtons(){
        resumeButton = new GameObject("ResumeButton");
        optionsButton = new GameObject("OptionsButton");
        quitButton = new GameObject("QuitButton");
        resumeText = new GameObject("ResumeText");
        optionsText = new GameObject("OptionsText");
        quitText = new GameObject("QuitText");
        //Setting parents to the panel containing the background
        resumeButton.transform.parent = panel.transform;
        optionsButton.transform.parent = panel.transform;
        quitButton.transform.parent = panel.transform;
        //setting parents for text in the buttons
        resumeText.transform.parent = resumeButton.transform;
        optionsText.transform.parent = optionsButton.transform;
        quitText.transform.parent = quitButton.transform;
        


        //Adding button and Text mesh components
        resumeBtnScript = resumeButton.AddComponent<Button>();
        optionsBtnScript = optionsButton.AddComponent<Button>();
        quitBtnScript = quitButton.AddComponent<Button>();
        
        //set images in buttons and next the textmesh
        Color transparentBlack = new Color(0F,0F,0F,0.7F);
        Image resumeImage = resumeButton.AddComponent<Image>();
        Image optionsImage = optionsButton.AddComponent<Image>();
        Image quitImage = quitButton.AddComponent<Image>();
        resumeImage.color = transparentBlack;
        optionsImage.color = transparentBlack;
        quitImage.color = transparentBlack;

        //setting text mesh in text object
        TextMeshPro resumeTextMesh = resumeText.AddComponent<TextMeshPro>();
        TextMeshPro optionsTextMesh = optionsText.AddComponent<TextMeshPro>();
        TextMeshPro quitTextMesh = quitText.AddComponent<TextMeshPro>();
        //Setting onClick
        resumeBtnScript.onClick.AddListener(resumeOnClick);
        optionsBtnScript.onClick.AddListener(optionsOnClick);
        quitBtnScript.onClick.AddListener(quitOnClick);


        //Setting text and font size
        resumeTextMesh.text = "Resume";
        optionsTextMesh.text = "Options";
        quitTextMesh.text = "Quit";
        resumeTextMesh.fontSize = 10;
        optionsTextMesh.fontSize = 10;
        quitTextMesh.fontSize = 10;
        RectTransform rectTransform;
        //Setting their positions and clickable boxes
        rectTransform = resumeButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(5, 1.3F);
        rectTransform.localPosition = new Vector3(0,1.5F,0);
        rectTransform = optionsButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(5, 1.3F);
        rectTransform.localPosition = new Vector3(0,0,0);
        rectTransform = quitButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(5, 1.3F);
        rectTransform.localPosition = new Vector3(0,-1.5F,0);
        //Alignment Settings
        resumeTextMesh.alignment = TextAlignmentOptions.Center;
        optionsTextMesh.alignment = TextAlignmentOptions.Center;
        quitTextMesh.alignment = TextAlignmentOptions.Center;
        //moving the text to the front of everything
        resumeTextMesh.sortingOrder = 25;
        optionsTextMesh.sortingOrder = 25;
        quitTextMesh.sortingOrder = 25;



    }
    private void disable(){
        escMenuContainer.SetActive(false);
        enabledMenu = false;
    }
    private void enable(){
        escMenuContainer.SetActive(true);
        enabledMenu = true;
    }



    private void pauseGame(){

    }
    private void startGame(){

    }
}