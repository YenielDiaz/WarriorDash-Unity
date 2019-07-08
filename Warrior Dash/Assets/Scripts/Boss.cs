using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] Level level;

    protected override void Start()
    {
        base.Start();
        level = FindObjectOfType<Level>();
    }

    //destroy gameobject and trigger death vfx,sfx
    protected override void Die()
    {
        base.Die();
        level.LoadNextLevel();
    }
}
