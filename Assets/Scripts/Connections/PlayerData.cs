using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Vector3 newPosition;
    private Transform cameraTransform;
    private bool isBtnPressed;

    private static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBtnPressed()
    {
        isBtnPressed = true;
    }

    public bool LoadPlayerPosition(GameObject player, GameObject playBtn)
    {
        if (!isBtnPressed)
            return isBtnPressed;

        player.transform.position = newPosition;
        cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(newPosition.x, 0, -10);
        playBtn.SetActive(false);
        return isBtnPressed;
    }
}
