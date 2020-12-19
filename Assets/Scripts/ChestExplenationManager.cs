using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChestExplenationManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //the text file containing the intro
    public TextAsset introStory;

    //0 is main, 1 is gain, 2 is loss
    public int typeOfIntro;
   
    //the story divided in string for each new line
    string[] introParts;

    int pageCounter = 0;

    Transform circles;

    public Sprite coinTexture;
    public Sprite webTexture;
    Sprite sackTexture;

    Text leftAmountTex;
    Text rightAmountTex;

    Text leftProbabilityText;
    Text rightProbabilityText;

    //store all the bags of the cuptask
    Transform[] bags;



    // Use this for initialization
    void Start()
    {
        //otherwise it will update from the game stats
        if(typeOfIntro != 2)
        {
            Text trialCounter = GameObject.FindGameObjectWithTag("TrialCounterText").GetComponent<Text>();
            //TODO mettere un IF a seconda di lingua
            //trialCounter.text = "1 di 54";

            trialCounter.text = "1 of 54";
        }

        leftProbabilityText = transform.Find("CupTask5Intro").gameObject.transform.Find("Header").gameObject.transform.Find("LeftProbabilityButton").gameObject.transform.Find("LeftProbabilityText").GetComponent<Text>();

        rightProbabilityText = transform.Find("CupTask5Intro").gameObject.transform.Find("Header").gameObject.transform.Find("RightProbabilityButton").gameObject.transform.Find("RightProbabilityText").GetComponent<Text>();

        //get the Text element on the left amount
        leftAmountTex = GameObject.FindGameObjectWithTag("LeftText").GetComponent<Text>();
        rightAmountTex = GameObject.FindGameObjectWithTag("RightText").GetComponent<Text>();

        Transform bagsParent = GameObject.FindGameObjectWithTag("Bags").transform;

        //create the bags array
        bags = new Transform[bagsParent.childCount];

        //populate the bags array
        for (int i = 0; i < bagsParent.childCount; i++)
        {
            bags[i] = bagsParent.GetChild(i);

        }

        sackTexture = GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite;

        circles = GameObject.FindGameObjectWithTag("Circles").GetComponent<Transform>();
        //set all circles as inactive
        for (int i = 0; i< circles.childCount;i++)
        {
            circles.GetChild(i).gameObject.SetActive(false);
        }


        //on wakeup the OK and next button are not visible
        transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject.SetActive(false);
        transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject.SetActive(false);

        //create the different headers from the fiel
        introParts = introStory.text.Split('\n');

        //set the first header
        transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text = introParts[pageCounter];

        //the game is frozen untill the intro is read
        Time.timeScale = 0;


    }

    public void SetStory(TextAsset textAsset)
    {
        introStory = textAsset;
    }
  

   


    // Update is called once per frame
    void Update()
    {
        if(typeOfIntro == 0)
        {
            UpdateMainIntroduction();
        }
        if(typeOfIntro == 1)
        {
            UpdateGainIntro();
        }
        if (typeOfIntro == 2)
        {
            UpdateLossIntro();
        }

        Canvas.ForceUpdateCanvases();

    }

    void UpdateGainIntro()
    {
        //TODO mettere un IF a seconda di lingua
        //leftProbabilityText.text = "1 su 5";
        //rightProbabilityText.text = "5 su 5";

        leftProbabilityText.text = "1 on 5";
        rightProbabilityText.text = "5 on 5";
        leftAmountTex.text = "+3";
        rightAmountTex.text = "+1";

        //GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = sackTexture;


        for (int i = 0; i < circles.childCount; i++)
        {
            circles.GetChild(i).gameObject.SetActive(false);
        }

        if(pageCounter == 0)
        {
            transform.Find("CupTask5Intro").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("CupTask5Intro").gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = sackTexture;
        }
            

        //sacchetti sx
        if (pageCounter == 1)
        {
            circles.GetChild(3).gameObject.SetActive(true);
        }

        //sacchetti sx probabilita'
        else if (pageCounter == 2)
        {
            circles.GetChild(5).gameObject.SetActive(true);
        }

        //sacchetti sx quantita'
        else if (pageCounter == 3)
        {
            circles.GetChild(4).gameObject.SetActive(true);
        }

        //saccehtti dx e probabilita'
        else if (pageCounter == 4)
        {
            circles.GetChild(0).gameObject.SetActive(true);
            //circles.GetChild(2).gameObject.SetActive(true);
        }

        //soldi dx
        else if (pageCounter == 5)
        {
            circles.GetChild(1).gameObject.SetActive(true);
        }

        else if (pageCounter == 7)
        {
            //the red circle around the coin, we want it on
            circles.GetChild(6).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
        }
       

        //bottino e soldino
        if (pageCounter == 8)
        {
            circles.GetChild(6).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
            IntroMaster.introMaster.SetActiveUI("MoneyCounterArrow");
            IntroMaster.introMaster.SetActiveUI("MoneyCounterCircle");
        }
        else
        {
            IntroMaster.introMaster.SetInactiveUI("MoneyCounterArrow");
            IntroMaster.introMaster.SetInactiveUI("MoneyCounterCircle");
        }
        //ragnatela
        if (pageCounter == 9)
        {
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = webTexture;
            circles.GetChild(6).gameObject.SetActive(true);
        }


        //
        if (pageCounter > 5)
        {
            leftProbabilityText.text = "";
            rightProbabilityText.text = "";
        }


        //change money depending on the page
        if (pageCounter < 8)
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "0";
        }
        else
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "+3";
        }

    }

    void UpdateLossIntro()
    {
        //TODO mettere un IF a seconda di lingua
        //leftProbabilityText.text = "1 su 5";
        //rightProbabilityText.text = "5 su 5";

        circles.GetChild(1).gameObject.SetActive(true);
        circles.GetChild(4).gameObject.SetActive(true);
        leftAmountTex.color = Color.red;
        rightAmountTex.color = Color.red;
        leftAmountTex.text = "-3";
        rightAmountTex.text = "-1";
        leftProbabilityText.text = "1 on 5";
        rightProbabilityText.text = "5 on 5";

        /**
        if(Statistics.stats!= null)
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "" + Statistics.stats.playerStats.currentMoney;
            GameObject.FindGameObjectWithTag("TrialCounterText").GetComponent<Text>().text = "" + (Statistics.stats.cupTaskStats.currentTrailsOnAllTask - 1) + " di " + ((Statistics.stats.cupTaskStats.totalNumberOfTrials - 1) * 2);

        }
        **/


        for (int i = 0; i < circles.childCount; i++)
        {
            circles.GetChild(i).gameObject.SetActive(false);
        }

        if (pageCounter == 0)
        {
            transform.Find("CupTask5Intro").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("CupTask5Intro").gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = sackTexture;
        }


        //sacchetti sx
        if (pageCounter == 1)
        {
            circles.GetChild(3).gameObject.SetActive(true);
        }

        //sacchetti sx probabilita'
        else if (pageCounter == 2)
        {
            circles.GetChild(5).gameObject.SetActive(true);
        }

        //sacchetti sx quantita'
        else if (pageCounter == 3)
        {
            circles.GetChild(4).gameObject.SetActive(true);
        }

        //saccehtti dx e probabilita'
        else if (pageCounter == 4)
        {
            circles.GetChild(0).gameObject.SetActive(true);
            //circles.GetChild(2).gameObject.SetActive(true);
        }

        //soldi dx
        else if (pageCounter == 5)
        {
            circles.GetChild(1).gameObject.SetActive(true);
        }

        else if (pageCounter == 7)
        {
            //the red circle around the coin, we want it on
            circles.GetChild(6).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
        }

        //bottino e soldino
        if (pageCounter == 8)
        {
            circles.GetChild(6).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
            IntroMaster.introMaster.SetActiveUI("MoneyCounterArrow");
            IntroMaster.introMaster.SetActiveUI("MoneyCounterCircle");
        }
        else
        {
            IntroMaster.introMaster.SetInactiveUI("MoneyCounterArrow");
            IntroMaster.introMaster.SetInactiveUI("MoneyCounterCircle");
        }
        //ragnatela
        if (pageCounter == 9)
        {
            GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = webTexture;
            circles.GetChild(6).gameObject.SetActive(true);
        }


        //
        if (pageCounter > 5)
        {
            leftProbabilityText.text = "";
            rightProbabilityText.text = "";
        }


        //change money depending on the page
        if (pageCounter < 8)
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "0";
           
        }
        else
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "-3";
        }

    }

    //update the text based on the page we are in
    void UpdateMainIntroduction()
    {
        leftProbabilityText.text = "";
        rightProbabilityText.text = "";
        leftAmountTex.text = "";
        rightAmountTex.text = "";

        for (int i = 0; i < bags.Length; i++)
        {
            bags[i].gameObject.SetActive(true);
        }

        if (pageCounter == 0)
        {
            IntroMaster.introMaster.SetActiveUI("TurnCounterArrow");
            IntroMaster.introMaster.SetActiveUI("TurnCounterCircle");
        }
        else
        {
            IntroMaster.introMaster.SetInactiveUI("TurnCounterArrow");
            IntroMaster.introMaster.SetInactiveUI("TurnCounterCircle");
        }

        //3 bags
        if (pageCounter == 2)
        {
            bags[3].gameObject.SetActive(false);
            bags[5].gameObject.SetActive(false);
            bags[8].gameObject.SetActive(false);
            bags[10].gameObject.SetActive(false);

        }

        //2 bags
        if (pageCounter == 3)
        {
            bags[2].gameObject.SetActive(false);
            bags[4].gameObject.SetActive(false);
            bags[6].gameObject.SetActive(false);
            bags[7].gameObject.SetActive(false);
            bags[9].gameObject.SetActive(false);
            bags[11].gameObject.SetActive(false);

        

        }


        /**

        for (int i = 0; i < circles.childCount; i++)
        {
            if (i < 7)
            {
                if (i == pageCounter)
                {
                    circles.GetChild(i).gameObject.SetActive(true);
                }
                else
                    circles.GetChild(i).gameObject.SetActive(false);
            }
            else
                circles.GetChild(i).gameObject.SetActive(false);

        }

        for (int i = 0; i < bags.Length; i++)
        {
            bags[i].gameObject.SetActive(true);
        }

        //do this checks only if the middle bags are active
        if (bags[4].gameObject.activeSelf && bags[9].gameObject.activeSelf)
        {

            //soldino
            if (pageCounter == 7)
            {
                //the red circle around the coin, we want it on
                circles.GetChild(6).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
            }
            else
            {
                GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = sackTexture;
            }

            //bottino e soldino
            if (pageCounter == 8)
            {
                circles.GetChild(6).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
                IntroMaster.introMaster.SetActiveUI("MoneyCounterArrow");
                IntroMaster.introMaster.SetActiveUI("MoneyCounterCircle");
            }
            else
            {
                IntroMaster.introMaster.SetInactiveUI("MoneyCounterArrow");
                IntroMaster.introMaster.SetInactiveUI("MoneyCounterCircle");
            }
            //ragnatela
            if (pageCounter == 9)
            {
                GameObject.FindGameObjectWithTag("MiddleRisky").GetComponent<Image>().sprite = webTexture;
                circles.GetChild(7).gameObject.SetActive(true);
            }
            else
            {
                GameObject.FindGameObjectWithTag("MiddleRisky").GetComponent<Image>().sprite = sackTexture;
            }

            //highlight the two mnoey amount
            if (pageCounter == 11)
            {
                circles.GetChild(1).gameObject.SetActive(true);
                circles.GetChild(4).gameObject.SetActive(true);
            }

            //highlight the safe loss amount, the current money and the middle safe bag
            if (pageCounter == 12)
            {
                circles.GetChild(4).gameObject.SetActive(true);
                circles.GetChild(6).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MiddleSafe").GetComponent<Image>().sprite = coinTexture;
                IntroMaster.introMaster.SetActiveUI("MoneyCounterArrow");
                IntroMaster.introMaster.SetActiveUI("MoneyCounterCircle");
            }

            //highlight the current money and the middle risky bag
            if (pageCounter == 13)
            {
                circles.GetChild(7).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("MiddleRisky").GetComponent<Image>().sprite = webTexture;
                IntroMaster.introMaster.SetActiveUI("MoneyCounterArrow");
                IntroMaster.introMaster.SetActiveUI("MoneyCounterCircle");
            }

        }


        //change money depending on the page
        if (pageCounter < 8)
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "0";
        }
        else if (pageCounter >= 8 && pageCounter < 12)
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "+1";
        }
        else if (pageCounter >= 12)
        {
            GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>().text = "0";
        }



        //3 bags
        if (pageCounter == 15)
        {
            bags[3].gameObject.SetActive(false);
            bags[5].gameObject.SetActive(false);
            bags[8].gameObject.SetActive(false);
            bags[10].gameObject.SetActive(false);

            leftProbabilityText.text = "3 su 3";
            rightProbabilityText.text = "1 su 3";
        }

        //2 bags
        if (pageCounter == 16)
        {
            bags[2].gameObject.SetActive(false);
            bags[4].gameObject.SetActive(false);
            bags[6].gameObject.SetActive(false);
            bags[7].gameObject.SetActive(false);
            bags[9].gameObject.SetActive(false);
            bags[11].gameObject.SetActive(false);

            leftProbabilityText.text = "2 su 2";
            rightProbabilityText.text = "1 su 2";

        }

        //2 chest opened
        if (pageCounter == 17)
        {
            IntroMaster.introMaster.SetActiveUI("TurnCounterArrow");
            IntroMaster.introMaster.SetActiveUI("TurnCounterCircle");
        }
        else
        {
            IntroMaster.introMaster.SetInactiveUI("TurnCounterArrow");
            IntroMaster.introMaster.SetInactiveUI("TurnCounterCircle");
        }


        //rosso
        if (pageCounter >= 11)
        {
            leftAmountTex.color = Color.red;
            rightAmountTex.color = Color.red;
            leftAmountTex.text = "-1";
            rightAmountTex.text = "-5";
        }
        else
        {
            leftAmountTex.color = Color.green;
            rightAmountTex.color = Color.green;
            leftAmountTex.text = "+1";
            rightAmountTex.text = "+3";
        }
    **/

    }



    public void OnPointerClick(PointerEventData eventData)
    {
       
        //next button
        if (transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject == eventData.pointerEnter |
            transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").Find("NextButtonText").gameObject == eventData.pointerEnter)
        {
            UpdateNextButton(eventData);
        }

        //back button
        else if (transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject == eventData.pointerEnter |
           transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").Find("BackText").gameObject == eventData.pointerEnter)
        {
            UpdateBackButton(eventData);

        }

        //ok button
        else if (transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject == eventData.pointerEnter |
          transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").Find("OKText").gameObject == eventData.pointerEnter)
        {
            UpdateOKButton(eventData);

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }

    private void UpdateOKButton(PointerEventData eventData)
    {
        //defreeze the game
        Time.timeScale = 0;

        //Transform clone = transform;

        // Debug.Log(SceneManager.GetActiveScene().buildIndex);
        LevelManager.levelManager.LoadNextLevelWithFading(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //Destroy(clone.gameObject);
    }


    private void UpdateBackButton(PointerEventData eventData)
    {
        //update the color of the children
        transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").Find("BackText").GetComponent<Text>().color = Color.red;

        //update page number
        if (pageCounter != 0)
            pageCounter--;

        //update the the on the page
        transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text = introParts[pageCounter];

        //toggle the ok button
        if (pageCounter != introParts.Length - 1 && transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject.activeSelf)
            transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject.SetActive(false);

        //toggle the back button
        if (pageCounter == 0 && transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject.activeSelf)
            transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject.SetActive(false);
        //toggle the next button
        if (pageCounter != introParts.Length - 1 && !transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject.activeSelf)
            transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        //find the corerct children
        if (transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject == eventData.pointerEnter |
            transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").Find("NextButtonText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").Find("NextButtonText").GetComponent<Text>().color = Color.red;
        }
        else if (transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject == eventData.pointerEnter |
            transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").Find("BackText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").Find("BackText").GetComponent<Text>().color = Color.red;
        }
        else if (transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject == eventData.pointerEnter |
          transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").Find("OKText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").Find("OKText").GetComponent<Text>().color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject == eventData.pointerEnter |
           transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").Find("NextButtonText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").Find("NextButtonText").GetComponent<Text>().color = Color.white;
        }

        else if (transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject == eventData.pointerEnter |
           transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").Find("BackText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").Find("BackText").GetComponent<Text>().color = Color.white;
        }

        else if (transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject == eventData.pointerEnter |
          transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").Find("OKText").gameObject == eventData.pointerEnter)
        {
            //update the color of the children
            transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").Find("OKText").GetComponent<Text>().color = Color.white;
        }
    }

   
    private void UpdateNextButton(PointerEventData eventData)
    {

        if (pageCounter < introParts.Length - 1)
            pageCounter++;

        //update the text on the header (IntroTex)
        transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text = introParts[pageCounter];

        //toggle the back button
        if (pageCounter != 0 && !transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject.activeSelf)
            transform.Find("IntroCanvas").gameObject.transform.Find("BackButton").gameObject.SetActive(true);

        //toggle the ok button
        if (pageCounter == introParts.Length - 1 && !transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject.activeSelf)
            transform.Find("IntroCanvas").gameObject.transform.Find("OKButton").gameObject.SetActive(true);

        //toggle the next button
        if (pageCounter == introParts.Length - 1 && transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject.activeSelf)
            transform.Find("IntroCanvas").gameObject.transform.Find("NextButton").gameObject.SetActive(false);


    }

}
