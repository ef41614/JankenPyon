using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    private GameObject player;
    //Unityちゃんとカメラの距離
    private float difference;

    // Use this for initialization
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.player = GameObject.Find("player");
        //Unityちゃんとカメラの位置（z座標）の差を求める
        this.difference = player.transform.position.z - this.transform.position.z;
        Debug.Log("MyCameraController 出席確認");


    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの位置に合わせてカメラの位置を移動
        this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
