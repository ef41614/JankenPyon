using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHeadNameController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    public GameObject playerHead;
    //Unityちゃんとカメラの距離
    public float difference;

    // Use this for initialization
    void Start()
    {
        Debug.Log("MyHeadNameController 出席確認");
    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの位置に合わせてカメラの位置を移動
        this.transform.position = new Vector3(playerHead.transform.position.x, playerHead.transform.position.y, this.transform.position.z);
    }


    public void SetMyHeadName()
    {
        Debug.Log("SetMyHeadName 実行確認");
        //Unityちゃんのオブジェクトを取得
        this.playerHead = GameObject.FindWithTag("MyHead");
        //Unityちゃんとカメラの位置（z座標）の差を求める
        this.difference = playerHead.transform.position.z - this.transform.position.z;
    }
}
