using UnityEngine;
using UnityEngine.Events;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Roulette roulette1;
    [SerializeField] private Arrow arrow1;

    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private ScoreCounter bid;
    
    

    public UnityEvent arrowCoincidedEvent;
    public UnityEvent endSpinsEvent;
    
    public UnityEvent addedScoreEvent;
    public UnityEvent remowedScoreEvent;

    private int endWorkSlots = 0;

    public void SetEndSlot()
    {
        endWorkSlots++;
        GetResults();
    }

    private void OnEnable()
    {
        roulette1.endRotateEvent?.AddListener(SetEndSlot);
     
    }

    private void OnDisable()
    {
        roulette1.endRotateEvent.RemoveListener(SetEndSlot);
      
    }

    private float GetValue()
    {
  
        return arrow1.collidedObject.Value * bid.Score;
    }

    public void GetResults()
    {
        if (endWorkSlots == 1)
        {
            if (arrow1.collidedObject.Value > 0)
            {
                addedScoreEvent?.Invoke();
                scoreCounter.Add(GetValue());
            }
            else
            {
                scoreCounter.TakeAway(-GetValue());
                remowedScoreEvent?.Invoke();
            }
            endSpinsEvent?.Invoke();
            endWorkSlots = 0;
        }
    }
}
