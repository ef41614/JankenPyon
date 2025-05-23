﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    string mode;                 // モード(ONLINE, OFFLINE)
    string dispStatus;           // 画面項目：状態
    string dispMessage;          // 画面項目：メッセージ
    string dispRoomName;         // 画面項目：ルーム名
    List<RoomInfo> roomDispList; // 画面項目：ルーム一覧
    byte MaxPlayers_inRoom = 4;         // 1ルームに入れる最大人数
    RoomOptions roomOptions;
    string Chimei = "";
    string Michi = "";
    //string Jikan = "";

    int Rnd_Chimei;
    int Rnd_Michi;
    int Rnd_Jikan;

    float span = 5f;
    private float currentTime = 0f; // test
    public GameObject BGM_SE_Manager;
    BGM_SE_Manager BGM_SE_MSC;

    // 状態
    public enum Status
    {
        ONLINE,   // オンライン
        OFFLINE,  // オフライン
    };



    private void Awake()
    {
        BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
    }


    private void Start()
    {
        Debug.Log("PhotonManager 出席確認");
        BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
        BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();
        BGM_SE_MSC.SasazukaHighwayPark_BGM();

        Debug.Log("あいことばを確認します");
        Debug.Log("あいことばは「 " + BGM_SE_MSC.Aikotoba + " 」です");
        Debug.Log("あいことばを確認しました");

        Set_initial_Parameters();               // ランダムマッチ用のルーム名を作成します
        SetOnline_ToStart();                    // ConnectPhoton 処理を実行します
        //RandomMatching();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > span)
        {
            Teiki_Update();
            currentTime = 0f;
        }
    }

    // 変数初期化処理
    private void Set_initial_Parameters()      // ランダムマッチ用のルーム名を作成します
    {
        Debug.Log("Set_initial_Parameters() 変数初期化処理");

        BGM_SE_MSC.stage_No = UnityEngine.Random.Range(0, 4);      // Battle ステージ

        // 部屋名設定
        RudCreate_Chimei();
        RudCreate_Michi();
        //RudCreate_Jikan();
        dispRoomName = Chimei + " " + Michi;
        Debug.Log("dispRoomName をランダム生成します" + dispRoomName);
        //dispRoomName = "";
        dispMessage = "";
        //dispStatus = Status.OFFLINE.ToString();
        //roomDispList = new List<RoomInfo>();
    }

    private void SetOnline_ToStart()           // ConnectPhoton 処理を実行します
    {
        Debug.Log("はじめから ONラインで実行する");
        dispStatus = Status.ONLINE.ToString();
        roomDispList = new List<RoomInfo>();
        ConnectPhoton(false);
    }



    // Photonサーバ接続処理
    public void ConnectPhoton(bool boolOffline)   // ConnectPhoton 処理を実行します → OnConnectedToMaster へ
    {
        Debug.Log("ConnectPhoton() Photonサーバ接続処理");
        if (boolOffline)
        {
            // オフラインモードを設定
            mode = Status.OFFLINE.ToString();
            PhotonNetwork.OfflineMode = true; // OnConnectedToMaster()が呼ばれる
            dispMessage = "OFFLINEモードで起動しました。";
            return;
        }
        // Photonサーバに接続する
        Debug.Log("Photonサーバに接続する");
        mode = Status.ONLINE.ToString();
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("ConnectUsingSettings を実施しました");
    }

    // Photonサーバ切断処理
    public void DisConnectPhoton()
    {
        PhotonNetwork.Disconnect();
        // 変数初期化
        Set_initial_Parameters();
    }

    // コールバック：Photonサーバ接続完了
    public override void OnConnectedToMaster()  // OnConnectedToMaster 処理を実行します → OnJoinedLobby へ
    {
        base.OnConnectedToMaster();
        if (Status.ONLINE.ToString().Equals(mode))
        {
            dispStatus = Status.ONLINE.ToString();
            dispMessage = "サーバに接続しました。";
            Debug.Log("サーバに接続しました");

            // ロビーに接続
            Debug.Log("ロビーに接続 JoinLobby 実行前");
            PhotonNetwork.JoinLobby();
            Debug.Log("ロビーに接続 JoinLobby 実行しました");
        }
    }

    // コールバック：Photonサーバ接続失敗
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        dispMessage = "サーバから切断しました。";
        Debug.Log("サーバから切断しました");
        dispStatus = Status.OFFLINE.ToString();
    }

    // コールバック：ロビー入室完了
    public override void OnJoinedLobby()       // OnJoinedLobby 処理を実行します → RandomMatching / または /  へ
    {
        Debug.Log("ロビー入室完了");
        base.OnJoinedLobby();

        Debug.Log("あいことばを確認します2");
        Debug.Log("あいことばは「 " + BGM_SE_MSC.Aikotoba + " 」です");
        Debug.Log("あいことばを確認しました2");

        if (BGM_SE_MSC.Aikotoba == "")       // あいことば が空欄ならば
        {
            Debug.Log("あいことば は使わず、ランダムマッチを実施します");
            RandomMatching();            // ランダムマッチ RandomMatching を実施する
        }
        else                             // あいことば が入力されていたら
        {
            Debug.Log("あいことばで 待ち合わせをします");
            EnterRoom_byAikotoba();      // あいことば を使ってルームに入室します
        }
    }

    public void RandomMatching()               // 既に作成されているルームの中の一つにランダムで参加する。（条件に該当しないルームは、ランダムマッチングの対象から除外される。）
    {
        Debug.Log("既に作成されているルームの中の一つにランダムで参加する。条件に該当しないルームは、ランダムマッチングの対象から除外される。");
        PhotonNetwork.JoinRandomRoom();        // （問題なく行ったら）OnJoinedRoom 処理を実行します
    }



    public void EnterRoom_byAikotoba()         // あいことば を使ってルームに入室します
    {
        Debug.Log("あいことば を使ってルームに入室します。");
        PhotonNetwork.JoinRoom(BGM_SE_MSC.Aikotoba);
    }

    // ランダムマッチングが失敗した時に呼ばれるコールバック
    public override void OnJoinRandomFailed(short returnCode, string message)  // ランダムマッチで 参加できるルームが存在しないなら、新しいルームを作成する
    {
        Debug.Log("ランダムに参加できるルームが存在しないため、新しいルームを作成します。");
        CreateRoom(dispRoomName);      // ランダムに参加できるルームが存在しないなら、新しいルームを作成する
    }

    // 特定の部屋への入室に失敗した時
    public override void OnJoinRoomFailed(short returnCode, string message)   // あいことば で参加できるルームが存在しないなら、新しいルームを作成する
    {
        Debug.Log("あいことば で参加できるルームが存在しないため、新しいルームを作成します");
        CreateRoom_byAikotoba();      // あいことば で参加できるルームが存在しないため、新しいルームを作成します
    }

    public void CreateRoom_byAikotoba()         // あいことば を使ってルームを作成します（公開はしない）
    {
        Debug.Log("あいことば を使ってルームを作成します。");
        // ルームオプションの基本設定
        roomOptions = new RoomOptions
        {
            // 部屋の最大人数
            MaxPlayers = MaxPlayers_inRoom,
            // 公開
            IsVisible = false
        };
        PhotonNetwork.CreateRoom(BGM_SE_MSC.Aikotoba, roomOptions);
        Debug.Log("ルーム作成処理 CreateRoom_byAikotoba を実行しました");
    }

    // ルーム一覧更新処理
    // (ロビーに入室した時、他のプレイヤーが更新した時のみ)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("ルーム一覧更新処理");
        base.OnRoomListUpdate(roomList);
        // ルーム一覧更新
        foreach (var info in roomList)
        {
            if (!info.RemovedFromList)
            {
                // 更新データが削除でない場合
                roomDispList.Add(info);
            }
            else
            {
                // 更新データが削除の場合
                roomDispList.Remove(info);
            }
        }
    }

    public void Teiki_Update()
    {
        Debug.Log("ルーム一覧を定期アップデートします");
        // ロビーに入り直す
        roomDispList = new List<RoomInfo>();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinLobby();

        base.OnRoomListUpdate(roomDispList);
        // ルーム一覧更新
        foreach (var info in roomDispList)
        {
            if (!info.RemovedFromList)
            {
                // 更新データが削除でない場合
                roomDispList.Add(info);
            }
            else
            {
                // 更新データが削除の場合
                roomDispList.Remove(info);
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                Debug.Log("ルームが満員なので非表示にします。");
                roomDispList.Remove(info);
            }
        }
        Debug.Log("ルーム一覧を定期アップデートしました");
    }

    // ルーム作成処理
    public void CreateRoom(string roomName)
    {
        Debug.Log("ルーム作成処理 CreateRoom を実行します");
        // ルームオプションの基本設定
        roomOptions = new RoomOptions
        {
            // 部屋の最大人数
            MaxPlayers = MaxPlayers_inRoom,
        };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("ルーム作成処理 CreateRoom を実行しました");
    }

    // ルーム入室処理
    public void ConnectToRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // コールバック：ルーム作成完了
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        dispMessage = "ルームを作成しました。";
        Debug.Log("Battleシーンへ遷移します" + dispRoomName);
        BGM_SE_MSC.Stop_BGM();
        //battleシーンへ遷移
        PhotonNetwork.LoadLevel("Battle");
    }

    // コールバック：ルーム作成失敗
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        dispMessage = "ルーム作成に失敗しました。";
    }

    // コールバック：ルームに入室した時
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (BGM_SE_MSC.Aikotoba == "")       // あいことば が空欄ならば（＝ランダムマッチなら）
        {
            // 表示ルームリストに追加する
            roomDispList.Add(PhotonNetwork.CurrentRoom);
        }

        dispMessage = "【" + PhotonNetwork.CurrentRoom.Name + "】" + "に入室しました。";
        Debug.Log("ルームに入りました。");
        Debug.Log("Battleシーンへ遷移します");

        //battleシーンをロード
        PhotonNetwork.LoadLevel("Battle");

        // 自身がルームに参加した時に満員になったら、以降そのルームを参加拒否設定にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("ルームが満員になりました。もう入れません。");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            GUILayout.Label("ルームが満員になりました。もう入れません。");
        }
    }


    #region// ステージ名を組み合わせでランダムに生成する処理
    public void RudCreate_Chimei()  //変数「 Rnd_Chimei 」の値を元に、 全10パターンの間で場合分けをする
    {
        Rnd_Chimei = UnityEngine.Random.Range(1, 10);

        if (BGM_SE_MSC.stage_No == 0)  // 街の街道
        {
            switch (Rnd_Chimei)
            {
                case 1: //
                    Chimei = "コンパトリ";
                    break;
                case 2: //
                    Chimei = "ペリアーテ";
                    break;
                case 3: //
                    Chimei = "サクローナ";
                    break;
                case 4: //
                    Chimei = "モンテルニ";
                    break;
                case 5: //
                    Chimei = "ジェナポリ";
                    break;
                case 6: //
                    Chimei = "クレモデナ";
                    break;
                case 7: //
                    Chimei = "ディカーラ";
                    break;
                case 8: //
                    Chimei = "ディポバッソ";
                    break;
                case 9: //
                    Chimei = "スカレッコ";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

        if (BGM_SE_MSC.stage_No == 1)   // エジプトのピラミッド
        {
            switch (Rnd_Chimei)
            {
                case 1: //
                    Chimei = "ラッシメピレ";
                    break;
                case 2: //
                    Chimei = "ペエニ";
                    break;
                case 3: //
                    Chimei = "ドラクーアー";
                    break;
                case 4: //
                    Chimei = "リングアコ";
                    break;
                case 5: //
                    Chimei = "コツヨン";
                    break;
                case 6: //
                    Chimei = "ニュポ";
                    break;
                case 7: //
                    Chimei = "ヘテー";
                    break;
                case 8: //
                    Chimei = "ストーララト";
                    break;
                case 9: //
                    Chimei = "ソコゾダ";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

        if (BGM_SE_MSC.stage_No == 2)   // ナイトタウン
        {
            switch (Rnd_Chimei)
            {
                case 1: //
                    Chimei = "トラース";
                    break;
                case 2: //
                    Chimei = "ナブスト";
                    break;
                case 3: //
                    Chimei = "モンファルク";
                    break;
                case 4: //
                    Chimei = "ケーハイク";
                    break;
                case 5: //
                    Chimei = "ローリ";
                    break;
                case 6: //
                    Chimei = "ゼンデ";
                    break;
                case 7: //
                    Chimei = "コーバー";
                    break;
                case 8: //
                    Chimei = "グランベルス";
                    break;
                case 9: //
                    Chimei = "ウフォレート";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

        if (BGM_SE_MSC.stage_No == 3)   // 運動公園
        {
            switch (Rnd_Chimei)
            {
                case 1: //
                    Chimei = "ルスル";
                    break;
                case 2: //
                    Chimei = "イーストン";
                    break;
                case 3: //
                    Chimei = "ソルムスピ";
                    break;
                case 4: //
                    Chimei = "フォート";
                    break;
                case 5: //
                    Chimei = "エックリー";
                    break;
                case 6: //
                    Chimei = "ルディーキャン";
                    break;
                case 7: //
                    Chimei = "ポップルドウ";
                    break;
                case 8: //
                    Chimei = "ミアドロ";
                    break;
                case 9: //
                    Chimei = "シーブロ";
                    break;
                default:
                    // その他処理
                    break;
            }
        }
    }

    public void RudCreate_Michi()  //変数「 Rnd_Michi 」の値を元に、 全10パターンの間で場合分けをする
    {
        Rnd_Michi = UnityEngine.Random.Range(1, 10);

        if (BGM_SE_MSC.stage_No == 0)   // 街の街道
        {
            switch (Rnd_Michi)
            {
                case 1: //
                    Michi = " いなかみち";
                    break;
                case 2: //
                    Michi = " ストリート";
                    break;
                case 3: //
                    Michi = " しょうてんがい";
                    break;
                case 4: //
                    Michi = " ゆうほどう";
                    break;
                case 5: //
                    Michi = " さんぽみち";
                    break;
                case 6: //
                    Michi = " ２ばんがい";
                    break;
                case 7: //
                    Michi = " ひがしどおり";
                    break;
                case 8: //
                    Michi = " ちゅうおうどおり";
                    break;
                case 9: //
                    Michi = " うらみち";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

        if (BGM_SE_MSC.stage_No == 1)   // エジプトのピラミッド
        {
            switch (Rnd_Michi)
            {
                case 1: //
                    Michi = " 王の間";
                    break;
                case 2: //
                    Michi = " 王妃の間";
                    break;
                case 3: //
                    Michi = " 大回廊";
                    break;
                case 4: //
                    Michi = " 地下の間";
                    break;
                case 5: //
                    Michi = " 玄室";
                    break;
                case 6: //
                    Michi = " 上昇通路";
                    break;
                case 7: //
                    Michi = " 水平通路";
                    break;
                case 8: //
                    Michi = " 下降通路";
                    break;
                case 9: //
                    Michi = " 控えの間";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

        if (BGM_SE_MSC.stage_No == 2)   // ナイトタウン
        {
            switch (Rnd_Michi)
            {
                case 1: //
                    Michi = " いなかみち";
                    break;
                case 2: //
                    Michi = " ストリート";
                    break;
                case 3: //
                    Michi = " しょうてんがい";
                    break;
                case 4: //
                    Michi = " ゆうほどう";
                    break;
                case 5: //
                    Michi = " さんぽみち";
                    break;
                case 6: //
                    Michi = " ２ばんがい";
                    break;
                case 7: //
                    Michi = " ひがしどおり";
                    break;
                case 8: //
                    Michi = " ちゅうおうどおり";
                    break;
                case 9: //
                    Michi = " うらみち";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

        if (BGM_SE_MSC.stage_No == 3)   // 運動公園
        {
            switch (Rnd_Michi)
            {
                case 1: //
                    Michi = " 運動公園";
                    break;
                case 2: //
                    Michi = " 運動場";
                    break;
                case 3: //
                    Michi = " パルコ";
                    break;
                case 4: //
                    Michi = " 競技場";
                    break;
                case 5: //
                    Michi = " グラウンド";
                    break;
                case 6: //
                    Michi = " スポーツ場";
                    break;
                case 7: //
                    Michi = " アスレチック場";
                    break;
                case 8: //
                    Michi = " パーク";
                    break;
                case 9: //
                    Michi = " 広場";
                    break;
                default:
                    // その他処理
                    break;
            }
        }

    }

    /*
    public void RudCreate_Jikan()  //変数「 Rnd_Jikan 」の値を元に、 全5パターンの間で場合分けをする
    {
        int Rnd_Jikan = UnityEngine.Random.Range(1, 6);

        switch (Rnd_Jikan)
        {
            case 1: //
                Jikan = " ひるさがり";
                break;
            case 2: //
                Jikan = " おひるどき";
                break;
            case 3: //
                Jikan = " 15じ";
                break;
            case 4: //
                Jikan = " ゆうがた";
                break;
            case 5: //
                Jikan = " あさ";
                break;
            default:
                // その他処理
                break;
        }
    }
    */
    #endregion

    #region// 設定GUI
    // ---------- 設定GUI ----------
    void OnGUI()
    {
        float scale = Screen.height / 480.0f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(
            Screen.width * 0.5f, Screen.height * 0.5f, 0),
            Quaternion.identity,
            new Vector3(scale, scale, 1.0f));

        GUI.Window(0, new Rect(-200, -200, 400, 400),
            NetworkSettingWindow, "まちあわせ エリア");
    }

    Vector2 scrollPosition;
    void NetworkSettingWindow(int windowID)
    {
        // ステータス, メッセージの表示
        GUILayout.BeginHorizontal();
        GUILayout.Label("状態: " + dispStatus, GUILayout.Width(100));
        GUILayout.FlexibleSpace();
        if (Status.ONLINE.ToString().Equals(dispStatus))
        {
            // サーバ接続時のみ表示
            if (GUILayout.Button("切断"))
                DisConnectPhoton();
        }
        GUILayout.EndHorizontal();
        GUILayout.Label(dispMessage);
        GUILayout.Space(20);

        if (!Status.ONLINE.ToString().Equals(dispStatus))
        {
            // --- 初期表示時、OFFLINEモードのみ表示
            // マスターサーバに接続する
            GUILayout.Label("【モード選択】");
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("ONLINE Mode"))
                ConnectPhoton(false);
            if (GUILayout.Button("OFFLINE Mode"))
                ConnectPhoton(true);
            GUILayout.EndHorizontal();
        }
        else if (Status.ONLINE.ToString().Equals(dispStatus))
        {
            // --- ONLINEモードのみ表示
            if (!(PhotonNetwork.CurrentRoom != null))
            {
                // ルーム作成
                GUILayout.Label("【ルーム作成】");
                GUILayout.BeginHorizontal();
                GUILayout.Label("　ルーム名: ");
                dispRoomName = GUILayout.TextField(dispRoomName, GUILayout.Width(300));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                // 作成ボタン
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("作成 & 入室"))
                {
                    CreateRoom(dispRoomName);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(20);

                // ルーム一覧
                GUILayout.Label("【ルーム一覧 (クリックで入室)】");
                // 一覧表示
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(380), GUILayout.Height(180));
                if (roomDispList != null && roomDispList.Count > 0)
                {
                    // 更新ボタン
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("更新"))
                    {
                        // ロビーに入り直す
                        roomDispList = new List<RoomInfo>();
                        PhotonNetwork.LeaveLobby();
                        PhotonNetwork.JoinLobby();
                    }
                    // ルーム一覧
                    GUILayout.EndHorizontal();
                    foreach (RoomInfo roomInfo in roomDispList)
                        if (GUILayout.Button(roomInfo.Name, GUI.skin.box, GUILayout.Width(360)))
                            ConnectToRoom(roomInfo.Name);
                }
                GUILayout.EndScrollView();
            }
        }
    }
    #endregion
}
