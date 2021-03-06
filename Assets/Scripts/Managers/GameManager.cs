using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public ShopManager shopManager;
    public SortLayerSetter sortLayerSetter;
    public UiManager uiManager;
    public AudioManager soundManager;
    public ScoreSystem scoreSystem;
    public BuildingSystem buildingSystem;
    public UpgradeSystem upgradeSystem;
    public CameraShaker cameraShaker;
    public AstarPath Astar;

    public static GameManager Instance;

    public delegate void AstarHanler();
    public event AstarHanler OnGridChange;

    private void Awake()
    {
        Instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShakeScreen(float power)
    {
        cameraShaker.ShakeOnce(power, 10f, 0.1f, 0.3F);
    }

    public void ScanAStar()
    {
        Astar.Scan();
        OnGridChange?.Invoke();
    }
}
