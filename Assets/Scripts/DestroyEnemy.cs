using UnityEngine;
using UnityEngine.Events;

public class DestroyEnemy : MonoBehaviour
{
    private readonly int enemiesNextLevel = 15;
    private readonly int numberOfLevels = 4;
    private static int enemiesCounter = 0;

    private Animator blink;
    private SpriteRenderer spriteRenderer;

    public GameObject winScreenUI;

    public UnityEvent IncreaseFlagsEvent;


    public EnemyController enemyController;

    private void Start()
    {
        blink = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyController = enemyController.GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D fire)
    {
        enemyController.IsDestroyed = true;
        blink.SetTrigger("getsHit");
        
        enemiesCounter++;

        if (enemiesCounter % enemiesNextLevel == 0 & enemiesCounter < numberOfLevels * enemiesNextLevel)
        {
            IncreaseFlagsEvent?.Invoke();
        }
        else if(enemiesCounter == numberOfLevels * enemiesNextLevel)
        {
            winScreenUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (spriteRenderer.color != Color.white)
        { 
            spriteRenderer.color = Color.white; 
        }
    }
}
