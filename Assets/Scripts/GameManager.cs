using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public AstarPath Astar;
    public BattleManager battleManager;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ScanAStar()
    {
        Astar.Scan();
    }
}