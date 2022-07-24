using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpriteSetter : MonoBehaviour
{
    private Entity _entity;

    private void Start() => _entity = GetComponent<Entity>();

    private void CheckRotation()
    {
        for (int i = 0; i < _entity.BuildingParams.angles.Length; i++)
        {
            if (transform.rotation.z == _entity.BuildingParams.angles[i])
            {
                if (i >= _entity.BuildingParams.sprites.Length)
                    i = _entity.BuildingParams.sprites.Rank;
                _entity.SpriteRenderer.sprite = _entity.BuildingParams.sprites[i];
            }
        }
    }
}
