using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseCondition : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject ResultsHolder;
    public MaskHolder playerContestant;
    public MaskHolder[] contestants;

    private List<GameObject> ContestantResultsObjects;

    void OnEnable()
    {
        GameManager.onMatchEnd += Results;
    }

    void OnDisable()
    {
        GameManager.onMatchEnd -= Results;
    }

    void Start()
    {
        FillContestantResultsObjectList();
    }

    void FillContestantResultsObjectList()
    {
        ContestantResultsObjects = new List<GameObject>();
        foreach(Transform child in ResultsHolder.transform)
        {
            ContestantResultsObjects.Add(child.gameObject);
        }
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
        SortResults();
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

    void SortResults()
    {
        //first make a full contestants list including the player
        List<MaskHolder> allContestants =  new List<MaskHolder>();
        foreach(MaskHolder maskHolder in contestants)
        {
            allContestants.Add(maskHolder);
        }
        allContestants.Add(playerContestant);

        AssignResults(BubbleSortedList(allContestants, allContestants.Count));
        ResultsHolder.SetActive(true);
    }

    List<MaskHolder> BubbleSortedList(List<MaskHolder> originalList, int originalLength)
    {
        int i, j;
        MaskHolder temp;
        bool swapped;

        for (i = 0; i < originalLength - 1; i++) {
            swapped = false;
            for (j = 0; j < originalLength - i - 1; j++) {
                if (originalList[j].currentContestantPoints < originalList[j + 1].currentContestantPoints) {
                    // Swap arr[j] and arr[j+1]
                    temp = originalList[j];
                    originalList[j] = originalList[j + 1];
                    originalList[j + 1] = temp;
                    swapped = true;
                }
            }

            // If no two elements were swapped by inner loop, then break
            if (swapped == false)
                break;
        }

        return originalList;
    }

    void AssignResults(List<MaskHolder> maskHolders)
    {
        if(maskHolders.Count == ContestantResultsObjects.Count)
        {
            for(int i=0; i < ContestantResultsObjects.Count; i++)
            {
                Sprite contestantIcon = maskHolders[i].pointsText.gameObject.GetComponentInParent<Image>().sprite;
                string contestantPoints = maskHolders[i].pointsText.text;

                ContestantResultsObjects[i].GetComponent<Image>().sprite = contestantIcon;
                ContestantResultsObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = contestantPoints; 
            }
        }
        else
        {
            Debug.LogWarning("Make sure list of contestants matches list of result objects");
        }
    }
}
