using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;

    public StringChangedEvent ScoreChangedEvent = new StringChangedEvent();

    [SerializeField]
    private int scoreMultiplier = 1;

    private int score;
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            ScoreChangedEvent.Invoke(value.ToString());
        }
    }

    public void IncreaseScore(int value)
    {
        Score += (value * scoreMultiplier);
    }

    #region Setup
    void Awake()
    {
        Instance = this;
    }
    #endregion
}
