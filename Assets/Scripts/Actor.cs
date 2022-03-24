using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public Gun gun;
    public GameObject body;
    public GameObject deadBody;
    public float deadTime;

    private IEnumerator DieCoroutine()
    {
        gameObject.SetActive(false);
        GameObject deadObj =
        Instantiate(deadBody, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        yield return new WaitForSeconds(deadTime);
        Destroy(deadObj);
        Destroy(gameObject);
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }
}
