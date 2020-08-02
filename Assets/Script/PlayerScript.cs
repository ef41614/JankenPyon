using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public int MoveForwardNum; // 進む（ジャンプする）回数
    private Animator anim = null;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a"))
        {
            anim.SetBool("run", true);
            //JumpRight();
        }
        else
        {
            //anim.SetBool("run", false);
        }
    }

    public void JumpRight()
    {
        anim.SetBool("run", true);
        /*   transform.DOLocalMove(new Vector2(1, 0f), 0.5f)
               .SetRelative()
               .OnComplete(() => {
                         anim.SetBool("run", false);
                     });
                     */

       // OnBtnSpeed200();
        // 移動終了地点  // ジャンプする力  // ジャンプする回数   // アニメーション時間
        transform.DOJump(new Vector3(MoveForwardNum*1.0f, 0f), 0.5f, MoveForwardNum, MoveForwardNum*1.0f).SetRelative().SetEase(Ease.Linear)
                    .SetRelative()
            .OnComplete(() => {       // ジャンプが終了したら、以下の操作をする
                anim.SetBool("run", false);  // アニメーションを run → stand に遷移させる
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
