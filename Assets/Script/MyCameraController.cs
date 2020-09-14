using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    public GameObject player;
    //Unityちゃんとカメラの距離
    public float difference;
    bool FirstSetOK = false;

    // Use this for initialization
    void Start()
    {
        Debug.Log("MyCameraController 出席確認");
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstSetOK)
        {
            //Unityちゃんの位置に合わせてカメラの位置を移動
            this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }


    public void SetMyCamera()
    {
        Debug.Log("SetMyCamera 実行確認");
        //Unityちゃんのオブジェクトを取得
        this.player = GameObject.FindWithTag("MyPlayer");
        //Unityちゃんとカメラの位置（z座標）の差を求める
        this.difference = player.transform.position.z - this.transform.position.z;
        FirstSetOK = true;
    }
}
