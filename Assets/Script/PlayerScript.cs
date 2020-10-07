using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    [Header("進む（ジャンプする）回数")]
    public int MoveForward_StepNum = 3; // 進む（ジャンプする）回数
    private Animator anim = null;
    public SpriteRenderer MySprite;           // スプライト → order in layer の順番調整に使用する
    public int int_MySpriteOrder = 0;  // order in layer の順番調整に使用する整数
    public GameObject SelectJankenManager; //ヒエラルキー上のオブジェクト名
    SelectJanken SelectJankenMSC;//スクリプト名 + このページ上でのニックネーム
    public GameObject Text_StepNum;
    float span = 0.1f;
    private float currentTime = 0f; // test

    // Start is called before the first frame update
    void Start()
    {
        MySprite = gameObject.GetComponent<SpriteRenderer>();
        MySprite.sortingOrder = 1;              // int
        anim = GetComponent<Animator>();
        Debug.Log("進む（ジャンプする）回数 (MoveForward_StepNum) : " + MoveForward_StepNum);

        SelectJankenManager = GameObject.Find("SelectJankenManager");
        //SelectJankenManager = GameObject.FindWithTag("SelectJankenManager");
        SelectJankenMSC = SelectJankenManager.GetComponent<SelectJanken>();
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
        SelectJankenMSC.Check_KageDistance();               //  MyKage と MyPlayer の距離を求める（Y軸の初期位置）
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
                    SelectJankenMSC.ShareAfterJump();     // 右にジャンプ（ぴょーん！）が完了してからの処理 ⇒ 全員に共有する
                });
        }
        else
        {
            Debug.LogError("進む（ジャンプする）回数 (MoveForward_StepNum) が 0 です");
            SelectJankenMSC.MoveTo_MyKagePos();   // MyKage の位置へ移動する（Y軸位置微調整）
            SelectJankenMSC.ShareAfterJump();     // 右にジャンプ（ぴょーん！）が完了してからの処理 ⇒ 全員に共有する
        }
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