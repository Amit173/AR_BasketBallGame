using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreChecking : MonoBehaviour
{
    // Start is called before the first frame update
    public static int HighestScore;
    public TextMeshProUGUI HighestScoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HighestScoreText.text = HighestScore.ToString();
    }
}
