using UnityEngine;

namespace ShapeBuilder
{
    public interface IShapeItem 
    {
        Vector3 Size { get; }
        Transform Transform { get; }
        void SetLocalPosition(Vector3 position); 
    }
}
