using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class TestBuildPyramid : MonoBehaviour
{
    private readonly string _pyramidName = "ClassicModePyramid";
    private readonly int _pyramidBallCount = 15;
    private readonly int _maxPyramidSize = 10;

    [SerializeField]
    private Ball _ballPrefab;

    [SerializeField]
    private Transform _spawnPoint;

    private Transform _parent;

    private void Start()
    {
        _parent = new GameObject().transform;
        _parent.transform.position = _spawnPoint.position;
        BuildPyramid(_spawnPoint.position);
    }

    private void BuildPyramid(Vector3 spawnPosition)
    {
        var balls = new List<Ball>();
        var pyramid = new GameObject().transform;
        pyramid.name = _pyramidName;
        pyramid.transform.position = spawnPosition;

        for (int i = 0; i < _pyramidBallCount; i++)
        {
            var ball = Instantiate(_ballPrefab, pyramid);
            ball.transform.localPosition = Vector3.zero;

            balls.Add(ball);
        }

        if (balls == null || balls.Count <= 0)
        {
            Debug.LogError("Balls are null or empty");
        }

        int columnCount = 0;
        int pyramidaBallCount = _pyramidBallCount;
        float widthInterval = balls[0].Size.x;
        float heightInterval = balls[0].Size.z;

        for (int i = 1; i < _maxPyramidSize; i++)
        {
            pyramidaBallCount -= i;
            columnCount++;

            if (pyramidaBallCount <= 0)
            {
                break;
            }
        }

        int index = 0;

        for (int i = 1; i <= columnCount; i++)
        {
            if (i == 1)
            {
                balls[index].transform.localPosition = Vector3.zero;
                index++;
                continue;
            }
            bool isEvenNumber = i % 2 == 0;
            for (int j = 1; j <= i; j++)
            {
                if (index >= balls.Count)
                {
                    return;
                }

                var placePosition = spawnPosition;
                if (isEvenNumber)
                {
                    placePosition.x -= (widthInterval / 2) + (widthInterval * (i / 2 - 1));
                }
                else
                {
                    placePosition.x -= widthInterval * (i / 2);
                }

                placePosition.x += (j - 1) * widthInterval;
                placePosition.z += heightInterval * (i - 1);
                balls[index].transform.position = placePosition;
                index++;
            }
        }
    }
}
