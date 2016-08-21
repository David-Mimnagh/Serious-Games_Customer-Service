using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using SimpleJSON;
using System;
using System.Text.RegularExpressions;

public class UIManagerScript : MonoBehaviour {

    //EngAGe
    private const int idSG = 291;


    // MenuScene
    public Text txt_title;
    public Text txt_description;
    public Text txt_listbestplayers;
    public Animator startButton;
    public Animator settingsButton;
    public Animator dialog;
    public Animator imgLevel;
    public Animator contentPanel;
    public Animator gearImage;
    public Slider sdr_level;
    //public GameObject badgeDialog;
    //public GameObject infoDialog;
    //public GameObject leaderboardDialog;

    // LoginScene
    public Text txtUsername;
	public InputField txtPassword;
	public Text txtLoginParagraph;
	
	private static string username;
	private static string password;

    //Parameter Scene
    public Text txtWelcome;
    public InputField inputPrefab;
    public GameObject inputParent;
    private List<InputField> inputFields = new List<InputField>();
    
    // game scene
    public Conversations convo;
    public Texture2D coinIconTexture;
    public Texture2D livesIconTexture;
    public Texture2D euIconTexture;

    public Text pointsLabel;
    public Text euLabel;
    public GameObject restartWinDialog;
	public GameObject restartLoseDialog;
	
	public GameObject feedbackDialog;
	
	//public Image life1;
	//public Image life2;
	//public Image life3;

    public Text txt_Feedback;

	//private static int difficulty = 2;

	//public int getDifficulty()
	//{
	//	return difficulty;
	//}

	void Start()
	{
        if (Application.loadedLevelName.Equals("LoginScene"))
        {
            txtLoginParagraph.enabled = (EngAGe.E.getErrorCode() > 0);
            txtLoginParagraph.text = EngAGe.E.getError();
        }
        else if (Application.loadedLevelName.Equals("ParametersScene"))
        {
            //txtWelcome.text = "Welcome " + username;
            int i = 0;
            // loop on all the player's characteristics needed
            foreach (JSONNode param in EngAGe.E.getParameters())
            {
                // creates a text field in the panel parameters of the scene
                InputField inputParam = (InputField)Instantiate(inputPrefab);
                inputParam.name = "input_" + param["name"];
                inputParam.transform.SetParent(inputParent.transform);
                inputParam.text = param["question"];

                // position them, aligned vertically
                RectTransform transform = inputParam.transform as RectTransform;
                transform.anchoredPosition = new Vector2(0, 20 - i * 50);
                // save the input in the input array 
                inputFields.Add(inputParam);
                i++;
            }
        }
        if (Application.loadedLevelName.Equals("MainMenu"))
        {
            // retrieve data about the game, the badges earned and the leader board
           // StartCoroutine(EngAGe.E.getGameDesc(idSG));
           // StartCoroutine(EngAGe.E.getBadgesWon(idSG));
            //StartCoroutine(EngAGe.E.getLeaderboard(idSG));

            // retrieve EngAGe data about the game
            //StartCoroutine(EngAGe.E.getGameDesc(idSG));
            //StartCoroutine(EngAGe.E.getBadgesWon(idSG));

            //RectTransform transform = contentPanel.gameObject.transform as RectTransform;
            //Vector2 position = transform.anchoredPosition;
            //position.y -= transform.rect.height;
            //transform.anchoredPosition = position;

            //// close all three windows
            //badgeDialog.SetActive(false);
            //infoDialog.SetActive(false);
            //leaderboardDialog.SetActive(false);
        }
        else if (Application.loadedLevelName.Equals("scene2withguy"))
        {
            //restartWinDialog.SetActive(false);
            //restartLoseDialog.SetActive(false);
            //feedbackDialog.SetActive(false);

            UpdateScores();
        }
	} 

	public void GoToMenu()
	{
        Debug.Log(EngAGe.E.getParameters());
        // for each parameter required
        foreach (JSONNode param in EngAGe.E.getParameters())
        {
            // find the corresponding input field
            foreach (InputField inputField in inputFields)
            {
                if (inputField.name == "input_" + param["name"])
                {
                    // and store the value in the JSON
                    param.Add("value", inputField.textComponent.text);
                }
            }
        }
        Debug.Log(EngAGe.E.getParameters());
        Application.LoadLevel("MainMenu");
    }
	
	public void StartGame()
	{
        StartCoroutine(EngAGe.E.startGameplay(idSG, "scene2withguy"));
    }

    public void GetStarted()
    {
        username = txtUsername.text;
        password = txtPassword.text;

        StartCoroutine(EngAGe.E.loginStudent(idSG, username, password, "LoginScene", "MainMenu", "ParametersScene"));
    }

    public void GetStartedGuest()
	{
        StartCoroutine(EngAGe.E.guestLogin(idSG, "LoginScene", "ParametersScene"));
    }

	
	public void OpenFeedback()
	{
		feedbackDialog.SetActive (true);
	}

    //public void CloseFeedback()
    //{
    //	//feedbackDialog.SetActive (false);
    //}

    public void UpdateFeedback(JSONArray feedbackReceived)
    {

        foreach (JSONNode f in feedbackReceived)
        {
            // set color to write line into
            string color = "black";
            if (string.Equals( f["type"], "POSITIVE"))
                color = "green";
            if (string.Equals(f["type"], "NEGATIVE"))
                color = "red";
            txt_Feedback.text += "<color=\"" + color + "\">" + f["message"] + "</color>\n\n";
    
            }
    }

    public void ActionAssessed(JSONNode jsonReturned)
    {
        UpdateScores();
        UpdateFeedback(jsonReturned["feedback"].AsArray);
    }

    
           

    public void UpdateScores()
    {
        foreach (JSONNode score in EngAGe.E.getScores())
        {
            string scoreName = score["name"];
            string scoreValue = score["value"];

            if (string.Equals(scoreName, "score"))
            {
                float.Parse(scoreValue).ToString();
            }
            //else if (string.Equals(scoreName, "score"))
            //{
            //euLabel.text = float.Parse(scoreValue).ToString();
            //}
            //else if (string.Equals(scoreName, "rage"))
            //{
            //float livesFloat = float.Parse(scoreValue);
            //int lives = Mathf.RoundToInt(livesFloat);
            //life3.gameObject.SetActive(lives > 2);
            //life2.gameObject.SetActive(lives > 1);
            //life1.gameObject.SetActive(lives > 0);
            //}
        }
    }

    public void ExitToMenu()
	{
		Application.LoadLevel ("MenuScene");
	}
}


