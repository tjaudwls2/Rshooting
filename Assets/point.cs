using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
       if((GameManager.GameManagerthis.player.transform.position - this.transform.position).magnitude < 5)
        {
            this.transform.Translate((GameManager.GameManagerthis.player.transform.position - this.transform.position).normalized * Time.deltaTime*20);


        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.GameManagerthis.LevelGet(1);

            Destroy(this.gameObject);
        }
    }


}
