using UnityEngine;

namespace ShapeBuilder
{
    [System.Serializable]
    public class PyramidShapeBuilder : IShapeBuilder
    {
        private readonly string _containerName = "ClassicModePyramidContainer";
        private readonly int _maxColumnSize = 10;
        private readonly int _maxBallCount = 15;

        public void Build(Vector3 spawnPosition, IShapeItem[] shapeItems)
        {
            if (shapeItems == null || shapeItems.Length <= 0)
            {
                Debug.LogError("Balls are null or empty");
            }

            var container = new GameObject().transform;
            container.name = _containerName;
            container.transform.position = Vector3.zero;

            foreach (var item in shapeItems)
            {
                item.Transform.parent = container;
            }

            int columnCount = 0;
            int pyramidaBallCount = _maxBallCount;
            float widthInterval = shapeItems[0].Size.x;
            float heightInterval = shapeItems[0].Size.z;

            for (int i = 1; i < _maxColumnSize; i++)
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
                    shapeItems[index].SetLocalPosition(spawnPosition);
                    index++;
                    continue;
                }
                bool isEvenNumber = i % 2 == 0;
                for (int j = 1; j <= i; j++)
                {
                    if (index >= shapeItems.Length)
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
                    shapeItems[index].SetLocalPosition(placePosition);
                    index++;
                }
            }
        }
    }
}
