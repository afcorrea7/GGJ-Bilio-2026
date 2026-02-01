using UnityEngine;
using UnityEngine.Assertions.Must;

public class WinLoseCondition : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;
    public MaskHolder playerContestant;
    public MaskHolder[] contestants;

    void OnEnable()
    {
        GameManager.onMatchEnd += Results;
    }

    void Results()
    {
        if (IsPlayerVictory())
        {
            WinPanel.gameObject.SetActive(true);
        }
        else
        {
            LosePanel.gameObject.SetActive(true);
        }
    }

    bool IsPlayerVictory()
    {
        bool playerHasMaxPoints = true;
        for(int i=0; i < contestants.Length; i++)
        {
            if(playerContestant.currentContestantPoints < contestants[i].currentContestantPoints)
            {
                playerHasMaxPoints = false;
            }
        }

        return playerHasMaxPoints;
    }
}
