using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHeadNameController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    public GameObject playerHead;
    //Unityちゃんとカメラの距離
    public float difference;
    bool FirstSetOK = false;

    Transform MyPosStart_Trans;  // スタートラインの位置情報 (Transform)
    Vector3 MyPosStart_Vec3;     // スタートラインの位置情報 (Vector3)

    // Use this for initialization
    void Start()
    {
        Debug.Log("MyHeadNameController 出席確認");
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstSetOK)
        {
            //Unityちゃんの位置に合わせてカメラの位置を移動
            this.transform.position = new Vector3(playerHead.transform.position.x, playerHead.transform.position.y, this.transform.position.z);
        }
    }


    public void SetMyHeadName() // スタートラインに移動してからの処理（位置の保存）
    {
        Debug.Log("SetMyHeadName 実行確認");
        //Unityちゃんのオブジェクトを取得
        this.playerHead = GameObject.FindWithTag("MyHead");
        //Unityちゃんとカメラの位置（z座標）の差を求める
        this.difference = playerHead.transform.position.z - this.transform.position.z;
        FirstSetOK = true;
    }
}
