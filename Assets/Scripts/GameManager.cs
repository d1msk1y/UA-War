using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public ShopManager shopManager;
    public UiManager uiManager;
    public SoundManager soundManager;
    public ScoreSystem scoreSystem;
    public BuildingSystem buildingSystem;
    public UpgradeSystem upgradeSystem;
    public AstarPath Astar;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ScanAStar()
    {
        Astar.Scan();
    }
}
