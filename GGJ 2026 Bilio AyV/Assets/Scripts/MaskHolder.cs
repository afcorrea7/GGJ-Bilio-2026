using TMPro;
using UnityEngine;

public class MaskHolder : MonoBehaviour
{
    public bool hasMask;
    public int currentContestantPoints;
    public TextMeshProUGUI pointsText;
    public GameObject maskOnContestantObject;
    public GameObject maskPickUpObject; //make it reappear on drop;

    [Header("Event Triggereres")]
    public GameEvent MaskNewOwner;
    public GameEvent MaskDropped;

    void Start()
    {
        pointsText.text = "0";
    }

    public void ObtainMask()
    {
        hasMask = true;
        maskOnContestantObject.SetActive(true);
        
        MaskNewOwner.TriggerEvent();
    }

    public void LoseMask()
    {
        if (hasMask)
        {
            hasMask = false;
            maskOnContestantObject.SetActive(false);
            DropMask();
            MaskDropped.TriggerEvent();
        }

    }

    void DropMask()
    {
        maskPickUpObject.transform.position = transform.position;
        maskPickUpObject.SetActive(true);
    }

    public void UpdatePoints(int incomingPoints)
    {
        currentContestantPoints += incomingPoints;
        pointsText.text = currentContestantPoints.ToString();
    }
}
