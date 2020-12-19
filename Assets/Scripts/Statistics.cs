using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {

    public static Statistics stats;
    public PlayerStats playerStats = new PlayerStats();
    public CupTaskStats cupTaskStats = new CupTaskStats();

    private void Awake()
    {
        if(stats == null)
        {
            stats = GameObject.FindGameObjectWithTag("stats").GetComponent<Statistics>();
        }

        //cupTaskStats.Init();
        //playerStats.Init();

        DontDestroyOnLoad(this);

       // Debug.Log(playerStats.currentMoney);
    }

    //da vedere meglio, forse qui meglio fare per ogni giocata diciamo cosa ha scelto, l'ev di tutti e due e se ha vinto o perso
    public class PlayerStats
    {
        private int _currentMoney;
        private int _startingMoney = 0;
        private int _numberOfRiskyChoices;
        private int _numberOfSafeChoices;
        private int _numberOfGoodEVChoices;
        private int _numberOfBadEVChoices;
        private int _numberOfNeutralEVChoices;

        //TODO, ask an input at the beginning of the game
        private int _playerID;
        private int _playerGender;
        private int _playerMonthOfBirth;
        private int _playerYearOfBirth;

        //the bag selected from the player
        private int _clickedBag;

        private DateTime _startOfTrial;
        private DateTime _taskStartTime;
        private DateTime _taskEndtime;

        //0 safe 1 risky
        private int _currentChoice;

        public int clickedBag
        {
            get { return _clickedBag; }
            set { _clickedBag = value; }
        }

        public PlayerStats()
        {
            currentMoney = startingMoney;
        }

        public int playerGender
        {
            get { return _playerGender; }
            set { _playerGender = value; }
        }

        public int playerMonthOfBirth
        {
            get { return _playerMonthOfBirth; }
            set { _playerMonthOfBirth = value; }
        }

        public int playerYearOfBirth
        {
            get { return _playerYearOfBirth; }
            set { _playerYearOfBirth = value; }
        }

        public int startingMoney
        {
            get { return _startingMoney; }
            set { _startingMoney = value; }
        }

        public int numberOfBadEVChoices
        {
            get { return _numberOfBadEVChoices; }
            set { _numberOfBadEVChoices = value; }
        }

        public int numberOfNeutralEVChoices
        {
            get { return _numberOfNeutralEVChoices; }
            set { _numberOfNeutralEVChoices = value; }
        }

        public DateTime startOfTrail
        {
            get { return _startOfTrial; }
            set { _startOfTrial = value; }
        }

        public DateTime taskStartTime
        {
            get { return _taskStartTime; }
            set { _taskStartTime = value; }
        }
        public DateTime taskEndtime
        {
            get { return _taskEndtime; }
            set { _taskEndtime = value; }
        }


        public int currentMoney
        {
            get { return _currentMoney; }
            set { _currentMoney = value; }
        }

        public int playerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }

        public int currentChoice
        {
            get { return _currentChoice; }
            set { _currentChoice = value; }
        }

        public int numberOfRiskyChoices
        {
            get { return _numberOfRiskyChoices; }
            set { _numberOfRiskyChoices = value; }
        }

        public int numberOfSafeChoices
        {
            get { return _numberOfSafeChoices; }
            set { _numberOfSafeChoices = value; }
        }

        public int numberOfGoodEVChoices
        {
            get { return _numberOfGoodEVChoices; }
            set { _numberOfGoodEVChoices = value; }
        }

        public void Init()
        {
            currentMoney = startingMoney;
            numberOfRiskyChoices = 0;
            numberOfSafeChoices = 0;
            numberOfGoodEVChoices = 0;

            startOfTrail = System.DateTime.Now;
        }

    }


    public class CupTaskStats
    {

        //the EV, calculated afeterwards (in the class will be calclulated immediatly)
        //if the safeEV > riskyEV would have been better to be safe and vice-versa
        private float _safeEV;
        private float _riskyEV;

        //the total number of caps in the trial
        private int _numberOfCaps;
        //the number of winning cups in the risky condition
        private int _numberOfRiskyWinningCaps;
        //the amount to win in the safe condition
        private int _safeWinningAmount;
        //the amount to win in the risky condition
        private int _riskyWinningAmount;

        //the condition, 0 gain, 1 loss
        private int _condition;

        //is the safe choice on the left? fale = no, true = yes
        private bool _safeIsOnTheLeft;

        //the total number of trials in the task
        private int _totalNumberOfTrials;
        //the number of the trial we are at, starts from 1 as the first row of the array is empty because in the text there a re the  headers
        private int _currentTrail = 1;

        private int _currentTrailsOnAllTask = 1;

        private int _numberOfLossTrials = 0;
        private int _numberOfGainTrials = 0;


        //the start of that trial since the start of the game
        private float _startTime;
        //the end of that trial since the start of the game
        private float _endTime;
        //how long did it take to make a choice? in milliseconds
        private float _deltaTime;


        public int numberOfLossTrials
        {
            get { return _numberOfLossTrials; }
            set { _numberOfLossTrials = value; }
        }


        public int numberOfGainTrials
        {
            get { return _numberOfGainTrials; }
            set { _numberOfGainTrials = value; }
        }


        public int totalNumberOfTrials
        {
            get { return _totalNumberOfTrials; }
            set { _totalNumberOfTrials = value; }
        }

        public int currentTrail
        {
            get { return _currentTrail; }
            set { _currentTrail = value; }
        }

        public int currentTrailsOnAllTask
        {
            get { return _currentTrailsOnAllTask; }
            set { _currentTrailsOnAllTask = value; }
        }

        public bool safeIsOnTheLeft
        {
            get { return _safeIsOnTheLeft; }
            set { _safeIsOnTheLeft = value; }
        }

        public float startTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public int condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public float endtime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public float deltaTime
        {
            get { return _deltaTime; }
            set { _deltaTime = value; }
        }

        public int numberOfCaps
        {
            get { return _numberOfCaps; }
            set { _numberOfCaps = Mathf.Clamp(value, 1, 5); }
        }

        public int numberOfRiskyWinningCaps
        {
            get { return _numberOfRiskyWinningCaps; }
            set { _numberOfRiskyWinningCaps = Mathf.Clamp(value, 1, 5); }
        }

        public int safeWinningAmount
        {
            get { return _safeWinningAmount; }
            set { _safeWinningAmount = value; }
        }

        public int riskyWinningAmount
        {
            get { return _riskyWinningAmount; }
            set { _riskyWinningAmount = value; }
        }

        public void Init()
        {
            numberOfCaps = 3;
            numberOfRiskyWinningCaps = 1;
            safeWinningAmount = 10;
            riskyWinningAmount = 30;

        }

        //calculate and return the EV
        public float GetSafeEV()
        {
            _safeEV = (float)safeWinningAmount;
            return _safeEV;

        }


        //calculate and return the EV
        public float GetRiskyEV()
        {
            _riskyEV = (float)riskyWinningAmount / (float)numberOfCaps * (float)numberOfRiskyWinningCaps;
            return _riskyEV;

        }


    }

    //gamevents are used as information for the training of NN, it updates information on each frame, the output is a 
    //[playerID[ [frameXgamevents] matrix
    public class GameEvents
    {
        private int _playerID;
        private int _currentFrame;
        private int[,] _events = new int[15000,10];

        public int PlayerID
        {
            get
            {
                return _playerID;
            }

            set
            {
                _playerID = value;
            }
        }

        public int CurrentFrame
        {
            get
            {
                return _currentFrame;
            }

            set
            {
                _currentFrame = value;
            }
        }

        public void MoveLeft()
        {
            _events[_currentFrame, 0] += 1;
        }

        public void MoveRight()
        {
            _events[_currentFrame, 1] += 1;
        }
    }
}
