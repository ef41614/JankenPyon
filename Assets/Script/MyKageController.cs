using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyKageController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    public GameObject MyAshi;
    //Unityちゃんとかげの距離
    public float difference;

    Transform PosStartTransform;  // 開始位置の位置情報 (Transform)
    Vector3 PosStart;             // 開始位置の位置情報 (Vector3)

    // Use this for initialization
    void Start()
    {
        this.MyAshi = GameObject.FindWithTag("MyAshi");
        Debug.Log("MyKageController 出席確認");
        Debug.Log("MyAshi.transform.position.y まえ1" + MyAshi.transform.position.y);
        Debug.Log("this.transform.position.y まえ1" + transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの位置に合わせてかげの位置を移動
//        this.transform.position = new Vector3(MyAshi.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = new Vector3(MyAshi.transform.position.x, PosStart.y, this.transform.position.z);
    }


    public void SetMyKage()
    {
        Debug.Log("SetMyCamera 実行確認");
        //Unityちゃんのオブジェクトを取得
        this.MyAshi = GameObject.FindWithTag("MyAshi");
        //Unityちゃんとかげの位置（z座標）の差を求める
        this.difference = MyAshi.transform.position.z - this.transform.position.z;
        //Unityちゃんの位置に合わせてかげの位置を移動（初期設定）
        Debug.Log("MyAshi.transform.position.y まえ2" + MyAshi.transform.position.y);
        Debug.Log("this.transform.position.y まえ2" + transform.position.y);
        this.transform.position = new Vector3(MyAshi.transform.position.x, MyAshi.transform.position.y, MyAshi.transform.position.z);
        Debug.Log("this.transform.position.y あと1" + transform.position.y);

        // transformを取得
        PosStartTransform = this.transform;

        // 開始位置の座標を取得
        PosStart = PosStartTransform.position;

        // 親子関係を解除する
        //this.gameObject.transform.parent = null;
        this.MyAshi = GameObject.FindWithTag("MyAshi");
    }
}
