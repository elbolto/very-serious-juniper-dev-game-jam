using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreManager : MonoBehaviour
{
    public Transform shipPosition;
    public Player player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endingText;
    public GameObject fuelSlider;
    public float scoreDivision;
    private float _score;
    //tracks last x position
    private float _temp;
    private float _max;
    private float _startX; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startX = shipPosition.position.x;
        _max = _startX;
        _score = 0;
        fuelSlider.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        endingText.gameObject.SetActive(false);
        player.OnRespawned += Reset;
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = shipPosition.position.x;
        if(_max < currentX && player.IsAlive())
        {
            _max = currentX;
            //divide by a value so points are lower
            _score = (_max - _startX) / scoreDivision;
            //convert to int
            scoreText.text = Mathf.FloorToInt(_score).ToString();
        }
        if(!player.IsAlive())
        {
            Ending();
        }
        _temp = shipPosition.position.x;
        
    }

    private void Ending()
    {
        endingText.text = "Final score: " + Mathf.FloorToInt(_score).ToString();
        scoreText.gameObject.SetActive(false);
        endingText.gameObject.SetActive(true);
        fuelSlider.gameObject.SetActive(false);
    }

    private void Reset()
    {
        _score = 0;
        _startX = shipPosition.position.x;
        _max = _startX;
        scoreText.gameObject.SetActive(true);
        endingText.gameObject.SetActive(false);
        fuelSlider.gameObject.SetActive(true);
    }
}
