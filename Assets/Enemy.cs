using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : badukstone
{
    public bool attack;
    public Vector3 target;
    public float drag;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(hp<=0)
        {
           // GameManager.GameManagerthis.Enemy.Remove(this.gameObject);
            GameManager.GameManagerthis.LevelGet(5);
            Destroy(this.gameObject);
        }



    }

}
