using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    public Transform cupTaskPrefab;
    public Transform wantToPlayPrefab;

    public UIOverlayHandler uIOverlayHandler;
    public Chest chest = new Chest();
    public Transform spawnParticlePrefab;
    public IntroHandler introPreFab = new IntroHandler();
    public IntroHandler inputInformation = new IntroHandler();

    public TextAsset capTaskTrials;

    //added in the cuptaskstats class
    //the number of the trial we are at, starts from 1 as the first row of the array is empty because in the text there a re the  headers
    //private int currentTrail = 1;
    //private int totalNumberOfTrials;


    private Transform wantToPlayclone;
    public Transform cupTaskclone;

    private float spawnDelay = 1f;
    private float respawnEffectDuration = 3f;

    //the number of element of a trial: numberOfCaps, numberOfwinningCups, riskyamount, safeAmount, condition
    private int elementsOfATrial = 5;

    private int[,] trials;
  

    public int panelYOffset = 10;

    // public Transform panelPreFab;

    private void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
       
    }


    // Use this for initialization
    void Start() {

        uIOverlayHandler = GameObject.FindGameObjectWithTag("UIOverlay").GetComponent<UIOverlayHandler>();

        //create the first array of trials, the 0 one
        createArrayOftrials();

        //disable this for skipping the start story debugging
        //Instantiate(inputInformation);
        //if you disable it enable this otherwise it won't run
        Time.timeScale = 1;

        


    }

    // Update is called once per frame
    void Update() {

    }

    //create a new istance of captask
    public void OpenCupTask(Transform _menu)
    {
        //uIOverlayHandler.CreateGameOverMenu();
        //create a captask if there isn't already one
        //Debug.Log(ca)
        if (cupTaskclone == null)
        {
            //if the trial is not finished
            if (Statistics.stats.cupTaskStats.totalNumberOfTrials > Statistics.stats.cupTaskStats.currentTrail)
            {
                updateTrialStats(Statistics.stats.cupTaskStats.currentTrail);
                Statistics.stats.playerStats.startOfTrail = DateTime.Now;

                Vector3 panelPos = new Vector3(_menu.position.x, _menu.position.y + panelYOffset, _menu.position.z);

                cupTaskclone = Instantiate(cupTaskPrefab, panelPos, _menu.rotation);
                //get the starting time when the cupTask is started, not absolute, since the start of the game, it's possible to have real world time if wanted
                Statistics.stats.cupTaskStats.startTime = Time.realtimeSinceStartup;

            }
            //the game is finished! throw the game over menu and don't do anything
            //else
                //uIOverlayHandler.ActivateGameOverMenu();
        }
    }

    //destroy an istance of the captask
    public IEnumerator DestroyCupTask(Transform prova)
    {
        if (prova != null)
        {

            //get the end time and calculate the deltatime (i.e. how long did it take to complete the task)
            Statistics.stats.cupTaskStats.endtime = Time.realtimeSinceStartup;
            Statistics.stats.cupTaskStats.deltaTime = (Statistics.stats.cupTaskStats.endtime - Statistics.stats.cupTaskStats.startTime);
            printVarToFile();

            //increase the trial counter
            Statistics.stats.cupTaskStats.currentTrail++;
            Statistics.stats.cupTaskStats.currentTrailsOnAllTask++;

            //the particles on the chest will spawn
            Transform destroyedChestParticles = Instantiate(spawnParticlePrefab,chest.transform.position,chest.transform.rotation);

            //after the spawn delay a new chest will appear
            yield return new WaitForSeconds(spawnDelay);

            //destroy the old cuptask
            Destroy(prova.gameObject);

            //"respawn" a new chest, it just move the old one but it's all good, if the game is not over
            if (Statistics.stats.cupTaskStats.totalNumberOfTrials > Statistics.stats.cupTaskStats.currentTrail)
                chest.MoveChest();
            //if the game is over open the end menu
            else
            {
                Transform clone = chest.transform;
                Destroy(clone.gameObject);
                yield return new WaitForSeconds(1f);
                uIOverlayHandler.ActivateGameOverMenu();
            }

            //will respawn with some particles

            if(chest != null)
            {
                Transform newChestParticles = Instantiate(spawnParticlePrefab, chest.transform.position, chest.transform.rotation);

                //how long before the first particle disappear
                yield return new WaitForSeconds(2f);

                if (destroyedChestParticles != null)
                    Destroy(destroyedChestParticles.gameObject);

                yield return new WaitForSeconds(respawnEffectDuration);
                if (newChestParticles != null)
                    Destroy(newChestParticles.gameObject);
            }
        }
    }

    //create the array containing each trail information (money to win in risk/loss, number of caps etc) from the text file trials (in input)
    public void createArrayOftrials()
    {
        string[] dataLines = capTaskTrials.text.Split('\n');

        string[][] dataPairs = new string[dataLines.Length][];
        int lineNum = 0;
        foreach (string line in dataLines)
        {
            dataPairs[lineNum++] = line.Split(',');
        }

        //create the array with the stored value of the trail, taken from the trial.txt file
        trials = new int[dataPairs.Length, elementsOfATrial];

        //inizialize the total number of trial base on the one in the text
        Statistics.stats.cupTaskStats.totalNumberOfTrials = dataPairs.Length;

           

        for (int x =1; x < dataPairs.Length; x++)
        {
            for (int y = 0; y < elementsOfATrial; y++)
            {
                trials[x,y] = int.Parse(dataPairs[x][y]);
            }
        }

        //randomize the trials array, and it works! may be better to disable it while debugging
        ShuffleArray(trials, Statistics.stats.cupTaskStats.totalNumberOfTrials, elementsOfATrial);

        //if we are not in the gain condition increase the lenght of the array so it will be possibile to keep going
        /**
        Debug.Log(trials[1, 0] + " " + trials[1, 1] + " " + trials[1, 2] + " " + trials[1, 3] + " " + trials[1, 4]);
        Debug.Log(trials[2, 0] + " " + trials[2, 1] + " " + trials[2, 2] + " " + trials[2, 3] + " " + trials[2, 4]);
        Debug.Log(trials[3, 0] + " " + trials[3, 1] + " " + trials[3, 2] + " " + trials[3, 3] + " " + trials[3, 4]);
        Debug.Log(trials[4, 0] + " " + trials[4, 1] + " " + trials[4, 2] + " " + trials[4, 3] + " " + trials[4, 4]);
        Debug.Log(trials[5, 0] + " " + trials[5, 1] + " " + trials[5, 2] + " " + trials[5, 3] + " " + trials[5, 4]);
        Debug.Log(trials[6, 0] + " " + trials[6, 1] + " " + trials[6, 2] + " " + trials[6, 3] + " " + trials[6, 4]);
        **/

    }


    //shuffle the array to randomize the trials, now all the trails are randomized but we can also select some trial "trains" to randomize
    public static void ShuffleArray<T>(T[,] arr, int arrayLength, int arrayWidth)
    {
        for (int i = arrayLength - 1; i > 0; i--)
        {
            //the 0 contains the labels, we don't want to shuffle them!
            int r = UnityEngine.Random.Range(1, i);

            for (int y = 0; y < arrayWidth; y++)
            {
                T tmp = arr[i, y];
                arr[i, y] = arr[r, y];
                arr[r, y] = tmp;
            }
        }
    }

    


    public void updateTrialStats(int trialNumber)
    {
        //update the data for the next trail, the data are tasken from the trials text
        //the number of caps of this trial
        Statistics.stats.cupTaskStats.numberOfCaps = trials[trialNumber, 0];
        //the number of winning risky caps of this trial
        Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps = trials[trialNumber, 1];
        //the amount safe winning amount in this trial
        Statistics.stats.cupTaskStats.safeWinningAmount = trials[trialNumber, 2];
        //the risky amount in this trial
        Statistics.stats.cupTaskStats.riskyWinningAmount = trials[trialNumber, 3];
        //the trial type. 0 = gain, 1 =loss
        Statistics.stats.cupTaskStats.condition = trials[trialNumber, 4];

    }



    public void printVarToFile()
    {
        //print to file all the relevant informations (i.e. choice, win/lose etc)
        string info =
           //the player ID
           //Statistics.stats.playerStats.playerID + ", " +
            //the day of the trial
            Statistics.stats.playerStats.startOfTrail + ", " +
            //the number of the trial
            Statistics.stats.cupTaskStats.currentTrailsOnAllTask + ", " +
            //is the safe choice on the left? 0 = no, 1 = yes
            Convert.ToInt32(Statistics.stats.cupTaskStats.safeIsOnTheLeft) + ", " +
            //the condition of the trial, 0 gain, 1 loss
            Statistics.stats.cupTaskStats.condition + ", " +
            //the selected bag, from 0 to 9
            Statistics.stats.playerStats.clickedBag + ", " +
            //the risky Espectect Value
            Statistics.stats.cupTaskStats.GetRiskyEV() + ", " +
            //the safe EV
            Statistics.stats.cupTaskStats.GetSafeEV() + ", " +
            //the number of caps AND winning caps, is the same :)
            Statistics.stats.cupTaskStats.numberOfCaps + ", " +
            //the number of isky winning caps
            Statistics.stats.cupTaskStats.numberOfRiskyWinningCaps + ", " +
            //the risky winning amount
            Statistics.stats.cupTaskStats.riskyWinningAmount + ", " +
            //the safe winning amount
            Statistics.stats.cupTaskStats.safeWinningAmount + ", " +
            //the current player money
            Statistics.stats.playerStats.currentMoney + ", " +
            //the current choice (i.e. risky = 1 or safe = 0)
            Statistics.stats.playerStats.currentChoice + ", " +
            //the start of this task
            Statistics.stats.cupTaskStats.startTime + ", " +
            //the end of this task
            Statistics.stats.cupTaskStats.endtime + ", " +
            //the time used for the choice
            Statistics.stats.cupTaskStats.deltaTime;

        HandleTextFile.handleTextFile.WriteString(info);
        HandleTextFile.handleTextFile.WriteStringOnSameLine(info);
    }

    //deprecated methods

    /**
     * 
    public void StartStory()
    {
        Instantiate(introPreFab);
    }


    //create a new istance of the want to play menu
    public void CreateWantToPlayMenu(Transform _transform)
    {
        //create the want to play menu if is not alreasdy there AND the cuptask is not started yet
        if (wantToPlayclone == null && cupTaskclone == null)
        {
            Vector3 panelPos = new Vector3(_transform.position.x, _transform.position.y + panelYOffset, _transform.position.z);
            wantToPlayclone = Instantiate(wantToPlayPrefab, panelPos, _transform.rotation);
        }

    }

    //destroy an istance of the want to play menu
    public void DestroyWantToPlayMenu(bool openCup)
    {
        //if the button pressed is yes openCup will be true and it will open the cupTaskScreen
        if (openCup)
            OpenCupTask(wantToPlayclone);
        if (wantToPlayclone != null)
            Destroy(wantToPlayclone.gameObject);
    }
     * */


}
