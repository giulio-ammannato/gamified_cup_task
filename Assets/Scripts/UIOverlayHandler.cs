using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOverlayHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    //public Transform gameOverPrefab;

    private Transform clone;

    private string diOrOf = " of ";

    Text currentMoneyText;
    Text trialCounterText;

    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(this);

        //transform.Find("GameOverMenu").gameObject.SetActive(false);

        clone = transform.Find("GameOverMenu");
        clone.gameObject.SetActive(false);

        //get the current money, first look for the text element 
        currentMoneyText = GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>();

        //get the current trial, first look for the text element 
        trialCounterText = GameObject.FindGameObjectWithTag("TrialCounterText").GetComponent<Text>();
     
    }
	
	// Update is called once per frame
	void Update () {

        //set the current moeny and the current trial in the UI elements
        //update the current number of trial
        trialCounterText.text = "" + (Statistics.stats.cupTaskStats.currentTrailsOnAllTask-1) + diOrOf + ((Statistics.stats.cupTaskStats.totalNumberOfTrials - 1) * 2);
        //update the current money
        currentMoneyText.text = "" + Statistics.stats.playerStats.currentMoney;

        if (clone.gameObject.activeSelf)
        {
            Canvas.ForceUpdateCanvases();
        }

    }

    public void ActivateGameOverMenu()
    {

        //if it is the last scene before game over, in this case the last scene is the loss cup task
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LossCupTask"))
        {
            //update the game over menu with the relevant stats and display it
            clone.Find("NeutralText").GetComponent<Text>().text = "Netraul choices: " + Statistics.stats.playerStats.numberOfNeutralEVChoices;
            clone.Find("GoodChoiceText").GetComponent<Text>().text = "Good choices: " + Statistics.stats.playerStats.numberOfGoodEVChoices;
            clone.Find("BadChoiceText").GetComponent<Text>().text = "Bad choices: " + Statistics.stats.playerStats.numberOfBadEVChoices;
            clone.Find("RiskyChoicesText").GetComponent<Text>().text = "Risky choices: " + Statistics.stats.playerStats.numberOfRiskyChoices;
            clone.Find("SafeChoicesText").GetComponent<Text>().text = "Safe choices: " + Statistics.stats.playerStats.numberOfSafeChoices;
            clone.Find("MoneyDifferenceText").GetComponent<Text>().text = "Money difference: " + (Statistics.stats.playerStats.currentMoney - Statistics.stats.playerStats.startingMoney);

            //the task is over, set the end time of the task
            Statistics.stats.playerStats.taskEndtime = DateTime.Now;
            //write the end on the trial
            HandleTextFile.handleTextFile.WriteStringOnSameLine((string)Statistics.stats.playerStats.taskEndtime.ToString());
            HandleTextFile.handleTextFile.WriteString((string)Statistics.stats.playerStats.taskEndtime.ToString());

            clone.gameObject.SetActive(true);
            Time.timeScale = 0;

        }
        //else load the next scene
        else
        {
           
            //reset the trial counter
            Statistics.stats.cupTaskStats.currentTrail = 1;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            LevelManager.levelManager.LoadNextLevelWithFading(SceneManager.GetActiveScene().buildIndex + 1);
           

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //find the corerct children
        if (clone.Find("FinishButton").gameObject == eventData.pointerEnter | clone.Find("FinishButton").Find("FinishText").gameObject == eventData.pointerEnter)
        {
            //goes back to the main menu
            Destroy(GameObject.Find("Statistics"));
            Destroy(GameObject.Find("HandleTextFile"));
            Destroy(GameObject.Find("UIOverlay"));
            Destroy(GameObject.Find("LevelManager"));

            SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //find the corerct children
        if (clone.Find("FinishButton").gameObject == eventData.pointerEnter | clone.Find("FinishButton").Find("FinishText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            clone.Find("FinishButton").Find("FinishText").GetComponent<Text>().color = Color.red;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //find the corerct children
        if (clone.Find("FinishButton").gameObject == eventData.pointerEnter | clone.Find("FinishButton").Find("FinishText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            clone.Find("FinishButton").Find("FinishText").GetComponent<Text>().color = Color.white;
        }
    }
}
