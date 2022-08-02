using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBall : MonoBehaviour
{
    [SerializeField]
    private CueStick _cueStick;

    [SerializeField]
    private MeshFilter _model;

    private bool _block;
    private float _distance;

    private void Start()
    {
        var size = Vector3.Scale(_model.sharedMesh.bounds.size, _model.transform.localScale);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _block = true;
        }

        if (!_block)
            _distance = Position.z - _cueStick.Position.z;
    }

    public Vector3 Position => transform.position;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_cueStick.Position + _cueStick.transform.forward * _distance, 0.025f);
    }
}
