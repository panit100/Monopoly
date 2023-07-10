using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CELLTYPE
{
    CONER,
    EDGE,
}

public class Grid : MonoBehaviour
{
    public CELLTYPE cellType;

    [SerializeField] Grid NextCell;

    [SerializeField] List<Transform> positionOnGrids = new List<Transform>();

    [SerializeField] List<PlayerController> playerOnGrid = new List<PlayerController>();

    public virtual void PlayerMoveIn(PlayerController player)
    {
        playerOnGrid.Add(player);

        UpdatePlayerPosition();
    }

    public virtual void PlayerMoveOut(PlayerController player)
    {
        playerOnGrid.Remove(player);

        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition()
    {
        for(int player = 0; player < playerOnGrid.Count; player++)
        {
            playerOnGrid[player].positionOnGrid = positionOnGrids[player];
            playerOnGrid[player].UpdatePosition();
        }
    }

    public Grid GetNextCell()
    {
        return NextCell;
    }

    public List<Transform> GetPositionOnGrids()
    {
        return positionOnGrids;
    }
}
