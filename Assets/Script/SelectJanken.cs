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

    bool CreatePlayerPrefab_Flg = true;

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

    public GameObject Panel_Intro;

    public GameObject GoalCorn_Head;  // ゴールラインのコーン
    public GameObject GameSet_LOGO;
    public GameObject OpenMyJankenPanel_Button;   // 右上の開始ボタン
    public GameObject Debug_Buttons;  // デバッグ用のボタン
    public GameObject WinPanel;       // 優勝者決定後のパネル

    public GameObject Winner_avator_1;
    public GameObject Winner_avator_2;
    public GameObject Winner_avator_3;
    public GameObject Winner_avator_4;

    Transform MyKage_Trans;  // かげの位置情報 (Transform)
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

    float KageDistance;      // かげとMyPlayerとの距離情報 (Vector3)
    Vector3 PosMyKage;         // かげの位置情報 (Vector3)
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
    public Text Text_RoomName;
    public Text Text_WinnerName;

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

    public int KP1 = -1;                     // ジャンケン勝敗判定（Hantei_Stream）に使う、仮の値 （0：グー、1：チョキ、2：パー）
    public int KP2 = -1;
    public int KP3 = -1;
    public int KP4 = -1;

    public int int_WaitingPlayers_All = 0;       // 現在待機中の総人数
    public int int_IamNowWaiting = 0;        // 待機中フラグ 「0：まだ決定してない（待機まえ）」、「1：決定済み（待機中）」
    public int int_NowWaiting_Player1 = 0;   // ジャンケン手 決定して待機中かどうか （0：まだ決定してない、1：決定して待機中）
    public int int_NowWaiting_Player2 = 0;
    public int int_NowWaiting_Player3 = 0;
    public int int_NowWaiting_Player4 = 0;

    public int Iam_alive = 1;
    public int alivePlayer1 = 1;            // ジャンケンで残留してれば 1 、負けたら 0
    public int alivePlayer2 = 1;
    public int alivePlayer3 = 1;
    public int alivePlayer4 = 1;

    public int SankaNinzu = 0;              // 総参加人数
    //int anzenPoint = 0;
    public int WinnerNum = -1;              // 勝ったプレイヤーの番号

    public int original_StepNum;            // ジャンプして移動するステップ数（の元となる変数）

    public int int_MatchPlayerMaxNum = 4;   // このルームの最大プレイヤー人数（4人 / 10人 / 20人）
    public int int_conMyCharaAvatar = 0;    // ログイン前に選んだキャラクターのアバター番号

    [SerializeField]
    public int count_a = 1;
    public int NumLivePlayer = 0;           // 残りのプレイヤー人数
    public int count_RoundRoop = 1;         // ジャンケン勝ち負け判定のループ回数（現在何ラウンド目のループか？）

    public bool NoneGu = false;
    public bool NoneChoki = false;
    public bool NonePa = false;
    public bool bool_CanDo_Hantei_Stream = false;  // 勝敗判定（Hantei_Stream） を実行できるかのフラグ

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
    public string AcutivePlayerName = "me";
    public string AcutivePlayerID = "0000";

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
    public Button Btn_Omakase;

    public bool CanPushBtn_A = true;
    public bool CanPushBtn_B = true;
    public bool CanPushBtn_C = true;
    public bool CanPushBtn_D = true;
    public bool CanPushBtn_E = true;
    public bool CanPushBtn_Omakase = true;

    public GameObject Text_WaitingPlayers_All;
    public GameObject Text_Wait_Me; // test
    public GameObject Text_Wait_P1;
    public GameObject Text_Wait_P2;
    public GameObject Text_Wait_P3;
    public GameObject Text_Wait_P4;
    public GameObject Text_NickName;
    public GameObject Text_AcutivePlayerName;

    public float JumpMaeTaiki = 2.0f;
    float span = 0.5f;
    private float currentTime = 0f; // test

    public GameObject BGM_SE_Manager;
    BGM_SE_Manager BGM_SE_MSC;

    public Text text_Game_kaishi_MAE;
    public Text text_Game_kaishi_CHU;
    public Text text_Room_shimekiri;
    public bool Shiai_Kaishi = false; // 初期設定は 試合開始できない（試合前）

    public int int_Ikemasu_All = 0;       // 現在「試合開始、いけます！」な人の総人数
    public int int_Iam_Ikemasu = 0;        // 「試合開始、いけます！」の表明フラグ 「0：まだいけない」、「1：いけます！」
    public int int_Ikemasu_Player1 = 0;
    public int int_Ikemasu_Player2 = 0;
    public int int_Ikemasu_Player3 = 0;
    public int int_Ikemasu_Player4 = 0;

    public GameObject Panel_Ikemasu;
    public GameObject StartLogo;

    public GameObject Aisatsu_Panel;
    public Text text_AisatsuP1;           // P1のあいさつ文
    public Text text_AisatsuP2;           // P2のあいさつ文
    public Text text_AisatsuP3;           // P3のあいさつ文
    public Text text_AisatsuP4;           // P4のあいさつ文

    public Text Text_Announcement;        // アナウンス文
    public bool Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
    public bool Countdown_Push_JankenTe_KetteiButton_Flg = false;
    public bool Push_JankenTe_KetteiButton_Flg = false;

    int Countdown_timer_PanelOpen = 1;

    int Countdown_timer_Kettei = 1;
    public bool GameSet_Flg = false;      // ゲームセットしたかのフラグ
    public string str_AisatsuBun = "";           // あいさつ文の中身

    //private AudioSource audioSource = null;

    //public AudioClip FunAndLight;   // Battle シーンBGM
    //public AudioClip whistle;
    //public AudioClip Fanfare_solo;
    //public AudioClip Fanfare_Roop;
    //public UnityEngine.UI.Slider volSlider;

    public GameObject SubCamera_Group;
    public float PosX_Winner;
    public float Kari_PosX_Winner;
    public GameObject SubCamera;
    Transform SubCamera_Trans;    // SubCamera の位置情報 (Transform)
    Vector3 PosSubCamera;         // SubCamera の位置情報 (Vector3)

    bool ToRight_SubCamera = false;    //  右ボタンを押しているかの真偽値
    bool ToLeft_SubCamera = false;     //  左ボタンを押しているかの真偽値

    public GameObject cafe_kanban_035;
    public GameObject cafe_kanban_025;
    public GameObject cafe_kanban_015;
    public GameObject cafe_kanban_005;
    public GameObject cafe_kanban_0_5;
    Transform cafe_kanban_0_5_Trans;    // 位置情報 (Transform)
    Vector3 Poscafe_kanban_0_5;         // 位置情報 (Vector3)

    public GameObject Taiki_OK_P1;
    public GameObject Taiki_OK_P2;
    public GameObject Taiki_OK_P3;
    public GameObject Taiki_OK_P4;
    #endregion

    #region // 【START】初期設定の処理一覧

    void Awake()
    {
        BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
        BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();

        this.photonView = GetComponent<PhotonView>();
        Debug.Log("【START-01】SelectJanken void Awake() 出席確認1");
        Debug.Log("【START-01】キャラ アバター 誰を選んだかを（ログイン前画面から）コンバートします");
        int_conMyCharaAvatar = CLauncherScript.get_int_MyCharaAvatar(); // 【START-01】キャラ アバター 誰を選んだか（ログイン前画面からコンバートする）

        Debug.Log("【START-02】int_conMyCharaAvatar（★キャラアバター） ： " + int_conMyCharaAvatar);

        Debug.Log("【START-03】他スクリプトと連携できるようにします");
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
        TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
        MyCameraControllerMSC = MainCamera.GetComponent<MyCameraController>();
        CloseWinPanel();
        CloseDebug_Buttons();
        ClosePanel_Intro();
        if (BGM_SE_MSC.firstMatch <= 3)
        {
            AppearPanel_Intro();
        }
        text_Game_kaishi_MAE.text = "しあい かいし まえ";
        text_Game_kaishi_CHU.text = "";
        //audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        //var customProperties = photonView.Owner.CustomProperties;
        Debug.Log("【START-03】SelectJanken  void Start() 出席確認2");

        myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");

        Debug.Log("【START-03】 各変数を 初期化（リセット）します");
        count_a = 1;
        Debug.Log("【START-03】 各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする");
        ResetAlivePlayer();            //【START-03】 各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする

        Debug.Log("【START-04】自プレイヤーを生成します");
        //CreatePlayerPrefab();          //【START-04】Photonに接続していれば自プレイヤーを生成
        Debug.Log("CreatePlayerPrefab_Flg ： " + CreatePlayerPrefab_Flg);

        /*
        if (BGM_SE_MSC.firstRead_Selectjanken == 0)
        {
            Debug.Log("firstRead_Selectjanken  0 です");
            Debug.Log("myPlayer が 存在していなかったのでキャラ作成します！！！");
            //Debug.Log("フラグON だったのでキャラ作成します！！！");
            CreatePlayerPrefab();          //【START-04】Photonに接続していれば自プレイヤーを生成
            CreatePlayerPrefab_Flg = false;
            BGM_SE_MSC.firstRead_Selectjanken = 1;
        }
        else
        {
            Debug.Log("firstRead_Selectjanken  1 です。CreatePlayerPrefab 処理はしません");
        }
        */

        Debug.Log("myPlayer が 存在していなかったのでキャラ作成します！！！");
        //Debug.Log("フラグON だったのでキャラ作成します！！！");
        CreatePlayerPrefab();          //【START-04】Photonに接続していれば自プレイヤーを生成
        CreatePlayerPrefab_Flg = false;
        BGM_SE_MSC.firstRead_Selectjanken = 1;

        myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");

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

        Debug.Log("【START-12】右上の開始ボタンを押せるように各値をリセット ⇒ 全員に共有する");
        ShareAfterJump();   //【START-12】右上の開始ボタンを押せるように各値をリセット ⇒ 全員に共有する

        BGM_SE_MSC.FunAndLight_BGM();      // Battle シーンBGM
        CloseStartLogo();
        CloseAisatsu_Panel();
        Reset_AllAisatsu();
        //Erase_Text_Announcement();
        AppearPanel_Ikemasu();
        //ClosePanel_Ikemasu();
        CheckStart_GameMatch();                 // 試合開始できるか確認する処理
                                                //var sequence = DOTween.Sequence();
                                                //sequence.InsertCallback(5f, () => AppearPanel_Ikemasu());
                                                //MoveTo_cafe_kanban_0_5();   // SubCamera を -5 の位置に移動する
        CloseSubCamera_Group();
    }


    void Update()
    {
        // test
        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            //Debug.LogFormat("{0}秒経過", span);
            //Debug.LogFormat("待機中合計：" + int_WaitingPlayers_All + "人");
            /*
            Text_WaitingPlayers_All.GetComponent<Text>().text = "待機中合計："+int_WaitingPlayers_All + "人";
            Text_Wait_Me.GetComponent<Text>().text = int_NowWaiting_Player1 + "";
            Text_Wait_P1.GetComponent<Text>().text = int_NowWaiting_Player1 + "";
            Text_Wait_P2.GetComponent<Text>().text = int_NowWaiting_Player2 + "";
            Text_Wait_P3.GetComponent<Text>().text = int_NowWaiting_Player3 + "";
            Text_Wait_P4.GetComponent<Text>().text = int_NowWaiting_Player4 + "";
            
            Debug.Log("alivePlayer1 ： " + alivePlayer1);
            Debug.Log("alivePlayer2 ： " + alivePlayer2);
            Debug.Log("alivePlayer3 ： " + alivePlayer3);
            Debug.Log("alivePlayer4 ： " + alivePlayer4);

            Text_NickName.GetComponent<Text>().text = PhotonNetwork.NickName + "";
            Text_AcutivePlayerName.GetComponent<Text>().text = AcutivePlayerName + "";
            */
            //Debug.Log("PhotonNetwork.CurrentRoom.Name ： " + PhotonNetwork.CurrentRoom.Name);

            // Debug.Log("このルームに入れるかどうか："+ PhotonNetwork.CurrentRoom.IsOpen);
            if(Shiai_Kaishi)
            {
                text_Room_shimekiri.text = "試合開始したので、ルームへの入室をしめきりました";
            }

            if (bool_CanDo_Hantei_Stream)       // 勝敗判定（Hantei_Stream） を実行できるかのフラグ（CanDoフラグ ON）
            {
                Debug.Log("[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●");
                //Debug.Log("[Update] CanDoフラグ が ON であるならば、速やかに Check_Can_Hantei_Stream を実行します");
                Debug.Log("[Update] 全員待機中です。CanDoフラグ が ON であるので、速やかに 勝敗判定（Hantei_Stream） をローカル実行します");
                Debug.Log("[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●[Update] ●");
                //Check_Can_Hantei_Stream();      //【JK-12】勝敗判定（Hantei_Stream）フェーズへ進めるか確認する
                Hantei_Stream();      // 【JK-21】ジャンケン勝敗判定（Hantei_Stream）（ラウンドループ）ローカル実施 ⇒ 勝ったプレイヤー1名のみジャンプで前進する
            }

            if (Shiai_Kaishi == false)  // 試合開始まえであれば、判定処理を実施する（試合中は判定する必要なし）
            {
                if (int_Ikemasu_Player1 == 0)  // 待機まえ：まだ「試合開始いけます」ボタンを押してません
                {
                    CloseTaiki_OK_P1();        // 「OK」マーカーを閉じる
                }
                else                           // 待機中（いけます！）
                {
                    AppearTaiki_OK_P1();       // 「OK」マーカーを表示する
                }

                if (int_Ikemasu_Player2 == 0)  // 待機まえ
                {
                    CloseTaiki_OK_P2();
                }
                else                          // 待機中
                {
                    AppearTaiki_OK_P2();
                }

                if (int_Ikemasu_Player3 == 0)  // 待機まえ
                {
                    CloseTaiki_OK_P3();
                }
                else                          // 待機中
                {
                    AppearTaiki_OK_P3();
                }

                if (int_Ikemasu_Player4 == 0)  // 待機まえ
                {
                    CloseTaiki_OK_P4();
                }
                else                          // 待機中
                {
                    AppearTaiki_OK_P4();
                }
            }
            else  // 試合開始後（試合中）
            {
                if (int_NowWaiting_Player1 == 0)  // 待機まえ：まだジャンケン手 決めてません
                {
                    CloseTaiki_OK_P1();
                }
                else                              // 待機中（決定ボタン押下済み）
                {
                    AppearTaiki_OK_P1();
                }

                if (int_NowWaiting_Player2 == 0)  // 待機まえ
                {
                    CloseTaiki_OK_P2();
                }
                else                              // 待機中（決定ボタン押下済み）
                {
                    AppearTaiki_OK_P2();
                }

                if (int_NowWaiting_Player3 == 0)  // 待機まえ
                {
                    CloseTaiki_OK_P3();
                }
                else                              // 待機中（決定ボタン押下済み）
                {
                    AppearTaiki_OK_P3();
                }

                if (int_NowWaiting_Player4 == 0)  // 待機まえ
                {
                    CloseTaiki_OK_P4();
                }
                else                              // 待機中（決定ボタン押下済み）
                {
                    AppearTaiki_OK_P4();
                }
            }

            currentTime = 0f;
        }

        if (ToRight_SubCamera)
        {
            SubCamera_GoRight();            // 右に動かすためのメソッドを呼び出す
        }
        else if (ToLeft_SubCamera)
        {
            SubCamera_GoLeft();            // 左に動かすためのメソッドを呼び出す
        }
        else
        {
            //          ボタンを押していない時
            // SubCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
            //          プレイヤーを元の角度に戻す
        }
        // Debug.LogFormat("待機中合計：" + int_WaitingPlayers_All + "人");
    }

    public void CreatePlayerPrefab()    //【START-04】Photonに接続していれば自プレイヤーを生成
    {
        Debug.Log("【START-04】Photonに接続したので 自プレイヤーを生成");

        //GameObject MyPlayer = PhotonNetwork.Instantiate(this.MyPlayerPrefab.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
        //GameObject MyPlayer = PhotonNetwork.Instantiate(this.MyPlayerPrefab.name);
        //GameObject Player1 = PhotonNetwork.Instantiate(this.playerPrefab.name);

        if (int_conMyCharaAvatar == 1)  // うたこ
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_utako.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            utakoClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("utakoClone の名前は: " + utakoClone.name);
            PlayerSC = utakoClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 2) // Unityちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_unitychan.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            unitychanClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("unitychanClone の名前は: " + unitychanClone.name);
            PlayerSC = unitychanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 3) // Pちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_pchan.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            pchanClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("pchanClone の名前は: " + pchanClone.name);
            PlayerSC = pchanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 4) // モブちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_mobuchan.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            mobuchanClone = GameObject.FindWithTag("MyPlayer");
            Debug.Log("mobuchanClone の名前は: " + mobuchanClone.name);
            PlayerSC = mobuchanClone.GetComponent<PlayerScript>();
        }
        else
        {
            Debug.Log("【START-04】自プレイヤーを生成 できませんでした");
        }
    }


    #region // 【START-07】Battleシーン遷移後、初期設定・配置の処理一覧（アイコンのセット等）
    public void ToShare_InitialSetting()    // 【START-07】スタート時 初期設定を全プレイヤーで共有
    {
        //TestRoomControllerSC.PNameCheck();  // 現在の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Share_AcutivePlayerID();            // 【START-07】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
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
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("AcutivePlayerName  " + AcutivePlayerName);
        Debug.Log("AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Debug.Log("【START-07】プレイヤー1のアイコンをセットします");
            SharePlayerIcon_Player1();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark1();                // 【START-07】スタートマーク1 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 1;    // order in layer の順番調整に使用する整数
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Debug.Log("【START-07】プレイヤー2のアイコンをセットします");
            SharePlayerIcon_Player2();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark2();                // 【START-07】スタートマーク2 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 2;    // order in layer の順番調整に使用する整数
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Debug.Log("【START-07】プレイヤー3のアイコンをセットします");
            SharePlayerIcon_Player3();
            Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark3();                // 【START-07】スタートマーク3 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 3;    // order in layer の順番調整に使用する整数
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
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
        Text_MyPName_SelectPanel.text = AcutivePlayerName;  // SelectPanel にAcutivePlayerName をセット
        Text_Head_MyPName.text = AcutivePlayerName;         // 画面上部 にAcutivePlayerName をセット
        SetMyIcon_SelectPanel();                 //【START-07】私のアイコンをセレクトパネルにセットします
        Text_RoomName.text = PhotonNetwork.CurrentRoom.Name;         // 画面上部 に Text_RoomName をセット
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

    public void Share_AcutivePlayerID() // 【START-07】【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    {
        // TestRoomControllerSC.PNameCheck();
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
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
            AcutivePlayerName = senderName;
            AcutivePlayerID = senderID;
        }
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        //Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("AcutivePlayerName  " + AcutivePlayerName);  // 今ボタン押した人
        //Debug.Log("AcutivePlayerID  " + AcutivePlayerID);  // 今ボタン押した人
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

    public void NinzuCheck()                          // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
    {
        Debug.Log("【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します");
        TestRoomControllerSC.PNameCheck();            // 現在の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        SankaNinzu = TestRoomControllerSC.int_JoinedPlayerAllNum;  // 総参加人数 を更新
        int_WaitingPlayers_All = int_NowWaiting_Player1 + int_NowWaiting_Player2 + int_NowWaiting_Player3 + int_NowWaiting_Player4;   // 現在待機中の総人数 を更新
        Debug.Log("総参加人数（SankaNinzu） ： " + SankaNinzu);
        Debug.Log("現在待機中の総人数（int_WaitingPlayers_All） ： " + int_WaitingPlayers_All);
    }

    public void AllPlayerWaiting_Forcibly()
    {
        Debug.Log("強制的に全員の待機フラグを 1 （待機中）にします");
        int_WaitingPlayers_All = SankaNinzu;
    }


    public void ToShareNinzu_2()   // 参加人数 2名 は居る → 全員に共有
    {
        Debug.Log("ToShare 参加人数 2名 は居る");
        photonView.RPC("ShareNinzu_2", RpcTarget.All);
    }

    public void ToShareNinzu_3()   // 参加人数 3名 は居る → 全員に共有
    {
        Debug.Log("ToShare 参加人数 3名 は居る");
        photonView.RPC("ShareNinzu_3", RpcTarget.All);
    }

    public void ToShareNinzu_4()   // 参加人数 4名 は居る → 全員に共有
    {
        Debug.Log("ToShare 参加人数 4名 は居る");
        photonView.RPC("ShareNinzu_4", RpcTarget.All);
    }

    [PunRPC]
    public void ShareNinzu_2()   // 参加人数 2名 は居る → 全員に共有
    {
        Debug.Log("参加人数 2名 は居る → 全員に共有");
        NumLivePlayer = 2;
        SankaNinzu = 2;
    }

    [PunRPC]
    public void ShareNinzu_3()   // 参加人数 3名 は居る → 全員に共有
    {
        Debug.Log("参加人数 3名 は居る → 全員に共有");
        NumLivePlayer = 3;
        SankaNinzu = 3;
    }

    [PunRPC]
    public void ShareNinzu_4()   // 参加人数 4名 は居る → 全員に共有
    {
        Debug.Log("参加人数 4名 は居る → 全員に共有");
        NumLivePlayer = 4;
        SankaNinzu = 4;
    }
    #endregion

    #endregion

    #region // 右上の「開始ボタン」を自動で押す処理

    public void Countdown_Until_Push_OpenMyJankenPanel_Button()   // ジャンケンパネルが開かれていないならば、カウントダウン開始
    {
        Debug.Log("Countdown_timer_PanelOpen : " + Countdown_timer_PanelOpen);
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)  // ジャンケンパネルが既に表示されていたら
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;  // ボタンフラグをOFFにする
            Erase_Text_Announcement();
        }
        Debug.Log("右上の「開始ボタン」を自動で押す:ジャンケンパネルが開かれていない（アクティブでない）ならば、カウントダウン開始");
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg && GameSet_Flg == false)
        {
            Debug.Log("右上の「開始ボタン」を自動で押す:ジャンケンパネルが開かれていないので、ボタンを おしてね Text");
            Text_Announcement.text = "ボタンを おしてね";   // テキスト表示

            if (Countdown_timer_PanelOpen == 1)             // ボタンを おしてね のSEを流す
            {
                var sequence5 = DOTween.Sequence();
                sequence5.InsertCallback(5f, () => Checking_PanelOpen(5));
            }
            else if (Countdown_timer_PanelOpen == 2)
            {
                var sequence10 = DOTween.Sequence();
                sequence10.InsertCallback(5f, () => Checking_PanelOpen(10));
            }
            else if (Countdown_timer_PanelOpen == 3)
            {
                var sequence15 = DOTween.Sequence();
                sequence15.InsertCallback(5f, () => Checking_PanelOpen(15));
            }
            else if (Countdown_timer_PanelOpen == 4)       // ボタンを おしてね のSEを流す
            {
                var sequence20 = DOTween.Sequence();
                sequence20.InsertCallback(5f, () => Checking_PanelOpen(20));
            }
            else if (Countdown_timer_PanelOpen == 5)
            {
                var sequence25 = DOTween.Sequence();
                sequence25.InsertCallback(5f, () => Checking_PanelOpen(25));
            }
            else if (Countdown_timer_PanelOpen == 6)
            {
                var sequence30 = DOTween.Sequence();
                sequence30.InsertCallback(5f, () => Auto_Push_OpenMyJankenPanel_Button());
            }
            else
            {
                Debug.Log("右上「開始ボタン」の Countdown_timer_PanelOpen が 1～6 以外です");
                ResetCountdown_timer_PanelOpen_1();
            }
        }
        else
        {
            Debug.Log("右上「開始ボタン」を自動で押す:既にジャンケンパネルが開かれているようです・・・");
        }
    }

    public void ResetCountdown_timer_PanelOpen_1()
    {
        Countdown_timer_PanelOpen = 1;
    }

    public void Checking_PanelOpen(int timeHyoji)
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        Debug.Log("右上「開始ボタン」のカウントダウン：" + timeHyoji);
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg && GameSet_Flg == false)
        {
            if(timeHyoji == 5 || timeHyoji == 20)
            {
                bottonwo_oshitene_SE();   // ボタンを おしてね のSEを流す
            }
            Countdown_timer_PanelOpen++;
            Countdown_Until_Push_OpenMyJankenPanel_Button();
        }
        else
        {
            Debug.Log("右上「開始ボタン」のカウントダウン、ここで中断です・・");
            ResetCountdown_timer_PanelOpen_1();
        }
    }

    public void bottonwo_oshitene_SE()
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg && GameSet_Flg == false)
        {
            Debug.Log("右上「開始ボタン」を自動で押す: ボタンを おしてね SE を流します");
            BGM_SE_MSC.bottonwo_oshitene_SE();         // ボタンを おしてね のSEを流す
        }
        else
        {
            Debug.Log("右上「開始ボタン」を自動で押す → ボタンを おしてね SE を流す条件を満たしていません・・・");
        }
    }

    public void Auto_Push_OpenMyJankenPanel_Button()   // ジャンケンパネルが開かれていないならば、右上の開始ボタンを自動で押す
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg && GameSet_Flg == false)
        {
            Debug.Log("右上の「開始ボタン」を自動で押す:ジャンケンパネルが開かれていないので、開始ボタンを おします Auto");
            PushOpenMyJankenPanel_Button();            // 右上の開始ボタンを自動で押す
        }
        else
        {
            Debug.Log("右上の「開始ボタン」を自動で押す → 開始ボタンを押す 条件を満たしていません・・・");
        }
        ResetCountdown_timer_PanelOpen_1();
    }

    public void Erase_Text_Announcement()
    {
        Text_Announcement.text = "";                   // アナウンス テキスト文をリセットする
    }

    #endregion


    #region // ジャンケン手「決定ボタン」を自動で押す処理

    public void Countdown_Until_Push_JankenTe_KetteiButton()   // ジャンケンパネルが開かれていて、決定ボタンか押されていならば、カウントダウン開始
    {
        Debug.Log("Countdown_timer_Kettei : " + Countdown_timer_Kettei);

        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        else
        {
            Countdown_Push_JankenTe_KetteiButton_Flg = false;
        }
        Debug.Log("ジャンケンパネルが開かれていて、決定ボタンか押されていないならば、カウントダウン開始します");
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg)
        {
            Debug.Log("ジャンケンパネルが開かれていて、決定ボタンか押されていないので、カウントダウン開始しました");
            Debug.Log("カードを選んでね");

            if (Countdown_timer_Kettei == 1)
            {
                var sequence5 = DOTween.Sequence();
                sequence5.InsertCallback(5f, () => Countdown_KetteiButton_Hyoji(5));
            }
            else if (Countdown_timer_Kettei == 2)
            {
                var sequence10 = DOTween.Sequence();
                sequence10.InsertCallback(5f, () => Countdown_KetteiButton_Hyoji(10));
            }
            else if (Countdown_timer_Kettei == 3)
            {
                var sequence15 = DOTween.Sequence();
                sequence15.InsertCallback(5f, () => Countdown_KetteiButton_Hyoji(15));
            }
            else if (Countdown_timer_Kettei == 4)
            {
                var sequence20 = DOTween.Sequence();
                sequence20.InsertCallback(5f, () => Countdown_KetteiButton_Hyoji(20));
            }
            else if (Countdown_timer_Kettei == 5)
            {
                var sequence25 = DOTween.Sequence();
                sequence25.InsertCallback(5f, () => Countdown_KetteiButton_Hyoji(25));
            }
            else if (Countdown_timer_Kettei == 6)
            {
                var sequence = DOTween.Sequence();
                sequence.InsertCallback(5f, () => Auto_Push_Omakase_Button());
            }
            else
            {
                Debug.Log("「決定ボタン」の Countdown_timer_Kettei が 1～6 以外です");
                ResetCountdown_timer_Kettei_1();
            }
            //var sequenc3 = DOTween.Sequence();
            //sequenc3.InsertCallback(35f, () => korede_iikana_SE());

            //var sequence2 = DOTween.Sequence();
            //sequence2.InsertCallback(45f, () => Auto_Push_JankenTe_KetteiButton());
        }
        else
        {
            Debug.Log("「決定ボタン」を自動で押す:ジャンケンパネル閉じているようです・・・");
        }
    }

    public void ResetCountdown_timer_Kettei_1()
    {
        Countdown_timer_Kettei = 1;
    }

    public void Countdown_KetteiButton_Hyoji(int timeHyoji)   // ジャンケンパネルが開かれていて、決定ボタンか押されていならば、カウントダウン開始
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        else
        {
            Countdown_Push_JankenTe_KetteiButton_Flg = false;
        }
        Debug.Log("決定ボタンのカウントダウン：" + timeHyoji);
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg)
        {
            Countdown_timer_Kettei++;
            Countdown_Until_Push_JankenTe_KetteiButton();
        }
        else
        {
            Debug.Log("「決定ボタン」のカウントダウン、ここで中断です・・");
            ResetCountdown_timer_Kettei_1();
        }
    }

    public void Auto_Push_Omakase_Button()           // おまかせボタンを押す
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        else
        {
            Countdown_Push_JankenTe_KetteiButton_Flg = false;
        }
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg)
        {
            Debug.Log("決定ボタンか押されていないので、おまかせボタンを自動で押す Auto");
            PushBtn_Omakase();                      // おまかせボタンを自動で押す

            var sequenc3 = DOTween.Sequence();
            sequenc3.InsertCallback(5f, () => korede_iikana_SE());
        }
        else
        {
            Debug.Log("「決定ボタン」を自動で押す → おまかせボタンを自動で押す 条件を満たしていません・・・");
        }
        ResetCountdown_timer_Kettei_1();
    }

    public void korede_iikana_SE()   // これでいいかな？ SE
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        else
        {
            Countdown_Push_JankenTe_KetteiButton_Flg = false;
        }
        if (ShuffleCardsMSC.KetteiBtn.activeSelf)
        {
            Push_JankenTe_KetteiButton_Flg = true;
        }
        else
        {
            Push_JankenTe_KetteiButton_Flg = false;
        }
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg && Push_JankenTe_KetteiButton_Flg)
        {
            Debug.Log("決定ボタンか押されていないので、これでいいかな？ SEを流す");
            BGM_SE_MSC.korede_iikana_SE();         // これでいいかな？ SEを流す

            var sequence2 = DOTween.Sequence();
            sequence2.InsertCallback(10f, () => Auto_Push_JankenTe_KetteiButton());
        }
        else
        {
            Debug.Log("「決定ボタン」を自動で押す → これでいいかな？ SEを流す 条件を満たしていません・・・");
        }
        ResetCountdown_timer_Kettei_1();
    }


    public void Auto_Push_JankenTe_KetteiButton()   // ジャンケンパネルが開かれていて、決定ボタンか押されていないならば、決定ボタンを自動で押す
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        else
        {
            Countdown_Push_JankenTe_KetteiButton_Flg = false;
        }
        if (ShuffleCardsMSC.KetteiBtn.activeSelf)
        {
            Push_JankenTe_KetteiButton_Flg = true;
        }
        else
        {
            Push_JankenTe_KetteiButton_Flg = false;
        }
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg && Push_JankenTe_KetteiButton_Flg)
        {
            Debug.Log("決定ボタンか押されていないので、決定ボタンを自動で押す Auto");
            JankenTe_Kettei();                     // 決定ボタンを自動で押す
        }
        else
        {
            Debug.Log("「決定ボタン」を自動で押す → 押す条件を満たしていません・・・");
        }
        ResetCountdown_timer_Kettei_1();
    }

    #endregion

    #region // 待機OKマーカーの処理一連

    public void CloseTaiki_OK_All()
    {
        CloseTaiki_OK_P1();
        CloseTaiki_OK_P2();
        CloseTaiki_OK_P3();
        CloseTaiki_OK_P4();
    }

    public void AppearTaiki_OK_P1()
    {
        Taiki_OK_P1.SetActive(true);
    }
    
    public void CloseTaiki_OK_P1()
    {
        Taiki_OK_P1.SetActive(false);
    }

    public void AppearTaiki_OK_P2()
    {
        Taiki_OK_P2.SetActive(true);
    }

    public void CloseTaiki_OK_P2()
    {
        Taiki_OK_P2.SetActive(false);
    }

    public void AppearTaiki_OK_P3()
    {
        Taiki_OK_P3.SetActive(true);
    }

    public void CloseTaiki_OK_P3()
    {
        Taiki_OK_P3.SetActive(false);
    }

    public void AppearTaiki_OK_P4()
    {
        Taiki_OK_P4.SetActive(true);
    }

    public void CloseTaiki_OK_P4()
    {
        Taiki_OK_P4.SetActive(false);
    }
    #endregion

    #region// 【JK-01】からの処理 右上のジャンケン セット「開始ボタン」を押してからの、一連の処理

    public void PushOpenMyJankenPanel_Button()    // 【JK-01】OpenMyJankenPanel_Button（右上のセット開始ボタン） を押した時の処理
    {
        MoveTo_MyKagePos();  // 裏でY軸位置の調整
        ResetCountdown_timer_Kettei_1();
        Countdown_Push_JankenTe_KetteiButton_Flg = true;
        Erase_Text_Announcement();                // アナウンス テキスト文をリセットする
        Countdown_Push_OpenMyJankenPanel_Button_Flg = false;

        //AfterJump();   // 右にジャンプ（ぴょーん！）が完了してからの処理（右上の開始ボタンを押せるように各値をリセット）
        Debug.Log("【JK-01】******************************************************************");
        Debug.Log("【JK-01】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        Debug.Log("【JK-01】OpenMyJankenPanel_Button（右上のセット開始ボタン） が押されました。セットを開始します。カードを配ります");
        Debug.Log("【JK-01】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        Debug.Log("【JK-01】******************************************************************");
        Debug.Log("！！【JK-01】各プレイヤーの生存フラグは そのままです（リセットしません）！！");
        Debug.Log("！！【JK-01】各プレイヤーの待機中フラグは そのままです（リセットしません）！！");
        //ResetAlivePlayer();      // 【JK-01】各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする
        //Reset_NowWaiting();      // 【JK-01】待機中確認のパラメータ 初期化 0：待機前（初期値） にする
        Debug.Log("【JK-01】共通ジャンケン パネル（ベース）を表示します");
        ShuffleCardsMSC.AppearJankenCards_Panel();
        Debug.Log("【JK-01】自分のジャンケン パネル（カード選択画面）を表示します");
        ShuffleCardsMSC.AppearMyJankenPanel();
        //ShuffleCardsMSC.AppearWait_JankenPanel();
        Debug.Log("【JK-01】セット開始したばかりなので、ジャンケン手「決定ボタン」を非表示にします");
        Check_CanAppear_KetteiBtn();     // 【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら決定ボタンを表示する
        Countdown_Until_Push_JankenTe_KetteiButton();     // ジャンケンパネルが開かれていて、決定ボタンか押されていならば、カウントダウン開始
    }

    public void Janken_ExtraInning()              //【JK-37】ジャンケンカードを配る前の処理（延長戦突入時）
    {
        Debug.Log("【JK-38】■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■");
        Debug.Log("【JK-38】（延長戦）決着がつかなかったので延長戦に突入します！ジャンケンカードを配る準備をします。");
        Debug.Log("【JK-38】■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■");
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

        ToCheck_Iam_alive();            // ジャンケンで自分が生き残っているかどうかの確認をする
        if (Iam_alive == 1)    // 自分がまだジャンケン生存者であるならば
        {
            Debug.Log("【JK-46】（延長戦）自分はまだジャンケン生存者です。私の待機フラグは0（待機まえ）です。黒カバーしません。");
            Debug.Log("【JK-46】（延長戦）カードを選んで、延長戦を闘います！");
            Debug.Log("【JK-47】（延長戦）「待機中」画面 を非表示にします （→ カード選べるようになる）");
            ShuffleCardsMSC.CloseWait_JankenPanel();        //【JK-47】「待機中」画面 を非表示にする
            Countdown_Until_Push_JankenTe_KetteiButton();     // ジャンケンパネルが開かれていて、決定ボタンか押されていならば、カウントダウン開始
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

    #region // マッチング（ルーム入室）後、4人そろってから（あるいは一定数以上が「はじめる」を押したら）試合開始する処理
    public void Push_Ikemasu_Button()  // Ikemasu ボタンを押した時
    {
        Share_Iam_Ikemasu();           // 私「試合開始、いけます！」を全員に向け共有する
        Debug.Log("全員の Ikemasu を合計します");
        photonView.RPC("Gokei_Ikemasu_PlayersAll", RpcTarget.All);
        //Gokei_Ikemasu_PlayersAll();  // Ikemasu を合計する
        CheckStart_GameMatch();        // 試合開始できるか確認する処理
        ClosePanel_Ikemasu();
    }

    public void CheckStart_GameMatch()  // 試合開始できるか確認する処理
    {
        if (Shiai_Kaishi == false)  // 試合開始まえであれば、判定処理を実施する（試合中は判定する必要なし）
        {
            Debug.Log("現在の参加人数をチェック NinzuCheck");
            NinzuCheck();
            Debug.Log("Ikemasu を合計します");
            Gokei_Ikemasu_PlayersAll();  // Ikemasu を合計する
            Debug.Log("試合開始まえなので、試合開始できるか 判定処理を実施します");
            if (SankaNinzu == 4)
            {
                Debug.Log("4人そろったので、試合を開始します");
                photonView.RPC("Start_GameMatch", RpcTarget.All);
                //Start_GameMatch();     // 試合開始する処理
            }
            else if (SankaNinzu == 3)
            {
                if(int_Ikemasu_All >= 3)
                {
                    Debug.Log("3人いけます！ので、試合を開始します");
                    photonView.RPC("Start_GameMatch", RpcTarget.All);
                    //Start_GameMatch();     // 試合開始する処理
                }
            }
            else if (SankaNinzu == 2)
            {
                if (int_Ikemasu_All >= 2)
                {
                    Debug.Log("2人いけます！ので、試合を開始します");
                    photonView.RPC("Start_GameMatch", RpcTarget.All);
                    //Start_GameMatch();     // 試合開始する処理
                }
            }
            else
            {
                Debug.Log("まだ一人なので、他の参加者を待ちます");
            }
        }
    }

    [PunRPC]
    public void Start_GameMatch()  // 試合開始する処理
    {
        Shiai_Kaishi = true;
        text_Game_kaishi_MAE.text = "";
        text_Game_kaishi_CHU.text = "しあい中";
        if (PhotonNetwork.CurrentRoom.IsOpen)          // まだ入室許可が出ていたら
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;  // これ以上、入室できないようにする
        }
        // CloseTaiki_OK_All();
        CloseOpenMyJankenPanel_Button();
        CloseDebug_Buttons();
        ClosePanel_Ikemasu();
        ClosePanel_Intro();
        CloseAisatsu_Panel();
        CloseWinPanel();
        Reset_AllAisatsu();
        ShuffleCardsMSC.Reset_All();
        //ShuffleCardsMSC.Set_All();
        ShuffleCardsMSC.ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
        AppearStartLogo();
        BGM_SE_MSC.StartRappa_SE();  // ★ 開始のラッパを鳴らす！
        Countdown_Push_OpenMyJankenPanel_Button_Flg = true;
        //var sequence2 = DOTween.Sequence();
        //sequence2.InsertCallback(3f, () => Countdown_Until_Push_OpenMyJankenPanel_Button());
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3f, () => Start_GameMatch_After3());
    }

    public void Start_GameMatch_After3()  // 試合開始してから 3秒後 にする処理
    {
        Debug.Log("試合開始してから 3秒後 にする処理をします");
        ShuffleCardsMSC.Set_All();
        CloseTaiki_OK_All();
        CloseStartLogo();
        AppearOpenMyJankenPanel_Button();
        Countdown_Until_Push_OpenMyJankenPanel_Button();
    }


    public void Share_Iam_Ikemasu()    // 私「試合開始、いけます！」を全員に向け共有する
    {
        int_Iam_Ikemasu = 1;  // 私は いけます！
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Debug.Log("プレイヤー1はいけます！");
            // int_Ikemasu_Player1 = 1;
            photonView.RPC("Player1_Ikemasu", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Debug.Log("プレイヤー2はいけます！");
            photonView.RPC("Player2_Ikemasu", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Debug.Log("プレイヤー3はいけます！");
            photonView.RPC("Player3_Ikemasu", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            Debug.Log("プレイヤー4はいけます！");
            photonView.RPC("Player4_Ikemasu", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Player1_Ikemasu()  // Player1 が いけます！⇒ 全員に情報提供（共有）する
    {
        Debug.Log("[PunRPC] Player1（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player1 = 1;
    }
    [PunRPC]
    public void Player2_Ikemasu()  // Player2 が いけます！⇒ 全員に情報提供（共有）する
    {
        Debug.Log("[PunRPC] Player2（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player2 = 1;
    }
    [PunRPC]
    public void Player3_Ikemasu()  // Player3 が いけます！⇒ 全員に情報提供（共有）する
    {
        Debug.Log("[PunRPC] Player3（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player3 = 1;
    }
    [PunRPC]
    public void Player4_Ikemasu()  // Player4 が いけます！⇒ 全員に情報提供（共有）する
    {
        Debug.Log("[PunRPC] Player4（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player4 = 1;
    }

    [PunRPC]
    public void Gokei_Ikemasu_PlayersAll()  // Ikemasu を合計する
    {
        int_Ikemasu_All = int_Ikemasu_Player1 + int_Ikemasu_Player2 + int_Ikemasu_Player3 + int_Ikemasu_Player4;    // 現在「試合開始、いけます！」な人の総人数 を更新
        Debug.Log("「試合開始、いけます！」の総人数（int_Ikemasu_All） ： " + int_Ikemasu_All);
    }

    public void AppearPanel_Ikemasu()
    {
        Panel_Ikemasu.SetActive(true);
    }

    public void ClosePanel_Ikemasu()
    {
        Panel_Ikemasu.SetActive(false);
    }

    public void AppearStartLogo()
    {
        StartLogo.SetActive(true);
    }

    public void CloseStartLogo()
    {
        StartLogo.SetActive(false);
    }
    #endregion

    #region// じゃんけんカード 手のセット
    public void Share_Done_FirstChancePush()  // 王さま-どれい-セットチャンス 判定したら 0→1 [ 共有する ]
    {
        photonView.RPC("Done_FirstChancePush", RpcTarget.All);
    }

    [PunRPC]
    public void Done_FirstChancePush()  // 王さま-どれい-セットチャンス 判定したら 0→1 にする
    {
        ShuffleCardsMSC.FirstChancePush_Flg = 1;
    }

    public void Share_Reset_FirstChancePush_Flg()  // 王さま-どれい-セットチャンス リセットして 1→0 にする [ 共有する ]
    {
        photonView.RPC("Reset_FirstChancePush_Flg", RpcTarget.All);
    }

    [PunRPC]
    public void Reset_FirstChancePush_Flg()  // 王さま-どれい-セットチャンス リセットして 1→0 にする
    {
        ShuffleCardsMSC.FirstChancePush_Flg = 0;
    }
    #endregion

    #region // あいさつボタンを押した時の処理 一連
    public void Aisatsu_Who_Say()     // 誰があいさつしたの？ →それによってセリフの表示位置が変わる
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("Aisatsu_P1", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("Aisatsu_P2", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("Aisatsu_P3", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("Aisatsu_P4", RpcTarget.All);
        }
    }


    public void Share_Overwrite_Konchiwa()  // こんにちは
    {
        photonView.RPC("Overwrite_Konchiwa", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Konchiwa()
    {
        str_AisatsuBun = "こんにちは";
    }


    public void Share_Overwrite_Yoroshiku()  // よろしく
    {
        photonView.RPC("Overwrite_Yoroshiku", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Yoroshiku()
    {
        str_AisatsuBun = "よろしく";
    }


    public void Share_Overwrite_Arigato()  // ありがとう
    {
        photonView.RPC("Overwrite_Arigato", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Arigato()
    {
        str_AisatsuBun = "ありがとう";
    }


    public void Share_Overwrite_Matane()  // またね
    {
        photonView.RPC("Overwrite_Matane", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Matane()
    {
        str_AisatsuBun = "またねー";
    }


    public void Share_Overwrite_Baybay()  // バイバーイ
    {
        photonView.RPC("Overwrite_Baybay", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Baybay()
    {
        str_AisatsuBun = "バイバーイ";
    }


    public void Share_Overwrite_Otsukare()  // おつかれさま
    {
        photonView.RPC("Overwrite_Otsukare", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Otsukare()
    {
        str_AisatsuBun = "おつかれさま";
    }

    public void Share_Overwrite_Omedeto()  // おめでとう
    {
        photonView.RPC("Overwrite_Omedeto", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Omedeto()
    {
        str_AisatsuBun = "おめでとう";
    }


    public void Share_Overwrite_Kuu()  // くぅー！！
    {
        photonView.RPC("Overwrite_Kuu", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Kuu()
    {
        str_AisatsuBun = "くぅー！！";
    }


    public void Share_Overwrite_Shimatta()  // しまった！
    {
        photonView.RPC("Overwrite_Shimatta", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Shimatta()
    {
        str_AisatsuBun = "しまった！";
    }


    public void Share_Overwrite_Maji()  // マジッ！？
    {
        photonView.RPC("Overwrite_Maji", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Maji()
    {
        str_AisatsuBun = "マジッ！？";
    }


    public void Share_Overwrite_Yatta()  // やったね！
    {
        photonView.RPC("Overwrite_Yatta", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Yatta()
    {
        str_AisatsuBun = "やったね！";
    }


    public void Share_Overwrite_Yoshi()  // よしっ！
    {
        photonView.RPC("Overwrite_Yoshi", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Yoshi()
    {
        str_AisatsuBun = "よしっ！";
    }


    public void Share_Overwrite_ChottoMatte()  // ちょっとまって
    {
        photonView.RPC("Overwrite_ChottoMatte", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_ChottoMatte()
    {
        str_AisatsuBun = "ちょっとまって";
    }


    public void Share_Overwrite_Hajimeru()  // はじめる？
    {
        photonView.RPC("Overwrite_Hajimeru", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Hajimeru()
    {
        str_AisatsuBun = "はじめる？";
    }


    public void Share_Overwrite_Yaro()  // やろう！
    {
        photonView.RPC("Overwrite_Yaro", RpcTarget.All);
    }

    [PunRPC]
    public void Overwrite_Yaro()
    {
        str_AisatsuBun = "やろう！";
    }


    // ●P1
    [PunRPC]
    public void Aisatsu_P1()
    {
        text_AisatsuP1.text = str_AisatsuBun;
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2.5f, () => Erase_AisatsuP1());
    }

    public void Erase_AisatsuP1()  // あいさつ文を空欄にする（消しゴムで消すかのように）
    {
        text_AisatsuP1.text = "";
    }


    // ●P2
    [PunRPC]
    public void Aisatsu_P2()
    {
        text_AisatsuP2.text = str_AisatsuBun;
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2.5f, () => Erase_AisatsuP2());
    }

    public void Erase_AisatsuP2()  // あいさつ文を空欄にする（消しゴムで消すかのように）
    {
        text_AisatsuP2.text = "";
    }


    // ●P3
    [PunRPC]
    public void Aisatsu_P3()
    {
        text_AisatsuP3.text = str_AisatsuBun;
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2.5f, () => Erase_AisatsuP3());
    }

    public void Erase_AisatsuP3()  // あいさつ文を空欄にする（消しゴムで消すかのように）
    {
        text_AisatsuP3.text = "";
    }


    // ●P4
    [PunRPC]
    public void Aisatsu_P4()
    {
        text_AisatsuP4.text = str_AisatsuBun;
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2.5f, () => Erase_AisatsuP4());
    }

    public void Erase_AisatsuP4()  // あいさつ文を空欄にする（消しゴムで消すかのように）
    {
        text_AisatsuP4.text = "";
    }


    public void Reset_AllAisatsu()
    {
        text_AisatsuP1.text = "";
        text_AisatsuP2.text = "";
        text_AisatsuP3.text = "";
        text_AisatsuP4.text = "";
    }

    public void AppearAisatsu_Panel()
    {
        Aisatsu_Panel.SetActive(true);
    }

    public void CloseAisatsu_Panel()
    {
        Aisatsu_Panel.SetActive(false);
    }
    #endregion


    #region //【JK-05】ジャンケン手 決定ボタン（「これでOK!」）を押した時の処理以降
    public void JankenTe_Kettei()               // 【JK-05】からの処理 ： ジャンケン手 決定ボタン（「これでOK!」）を押した時の処理
    {
        Countdown_Push_JankenTe_KetteiButton_Flg = false;
        CheckAlivePlayer_DependOn_Absent();     // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）

        photonView.RPC("Share_Push_KetteiBtn", RpcTarget.All);
        Debug.Log("【JK-05】ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        ShuffleCardsMSC.CloseMyJankenPanel();   // 不要なパネルを閉じる

        Debug.Log("【JK-06】私のジャンケン手をみんなに提供（共有）します");
        ToSharePlayerTeNum();                   // 【JK-06】私のジャンケン手をみんなに提供（共有）します

        Debug.Log("【JK-08】決定ボタンを押したので、他のプレイヤーを待っています");
        ShuffleCardsMSC.AppearWait_JankenPanel();   // 待機中パネルを表示

        Debug.Log("【JK-10】私は待機中です");
        int_IamNowWaiting = 1;                  // 自分のジャンケン手 決定して待機中 （0：まだ決定してない、1：決定して待機中）

        Debug.Log("【JK-11】私が待機中ということを、全員（他のプレイヤー）に情報提供（共有）します");
        ToCheck_NowWaiting();                   // ジャンケンで自分が待機中の旨を 情報提供（共有）する

        Debug.Log("【JK-12】全員の待機フラグを確認します。その上で 勝敗判定（Hantei_Stream）フェーズへ進めるか確認します");
        Debug.Log("【JK-12】CanDoフラグ に関わらず、2 秒待機後、Check_Can_Hantei_Stream を実行します");
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2f, () => Check_Can_Hantei_Stream());
    }

    [PunRPC]
    public void Share_Push_KetteiBtn()               // 【JK-05】ジャンケン手 決定ボタン（「これでOK!」）押下したことを伝える
    {
        Debug.Log("【JK-05】******************************************************************");
        Debug.Log("【JK-05】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        Debug.Log("【JK-05】AcutivePlayerName" + AcutivePlayerName + "が ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        Debug.Log("【JK-05】PhotonNetwork.NickName" + PhotonNetwork.NickName + "が ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        Debug.Log("【JK-05】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        Debug.Log("【JK-05】******************************************************************");
    }

    #region // 【JK-11】待機中フラグ関連の処理
    public void ToCheck_NowWaiting()        // 【JK-11_1】ジャンケンで自分が待機中の旨を 情報提供（共有）する
    {
        //TestRoomControllerSC.PNameCheck();  // 現在の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Share_AcutivePlayerID();            // 【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Check_NowWaiting();
    }

    public void Check_NowWaiting()          // 【JK-11_2】ジャンケンで自分が待機中の旨を 情報提供（共有）する
    {
        Debug.Log("【JK-11_2】* ジャンケンで自分が待機中かどうかの確認をします *");
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("AcutivePlayerName  " + AcutivePlayerName);
        Debug.Log("AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Debug.Log("プレイヤー1は待機中です");
            // int_NowWaiting_Player1 = 1;
            photonView.RPC("Player1_NowWaiting", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Debug.Log("プレイヤー2は待機中です");
            photonView.RPC("Player2_NowWaiting", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Debug.Log("プレイヤー3は待機中です");
            photonView.RPC("Player3_NowWaiting", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            Debug.Log("プレイヤー4は待機中です");
            photonView.RPC("Player4_NowWaiting", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Player1_NowWaiting()  // 【JK-11_3】Player1 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("[PunRPC] 【JK-11_3】Player1（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player1 = 1;   // 0：待機前（初期値）、 1：待機中（決定ボタン押下後）
    }
    [PunRPC]
    public void Player2_NowWaiting()  // 【JK-11_3】Player2 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("[PunRPC] 【JK-11_3】Player2（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player2 = 1;
    }
    [PunRPC]
    public void Player3_NowWaiting()  // 【JK-11_3】Player3 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("[PunRPC] 【JK-11_3】Player3（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player3 = 1;
    }
    [PunRPC]
    public void Player4_NowWaiting()  // 【JK-11_3】Player4 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("[PunRPC] 【JK-11_3】Player4（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player4 = 1;
    }

    public void Reset_NowWaiting()    //【JK-38】【JK-204】待機中確認のパラメータ 初期化
    {
        Debug.Log("【JK-38】【JK-204】全員の待機中フラグを 0（待機まえ）にリセットします");
        int_NowWaiting_Player1 = 0;   // 0：待機前（初期値:決定ボタン押下前）、 1：待機中（決定ボタン押下後）
        int_NowWaiting_Player2 = 0;
        int_NowWaiting_Player3 = 0;
        int_NowWaiting_Player4 = 0;
        int_WaitingPlayers_All = 0;
        int_IamNowWaiting = 0;
    }
    #endregion

    [PunRPC]
    public void CanDo_Hantei_Stream_OFF()  // 勝敗判定（Hantei_Stream） を実行できるかのフラグ（CanDoフラグ OFF）
    {
        Debug.Log("CanDoフラグ OFF");
        bool_CanDo_Hantei_Stream = false;
    }

    [PunRPC]
    public void CanDo_Hantei_Stream_ON()  // 勝敗判定（Hantei_Stream） を実行できるかのフラグ（CanDoフラグ ON）
    {
        Debug.Log("CanDoフラグ ON（全員待機中です。）");
        bool_CanDo_Hantei_Stream = true;
    }

    public void Check_Can_Hantei_Stream()      // 【JK-12】勝敗判定（Hantei_Stream）フェーズへ進めるか確認する： 全員待機中であれば、勝敗判定（Hantei_Stream）に進む。/ 一人でも待機まえであれば、何もしない（処理せず全員揃うまで待つ）
    {
        Debug.Log("ローカルの CanDoフラグ を OFF にします");
        CanDo_Hantei_Stream_OFF();             // 勝敗判定（Hantei_Stream） を実行できるかのフラグ（CanDoフラグ OFF）
        Debug.Log("ローカルの CanDoフラグ を OFF にしました");
        Debug.Log("【JK-12】Check_Can_Hantei_Stream()スタート： 勝敗判定（Hantei_Stream）フェーズへ進めるか確認します");
        Debug.Log("【JK-12】全員待機中であれば、勝敗判定（Hantei_Stream）に進む。/ 一人でも待機まえであれば、何もしない（処理せず全員揃うまで待つ）");
        Debug.Log("【JK-12】■count_RoundRoop : " + count_RoundRoop + " ラウンド");    // N回目のジャンケンループ
        if (count_RoundRoop == 1)  // 1ラウンド目
        {
            Debug.Log("ラウンドループ 1回目");
            Debug.Log(TestRoomControllerSC.allPlayers.Length + ": allPlayers.Length");
            Debug.Log("現在の参加人数は " + TestRoomControllerSC.int_JoinedPlayerAllNum);
            Debug.Log("【JK-12】総参加人数 と 現在待機中の総人数 をチェックします");
            NinzuCheck();                          // 【JK-12】総参加人数 と 現在待機中の総人数
            if (int_WaitingPlayers_All == SankaNinzu)  // 参加している全員が待機中になっていたら
            {
                Debug.Log("■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting");
                Debug.Log("【JK-12_1】全員手が決定しました。全員待機中です。勝敗判定（Hantei_Stream）に進みます！！");
                Debug.Log("■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting■ NowWaiting");
                Debug.Log("[RPC] [RPC] [RPC] [RPC] [RPC] [RPC] [RPC] [RPC] [RPC] ");
                Debug.Log("[RPC] 全員待機中です。CanDoフラグ を ON にし、全員に共有します");
                Debug.Log("[RPC] [RPC] [RPC] [RPC] [RPC] [RPC] [RPC] [RPC] [RPC] ");
                photonView.RPC("CanDo_Hantei_Stream_ON", RpcTarget.All);    // 勝敗判定（Hantei_Stream） を実行できるかのフラグ（CanDoフラグ ON）
                Debug.Log("！！！！！ 勝敗判定（Hantei_Stream）はローカルの Update で各自実行してもらいます ！！！！！");
                //Hantei_Stream();                   // 【JK-21】ジャンケン勝敗判定（Hantei_Stream）実施 ⇒ 勝ったプレイヤー1名のみジャンプで前進する
                //photonView.RPC("Hantei_Stream", RpcTarget.All);
            }
            else                                   // 一人でも待機まえである
            {
                Debug.Log("【JK-12_2】まだ決定ボタンを 押していない人がいます");
                Debug.Log("【JK-12_2】全員揃うまで待ちます...");
                Debug.Log("【JK-12_2】Now Waiting ...");
            }
        }
        else                      // 2ラウンド目以降
        {
            Debug.Log("ラウンドループ 2回目以降");
            //photonView.RPC("Hantei_Stream", RpcTarget.All);
            //Debug.Log("！！！！！ Hantei_Stream() はローカルの Update で各自実行してもらいます ！！！！！");
            Debug.Log("自動的に 勝敗判定（Hantei_Stream）に進みます。ローカル実行してください。");
            Hantei_Stream();      //【JK-21】ジャンケン勝敗判定 実施 ⇒ ジャンケン勝者を1名に絞り込む（2名以上なら Check_Can_Hantei_Stream() に戻る）
        }
    }

    public void Hantei_Stream()  //【JK-21】ジャンケン勝敗判定 実施 ⇒ ジャンケン勝者を1名に絞り込む（2名以上なら Check_Can_Hantei_Stream() に戻る）
    {
        CanDo_Hantei_Stream_OFF();             // 勝敗判定（Hantei_Stream） を実行できるかのフラグ（CanDoフラグ OFF）
        Debug.Log("【JK-21】勝敗判定（Hantei_Stream） 開始");

        ToCheck_Iam_alive();            // ジャンケンで自分が生き残っているかどうかの確認をする

        Debug.Log("【JK-21】黒カバー表示確認");
        if (Iam_alive == 1) // 自分がジャンケン生存者であるなら
        {
            Debug.Log("【JK-22】私は生きています！");
            ShuffleCardsMSC.CloseWait_JankenPanel();        // 「待機中」を非表示にする
        }
        else                // 自分がジャンケン敗北者なら
        {
            Debug.Log("【JK-22】はぁ、はぁ、敗北者？");
            ShuffleCardsMSC.AppearWait_JankenPanel();       // 「待機中」を表示させる（画面を隠す）
        }

        Debug.Log("【JK-23】ジャンケン ラウンドループ を 開始します");
        CountLivePlayer();            //【JK-26】残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
        Debug.Log("NumLivePlayer（ジャンケン生存者）" + NumLivePlayer);

        if (NumLivePlayer >= 2)  // ジャンケン生存者が2人以上残っている場合
        {
            Debug.Log("今、" + count_RoundRoop + "回目 のラウンドループです。まだ生存者2人以上です。");
            if (count_RoundRoop <= 5)     // 1～5回目 ラウンドループ
            {
                JankenBattle_OneRoop();   //【JK-23】じゃんけん手の勝ち負けを判定 → 生存人数（NumLivePlayer）が減る（⇒その後、Check_Can_Hantei_Stream() に戻る）
            }
            else                          // 6回目以降 ラウンドループ
            {
                Debug.Log("ラウンドループを5回繰り返しましたが決着つきませんでした。残り1人になるまでやり直します");
                PrepareToNextSet();       //【JK-28】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット ＆ count_RoundRoop 1に戻す【JK-36】
                Debug.Log("【JK-37】（延長戦）延長戦に突入します");
                Janken_ExtraInning();     //【JK-37】（延長戦）ジャンケンカードを配る前の処理（延長戦突入時） ⇒ 生存者 1人になるまでやり直し（右上の開始ボタン押下時とほぼ同じ）
            }
        }
        else                   //【JK-27_3】ジャンケン生存者が1人のみの場合
        {
            Debug.Log("【JK-27_3】決着！ 生存者 1名になりました");    // ここでジャンケンの勝者が 1名 になった
            AfterWinnerDecision();    //【JK-100】この時点で ジャンケン生存者は 1名です。これから勝者の前進ジャンプ処理に移ります。
        }
    }

    /*
        public void JankenBattle_OneRoop()    // 【JK-23-】ジャンケンバトルの１ループ分処理（1ラウンド）
        {
            Debug.Log("【JK-23-】■count_RoundRoop : " + count_RoundRoop + " 回目（ラウンド）のジャンケンループ ");    // N回目のジャンケンループ
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(2f * count_RoundRoop, () => JankenBattle_MainPart());  // ジャンケンバトルのメイン判定処理（2秒待機後）
        }
      */

    public void JankenBattle_OneRoop()          //【JK-23-】ジャンケンバトルの１ループ分処理（1ラウンド）
    {
        Debug.Log("【JK-23-】■count_RoundRoop : " + count_RoundRoop + " 回目（ラウンド）のジャンケンループ ");    // N回目のジャンケンループ
        StartCoroutine("Entrance_MainPart");    // メイン判定処理の前段階（2秒待機後に MainPart へ）
    }

    IEnumerator Entrance_MainPart()             // メイン判定処理の前段階（2秒待機後に MainPart へ）
    {
        Debug.Log("【JK-23-】メイン判定処理の前段階（2秒待機後に MainPart へ）");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("【JK-23-】2秒 待機しました");
        JankenBattle_MainPart();
    }

    public void JankenBattle_MainPart()   // 【JK-23-】ジャンケンバトルのメイン判定処理
    {
        Debug.Log("【JK-23-】ジャンケンバトルのメイン判定処理");
        SetKP_counter();              //【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
        Syohai_Hantei();              //【JK-25】N回目 のループ における 残留プレイヤー同士の じゃんけん手の勝ち負けを判定 → 人数が減る
        CountLivePlayer();            //【JK-26】残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
        count_RoundRoop++;            // N回目 のループ を 1 進める
        Debug.Log("ジャンケンバトルのメイン判定処理おわり！ ラウンドループのはじめに戻ります。");
        Check_Can_Hantei_Stream();    // ラウンドループのはじめに戻ります。
    }


    public void PrepareToNextSet()    // 【JK-28】【JK-201】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット【JK-36】
    {
        Debug.Log("【JK-28】【JK-201】PrepareToNextSet 次のセットへ移る準備をします");
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

    public void AfterWinnerDecision()     //【JK-100】この時点で ジャンケン生存者は 1名です。これから勝者の前進ジャンプ処理に移ります。
    {
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
        ToCheck_Iam_Winner();         //【JK-104】ジャンケンで自分が勝利者かどうかの確認をする → 勝ってたら右にジャンプ（ぴょーん！）！【JK-110】

        Debug.Log("【JK-200】不要なパネルを閉じて、デフォルト状態にします");
        ShuffleCardsMSC.ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
    }

    public void Check_WaitingFlg_DependOn_alive()  //【JK-44】（延長戦）ジャンケン生存者（aliveフラグが 1 の人）は待機フラグを0に、敗北者は待機フラグを1にする && 黒カバー表示
    {
        CheckAlivePlayer_DependOn_Absent();        // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）

        if (alivePlayer1 == 1)                     // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player1 = 0;            // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player1 = 1;         // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player1_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P1();            // 黒カバー表示
        }

        if (alivePlayer2 == 1)                    // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player2 = 0;           // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player2 = 1;        // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player2_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P2();           // 黒カバー表示
        }

        if (alivePlayer3 == 1)                   // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player3 = 0;          // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player3 = 1;       // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player3_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P3();          // 黒カバー表示
        }

        if (alivePlayer4 == 1)                  // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player4 = 0;         // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            //int_NowWaiting_Player4 = 1;       // 待機フラグを 1 ：待機中 にする
            photonView.RPC("Player4_NowWaiting", RpcTarget.All);
            AppearImg_CoverBlack_P4();          // 黒カバー表示
        }
        Debug.Log("【JK-45】（延長戦）生き残っている者のみが「待機まえ」になりました。敗北者は待機中（見守り中）です。");
    }

    public void ShareAfterJump()   // 右にジャンプ（ぴょーん！）が完了してからの処理（右上の開始ボタンを押せるように各値をリセット） ⇒ 全員に共有する
    {
        photonView.RPC("AfterJump", RpcTarget.All);
    }

    [PunRPC]
    public void AfterJump()   // 右にジャンプ（ぴょーん！）が完了してからの処理（右上の開始ボタンを押せるように各値をリセット）
    {
        //CloseSubCamera_Group();      // ジャンプ終わったら サブカメラ非表示
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(1.5f, () => CloseSubCamera_Group());

        Debug.Log("【JK-201】PrepareToNextSet 次のセットへ移る準備 をします");
        PrepareToNextSet();           //【JK-201】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット
        Debug.Log("【JK-202】PrepareToNextSet 次のセットへ移る準備 終わりました");

        Debug.Log("【JK-203】全員の aliveフラグ を 1 にします（全員生存）");
        ResetAlivePlayer();           //【JK-203】各種 生存者カウンター リセット
        //anzenPoint = 0;

        Debug.Log("【JK-204】待機中フラグ（確認用パラメータ） を 初期化（0にする）");
        Reset_NowWaiting();      // 待機中フラグ（確認用パラメータ） を 初期化（0にする）

        ResetCountdown_timer_PanelOpen_1();
        //Countdown_Push_OpenMyJankenPanel_Button_Flg = true;
        Countdown_Until_Push_OpenMyJankenPanel_Button();   // ジャンケンパネルが開かれていないならば、ボタンを押すようにアナウンスする
    }

    public void Share_MyJankenPanel_Button_Flg_ON()   // ジャンケン開始ボタン フラグ をON
    {
        photonView.RPC("MyJankenPanel_Button_Flg_ON", RpcTarget.All);
    }

    [PunRPC]
    public void MyJankenPanel_Button_Flg_ON()         // ジャンケン開始ボタン フラグ をON
    {
        Countdown_Push_OpenMyJankenPanel_Button_Flg = true;
    }


    public void ResetAlivePlayer()         //【START-03】【JK-203】各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする）
    {
        Debug.Log("【START-03】【JK-203】全員の aliveフラグ を 1 にします（全員生存）");
        alivePlayer1 = 1;                    // ジャンケンで残留してれば 1 、負けたら 0
        alivePlayer2 = 1;
        alivePlayer3 = 1;
        alivePlayer4 = 1;
        Iam_alive = 1;
        CloseImg_CoverBlack_All();           // ジャンケン手の黒カバーをリセット（非表示）

        CheckAlivePlayer_DependOn_Absent();  // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）
        count_RoundRoop = 1;                 // ラウンドループを1に戻す
    }

    public void CheckAlivePlayer_DependOn_Absent()         // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）
    {
        Debug.Log("生存カウンターのチェック前");
        Debug.Log("alivePlayer1 ： " + alivePlayer1);
        Debug.Log("alivePlayer2 ： " + alivePlayer2);
        Debug.Log("alivePlayer3 ： " + alivePlayer3);
        Debug.Log("alivePlayer4 ： " + alivePlayer4);

        Debug.Log("生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）");
        if (TestRoomControllerSC.string_PID2 == "")
        {
            alivePlayer2 = 0;
        }

        if (TestRoomControllerSC.string_PID3 == "")
        {
            alivePlayer3 = 0;
        }

        if (TestRoomControllerSC.string_PID4 == "")
        {
            alivePlayer4 = 0;
        }
        Debug.Log("alivePlayer1 ： " + alivePlayer1);
        Debug.Log("alivePlayer2 ： " + alivePlayer2);
        Debug.Log("alivePlayer3 ： " + alivePlayer3);
        Debug.Log("alivePlayer4 ： " + alivePlayer4);
        /*
        Debug.Log("KP値に応じて生存カウンターのチェック");
        if (KP1 != -1)
        {
            alivePlayer2 = 1;
        }

        if (TestRoomControllerSC.string_PID3 == "")
        {
            alivePlayer3 = 0;
        }

        if (TestRoomControllerSC.string_PID4 == "")
        {
            alivePlayer4 = 0;
        }
        Debug.Log("alivePlayer1 ： " + alivePlayer1);
        Debug.Log("alivePlayer2 ： " + alivePlayer2);
        Debug.Log("alivePlayer3 ： " + alivePlayer3);
        Debug.Log("alivePlayer4 ： " + alivePlayer4);
        */
    }

    public void ToCheck_Iam_alive()            // ジャンケンで自分が生き残っているかどうかの確認をする
    {
        //TestRoomControllerSC.PNameCheck();   // 現在の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        NinzuCheck();                          // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Share_AcutivePlayerID();               // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Check_Iam_alive();
    }

    public void Check_Iam_alive()              // ジャンケンで自分が生き残っているかどうかの確認をする
    {
        Debug.Log("* ジャンケンで自分が生き残っているかどうかの確認をします *");
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("AcutivePlayerName  " + AcutivePlayerName);
        Debug.Log("AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
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

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
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

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
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

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
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

    public void WhoIsWinner()                //【JK-103】ジャンケン勝敗の勝利者は？
    {
        WinnerNum = -1;                      // 一旦リセット
        CheckAlivePlayer_DependOn_Absent();  // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）
        if (alivePlayer1 == 1)
        {
            Debug.Log("【JK-103】Player1 勝利");
            WinnerNum = 1;
            Text_WinnerName.text = TestRoomControllerSC.string_PName1;
        }
        else if (alivePlayer2 == 1)
        {
            Debug.Log("【JK-103】Player2 勝利");
            WinnerNum = 2;
            Text_WinnerName.text = TestRoomControllerSC.string_PName2;
        }
        else if (alivePlayer3 == 1)
        {
            Debug.Log("【JK-103】Player3 勝利");
            WinnerNum = 3;
            Text_WinnerName.text = TestRoomControllerSC.string_PName3;
        }
        else if (alivePlayer4 == 1)
        {
            Debug.Log("【JK-103】Player4 勝利");
            WinnerNum = 4;
            Text_WinnerName.text = TestRoomControllerSC.string_PName4;
        }
        else
        {
            Debug.LogError("【JK-103】勝利いない？");
        }
    }

    public void ToCheck_Iam_Winner()           //【JK-104】ジャンケンで自分が勝利者かどうかの確認をするための準備【JK-106】
    {
        //TestRoomControllerSC.PNameCheck();     // 現在の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Share_AcutivePlayerID();               // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        Check_Iam_Winner();
    }

    public void Check_Iam_Winner()         //【JK-105】ジャンケンで自分が勝利者かどうかの確認をする
    {
        Debug.Log("【JK-105】ジャンケンで自分が勝利者かどうかの確認をします。自分が勝ってたらジャンプします。");
        WhoAreYou();     // 私の名前（真名）を表示
        Debug.Log("【JK-105】AcutivePlayerName  " + AcutivePlayerName);
        Debug.Log("【JK-105】AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            if (WinnerNum == 1)          // プレイヤー1 が勝利者
            {
                Debug.Log("【JK-106】P1 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            if (WinnerNum == 2)          // プレイヤー2 が勝利者
            {
                Debug.Log("【JK-106】P2 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            if (WinnerNum == 3)          // プレイヤー3 が勝利者
            {
                Debug.Log("【JK-106】P3 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
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


    public void CountLivePlayer()       //【JK-26】残留しているプレイヤー人数をカウントする
    {
        CheckAlivePlayer_DependOn_Absent();  // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）
        NumLivePlayer = alivePlayer1 + alivePlayer2 + alivePlayer3 + alivePlayer4;
        Debug.Log("【JK-26】NumLivePlayer 残留プレイヤー数 ： " + NumLivePlayer);
    }

    public void SetKP_counter()         //【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
    {
        Debug.Log("【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する SetKP_counter()");    // N回目のラウンドループ
        Debug.Log("【JK-24】count_RoundRoop" + count_RoundRoop + " 回目（ラウンド）のジャンケンループ ");    // N回目のラウンドループ

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

    public void Check_Gu_Existence()    //【JK-25】NoneGu の判定を返す
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

    public void Check_Choki_Existence() //【JK-25】NoneChoki の判定を返す
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

    public void Check_Pa_Existence()    //【JK-25】NonePa の判定を返す
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

    public void Syohai_Hantei()         //【JK-25】生存者同士の 勝ち負けを判定（負けた人のaliveフラグ を 0 にする） → 人数が減る ＆＆ 移動ステップ数を 勝った手に応じて上書き
    {
        Debug.Log("");
        Debug.Log("");
        Debug.Log("【JK-25】Syohai_Hantei スタート");
        Debug.Log("");
        Debug.Log("");
        CheckAlivePlayer_DependOn_Absent();  //【JK-25】生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）
        Check_Gu_Existence();                //【JK-25】 N回目の すべてのプレイヤーの手 の中に グー(0)   があるか ： NoneGu を取得
        Check_Choki_Existence();             //【JK-25】 N回目の すべてのプレイヤーの手 の中に チョキ(1) があるか ： NoneChoki を取得
        Check_Pa_Existence();                //【JK-25】 N回目の すべてのプレイヤーの手 の中に パー(2)   があるか ： NonePa を取得

        if (NoneGu)           //【JK-25】（全員 Gu 無し）ちょき か ぱー
        {
            Debug.Log("【JK-25】(NoneGu) （全員 Gu 無し）ちょき か ぱー です");
            if (NoneChoki)    //（全員 Choki 無し）ぱー のみ
            {
                Debug.Log("【JK-25】(NoneGu) ⇒ ぱー のみ あいこ");
                Aiko();       //    ⇒ ぱー のみ
            }
            else if (NonePa)  // (全員 Pa 無し) ちょき のみ
            {
                Debug.Log("【JK-25】(NoneGu) ⇒ ちょき のみ あいこ");
                Aiko();       //    ⇒ ちょき のみ
            }
            else              // ちょき と ぱー
            {
                Debug.Log("【JK-25】(NoneGu) ちょき と ぱー");
                Win_Choki();
                Lose_Pa();    //    ⇒  ぱー の人のみ 脱落
            }
        }
        else if (NoneChoki)   //【JK-25】(全員 Choki 無し) ぐー か ぱー （↓これ以降、ぐー は必ずある）
        {
            Debug.Log("【JK-25】(NoneChoki) (全員 Choki 無し) ぐー か ぱー です");
            if (NoneGu)       // (全員 Gu 無し) ぱー のみ
            {
                Debug.Log("【JK-25】(NoneChoki) ⇒  ぱー のみ あいこ");
                Aiko();       //    ⇒  ぱー のみ
            }
            else if (NonePa)  // (全員 Pa 無し) ぐー のみ
            {
                Debug.Log("【JK-25】(NoneChoki) ⇒  ぐー のみ あいこ");
                Aiko();       //    ⇒  ぐー のみ
            }
            else              // ぐー と ぱー
            {
                Debug.Log("【JK-25】(NoneChoki) ぐー と ぱー");
                Win_Pa();
                Lose_Gu();    //    ⇒  ぐー の人のみ 脱落
            }
        }
        else if (NonePa)      //【JK-25】(全員 Pa 無し)  ぐー か ちょき （↓これ以降、ぐー と ちょき は必ずある）
        {
            Debug.Log("【JK-25】(NonePa) (全員 Pa 無し)  ぐー か ちょき です");
            if (NoneGu)          // (全員 Gu 無し) ちょき のみ
            {
                Debug.Log("【JK-25】(NonePa) ⇒  ちょき のみ あいこ");
                Aiko();          //    ⇒  ちょき のみ
            }
            else if (NoneChoki)  //（全員 Choki 無し）ぐー のみ
            {
                Debug.Log("【JK-25】(NonePa) ⇒  ぐー のみ あいこ");
                Aiko();          //    ⇒  ぐー のみ
            }
            else                 // ぐー と ちょき
            {
                Debug.Log("【JK-25】(NonePa) ぐー と ちょき");
                Win_Gu();
                Lose_Choki();    //    ⇒  ちょき の人のみ 脱落
            }
        }
        else                     //【JK-25】 ぐー か ちょき か ぱー（↓これ以降、ぐー と ちょき と ぱー は必ずある）
        {
            Debug.Log("【JK-25】ぐー ちょき ぱー 全部 で あいこ");
            Aiko();              // ぐー ちょき ぱー 全部
        }
    }

    public void Aiko()              //【JK-25】残っている人、全員残留
    {
        // 残っている人、全員残留
        Debug.Log("【JK-25】あいこ です");
        photonView.RPC("ShareInfo_Aiko", RpcTarget.All);
    }
    [PunRPC]
    public void ShareInfo_Aiko()    //【JK-25】あいこを 全員に共有
    {
        Debug.Log("【JK-25】あいこ です");
        Share_AcutivePlayerID();    // 現在操作している人のプレイヤー名取得
    }

    public void Win_Gu()            //【JK-25】ぐー の人のみ 残留（移動ステップ数を 3 に上書き）
    {
        // ぐー の人のみ 残留
        Debug.Log("【JK-25】ぐー の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Gu", RpcTarget.All);    //【JK-25】 ぐー の人のみ 残留（Win_Gu）を 全員に共有
        original_StepNum = 3;       // 移動ステップ数を 3 に上書き
        photonView.RPC("ShareStepNum_3", RpcTarget.All);    // 移動ステップ数を 3 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Gu()  //【JK-25】 ぐー の人のみ 残留（Win_Gu）を 全員に共有
    {
        Debug.Log("【JK-25】ぐー の人のみ 残留（Win_Gu）");
        Share_AcutivePlayerID();    // 現在操作している人のプレイヤー名取得
    }

    public void Win_Choki()         //【JK-25】ちょき の人のみ 残留（移動ステップ数を 6 に上書き）
    {
        // ちょき の人のみ 残留
        Debug.Log("【JK-25】ちょき の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Choki", RpcTarget.All);    //【JK-25】 ちょき の人のみ 残留（Win_Choki）を 全員に共有
        original_StepNum = 6;      // 移動ステップ数を 6 に上書き
        photonView.RPC("ShareStepNum_6", RpcTarget.All);    // 移動ステップ数を 6 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Choki()   //【JK-25】 ちょき の人のみ 残留（Win_Choki）を 全員に共有
    {
        Debug.Log("【JK-25】ちょき の人のみ 残留");
        Share_AcutivePlayerID();       // 現在操作している人のプレイヤー名取得
    }

    public void Win_Pa()               //【JK-25】ぱー の人のみ 残留（移動ステップ数を 6 に上書き）
    {
        // ぱー の人のみ 残留
        Debug.Log("【JK-25】ぱー の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Pa", RpcTarget.All);    //【JK-25】 ぱー の人のみ 残留（Win_Pa）を 全員に共有
        original_StepNum = 6;     // 移動ステップ数を 6 に上書き
        photonView.RPC("ShareStepNum_6", RpcTarget.All);    // 移動ステップ数を 6 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Pa()   //【JK-25】 ぱー の人のみ 残留（Win_Pa）を 全員に共有
    {
        Debug.Log("【JK-25】ぱー の人のみ 残留");
        Share_AcutivePlayerID();     // 現在操作している人のプレイヤー名取得
    }

    public void Lose_Gu()     //【JK-25】ぐー の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        Debug.Log("【JK-25】ぐー（0） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
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

    public void Lose_Choki()  //【JK-25】ちょき の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        Debug.Log("【JK-25】ちょき（1） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
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

    public void Lose_Pa()     //【JK-25】ぱー の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        Debug.Log("【JK-25】ぱー（2） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
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

    #region                   //【JK-25】 移動ステップ数 の共有
    [PunRPC]
    public void ShareStepNum_1()   //【JK-25】 移動ステップ数を 1 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 1;     // 移動ステップ数を 1 に上書き
        Debug.Log("移動ステップ数（original_StepNum）を 1 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_3()   //【JK-25】 移動ステップ数を 3 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 3;     // 移動ステップ数を 3 に上書き
        Debug.Log("移動ステップ数（original_StepNum）を 3 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_4()   //【JK-25】 移動ステップ数を 4 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 4;     // 移動ステップ数を 4 に上書き
        Debug.Log("移動ステップ数（original_StepNum）を 4 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_6()   //【JK-25】 移動ステップ数を 6 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 6;     // 移動ステップ数を 6 に上書き
        Debug.Log("移動ステップ数（original_StepNum）を 6 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_8()   //【JK-25】 移動ステップ数を 8 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 8;     // 移動ステップ数を 8 に上書き
        Debug.Log("移動ステップ数（original_StepNum）を 8 に上書きしました");
    }
    #endregion


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
        //TestRoomControllerSC.PNameCheck(); // プレイヤー名が埋まっていなかったら入れる
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Share_AcutivePlayerID();  // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        //photonView.RPC("SharePlayerTeNum", RpcTarget.All);
        SharePlayerTeNum();
    }

    public void SharePlayerTeNum()    // 【JK-06】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）【JK-08】
    {
        Debug.Log("【JK-06】************ データ共有 SharePlayerTeNum  スタート **********");
        //Debug.Log(TestRoomControllerSC.string_PID1 + ": TestRoomControllerSC.string_PID1");
        //Debug.Log(TestRoomControllerSC.string_PID2 + ": TestRoomControllerSC.string_PID2");
        //Debug.Log(TestRoomControllerSC.string_PID3 + ": TestRoomControllerSC.string_PID3");
        //Debug.Log(TestRoomControllerSC.string_PID4 + ": TestRoomControllerSC.string_PID4");
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        //Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        //Debug.Log("AcutivePlayerName  " + AcutivePlayerName);  // 今ボタン押した人
        //Debug.Log("AcutivePlayerID  " + AcutivePlayerID);  // 今ボタン押した人

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1)
        {
            Debug.Log("【JK-07】今からプレイヤー1＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player1();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2)
        {
            Debug.Log("【JK-07】今からプレイヤー2＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player2();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3)
        {
            Debug.Log("【JK-07】今からプレイヤー3＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player3();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4)
        {
            Debug.Log("【JK-07】今からプレイヤー4＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
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

    public void PushBtn_Omakase() // 【JK-02】おまかせボタン押したよ
    {
        Debug.Log("【JK-02】おまかせボタン押したよ");
        if (CanPushBtn_Omakase)
        {
            Push_Btn_A();
            Push_Btn_B();
            Push_Btn_C();
            Push_Btn_D();
            Push_Btn_E();
        }
        Btn_Omakase.interactable = false;
        CanPushBtn_Omakase = false;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
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
        ToCanPush_Omakase();
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

    public void ToCanPush_Omakase() // 【JK-02】おまかせボタン押せるようにするよ
    {
        Btn_Omakase.interactable = true;
        CanPushBtn_Omakase = true;
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
        Debug.Log("ジャンケン勝者の位置に応じて、SubCamera を移動します");
        SetPosX_SubCamera_AccordingTo_Winner();  // ジャンケン勝者の位置に応じて、SubCamera を移動する

        Debug.Log("【JK-107】FromWin_ToJump （ジャンプ移動）処理に入ります");
        Set_StepNum();               //【JK-108】ジャンプする回数を設定する（変数上書き） 
        bridge_JumpToRight();        //【JK-109】右方向へ 指定された回数 ぴょん と跳ねながら移動する
    }

    public void Set_StepNum()        //【JK-108】ジャンプする回数を設定する（変数上書き） 
    {
        Debug.Log("【JK-108】ジャンプする回数を設定（変数上書き）します");
        Debug.Log("PlayerSC.MoveForward_StepNum :" + PlayerSC.MoveForward_StepNum);
        Debug.Log("original_StepNum :" + original_StepNum);
        PlayerSC.MoveForward_StepNum = original_StepNum;   // ジャンプして移動するステップ数（の元となる変数）に上書きする
        Debug.Log("PlayerSC.MoveForward_StepNum :" + PlayerSC.MoveForward_StepNum);
    }

    public void bridge_JumpToRight()  //【JK-109】右方向へ 指定された回数 ぴょん と跳ねながら移動する
    {
        Debug.Log("【JK-109】bridge_JumpToRight（ジャンプ移動） を実行します");
        Debug.Log("【JK-109】処理前に " + JumpMaeTaiki + "秒 待機します");
        Debug.Log("【JK-109】JumpMaeTaiki : " + JumpMaeTaiki);

        //PlayerSC.JumpRight();
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(JumpMaeTaiki, () => PlayerSC.JumpRight());
        //Debug.Log("【JK-110】ぴょーん！ ぴょーん！ ぴょーん！");
    }

    public void Check_KageDistance()               //  MyKage と MyPlayer の距離を求める（Y軸の初期位置）
    {
        KageDistance = myPlayer.transform.position.y - MyKage.transform.position.y;
    }

    public void MoveTo_MyKagePos()                 //  MyKage の位置へ移動する（Y軸位置微調整）
    {
        /*
        MyKage_Trans = MyKage.transform;           // transformを取得
        PosMyKage = MyKage_Trans.position;         // MyKage の座標を取得
        myPlayer.transform.position = PosMyKage;   // プレイヤー位置を MyKage に移動
        */
        myPlayer.transform.position = new Vector3(myPlayer.transform.position.x, MyKage.transform.position.y + KageDistance, myPlayer.transform.position.z);
    }

    public void bridge_GetDamage()
    {
        Debug.Log("bridge_GetDamage を実行します");
        PlayerSC.receivedDammage();
        BGM_SE_MSC.korede_iikana_SE();         // これでいいかな？ SEを流す

    }

    #region // サブカメラの処理一連
    public void SetPosX_SubCamera_AccordingTo_Winner()  // ジャンケン勝者の位置に応じて、SubCamera を移動する
    {
        Debug.Log("PosX_Winner : " + PosX_Winner);
        PosX_Winner = MyKage.transform.position.x;
        Debug.Log("PosX_Winner : " + PosX_Winner);
        share_SubCamera_Moving();    // サブカメラ を移動 ⇒ 全員に共有
        //AppearSubCamera_Group();   // サブカメラ を表示
        photonView.RPC("AppearSubCamera_Group", RpcTarget.All);  // 全員にサブカメラを一斉に開かせる
    }

    public void share_SubCamera_Moving()              // ジャンケン勝者近くの cafe_kanban の位置に SubCamera を移動する
    {
        if (PosX_Winner >= cafe_kanban_035.transform.position.x)
        {
            photonView.RPC("MoveTo_cafe_kanban_025", RpcTarget.All);

        }
        if (PosX_Winner >= cafe_kanban_025.transform.position.x)
        {
            photonView.RPC("MoveTo_cafe_kanban_015", RpcTarget.All);

        }
        if (PosX_Winner >= cafe_kanban_015.transform.position.x)
        {
            photonView.RPC("MoveTo_cafe_kanban_005", RpcTarget.All);

        }
        if (PosX_Winner >= cafe_kanban_005.transform.position.x)
        {
            photonView.RPC("MoveTo_cafe_kanban_0_5", RpcTarget.All);
        }
    }

    /*
    public void Move_SubCamera_ToWinner()
    {
        SubCamera.transform.position = new Vector3(PosX_Winner, SubCamera.transform.position.y, SubCamera.transform.position.z);
    }
    */

    [PunRPC]
    public void MoveTo_cafe_kanban_035()   // SubCamera を 035 の位置に移動する  -28.4
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_035.transform.position.x, SubCamera.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_025()   // SubCamera を 025 の位置に移動する
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_025.transform.position.x, SubCamera.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_015()   // SubCamera を 015 の位置に移動する  -8.4
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_015.transform.position.x, SubCamera.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_005()   // SubCamera を 005 の位置に移動する  1.43
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_005.transform.position.x, SubCamera.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_0_5()   // SubCamera を -5 の位置に移動する   10.5
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_0_5.transform.position.x, SubCamera.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void AppearSubCamera_Group()
    {
        Debug.Log("SubCamera サブカメラ 表示します");
        SubCamera_Group.SetActive(true);
    }
        
    public void CloseSubCamera_Group()
    {
        Debug.Log("SubCamera サブカメラ 非表示");
        SubCamera_Group.SetActive(false);
    }

    public void Right_PushDown()          //      右ボタンを押している間
    {
        ToRight_SubCamera = true;
    }

    public void Right_PushUp()            //      右ボタンを押すのをやめた時
    {
        ToRight_SubCamera = false;
    }

    public void Left_PushDown()         //      左ボタンを押している間

    {
        ToLeft_SubCamera = true;
    }

    public void Left_PushUp()          //      左ボタンを押すのをやめた時
    {        
        ToLeft_SubCamera = false;
    }

    public void SubCamera_GoRight()
    {
        if (SubCamera.transform.position.x <= GoalCorn_Head.transform.position.x)  // ゴールコーンより右に行かない限り
        {
            SubCamera.transform.position += new Vector3(5.0f * Time.deltaTime, 0, 0);        // SubCameraをx軸方向に秒速5.0fで動かす
        }
    }

    public void SubCamera_GoLeft()
    {
        if (SubCamera.transform.position.x >= StartCorn_Head.transform.position.x)  // スタートコーンより左に行かない限り
        { 
            SubCamera.transform.position += new Vector3(-5.0f * Time.deltaTime, 0, 0);      // SubCameraをx軸方向に秒速-5.0fで動かす
        }
    }

    #endregion

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
        BGM_SE_MSC.firstRead_Selectjanken = 0;
        BGM_SE_MSC.firstRead_TestRoomController = 0;
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
    public void AcutivePlayerNameIs(Player player)
    {
        Debug.Log("私の名前は 「" + player.NickName + " 」でござる");
    }
    */

    public void WhoAreYou()    // 私の名前（真名）を表示
    {
        Debug.Log("私の名前(真名)は・・・");
        Debug.Log("PhotonNetwork.NickName ランチャー：" + PhotonNetwork.NickName);
    }

    public void Judge_GOAL()   // ゴールラインに到達したか判定する
    {
        if(myPlayer.transform.position.x >= GoalCorn_Head.transform.position.x)
        {
            Debug.Log("GOOOOOALLL！！！！");
            Check_Champ_Avator();
            photonView.RPC("ShareGameSet", RpcTarget.All);    
        }
    }

    public void Check_Champ_Avator()
    {
        Debug.Log("チャンピョンのアバターをセットします。");
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            if (WinnerNum == 1)          // プレイヤー1 が勝利者
            {
                Debug.Log("P1 私がチャンプだ！！！！");
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            if (WinnerNum == 2)          // プレイヤー2 が勝利者
            {
                Debug.Log("P2 私がチャンプだ！！！！");
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            if (WinnerNum == 3)          // プレイヤー3 が勝利者
            {
                Debug.Log("P3 私がチャンプだ！！！！");
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            if (WinnerNum == 4)          // プレイヤー4 が勝利者
            {
                Debug.Log("P4 私がチャンプだ！！！！");
            }
        }
        SetMyAvator_ForChamp();  // チャンプ のアバターを ゴールパネル（表彰台）にセットします。
    }

    public void SetMyAvator_ForChamp()  // チャンプ のアバターを ゴールパネル（表彰台）にセットします。
    {
        if (int_conMyCharaAvatar == 1)  // うたこ
        {
            photonView.RPC("AppearWinner_avator_1", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2) // Unityちゃん
        {
            photonView.RPC("AppearWinner_avator_2", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3) // Pちゃん
        {
            photonView.RPC("AppearWinner_avator_3", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4) // モブちゃん
        {
            photonView.RPC("AppearWinner_avator_4", RpcTarget.All);
        }
        Debug.Log("チャンプ のアバターを ゴールパネル（表彰台）にセットしました。");
    }

    [PunRPC]
    public void ShareGameSet()   // GOAL して GameSet した旨を全員に共有する
    {
        Debug.Log("GOOOOOALLL！！！！");
        Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
        GameSet_Flg = true;
        Erase_Text_Announcement();
        AppearGameSet_LOGO();
        CloseOpenMyJankenPanel_Button();
        CloseDebug_Buttons();
        BGM_SE_MSC.Stop_BGM();
        BGM_SE_MSC.whistle_SE();
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3f, () => AfterGameSet());
    }

    public void AfterGameSet()
    {
        Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
        GameSet_Flg = true;
        Erase_Text_Announcement();
        AppearWinPanel();
        BGM_SE_MSC.Fanfare_solo_SE();
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(5f, () => BGM_SE_MSC.Fanfare_Roop_BGM());
    }

    public void AppearGameSet_LOGO()
    {
        GameSet_LOGO.SetActive(true);
    }

    public void CloseGameSet_LOGO()
    {
        GameSet_LOGO.SetActive(false);
    }

    public void AppearOpenMyJankenPanel_Button()
    {
        OpenMyJankenPanel_Button.SetActive(true);
    }

    public void CloseOpenMyJankenPanel_Button()
    {
        OpenMyJankenPanel_Button.SetActive(false);
    }

    public void AppearDebug_Buttons()
    {
        Debug_Buttons.SetActive(true);
    }

    public void CloseDebug_Buttons()
    {
        Debug_Buttons.SetActive(false);
    }

    public void AppearWinPanel()
    {
        WinPanel.SetActive(true);
    }

    public void CloseWinPanel()
    {
        WinPanel.SetActive(false);
    }

    [PunRPC]
    public void AppearWinner_avator_1()
    {
        Winner_avator_1.SetActive(true);

    }

    public void CloseWinner_avator_1()
    {
        Winner_avator_1.SetActive(false);
    }

    [PunRPC]
    public void AppearWinner_avator_2()
    {
        Winner_avator_2.SetActive(true);

    }

    public void CloseWinner_avator_2()
    {
        Winner_avator_2.SetActive(false);
    }

    [PunRPC]
    public void AppearWinner_avator_3()
    {
        Winner_avator_3.SetActive(true);

    }

    public void CloseWinner_avator_3()
    {
        Winner_avator_3.SetActive(false);
    }

    [PunRPC]
    public void AppearWinner_avator_4()
    {
        Winner_avator_4.SetActive(true);

    }

    public void CloseWinner_avator_4()
    {
        Winner_avator_4.SetActive(false);
    }

    public void AppearPanel_Intro()
    {
        Panel_Intro.SetActive(true);
    }
       
    public void ClosePanel_Intro()
    {
        Panel_Intro.SetActive(false);
    }
    // End

}