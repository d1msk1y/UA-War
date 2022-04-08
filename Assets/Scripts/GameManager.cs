using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public AstarPath Astar;
    public BattleManager battleManager;
    public BuildingSystem buildingSystem;
    public ScoreSystem scoreSystem;
    public ShopManager shopManager;
    public UpgradeSystem upgradeSystem;

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
