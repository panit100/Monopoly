using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] Button tossButton;
    public int diceFace = 6;
    public int diceNumber = 0;

    Animator animator;

    void Awake() 
    {
        animator = GetComponent<Animator>();

        AddListenerButton();
    }

    void AddListenerButton()
    {
        tossButton.onClick.AddListener(OnToss);
    }

    void RemoveListenerButton()
    {
        tossButton.onClick.RemoveAllListeners();
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

    void OnDestroy() 
    {
        RemoveListenerButton();
    }

    public void EnableButton(bool enable)
    {
        tossButton.gameObject.SetActive(enable);
    }
}
