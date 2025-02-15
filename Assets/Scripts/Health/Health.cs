using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3;
    public float currentHealth { get; private set; }

    private PlayerTriggers playerTriggers;
    private SceneChanger sceneChanger;

    private void Awake()
    {
        currentHealth = startingHealth;

        playerTriggers = FindObjectOfType<PlayerTriggers>();
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    private void OnEnable()
    {
        sceneChanger.OnRespawn += Respawn;
    }

    private void OnDisable()
    {
        sceneChanger.OnRespawn -= Respawn;
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(0.5f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //set animation
        }
        else
        {
            playerTriggers.OnDeath?.Invoke();
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
    }
}
