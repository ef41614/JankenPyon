using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using say;　// 対象のスクリプトの情報を取得
using BEFOOL.PhotonTest;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SelectJanken : MonoBehaviour, IPunObservable
{
    #region// 変数宣言
    //誰かがログインする度に生成するプレイヤーPrefab
    public GameObject MyPlayerPrefab;
    public GameObject playerPrefab_utako;
    public GameObject playerPrefab_unitychan;
    public GameObject playerPrefab_pchan;
    public GameObject playerPrefab_mobuchan;
    [SerializeField] SortingGroup MysortingGroup;

    public GameObject StartCorn_Head;  // スタートラインのコーン
    public GameObject StartCorn_Foot;  // スタートラインのコーン
    public GameObject StartMark1;  // スタートラインのオブジェクト
    public GameObject StartMark2;  // スタートラインのオブジェクト
    public GameObject StartMark3;  // スタートラインのオブジェクト
    public GameObject StartMark4;  // スタートラインのオブジェクト
    public GameObject PosPlayer1_obj;
    public GameObject PosPlayer2_obj;
    public GameObject PosPlayer3_obj;
    public GameObject PosPlayer4_obj;

    Transform StartCorn_HeadTransform;  // スタートラインの位置情報 (Transform)
    Transform StartCorn_FootTransform;  // スタートラインの位置情報 (Transform)
    Transform StartTrans1;  // スタートラインの位置情報 (Transform)
    Transform StartTrans2;  // スタートラインの位置情報 (Transform)
    Transform StartTrans3;  // スタートラインの位置情報 (Transform)
    Transform StartTrans4;  // スタートラインの位置情報 (Transform)
    Transform transformPlayer1;
    Transform transformPlayer2;
    Transform transformPlayer3;
    Transform transformPlayer4;

    Vector3 PosStartCorn_Head;     // スタートラインの位置情報 (Vector3)
    Vector3 PosStartCorn_Foot;     // スタートラインの位置情報 (Vector3)
    Vector3 PosStartMark1;     // スタートラインの位置情報 (Vector3)
    Vector3 PosStartMark2;     // スタートラインの位置情報 (Vector3)
    Vector3 PosStartMark3;     // スタートラインの位置情報 (Vector3)
    Vector3 PosStartMark4;     // スタートラインの位置情報 (Vector3)
    Vector3 PosPlayer1;
    Vector3 PosPlayer2;
    Vector3 PosPlayer3;
    Vector3 PosPlayer4;

    public Sprite sprite_Gu;
    public Sprite sprite_Choki;
    public Sprite sprite_Pa;

    public Sprite sprite_Icon_utako;
    public Sprite sprite_Icon_Unitychan;
    public Sprite sprite_Icon_Pchan;
    public Sprite sprite_Icon_mobuchan;

    public Image MyTeImg_1;
    public Image MyTeImg_2;
    public Image MyTeImg_3;
    public Image MyTeImg_4;
    public Image MyTeImg_5;

    public Image Img_CoverBlack_P1;
    public Image Img_CoverBlack_P2;
    public Image Img_CoverBlack_P3;
    public Image Img_CoverBlack_P4;

    public Image Img_Player1_Te1;
    public Image Img_Player1_Te2;
    public Image Img_Player1_Te3;
    public Image Img_Player1_Te4;
    public Image Img_Player1_Te5;

    public Image Img_Player2_Te1;
    public Image Img_Player2_Te2;
    public Image Img_Player2_Te3;
    public Image Img_Player2_Te4;
    public Image Img_Player2_Te5;

    public Image Img_Player3_Te1;
    public Image Img_Player3_Te2;
    public Image Img_Player3_Te3;
    public Image Img_Player3_Te4;
    public Image Img_Player3_Te5;

    public Image Img_Player4_Te1;
    public Image Img_Player4_Te2;
    public Image Img_Player4_Te3;
    public Image Img_Player4_Te4;
    public Image Img_Player4_Te5;

    public Image Img_Icon_Player1;
    public Image Img_Icon_Player2;
    public Image Img_Icon_Player3;
    public Image Img_Icon_Player4;

    public Image Img_Head_MyIcon;
    public Text Text_Head_MyPName;
    public Image Img_MyIcon_SelectPanel;
    public Text Text_MyPName_SelectPanel;

    public Text MyNumTeText_1;
    public Text MyNumTeText_2;
    public Text MyNumTeText_3;
    public Text MyNumTeText_4;
    public Text MyNumTeText_5;

    public Text Text_JankenPlayer1_Te1;
    public Text Text_JankenPlayer1_Te2;
    public Text Text_JankenPlayer1_Te3;
    public Text Text_JankenPlayer1_Te4;
    public Text Text_JankenPlayer1_Te5;

    public Text Text_JankenPlayer2_Te1;
    public Text Text_JankenPlayer2_Te2;
    public Text Text_JankenPlayer2_Te3;
    public Text Text_JankenPlayer2_Te4;
    public Text Text_JankenPlayer2_Te5;

    public Text Text_JankenPlayer3_Te1;
    public Text Text_JankenPlayer3_Te2;
    public Text Text_JankenPlayer3_Te3;
    public Text Text_JankenPlayer3_Te4;
    public Text Text_JankenPlayer3_Te5;

    public Text Text_JankenPlayer4_Te1;
    public Text Text_JankenPlayer4_Te2;
    public Text Text_JankenPlayer4_Te3;
    public Text Text_JankenPlayer4_Te4;
    public Text Text_JankenPlayer4_Te5;

    public int Int_MyJanken_Te1 = -1;
    public int Int_MyJanken_Te2 = -1;
    public int Int_MyJanken_Te3 = -1;
    public int Int_MyJanken_Te4 = -1;
    public int Int_MyJanken_Te5 = -1;

    public int int_Player1_Te1 = -1;
    public int int_Player1_Te2 = -1;
    public int int_Player1_Te3 = -1;
    public int int_Player1_Te4 = -1;
    public int int_Player1_Te5 = -1;

    public int int_Player2_Te1 = -1;
    public int int_Player2_Te2 = -1;
    public int int_Player2_Te3 = -1;
    public int int_Player2_Te4 = -1;
    public int int_Player2_Te5 = -1;

    public int int_Player3_Te1 = -1;
    public int int_Player3_Te2 = -1;
    public int int_Player3_Te3 = -1;
    public int int_Player3_Te4 = -1;
    public int int_Player3_Te5 = -1;

    public int int_Player4_Te1 = -1;
    public int int_Player4_Te2 = -1;
    public int int_Player4_Te3 = -1;
    public int int_Player4_Te4 = -1;
    public int int_Player4_Te5 = -1;

    public int KP1 = -1; // ジャンケン勝敗判定に使う、仮の値 （0：グー、1：チョキ、2：パー）
    public int KP2 = -1;
    public int KP3 = -1;
    public int KP4 = -1;

    public int int_IamNowWaiting = 0;        // 待機中フラグ 「0：まだ決定してない（待機まえ）」、「1：決定済み（待機中）」
    public int int_NowWaiting_Player1 = 0;   // ジャンケン手 決定して待機中かどうか （0：まだ決定してない、1：決定して待機中）
    public int int_NowWaiting_Player2 = 0;
    public int int_NowWaiting_Player3 = 0;
    public int int_NowWaiting_Player4 = 0;

    public int Iam_alive = 1;
    public int alivePlayer1 = 1; // ジャンケンで残留してれば 1 、負けたら -1
    public int alivePlayer2 = 1;
    public int alivePlayer3 = 1;
    public int alivePlayer4 = 1;

    int SankaNinzu = 0;       // 総参加人数
    int int_WaitingPlayers = 0;   // 現在待機中の総人数
    int anzenPoint = 0;
    public int WinnerNum = -1;     // 勝ったプレイヤーの番号

    public int original_StepNum;  // ジャンプして移動するステップ数（の元となる変数）

    public int int_MatchPlayerMaxNum = 4;   // このルームの最大プレイヤー人数（4人 / 10人 / 20人）
    public int int_conMyCharaAvatar = 0;    // ログイン前に選んだキャラクターのアバター番号

    [SerializeField]
    public int count_a = 1;
    public int NumLivePlayer = 0;       // 残りのプレイヤー人数
    public int count_RoundRoop = 1;     // ジャンケン勝ち負け判定のループ回数（現在何ラウンド目のループか？）

    public bool NoneGu = false;
    public bool NoneChoki = false;
    public bool NonePa = false;

    public string PresentPlayerID;
    private PhotonView photonView = null;

    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム

    public GameObject TestRoomController;  //ヒエラルキー上のオブジェクト名
    TestRoomController TestRoomControllerSC;

    public GameObject pchanClone; //ヒエラルキー上のオブジェクト名
    public GameObject utakoClone; //ヒエラルキー上のオブジェクト名
    public GameObject unitychanClone; //ヒエラルキー上のオブジェクト名
    public GameObject mobuchanClone; //ヒエラルキー上のオブジェクト名
    PlayerScript PlayerSC;//スクリプト名 + このページ上でのニックネーム

    public GameObject MainCamera; //ヒエラルキー上のオブジェクト名
    MyCameraController MyCameraControllerMSC;//スクリプト名 + このページ上でのニックネーム

    public GameObject MyKage; //ヒエラルキー上のオブジェクト名
    MyKageController MyKageControllerMSC;//スクリプト名 + このページ上でのニックネーム

    public GameObject Text_MyHeadName; //ヒエラルキー上のオブジェクト名
    MyHeadNameController MyHeadNameControllerMSC;//スクリプト名 + このページ上でのニックネーム

    //public GameObject PushTeBtnManager; //ヒエラルキー上のオブジェクト名
    //PushTeBtn PushTeBtnMSC;
    //public PushTeBtn PushTeBtnMSC;

    public GameObject myPlayer;

    public int rap1 = 0;
    public int receiveRap1 = 0;

    public string senderName = "anonymous";
    public string senderID = "2434";
    public string MyName = "me";
    public string MyID = "0000";

    public bool isSelected_A = false;
    public bool isSelected_B = false;
    public bool isSelected_C = false;
    public bool isSelected_D = false;
    public bool isSelected_E = false;

    public Button Btn_A;
    public Button Btn_B;
    public Button Btn_C;
    public Button Btn_D;
    public Button Btn_E;

    public bool CanPushBtn_A = true;
    public bool CanPushBtn_B = true;
    public bool CanPushBtn_C = true;
    public bool CanPushBtn_D = true;
    public bool CanPushBtn_E = true;

    public GameObject Text_Wait_Me; // test
    public GameObject Text_Wait_P1;
    public GameObject Text_Wait_P2;
    public GameObject Text_Wait_P3;
    public GameObject Text_Wait_P4;
    public float span = 5f;
    private float currentTime = 0f; // test

    #endregion

    #region // 【START】初期設定の処理一覧

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
        Debug.Log("【START-01】SelectJanken 出席確認1");
        Debug.Log("【START-01】キャラ アバター 誰を選んだかを（ログイン前画面から）コンバートします");
        int_conMyCharaAvatar = CLauncherScript.get_int_MyCharaAvatar(); // 【START-01】キャラ アバター 誰を選んだか（ログイン前画面からコンバートする）
    }

    void Start()
    {
        //var customProperties = photonView.Owner.CustomProperties;
        Debug.Log("【START-02】SelectJanken 出席確認2");
        Debug.Log("【START-02】int_conMyCharaAvatar（★キャラアバター） ： " + int_conMyCharaAvatar);

        Debug.Log("【START-03】他スクリプトと連携できるようにします");
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
        TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
        MyCameraControllerMSC = MainCamera.GetComponent<MyCameraController>();

        myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");

        Debug.Log("【START-03】 各変数を 初期化（リセット）します");
        count_a = 1;
        Debug.Log("【START-03】 各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする");
        ResetAlivePlayer();            //【START-03】 各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする

        Debug.Log("【START-04】自プレイヤーを生成します");
        CreatePlayerPrefab();          //【START-04】Photonに接続していれば自プレイヤーを生成

        Debug.Log("【START-05】スタートラインにランダムに移動させます");
        MoveToStartLineRandom();       //【START-05】スタートラインにランダムに移動させる

        Debug.Log("【START-06】MyPlayer にカメラを追従するようにセットします");
        MyCameraControllerMSC.SetMyCamera();  //【START-06】MyPlayer にカメラを追従するようにセット

        MyKage = GameObject.FindWithTag("MyKage");
        MyKageControllerMSC = MyKage.GetComponent<MyKageController>();

        Debug.Log("【START-07】スタート時 初期設定を全プレイヤーで共有する（座標、顔アイコン、頭上プレイヤー名）");
        ToShare_InitialSetting();     // 【START-07】スタート時 初期設定を全プレイヤーで共有

        Debug.Log("【START-09】ジャンケン手「決定ボタン」を表示できるか確認します");
        Check_CanAppear_KetteiBtn();  // 【START-09】ジャンケン手「決定ボタン」を表示できるか確認

        Debug.Log("【START-10】総参加人数 と 現在待機中の総人数 をチェックします");
        NinzuCheck();                 // 【START-10】総参加人数 と 現在待機中の総人数
        NumLivePlayer = SankaNinzu;
        Debug.Log("【START-11】NumLivePlayer（総参加人数＝生存者数） は " + NumLivePlayer);
    }

    void Update()
    {
        // test
        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            Debug.LogFormat("{0}秒経過", span);
            Text_Wait_Me.GetComponent<Text>().text = int_NowWaiting_Player1 + "";
            Text_Wait_P1.GetComponent<Text>().text = int_NowWaiting_Player1 + "";
            Text_Wait_P2.GetComponent<Text>().text = int_NowWaiting_Player2 + "";
            Text_Wait_P3.GetComponent<Text>().text = int_NowWaiting_Player3 + "";
            Text_Wait_P4.GetComponent<Text>().text = int_NowWaiting_Player4 + "";
            currentTime = 0f;
        }
    }

    public void CreatePlayerPrefab()    //【START-04】Photonに接続していれば自プレイヤーを生成
    {
        Debug.Log("【START-04】Photonに接続したので 自プレイヤーを生成");

        //GameObject MyPlayer = PhotonNetwork.Instantiate(this.MyPlayerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        //GameObject MyPlayer = PhotonNetwork.Instantiate(this.MyPlayerPrefab.name);
        //GameObject Player1 = PhotonNetwork.Instantiate(this.playerPrefab.name);

        if (int_conMyCharaAvatar == 1)  // うたこ
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_utako.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            utakoClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("utakoClone の名前は: " + utakoClone.name);
            PlayerSC = utakoClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 2) // Unityちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_unitychan.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            unitychanClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("unitychanClone の名前は: " + unitychanClone.name);
            PlayerSC = unitychanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 3) // Pちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_pchan.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            pchanClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("pchanClone の名前は: " + pchanClone.name);
            PlayerSC = pchanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 4) // モブちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_mobuchan.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            mobuchanClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("mobuchanClone の名前は: " + mobuchanClone.name);
            PlayerSC = mobuchanClone.GetComponent<PlayerScript>();
        }
    }


    #region // 【START-07】Battleシーン遷移後、初期設定・配置の処理一覧（アイコンのセット等）
    public void ToShare_InitialSetting()    // 【START-07】スタート時 初期設定を全プレイヤーで共有
    {
        TestRoomControllerSC.PNameCheck();  // プレイヤー名が埋まっていなかったら入れる
        MyPlayID();                         // 【START-07】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Share_InitialSetting();             // 【START-07】スタート時 初期設定を全プレイヤーで共有する（座標、顔アイコン、頭上プレイヤー名）
        if (int_MatchPlayerMaxNum > 4)      // マッチ人数が5人以上であるならば
        {
            Debug.Log("【START-08】マッチ人数が5人以上のため、スタートラインにランダムに移動させます");
            MoveToStartLineRandom();        // スタートラインにランダムに移動させる
            Debug.Log("【START-08】マッチ人数が5人以上のため、スタートラインにランダムに移動させました");
        }
    }

    public void Share_InitialSetting()     //【START-07】スタート時 初期設定を全プレイヤーで共有する（座標、顔アイコン、頭上プレイヤー名）
    {
        Debug.Log("【START-07】* Share_InitialSetting 実行 *");
        Debug.Log("MyName  " + MyName);
        Debug.Log("MyID  " + MyID);

        if (MyID == TestRoomControllerSC.string_PID1) // 自身がプレイヤー1 であるなら
        {
            Debug.Log("【START-07】プレイヤー1のアイコンをセットします");
            SharePlayerIcon_Player1();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark1();                // 【START-07】スタートマーク1 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 1;    // order in layer の順番調整に使用する整数
        }

        else if (MyID == TestRoomControllerSC.string_PID2) // 自身がプレイヤー2 であるなら
        {
            Debug.Log("【START-07】プレイヤー2のアイコンをセットします");
            SharePlayerIcon_Player2();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark2();                // 【START-07】スタートマーク2 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 2;    // order in layer の順番調整に使用する整数
        }

        else if (MyID == TestRoomControllerSC.string_PID3) // 自身がプレイヤー3 であるなら
        {
            Debug.Log("【START-07】プレイヤー3のアイコンをセットします");
            SharePlayerIcon_Player3();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark3();                // 【START-07】スタートマーク3 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 3;    // order in layer の順番調整に使用する整数
        }

        else if (MyID == TestRoomControllerSC.string_PID4) // 自身がプレイヤー4 であるなら
        {
            Debug.Log("【START-07】プレイヤー4のアイコンをセットします");
            SharePlayerIcon_Player4();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark4();                // 【START-07】スタートマーク4 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 4;    // order in layer の順番調整に使用する整数
        }

        Debug.Log("【START-07】MyPlayer に かげ を追従するようにセットします");
        MyKageControllerMSC.SetMyKage();       // MyPlayer に かげ を追従するようにセット

        Debug.Log("【START-07】order in layer （画像表示順）の順番調整をします");
        PlayerSC.SortMySpriteOrder();          // order in layer （画像表示順）の順番調整を実施する

        Debug.Log("【START-07】名前とアイコンをセットします");
        Text_MyPName_SelectPanel.text = MyName;  // SelectPanel にMyName をセット
        Text_Head_MyPName.text = MyName;         // 画面上部 にMyName をセット
        SetMyIcon_SelectPanel();                 //【START-07】私のアイコンをセレクトパネルにセットします
    }

    public void SetMyIcon_SelectPanel()  // 【START-07】私のアイコンをセレクトパネルにセットします
    {
        if (int_conMyCharaAvatar == 1)  // うたこ
        {
            Img_MyIcon_SelectPanel.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
            Img_Head_MyIcon.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
        }
        else if (int_conMyCharaAvatar == 2) // Unityちゃん
        {
            Img_MyIcon_SelectPanel.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
            Img_Head_MyIcon.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
        }
        else if (int_conMyCharaAvatar == 3) // Pちゃん
        {
            Img_MyIcon_SelectPanel.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
            Img_Head_MyIcon.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
        }
        else if (int_conMyCharaAvatar == 4) // モブちゃん
        {
            Img_MyIcon_SelectPanel.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
            Img_Head_MyIcon.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
        }
        Debug.Log("【START-07】私のアイコンをセレクトパネルにセットしました");
    }

    public void MoveToStartLineRandom()  // 【START-05】プレイヤーをスタートラインにランダムに移動(配置)させる
    {
        // transformを取得
        StartCorn_HeadTransform = StartCorn_Head.transform;
        StartCorn_FootTransform = StartCorn_Foot.transform;

        // スタートラインの座標を取得
        PosStartCorn_Head = StartCorn_HeadTransform.position;
        PosStartCorn_Foot = StartCorn_FootTransform.position;

        // プレイヤー位置をスタートラインにランダムに移動
        float Rnd_PosX = UnityEngine.Random.Range(-0.5f, -1.0f);
        float Rnd_PosY = UnityEngine.Random.Range(-0.2f, -2.0f);
        myPlayer.transform.position = new Vector3(StartCorn_HeadTransform.position.x + Rnd_PosX, StartCorn_HeadTransform.position.y + Rnd_PosY, StartCorn_HeadTransform.position.z);
        Debug.Log("【START-05】プレイヤーをスタートラインにランダムに移動(配置)させました");
    }

    public void MyPlayID() // 【START-07】【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    {
        // TestRoomControllerSC.PNameCheck();
        photonView.RPC("PlayerIDCheck", RpcTarget.All);
    }

    /// <summary>
    /// 【START-07】【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    /// </summary>
    /// <param name="mi">現プレイヤー名とプレイヤーID 取得、共有</param>
    [PunRPC]
    public void PlayerIDCheck(PhotonMessageInfo mi)  // 【START-07】【JK-11_1】
    {
        Debug.Log("【START-07】【JK-11_1】[PunRPC] PlayerIDCheck");
        //string senderName = "anonymous";
        //string senderID = "2434";
        if (mi.Sender != null)
        {
            senderName = mi.Sender.NickName;
            senderID = mi.Sender.UserId;
            MyName = senderName;
            MyID = senderID;
        }
        Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("MyName  " + MyName);  // 今ボタン押した人
        Debug.Log("MyID  " + MyID);  // 今ボタン押した人
    }


    public void MoveToStartMark1()                    //【START-07】スタートマーク1 の位置へ移動する
    {
        StartTrans1 = StartMark1.transform;           // transformを取得
        PosStartMark1 = StartTrans1.position;         // スタートマーク1 の座標を取得
        myPlayer.transform.position = PosStartMark1;  // プレイヤー位置を スタートマーク1 に移動
    }

    public void MoveToStartMark2()                    //【START-07】スタートマーク2 の位置へ移動する
    {
        StartTrans2 = StartMark2.transform;           // transformを取得
        PosStartMark2 = StartTrans2.position;         // スタートマーク2 の座標を取得
        myPlayer.transform.position = PosStartMark2;  // プレイヤー位置を スタートマーク2 に移動
    }

    public void MoveToStartMark3()                    //【START-07】スタートマーク3 の位置へ移動する
    {
        StartTrans3 = StartMark3.transform;           // transformを取得
        PosStartMark3 = StartTrans3.position;         // スタートマーク3 の座標を取得
        myPlayer.transform.position = PosStartMark3;  // プレイヤー位置を スタートマーク3 に移動
    }

    public void MoveToStartMark4()                    //【START-07】スタートマーク4 の位置へ移動する
    {
        StartTrans4 = StartMark4.transform;           // transformを取得
        PosStartMark4 = StartTrans4.position;         // スタートマーク4 の座標を取得
        myPlayer.transform.position = PosStartMark4;  // プレイヤー位置を スタートマーク4 に移動
    }

    public void SharePlayerIcon_Player1()             //【START-07】プレイヤー1 のアイコンをセットします
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("SetIconP1_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("SetIconP1_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("SetIconP1_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("SetIconP1_mobuchan", RpcTarget.All);
        }
        Debug.Log("【START-07】プレイヤー1のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP1_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
    }
    [PunRPC]
    public void SetIconP1_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
    }
    [PunRPC]
    public void SetIconP1_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
    }
    [PunRPC]
    public void SetIconP1_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
    }

    public void SharePlayerIcon_Player2()             // 【START-07】プレイヤー2 のアイコンをセットします
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("SetIconP2_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("SetIconP2_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("SetIconP2_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("SetIconP2_mobuchan", RpcTarget.All);
        }
        Debug.Log("【START-07】プレイヤー2のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP2_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
    }
    [PunRPC]
    public void SetIconP2_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
    }
    [PunRPC]
    public void SetIconP2_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
    }
    [PunRPC]
    public void SetIconP2_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
    }

    public void SharePlayerIcon_Player3()             // 【START-07】プレイヤー3 のアイコンをセットします
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("SetIconP3_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("SetIconP3_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("SetIconP3_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("SetIconP3_mobuchan", RpcTarget.All);
        }
        Debug.Log("【START-07】プレイヤー3のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP3_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
    }
    [PunRPC]
    public void SetIconP3_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
    }
    [PunRPC]
    public void SetIconP3_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
    }
    [PunRPC]
    public void SetIconP3_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
    }

    public void SharePlayerIcon_Player4()             // 【START-07】プレイヤー4 のアイコンをセットします
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("SetIconP4_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("SetIconP4_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("SetIconP4_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("SetIconP4_mobuchan", RpcTarget.All);
        }
        Debug.Log("【START-07】プレイヤー4のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP4_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
    }
    [PunRPC]
    public void SetIconP4_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
    }
    [PunRPC]
    public void SetIconP4_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
    }
    [PunRPC]
    public void SetIconP4_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
    }

    public void NinzuCheck()                          // 【START-10】【JK-12】総参加人数 と 現在待機中の総人数 の確認
    {
        SankaNinzu = TestRoomControllerSC.int_JoinedPlayerAllNum;       // 総参加人数
        int_WaitingPlayers = int_NowWaiting_Player1 + int_NowWaiting_Player2 + int_NowWaiting_Player3 + int_NowWaiting_Player4;   // 現在待機中の総人数
        Debug.Log("SankaNinzu ： " + SankaNinzu);
        Debug.Log("int_WaitingPlayers ： " + int_WaitingPlayers);
    }
    #endregion

    #endregion

    #region// 【JK-01】からの処理 右上のジャンケン セット「開始ボタン」を押してからの、一連の処理

    public void PushOpenMyJankenPanel_Button()       // 【JK-01】OpenMyJankenPanel_Button（右上のセット開始ボタン） を押した時の処理
    {
        Debug.Log("【JK-01】******************************************************************");
        Debug.Log("【JK-01】OpenMyJankenPanel_Button が押されました。セットを開始します。カードを配ります");
        Debug.Log("【JK-01】******************************************************************");
        ResetAlivePlayer();      // 【JK-01】各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする
        Reset_NowWaiting();      // 【JK-01】待機中確認のパラメータ 初期化 0：待機前（初期値） にする
        Debug.Log("【JK-01】共通ジャンケン パネル（ベース）を表示します");
        ShuffleCardsMSC.AppearJankenCards_Panel();
        Debug.Log("【JK-01】自分のジャンケン パネル（カード選択画面）を表示します");
        ShuffleCardsMSC.AppearMyJankenPanel();
        //ShuffleCardsMSC.AppearWait_JankenPanel();
        Debug.Log("【JK-01】セット開始したばかりなので、ジャンケン手「決定ボタン」を非表示にします");
        Check_CanAppear_KetteiBtn();     // 【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら決定ボタンを表示する
    }

    public void Janken_ExtraInning()     //【JK-37】ジャンケンカードを配る前の処理（延長戦突入時）
    {
        Debug.Log("【JK-38】（延長戦）決着がつかなかったので延長戦に突入します！ジャンケンカードを配る準備をします。");
        Debug.Log("【JK-38】（延長戦）各プレイヤーの生存フラグは そのままです（リセットしません）");
        // ResetAlivePlayer();      
        Debug.Log("【JK-39】（延長戦）待機中フラグを全員一律 初期化 0：待機前（初期値） にします");
        Reset_NowWaiting();                    //【JK-39】 待機中フラグを全員一律 初期化 0：待機前（初期値） にする
        Debug.Log("【JK-40】（延長戦）共通ジャンケン パネル（ベース）を表示します");
        ShuffleCardsMSC.AppearJankenCards_Panel();
        Debug.Log("【JK-41】（延長戦）自分のジャンケン パネル（カード選択画面）を表示します");
        ShuffleCardsMSC.AppearMyJankenPanel();
        Debug.Log("【JK-42】（延長戦）フラグに関わらず、全員一律「待機中」画面 を表示します");
        ShuffleCardsMSC.AppearWait_JankenPanel();
        Debug.Log("【JK-43】（延長戦）延長戦 開始したばかりなので、ジャンケン手「決定ボタン」を非表示にします");
        Check_CanAppear_KetteiBtn();          //【JK-43】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら決定ボタンを表示する
        Debug.Log("【JK-44】（延長戦）ジャンケン生存者は待機フラグを0（待機まえ）に、敗北者は待機フラグを1（待機中）にし、黒カバーします");
        Check_WaitingFlg_DependOn_alive();    //【JK-44】ジャンケン生存者は待機フラグを0（待機まえ）に、敗北者は待機フラグを1（待機中）にする&& 黒カバー表示【JK-45】
        Debug.Log("【JK-45】（延長戦）待機中フラグのプレイヤー間での共有が終わりました。");
        if (Iam_alive == 1)    // 自分がまだジャンケン生存者であるならば
        {
            Debug.Log("【JK-46】（延長戦）自分はまだジャンケン生存者です。私の待機フラグは0（待機まえ）です。黒カバーしません。");
            Debug.Log("【JK-46】（延長戦）カードを選んで、延長戦を闘います！");
            Debug.Log("【JK-47】（延長戦）「待機中」画面 を非表示にします （→ カード選べるようになる）");
            ShuffleCardsMSC.CloseWait_JankenPanel();        //【JK-47】「待機中」画面 を非表示にする
        }
        else                   // 自分がジャンケン敗北者であるならば
        {
            Debug.Log("【JK-46】（延長戦）自分はジャンケン敗北者...。私の待機フラグは1（待機中）です。黒カバーします。");
            Debug.Log("【JK-46】（延長戦）もうカードを選ぶこともできません。見守るだけです。");
            Debug.Log("【JK-47】（延長戦）私は待機中。「待機中」画面 は表示しておきます。（カードは選べない）");
        }
    }

    public void Check_CanAppear_KetteiBtn()       // 【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたらボタンを表示する
    {
        Debug.Log("【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら「決定ボタン」を表示する");
        ShuffleCardsMSC.CloseKetteiBtn();         // ボタンを閉じる（消す）
        if (!CanPushBtn_A && !CanPushBtn_B && !CanPushBtn_C && !CanPushBtn_D && !CanPushBtn_E)  // 5つすべてのジャンケン手を押した後ならば
        {
            ShuffleCardsMSC.AppearKetteiBtn();    // 決定ボタンを表示する
            Debug.Log("【JK-01】条件に合っていたため「決定ボタン」を表示しました");
        }
    }
    #endregion



    #region //【JK-05】ジャンケン手 決定ボタン（「これでOK!」）を押した時の処理以降
    public void JankenTe_Kettei()               // 【JK-05】からの処理 ： ジャンケン手 決定ボタン（「これでOK!」）を押した時の処理
    {
        Debug.Log("【JK-05】******************************************************************");
        Debug.Log("【JK-05】ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        Debug.Log("【JK-05】******************************************************************");
        ShuffleCardsMSC.CloseMyJankenPanel();   // 不要なパネルを閉じる

        Debug.Log("【JK-06】私のジャンケン手をみんなに提供（共有）します");
        ToSharePlayerTeNum();                   // 【JK-06】私のジャンケン手をみんなに提供（共有）します

        Debug.Log("【JK-08】決定ボタンを押したので、他のプレイヤーを待っています");
        ShuffleCardsMSC.AppearWait_JankenPanel();   // 待機中パネルを表示

        Debug.Log("【JK-10】私は待機中です");
        int_IamNowWaiting = 1;                  // 自分のジャンケン手 決定して待機中 （0：まだ決定してない、1：決定して待機中）

        Debug.Log("【JK-11】私が待機中ということを、全員（他のプレイヤー）に情報提供（共有）します");
        ToCheck_NowWaiting();                   // ジャンケンで自分が待機中の旨を 情報提供（共有）する

        Debug.Log("【JK-12】全員手が決定し、勝敗判定フェーズへ進めるか確認します");
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2f, () => Check_Can_Hantei_Stream());
    }

    #region // 【JK-11】待機中フラグ関連の処理
    public void ToCheck_NowWaiting()        // 【JK-11_1】ジャンケンで自分が待機中の旨を 情報提供（共有）する
    {
        TestRoomControllerSC.PNameCheck();  // プレイヤー名が埋まっていなかったら入れる
        MyPlayID();                         // 【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Check_NowWaiting();
    }

    public void Check_NowWaiting()          // 【JK-11_2】ジャンケンで自分が待機中の旨を 情報提供（共有）する
    {
        Debug.Log("【JK-11_2】* ジャンケンで自分が待機中かどうかの確認をします *");
        Debug.Log("MyName  " + MyName);
        Debug.Log("MyID  " + MyID);

        if (MyID == TestRoomControllerSC.string_PID1) // 自身がプレイヤー1 であるなら
        {
            Debug.Log("プレイヤー1は待機中です");
            // int_NowWaiting_Player1 = 1;
            photonView.RPC("Player1_NowWaiting", RpcTarget.All);
        }

        else if (MyID == TestRoomControllerSC.string_PID2) // 自身がプレイヤー2 であるなら
        {
            Debug.Log("プレイヤー2は待機中です");
            photonView.RPC("Player2_NowWaiting", RpcTarget.All);
        }

        else if (MyID == TestRoomControllerSC.string_PID3) // 自身がプレイヤー3 であるなら
        {
            Debug.Log("プレイヤー3は待機中です");
            photonView.RPC("Player3_NowWaiting", RpcTarget.All);
        }

        else if (MyID == TestRoomControllerSC.string_PID4) // 自身がプレイヤー4 であるなら
        {
            Debug.Log("プレイヤー4は待機中です");
            photonView.RPC("Player4_NowWaiting", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Player1_NowWaiting()  // 【JK-11_3】Player1 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        Debug.Log("[PunRPC] 【JK-11_3】Player1 が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player1 = 1;   // 0：待機前（初期値）、 1：待機中（決定ボタン押下後）
    }
    [PunRPC]
    public void Player2_NowWaiting()  // 【JK-11_3】Player2 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        int_NowWaiting_Player2 = 1;
    }
    [PunRPC]
    public void Player3_NowWaiting()  // 【JK-11_3】Player3 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        int_NowWaiting_Player3 = 1;
    }
    [PunRPC]
    public void Player4_NowWaiting()  // 【JK-11_3】Player4 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        int_NowWaiting_Player4 = 1;
    }

    public void Reset_NowWaiting()    // 【JK-01】【JK-38】待機中確認のパラメータ 初期化
    {
        Debug.Log("【JK-01】【JK-38】全員の待機中フラグを 0（待機まえ）にリセットします");
        int_NowWaiting_Player1 = 0;   // 0：待機前（初期値:決定ボタン押下前）、 1：待機中（決定ボタン押下後）
        int_NowWaiting_Player2 = 0;
        int_NowWaiting_Player3 = 0;
        int_NowWaiting_Player4 = 0;
    }
    #endregion

    public void Check_Can_Hantei_Stream()      // 【JK-12】勝敗判定フェーズへ進めるか確認する
    {
        Debug.Log(TestRoomControllerSC.allPlayers.Length + ": allPlayers.Length");
        Debug.Log("現在の参加人数は " + TestRoomControllerSC.int_JoinedPlayerAllNum);
        Debug.Log("【JK-12】総参加人数 と 現在待機中の総人数 をチェックします");
        NinzuCheck();                          // 【JK-12】総参加人数 と 現在待機中の総人数
        if (int_WaitingPlayers == SankaNinzu)  // 参加している全員が待機中になっていたら
        {
            Debug.Log("【JK-12_1】全員手が決定しました。全員待機中です。勝敗判定に進みます！！");
            //Hantei_Stream();                   // 【JK-21】ジャンケン勝敗判定実施 ⇒ 勝ったプレイヤー1名のみジャンプで前進する
            photonView.RPC("Hantei_Stream", RpcTarget.All);
        }
        else
        {
            Debug.Log("【JK-12_2】まだ決定ボタンを 押していない人がいます");
        }
    }

    [PunRPC]
    public void Hantei_Stream()  // 【JK-21】ジャンケン勝敗判定（ラウンドループ）実施 ⇒ 勝ったプレイヤー1名のみジャンプで前進する
    {
        Debug.Log("【JK-21】ジャンケン勝敗判定 開始");

        if (Iam_alive == 1) // 自分がジャンケン生存者である
        {
            Debug.Log("【JK-22】私は生きています！");
            ShuffleCardsMSC.CloseWait_JankenPanel();        //●非表示にする
        }
        else   // ジャンケン敗北者
        {
            Debug.Log("【JK-22】はぁ、はぁ、敗北者？");
            ShuffleCardsMSC.AppearWait_JankenPanel();       //●表示させる
        }

        Debug.Log("【JK-23】ジャンケン ラウンドループ を 開始します");
        // 1回目ループ
        if (NumLivePlayer > 1)  // ジャンケン生存者が2人以上残っている場合
        {
            JankenBattle_OneRoop();   // 【JK-23】ジャンケンバトルの１ループ分処理   
        }

        // 2回目ループ            
        if (NumLivePlayer > 1)  // ジャンケン生存者が2人以上残っている場合
        {
            JankenBattle_OneRoop();   // 【JK-23】ジャンケンバトルの１ループ分処理   
        }

        // 3回目ループ            
        if (NumLivePlayer > 1)  // ジャンケン生存者が2人以上残っている場合
        {
            JankenBattle_OneRoop();   // 【JK-23】ジャンケンバトルの１ループ分処理   
        }

        // 4回目ループ            
        if (NumLivePlayer > 1)  // ジャンケン生存者が2人以上残っている場合
        {
            JankenBattle_OneRoop();   // 【JK-23】ジャンケンバトルの１ループ分処理   
        }

        // 5回目ループ            
        if (NumLivePlayer > 1)  // ジャンケン生存者が2人以上残っている場合
        {
            JankenBattle_OneRoop();   // 【JK-23】ジャンケンバトルの１ループ分処理   
        }

        if (anzenPoint < 5)
        {
            // 【JK-27】生存者人数チェック： ジャンケン生存者が2人以上残っているか？ → 2人以上ならジャンケンカード再選択（延長戦）へ
            Debug.Log("【JK-27】5ラウンド終わり、ジャンケン生存者が2人以上残っているか、確認します");

            if (NumLivePlayer > 1)        //【JK-27_2】ジャンケン生存者が2人以上残っている場合
            {
                Debug.Log("【JK-27_2】5ラウンド終わりましたが、ジャンケン生存者が2人以上残っている ので 1人になるまでやり直します");
                anzenPoint++;
                Debug.Log("anzenPoint : " + anzenPoint);
                PrepareToNextSet();       //【JK-28】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット【JK-36】
                Debug.Log("【JK-37】（延長戦）延長戦に突入します");
                Janken_ExtraInning();     //【JK-37】（延長戦）ジャンケンカードを配る前の処理（延長戦突入時）
                //Hantei_Stream();        // 生存者 1人になるまでやり直し
            }
            else                          //【JK-27_2】ジャンケン生存者が1人のみの場合
            {
                Debug.Log("【JK-27_2】生存者 1名になりました");    // ここでジャンケンの勝者が 1名 になった
            }
        }
        else
        {
            Debug.LogError("【JK-2*】anzenPoint が 5回以上になりました。 スクリプトの見直しが必要です");
        }

        Debug.Log("【JK-100】この時点で ジャンケン生存者は 1名です。これから勝者の前進ジャンプ処理に移ります。");

        if (SankaNinzu == 1)          //【JK-101】参加人数が1人の時（テストプレイ時）
        {
            Debug.Log("【JK-101】参加人数が1人なので（テストプレイ時）、移動ステップ数を 4 に上書きします");
            original_StepNum = 4;     // 移動ステップ数を 4 に上書き
        }

        Debug.Log("【JK-102】ジャンケン勝敗 判定おわり");    // 【JK-102】ここでジャンケンの勝者が 1名 決まっている

        Debug.Log("【JK-103】ジャンケン勝敗の勝利者は？");
        WhoIsWinner();                //【JK-103】ジャンケン勝敗の勝利者は？

        Debug.Log("【JK-104】ジャンケンで自分が勝利者かどうかの確認をします");
        ToCheck_Iam_Winner();         //【JK-104】ジャンケンで自分が勝利者かどうかの確認をする → 勝ってたら右にジャンプ！【JK-106】

        Debug.Log("【JK-107】PrepareToNextSet 次のセットへ移る準備 をします");
        PrepareToNextSet();           //【JK-107】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット
        Debug.Log("【JK-108】PrepareToNextSet 次のセットへ移る準備 終わりました");

        Debug.Log("【JK-109】全員の aliveフラグ を 1 にします（全員生存）");
        ResetAlivePlayer();           //【JK-109】各種 生存者カウンター リセット
        anzenPoint = 0;
        ShuffleCardsMSC.ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
        Reset_NowWaiting();      // 待機中確認のパラメータ 初期化
    }


    public void JankenBattle_OneRoop()    // 【JK-23-】ジャンケンバトルの１ループ分処理（1ラウンド）
    {
        Debug.Log("【JK-23-】■count_RoundRoop : " + count_RoundRoop + " 回目（ラウンド）のジャンケンループ ");    // N回目のジャンケンループ
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2f * count_RoundRoop, () => JankenBattle_MainPart());  // ジャンケンバトルのメイン判定処理（2秒待機後）
    }

    public void JankenBattle_MainPart()   // 【JK-23-】ジャンケンバトルのメイン判定処理
    {
        SetKP_counter();    // 【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
        Syohai_Hantei();    // 【JK-25】N回目 のループ における 残留プレイヤー同士の じゃんけん手の勝ち負けを判定 → 人数が減る
        CountLivePlayer();  // 【JK-26】残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
        count_RoundRoop++;  // N回目 のループ を 1 進める
    }


    public void PrepareToNextSet()    // 【JK-28】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット【JK-36】
    {
        Debug.Log("【JK-28】PrepareToNextSet 次のセットへ移る準備をします");
        ResetMyNumTe_All();               // 【JK-29】MyNumTe 数値を -1 にリセット（int,text）
        Reset_MyRireki_All();             // 【JK-30】MyRireki イメージを null にリセット（Image）
        ToCanPush_All();                  // 【JK-31】じゃんけんカードボタン を押せるようにする(フラグのリセット）（bool）
        ResetPlayerTeNum();               // 【JK-32】Player1 ～ Player4 のじゃんけん手 数値を -1 にリセット（int,text）
        ResetImg_PlayerlayerRireki_All(); // 【JK-33】Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）
        Debug.Log("【JK-34】じゃんけんカード 手のリセット");
        ShuffleCardsMSC.Reset_All();      // 【JK-34】じゃんけんカード 手のリセット
        Debug.Log("【JK-35】じゃんけんカード 手のセット");
        ShuffleCardsMSC.Set_All();        // 【JK-35】じゃんけんカード 手のセット
                                          // ResetAlivePlayer();  // 各種カウンター リセット
        count_RoundRoop = 1;              // 【JK-36】ラウンドループカウンター 1に戻す
    }

    public void Check_WaitingFlg_DependOn_alive()  //【JK-44】（延長戦）ジャンケン生存者（aliveフラグが 1 の人）は待機フラグを0に、敗北者は待機フラグを1にする && 黒カバー表示
    {
        if (alivePlayer1 == 1)            // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player1 = 0;  // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player1 = 1;  // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player1_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P1();   // 黒カバー表示
        }

        if (alivePlayer2 == 1)            // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player2 = 0;  // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player2 = 1;  // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player2_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P2();   // 黒カバー表示
        }

        if (alivePlayer4 == 1)            // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player3 = 0;  // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player3 = 1;  // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player3_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P3();   // 黒カバー表示
        }

        if (alivePlayer4 == 1)            // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player4 = 0;  // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player4 = 1;  // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player4_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P4();   // 黒カバー表示
        }
        Debug.Log("【JK-45】（延長戦）生き残っている者のみが「待機まえ」になりました。敗北者は待機中（見守り中）です。");
    }

    public void ResetAlivePlayer()         // 【JK-01】各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする）
    {
        Debug.Log("【JK-01】全員の aliveフラグ を 1 にします（全員生存）");
        alivePlayer1 = 1;           // ジャンケンで残留してれば 1 、負けたら 0
        alivePlayer2 = 1;
        alivePlayer3 = 1;
        alivePlayer4 = 1;
        Iam_alive = 1;
        CloseImg_CoverBlack_All();  // ジャンケン手の黒カバーをリセット（非表示）

        count_RoundRoop = 1;        // ラウンドループを1に戻す
    }

    public void ToCheck_Iam_alive()        // ジャンケンで自分が生き残っているかどうかの確認をする
    {
        TestRoomControllerSC.PNameCheck();  // プレイヤー名が埋まっていなかったら入れる
        MyPlayID();                         // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Check_Iam_alive();
    }

    public void Check_Iam_alive()          // ジャンケンで自分が生き残っているかどうかの確認をする
    {
        Debug.Log("* ジャンケンで自分が生き残っているかどうかの確認をします *");
        Debug.Log("MyName  " + MyName);
        Debug.Log("MyID  " + MyID);

        if (MyID == TestRoomControllerSC.string_PID1) // 自身がプレイヤー1 であるなら
        {
            if (alivePlayer1 == 1) // Player1 が生きている
            {
                Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }

        else if (MyID == TestRoomControllerSC.string_PID2) // 自身がプレイヤー2 であるなら
        {
            if (alivePlayer2 == 1) // Player2 が生きている
            {
                Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }

        else if (MyID == TestRoomControllerSC.string_PID3) // 自身がプレイヤー3 であるなら
        {
            if (alivePlayer3 == 1) // Player3 が生きている
            {
                Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }

        else if (MyID == TestRoomControllerSC.string_PID4) // 自身がプレイヤー4 であるなら
        {
            if (alivePlayer4 == 1) // Player4 が生きている
            {
                Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }
    }

    public void WhoIsWinner()              //【JK-103】ジャンケン勝敗の勝利者は？
    {
        WinnerNum = -1;                    // 一旦リセット
        if (alivePlayer1 == 1)
        {
            Debug.Log("【JK-103】Player1 勝利");
            WinnerNum = 1;
        }
        else if (alivePlayer2 == 1)
        {
            Debug.Log("【JK-103】Player2 勝利");
            WinnerNum = 2;
        }
        else if (alivePlayer3 == 1)
        {
            Debug.Log("【JK-103】Player3 勝利");
            WinnerNum = 3;
        }
        else if (alivePlayer4 == 1)
        {
            Debug.Log("【JK-103】Player4 勝利");
            WinnerNum = 4;
        }
        else
        {
            Debug.LogError("【JK-103】勝利いない？");
        }
    }

    public void ToCheck_Iam_Winner()       //【JK-104】ジャンケンで自分が勝利者かどうかの確認をするための準備【JK-106】
    {
        TestRoomControllerSC.PNameCheck();  // プレイヤー名が埋まっていなかったら入れる
        MyPlayID();                         // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Check_Iam_Winner();
    }

    public void Check_Iam_Winner()         //【JK-105】ジャンケンで自分が勝利者かどうかの確認をする
    {
        Debug.Log("【JK-105】ジャンケンで自分が勝利者かどうかの確認をします。自分が勝ってたらジャンプします。");
        Debug.Log("【JK-105】MyName  " + MyName);
        Debug.Log("【JK-105】MyID  " + MyID);

        if (MyID == TestRoomControllerSC.string_PID1) // 自身がプレイヤー1 であるなら
        {
            if (WinnerNum == 1)          // プレイヤー1 が勝利者
            {
                Debug.Log("【JK-106】P1 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (MyID == TestRoomControllerSC.string_PID2) // 自身がプレイヤー2 であるなら
        {
            if (WinnerNum == 2)          // プレイヤー2 が勝利者
            {
                Debug.Log("【JK-106】P2 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (MyID == TestRoomControllerSC.string_PID3) // 自身がプレイヤー3 であるなら
        {
            if (WinnerNum == 3)          // プレイヤー3 が勝利者
            {
                Debug.Log("【JK-106】P3 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (MyID == TestRoomControllerSC.string_PID4) // 自身がプレイヤー4 であるなら
        {
            if (WinnerNum == 4)          // プレイヤー4 が勝利者
            {
                Debug.Log("【JK-106】P4 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }
    }

    public void WaitTime_2nd()
    {
        Debug.Log("2秒待ち");
    }


    public void CountLivePlayer()     // 【JK-26】残留しているプレイヤー人数をカウントする
    {
        NumLivePlayer = alivePlayer1 + alivePlayer2 + alivePlayer3 + alivePlayer4;
        Debug.Log("【JK-26】NumLivePlayer 残留プレイヤー数 ： " + NumLivePlayer);
    }

    public void SetKP_counter()      // 【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
    {
        Debug.Log("【JK-24】count_RoundRoop" + count_RoundRoop);
        if (count_RoundRoop == 1)
        {
            KP1 = int_Player1_Te1;
            KP2 = int_Player2_Te1;
            KP3 = int_Player3_Te1;
            KP4 = int_Player4_Te1;
        }
        else if (count_RoundRoop == 2)
        {
            KP1 = int_Player1_Te2;
            KP2 = int_Player2_Te2;
            KP3 = int_Player3_Te2;
            KP4 = int_Player4_Te2;
        }
        else if (count_RoundRoop == 3)
        {
            KP1 = int_Player1_Te3;
            KP2 = int_Player2_Te3;
            KP3 = int_Player3_Te3;
            KP4 = int_Player4_Te3;
        }
        else if (count_RoundRoop == 4)
        {
            KP1 = int_Player1_Te4;
            KP2 = int_Player2_Te4;
            KP3 = int_Player3_Te4;
            KP4 = int_Player4_Te4;
        }
        else if (count_RoundRoop == 5)
        {
            KP1 = int_Player1_Te5;
            KP2 = int_Player2_Te5;
            KP3 = int_Player3_Te5;
            KP4 = int_Player4_Te5;
        }
        else
        {
            Debug.Log("count_RoundRoop ６回 超えました");
            // 再度、ジャンケン手カードの選択をする
        }
        Debug.Log("KP1 ： " + KP1);
        Debug.Log("KP2 ： " + KP2);
        Debug.Log("KP3 ： " + KP3);
        Debug.Log("KP4 ： " + KP4);
    }

    public void Check_Gu_Existence() // NoneGu の判定を返す
    {
        bool NoneGuP1 = true;
        bool NoneGuP2 = true;
        bool NoneGuP3 = true;
        bool NoneGuP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 0) // グー ではない
            {
                NoneGuP1 = true; // グー 無し
            }
            else
            {
                NoneGuP1 = false; // グー 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneGuP1 = true; // グー無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 0) // グー ではない
            {
                NoneGuP2 = true; // グー 無し
            }
            else
            {
                NoneGuP2 = false; // グー 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneGuP2 = true; // グー無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 0) // グー ではない
            {
                NoneGuP3 = true; // グー 無し
            }
            else
            {
                NoneGuP3 = false; // グー 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneGuP3 = true; // グー無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 0) // グー ではない
            {
                NoneGuP4 = true; // グー 無し
            }
            else
            {
                NoneGuP4 = false; // グー 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneGuP4 = true; // グー無しON
        }

        if (NoneGuP1 && NoneGuP2 && NoneGuP3 && NoneGuP4)
        {
            NoneGu = true; // グー無しON
        }
        else
        {
            NoneGu = false; // グー無しOFF
        }
        Debug.Log("NoneGuP1 ： " + NoneGuP1);
        Debug.Log("NoneGuP2 ： " + NoneGuP2);
        Debug.Log("NoneGuP3 ： " + NoneGuP3);
        Debug.Log("NoneGuP4 ： " + NoneGuP4);
    }

    public void Check_Choki_Existence() // NoneChoki の判定を返す
    {
        bool NoneChokiP1 = true;
        bool NoneChokiP2 = true;
        bool NoneChokiP3 = true;
        bool NoneChokiP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 1) // チョキ ではない
            {
                NoneChokiP1 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP1 = false; // チョキ 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneChokiP1 = true; // チョキ無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 1) // チョキ ではない
            {
                NoneChokiP2 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP2 = false; // チョキ 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneChokiP2 = true; // チョキ無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 1) // チョキ ではない
            {
                NoneChokiP3 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP3 = false; // チョキ 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneChokiP3 = true; // チョキ無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 1) // チョキ ではない
            {
                NoneChokiP4 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP4 = false; // チョキ 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneChokiP4 = true; // チョキ無しON
        }

        if (NoneChokiP1 && NoneChokiP2 && NoneChokiP3 && NoneChokiP4)
        {
            NoneChoki = true; // チョキ無しON
        }
        else
        {
            NoneChoki = false; // チョキ無しOFF
        }
        Debug.Log("NoneChokiP1 ： " + NoneChokiP1);
        Debug.Log("NoneChokiP2 ： " + NoneChokiP2);
        Debug.Log("NoneChokiP3 ： " + NoneChokiP3);
        Debug.Log("NoneChokiP4 ： " + NoneChokiP4);
    }

    public void Check_Pa_Existence() // NonePa の判定を返す
    {
        bool NonePaP1 = true;
        bool NonePaP2 = true;
        bool NonePaP3 = true;
        bool NonePaP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 2) // パー ではない
            {
                NonePaP1 = true; // パー 無し
            }
            else
            {
                NonePaP1 = false; // パー 有り
            }
        }
        else  // Player1 が脱落後
        {
            NonePaP1 = true; // パー無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 2) // パー ではない
            {
                NonePaP2 = true; // パー 無し
            }
            else
            {
                NonePaP2 = false; // パー 有り
            }
        }
        else  // Player2 が脱落後
        {
            NonePaP2 = true; // パー無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 2) // パー ではない
            {
                NonePaP3 = true; // パー 無し
            }
            else
            {
                NonePaP3 = false; // パー 有り
            }
        }
        else  // Player3 が脱落後
        {
            NonePaP3 = true; // パー無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 2) // パー ではない
            {
                NonePaP4 = true; // パー 無し
            }
            else
            {
                NonePaP4 = false; // パー 有り
            }
        }
        else  // Player4 が脱落後
        {
            NonePaP4 = true; // パー無しON
        }

        if (NonePaP1 && NonePaP2 && NonePaP3 && NonePaP4)
        {
            NonePa = true; // パー無しON
        }
        else
        {
            NonePa = false; // パー無しOFF
        }
        Debug.Log("NonePaP1 ： " + NonePaP1);
        Debug.Log("NonePaP2 ： " + NonePaP2);
        Debug.Log("NonePaP3 ： " + NonePaP3);
        Debug.Log("NonePaP4 ： " + NonePaP4);
    }

    public void Syohai_Hantei()  // 【JK-25】N回目 のループ における 残留プレイヤー同士の じゃんけん手の勝ち負けを判定（負けた人のaliveフラグ を 0 にする） → 人数が減る
    {
        Debug.Log("【JK-25】Syohai_Hantei スタート");
        Check_Gu_Existence();     // N回目の すべてのプレイヤーの手 の中に グー(0)   があるか ： NoneGu を取得
        Check_Choki_Existence();  // N回目の すべてのプレイヤーの手 の中に チョキ(1) があるか ： NoneChoki を取得
        Check_Pa_Existence();     // N回目の すべてのプレイヤーの手 の中に パー(2)   があるか ： NonePa を取得

        if (NoneGu) // 全員 Gu 無し (ちょき か ぱー)
        {
            Debug.Log("(NoneGu) です");
            if (NoneChoki) // 全員 Choki 無し (ぱー のみ)
            {
                Aiko(); // ぱー のみ
            }
            else // ちょき か ぱー
            {
                Win_Choki();
                Lose_Pa();    // ぱー の人のみ 脱落
            }
        }
        else if (NoneChoki) //(全員 Choki 無し) ぐー か ぱー （↓これ以降、ぐー は必ずある）
        {
            Debug.Log("(NoneChoki) です");
            if (NonePa) //(全員 Pa 無し) ぐー のみ
            {
                Aiko(); // ぐー のみ
            }
            else // ぐー か ぱー
            {
                Win_Pa();
                Lose_Gu();  // ぐー の人のみ 脱落
            }
        }
        else if (NonePa) //(全員 Pa 無し)  ぐー か ちょき （↓これ以降、ぐー と ちょき は必ずある）
        {
            Debug.Log("(NonePa) です");
            if (NoneGu) //(全員 Gu 無し) ちょき のみ
            {
                Aiko(); // ちょき のみ
            }
            else // ぐー か ちょき
            {
                Win_Gu();
                Lose_Choki();  // ちょき の人のみ 脱落
            }
        }
        else // ぐー か ちょき か ぱー（↓これ以降、ぐー と ちょき と ぱー は必ずある）
        {
            Aiko(); // ぐー ちょき ぱー 全部
        }
    }

    public void Aiko()
    {
        // 残っている人、全員残留
        Debug.Log("あいこ です");
    }

    public void Win_Gu()
    {
        // ぐー の人のみ 残留
        original_StepNum = 3;     // 移動ステップ数を 3 に上書き
    }

    public void Win_Choki()
    {
        // ちょき の人のみ 残留
        original_StepNum = 6;     // 移動ステップ数を 6 に上書き
    }

    public void Win_Pa()
    {
        // ぱー の人のみ 残留
        original_StepNum = 6;     // 移動ステップ数を 6 に上書き
    }

    public void Lose_Gu()     // ぐー の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        if (KP1 == 0)
        {
            alivePlayer1 = 0;
            AppearImg_CoverBlack_P1();
        }
        if (KP2 == 0)
        {
            alivePlayer2 = 0;
            AppearImg_CoverBlack_P2();
        }
        if (KP3 == 0)
        {
            alivePlayer3 = 0;
            AppearImg_CoverBlack_P3();
        }
        if (KP4 == 0)
        {
            alivePlayer4 = 0;
            AppearImg_CoverBlack_P4();
        }
    }

    public void Lose_Choki()  // ちょき の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        if (KP1 == 1)
        {
            alivePlayer1 = 0;
            AppearImg_CoverBlack_P1();
        }
        if (KP2 == 1)
        {
            alivePlayer2 = 0;
            AppearImg_CoverBlack_P2();
        }
        if (KP3 == 1)
        {
            alivePlayer3 = 0;
            AppearImg_CoverBlack_P3();
        }
        if (KP4 == 1)
        {
            alivePlayer4 = 0;
            AppearImg_CoverBlack_P4();
        }
    }

    public void Lose_Pa()     // ぱー の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        if (KP1 == 2)
        {
            alivePlayer1 = 0;
            AppearImg_CoverBlack_P1();
        }
        if (KP2 == 2)
        {
            alivePlayer2 = 0;
            AppearImg_CoverBlack_P2();
        }
        if (KP3 == 2)
        {
            alivePlayer3 = 0;
            AppearImg_CoverBlack_P3();
        }
        if (KP4 == 2)
        {
            alivePlayer4 = 0;
            AppearImg_CoverBlack_P4();
        }
    }

    #region // 黒カバーの表示・非表示
    public void AppearImg_CoverBlack_P1() // 黒カバー 表示させる
    {
        Img_CoverBlack_P1.enabled = true;
    }

    public void AppearImg_CoverBlack_P2() // 黒カバー 表示させる
    {
        Img_CoverBlack_P2.enabled = true;
    }

    public void AppearImg_CoverBlack_P3() // 黒カバー 表示させる
    {
        Img_CoverBlack_P3.enabled = true;
    }

    public void AppearImg_CoverBlack_P4() // 黒カバー 表示させる
    {
        Img_CoverBlack_P4.enabled = true;
    }

    public void CloseImg_CoverBlack_All()  // すべての黒カバーを閉じる（消す）
    {
        CloseImg_CoverBlack_P1();
        CloseImg_CoverBlack_P2();
        CloseImg_CoverBlack_P3();
        CloseImg_CoverBlack_P4();
    }

    public void CloseImg_CoverBlack_P1() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P1.enabled = false;
    }

    public void CloseImg_CoverBlack_P2() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P2.enabled = false;
    }

    public void CloseImg_CoverBlack_P3() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P3.enabled = false;
    }

    public void CloseImg_CoverBlack_P4() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P4.enabled = false;
    }
    #endregion

    #endregion


    public void ToSharePlayerTeNum()  // 【JK-06】私のジャンケン手をみんなに提供（共有）します
    {
        TestRoomControllerSC.PNameCheck(); // プレイヤー名が埋まっていなかったら入れる
        MyPlayID();  // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        //photonView.RPC("SharePlayerTeNum", RpcTarget.All);
        SharePlayerTeNum();
    }

    public void SharePlayerTeNum()    // 【JK-06】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）【JK-08】
    {
        Debug.Log("【JK-06】************ データ共有 SharePlayerTeNum  スタート **********");
        Debug.Log(TestRoomControllerSC.string_PID1 + ": TestRoomControllerSC.string_PID1");
        Debug.Log(TestRoomControllerSC.string_PID2 + ": TestRoomControllerSC.string_PID2");
        Debug.Log(TestRoomControllerSC.string_PID3 + ": TestRoomControllerSC.string_PID3");
        Debug.Log(TestRoomControllerSC.string_PID4 + ": TestRoomControllerSC.string_PID4");
        Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("MyName  " + MyName);  // 今ボタン押した人
        Debug.Log("MyID  " + MyID);  // 今ボタン押した人

        if (senderID == TestRoomControllerSC.string_PID1)
        {
            Debug.Log("【JK-07】今からプレイヤー1＝私（" + MyName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player1();
        }

        if (senderID == TestRoomControllerSC.string_PID2)
        {
            Debug.Log("【JK-07】今からプレイヤー2＝私（" + MyName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player2();
        }

        if (senderID == TestRoomControllerSC.string_PID3)
        {
            Debug.Log("【JK-07】今からプレイヤー3＝私（" + MyName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player3();
        }

        if (senderID == TestRoomControllerSC.string_PID4)
        {
            Debug.Log("【JK-07】今からプレイヤー4＝私（" + MyName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player4();
        }

        Debug.Log("【JK-08】************ データ共有 SharePlayerTeNum おわり **********");
    }


    public void ResetPlayerTeNum()    //【JK-32】 Player1 ～ Player4 のじゃんけん手 数値を -1 にリセット（int,text）
    {
        int_Player1_Te1 = -1;
        int_Player1_Te2 = -1;
        int_Player1_Te3 = -1;
        int_Player1_Te4 = -1;
        int_Player1_Te5 = -1;

        int_Player2_Te1 = -1;
        int_Player2_Te2 = -1;
        int_Player2_Te3 = -1;
        int_Player2_Te4 = -1;
        int_Player2_Te5 = -1;

        int_Player3_Te1 = -1;
        int_Player3_Te2 = -1;
        int_Player3_Te3 = -1;
        int_Player3_Te4 = -1;
        int_Player3_Te5 = -1;

        int_Player4_Te1 = -1;
        int_Player4_Te2 = -1;
        int_Player4_Te3 = -1;
        int_Player4_Te4 = -1;
        int_Player4_Te5 = -1;

        Text_JankenPlayer1_Te1.text = "-1";
        Text_JankenPlayer1_Te2.text = "-1";
        Text_JankenPlayer1_Te3.text = "-1";
        Text_JankenPlayer1_Te4.text = "-1";
        Text_JankenPlayer1_Te5.text = "-1";

        Text_JankenPlayer2_Te1.text = "-1";
        Text_JankenPlayer2_Te2.text = "-1";
        Text_JankenPlayer2_Te3.text = "-1";
        Text_JankenPlayer2_Te4.text = "-1";
        Text_JankenPlayer2_Te5.text = "-1";

        Text_JankenPlayer3_Te1.text = "-1";
        Text_JankenPlayer3_Te2.text = "-1";
        Text_JankenPlayer3_Te3.text = "-1";
        Text_JankenPlayer3_Te4.text = "-1";
        Text_JankenPlayer3_Te5.text = "-1";

        Text_JankenPlayer4_Te1.text = "-1";
        Text_JankenPlayer4_Te2.text = "-1";
        Text_JankenPlayer4_Te3.text = "-1";
        Text_JankenPlayer4_Te4.text = "-1";
        Text_JankenPlayer4_Te5.text = "-1";

        Debug.Log("【JK-32】PlayerTeNum を すべて「-1」に リセットしました");
    }

    public void Reset_MyRireki_All()  // 【JK-30】MyRireki イメージを null にリセット（Image）
    {
        Debug.Log("【JK-30】MyRireki イメージを null にリセット（Image）します");
        MyTeImg_1.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_2.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_3.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_4.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_5.gameObject.GetComponent<Image>().sprite = null;
    }

    public void ResetImg_PlayerlayerRireki_All()  // 【JK-33】Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）
    {
        Debug.Log("【JK-33】Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）");

        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = null;

        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = null;

        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = null;

        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = null;
    }

    #region// ●【JK-07】Num_Player1
    #region// 【JK-07】PlayerTeNum_Player1_1
    public void ToSharePlayerTeNum_Player1_1_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player1_Te1 = 0;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player1_Te1 = 1;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player1_Te1 = 2;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_2
    public void ToSharePlayerTeNum_Player1_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te2 = 0;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te2 = 1;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te2 = 2;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te2.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_3
    public void ToSharePlayerTeNum_Player1_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te3 = 0;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te3 = 1;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te3 = 2;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te3.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_4
    public void ToSharePlayerTeNum_Player1_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te4 = 0;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te4 = 1;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te4 = 2;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te4.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_5
    public void ToSharePlayerTeNum_Player1_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te5 = 0;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te5 = 1;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te5 = 2;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te5.text = "2";
    }
    #endregion
    #endregion

    #region// ●【JK-07】Num_Player2
    #region// 【JK-07】PlayerTeNum_Player2_1
    public void ToSharePlayerTeNum_Player2_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player2_Te1 = 0;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player2_Te1 = 1;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player2_Te1 = 2;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_2
    public void ToSharePlayerTeNum_Player2_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te2 = 0;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te2 = 1;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te2 = 2;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te2.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_3
    public void ToSharePlayerTeNum_Player2_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te3 = 0;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te3 = 1;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te3 = 2;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te3.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_4
    public void ToSharePlayerTeNum_Player2_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te4 = 0;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te4 = 1;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te4 = 2;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te4.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_5
    public void ToSharePlayerTeNum_Player2_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te5 = 0;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te5 = 1;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te5 = 2;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te5.text = "2";
    }
    #endregion
    #endregion

    #region// ●【JK-07】Num_Player3
    #region// 【JK-07】PlayerTeNum_Player3_1
    public void ToSharePlayerTeNum_Player3_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player3_Te1 = 0;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player3_Te1 = 1;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player3_Te1 = 2;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_2
    public void ToSharePlayerTeNum_Player3_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te2 = 0;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te2 = 1;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te2 = 2;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te2.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_3
    public void ToSharePlayerTeNum_Player3_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te3 = 0;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te3 = 1;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te3 = 2;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te3.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_4
    public void ToSharePlayerTeNum_Player3_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te4 = 0;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te4 = 1;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te4 = 2;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te4.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_5
    public void ToSharePlayerTeNum_Player3_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te5 = 0;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te5 = 1;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te5 = 2;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te5.text = "2";
    }
    #endregion
    #endregion

    #region// ●【JK-07】Num_Player4
    #region// 【JK-07】PlayerTeNum_Player4_1
    public void ToSharePlayerTeNum_Player4_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player4_Te1 = 0;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player4_Te1 = 1;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player4_Te1 = 2;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_2
    public void ToSharePlayerTeNum_Player4_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te2 = 0;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te2 = 1;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te2 = 2;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te2.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_3
    public void ToSharePlayerTeNum_Player4_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te3 = 0;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te3 = 1;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te3 = 2;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te3.text = "2";
    }
    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_4
    public void ToSharePlayerTeNum_Player4_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te4 = 0;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te4 = 1;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te4 = 2;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te4.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player4_5
    public void ToSharePlayerTeNum_Player4_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te5 = 0;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te5 = 1;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te5 = 2;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te5.text = "2";
    }
    #endregion
    #endregion


    public void SharePlayerTeNum_Player1()  //【JK-07】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player1_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player1_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player1_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player1_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player1_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player1_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player1_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player1_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player1_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player1_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player1_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player1_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player1_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player1_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player1_5_is_Pa();
        }
        /*
        // int の反映
        int_Player1_Te1 = Int_MyJanken_Te1;
        int_Player1_Te2 = Int_MyJanken_Te2;
        int_Player1_Te3 = Int_MyJanken_Te3;
        int_Player1_Te4 = Int_MyJanken_Te4;
        int_Player1_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player1_Te1.sprite = MyTeImg_1.sprite;
        Img_Player1_Te2.sprite = MyTeImg_2.sprite;
        Img_Player1_Te3.sprite = MyTeImg_3.sprite;
        Img_Player1_Te4.sprite = MyTeImg_4.sprite;
        Img_Player1_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer1_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer1_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer1_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer1_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer1_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SharePlayerTeNum_Player2()  //【JK-07】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player2_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player2_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player2_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player2_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player2_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player2_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player2_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player2_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player2_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player2_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player2_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player2_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player2_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player2_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player2_5_is_Pa();
        }
        /*
        // int の反映
        int_Player2_Te1 = Int_MyJanken_Te1;
        int_Player2_Te2 = Int_MyJanken_Te2;
        int_Player2_Te3 = Int_MyJanken_Te3;
        int_Player2_Te4 = Int_MyJanken_Te4;
        int_Player2_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player2_Te1.sprite = MyTeImg_1.sprite;
        Img_Player2_Te2.sprite = MyTeImg_2.sprite;
        Img_Player2_Te3.sprite = MyTeImg_3.sprite;
        Img_Player2_Te4.sprite = MyTeImg_4.sprite;
        Img_Player2_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer2_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer2_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer2_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer2_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer2_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SharePlayerTeNum_Player3()  //【JK-07】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player3_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player3_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player3_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player3_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player3_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player3_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player3_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player3_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player3_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player3_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player3_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player3_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player3_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player3_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player3_5_is_Pa();
        }
        /*
        // int の反映
        int_Player3_Te1 = Int_MyJanken_Te1;
        int_Player3_Te2 = Int_MyJanken_Te2;
        int_Player3_Te3 = Int_MyJanken_Te3;
        int_Player3_Te4 = Int_MyJanken_Te4;
        int_Player3_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player3_Te1.sprite = MyTeImg_1.sprite;
        Img_Player3_Te2.sprite = MyTeImg_2.sprite;
        Img_Player3_Te3.sprite = MyTeImg_3.sprite;
        Img_Player3_Te4.sprite = MyTeImg_4.sprite;
        Img_Player3_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer3_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer3_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer3_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer3_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer3_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SharePlayerTeNum_Player4()  //【JK-07】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player4_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player4_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player4_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player4_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player4_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player4_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player4_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player4_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player4_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player4_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player4_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player4_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player4_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player4_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player4_5_is_Pa();
        }
        /*
        // int の反映
        int_Player4_Te1 = Int_MyJanken_Te1;
        int_Player4_Te2 = Int_MyJanken_Te2;
        int_Player4_Te3 = Int_MyJanken_Te3;
        int_Player4_Te4 = Int_MyJanken_Te4;
        int_Player4_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player4_Te1.sprite = MyTeImg_1.sprite;
        Img_Player4_Te2.sprite = MyTeImg_2.sprite;
        Img_Player4_Te3.sprite = MyTeImg_3.sprite;
        Img_Player4_Te4.sprite = MyTeImg_4.sprite;
        Img_Player4_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer4_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer4_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer4_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer4_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer4_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SelectGu()     // 【JK-03】手をグーにセット
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_1.text = "0";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_2.text = "0";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_3.text = "0";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_4.text = "0";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_5.text = "0";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        Debug.Log("【JK-03】手をグーにセット");
        PlayerTeNumSet(0);
        Debug.Log("【JK-04】手をグーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectChoki()  // 【JK-03】手をチョキにセット
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_1.text = "1";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_2.text = "1";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_3.text = "1";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_4.text = "1";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_5.text = "1";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(1); //手をチョキにセット
        Debug.Log("【JK-03】手をチョキにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 1);
        PlayerTeNumSet(1);
        Debug.Log("【JK-04】手をチョキにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectPa()     // 【JK-03】手をパーにセット
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_1.text = "2";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_2.text = "2";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_3.text = "2";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_4.text = "2";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_5.text = "2";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(2); //手をパーにセット
        Debug.Log("【JK-03】手をパーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All,2);
        PlayerTeNumSet(2);
        Debug.Log("【JK-04】手をパーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void PlayerTeNumSet(int PTN)  // 【JK-04】私のジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）です。それをセットします。
    {
        Debug.Log("【JK-04】************ ********** *********** **********");
        Debug.Log(PTN + ": PTN");

        Debug.Log("【JK-04】現在自分がジャンケンカードボタン押したよ");
        if (Int_MyJanken_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Debug.Log("【JK-04】Int_MyJanken_Te1 代入前" + Int_MyJanken_Te1);
            Int_MyJanken_Te1 = PTN;
            Debug.Log("【JK-04】Int_MyJanken_Te1 代入後" + Int_MyJanken_Te1);
            Debug.Log("【JK-04】自分_1 手のセットOK");
        }
        else if (Int_MyJanken_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te2 = PTN;
            Debug.Log("【JK-04】自分_2 手のセットOK");
        }
        else if (Int_MyJanken_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te3 = PTN;
            Debug.Log("【JK-04】自分_3 手のセットOK");
        }
        else if (Int_MyJanken_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te4 = PTN;
            Debug.Log("【JK-04】自分_4 手のセットOK");
        }
        else if (Int_MyJanken_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te5 = PTN;
            Debug.Log("【JK-04】自分_5 手のセットOK");
        }
        else
        {
            Debug.Log("【JK-04】現在自分の 5こすべて手が決まったよ");
        }
    }


    #region// 【JK-02】ジャンケンカードボタン 押した時の処理（フラグを処理済みにする）


    public void Push_Btn_A() // 【JK-02】ジャンケンカードボタン押したよ
    {
        Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_A)
        {
            Debug.Log("【JK-02】RndCreateCard_A ： " + ShuffleCardsMSC.RndCreateCard_A);
            if (ShuffleCardsMSC.RndCreateCard_A == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_A.interactable = false;
        CanPushBtn_A = false;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
    }

    public void Push_Btn_B() // 【JK-02】ジャンケンカードボタン押したよ
    {
        Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_B)
        {
            Debug.Log("【JK-02】RndCreateCard_B ： " + ShuffleCardsMSC.RndCreateCard_B);
            if (ShuffleCardsMSC.RndCreateCard_B == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_B.interactable = false;
        CanPushBtn_B = false;
        Check_CanAppear_KetteiBtn();  // ジャンケン手決定ボタンを表示できるか確認
    }

    public void Push_Btn_C() // 【JK-02】ジャンケンカードボタン押したよ
    {
        Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_C)
        {
            Debug.Log("【JK-02】RndCreateCard_C ： " + ShuffleCardsMSC.RndCreateCard_C);
            if (ShuffleCardsMSC.RndCreateCard_C == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_C == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_C == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_C.interactable = false;
        CanPushBtn_C = false;
        Check_CanAppear_KetteiBtn();  // ジャンケン手決定ボタンを表示できるか確認
    }

    public void Push_Btn_D() // 【JK-02】ジャンケンカードボタン押したよ
    {
        Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_D)
        {
            Debug.Log("【JK-02】RndCreateCard_D ： " + ShuffleCardsMSC.RndCreateCard_D);
            if (ShuffleCardsMSC.RndCreateCard_D == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_D.interactable = false;
        CanPushBtn_D = false;
        Check_CanAppear_KetteiBtn();  // ジャンケン手決定ボタンを表示できるか確認
    }

    public void Push_Btn_E() // 【JK-02】ジャンケンカードボタン押したよ
    {
        Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_E)
        {
            Debug.Log("【JK-02】RndCreateCard_E ： " + ShuffleCardsMSC.RndCreateCard_E);
            if (ShuffleCardsMSC.RndCreateCard_E == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_E.interactable = false;
        CanPushBtn_E = false;
        Check_CanAppear_KetteiBtn();  // ジャンケン手決定ボタンを表示できるか確認
    }

    #endregion


    #region// 【JK-02】ジャンケンカードボタン を押せるようにする(フラグのリセット）

    public void ToCanPush_All()   //【JK-02】 【JK-31】じゃんけんカードボタン を押せるようにする(フラグのリセット）（bool）
    {
        Debug.Log("【JK-02】【JK-31】すべてのジャンケンカードボタン を押せるようにします(フラグのリセット）");
        ToCanPush_A();
        ToCanPush_B();
        ToCanPush_C();
        ToCanPush_D();
        ToCanPush_E();
    }

    public void ToCanPush_A() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_A.interactable = true;
        CanPushBtn_A = true;
    }

    public void ToCanPush_B() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_B.interactable = true;
        CanPushBtn_B = true;
    }

    public void ToCanPush_C() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_C.interactable = true;
        CanPushBtn_C = true;
    }

    public void ToCanPush_D() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_D.interactable = true;
        CanPushBtn_D = true;
    }

    public void ToCanPush_E() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_E.interactable = true;
        CanPushBtn_E = true;
    }

    #endregion

    #region// 【JK-04】じゃんけん手ナンバー リセット

    public void ResetMyNumTe_All()  // 【JK-04】【JK-29】数値を -1 にリセット（int,text）
    {
        count_a = 1;

        MyNumTeText_1.text = "-1";
        MyNumTeText_2.text = "-1";
        MyNumTeText_3.text = "-1";
        MyNumTeText_4.text = "-1";
        MyNumTeText_5.text = "-1";

        Int_MyJanken_Te1 = -1;
        Int_MyJanken_Te2 = -1;
        Int_MyJanken_Te3 = -1;
        Int_MyJanken_Te4 = -1;
        Int_MyJanken_Te5 = -1;

        Debug.Log("【JK-04】【JK-29】自分のジャンケン手をリセットしました");
    }
    #endregion





    public void FromWin_ToJump()     //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
    {
        Debug.Log("【JK-107】FromWin_ToJump （ジャンプ移動）処理に入ります");
        Set_StepNum();               //【JK-108】ジャンプする回数を設定する（変数上書き） 
        bridge_JumpToRight();        //【JK-109】右方向へ 指定された回数 ぴょん と跳ねながら移動する
    }

    public void Set_StepNum()        //【JK-108】ジャンプする回数を設定する（変数上書き） 
    {
        Debug.Log("【JK-108】ジャンプする回数を設定（変数上書き）します");
        PlayerSC.MoveForward_StepNum = original_StepNum;   // ジャンプして移動するステップ数（の元となる変数）に上書きする
    }

    public void bridge_JumpToRight()  //【JK-109】右方向へ 指定された回数 ぴょん と跳ねながら移動する
    {
        Debug.Log("【JK-109】bridge_JumpToRight（ジャンプ移動） を実行します");
        PlayerSC.JumpRight();
        Debug.Log("【JK-110】ぴょーん！ ぴょーん！ ぴょーん！");
    }

    public void bridge_GetDamage()
    {
        Debug.Log("bridge_GetDamage を実行します");
        PlayerSC.receivedDammage();
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
    // オーナーの場合
    if (stream.IsWriting)
    {
        stream.SendNext(int_Player1_Te1);
        stream.SendNext(int_Player2_Te1);
        if (TestRoomControllerSC.allPlayers.Length >= 3)
        {
            stream.SendNext(int_Player3_Te1);
        }
        if (TestRoomControllerSC.allPlayers.Length >= 4)
        {
            stream.SendNext(int_Player4_Te1);
        }
    }
    // オーナー以外の場合
    else
    {
        this.receiveint_Player1_Te1 = (int)stream.ReceiveNext();
        SelectJankenMSC.int_Player1_Te1 = receiveint_Player1_Te1;
        this.receiveint_Player2_Te1 = (int)stream.ReceiveNext();
        SelectJankenMSC.int_Player2_Te1 = receiveint_Player2_Te1;
        if (TestRoomControllerSC.allPlayers.Length >= 3)
        {
            this.receiveint_Player3_Te1 = (int)stream.ReceiveNext();
            SelectJankenMSC.int_Player3_Te1 = receiveint_Player3_Te1;
        }
        if (TestRoomControllerSC.allPlayers.Length >= 4)
        {
            this.receiveint_Player4_Te1 = (int)stream.ReceiveNext();
            SelectJankenMSC.int_Player4_Te1 = receiveint_Player4_Te1;
        }
    }
    */
    }

    private void RequestOwner()
    {
        if (this.photonView.IsMine == false)
        {
            if (this.photonView.OwnershipTransfer != OwnershipOption.Request)
                Debug.LogError("OwnershipTransferをRequestに変更してください。");
            else
                this.photonView.RequestOwnership();
        }
    }

    public void BackTo_TitleScene() // タイトル画面へ戻ります
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Launcher");
    }

    public void BackTo_LobbyScene() // ロビー画面へ戻ります
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }


    /*
    [PunRPC]
    public void CheckPlayerTeNum()
    {
        Debug.Log("************ CheckPlayerTeNum *********** **********");
        Debug.Log("***  Player1  ***********");
        Debug.Log("int_Player1_Te1 " + int_Player1_Te1);
        Debug.Log("int_Player1_Te2 " + int_Player1_Te2);
        Debug.Log("int_Player1_Te3 " + int_Player1_Te3);
        Debug.Log("int_Player1_Te4 " + int_Player1_Te4);
        Debug.Log("int_Player1_Te5 " + int_Player1_Te5);
        Debug.Log("***  Player2  ***********");
        Debug.Log("int_Player2_Te1 " + int_Player2_Te1);
        Debug.Log("int_Player2_Te2 " + int_Player2_Te2);
        Debug.Log("int_Player2_Te3 " + int_Player2_Te3);
        Debug.Log("int_Player2_Te4 " + int_Player2_Te4);
        Debug.Log("int_Player2_Te5 " + int_Player2_Te5);
        Debug.Log("***  Player3  ***********");
        Debug.Log("int_Player3_Te1 " + int_Player3_Te1);
        Debug.Log("int_Player3_Te2 " + int_Player3_Te2);
        Debug.Log("int_Player3_Te3 " + int_Player3_Te3);
        Debug.Log("int_Player3_Te4 " + int_Player3_Te4);
        Debug.Log("int_Player3_Te5 " + int_Player3_Te5);
        Debug.Log("***  Player4  ***********");
        Debug.Log("int_Player4_Te1 " + int_Player4_Te1);
        Debug.Log("int_Player4_Te2 " + int_Player4_Te2);
        Debug.Log("int_Player4_Te3 " + int_Player4_Te3);
        Debug.Log("int_Player4_Te4 " + int_Player4_Te4);
        Debug.Log("int_Player4_Te5 " + int_Player4_Te5);
    }
    public void OnMouseDown()
    {
        // photonView.RPC("SelectJankenCard", RpcTarget.All);
    }
    public void MyNameIs(Player player)
    {
        Debug.Log("私の名前は 「" + player.NickName + " 」でござる");
    }
    */


    // End

}