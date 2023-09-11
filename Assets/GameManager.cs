using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public enum turn
{
    내턴,
    내공격턴,
    상대턴

}


public class GameManager : MonoBehaviour
{
    public bool dragOn;
    public GameObject player;
    GameObject Arrow;
    Vector2 startpos, endpos;
    public turn Turn = turn.내턴;
    public int Maxpoa = 2;
    public int poa = 0; //power of action 행동력
    public float sometime; // 각턴마다의 텀
    float touchTime;
    public List<GameObject> Enemy;
    public int wave=1;

    public ItemSo ItemSo;

    public static GameManager gameManager = null;
    public static GameManager GameManagerthis
    {
        get
        {
            if (null == gameManager)
            {
                return null;
            }
            return gameManager;
        }


    }
    private void Awake()
    {
        if (null == gameManager)
        {

            gameManager = this;


        }
        else
        {

            Destroy(this.gameObject);
        }
        Arrow = player.transform.Find("Arrow").gameObject;
        SpawnTime = 2;
        Spawn();

 

    }
    public float SpawnTime;

    [System.Serializable]
    public class EnemySpawn
    {
        public GameObject Enemy;
        public int EnemyCount;
        public bool thisboss;

    }
    public EnemySpawn[] EnemySpawns;
    public int SpawnCount;
    public bool bossSpawn;

    public int Level;//레벨
    public float currentLevel, levelMax; // 현재경험치,맥스경험치
    public GameObject levelPoint;
    public int levelPointCount;

    public Image hpgage, lvgage;
    public TextMeshProUGUI lvtext, wavetext;

    public GameObject LevelUpUI;


    // Update is called once per frame
    void Update()
    {
        turnManager();
        SliderManager();

    }

    public void SliderManager()
    {
        hpgage.fillAmount = player.GetComponent<badukstone>().hp / player.GetComponent<badukstone>().maxhp;
        lvgage.fillAmount = currentLevel/levelMax;
        lvtext.text = "Lv"+Level.ToString();
        wavetext.text = "Wave"+wave.ToString();
    }

    bool uitouch;
    public void turnManager()
    {
        if (Turn == turn.내턴)
        {
            if (Input.touchCount == 1)
            {
                touchTime += Time.deltaTime;

           
                    Touch touchZero = Input.GetTouch(0);


          
                

                if (touchZero.phase == TouchPhase.Began)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        Arrow.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        startpos = touchZero.position;
                        endpos = touchZero.position;
                        uitouch = false;
                    }
                    else
                    {
                        uitouch = true;
                    }
               
                }

                if (!uitouch)
                {
                    if (touchZero.phase == TouchPhase.Moved)
                    {
                        endpos = touchZero.position;

                    }
                    if (touchTime > 0.2f)
                    {

                        dragOn = true;
                        Vector3 touchZeroPrevPos = (endpos - startpos) * 0.8f;
                        float touchZeroPrevPosPower = Mathf.Clamp(touchZeroPrevPos.magnitude, 1, 1000) * 0.003f;

                        Arrow.SetActive(true);
                        Arrow.transform.localScale = new Vector3((Mathf.Lerp(0.5f, 2, touchZeroPrevPosPower)), 0.5f, 0.5f);
                        Arrow.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(-touchZeroPrevPos.normalized.y, -touchZeroPrevPos.normalized.x) * Mathf.Rad2Deg);




                        if (touchZero.phase == TouchPhase.Ended)
                        {
                            player.GetComponent<Rigidbody2D>().AddForce(-touchZeroPrevPos.normalized * Arrow.transform.localScale.x * 2500f);
                            Arrow.SetActive(false);
                            Arrow.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            dragOn = false;
                            poa++;
                            touchTime = 0;
                            Turn = turn.내공격턴;

                        }


                    }
                }
            }
        }
        else if (Turn == turn.내공격턴)
        {
            sometime += Time.deltaTime;
            EnemyVzero();
            if (sometime >= 1f)
            {
                if (player.GetComponent<Player>().Vzero && EnemyVzero())
                {

                    Camera.main.GetComponent<PinchZoom>().turnOn = true;
                    sometime = 0;
                    nextTurn();
                }
            }

        }
        else if (Turn == turn.상대턴)
        {
            sometime += Time.deltaTime;
            EnemyVzero();
            if (sometime >= 1f)
            {
                if (player.GetComponent<Player>().Vzero && EnemyVzero())
                {
                    foreach (GameObject enemy in Enemy)
                    {

                        enemy.GetComponent<ITargetOn>().TargetOn();


                    }
                    Camera.main.GetComponent<PinchZoom>().turnOn = true;
                    Turn = turn.내턴;
                    if(!bossSpawn)
                    Spawn();
           
                    sometime = 0;
                }
            }



        }
    }

    public bool EnemyVzero()
    {
        foreach (GameObject enemy in Enemy)
        {
            if (!enemy.GetComponent<Enemy>().Vzero)
                return false;

        }
        return true;
    }

    public void nextTurn()
    {


        if (poa == Maxpoa)
        {



            Turn = turn.상대턴;
            poa = 0;
            foreach (GameObject enemy in Enemy)
            {
                enemy.GetComponent<Enemy>().attack = true;
            }


        }
        else
        {

            Camera.main.GetComponent<PinchZoom>().turnOn = true;
            Turn = turn.내턴;
            sometime = 0;


        }


    }
 
    public void Spawn()
    {
        SpawnTime++;
        if (EnemySpawns.Length > SpawnCount)
        {
            if (SpawnTime == 3||Enemy.Count==0)
            {
                for (int i = 0; i < EnemySpawns[SpawnCount].EnemyCount; i++)
                {
                    Vector2 pos = new Vector2(Random.Range(-7.2f, 7.21f), Random.Range(-14f, 14.1f));
                    while (Vector2.Distance(player.transform.position, pos) < 4)
                    {
                        pos = new Vector2(Random.Range(-7.2f, 7.21f), Random.Range(-14f, 14.1f));
                    }

                    GameObject Enemys = Instantiate(EnemySpawns[SpawnCount].Enemy, pos, this.transform.rotation);
                    Enemy.Add(Enemys);

                }
                LevelSpawn();
                bossSpawn = EnemySpawns[SpawnCount].thisboss;
                SpawnCount++;
                SpawnTime = 0;
                wave++;
            }
        }
    }

    public void LevelSpawn()
    {
        for (int i = 0; i < levelPointCount; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-7.2f, 7.21f), Random.Range(-14f, 14.1f));
            Instantiate(levelPoint, pos, this.transform.rotation);
        }
    }

    public void LevelGet(int plus)
    {
        currentLevel+=plus;

        if (currentLevel > levelMax)
        {
            currentLevel -= levelMax;
            Level++;
            LevelUp();
        }

    }

    public void LevelUp()
    {
        LevelUpUI.SetActive(true);
        LevelUpUI.GetComponent<Image>().DOFade(0.8f, 1);
        DOTween.Sequence().OnStart(() =>
        {
            LevelUpUI.transform.GetChild(0).gameObject.GetComponent<RectTransform>().DOAnchorPosY(0,1);


        });


    }



}
