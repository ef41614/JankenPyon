using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    [Header("進む（ジャンプする）回数")] public int MoveForward_StepNum = 3; // 進む（ジャンプする）回数
    private Animator anim = null;
    public SpriteRenderer MySprite;           // スプライト → order in layer の順番調整に使用する
    public int int_MySpriteOrder = 0;  // order in layer の順番調整に使用する整数

    // Start is called before the first frame update
    void Start()
    {
        MySprite = gameObject.GetComponent<SpriteRenderer>();
        MySprite.sortingOrder = 1;              // int
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SortMySpriteOrder()   // order in layer （画像表示順）の順番調整を実施する
    {
        MySprite.sortingOrder = int_MySpriteOrder;
    }

    public void JumpRight()       // 右方向へ 指定された回数 ぴょん と跳ねながら移動する
    {
        anim.SetBool("run", true);
        //////////////////////////// 移動終了地点   // ジャンプする力  // ジャンプする回数   // アニメーション時間
        transform
            .DOJump(new Vector3(MoveForward_StepNum * 1.0f, 0f), 0.5f, MoveForward_StepNum, MoveForward_StepNum * 1.0f)
            .SetRelative()
            .SetEase(Ease.Linear)
            .SetRelative()
            .OnComplete(() => {                  // ジャンプが終了したら、以下の操作をする
                anim.SetBool("run", false);      // アニメーションを run → stand に遷移させる
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