using Base.Extension;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameModule
{
    public class HorizontalCueRotator : ICueRotator
    {
        private readonly float _sensivity = 6f;
        private readonly int UI_Layer = LayerMask.NameToLayer("UI");

        private CoroutineRunner _coroutineRunner;
        private Coroutine _horizontalRotateC;
        private Cue _cue;
        private Vector2 _startTouchPosition;
        private Vector2 _previousTouchPosition;
        private bool _isContinueOverUI;

        public void Activate()
        {
            _horizontalRotateC = _coroutineRunner.StartCoroutine(HorizontalRotate());
        }

        public void Deactivate()
        {
            StopHorizontalRotate();
        }

        public void Initialize()
        {

        }

        public void Initialize(Cue cue, CoroutineRunner coroutineRunner)
        {
            _cue = cue;
            _coroutineRunner = coroutineRunner;
        }

        private void StopHorizontalRotate()
        {
            if (_horizontalRotateC != null)
            {
                _coroutineRunner.StopCoroutine(_horizontalRotateC);
                _horizontalRotateC = null;
            }
        }

        private IEnumerator HorizontalRotate()
        {
            var ball = _cue.MainBall;

            yield return null;

            while (true)
            {
                if (Input.touchCount > 0)
                {
                    var touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        _startTouchPosition = touch.position;
                        _previousTouchPosition = touch.position;
                        _isContinueOverUI = false;
                    }

                    bool isOverUI = Extensions.IsPointerOverUIObject(touch.position, EventSystem.current);
                    if (isOverUI || _isContinueOverUI)
                    {
                        _isContinueOverUI = true;

                        yield return null;
                        continue;
                    }

                    var delta = touch.position - _previousTouchPosition;
                    _cue.transform.RotateAround(ball.Position, Vector3.up, -delta.x * Time.deltaTime * _sensivity);
                    _previousTouchPosition = touch.position;
                }

                yield return null;
            }
        }
    }
}
