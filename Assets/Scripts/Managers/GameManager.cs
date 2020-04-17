using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        Instance = this;
    }
}
