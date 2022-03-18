using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public AstarPath Astar;
    public BattleManager battleManager;

    public static GameManager instance;

    public void ScanAStar()
    {
        Astar.Scan();
    }

}
