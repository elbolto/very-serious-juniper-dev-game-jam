using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform shipPosition;
    public TextMeshProUGUI _scoreText;
    public float scoreDivision;
    private float _score;
    //tracks last x position
    private float _temp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _temp = shipPosition.position.x;
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = shipPosition.position.x - _temp;
        if(offset > 0)
        {
            //divide by a value so points are lower
            _score += offset / scoreDivision;
            //convert to int
            _scoreText.text = Mathf.FloorToInt(_score).ToString();
        }

        _temp = shipPosition.position.x;
    }
}
