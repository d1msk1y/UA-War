using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public int price;
    public int score;
    public float radius;
    public LayerMask targetMask;
    public float escapeSpeed;
}