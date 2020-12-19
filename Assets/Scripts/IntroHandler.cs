using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //the text file containing the intro
    public TextAsset introStory;

    //the inputfield object attached to this script
    InputField inputField;
    Text placeHolderText;
    //the text cointained in the input box
    Text inputText;

    bool haveInputfield;
    //the story divided in string for each new line
    string[] introParts;

    int pageCounter;

    int pageToActivate;
    bool hasPagetoActivate;

    private void Awake()
    {
        haveInputfield = false;
        if (transform == null)
            Instantiate(transform);

        hasPagetoActivate = true;
        pageToActivate = 2;
        pageCounter = 0;
    }

    // Use this for initialization
    void Start () {

        //if there is an input field initialize the objects
        if(transform.Find("IntroCanvas").gameObject.transform.Find("InputField") != null)
        {
            InitInput();
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
    public void SetPageToActivate(int page)
    {
        pageToActivate = page;
    }
    public void HasActivePages(bool condition)
    {
        hasPagetoActivate = condition;
    }

    private void InitInput()
    {
        //if it does have an input field, set it to true
        haveInputfield = true;

        //the inputfield object attached to this script
        inputField = transform.Find("IntroCanvas").gameObject.transform.Find("InputField").GetComponent<InputField>();

        //we just want numbersss
        inputField.contentType = InputField.ContentType.IntegerNumber;

        //the text showed before typing
        placeHolderText = transform.Find("IntroCanvas").gameObject.transform.Find("InputField").gameObject.transform.Find("Placeholder").GetComponent<Text>();

        //the text cointained in the input box
        inputText = transform.Find("IntroCanvas").gameObject.transform.Find("InputField").gameObject.transform.Find("InputText").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update () {

        Canvas.ForceUpdateCanvases();

        if(hasPagetoActivate)
        {
            if (pageCounter == pageToActivate)
            {
                IntroMaster.introMaster.SetActiveUI("RedArrowChest");
                IntroMaster.introMaster.SetActiveUI("RedCirleChest");
            }
            else
            {
                IntroMaster.introMaster.SetInactiveUI("RedArrowChest");
                IntroMaster.introMaster.SetInactiveUI("RedCirleChest");
            }
        }
       



        //if we have an input field
        if (haveInputfield)
        {
            //update the placeholder and/or the inputfield text according to the page we are in
            UpdaterText();
        }
        

    }

    //update the text based on the page we are in
    private void UpdaterText()
    {
        //INTRO
        if (introParts[0] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            placeHolderText.text = "";
            inputField.text = "";
        }

        //ID
        else if (introParts[1] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            placeHolderText.text = "Inserire ID partecipante";

            //if there is already something written (i.e. the researcher went back to modify something) it takes that
            if (Statistics.stats.playerStats.playerID != 0)
                inputField.text = "" + Statistics.stats.playerStats.playerID;
        }
        //GENDER
        else if (introParts[2] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            placeHolderText.text = "1 maschio, 2 femmina";

            //if there is already something written (i.e. the researcher went back to modify something) it takes that
            //the starting value is 0 because the coding is 1 and 2
            if (Statistics.stats.playerStats.playerGender != 0)
                inputField.text = "" + Statistics.stats.playerStats.playerGender;
        }
        //MONTH
        else if (introParts[3] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            placeHolderText.text = "Numero mese nascita";

            //if there is already something written (i.e. the researcher went back to modify something) it takes that
            if (Statistics.stats.playerStats.playerMonthOfBirth != 0)
                inputField.text = "" + Statistics.stats.playerStats.playerMonthOfBirth;
        }
        //YEAR
        else if (introParts[4] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            placeHolderText.text = "Anno di nascita";

            //if there is already something written (i.e. the researcher went back to modify something) it takes that
            if (Statistics.stats.playerStats.playerYearOfBirth != 0)
                inputField.text = "" + Statistics.stats.playerStats.playerYearOfBirth;
        }
        //CONFIRM SCREEN
        else if (introParts[5] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            placeHolderText.text = "";


        }
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
        Time.timeScale = 1;

        //if it does have an input field
        if (haveInputfield)
        {
            //wirte the info of the partecipant
            string info =
           //the player ID
           Statistics.stats.playerStats.playerID + ", " +
           //the player gender, 0 male, 1 female
           Statistics.stats.playerStats.playerGender + ", " +
           //the player month of birth
           Statistics.stats.playerStats.playerMonthOfBirth + ", " +
           //the player year of birth
           Statistics.stats.playerStats.playerYearOfBirth;

            //print the string to file
           HandleTextFile.handleTextFile.WriteString(info);

            HandleTextFile.handleTextFile.WriteStringOnSameLine(info);

            //GameMaster.gm.StartStory();
        }

        Transform clone = transform;
        Destroy(clone.gameObject);
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

    private void UpdatePlayerInfo()
    {

        //if there is something written in the input box
        if (inputText.text != "")
        {
            //if we are in the ID part (introparts [1] = ID
            if (introParts[1] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
            {
                //write the ID
                Statistics.stats.playerStats.playerID = Convert.ToInt32(inputText.text);

                //update page number if is not at the last page
                if (pageCounter < introParts.Length - 1)
                    pageCounter++;
            }
            //if we are in the gender part (introparts [2] = gender
            else if (introParts[2] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
            {
                //the value must be 1 or 2, otherwise it won't go to the next page
                if (Convert.ToInt32(inputText.text) == 1 | (Convert.ToInt32(inputText.text) == 2))
                {
                    Statistics.stats.playerStats.playerGender = Convert.ToInt32(inputText.text);

                    //update page number if is not at the last page
                    if (pageCounter < introParts.Length - 1)
                        pageCounter++;
                }
                     
            }
            //month in number
            //if we are in the month part (introparts [3] = month
            else if (introParts[3] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
            {
                //the value must from 1 to 12 otherwise it won't go to the next page
                if (Convert.ToInt32(inputText.text) > 0 && (Convert.ToInt32(inputText.text) < 13))
                {
                    Statistics.stats.playerStats.playerMonthOfBirth = Convert.ToInt32(inputText.text);

                    //update page number if is not at the last page
                    if (pageCounter < introParts.Length - 1)
                        pageCounter++;
                }
                    
            }
            //if we are in the year part (introparts [5] = year
            else if (introParts[4] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
            {
                //the value must from 1900 to 2017 otherwise it won't go to the next page
                if (Convert.ToInt32(inputText.text) >= 1900 && (Convert.ToInt32(inputText.text) < 2017))
                {
                    Statistics.stats.playerStats.playerYearOfBirth = Convert.ToInt32(inputText.text);
                    //update page number if is not at the last page
                    if (pageCounter < introParts.Length - 1)
                        pageCounter++;
                }
                   
            }

            //reset the input field to empty
            inputField.text = "";


        }
        //if we are in the first page is ok for no text 
        else if(introParts[0] == transform.Find("IntroCanvas").gameObject.transform.Find("IntroText").GetComponent<Text>().text)
        {
            if (pageCounter < introParts.Length - 1)
                pageCounter++;
        }
        
    }

    private void UpdateNextButton(PointerEventData eventData)
    {
        //if there is an input field
        if (haveInputfield)
        {
            //Debug.Log("c'e' ap[piccicato un inputfield");
            //Debug.Log(transform.Find("IntroCanvas").Find("InputField").Find("InputText").GetComponent<Text>().text);

            UpdatePlayerInfo();
        }
        //if there is no input just goes to the next page
        else
        {
            //update page number if is not at the last page
            if (pageCounter < introParts.Length - 1)
                pageCounter++;
        }
        

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
