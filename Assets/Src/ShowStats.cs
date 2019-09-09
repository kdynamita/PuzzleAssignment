using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text time;
    [SerializeField] private Text death;
    [SerializeField] private Text move;

    private Manager manager; 

    // Start is called before the first frame update
    void Start()
    {
        manager = Toolbox.GetInstance().GetManager();
        FindStats();
    }

    void FindStats()
    {
        score = GameObject.Find("ScoreText").GetComponent<Text>();
        time = GameObject.Find("TimeText").GetComponent<Text>();
        death = GameObject.Find("DeathText").GetComponent<Text>();
        move = GameObject.Find("MoveText").GetComponent<Text>();

        UpdateStats();
    }

    void UpdateStats()
    {
        score.text = manager.totalScore.ToString();
        death.text = manager.totalSpawned.ToString();
        move.text = manager.totalMoves.ToString();
    }
}
