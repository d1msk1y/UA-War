using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("Parameters")]
    public int price;
    public int score;

    [Header("Base References")]
    public Gun gun;
    public GameObject body;
    public GameObject deadBody;

    private IEnumerator DieCoroutine()
    {
        gameObject.SetActive(false);
        GameObject deadObj =
        Instantiate(deadBody, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        yield return new WaitForSeconds(30);
        Destroy(deadObj);
        Destroy(gameObject);
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
        GetComponent<EntityHealth>().onDieEvent += DropScore;
        GameManager.Instance.battleManager.currentEnemiesInAction.Remove(transform);
    }

    private void DropScore()
    {
        GameManager.Instance.scoreSystem.AddCoins(price);
        GameManager.Instance.scoreSystem.AddScore(score);
    }
}
