using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : badukstone
{
    public float drag;
    private void Start()
    {
        drag = GetComponent<Rigidbody2D>().drag;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (GameManager.GameManagerthis.Turn == turn.상대턴)
        {
            GetComponent<Rigidbody2D>().drag = drag * (hp==0 ? 0.1f:(hp / maxhp));

        }
        else
        {
            
                GetComponent<Rigidbody2D>().drag = drag;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GameManager.GameManagerthis.Turn == turn.상대턴)
            {
                hp -= collision.gameObject.GetComponent<Enemy>().damage;
            }
            else if (GameManager.GameManagerthis.Turn == turn.내공격턴)
            {
                collision.gameObject.GetComponent<Enemy>().hp-= damage;
            }



        }




    }


}

