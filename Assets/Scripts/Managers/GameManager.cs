using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Level
{
    public Animator door;
    public List<StackTracker> receivers;
    public string animParameter;

    public void CompleteLevel()
    {
        door.SetBool(animParameter, IsLevelComplete());
    }
    
    private bool IsLevelComplete()
    {
        foreach (StackTracker receiver in receivers)
        {
            if (receiver.IsActive == false)
            {
                return false;
            }
        }

        return true;
    }
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<Level> levels = new List<Level>();
    
    private void Awake()
    {
        Instance = this;

        foreach (Level level in levels)
        {
            foreach (StackTracker receiver in level.receivers)
            {
                receiver.statusAlteredEvent.AddListener(level.CompleteLevel);
            }
        }
    }
}
