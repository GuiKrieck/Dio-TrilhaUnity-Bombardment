using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUIScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;
    [SerializeField] public TextMeshProUGUI ShipLife;
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
        ShipLife.text = (GameManager.Instance.ShipLife.health.ToString() + " / " + GameManager.Instance.ShipLife.maxHealth.ToString());
    }
}
