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
    private float _traveledDistance; 
    private Vector3 _lastPosition; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _traveledDistance = 0;
        _score = 0;
        fuelSlider.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        endingText.gameObject.SetActive(false);
        player.OnRespawned += Reset;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.IsAlive())
        {
            _traveledDistance += (_lastPosition - shipPosition.position).magnitude;
            _lastPosition = shipPosition.position;

            //divide by a value so points are lower
            _score = _traveledDistance/scoreDivision;
            
            //convert to int
            scoreText.text = Mathf.FloorToInt(_score).ToString();
        }

        if(!player.IsAlive())
        {
            Ending();
        }
        
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
        _lastPosition = shipPosition.position;
        scoreText.gameObject.SetActive(true);
        endingText.gameObject.SetActive(false);
        fuelSlider.gameObject.SetActive(true);
    }
}
