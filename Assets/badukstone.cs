using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badukstone : MonoBehaviour
{
    public bool Vzero;
    public float hp,maxhp;
    public float damage;
    public float sometime;

    // Update is called once per frame
    protected virtual void Update()
    {


        if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            Vzero = false;
        }
        if (!Vzero)
        {
            if (sometime <= 0.01f)
            {
                sometime += Time.deltaTime;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, Time.deltaTime * 10);
                if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    Vzero = true;
                    sometime = 0;
                }
            }

        }
    
    
    
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            hp = 0;
            if(this.CompareTag("Enemy"))
            GameManager.GameManagerthis.Enemy.Remove(this.gameObject);

        }




    }




}
