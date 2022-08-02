using GameModule;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    private IGameInitializer _gameInitializer;

    private void Awake()
    {
        _gameInitializer.Initialize();
    }
}
