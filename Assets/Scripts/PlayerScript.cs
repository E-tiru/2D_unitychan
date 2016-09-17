using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed = 4f;
    public float x;
    public float camr;
    public float caml;
    public float jumpPower = 5;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float JumpFlag = 0;
    public const float Jump_Max = 2;

    public GameObject bullet;
    public GameObject zangeki;
    public GameObject mainCamera;
    private Rigidbody2D rigidbody2D;
    private Animator anim;

    private bool isGrounded;
    private bool isWall;
    private bool Wallleft;
    private bool Wallright;

    public LifeScript lifeScript;

    private Renderer renderer;
    void Start () {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
	}

    void Update(){
       
        jump();
        move();
        wall();
        slash();
        shot();
    }
    void wall(){
        //Linecastでユニティちゃんが壁に接触しているか判定
        isWall = Physics2D.Linecast(
        transform.position + transform.right * 0.6f,
        transform.position - transform.right * 0.6f,
        wallLayer);
        Wallright = Physics2D.Linecast(
        transform.position,
        transform.position + transform.right * 0.6f,
        wallLayer);
        Wallleft = Physics2D.Linecast(
        transform.position,
        transform.position - transform.right * 0.6f,
        wallLayer);

        //スペースキーを押したとき
        if(Input.GetKeyDown("space")&&isWall == true)
        {
            
            //Jumpアニメーションを止めて、Stickアニメーションを実行
            anim.SetBool("Jump", false);
            anim.SetTrigger("Jump");
            //anim.SetTrigger("Stick");
            //velocityにて上方向へ力を加える
            float x = Input.GetAxisRaw("Horizontal");
            if(Wallleft) rigidbody2D.velocity = new Vector2(5 * speed, jumpPower);
            if(Wallright) rigidbody2D.velocity = new Vector2(-5 * speed, jumpPower);
        }
    }
    void jump(){
        //Linecastでユニティちゃんの足元に地面があるか判定
        isGrounded = Physics2D.Linecast(
        transform.position,
        transform.position - transform.up * 0.05f,
        groundLayer);
        if (isGrounded)
        {
            JumpFlag = 0;
        }
        //スペースキーを押した時
        if (Input.GetKeyDown("space") && Jump_Max != JumpFlag)
        {
            //Dashアニメーションを止めて、Jumpアニメーションを実行
            anim.SetBool("Dash", false);
            anim.SetTrigger("Jump");
            //"AddForce",velocityにて上方向へ力を加える
            rigidbody2D.velocity = new Vector2(0, jumpPower);
            //着地しているかどうか
            if (isGrounded)
            {
                isGrounded = false;
                JumpFlag++;
            }
            else
            {
                JumpFlag = 2;
            }
        }
        //上下への移動速度を取得
        float velY = rigidbody2D.velocity.y;
        //移動速度が0.1より大きければ上昇
        bool isJumping = velY > 0.1f ? true : false;
        //移動速度が-0.1より小さければ下降
        bool isFalling = velY < -0.1f ? true : false;
        //結果をアニメータービューの変数へ反映する
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
    }
    void shot()
    {
        if (Input.GetKeyDown("c"))
        {
            Instantiate(bullet, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
        }
    }

    void slash()
    {
        if (Input.GetKeyDown("x"))
        {
            float r = transform.localScale.x;
            if(r > 0)Instantiate(zangeki, transform.position + new Vector3(r * 1.2f, 0.7f, 0f), transform.rotation);
            if(r < 0)Instantiate(zangeki, transform.position + new Vector3(r * 1.2f, 0.7f, 0f), transform.rotation);
        }
    }
    void move()
    {
        x = Input.GetAxisRaw("Horizontal");
        if (x != 0)
        {
            rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
            Vector2 temp = transform.localScale;
            temp.x = x;
            transform.localScale = temp;
            anim.SetBool("Dash", true);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            anim.SetBool("Dash", false);
        }
        Vector3 cameraPos = mainCamera.transform.position;

        //画面中央から左に4移動した位置をユニティちゃんが超えたら
        if (transform.position.x > mainCamera.transform.position.x + 2)
        {
            //カメラの位置を取得
            //ユニティちゃんの位置から右に4移動した位置を画面中央にする
            if (cameraPos.x < camr)
            {
                cameraPos.x = transform.position.x - 2;
                mainCamera.transform.position = cameraPos;
            }
        }
        //画面中央から右に4移動した位置をユニティちゃんが超えたら
        if (transform.position.x < mainCamera.transform.position.x - 2)
        {
            //カメラの位置を取得
            //ユニティちゃんの位置から左に4移動した位置を画面中央にする
            if (cameraPos.x > caml)
            {
                cameraPos.x = transform.position.x + 2;
                mainCamera.transform.position = cameraPos;
            }
        }
        //画面中央から上に4移動した位置をユニティちゃんが超えたら
        if (transform.position.y > mainCamera.transform.position.y + 2)
        {
            //カメラの位置を取得
            //ユニティちゃんの位置から右に4移動した位置を画面中央にする
            cameraPos.y = transform.position.y - 2;
            mainCamera.transform.position = cameraPos;
        }
        //画面中央から左に4移動した位置をユニティちゃんが超えたら
        if (transform.position.y < mainCamera.transform.position.y - 2)
        {
            //カメラの位置を取得
            //ユニティちゃんの位置から右に4移動した位置を画面中央にする
            if (cameraPos.y > 2)
            {
                cameraPos.y = transform.position.y + 2;
                mainCamera.transform.position = cameraPos;
            }
        }
        
        //カメラ表示領域の左下をワールド座標に変換
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        //カメラ表示領域の右上をワールド座標に変換
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //ユニティちゃんのポジションを取得
        Vector2 pos = transform.position;
        //ユニティちゃんのx座標の移動範囲をClampメソッドで制限
        pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
        transform.position = pos;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //穴に落ちた時の処理
        if(col.gameObject.tag == "Hole")//LifeScriptのGameoverメソッドを実行する
        lifeScript.Gameover();
        //Enemyとぶつかった時にコルーチンを実行
        if (col.gameObject.tag == "Enemy")
        {

            int count = 10;
            while (count > 0)
            {
                // プレイヤーの位置を後ろに飛ばす
                float s = 10f * Time.deltaTime;
                transform.Translate(Vector3.up * 0.01f);

                // プレイヤーのlocalScaleでどちらを向いているのかを判定
                if (transform.localScale.x >= 0)
                {
                    transform.Translate(Vector3.left * 0.02f);
                }
                else
                {
                    transform.Translate(Vector3.right * 0.02f);
                }
                count--;
            }
            StartCoroutine("Damage");
        }
    }

    IEnumerator Damage()
    {
        //レイヤーをPlayerDamageに変更
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        //while文を10回ループ
        int count = 10;
        while (count > 0)
        {
            //透明にする
            renderer.material.color = new Color(1, 1, 1, 0);
            //0.05秒待つ
            yield return new WaitForSeconds(0.05f);
            //元に戻す
            renderer.material.color = new Color(1, 1, 1, 1);
            //0.05秒待つ
            yield return new WaitForSeconds(0.05f);
            count--;
        }
        //レイヤーをPlayerに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}