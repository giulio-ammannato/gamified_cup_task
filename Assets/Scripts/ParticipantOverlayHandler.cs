using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParticipantOverlayHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public static ParticipantOverlayHandler participantOverlayHandler;

    InputField IDInputField;
    InputField genderInpuField;
    InputField yearInputField;
    InputField monthInputfield;

    Text IDText;
    Text genderText;
    Text monthText;
    Text yearText;

    bool okGender = false;
    bool okYear = false;
    bool okMonth = false;
    bool okID = false;

    // Use this for initialization
    void Start ()
    {

        Debug.Log(Application.persistentDataPath);

        //the ok button is hidden at the beginning
        transform.Find("OkButton").gameObject.SetActive(false);

        if (participantOverlayHandler == null)
        {
            participantOverlayHandler = GameObject.FindGameObjectWithTag("POH").GetComponent<ParticipantOverlayHandler>();
        }

        //get the input fields
        IDInputField = transform.Find("IDInput").GetComponent<InputField>();
        genderInpuField = transform.Find("GenderInput").GetComponent<InputField>();
        yearInputField = transform.Find("YearInput").GetComponent<InputField>();
        monthInputfield = transform.Find("MonthInput").GetComponent<InputField>();

        //attach a listener to all the input fields
        genderInpuField.onEndEdit.AddListener(delegate { GenderInput(genderInpuField); });
        yearInputField.onEndEdit.AddListener(delegate { YearInput(yearInputField); });
        IDInputField.onEndEdit.AddListener(delegate { IDInput(IDInputField); });
        monthInputfield.onEndEdit.AddListener(delegate { MonthInput(monthInputfield); });


        //Debug.Log(IDText.text);

    }
	
	// Update is called once per frame
	void Update () {

        //if ALL the input fields are not empy allow to start the game
        if(okGender && okID && okMonth && okYear)
        {
            transform.Find("OkButton").gameObject.SetActive(true);
        }
        else
            transform.Find("OkButton").gameObject.SetActive(false);

    }

    //check for a valid gender ID
    private void GenderInput(InputField input)
    {
        if(input != null)
        {
            if (Convert.ToInt32(input.text) == 1 | (Convert.ToInt32(input.text) == 2))
            {
                Statistics.stats.playerStats.playerGender = Convert.ToInt32(input.text);
                okGender = true;
            }
            else
            {
                input.text = "";
               
            }
               

        }
        else
            okGender = false;

    }

    //check for a valid year
    private void YearInput(InputField input)
    {
        if (input != null)
        {
            if (Convert.ToInt32(input.text) > 1915 && (Convert.ToInt32(input.text) < 2015))
            {
                Statistics.stats.playerStats.playerYearOfBirth = Convert.ToInt32(input.text);
                okYear = true;
            }
            else
            {
                okYear = false;
                input.text = "";
            }
               
        }
        else
            okYear = false;

    }

    private void MonthInput(InputField input)
    {
        if (input != null)
        {
            if (Convert.ToInt32(input.text) > 0 && (Convert.ToInt32(input.text) <= 12))
            {
                Statistics.stats.playerStats.playerMonthOfBirth = Convert.ToInt32(input.text);
                okMonth = true;
            }
            else
            {
                input.text = "";
                okMonth = false;
            }
               

        }
        else
            okMonth = false;

    }

    private void IDInput(InputField input)
    {
        if (input != null)
        {
            if (Convert.ToInt32(input.text) > 0 | (Convert.ToInt32(input.text) < 2000))
            {
                Statistics.stats.playerStats.playerID = Convert.ToInt32(input.text);
                okID = true;
            }
            else
            {
                okID = false;
                input.text = "";
            }
                

        }
        else
            okID = false;

    }


    public void OnPointerClick(PointerEventData eventData)
    {


        if (transform.Find("OkButton").gameObject == eventData.pointerEnter |
           transform.Find("OkButton").gameObject.transform.Find("Text").gameObject == eventData.pointerEnter)
        {
            //get the start time of the task
            Statistics.stats.playerStats.taskStartTime = DateTime.Now;
            //wirte the info of the partecipant
            string info =
           //the player ID
           Statistics.stats.playerStats.playerID + ", " +
           //the player gender, 0 male, 1 female
           Statistics.stats.playerStats.playerGender + ", " +
           //the player month of birth
           Statistics.stats.playerStats.playerMonthOfBirth + ", " +
           //the player year of birth
           Statistics.stats.playerStats.playerYearOfBirth + ", " +
           //the start of the game
           Statistics.stats.playerStats.taskStartTime;

            //print the string to file
            //first in the file with a new line per task
            HandleTextFile.handleTextFile.WriteString(info);

            //than to the one all in one lione, tellign first to go to a new line
            HandleTextFile.handleTextFile.WriteEndOfLine();
            HandleTextFile.handleTextFile.WriteStringOnSameLine(info);

            //goes to the next scene (cuptask)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

}
