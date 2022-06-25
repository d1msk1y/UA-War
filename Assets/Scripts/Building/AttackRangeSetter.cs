using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeSetter : MonoBehaviour
{
    [SerializeField] private Color _rangeCollor;
    [SerializeField] private SpriteRenderer _rangeRenderer;

    private void Start()
    {
        _rangeRenderer.color = _rangeCollor;
    }

    public void DisplayRange(float attackRange)
    {
        _rangeRenderer.gameObject.SetActive(true);
        _rangeRenderer.transform.localScale = new Vector2(attackRange * 2, attackRange * 2);
    }

    public void HideRange() => _rangeRenderer.gameObject.SetActive(false);
}