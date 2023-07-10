using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Grid currentGrid;
    Grid distinationGrid;

    public TEAM Team = TEAM.RED;
    public bool AI;

    int currentHitPoint;

    [HideInInspector]
    public Transform positionOnGrid;

    public UnityAction updateText;


    public void Initialize()
    {   
        currentGrid.PlayerMoveIn(this);
    }

    public void SetHitPoint(int hitPoint)
    {
        currentHitPoint = hitPoint;
        updateText();
    }

    public void MoveToNextGrid()
    {
        currentGrid.PlayerMoveOut(this);

        currentGrid = currentGrid.GetNextCell();

        currentGrid.PlayerMoveIn(this);
    }

    public void UpdatePosition()
    {
        Vector3 nextPos = new Vector3(positionOnGrid.position.x,transform.position.y,positionOnGrid.position.z);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(nextPos,1f));
        sequence.AppendInterval(.5f);
        sequence.OnComplete(() => {
            if(distinationGrid == null)
                return;

            if(Team == GameManager.inst.playerTurn)
            {
                if(currentGrid != distinationGrid)
                    MoveToNextGrid();
                else
                    GameManager.inst.OnChangeGameStage(GAMESTAGE.CALCULATEHITPOINT);
            }
        });
    }

    public Grid GetGrid()
    {
        return currentGrid;
    }

    public Grid GetDistinationGrid()
    {
        return distinationGrid;
    }

    public void SetStartGrid(Grid grid)
    {
        currentGrid = grid;
    }

    public int GetHitPoint()
    {
        return currentHitPoint;
    }

    public void Heal(int amount)
    {
        currentHitPoint += amount;
        updateText();
    }

    public void TakeDamage(int amount)
    {
        currentHitPoint -= amount;
        updateText();
    }

    public void OnPlayerDie()
    {
        currentGrid.PlayerMoveOut(this);

        Destroy(this.gameObject);
    }

    public void SetDistinationGrid(Grid grid)
    {
        distinationGrid = grid;
        MoveToNextGrid();
    }

#region AI
    public bool AutoBuy()
    {
        if(currentHitPoint >= 3)
            return true;
        else
            return false;
    }
#endregion
}
