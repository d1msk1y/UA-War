using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseController : MonoBehaviour
{
    [Header("References")]
    public Actor character;
    public Gun gun;
    public LayerMask targetMask;

    public static BaseController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gun.targetMask = targetMask;
    }

    private void Update()
    {
        Aiming();
        ShootingCheck();
    }

    private void ShootingCheck()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (GameManager.Instance.buildingSystem.SelectedObject != null || EventSystem.current.IsPointerOverGameObject())
                return;
            if (Input.GetKeyDown(KeyCode.Mouse0))
                CheckAmmo();
            gun.Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void CheckAmmo()
    {
        if (gun.Ammos <= 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameManager.Instance.soundManager.PlaySoundEvent(gun.gunConfig.noAmmo);
            GameManager.Instance.uiManager.SetNewGUIText(GameManager.Instance.uiManager._gunTxt, mousePos, "RELOAING!");
        }
    }

    private void Aiming()
    {
        Vector3 aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = aimPos - character.transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
        character.body.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private float BodyAiming(float angle)
    {
        float calculatedAngle = Mathf.Lerp(character.body.transform.rotation.z, angle, 0.1f);
        return calculatedAngle;
    }
}