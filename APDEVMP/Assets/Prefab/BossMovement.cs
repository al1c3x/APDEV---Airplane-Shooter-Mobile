using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject GameManagerGO;
    public GameObject playerShip;
    GameObject scoreUITextGO;
    public float speed = 2.0f;
    public GameObject ExplosionGO;
    private int hp = 2000;
    // Start is called before the first frame update
    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = transform.position;
        pos = new Vector2(pos.x, pos.y * 3/4);

        transform.position = pos;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        
      

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Collider2D bosscol = this.GetComponent<Collider2D>();
        if (col.tag == "PlayerShipTag")
        {
            PlayExplosion();
            hp -= 200;


        }
       
        else if ((col.tag == "PlayerBulletTag" && bosscol.tag == "Boss1")|| (col.tag == "GreenBullet" && bosscol.tag == "Boss2")|| (col.tag == "RedBullet" && bosscol.tag == "Boss3"))
        {
            PlayExplosion();
            hp -= playerShip.GetComponent<PlayerControl>().playerDamage() * 2;
        }
        else if ((col.tag == "PlayerBulletTag" && bosscol.tag == "Boss2") || (col.tag == "GreenBullet" && bosscol.tag == "Boss3") || (col.tag == "RedBullet" && bosscol.tag == "Boss1"))
        {
            PlayExplosion();
            hp -= playerShip.GetComponent<PlayerControl>().playerDamage();
        }
        else if ((col.tag == "PlayerBulletTag" && bosscol.tag == "Boss3") || (col.tag == "GreenBullet" && bosscol.tag == "Boss1") || (col.tag == "RedBullet" && bosscol.tag == "Boss2"))
        {
            PlayExplosion();
            hp -= playerShip.GetComponent<PlayerControl>().playerDamage() /2;
        }
        else if(hp == 0)
        {
            PlayExplosion();
            scoreUITextGO.GetComponent<GameScore>().Score += 1000;
            Destroy(gameObject);
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
