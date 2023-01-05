using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    //counting is done from the top
    public GameObject[] enemyFirstLine;
    public GameObject[] enemySecondLine;
    public GameObject[] enemyThirdLine;
    public GameObject[] enemyFourthLine;

    private readonly float repeatRateSlow = 0.6f;
    private readonly float timeDelayInvisibleEnemy = 0.4f;

    private AudioSource missedEnemy;
    public bool IsDestroyed { get; set; }

    public UnityEvent EnemyIsMissing;

    private void Start()
    {
        EnemyCall();
        missedEnemy = GetComponent<AudioSource>();
    }

    private void RandomEnemyMovement()
    {
        int randomFirstEnemy = Random.Range(0, enemyFirstLine.Length);
        int randomSecondEnemy = Random.Range(0, enemySecondLine.Length);
        int randomThirdEnemy = Random.Range(0, enemyThirdLine.Length);

        if (enemyFirstLine[randomFirstEnemy].activeSelf)
        {
            StartCoroutine(SlowEnemyMovement(randomFirstEnemy));
        }
        else if (enemySecondLine[randomSecondEnemy].activeSelf)
        {
            StartCoroutine(SlowEnemyMovement(randomSecondEnemy));
        }
        else
        {
            StartCoroutine(SlowEnemyMovement(randomThirdEnemy));
        }
    }

    IEnumerator SlowEnemyMovement(int enemyNumber)
    {
        enemyFirstLine[enemyNumber].SetActive(true);
        yield return new WaitForSeconds(repeatRateSlow);
        enemyFirstLine[enemyNumber].SetActive(false);

        if (!IsDestroyed)
        {
            enemySecondLine[enemyNumber].SetActive(true);
            yield return new WaitForSeconds(repeatRateSlow);
            enemySecondLine[enemyNumber].SetActive(false);
        }

        if (!IsDestroyed)
        {
            enemyThirdLine[enemyNumber].SetActive(true);
            yield return new WaitForSeconds(repeatRateSlow);
            enemyThirdLine[enemyNumber].SetActive(false);
        }

        if (!IsDestroyed & !PlayerController.isGameOver)
        {
            EnemyIsMissing?.Invoke(); //from PlayerActing
            missedEnemy.Play();
            enemyFourthLine[enemyNumber].SetActive(true);
            yield return new WaitForSeconds(timeDelayInvisibleEnemy);
            enemyFourthLine[enemyNumber].SetActive(false);
        }

        yield return new WaitForSeconds(repeatRateSlow);
        EnemyCall();
    }

    private void EnemyCall()
    {
        if (!GameManager.Instance.isGameOver)
        {
            IsDestroyed = false;
            RandomEnemyMovement();
        }
    }
}
