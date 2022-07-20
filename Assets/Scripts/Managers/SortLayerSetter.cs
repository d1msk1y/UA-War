using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SortLayerSetter : MonoBehaviour
{
    public List<SpriteRenderer> entities;

    private void Start() => GameManager.Instance.OnGridChange += UpdateLayers;

    private void UpdateLayers()
    {
        entities = entities.OrderBy(x => x.transform.position.y).ToList();
        for (int i = 0; i < entities.Count(); i++)
        {
            entities[i].sortingOrder = 0;
            entities[i].sortingOrder -= i;
        }
    }
}