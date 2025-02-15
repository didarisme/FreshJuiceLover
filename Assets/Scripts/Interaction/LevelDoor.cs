using System;
using UnityEngine;

public class LevelDoor : Interactable
{
    [SerializeField] private bool isEntryDoor;

    public event Action EnterTheDoor;
    public event Action SaveLevelProgress;

    public override void OnInteract()
    {
        if (isEntryDoor)
        {
            EnterTheDoor?.Invoke();
        }
        else
        {
            SaveLevelProgress?.Invoke();
            SceneChanger.LoadSceneByInd(0);
        }
    }

    public override void OnTrigger()
    {
        if (isEntryDoor)
        {
            PlayerMind.ShowBubble?.Invoke(1);
        }
        else
        {
            PlayerMind.ShowBubble?.Invoke(3);
        }
    }
}
