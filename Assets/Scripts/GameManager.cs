using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GAMESTAGE
{
    START,
    ROLLING,
    MOVING,
    BUYING,
    END,
}

[Flags]
public enum PLAYER
{
    NONE = 0,
    RED = 1,
    GREEN = 2,
    BLUE = 4,
    YELLOW = 8,
}

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GAMESTAGE currentGameStage;
    public PLAYER playerInGame;

    public PLAYER playerTurn;

    void Awake()
    {
        inst = this;        
    }

    void OnChangeGameStageStage(GAMESTAGE stage)
    {
        currentGameStage = stage;

        switch(currentGameStage)
        {
            case GAMESTAGE.START:
                break;
            case GAMESTAGE.ROLLING:
                break;
            case GAMESTAGE.MOVING:
                break;
            case GAMESTAGE.BUYING:
                break;
            case GAMESTAGE.END:
                break;
        }
    }

    void OnChangeTurn()
    {
        switch(playerTurn)
        {
            case PLAYER.RED:
                break;
            case PLAYER.GREEN:
                break;
            case PLAYER.BLUE:
                break;
            case PLAYER.YELLOW:
                break;
        }
    }

    
}
