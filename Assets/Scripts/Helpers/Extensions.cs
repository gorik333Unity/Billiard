using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

namespace Base.Extension
{
    public static class Extensions
    {
        private static System.Random _random = new System.Random();

        public static void SetActive(this CanvasGroup canvasGroup, bool state)
        {
            canvasGroup.alpha = state ? 1 : 0;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }

        public static void SynchronizeRotationWithWheel(this Transform transform, WheelCollider wheelCollider)
        {
            var rotation = new Vector3(0f, 0f, wheelCollider.rpm / 60 * 360 * Time.deltaTime);

            transform.Rotate(rotation);
        }

        public static float GetPercentValue(float value, float percent)
        {
            return value * (percent / 100);
        }

        public static void Shuffle<T>(this IList<T> values)
        {
            for (int i = values.Count - 1; i > 0; i--)
            {
                int k = _random.Next(i + 1);
                T value = values[k];
                values[k] = values[i];
                values[i] = value;
            }
        }

        public static Vector2Int StringToVector2Int(string vector)
        {
            MatchCollection matches = Regex.Matches(vector, @"\d+");

            string[] result = matches.Cast<Match>()
                                     .Take(2)
                                     .Select(match => match.Value)
                                     .ToArray();

            Vector2Int vectorInt = Vector2Int.zero;

            vectorInt.x = int.Parse(result[0]);
            vectorInt.y = int.Parse(result[1]);

            return vectorInt;
        }
    }

    public static class Vector3Extensions
    {
        /// <summary>
        /// Calculate distance beetween two vectors
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns> Returns distance </returns>
        public static float PerformanceDistance(Vector3 a, Vector3 b)
        {
            var distance = (a - b).sqrMagnitude;

            return Mathf.Sqrt(distance);
        }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }
    }

    public static class GameObjectExtensions
    {
        public static void SetActive(GameObject gameObject, CoroutineRunner coroutineRunner, float delay)
        {
            coroutineRunner.StartCoroutine(Deactivate(gameObject, delay));
        }

        private static IEnumerator Deactivate(GameObject gameObject, float delay)
        {
            yield return new WaitForSeconds(delay);

            gameObject.SetActive(false);
        }
    }

    public static class EnumerableHelper<E>
    {
        private static System.Random r;

        static EnumerableHelper()
        {
            r = new System.Random();
        }

        public static T Random<T>(IEnumerable<T> input)
        {
            return input.ElementAt(r.Next(input.Count()));
        }
    }

    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.Random(input);
        }
    }
}