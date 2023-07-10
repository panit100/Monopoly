using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TEAM uiTeam;

    [SerializeField] PlayerController player;
    
    [SerializeField] TMP_Text HPText;
    [SerializeField] Button playButton;
    [SerializeField] Button firstButton;
    [SerializeField] Button AIButton;

    void Start() 
    {
        AddListenerButton();
    }

    public void SetPlayer(PlayerController _player)
    {
        player = _player;
        player.updateText += UpdateHPText;
    }

    public void EnablePlayButton(bool enable)
    {
        playButton.gameObject.SetActive(enable);
    }

    public void EnableFirstButton(bool enable)
    {
        firstButton.gameObject.SetActive(enable);
    }

    public void EnableAIButton(bool enable)
    {
        AIButton.gameObject.SetActive(enable);
    }

    public void UpdateHPText()
    {
        HPText.text = $"HP : {player.GetHitPoint()}";
    }

    void AddListenerButton()
    {
        firstButton.onClick.AddListener(OnClickFirstButton);
        AIButton.onClick.AddListener(OnClickAI);
    }

    void RemoveListenerButton()
    {
        firstButton.onClick.RemoveAllListeners();
        AIButton.onClick.RemoveAllListeners();
    }

    public void OnClickPlayButton(string team)
    {
        if(GameManager.inst.currentGameStage != GAMESTAGE.WAITINGFORSTART)
            return;

        switch(team)
        {
            case "Red":
                GameManager.inst.OnClickPlayButton(TEAM.RED);
                break;
            case "Green":
                GameManager.inst.OnClickPlayButton(TEAM.GREEN);
                break;
            case "Blue":
                GameManager.inst.OnClickPlayButton(TEAM.BLUE);
                break;
            case "Yellow":
                GameManager.inst.OnClickPlayButton(TEAM.YELLOW);
                break;
        }

        EnablePlayButton(false);
        EnableFirstButton(true);
        EnableAIButton(true);
    }

    public void OnClickFirstButton()
    {
        if(GameManager.inst.currentGameStage != GAMESTAGE.WAITINGFORSTART)
            return;

        GameManager.inst.SelectFirstPlayer(uiTeam);

        foreach(var n in GameManager.inst.playerUis)
        {
            if(GameManager.inst.playerInGame.Find(m => (m.Team == n.uiTeam) && !m.AI))
                n.EnableFirstButton(true);
        }

        EnableFirstButton(false);
    }

    public void OnClickAI()
    {
        player.AI = true;
        EnableFirstButton(false);
        EnableAIButton(false);
    }

    void OnDestroy() 
    {
        RemoveListenerButton();
    }
}
