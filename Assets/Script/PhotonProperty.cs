﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;

public class PhotonProperty : MonoBehaviour
{
    // ルームプロパティ用のハッシュ(Dictionary)
    private static ExitGames.Client.Photon.Hashtable roomHash;

    private void Start()
    {
        // ルームプロパティ用のこのプレイヤーのハッシュ(Dictionary)を生成
        roomHash = new ExitGames.Client.Photon.Hashtable();
    }

    // ルームプロパティ ===========================================

    // RoomPropertyが更新された時に呼ばれる
    public void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable changedRoomHash)
    {
        // 更新したプレイヤーが保持しているハッシュを入れる
        roomHash = changedRoomHash;
    }

    // ルームプロパティのセット -----------------------------------
    // キーが既に存在していたら上書き
    public static void SetRoomProperty<T>(string key, T value)
    {
        roomHash[key] = value;

        // 自身のハッシュをネット上に送信
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    // 一次元配列用
    public static void SetRoomPropertyArray<T>(string key, T[] value)
    {
        // 要素数256を超える場合はここをshortなどに変更してください
        for (byte i = 0; i < value.Length; i++)
        {
            roomHash[key + i] = value[i];
        }

        // 自身のハッシュをネット上に送信
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    public static void SetRoomPropertyArray<T>(string key, byte arrayNum, T value)
    {
        roomHash[key + arrayNum] = value;

        // 自身のハッシュをネット上に送信
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    // キーが既に存在している場合エラーが出る
    public static void SetRoomPropertyAdd<T>(string key, T value)
    {
        roomHash.Add(key, value);

        // 自身のハッシュをネット上に送信
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }

    // ルームプロパティをゲット -----------------------------------
    public static T GetRoomProperty<T>(string key)
    {
        // outを受け取るための変数を用意
        object value;

        // 指定したキーがあれば返す
        if (roomHash.TryGetValue(key, out value))
        {
            // ボックス化解除
            return (T)value;
        }

        // 無かったらnullが返る
        return (T)value;
    }

    // 一次元配列用
    public static T GetRoomPropertyArray<T>(string key, byte arrayNum)
    {
        // outを受け取るための変数を用意
        object value;

        // 指定したキーがあれば返す
        if (roomHash.TryGetValue(key + arrayNum, out value))
        {
            // ボックス化解除
            return (T)value;
        }

        return (T)value;
    }
    // ============================================================
}