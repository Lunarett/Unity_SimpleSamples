using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreValueText;

    public void SetScoreText(string text)
    {
        scoreValueText.text = text;
    }
}
