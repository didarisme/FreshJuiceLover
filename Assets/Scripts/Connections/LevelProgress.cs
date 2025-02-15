using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private LevelDoor levelDoor;

    public static Action PickUpFruit;

    private string saveFruitsKey;
    private int fruits = 0;

    private void OnEnable()
    {
        levelDoor.SaveLevelProgress += SaveProgress;
        PickUpFruit += AddFruit;
    }

    private void OnDisable()
    {
        levelDoor.SaveLevelProgress -= SaveProgress;
        PickUpFruit -= AddFruit;
    }

    private void Start()
    {
        saveFruitsKey = "Level(" + SceneManager.GetActiveScene().buildIndex + ")Fruits";
    }

    private void AddFruit()
    {
        fruits++;
    }

    private void SaveProgress()
    {
        if (fruits > PlayerPrefs.GetInt(saveFruitsKey, 0))
        {
            PlayerPrefs.SetInt(saveFruitsKey, fruits);
            PlayerPrefs.Save();
        }
    }
}
