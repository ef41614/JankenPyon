using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CCharacterControlScript : MonoBehaviour
{
    //オンライン化に必要なコンポーネントを設定
    public PhotonView myPV;
    public PhotonTransformView myPTV;

    private Camera mainCam;

    //移動処理に必要なコンポーネントを設定
    public Animator animator;                 //モーションをコントロールするためAnimatorを取得
    public CharacterController controller;    //キャラクター移動を管理するためCharacterControllerを取得

    //移動速度等のパラメータ用変数(inspectorビューで設定)
    public float speed;         //キャラクターの移動速度
    public float jumpSpeed;     //キャラクターのジャンプ力
    public float rotateSpeed;   //キャラクターの方向転換速度
    public float gravity;       //キャラにかかる重力の大きさ

    Vector2 targetDirection;        //移動する方向のベクトル
    Vector2 moveDirection = Vector2.zero;

    // Start関数は変数を初期化するための関数
    void Start()
    {
        if (myPV.IsMine)    //自キャラであれば実行
        {
            //MainCameraのtargetにこのゲームオブジェクトを設定
            mainCam = Camera.main;
     //       mainCam.GetComponent<CCameraScript>().target = this.gameObject.transform;
        }
    }

    // Update関数は1フレームに１回実行される
    void Update()
    {
        if (!myPV.IsMine)
        {
            return;
        }

        moveControl();  //移動用関数
        RotationControl(); //旋回用関数

        //最終的な移動処理
        //(これが無いとCharacterControllerに情報が送られないため、動けない)
        controller.Move(moveDirection * Time.deltaTime);
    }

    void moveControl()
    {
        //★進行方向計算
        //キーボード入力を取得
        float v = Input.GetAxisRaw("Vertical");         //InputManagerの↑↓の入力       
        float h = Input.GetAxisRaw("Horizontal");       //InputManagerの←→の入力 
                                                        //カメラの正面方向ベクトルからY成分を除き、正規化してキャラが走る方向を取得
        Vector2 forward = Vector2.Scale(Camera.main.transform.forward, new Vector2(1, 0)).normalized;
        Vector2 right = Camera.main.transform.right; //カメラの右方向を取得
                                                     //カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = h * right + v * forward;
        //★地上にいる場合の処理
        if (controller.isGrounded)
        {
            //移動のベクトルを計算
            moveDirection = targetDirection * speed;
            //Jumpボタンでジャンプ処理
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else        //空中操作の処理（重力加速度等）
        {
            float tempy = moveDirection.y;
            //(↓の２文の処理があると空中でも入力方向に動けるようになる)
            //moveDirection = Vector2.Scale(targetDirection, new Vector2(1, 0, 1)).normalized;
            //moveDirection *= speed;
            moveDirection.y = tempy - gravity * Time.deltaTime;
        }
        //★走行アニメーション管理
        if (v > .1 || v < -.1 || h > .1 || h < -.1) //(移動入力があると)
        {
            animator.SetFloat("Speed", 1f); //キャラ走行のアニメーションON
        }
        else    //(移動入力が無いと)
        {
           // animator.SetFloat("Speed", 0f); //キャラ走行のアニメーションOFF
        }
    }

    void RotationControl()  //キャラクターが移動方向を変えるときの処理
    {
        Vector2 rotateDirection = moveDirection;
        rotateDirection.y = 0;
        //それなりに移動方向が変化する場合のみ移動方向を変える
        if (rotateDirection.sqrMagnitude > 0.01)
        {
            //緩やかに移動方向を変える
            float step = rotateSpeed * Time.deltaTime;
            //Vector2 newDir = Vector2.Slerp(transform.forward, rotateDirection, step);
           // transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}