using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyone : Enemy ,ISkill,ITargetOn
{



    private void Start()
    {
        TargetOn();
        drag =  GetComponent<Rigidbody2D>().drag;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (GameManager.GameManagerthis.Turn==turn.ªÛ¥Î≈œ)
        {
            Skill();
        }
        else
        {
            GetComponent<Rigidbody2D>().drag = drag * (hp == 0 ? 0.1f : (hp / maxhp));
        }

    }

    public void TargetOn()
    {
        target = GameManager.GameManagerthis.player.transform.position;
        Vector3 targetpos = target - this.transform.position;
        transform.Find("Arrow").gameObject.SetActive(true);
        transform.Find("Arrow").rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(targetpos.normalized.y, targetpos.normalized.x) * Mathf.Rad2Deg);
    }

    public void Skill()
    {
        transform.Find("Arrow").gameObject.SetActive(false);
        if (attack)
        {
       
            GetComponent<Rigidbody2D>().drag = drag;
            GetComponent<Rigidbody2D>().AddForce((target - transform.position).normalized * 5000f);
            attack = false;
        }
    }
}
