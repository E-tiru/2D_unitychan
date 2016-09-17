using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {
    Rigidbody2D rigidbody2D;
    public int speed = -3;

    //public GameObject explosion;
    public int loop;
    public int EnemyHp;
    public int attackPoint = 10;
    public LifeScript lifeScript;

    //メインカメラのタグ名　constは定数(絶対に変わらない値)
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    //カメラに映っているかの判定
    private bool _isRendered = false;
    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (_isRendered)
        {
            if (loop < 120)
            {
                rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
                loop++;
            }
            else
            {
                speed = -1 * speed;
                loop = 0;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (_isRendered)
        {
            if (col.tag == "Bullet") EnemyHp -= 1;
            if (col.tag == "Zangeki") EnemyHp -= 3;
                if (EnemyHp < 1)
                {
                    Destroy(gameObject);
                    //やられアニメーション
                    //Instantiate(explosion, transform.position, transform.rotation);
                    //if (Random.Range(0, 4) == 0)
                    //{
                    //    Instantiate(item, transform.position, transform.rotation);
                    //}
                }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //UnityChanとぶつかった時
        if (col.gameObject.tag == "UnityChan")
        {
            //LifeScriptのLifeDownメソッドを実行
            lifeScript.LifeDown(attackPoint);
        }
    }
    //Rendererがカメラに映ってる間に呼ばれ続ける
    void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedをtrue
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            _isRendered = true;
        }
        else
        {
            _isRendered = false;
        }
    }
}
