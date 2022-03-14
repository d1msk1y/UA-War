using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private GunConfig _gunConfig;
    [SerializeField] private Transform _firePos;
    public GameObject handler;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Shoot(Vector3 aimPos)
    {
        StartCoroutine(ShotFX(aimPos));
    }

    private IEnumerator ShotFX(Vector3 linePos)
    {
        _lineRenderer.SetPosition(0, _firePos.position);
        _lineRenderer.SetPosition(1, linePos);
        yield return new WaitForSeconds(0.025f);
        _lineRenderer.SetPosition(1, _firePos.position);
        yield return null;
    }

}
