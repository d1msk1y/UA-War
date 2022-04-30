using UnityEngine;
using System.Collections;

public class GunFxSetter : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private GameObject shotFX;

    private Gun gun;

    private void Awake()
    {
        gun = GetComponent<Gun>();
        SetGunShotFx();
    }

    private void SetGunShotFx()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        SetShotImg();
    }

    public IEnumerator ShotFX(Vector3 linePos)
    {
        _lineRenderer.SetPosition(0, gun.firePos.position);
        _lineRenderer.SetPosition(1, linePos);

        shotFX.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.025f);

        shotFX.gameObject.SetActive(false);

        _lineRenderer.SetPosition(1, gun.firePos.position);
        yield return null;
    }

    private void SetShotImg()
    {
        shotFX = new GameObject();
        shotFX.AddComponent<SpriteRenderer>().sprite = gun.gunConfig.shotGFX;
        shotFX.GetComponent<SpriteRenderer>().sortingLayerName = "VFX";
        shotFX.transform.position = gun.firePos.position;
        shotFX.transform.rotation = Quaternion.Euler(0, 0, 90);
        shotFX.transform.parent = gun.firePos;
        shotFX.transform.localScale = new Vector3(1, 1, 1);
    }
}