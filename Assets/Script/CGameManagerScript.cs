﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CGameManagerScript : MonoBehaviourPunCallbacks
{
    //誰かがログインする度に生成するプレイヤーPrefab
    public GameObject playerPrefab;
    void Start()
    {
        if (!PhotonNetwork.IsConnected)   //Phootnに接続されていなければ
        {
            SceneManager.LoadScene("Launcher"); //ログイン画面に戻る
            return;
        }
        //Photonに接続していれば自プレイヤーを生成
        //GameObject Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        //Debug.Log("Photonに接続したので 自プレイヤーを生成");
    }
    void OnGUI()
    {
        //ログインの状態を画面上に出力
        //GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}