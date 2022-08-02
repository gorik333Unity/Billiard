using Cinemachine;
using EnviromentModule;
using System.Collections;
using Tools;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    private readonly Vector3 _cuePosition = new Vector3(0f, 0.21f, -0.36f);
    private readonly Vector3 _boardPosition = new Vector3(0f, 1.5f, 0.54f);

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField]
    private Board _board;

    [SerializeField]
    private CueStick _cue;

    [SerializeField]
    private Ball _mainBall;

    [SerializeField]
    private float _multiplier;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(FromCueToBoard());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(FromBoardToCue());
        }
    }

    private IEnumerator FromCueToBoard()
    {
        var transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _virtualCamera.LookAt = _board.transform;
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime * _multiplier;
            var lerp = Vector3.Lerp(_cuePosition, _boardPosition, time);
            transposer.m_FollowOffset = lerp;

            if (time > 1f)
            {
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator FromBoardToCue()
    {
        var transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime * _multiplier;
            var lerp = Vector3.Lerp(_boardPosition, _cuePosition, time);
            transposer.m_FollowOffset = lerp;

            if (time > 1f)
            {
                _virtualCamera.LookAt = _cue.FollowPoint;
                yield break;
            }

            yield return null;
        }
    }
}
