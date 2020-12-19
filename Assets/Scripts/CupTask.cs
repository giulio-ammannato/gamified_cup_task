using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CupTask : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    //the text of the button in the menu
    public Text leftAmountText;
    public Text rigthAmountText;

    public Text leftProbability;
    public Text rigthProbability;

    public Transform coin;
    public Transform web;
   // public Transform transparent;

    private Sprite coinSprite;
    private Sprite webSprite;
    private Sprite sackSprite;
    //TODO cambiare a seconda di lingua scelta
    private string suOrOn = " on ";

    //ui elements
    //the two text on the right and left of the screen
    private Text currentMoneyText;
    private Text trialCounterText;
  //  private Text LosstrialCounterText;
   // private Text GaintrialCounterText;

    //IMPORTANT! the first 5 elements of the array have to be the safe bags
    public Image[] sacks;

    private bool haveToDie;
    private bool dead;

    private void Start()
    {
        //moved to UIoveralyHandler
        /**
        //get the current money, first look for the text element 
        currentMoneyText = GameObject.FindGameObjectWithTag("MoneyCounterText").GetComponent<Text>();
        // and then set the text to the current money
        currentMoneyText.text = "" +Statistics.stats.playerStats.currentMoney;

        //get the current trial, first look for the text element 
        trialCounterText = GameObject.FindGameObjectWithTag("TrialCounterText").GetComponent<Text>();
        // the trial we are at, it starts at 1 that's why we decrease by 1
        trialCounterText.text = "" + (Statistics.stats.cupTaskStats.currentTrailsOnAllTask) + " di " + ((Statistics.stats.cupTaskStats.totalNumberOfTrials-1)*2);
        **/

        //if it is a gain
        if (Statistics.stats.cupTaskStats.condition == 0)
           Statistics.stats.cupTaskStats.numberOfGainTrials++;
        else
           Statistics.stats.cupTaskStats.numberOfLossTrials++;

        //deprecated for the moment
        /**
        //get the number of loss trial, first look for the text element 
        LosstrialCounterText = GameObject.FindGameObjectWithTag("LossCounter").GetComponent<Text>();
        LosstrialCounterText.color = Color.red;
        LosstrialCounterText.text =Statistics.stats.cupTaskStats.numberOfLossTrials + " di " + ((Statistics.stats.cupTaskStats.totalNumberOfTrials - 1) / 2);

        //get the number of gain trial, first look for the text element 
        GaintrialCounterText = GameObject.FindGameObjectWithTag("GainCounter").GetComponent<Text>();
        GaintrialCounterText.color = Color.green;
        GaintrialCounterText.text = Statistics.stats.cupTaskStats.numberOfGainTrials + " di " + ((Statistics.stats.cupTaskStats.totalNumberOfTrials - 1) / 2);
        **/

        //randomize the place of the safe/risky (i.e. sometimes the safe will be on the right and sometime on the left)
        //TODO da input, ma non so cosa preferiscano..
        int r = Random.Range(0, 2);
        if (r == 0)
           Statistics.stats.cupTaskStats.safeIsOnTheLeft = false;
        else
           Statistics.stats.cupTaskStats.safeIsOnTheLeft = true;

        //check if the safe condition is on the left
        if (Statistics.stats.cupTaskStats.safeIsOnTheLeft)
        {
            //and get the current chance to win and amount to win for the two condition, if condition 0 a + is showed otherwise a -
            //gain condition
            if (Statistics.stats.cupTaskStats.condition == 0)
            {
                //update the info in the text files
                //the safe probability
                leftProbability.text = "" +Statistics.stats.cupTaskStats.numberOfCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                //the safe amount to win
                leftAmountText.text = "+" +Statistics.stats.cupTaskStats.safeWinningAmount;
                //change the color to green
                leftAmountText.color = Color.green;

                //the risky probability
                rigthProbability.text = "" +Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                //the risky amount
                rigthAmountText.text = "+" + Statistics.stats.cupTaskStats.riskyWinningAmount;
                //change the color to green
                rigthAmountText.color = Color.green;
            }
            //can be 0 or 1, so if is not 0 is 1 > loss
            else
            {
                //update the info in the text files
                //the safe probability
                leftProbability.text = "" +Statistics.stats.cupTaskStats.numberOfCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                leftAmountText.text =  "-" +Statistics.stats.cupTaskStats.safeWinningAmount;
                leftAmountText.color = Color.red;

                rigthProbability.text = "" +Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                rigthAmountText.text = "-" +Statistics.stats.cupTaskStats.riskyWinningAmount;
                rigthAmountText.color = Color.red;
            }
        }
        //the safe condition is on the right
        else
        {
            //and get the current chance to win and amount to win for the two condition, if condition 0 a + is showed otherwise a -
            //gain condition
            if (Statistics.stats.cupTaskStats.condition == 0)
            {
                rigthProbability.text = "" +Statistics.stats.cupTaskStats.numberOfCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                rigthAmountText.text =  "+" +Statistics.stats.cupTaskStats.safeWinningAmount;
                rigthAmountText.color = Color.green;

                leftProbability.text = "" +Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                leftAmountText.text =  "+" +Statistics.stats.cupTaskStats.riskyWinningAmount;
                leftAmountText.color = Color.green;
            }
            //can be 0 or 1, so if is not 0 is 1 > loss
            else
            {
                rigthProbability.text = "" +Statistics.stats.cupTaskStats.numberOfCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                rigthAmountText.text = "-" +Statistics.stats.cupTaskStats.safeWinningAmount;
                rigthAmountText.color = Color.red;

                leftProbability.text = "" +Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps + suOrOn + Statistics.stats.cupTaskStats.numberOfCaps;
                leftAmountText.text = "-" +Statistics.stats.cupTaskStats.riskyWinningAmount;
                leftAmountText.color = Color.red;
            }
        }
        

        //the menu was just made and is waiting for the player input
        haveToDie = false;
        dead = false;

        //get the sprites used subsequentially 
        coinSprite = coin.GetComponent<SpriteRenderer>().sprite;
        webSprite = web.GetComponent<SpriteRenderer>().sprite;
        sackSprite = sacks[0].sprite;
        //transparentSprite = transparent.GetComponent<SpriteRenderer>().sprite;

        /**
        //set all the sacks back with the sack sprite
        for (int x = 0; x < sacks.Length; x++)
        {
            sacks[x].sprite = sackSprite;
        }
    **/

        //then change the unused sakcs, for the moment with the web, we can easily change it, so if they are 5 they will render all of them
        if (Statistics.stats.cupTaskStats.numberOfCaps != 5)
            UpdateVisibleSacksOnNumberOfCaps();

        //freeze the rest of the game
        Time.timeScale = 0;
    }

    private void Update()
    {
        Canvas.ForceUpdateCanvases();
    }

    //handle the click on the captask, what choice is made etc
    public void OnPointerClick(PointerEventData eventData)
    {
        //se non si sta chiudendo il menu' controlla se e quale borsa e' stata cliccata
        if (!haveToDie)
            GetClickedBag(eventData);


        //kill this menu if it have to die, and is not already diing (it takes one sec), also the gameflow will start again (i.e. we can move)
        if (haveToDie & !dead)
        {
            //reset the gameflow (i.e. the character can move)
            Time.timeScale = 1;

            //kill the menu
           Statistics.stats.StartCoroutine(GameMaster.gm.DestroyCupTask(transform));
            dead = true;
        }
        

    }

    //update all the stats based on the safe choice
    private void SafeChoice(int x)
    {
        //the curernt choice is safe! = 0
       Statistics.stats.playerStats.currentChoice = 0;

        //increase the number of safe choice
       Statistics.stats.playerStats.numberOfSafeChoices += 1;

        //was it a good or bad choice based on EV? let's find out!
        UpdateChoice();
       
        //update the current coin with the amount won OR LOST
        //if we are in the win condition icnrease the money
        if(Statistics.stats.cupTaskStats.condition == 0)
           Statistics.stats.playerStats.currentMoney +=Statistics.stats.cupTaskStats.safeWinningAmount;
        //else you lose it, all, myahahahahah, bad choice friend..
        else
           Statistics.stats.playerStats.currentMoney -=Statistics.stats.cupTaskStats.safeWinningAmount;

        //moved to uioverlay handler
        //update the coin counter on the right with the amount won/lost
        //currentMoneyText.text = "" +Statistics.stats.playerStats.currentMoney;

        //TODO, quando si perde potrebbe esserci qualcos'altro
        //change the sack with the coin you have found!
        sacks[x].sprite = coinSprite;

        //if one choice is made the menu have to die!!
        haveToDie = true;   
    }

    //update all the stats based on the risky choice
    private void RiskyChoice(int x)
    {
        //the curernt choice is risky! = 1
       Statistics.stats.playerStats.currentChoice = 1;

        //increase the number of risky choice
       Statistics.stats.playerStats.numberOfRiskyChoices += 1;

        //was it a good or bad choice based on EV? let's find out!
        UpdateChoice();

        //check if you find the coin (win or loose, depending on the condition)
        if (Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps / Random.Range(Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps,Statistics.stats.cupTaskStats.numberOfCaps+1) >= 1)
        {
            //update the current coin with the amount won/lost
            //gain condition
            if (Statistics.stats.cupTaskStats.condition == 0)
               Statistics.stats.playerStats.currentMoney +=Statistics.stats.cupTaskStats.riskyWinningAmount;
            //loss condition
            else
               Statistics.stats.playerStats.currentMoney -=Statistics.stats.cupTaskStats.riskyWinningAmount;

            //moved to UI overlay handler
            //update the coin counter on the right with the amount won
            //currentMoneyText.text = "" +Statistics.stats.playerStats.currentMoney;

            //change the sack with the coin tyou have found!
            sacks[x].sprite = coinSprite;

            //if one choice is made the menu have to die!!
            haveToDie = true;
        }
        //seeno' tu perdi e te lo meriti sai.. Maa potrebeb anche andarti bene, se siamo in loss condition..
        else
        {
            //change the sack with the coin tyou have found!
            sacks[x].sprite = webSprite;

            //if one choice is made the menu have to die!!
            haveToDie = true;
        }
    }

    private void UpdateChoice()
    {
        //are we in a winning or loosing condition?
        //if we are in a win condition (0) and we picked a safe option (0)
        if (Statistics.stats.cupTaskStats.condition == 0 &&Statistics.stats.playerStats.currentChoice == 0)
        {
            
            //was it a good choice?
            if (Statistics.stats.cupTaskStats.GetSafeEV() >Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfGoodEVChoices += 1;

            }
            //or was is a bad choice?
            else if (Statistics.stats.cupTaskStats.GetSafeEV() <Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfBadEVChoices += 1;
            }
            //maybe it was the same EV?
            else if ((Statistics.stats.cupTaskStats.GetSafeEV() ==Statistics.stats.cupTaskStats.GetRiskyEV()))
            {
               Statistics.stats.playerStats.numberOfNeutralEVChoices += 1;
            }
        }
        //if we are in a loss condition is exactly the opposite! incredible eh..? :P
        else if(Statistics.stats.cupTaskStats.condition == 1 &&Statistics.stats.playerStats.currentChoice == 0)
        {
            //was it a bad choice?
            if (Statistics.stats.cupTaskStats.GetSafeEV() >Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfBadEVChoices += 1;

            }
            //or was is a good choice?
            else if (Statistics.stats.cupTaskStats.GetSafeEV() <Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfGoodEVChoices += 1;

            }
            //maybe it was the same EV?
            else if ((Statistics.stats.cupTaskStats.GetSafeEV() ==Statistics.stats.cupTaskStats.GetRiskyEV()))
            {
               Statistics.stats.playerStats.numberOfNeutralEVChoices += 1;
            }
        }

        //if we are in a win condition (0) and we picked a risky option (1)
        else if (Statistics.stats.cupTaskStats.condition == 0 &&Statistics.stats.playerStats.currentChoice == 1)
        {

            //was it a good choice?
            if (Statistics.stats.cupTaskStats.GetSafeEV() <Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfGoodEVChoices += 1;

            }
            //or was is a bad choice?
            else if (Statistics.stats.cupTaskStats.GetSafeEV() >Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfBadEVChoices += 1;
            }
            //maybe it was the same EV?
            else if ((Statistics.stats.cupTaskStats.GetSafeEV() ==Statistics.stats.cupTaskStats.GetRiskyEV()))
            {
               Statistics.stats.playerStats.numberOfNeutralEVChoices += 1;
            }
        }
        //if we are in a loss condition is exactly the opposite! incredible eh..? :P
        else if (Statistics.stats.cupTaskStats.condition == 1 &&Statistics.stats.playerStats.currentChoice == 1)
        {
            //was it a bad choice?
            if (Statistics.stats.cupTaskStats.GetSafeEV() <Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfBadEVChoices += 1;

            }
            //or was is a good choice?
            else if (Statistics.stats.cupTaskStats.GetSafeEV() >Statistics.stats.cupTaskStats.GetRiskyEV())
            {
               Statistics.stats.playerStats.numberOfGoodEVChoices += 1;

            }
            //maybe it was the same EV?
            else if ((Statistics.stats.cupTaskStats.GetSafeEV() ==Statistics.stats.cupTaskStats.GetRiskyEV()))
            {
               Statistics.stats.playerStats.numberOfNeutralEVChoices += 1;
            }
        }

    }

    //manage the click action
    private void GetClickedBag(PointerEventData eventData)
    {
        //Debug.Log(Statistics.stats.cupTaskStats.numberOfCaps);
        //cycle trought all the sacks
        for (int x = 0; x < sacks.Length; x++)
        {
            if (sacks[x].gameObject == eventData.pointerEnter)
            {
                //the number of the sacked that has been clicked
                Statistics.stats.playerStats.clickedBag = x;

                if (Statistics.stats.cupTaskStats.numberOfCaps == 5)
                {
                    //if the sae choice is on the left
                    if (Statistics.stats.cupTaskStats.safeIsOnTheLeft)
                    {
                        //safe
                        if (x < 5)
                        {
                            SafeChoice(x);
                            break;

                        }
                        //risky
                        else
                        {
                            RiskyChoice(x);
                            break;
                        }
                    }
                    //if the safe choice is on the right is the opposite
                    else
                    {
                        //safe
                        if (x >= 5)
                        {
                            SafeChoice(x);
                            break;

                        }
                        //risky
                        else
                        {
                            RiskyChoice(x);
                            break;
                        }
                    }
                }

                else if (Statistics.stats.cupTaskStats.numberOfCaps == 3)
                {
                    //if the sae choice is on the left
                    if (Statistics.stats.cupTaskStats.safeIsOnTheLeft)
                    {
                        //safe, qui pero' solo cap 0 2 4, quindi se e' pari
                        if (x < 5 && x % 2 == 0)
                        {
                            SafeChoice(x);
                            break;
                        }
                        //risky, anche qui pero' vogliamo solo le dispari, 5 7 9
                        else if (x >= 5 && x % 2 != 0)
                        {
                            RiskyChoice(x);
                            break;
                        }

                    }
                    //if the safe choice is on the right is the opposite
                    else
                    {
                        
                        if (x >= 5 && x % 2 != 0)
                        {
                            SafeChoice(x);
                            break;
                        }
                      
                        else if (x < 5 && x % 2 == 0)
                        {
                            RiskyChoice(x);
                            break;
                        }

                    }

                }

                else if (Statistics.stats.cupTaskStats.numberOfCaps == 2)
                {
                    //safe choice on the left
                    if (Statistics.stats.cupTaskStats.safeIsOnTheLeft)
                    {
                        //safe, qui pero' solo cap 1 e 3, quindi solo se e' dispari
                        if (x < 5 && x % 2 != 0)
                        {
                            SafeChoice(x);
                            break;
                        }
                        //risky, anche qui pero' vogliamo solo le pari, 6 e 8
                        else if (x >= 5 && x % 2 == 0)
                        {
                            RiskyChoice(x);
                            break;
                        }
                    }
                    //if the safe choice is on the right is the opposite
                    else
                    {
                        
                        if (x >= 5 && x % 2 == 0)
                        {
                            SafeChoice(x);
                            break;
                        }
                        else if (x < 5 && x % 2 != 0)
                        {
                            RiskyChoice(x);
                            break;
                        }
                    }
                }
            }
        }

    }

    private void UpdateVisibleSacksOnNumberOfCaps()
    {
        
        

        if (Statistics.stats.cupTaskStats.numberOfCaps == 3)
        {
            for (int x = 0; x < sacks.Length; x++)
            {
                //vanno renderati solo i sacchetti 0 2 4, quindi i dispari vanno disabilitati
                if (x < 5 && x % 2 != 0)
                {
                    //Debug.Log(x + "da 3");
                    sacks[x].gameObject.SetActive(false);
                    //sacks[x].sprite = webSprite;
                }
                //qui invece vanno disabilitati i pari perche' vogliamo 5 7 8 
                else if (x >= 5 && x % 2 == 0)
                {
                    //Debug.Log(x + "da 3");
                    sacks[x].gameObject.SetActive(false);
                    //sacks[x].sprite = webSprite;
                }
            }
        } 
    

        if (Statistics.stats.cupTaskStats.numberOfCaps == 2)
        {
            for (int x = 0; x < sacks.Length; x++)
            {
                //qui vogliamo solo 1 e 3, quindi i pari non li vogliamo renderare
                if (x < 5 && x % 2 == 0)
                {
                    //Debug.Log(x + "primi");
                    //sacks[x].sprite = webSprite;
                    sacks[x].gameObject.SetActive(false);
                }
                //qui invece vogliamo i pari 6 e 8 ma non i dispari
                else if (x >= 5 && x % 2 != 0)
                {
                    //Debug.Log(x + "secondi");
                    //sacks[x].sprite = webSprite;
                    sacks[x].gameObject.SetActive(false);
                }
            }
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerEnter);
        //Debug.Log(transform.Find("Safe_1"));
        //Debug.Log(eventData.pointerEnter);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerEnter);
        
    }

}
