using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int diceFace = 6;

    public int diceNumber = 0;

    Animator animator;

    void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void OnToss()
    {
        if(GameManager.inst.currentGameStage != GAMESTAGE.ROLLING)
            return;

        animator.SetTrigger("Toss");
    }

    public void Tossing()
    {
        diceNumber = Random.Range(0,diceFace) + 1;

        animator.SetBool( $"Face {diceNumber}",true);
    }

    public void OnTossed()
    {
        animator.SetBool($"Face {diceNumber}",false);

        GameManager.inst.OnFinishToss(diceNumber);
    }
}
