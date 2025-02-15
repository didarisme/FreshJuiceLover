using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer levelIconSprite;
    [SerializeField] private Text levelName;
    [SerializeField] private Text fruitsCounter;
    [SerializeField] private Text levelDecription;

    [SerializeField] private LevelDataSO[] levels;
    [SerializeField] private Lever[] levers;

    [SerializeField] private LevelDoor levelDoor;

    private int curLevelInd = 0;

    private void OnEnable()
    {
        levers[0].OnUseLever += PrevLevel;
        levers[1].OnUseLever += NextLevel;

        levelDoor.EnterTheDoor += StartLevel;
    }

    private void OnDisable()
    {
        levers[0].OnUseLever -= PrevLevel;
        levers[1].OnUseLever -= NextLevel;

        levelDoor.EnterTheDoor -= StartLevel;
    }

    private void Start()
    {
        FillDescription();
    }

    private void PrevLevel()
    {
        if (curLevelInd - 1 < 0)
            return;

        ChangeLevel(curLevelInd - 1);
    }

    private void NextLevel()
    {
        if (curLevelInd + 1 >= levels.Length)
            return;

        ChangeLevel(curLevelInd + 1);
    }

    private void ChangeLevel(int levelInd)
    {
        curLevelInd = levelInd;
        FillDescription();
    }

    private void FillDescription()
    {
        levelIconSprite.sprite = levels[curLevelInd].LevelIcon;
        levelName.text = levels[curLevelInd].Name;
        fruitsCounter.text = PlayerPrefs.GetInt("Level(" + levels[curLevelInd].SceneInd + ")Fruits", 0).ToString() + "/3 Fruits collected";
        levelDecription.text = levels[curLevelInd].Description;
    }

    private void StartLevel()
    {
        SceneChanger.LoadSceneByInd(curLevelInd + 1);
    }
}