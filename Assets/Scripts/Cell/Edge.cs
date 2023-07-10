using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDGELEVEL
{
    LV0,
    LV1,
    LV2,
    LV3,
}

public class Edge : Grid
{
    public TEAM owner;
    public EDGELEVEL level;
    public List<GameObject> LevelObjects = new List<GameObject>();
    public List<Material> teamColor = new List<Material>();


    void Start() 
    {
        SetupEdge();
    }

    void SetupEdge()
    {
        cellType = CELLTYPE.EDGE;
        ResetEdge();
    }

    public int GetDamage()
    {
        int damage = 0;
        switch(level)
        {
            case EDGELEVEL.LV0:
                damage = 0;
                break;
            case EDGELEVEL.LV1:
                damage = 1;
                break;
            case EDGELEVEL.LV2:
                damage = 2;
                break;
            case EDGELEVEL.LV3:
                damage = 3;
                break;
        }

        owner = TEAM.NONE;
        ChangeColorEdge();
        ResetLevelColor();
        level = EDGELEVEL.LV0;

        return damage;
    }

    public void Buy(PlayerController player)
    {
        if(owner == TEAM.NONE)
        {
            owner = player.Team;
            level = EDGELEVEL.LV1;
            ChangeColorEdge();
            LevelObjects[0].SetActive(true);
        }
        else
        {
            switch(level)
            {
                case EDGELEVEL.LV0:
                    break;
                case EDGELEVEL.LV1:
                    level = EDGELEVEL.LV2;
                    LevelObjects[1].SetActive(true);
                    player.TakeDamage(1);
                    break;
                case EDGELEVEL.LV2:
                    level = EDGELEVEL.LV3;
                    LevelObjects[2].SetActive(true);
                    player.TakeDamage(1);
                    break;
                case EDGELEVEL.LV3:
                    player.TakeDamage(0);
                    break;
            }
        }

        
    }

    void ChangeColorEdge()
    {
        switch(owner)
        {
            case TEAM.NONE:
                GetComponent<MeshRenderer>().material = teamColor[0];
                break;
            case TEAM.RED:
                GetComponent<MeshRenderer>().material = teamColor[1];
                break;
            case TEAM.GREEN:
                GetComponent<MeshRenderer>().material = teamColor[2];
                break;
            case TEAM.BLUE:
                GetComponent<MeshRenderer>().material = teamColor[3];
                break;
            case TEAM.YELLOW:
                GetComponent<MeshRenderer>().material = teamColor[4];
                break;
            
        }
    }

    void ResetLevelColor()
    {
        LevelObjects[0].SetActive(false);
        LevelObjects[1].SetActive(false);
        LevelObjects[2].SetActive(false);
    }

    public void ResetEdge()
    {
        owner = TEAM.NONE;
        level = EDGELEVEL.LV0;
        ChangeColorEdge();
        ResetLevelColor();
    }
}
