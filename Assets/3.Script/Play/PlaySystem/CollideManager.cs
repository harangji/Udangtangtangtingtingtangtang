using System;
using System.Collections.Generic;
using UnityEngine;

public class CollideManager : SingletonBase<CollideManager>
{
    public List<CharacterBase> Collidables { get; set; }

    private void Update()
    {
        for (int i = 0; i < Collidables.Count; i++)
        {
            for (int j = i + 1; j < Collidables.Count; j++)
            {
                float mDistance = Vector2.Distance(Collidables[i].transform.position, Collidables[j].transform.position);
            }
        }
    }

    public void CollideObserve()
    {
    }
}
