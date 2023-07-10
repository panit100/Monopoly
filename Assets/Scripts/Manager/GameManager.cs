using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GAMESTAGE
{
    // CREATEBOARD,
    WAITINGFORSTART,
    START,
    ROLLING,
    MOVING,
    CALCULATEHITPOINT,
    BUYING,
    END,
}

public enum TEAM
{
    NONE,
    RED,
    GREEN,
    BLUE,
    YELLOW,
}

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GAMESTAGE currentGameStage;
    public List<PlayerController> playerInGame = new List<PlayerController>();
    public List<PlayerUI> playerUis = new List<PlayerUI>();

    public TEAM playerTurn;

    [Header("Dice")]
    [SerializeField] Dice dice;

    [Header("Grid")]
    [SerializeField] List<Coner> coners = new List<Coner>();
    [SerializeField] List<Edge> edges = new List<Edge>();

    [Header("Buy and Skip Button")]
    [SerializeField] Button buyButton;
    [SerializeField] Button skipButton;

    [Header("Start Button and HitPoint Input")]
    [SerializeField] Button StartGameButton;
    [SerializeField] TMP_InputField InputHitPoint; 

    [Header("Restart Button")]
    [SerializeField] TMP_Text WinText;
    [SerializeField] Button RestartButton;

    [Header("Action Text")]
    [SerializeField] TMP_Text playerTurnText;

    [Header("Player Prefab")]
    [SerializeField] PlayerController redPlayerPrefab;
    [SerializeField] PlayerController greenPlayerPrefab;
    [SerializeField] PlayerController bluePlayerPrefab;
    [SerializeField] PlayerController yellowPlayerPrefab;

    PlayerController currentPlayer;
    int moveCount = 0;

    void Awake()
    {
        inst = this;        
    }

    void Start() 
    {
        AddListenerButton();

        OnChangeGameStage(GAMESTAGE.WAITINGFORSTART);
    }

    void AddListenerButton()
    {
        buyButton.onClick.AddListener(BuyEdge);
        skipButton.onClick.AddListener(SkipTurn);
        StartGameButton.onClick.AddListener(OnStartGame);
        RestartButton.onClick.AddListener(RestartGame);
    }

    void RemoveListenerButton()
    {
        buyButton.onClick.RemoveAllListeners();
        skipButton.onClick.RemoveAllListeners();
        StartGameButton.onClick.RemoveAllListeners();
        RestartButton.onClick.RemoveAllListeners();
    }

    public void OnChangeGameStage(GAMESTAGE stage)
    {
        currentGameStage = stage;

        switch(currentGameStage)
        {
            // case GAMESTAGE.CREATEBOARD:
            //     break;
            case GAMESTAGE.WAITINGFORSTART:
                dice.gameObject.SetActive(false);
                buyButton.gameObject.SetActive(false);
                skipButton.gameObject.SetActive(false);
                WinText.gameObject.SetActive(false);
                RestartButton.gameObject.SetActive(false);
                playerTurnText.gameObject.SetActive(false);

                foreach(var n in playerUis)
                {
                    n.gameObject.SetActive(true);
                    n.EnablePlayButton(true);
                    n.EnableFirstButton(false);
                }
                break;
            case GAMESTAGE.START:
                foreach(var n in playerUis)
                {
                    n.EnableFirstButton(false);
                    n.EnableAIButton(false);

                    if(playerInGame.Find(m => m.Team == n.uiTeam) == null)
                        n.gameObject.SetActive(false);
                }

                if(currentPlayer == null)
                    SelectFirstPlayer(playerInGame[0].Team);

                StartGameButton.gameObject.SetActive(false);
                InputHitPoint.gameObject.SetActive(false);

                UpdateTurnText();
                
                OnChangeGameStage(GAMESTAGE.ROLLING);
                break;
            case GAMESTAGE.ROLLING:
                dice.gameObject.SetActive(true);
                
                if(!currentPlayer.AI)
                    dice.EnableButton(true);
                else
                {
                    dice.EnableButton(false);
                    dice.OnToss();
                }

                break;
            case GAMESTAGE.MOVING:
                Grid tempGrid = currentPlayer.GetGrid();

                for(int i = 0; i < moveCount; i++)
                {
                    tempGrid = tempGrid.GetNextCell();
                }               

                currentPlayer.SetDistinationGrid(tempGrid);
                
                break;
            case GAMESTAGE.CALCULATEHITPOINT:
                CalculateDamageWhenEnterGrid();

                OnChangeGameStage(GAMESTAGE.BUYING);
                break;
            case GAMESTAGE.BUYING:
                if(currentPlayer.GetHitPoint() == 0)
                    OnChangeGameStage(GAMESTAGE.END);

                if(currentPlayer.GetGrid() is Coner)
                {
                    OnChangeGameStage(GAMESTAGE.END);
                }
                else if(currentPlayer.GetGrid() is Edge)
                {
                    if(!currentPlayer.AI)
                    {
                        buyButton.gameObject.SetActive(true);
                        skipButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        AIPlay();
                    }
                }
                break;
            case GAMESTAGE.END:
                PlayerController tempPlayer = currentPlayer;

                OnChangeTurn();

                if(IsPlayerGameOver(tempPlayer))
                {
                    if(IsLastPlayer())
                    {
                        OnGameEnd();
                        break;
                    }
                }

                OnChangeGameStage(GAMESTAGE.START);
                break;
        }
    }

    public void OnFinishToss(int _diceNumeber)
    {
        moveCount = _diceNumeber;
        dice.gameObject.SetActive(false);

        OnChangeGameStage(GAMESTAGE.MOVING);
    }

    void OnChangeTurn()
    {
        NextPlayerTurn();
    }

    TEAM NextPlayerTurn()
    {
        TEAM nextPlayer = TEAM.RED;

        if(currentPlayer == null)
        {
            currentPlayer = playerInGame[0];
            return nextPlayer; 
        }

        switch(playerTurn)
        {
            case TEAM.RED:
                if(playerInGame.Find(n => n.Team == TEAM.GREEN))
                    nextPlayer = TEAM.GREEN;
                else if(playerInGame.Find(n => n.Team == TEAM.BLUE))
                    nextPlayer = TEAM.BLUE;
                else if(playerInGame.Find(n => n.Team == TEAM.YELLOW))
                    nextPlayer = TEAM.YELLOW;
                break;
            case TEAM.GREEN:
                if(playerInGame.Find(n => n.Team == TEAM.BLUE))
                    nextPlayer = TEAM.BLUE;
                else if(playerInGame.Find(n => n.Team == TEAM.YELLOW))
                    nextPlayer = TEAM.YELLOW;
                else if(playerInGame.Find(n => n.Team == TEAM.RED))
                    nextPlayer = TEAM.RED;
                break;
            case TEAM.BLUE:
                if(playerInGame.Find(n => n.Team == TEAM.YELLOW))
                    nextPlayer = TEAM.YELLOW;
                else if(playerInGame.Find(n => n.Team == TEAM.RED))
                    nextPlayer = TEAM.RED;
                else if(playerInGame.Find(n => n.Team == TEAM.GREEN))
                    nextPlayer = TEAM.RED;
                break;
            case TEAM.YELLOW:
                if(playerInGame.Find(n => n.Team == TEAM.RED))
                    nextPlayer = TEAM.RED;
                else if(playerInGame.Find(n => n.Team == TEAM.GREEN))
                    nextPlayer = TEAM.RED;
                else if(playerInGame.Find(n => n.Team == TEAM.BLUE))
                    nextPlayer = TEAM.BLUE;
                break;
        }

        playerTurn = nextPlayer;
        currentPlayer = playerInGame.Find(n => n.Team == playerTurn);

        return playerTurn;
    }

    bool IsPlayerGameOver(PlayerController player)
    {
        if(player == null)
            return false;

        if(player.GetHitPoint() <= 0)
        {
            ResetEdgeWhenPlayerDie(player.Team);
            playerInGame.Remove(player);
            player.OnPlayerDie();
            return true;
        }

        return false;
    }

    bool IsLastPlayer()
    {
        return playerInGame.Count == 1;
    }

    void CalculateDamageWhenEnterGrid()
    {
        Grid tempGrid = currentPlayer.GetGrid();
        if(tempGrid is Coner)
        {
            tempGrid.GetComponent<Coner>().HealPlayer(currentPlayer);
        }

        if(tempGrid is Edge)
        {
            if(tempGrid.GetComponent<Edge>().owner != currentPlayer.Team)
                currentPlayer.TakeDamage(tempGrid.GetComponent<Edge>().GetDamage());
        }
    }

    public void BuyEdge()
    {
        currentPlayer.GetGrid().GetComponent<Edge>().Buy(currentPlayer);

        buyButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        OnChangeGameStage(GAMESTAGE.END);
    }

    public void SkipTurn()
    {
        if(currentGameStage == GAMESTAGE.BUYING)
        {
            buyButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(false);

            OnChangeGameStage(GAMESTAGE.END);
        }
    }

    public void OnStartGame()
    {
        if(playerInGame.Count < 2)
            return;

        if(int.Parse(InputHitPoint.text) < 1)
            return;

        foreach(var n in playerInGame)
        {
            n.SetHitPoint(int.Parse(InputHitPoint.text));
        }

        OnChangeGameStage(GAMESTAGE.START);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGameEnd()
    {
        switch(playerTurn)
        {
            case TEAM.RED:
                WinText.text = "<style=\"RedTeam\"> Red WIN!!!</style>";
                break;
            case TEAM.GREEN:
                WinText.text = "<style=\"GreenTeam\"> Green WIN!!!</style>";
                break;
            case TEAM.BLUE:
                WinText.text = "<style=\"BlueTeam\"> Blue WIN!!!</style>";
                break;
            case TEAM.YELLOW:
                WinText.text = "<style=\"YellowTeam\"> Yellow WIN!!!</style>";
                break;
        }

        WinText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        playerTurnText.gameObject.SetActive(false);
    }

    void ResetEdgeWhenPlayerDie(TEAM team)
    {
        foreach(var n in edges)
        {
            if(n.owner == team)
                n.ResetEdge();
        }
    }

    void OnDestroy() 
    {
        RemoveListenerButton();
    }

    public void OnClickPlayButton(TEAM team)
    {
        PlayerController _player;

        switch(team)
        {
            case TEAM.RED:
                _player = SpawnPlayer(redPlayerPrefab);
                playerUis[0].SetPlayer(_player);
                _player.Initialize();
                break;
            case TEAM.GREEN:
                _player = SpawnPlayer(greenPlayerPrefab);
                playerUis[1].SetPlayer(_player);
                _player.Initialize();
                break;
            case TEAM.BLUE:
                _player = SpawnPlayer(bluePlayerPrefab);
                playerUis[2].SetPlayer(_player);
                _player.Initialize();
                break;
            case TEAM.YELLOW:
                _player = SpawnPlayer(yellowPlayerPrefab);
                playerUis[3].SetPlayer(_player);
                _player.Initialize();
                break;
        }
    }

    public PlayerController SpawnPlayer(PlayerController playerPrefab)
    {
        PlayerController player = Instantiate(playerPrefab);

        playerInGame.Add(player);

        print(player.Team);

        switch(player.Team)
        {
            case TEAM.RED:
                player.SetStartGrid(coners[0]);
                break;
            case TEAM.GREEN:
                player.SetStartGrid(coners[1]);
                break;
            case TEAM.BLUE:
                player.SetStartGrid(coners[2]);
                break;
            case TEAM.YELLOW:
                player.SetStartGrid(coners[3]);
                break;
        }

        return player;
    }
    
    public void SelectFirstPlayer(TEAM team)
    {
        playerTurn = team;
        currentPlayer = playerInGame.Find(n => n.Team == playerTurn);
    }

    void AIPlay()
    {
        if(currentPlayer.AutoBuy())
            BuyEdge();
        else
            SkipTurn();
    }

    void UpdateTurnText()
    {
        switch(playerTurn)
        {
            case TEAM.RED:
                playerTurnText.text = "<style=\"RedTeam\"> Red Turn</style>";
                break;
            case TEAM.GREEN:
                playerTurnText.text = "<style=\"GreenTeam\"> Green Turn</style>";
                break;
            case TEAM.BLUE:
                playerTurnText.text = "<style=\"BlueTeam\"> Blue Turn</style>";
                break;
            case TEAM.YELLOW:
                playerTurnText.text = "<style=\"YellowTeam\"> Yellow Turn</style>";
                break;
        }

        playerTurnText.gameObject.SetActive(true);
    }
}
