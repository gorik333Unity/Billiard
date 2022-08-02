using ShapeBuilder;
using UnityEngine;

namespace Tools
{
    public class Ball : MonoBehaviour, IShapeItem
    {
        [SerializeField]
        private MeshFilter _model;

        [SerializeField]
        private Rigidbody _rigidbody;

        public Transform Transform => transform;
        public Vector3 Position => transform.position;
        public Vector3 Size { get; private set; }
        public Rigidbody Rigidbody => _rigidbody;

        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        private void Awake()
        {
            InitializeSize();
        }

        private void InitializeSize()
        {
            if (_model == null)
            {
                Debug.LogError("Model is null");
                return;
            }

            var meshSize = _model.sharedMesh.bounds.size;
            var localScale = _model.transform.localScale;

            Size = Vector3.Scale(meshSize, localScale);
        }
    }
}
