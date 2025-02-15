using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data/New Level Data")]
public class LevelDataSO : ScriptableObject
{
    public string Name;
    public Sprite LevelIcon;
    public int SceneInd;
    public string Description;
}
