using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public TEAM playerTurn;
    PlayerController currentPlayer;

    int moveCount = 0;

    void Awake()
    {
        inst = this;        
    }

    void Start() 
    {
        OnChangeGameStageStage(GAMESTAGE.WAITINGFORSTART);
    }

    void OnChangeGameStageStage(GAMESTAGE stage)
    {
        currentGameStage = stage;

        switch(currentGameStage)
        {
            // case GAMESTAGE.CREATEBOARD:
            //     break;
            case GAMESTAGE.WAITINGFORSTART:
                break;
            case GAMESTAGE.START:
                OnChangeTurn();
                break;
            case GAMESTAGE.ROLLING:
                break;
            case GAMESTAGE.MOVING:
                break;
            case GAMESTAGE.CALCULATEHITPOINT:
                break;
            case GAMESTAGE.BUYING:
                break;
            case GAMESTAGE.END:
                break;
        }
    }

    public void OnFinishToss(int _diceNumeber)
    {
        moveCount = _diceNumeber;

        OnChangeGameStageStage(GAMESTAGE.MOVING);
    }

    void OnChangeTurn()
    {
        switch(playerTurn)
        {
            case TEAM.RED:
                NextPlayerTurn();
                break;
            case TEAM.GREEN:
                NextPlayerTurn();
                break;
            case TEAM.BLUE:
                NextPlayerTurn();
                break;
            case TEAM.YELLOW:
                NextPlayerTurn();
                break;
        }
    }

    TEAM NextPlayerTurn()
    {
        TEAM nextPlayer = TEAM.RED;

        for(int i = 0; i < playerInGame.Count; i++)
        {
            if(playerInGame[i] == currentPlayer)
            {
                if(i == playerInGame.Count-1)
                {
                    nextPlayer = playerInGame[0].Team;
                    currentPlayer = playerInGame[0];
                }

                nextPlayer = playerInGame[i+1].Team;
                currentPlayer = playerInGame[i+1];
            }
        }

        return nextPlayer;
    }

    void IsPlayerGameOver()
    {
        if(currentPlayer.HitPoint >= 0)
        {
            //TODO: Game Over
        }
    }
    
}
