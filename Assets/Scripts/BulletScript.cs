﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    private GameObject player;
    private int speed = 12;
    // Use this for initialization
    void Start () {
        //ユニティちゃんオブジェクトを取得
        player = GameObject.FindWithTag("UnityChan");
        //rigidbody2Dコンポーネントを取得
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        //ユニティちゃんの向いている向きに弾を飛ばす
        rigidbody2D.velocity = new Vector2(speed * player.transform.localScale.x, rigidbody2D.velocity.y);
        //画像の向きをユニティちゃんに合わせる
        Vector2 temp = transform.localScale;
        temp.x = player.transform.localScale.x;
        transform.localScale = temp;
        //5秒後に消滅
        Destroy(gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        string Groundc = LayerMask.LayerToName(col.gameObject.layer);
        string Wallc = LayerMask.LayerToName(col.gameObject.layer);
        if (col.gameObject.tag == "Enemy" || Groundc == "Ground" || Wallc == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
