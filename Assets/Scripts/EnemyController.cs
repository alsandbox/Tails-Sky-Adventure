using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //counting is done from the top
    public GameObject[] enemyFirstLine;
    public GameObject[] enemySecondLine;
    public GameObject[] enemyThirdLine;
    public GameObject[] enemyFourthLine;

    private float repeatRateSlow = 0.6f;
    private float timeDelayInvisibleEnemy = 0.4f;

    private void Start()
    {
        EnemyCall();
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

        if (!GameManager.Instance.enemyDestroyed)
        {
            enemySecondLine[enemyNumber].SetActive(true);
            yield return new WaitForSeconds(repeatRateSlow);
            enemySecondLine[enemyNumber].SetActive(false);
        }

        if (!GameManager.Instance.enemyDestroyed)
        {
            enemyThirdLine[enemyNumber].SetActive(true);
            yield return new WaitForSeconds(repeatRateSlow);
            GameManager.Instance.PlayerMissesEnemy(enemyNumber);
            enemyThirdLine[enemyNumber].SetActive(false);
        }

        if (!GameManager.Instance.enemyDestroyed & !GameManager.Instance.isGameOver)
        {
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
            GameManager.Instance.enemyDestroyed = false;
            RandomEnemyMovement();
        }
    }
}
