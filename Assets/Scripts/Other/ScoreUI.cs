using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString("N0"); // ƒJƒ“ƒ}‹æØ‚è‚Å•\¦
    }
}
