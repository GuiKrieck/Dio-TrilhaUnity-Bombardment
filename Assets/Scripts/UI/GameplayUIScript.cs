using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUIScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        highestScoreLabel.text = GameManager.Instance.getHighestScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        highestScoreLabel.text = GameManager.Instance.getHighestScore().ToString();
    }
}
