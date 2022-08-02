using ShapeBuilder;
using UnityEngine;

namespace ShapeBuilder
{
    public interface IShapeBuilder
    {
        void Build(Vector3 spawnPosition, IShapeItem[] shapeItems);
    }
}
