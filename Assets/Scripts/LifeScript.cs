using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeScript : MonoBehaviour {
    public GameObject unitychan; //ユニティちゃん
    //public GameObject explosion; //爆発アニメーション
    public Text gameoverText; //ゲームオーバーの文字
    public bool gameover = false; //ゲームオーバー判定
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        //ライフが0以下になった時、
        if (rt.sizeDelta.y <= 0)
        {
            //ゲームオーバー判定がfalseなら爆発アニメーションを生成
            //GameOverメソッドでtrueになるので、1回のみ実行
            if (gameover == false)
            {
                //Instantiate(explosion, unitychan.transform.position + new Vector3(0, 1, 0), unityChan.transform.rotation);
            }
            //ゲームオーバー判定をtrueにし、ユニティちゃんを消去
            Gameover();
        }
        //ゲームオーバー判定がtrueの時、
        if (gameover)
        {
            //ゲームオーバーの文字を表示
            gameoverText.enabled = true;
            //画面をクリックすると
            if (Input.GetMouseButtonDown(0))
            {
                //タイトルへ戻る
                Application.LoadLevel("Title");
            }
        }
    }

    public void LifeDown(int ap)
    {
        rt.sizeDelta -= new Vector2(0, ap);
    }

    public void LifeUp(int hp)
    {
        rt.sizeDelta += new Vector2(0, hp);

        if (rt.sizeDelta.y > 240f)
        {
            rt.sizeDelta = new Vector2(51f, 240f);
        }
    }
    public void Gameover()
    {
        gameover = true;
        Destroy(unitychan);
    }
}