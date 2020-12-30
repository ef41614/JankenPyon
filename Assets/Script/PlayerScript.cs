using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using say;　// 対象のスクリプトの情報を取得


public class PlayerScript : MonoBehaviour
{
    public float speed;
    [Header("進む（ジャンプする）回数")]
    public int MoveForward_StepNum = 3; // 進む（ジャンプする）回数
    public Animator anim = null;
    public SpriteRenderer MySprite;           // スプライト → order in layer の順番調整に使用する
    public int int_MySpriteOrder = 0;  // order in layer の順番調整に使用する整数
    public GameObject SelectJankenManager; //ヒエラルキー上のオブジェクト名
    SelectJanken SelectJankenMSC;//スクリプト名 + このページ上でのニックネーム
    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム
    public GameObject Text_StepNum;
    float span = 0.1f;
    private float currentTime = 0f; // test
    int FlyingTime_byTaihou = 10;   // 人間大砲で飛んでいる時間
    public ParticleSystem Taihou_Kemuri;
    public ParticleSystem Sunabokori;

    // Start is called before the first frame update
    void Start()
    {
        MySprite = gameObject.GetComponent<SpriteRenderer>();
        MySprite.sortingOrder = 1;              // int
        anim = GetComponent<Animator>();
        Debug.Log("進む（ジャンプする）回数 (MoveForward_StepNum) : " + MoveForward_StepNum);

        SelectJankenManager = GameObject.Find("SelectJankenManager");
        SelectJankenMSC = SelectJankenManager.GetComponent<SelectJanken>();

        ShuffleCardsManager = GameObject.Find("ShuffleCardsManager");
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();

        Text_StepNum = GameObject.Find("Text_StepNum");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            //Debug.Log("original_StepNum ：" + SelectJankenMSC.original_StepNum);
            //Debug.Log("進む（ジャンプする）回数 (MoveForward_StepNum) : " + MoveForward_StepNum);

            //Text_StepNum.GetComponent<Text>().text = SelectJankenMSC.original_StepNum + "：歩数";
            currentTime = 0f;
        }
    }

    public void SortMySpriteOrder()   // order in layer （画像表示順）の順番調整を実施する
    {
        MySprite.sortingOrder = int_MySpriteOrder;
    }

    public void JumpRight()       // 右方向へ 指定された回数 ぴょん と跳ねながら移動する
    {
        //SelectJankenMSC.Check_KageDistance();               //  MyKage と MyPlayer の距離を求める（Y軸の初期位置）       
        Debug.Log("進む（ジャンプする）回数 (MoveForward_StepNum) を 上書きします");
        Debug.Log("original_StepNum ：" + SelectJankenMSC.original_StepNum);
        MoveForward_StepNum = SelectJankenMSC.original_StepNum;
        Debug.Log("進む（ジャンプする）回数 (MoveForward_StepNum) : " + MoveForward_StepNum);
        if (MoveForward_StepNum > 0)
        {
            anim.SetBool("run", true);
            Debug.Log("Take Off！！");
            Debug.Log("ぴょーん！ ぴょーん！ ぴょーん！");
            //////////////////////////// 移動終了地点   // ジャンプする力  // ジャンプする回数   // アニメーション時間
            transform
                // .DOJump(new Vector3(MoveForward_StepNum * 1.0f, 0f), 0.5f, MoveForward_StepNum, MoveForward_StepNum * 1.0f)
                .DOJump(new Vector3(SelectJankenMSC.original_StepNum * 1.0f, 0f), 0.5f, SelectJankenMSC.original_StepNum, SelectJankenMSC.original_StepNum * 1.0f)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetRelative()
                .OnComplete(() =>
                {                  // ジャンプが終了したら、以下の操作をする
                    anim.SetBool("run", false);      // アニメーションを run → stand に遷移させる
                    Debug.Log("スタッ！！（着地音）");
                    Debug.Log("ジャンプ 今終わりました！");
                    SelectJankenMSC.MoveTo_MyKagePos();   // MyKage の位置へ移動する（Y軸位置微調整）
                    SelectJankenMSC.Judge_GOAL();         // ゴールラインに到達したか判定する
                    //SelectJankenMSC.Countdown_Push_OpenMyJankenPanel_Button_Flg = true;
                    SelectJankenMSC.Share_MyJankenPanel_Button_Flg_ON();  // ジャンケン開始ボタン フラグ をON
                    int RndGo_Tarai = UnityEngine.Random.Range(1, 8);
                    if (RndGo_Tarai <= 2)            // たらいが落ちるフラグON
                    {
                        SelectJankenMSC.Tarai_to_SetWFlag = true;  // たらいが落ちると、確定で白旗一枚     
                    }
                    else if (RndGo_Tarai >= 5)       // アイテムカードの裏面Up 一連の処理 実施するフラグON
                    {
                        SelectJankenMSC.Stream_Encounter_ItemCard_UraUp();  // アイテムカードの裏面Up 一連の処理
                    }
                    SelectJankenMSC.ShareAfterJump();     // 右にジャンプ（ぴょーん！）が完了してからの処理 ⇒ 全員に共有する
                    SelectJankenMSC.FallTarai_stream();          // たらいを落とす
                });
        }
        else
        {
            Debug.LogError("進む（ジャンプする）回数 (MoveForward_StepNum) が 0 です");
            SelectJankenMSC.MoveTo_MyKagePos();   // MyKage の位置へ移動する（Y軸位置微調整）
            SelectJankenMSC.ShareAfterJump();     // 右にジャンプ（ぴょーん！）が完了してからの処理 ⇒ 全員に共有する
        }
    }

    public void Fly_byTaihou()     // 大砲によってキャラが飛ぶ
    {
        int FlyingTime_byTaihou = UnityEngine.Random.Range(8, 15);
        Taihou_Kemuri.Play();      // 大砲発射後の煙エフェクト
        anim.SetBool("fly_taihou", true);         // アニメーションを stand → fly に遷移させる
        Debug.Log("Take Off！！");
        Debug.Log("ズドオォォォーーーーーーンン！！！！");
        //////////////////////////// 移動終了地点   // ジャンプする力  // ジャンプする回数   // アニメーション時間
        transform
            // .DOJump(new Vector3(MoveForward_StepNum * 1.0f, 0f), 0.5f, MoveForward_StepNum, MoveForward_StepNum * 1.0f)
            .DOJump(new Vector3(FlyingTime_byTaihou * 1.0f, 0f), 8f, 1, FlyingTime_byTaihou * 0.1f)
            .SetRelative()
            .SetEase(Ease.Linear)
            .SetRelative()
            .OnComplete(() =>
            {                                         // ジャンプが終了したら、以下の操作をする
                anim.SetBool("fly_taihou", false);         // アニメーションを fly → stand に遷移させる
                Debug.Log("ドガッ！！（落下音）");
                Debug.Log("人間大砲での吹っ飛び 今終わりました！");
                //SelectJankenMSC.MoveTo_MyKagePos();   // MyKage の位置へ移動する（Y軸位置微調整）
                //SelectJankenMSC.Judge_GOAL();         // ゴールラインに到達したか判定する
                SelectJankenMSC.AfterFly_byTaihou();         // ゴールラインに到達したか判定する
                Taihou_Kemuri.Stop();
                Sunabokori.Play();                    // 砂埃エフェクト
            });
    }

    public void receivedDammage() // ダメージを受ける
    {
        anim.SetBool("damage", true);

        transform
            .DOShakePosition(1.2f, 0.1f)          // キャラを揺らす
            .OnComplete(() => {                  // 上の処理が終了したら、以下の操作をする
                anim.SetBool("damage", false);   // アニメーションを 遷移させる （damage → stand）
            });
    }

    public void OnBtnSpeed100()
    {
        anim.SetFloat("Speed", 1.0f);
        anim.SetTrigger("P_run");
    }

    public void OnBtnSpeed200()
    {
        anim.SetFloat("Speed", 2.0f);
        anim.SetTrigger("P_run");
    }
}