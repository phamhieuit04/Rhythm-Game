using TMPro;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    public static RuntimeUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maxComboText;


    private int score = 0;
    private int maxScore = 0;
    private int maxCombo = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        maxCombo = 0;
    }

    private void Update()
    {
        timeText.text = ((int)audioSource.time).ToString();
    }

    public void SetMaxCombo(int maxCombo)
    {
        this.maxCombo = maxCombo;
        maxComboText.text = maxCombo.ToString();
    }

    public void SetRunTimeText(string text)
    {
        switch (text)
        {
            case "Perfect":
                score += 3;
                break;
            case "Good":
                score += 2;
                break;
            case "Bed":
                score += 1;
                break;
        }
        scoreText.text = score.ToString() + "/" + maxScore.ToString();
    }
    public void SetMaxScore(int maxScore)
    {
        this.maxScore = maxScore;
        scoreText.text = score.ToString() + "/" + maxScore.ToString();
    }
}
