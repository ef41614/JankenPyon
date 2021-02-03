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
using UnityEngine.Events;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SelectJanken : MonoBehaviour, IPunObservable
{
    #region// 変数宣言
    //誰かがログインする度に生成するプレイヤーPrefab
    public GameObject playerPrefab_utako;
    public GameObject playerPrefab_unitychan;
    public GameObject playerPrefab_pchan;
    public GameObject playerPrefab_mobuchan;
    public GameObject playerPrefab_Zunko;
    [SerializeField] SortingGroup MysortingGroup;

    public GameObject Prefab_Encounter_ItemCard_UraUp;  // ジャンプ後に確率でエンカウントするアイテムカードの裏面
    GameObject Temp_Encounter_ItemCard_UraUp;           // ジャンプ後に確率でエンカウントするアイテムカードの裏面_Temp
    public GameObject MainCanvas;
    public GameObject Prefab_Encounter_ItemCard_Down;  // ジャンプ後に確率でエンカウントするアイテムカードの裏面_ダウン
    GameObject Temp_Encounter_ItemCard_Down;           // ジャンプ後に確率でエンカウントするアイテムカードの裏面_タウン_Temp

    public GameObject Encounter_ItemCard_UraUp;      // ジャンプ後に確率でエンカウントするアイテムカードの裏面
    public GameObject Encounter_ItemCard_Down;       // ジャンプ後に確率でエンカウントするアイテムカードの裏面_ダウン
    public GameObject Center_Mark;                   // 中央位置を示すためのマーカー
    public GameObject Upperr_Mark;                   // 画面上部位置を示すためのマーカー
    CardReverse CardReverseMSC;                      //スクリプト名 + このページ上でのニックネーム

    public Image ItemCard_Omote;   // アイテムカード表面デフォルト
    public Sprite Fatigue_Card;    // 疲労
    public Sprite Gold_Card;       // ゴールド
    public Sprite AnzenYoshi_Card;       // AnzenYoshi
    public Text text_Item_Setsumei;  // アイテムカードの説明
    public int Sum_AnzenYoshi_Card = 0;  // 安全ヨシカードの合計枚数

    public bool Katakori_to_SetWFlag = false;  // 肩こりのせいで白旗0～3枚
    public GameObject Katakori_Mark;           // 左に表示する肩こりマーク
    public GameObject HariQ_Button;
    public Text Text_Katakori_cure;
    public Text Text_Gold_fusoku_HariQ;
    public bool Katakori_hajimari_Flg = true;  // true：肩こり発症したばかり

    public Text text_Gold_Plus10;

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

    bool GoalFlg = false;

    public GameObject GoalCorn_Head;  // ゴールラインのコーン
    public GameObject GameSet_LOGO;
    public GameObject OpenMyJankenPanel_Button;   // 右上の開始ボタン
    public GameObject Debug_Buttons;  // デバッグ用のボタン
    public GameObject WinPanel;       // 優勝者決定後のパネル

    public GameObject Goal_Iwai_1;       // 優勝者決定後のお祝いメンバー
    public GameObject Goal_Iwai_2;       // 優勝者決定後のお祝いメンバー
    public GameObject Goal_Iwai_3;       // 優勝者決定後のお祝いメンバー

    public GameObject Winner_avator_1;
    public GameObject Winner_avator_2;
    public GameObject Winner_avator_3;
    public GameObject Winner_avator_4;
    public GameObject Winner_avator_5;

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

    public Sprite sprite_Gu;
    public Sprite sprite_Choki;
    public Sprite sprite_Pa;
    public Sprite sprite_King;
    public Sprite sprite_Dorei;
    public Sprite sprite_CardUra;
    public Sprite sprite_Muteki;
    public Sprite sprite_Wall;
    public Sprite sprite_WFlag;

    public Sprite sprite_Icon_utako;
    public Sprite sprite_Icon_Unitychan;
    public Sprite sprite_Icon_Pchan;
    public Sprite sprite_Icon_mobuchan;
    public Sprite sprite_Icon_Zunko;

    public Sprite sprite_Avator_Stand_utako;
    public Sprite sprite_Avator_Stand_Unitychan;
    public Sprite sprite_Avator_Stand_Pchan;
    public Sprite sprite_Avator_Stand_mobuchan;
    public Sprite sprite_Avator_Stand_Zunko;

    public Sprite sprite_Avator_Make_utako;
    public Sprite sprite_Avator_Make_Unitychan;
    public Sprite sprite_Avator_Make_Pchan;
    public Sprite sprite_Avator_Make_mobuchan;
    public Sprite sprite_Avator_Make_Zunko;

    public Sprite sprite_Avator_Kachi_utako;
    public Sprite sprite_Avator_Kachi_Unitychan;
    public Sprite sprite_Avator_Kachi_Pchan;
    public Sprite sprite_Avator_Kachi_mobuchan;
    public Sprite sprite_Avator_Kachi_Zunko;

    public Image MyTeImg_1;
    public Image MyTeImg_2;
    public Image MyTeImg_3;
    public Image MyTeImg_4;
    public Image MyTeImg_5;

    public float MyTeAlpha_1 = 255;   // 透明度の値
    public float MyTeAlpha_2 = 255;
    public float MyTeAlpha_3 = 255;
    public float MyTeAlpha_4 = 255;
    public float MyTeAlpha_5 = 255;

    public Image Img_CoverBlack_P1;
    public Image Img_CoverBlack_P2;
    public Image Img_CoverBlack_P3;
    public Image Img_CoverBlack_P4;

    public GameObject Make_Black1;
    public GameObject Make_Black2;
    public GameObject Make_Black3;
    public GameObject Make_Black4;

    public GameObject Kachi_White1;
    public GameObject Kachi_White2;
    public GameObject Kachi_White3;
    public GameObject Kachi_White4;

    public GameObject Logout_Kakejiku1;
    public GameObject Logout_Kakejiku2;
    public GameObject Logout_Kakejiku3;
    public GameObject Logout_Kakejiku4;

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

    public Image Img_Player1_Avator_underJankenTe;
    public Image Img_Player2_Avator_underJankenTe;
    public Image Img_Player3_Avator_underJankenTe;
    public Image Img_Player4_Avator_underJankenTe;

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
    int int_TochuTaiseki = 0;               // 途中退席した人の人数をカウントして、後で SankaNinzu から引く
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
    public bool NoneKing = false;
    public bool NoneDorei = false;
    public bool NoneMuteki = false;
    public bool NoneWall = false;
    public bool NoneWFlag = false;
    public bool bool_CanDo_Hantei_Stream = false;  // 勝敗判定（Hantei_Stream） を実行できるかのフラグ

    int int_WFlagP1 = 0;
    int int_WFlagP2 = 0;
    int int_WFlagP3 = 0;
    int int_WFlagP4 = 0;
    int NumWFlag_AllPlayer = 0;

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
    public GameObject ZunkoClone; //ヒエラルキー上のオブジェクト名
    PlayerScript PlayerSC;//スクリプト名 + このページ上でのニックネーム


    public GameObject MainCamera; //ヒエラルキー上のオブジェクト名
    MyCameraController MyCameraControllerMSC;//スクリプト名 + このページ上でのニックネーム

    public GameObject MyKage; //ヒエラルキー上のオブジェクト名
    MyKageController MyKageControllerMSC;//スクリプト名 + このページ上でのニックネーム

    public GameObject Text_MyHeadName; //ヒエラルキー上のオブジェクト名
    MyHeadNameController MyHeadNameControllerMSC;//スクリプト名 + このページ上でのニックネーム

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
    public Button Btn_StockCard_Up;
    public Button Btn_StockCard_Down;
    public Button Btn_Omakase;    
    public Button Btn_Redistribute;  // じゃんけんカードの再配布ボタン

    public int CanPushBtn_A = 0; // 0:true, 1:false
    public int CanPushBtn_B = 0; // 0:true, 1:false
    public int CanPushBtn_C = 0; // 0:true, 1:false
    public int CanPushBtn_D = 0; // 0:true, 1:false
    public int CanPushBtn_E = 0; // 0:true, 1:false
    public int CanPushBtn_StockCard_Up = 0;
    public int CanPushBtn_StockCard_Down = 0;
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
    private float currentTime10 = 0f; // test

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

    bool ToRight_SubCamera = false;    //  右ボタンを押しているかの真偽値（フラグ）
    bool ToLeft_SubCamera = false;     //  左ボタンを押しているかの真偽値（フラグ）

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

    public GameObject Taihou;
    public ParticleSystem Taihou_Bakuhatsu;
    //public ParticleSystem Taihou_Kemuri;
    public GameObject Panel_SyokaiTaihou;

    int Flg_AwakeDone = 0;
    int Flg_StartDone = 0;
    int Flg_IkemasuDone = 0;
    int Flg_StartGameMatchDone = 0;
    int Flg_OneLoopDone = 0;
    int Flg_AfterJumpDone = 0;
    int Flg_FromWin_ToJumpDone = 0;

    public Text Error01_Text;
    public Text Error02_Text;
    public Text Error03_Text;
    public Text Error04_Text;
    public Text Error05_Text;
    public Text Error06_Text;

    public Text Pos_Hasshin_Text;

    float X_dis30po = 0;
    float X_dis_betweenTop = 0;  // 自分と首位とのX軸距離
    float PosX_TopPlayer = 0;    // 首位のX軸距離
    float PosX_MyPlayer = 0;     // 自分のX軸距離
    float PosX_Player1;
    float PosX_Player2;
    float PosX_Player3;
    float PosX_Player4;
    float PosX_BottomPlayer = 0;
    float CourseLength = 30;     // コース全長

    int PosX_Koshin_PlayerNum = 0;      // 誰のX軸座標を更新するの？
    int PosX_Koshin_Atai = 0;         // 数値はいくつ？
    int PosX_Koshin_Atai_10keta = 0;
    int PosX_Koshin_Atai_1keta = 0;
    public float PosX_TaihouFlyer;      // 人間大砲で飛ぶ人のX軸距離
                                        //float CanTaihou_Distance;           // 大砲ボタンを出現させるのに必要な距離
    float receivePosX_Player1;
    float receivePosX_Player2;
    float receivePosX_Player3;
    float receivePosX_Player4;

    public SelectJanken SelectJankenMSC;

    public Text text_PosX_realP1;
    public Text text_PosX_realP2;
    public Text text_PosX_realP3;
    public Text text_PosX_realP4;

    public Text text_PosX_P1;
    public Text text_PosX_P2;
    public Text text_PosX_P3;
    public Text text_PosX_P4;
    bool Flg_Update_PosX = false;
    public bool Flg_CanUseTaihou = false;
    public GameObject Button_TaihouFire;
    public float flo_str = 0.1f;
    public int int_vib = 5;
    public float flo_ran = 30;
    public bool Flg_Taihou_punch = false;

    public GameObject Panel_ToTitle;

    public GameObject Ninja_Button;
    public GameObject Syoji_Panel;   // 忍者が相手の手札を覗く時に表示される障子穴のパネル

    int Gold_MyPlayer = 30;     // 自分の所持金（ゴールド）
    int Gold_Player1 = 30;
    int Gold_Player2 = 30;
    int Gold_Player3 = 30;
    int Gold_Player4 = 30;

    public Text text_MyGold;
    public Text text_Gold_P1;
    public Text text_Gold_P2;
    public Text text_Gold_P3;
    public Text text_Gold_P4;
    public Text Text_Gold_fusoku;

    int int_calculation_Gold;  // 現在の所持金（ゴールド）にマイナス/プラスする数値

    public GameObject Sara;
    public GameObject Tarai;
    public ParticleSystem Sara_Guwan;
    public ParticleSystem Tarai_Guwan;
    float PosY_taraiSet;
    float PosY_GuwanSet;
    public float flo_sky_taraiSet = 7.0f;   // 頭上何メートルにたらいをセットするか
    float realPosX_Player1;   // スタートマーカーの値を引かない、プレイヤー単体の値（X軸座標）
    float realPosX_Player2;
    float realPosX_Player3;
    float realPosX_Player4;

    public GameObject Image_Group_Introp;
    public GameObject Text_Group_Intro;

    public bool Tarai_to_SetWFlag = false;  // たらいが落ちると、確定で白旗一枚

    bool logon_player1 = true;  // false の時、ログオフ状態である
    bool logon_player2 = true;
    bool logon_player3 = true;
    bool logon_player4 = true;

    bool Flg_before_Hantei_Stream = true;           // 勝敗判定（Hantei_Stream） 実行前ならtrue、実行始まったらfalse
    bool Flg_before_Check_NowLoginMember = true;    // 実行前ならtrue、実行始まったらfalse
    bool Flg_PreCheck_Can_Hantei_Stream = true;     // 実行前ならtrue、実行始まったらfalse

    int int_Default_Life = 8;  //★
    int Life_MyPlayer = 0;     // 自分の体力
    int Life_Player1;
    int Life_Player2;
    int Life_Player3;
    int Life_Player4;
    int int_calculation_Life;  // 現在の体力 にマイナス/プラスする数値

    int Life_Koshin_PlayerNum = 0;      // 誰のLifeを更新するの？
    int Life_Koshin_Atai = 0;           // 数値はいくつ？

    public GameObject Heart1_P1;
    public GameObject Heart2_P1;
    public GameObject Heart3_P1;
    public GameObject Heart4_P1;
    public GameObject Heart5_P1;
    public GameObject Heart6_P1;
    public GameObject Heart7_P1;
    public GameObject Heart8_P1;

    public GameObject Heart1_P2;
    public GameObject Heart2_P2;
    public GameObject Heart3_P2;
    public GameObject Heart4_P2;
    public GameObject Heart5_P2;
    public GameObject Heart6_P2;
    public GameObject Heart7_P2;
    public GameObject Heart8_P2;

    public GameObject Heart1_P3;
    public GameObject Heart2_P3;
    public GameObject Heart3_P3;
    public GameObject Heart4_P3;
    public GameObject Heart5_P3;
    public GameObject Heart6_P3;
    public GameObject Heart7_P3;
    public GameObject Heart8_P3;

    public GameObject Heart1_P4;
    public GameObject Heart2_P4;
    public GameObject Heart3_P4;
    public GameObject Heart4_P4;
    public GameObject Heart5_P4;
    public GameObject Heart6_P4;
    public GameObject Heart7_P4;
    public GameObject Heart8_P4;

    public GameObject Heart_Damage_P1;
    public GameObject Heart_Damage_P2;
    public GameObject Heart_Damage_P3;
    public GameObject Heart_Damage_P4;

    public CanvasGroup canvas_Damage_P1;
    public CanvasGroup canvas_Damage_P2;
    public CanvasGroup canvas_Damage_P3;
    public CanvasGroup canvas_Damage_P4;

    public Text text_Life_P1;
    public Text text_Life_P2;
    public Text text_Life_P3;
    public Text text_Life_P4;

    public GameObject Panel_AutoLogout;
    int Level_MyHealing = 0;  // 体力がゼロになってから回復するまでの治療の程度 （治療レベル）
    public GameObject Panel_Kizetsu;

    public GameObject Stage_kaidou;
    public GameObject Stage_iseki;
    public GameObject NightTown;
    public GameObject ActivePark;

    #endregion

    #region // 【START】初期設定の処理一覧

    void Awake()
    {
        if (Flg_AwakeDone == 0)
        {
            Flg_AwakeDone = 1;
            BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
            BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();

            this.photonView = GetComponent<PhotonView>();
            Debug.Log("【START-01】SelectJanken void Awake() 出席確認1");
            //Debug.Log("【START-01】キャラ アバター 誰を選んだかを（ログイン前画面から）コンバートします");
            int_conMyCharaAvatar = CLauncherScript.get_int_MyCharaAvatar(); // 【START-01】キャラ アバター 誰を選んだか（ログイン前画面からコンバートする）

            //Debug.Log("【START-02】int_conMyCharaAvatar（★キャラアバター） ： " + int_conMyCharaAvatar);

            //Debug.Log("【START-03】他スクリプトと連携できるようにします");
            ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
            TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
            MyCameraControllerMSC = MainCamera.GetComponent<MyCameraController>();
            CloseWinPanel();
            CloseDebug_Buttons();
            ClosePanel_Intro();
            ClosePanel_ToTitle();

            AppearLogout_Kakejiku_All();      // すべての掛け軸を開く

            if (BGM_SE_MSC.firstMatch <= 3)
            {
                AppearPanel_Intro();
            }
            text_Game_kaishi_MAE.text = "しあい かいし まえ";
            text_Game_kaishi_CHU.text = "";
            //audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Awake 処理 ダブってます！！！");
            Error01_Text.text = "Awake処理ダブり";
        }
    }


    void Start()
    {
        if (Flg_StartDone == 0)
        {
            Flg_StartDone = 1;
            //var customProperties = photonView.Owner.CustomProperties;
            //Debug.Log("【START-03】SelectJanken  void Start() 出席確認2");
            ClosePanel_Ikemasu();
            myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");

            //Debug.Log("【START-03】 各変数を 初期化（リセット）します");
            count_a = 1;
            //Debug.Log("【START-03】 各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする");
            ResetAlivePlayer();            //【START-03】 各種 生存者カウンター リセット（全員の aliveフラグ を 1 にする

            //Debug.Log("【START-04】自プレイヤーを生成します");
            //Debug.Log("CreatePlayerPrefab_Flg ： " + CreatePlayerPrefab_Flg);


            //Debug.Log("myPlayer が 存在していなかったのでキャラ作成します！！！");
            if (CreatePlayerPrefab_Flg)
            {
                CreatePlayerPrefab();          //【START-04】Photonに接続していれば自プレイヤーを生成
            }
            CreatePlayerPrefab_Flg = false;
            BGM_SE_MSC.firstRead_Selectjanken = 1;

            myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");

            //Debug.Log("【START-05】スタートラインにランダムに移動させます");
            MoveToStartLineRandom();       //【START-05】スタートラインにランダムに移動させる

            //Debug.Log("【START-06】MyPlayer にカメラを追従するようにセットします");
            MyCameraControllerMSC.SetMyCamera();  //【START-06】MyPlayer にカメラを追従するようにセット

            MyKage = GameObject.FindWithTag("MyKage");
            MyKageControllerMSC = MyKage.GetComponent<MyKageController>();

            //Debug.Log("【START-07】スタート時 初期設定を全プレイヤーで共有する（座標、顔アイコン、頭上プレイヤー名）");
            ToShare_InitialSetting();     // 【START-07】スタート時 初期設定を全プレイヤーで共有
            Check_KageDistance();         //  MyKage と MyPlayer の距離を求める（Y軸の初期位置）

            //Debug.Log("【START-09】ジャンケン手「決定ボタン」を表示できるか確認します");
            Check_CanAppear_KetteiBtn();  // 【START-09】ジャンケン手「決定ボタン」を表示できるか確認

            //Debug.Log("【START-10】総参加人数 と 現在待機中の総人数 をチェックします");
            NinzuCheck();                 // 【START-10】総参加人数 と 現在待機中の総人数  && 2人以上で「いけますパネル」を表示
            NumLivePlayer = SankaNinzu;
            //Debug.Log("【START-11】NumLivePlayer（総参加人数＝生存者数） は " + NumLivePlayer);

            //Debug.Log("【START-12】右上の開始ボタンを押せるように各値をリセット ⇒ 全員に共有する");
            ShareAfterJump();   //【START-12】右上の開始ボタンを押せるように各値をリセット ⇒ 全員に共有する

            BattleStage_Set();                 // Battle シーンの 背景 と BGM をセットします

            StartSet_Life_Players();   // 体力をセットします[初期値のセット]
            CloseStartLogo();
            CloseAisatsu_Panel();
            CloseSyoji_Panel();
            CloseNinja_Button();
            Reset_AllAisatsu();
            CloseTarai();
            CloseSara();
            ClosePanel_AutoLogout();
            CloseBarrier();
            HariQ_Button.SetActive(false);    //非表示にする
            Katakori_Mark.SetActive(false);   //非表示にする

            CheckStart_GameMatch();                 // 試合開始できるか確認する処理
                                                    //var sequence = DOTween.Sequence();
                                                    //sequence.InsertCallback(5f, () => AppearPanel_Ikemasu());
                                                    //MoveTo_cafe_kanban_0_5();   // SubCamera を -5 の位置に移動する
            CloseSubCamera_Group();
            CloseEncounter_ItemCard_Down();
            CloseEncounter_ItemCard_UraUp();
            ClosePanel_Kizetsu();
            Button_TaihouFire.SetActive(false);
            AppearPanel_SyokaiTaihou();

            X_dis30po = cafe_kanban_005.transform.position.x - cafe_kanban_035.transform.position.x;
            //Debug.Log("X_dis30po : " + X_dis30po);

            SelectJankenMSC = myPlayer.GetComponent<SelectJanken>();
            Pos_Hasshin_Text.text = "";

            //Debug.Log("アイテムカードの裏面生成！");
            //Debug.Log("アイテムカードの裏面生成できたかな？？");
        }
        else
        {
            Debug.LogError("Start 処理 ダブってます！！！");
            Error02_Text.text = "Start処理ダブり";
        }
    }


    void Update()
    {
        // test
        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            text_PosX_P1.text =(CourseLength - PosX_Player1).ToString();
            text_PosX_P2.text =(CourseLength - PosX_Player2).ToString();
            text_PosX_P3.text =(CourseLength - PosX_Player3).ToString();
            text_PosX_P4.text =(CourseLength - PosX_Player4).ToString();

            text_MyGold.text = Gold_MyPlayer.ToString();


            if (Shiai_Kaishi)
            {
                text_Room_shimekiri.text = "試合開始したので、ルームへの入室をしめきりました";
                //Debug.Log(SankaNinzu + ": SankaNinzu");

                //Debug.Log("alivePlayer1 ： " + alivePlayer1);
                //Debug.Log("alivePlayer2 ： " + alivePlayer2);
                //Debug.Log("alivePlayer3 ： " + alivePlayer3);
                //Debug.Log("alivePlayer4 ： " + alivePlayer4);

                //Debug.Log("logon_player1 ： " + logon_player1);
                //Debug.Log("logon_player2 ： " + logon_player2);
                //Debug.Log("logon_player3 ： " + logon_player3);
                //Debug.Log("logon_player4 ： " + logon_player4);

                //Debug.Log("Flg_PreCheck_Can_Hantei_Stream ： " + Flg_PreCheck_Can_Hantei_Stream);


                if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)               // ジャンケンパネルが既に表示されていたら
                {
                    //CloseMyKakejiku();    // 自分はログインしているので、掛け軸外しますよ
                }
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

                if (GoalFlg)  // プレイヤーがゴールしたら
                {
                    if (Encounter_ItemCard_Down.activeSelf)   // アクティブだったら
                    {
                        CloseEncounter_ItemCard_Down();       // アイテムカード閉じる
                    }
                    if (HariQ_Button.activeSelf)   // アクティブだったら
                    {
                        HariQ_Button.SetActive(false);    //非表示にする
                    }
                }
                else  // まだ試合中なら
                {
                    if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)  // ジャンケンパネルが既に表示されていたら
                    {
                        HariQ_Button.SetActive(false);    //非表示にする
                    }
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

        }
    }

    #region // BattleStage_Set ステージセット
    [PunRPC]
    public void BattleStage_Set()     // Battle シーンの 背景 と BGM をセットします
    {
        Close_All_Stage();
        if (BGM_SE_MSC.stage_No == 0)
        {
            AppearStage_kaidou();
            BGM_SE_MSC.FunAndLight_BGM();       // Battle シーンBGM「街の街道」
        }
        else if (BGM_SE_MSC.stage_No == 1)
        {
            AppearStage_iseki();
            BGM_SE_MSC.iseki_ogg_BGM();         // Battle シーンBGM「ピラミッド（遺跡）」
        }
        else if (BGM_SE_MSC.stage_No == 2)
        {
            AppearNightTown();
            BGM_SE_MSC.NightTown_funk_BGM();    // Battle シーンBGM「ナイトタウン」
        }
        else if (BGM_SE_MSC.stage_No == 3)
        {
            AppearActivePark();
            BGM_SE_MSC.AcrivePark_jog_BGM();    // Battle シーンBGM「運動公園」
        }
    }

    public void Close_All_Stage()   
    {
        CloseStage_kaidou();
        CloseStage_iseki();
        CloseNightTown();
        CloseActivePark();
    }

    public void AppearStage_kaidou()  // 街道
    {
        Stage_kaidou.SetActive(true);
    }

    public void CloseStage_kaidou()  
    {
        Stage_kaidou.SetActive(false);
    }

    public void AppearStage_iseki()  // ピラミッド（遺跡）
    {
        Stage_iseki.SetActive(true);
    }

    public void CloseStage_iseki()
    {
        Stage_iseki.SetActive(false);
    }

    public void AppearNightTown()  // ナイトタウン
    {
        NightTown.SetActive(true);
    }

    public void CloseNightTown()
    {
        NightTown.SetActive(false);
    }


    public void AppearActivePark()  // 運動公園
    {
        ActivePark.SetActive(true);
    }

    public void CloseActivePark()
    {
        ActivePark.SetActive(false);
    }


    #region// ステージ情報共有                    // Battle シーンに移ったタイミングで
    public void ToShareStageNo()                  // ステージ情報共有（ログインしてきた他プレイヤーに、既に決定している StageNo を共有する）
    {
        if (BGM_SE_MSC.stage_No == 0)             // 街道
        {
            photonView.RPC("ShareStageNo_00", RpcTarget.Others);
        }
        else if (BGM_SE_MSC.stage_No == 1)       // Battle シーンBGM「ピラミッド（遺跡）」
        {
            photonView.RPC("ShareStageNo_01", RpcTarget.Others);
        }
        else if (BGM_SE_MSC.stage_No == 2)     // Battle シーンBGM「ナイトタウン」
        {
            photonView.RPC("ShareStageNo_02", RpcTarget.Others);
        }
        else if (BGM_SE_MSC.stage_No == 3)     // Battle シーンBGM「運動公園」
        {
            photonView.RPC("ShareStageNo_03", RpcTarget.Others);
        }
        photonView.RPC("BattleStage_Set", RpcTarget.Others);  // Battle シーンの 背景 と BGM をセットします
    }

    [PunRPC]
    public void ShareStageNo_00()
    {
        BGM_SE_MSC.stage_No = 0;
    }

    [PunRPC]
    public void ShareStageNo_01()
    {
        BGM_SE_MSC.stage_No = 1;
    }

    [PunRPC]
    public void ShareStageNo_02()
    {
        BGM_SE_MSC.stage_No = 2;
    }

    [PunRPC]
    public void ShareStageNo_03()
    {
        BGM_SE_MSC.stage_No = 3;
    }

    #endregion

    #endregion


    public void shareChangeFlg_PreCheck_Can_Hantei_Stream_True()   // 実行前ならtrue、実行始まったらfalse
    {
        photonView.RPC("ChangeFlg_PreCheck_Can_Hantei_Stream_True", RpcTarget.All);
    }

    [PunRPC]
    public void ChangeFlg_PreCheck_Can_Hantei_Stream_True()   // 実行前ならtrue、実行始まったらfalse
    {
        Flg_PreCheck_Can_Hantei_Stream = true;
    }

    [PunRPC]
    public void ChangeFlg_PreCheck_Can_Hantei_Stream_False()   // 実行前ならtrue、実行始まったらfalse
    {
        Flg_PreCheck_Can_Hantei_Stream = false;
    }

    public void CreatePlayerPrefab()    //【START-04】Photonに接続していれば自プレイヤーを生成
    {
        CreatePlayerPrefab_Flg = false;

        //Debug.Log("【START-04】Photonに接続したので 自プレイヤーを生成");

        if (int_conMyCharaAvatar == 1)  // うたこ
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_utako.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            utakoClone = GameObject.FindWithTag("MyPlayer");
            //Debug.Log("utakoClone の名前は: " + utakoClone.name);
            PlayerSC = utakoClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 2) // Unityちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_unitychan.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            unitychanClone = GameObject.FindWithTag("MyPlayer");
            //Debug.Log("unitychanClone の名前は: " + unitychanClone.name);
            PlayerSC = unitychanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 3) // Pちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_pchan.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            pchanClone = GameObject.FindWithTag("MyPlayer");
            //Debug.Log("pchanClone の名前は: " + pchanClone.name);
            PlayerSC = pchanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 4) // モブちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_mobuchan.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            mobuchanClone = GameObject.FindWithTag("MyPlayer");
            //Debug.Log("mobuchanClone の名前は: " + mobuchanClone.name);
            PlayerSC = mobuchanClone.GetComponent<PlayerScript>();
        }
        else if (int_conMyCharaAvatar == 5) // ずん子ちゃん
        {
            myPlayer = PhotonNetwork.Instantiate(playerPrefab_Zunko.name, new Vector3(-15f, 0f, 0f), Quaternion.identity, 0);
            ZunkoClone = GameObject.FindWithTag("MyPlayer");
            //Debug.Log("ZunkoClone の名前は: " + ZunkoClone.name);
            PlayerSC = ZunkoClone.GetComponent<PlayerScript>();
        }
        else
        {
            //Debug.Log("【START-04】自プレイヤーを生成 できませんでした");
        }
    }


    #region // 【START-07】Battleシーン遷移後、初期設定・配置の処理一覧（アイコンのセット等）
    public void ToShare_InitialSetting()    // 【START-07】スタート時 初期設定を全プレイヤーで共有
    {
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Share_InitialSetting();             // 【START-07】スタート時 初期設定を全プレイヤーで共有する（座標、顔アイコン、頭上プレイヤー名）
        if (int_MatchPlayerMaxNum > 4)      // マッチ人数が5人以上であるならば
        {
            //Debug.Log("【START-08】マッチ人数が5人以上のため、スタートラインにランダムに移動させます");
            MoveToStartLineRandom();        // スタートラインにランダムに移動させる
            //Debug.Log("【START-08】マッチ人数が5人以上のため、スタートラインにランダムに移動させました");
        }
    }

    public void Share_InitialSetting()     //【START-07】スタート時 初期設定を全プレイヤーで共有する（座標、顔アイコン、頭上プレイヤー名）
    {
        //Debug.Log("【START-07】* Share_InitialSetting 実行 *");
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("AcutivePlayerName  " + AcutivePlayerName);
        //Debug.Log("AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            //Debug.Log("【START-07】プレイヤー1のアイコンをセットします");
            SharePlayerIcon_Player1();
            //Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark1();                // 【START-07】スタートマーク1 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 1;    // order in layer の順番調整に使用する整数
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            //Debug.Log("【START-07】プレイヤー2のアイコンをセットします");
            SharePlayerIcon_Player2();
            //Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark2();                // 【START-07】スタートマーク2 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 2;    // order in layer の順番調整に使用する整数
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            //Debug.Log("【START-07】プレイヤー3のアイコンをセットします");
            SharePlayerIcon_Player3();
            //Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark3();                // 【START-07】スタートマーク3 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 3;    // order in layer の順番調整に使用する整数
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            //Debug.Log("【START-07】プレイヤー4のアイコンをセットします");
            SharePlayerIcon_Player4();
            //Debug.Log("【START-07】スタートマーク の位置へ移動します");
            MoveToStartMark4();                // 【START-07】スタートマーク4 の位置へ移動する
            PlayerSC.int_MySpriteOrder = 4;    // order in layer の順番調整に使用する整数
        }

        //Debug.Log("【START-07】MyPlayer に かげ を追従するようにセットします");
        MyKageControllerMSC.SetMyKage();       // MyPlayer に かげ を追従するようにセット

        //Debug.Log("【START-07】order in layer （画像表示順）の順番調整をします");
        PlayerSC.SortMySpriteOrder();          // order in layer （画像表示順）の順番調整を実施する

        //Debug.Log("【START-07】名前とアイコンをセットします");
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
        else if (int_conMyCharaAvatar == 5) // ずん子ちゃん
        {
            Img_MyIcon_SelectPanel.gameObject.GetComponent<Image>().sprite = sprite_Icon_Zunko;
            Img_Head_MyIcon.gameObject.GetComponent<Image>().sprite = sprite_Icon_Zunko;
        }
        //Debug.Log("【START-07】私のアイコンをセレクトパネルにセットしました");
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
        //Debug.Log("【START-05】プレイヤーをスタートラインにランダムに移動(配置)させました");
    }

    public void Share_AcutivePlayerID() // 【START-07】【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    {
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
    }

    /// <summary>
    /// 【START-07】【JK-11_1】現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    /// </summary>
    /// <param name="mi">現プレイヤー名とプレイヤーID 取得、共有</param>
    //[PunRPC]
    public void PlayerIDCheck(PhotonMessageInfo mi)  // 【START-07】【JK-11_1】
    {
        //Debug.Log("【START-07】【JK-11_1】[PunRPC] PlayerIDCheck");

        if (mi.Sender != null)
        {
            senderName = mi.Sender.NickName;
            senderID = mi.Sender.UserId;
            AcutivePlayerName = senderName;
            AcutivePlayerID = senderID;
        }
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("AcutivePlayerName  " + AcutivePlayerName);  // 今ボタン押した人
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
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("SetIconP1_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー1のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP1_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_utako;
    }
    [PunRPC]
    public void SetIconP1_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Unitychan;
    }
    [PunRPC]
    public void SetIconP1_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Pchan;
    }
    [PunRPC]
    public void SetIconP1_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_mobuchan;
    }
    [PunRPC]
    public void SetIconP1_Zunko()                     // 【START-07】アイコンを ずん子ちゃん にセット
    {
        Img_Icon_Player1.gameObject.GetComponent<Image>().sprite = sprite_Icon_Zunko;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Zunko;
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
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("SetIconP2_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー2のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP2_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_utako;
    }
    [PunRPC]
    public void SetIconP2_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Unitychan;
    }
    [PunRPC]
    public void SetIconP2_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Pchan;
    }
    [PunRPC]
    public void SetIconP2_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_mobuchan;
    }
    [PunRPC]
    public void SetIconP2_Zunko()                     // 【START-07】アイコンを ずん子ちゃん にセット
    {
        Img_Icon_Player2.gameObject.GetComponent<Image>().sprite = sprite_Icon_Zunko;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Zunko;
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
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("SetIconP3_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー3のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP3_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_utako;
    }
    [PunRPC]
    public void SetIconP3_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Unitychan;
    }
    [PunRPC]
    public void SetIconP3_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Pchan;
    }
    [PunRPC]
    public void SetIconP3_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_mobuchan;
    }
    [PunRPC]
    public void SetIconP3_Zunko()                     // 【START-07】アイコンを ずん子ちゃん にセット
    {
        Img_Icon_Player3.gameObject.GetComponent<Image>().sprite = sprite_Icon_Zunko;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Zunko;
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
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("SetIconP4_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー4のアイコンをセットしました");
    }
    [PunRPC]
    public void SetIconP4_utako()                     // 【START-07】アイコンを うたこ にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_utako;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_utako;
    }
    [PunRPC]
    public void SetIconP4_Unitychan()                 // 【START-07】アイコンを Unityちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_Unitychan;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Unitychan;
    }
    [PunRPC]
    public void SetIconP4_Pchan()                     // 【START-07】アイコンを Pちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_Pchan;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Pchan;
    }
    [PunRPC]
    public void SetIconP4_mobuchan()                  // 【START-07】アイコンを モブちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_mobuchan;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_mobuchan;
    }
    [PunRPC]
    public void SetIconP4_Zunko()                     // 【START-07】アイコンを ずん子ちゃん にセット
    {
        Img_Icon_Player4.gameObject.GetComponent<Image>().sprite = sprite_Icon_Zunko;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Stand_Zunko;
    }

    public void NinzuCheck()                          // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します ＆＆ 忍者ボタンの ON/OFF
    {
        //Debug.Log("【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します");
        TestRoomControllerSC.PNameCheck();            // 現在の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        SankaNinzu = TestRoomControllerSC.int_JoinedPlayerAllNum;  // 総参加人数 を更新
        SankaNinzu = SankaNinzu - int_TochuTaiseki;   // 途中退席した人の人数をカウントして、後で SankaNinzu から引く

        if (logon_player1 == false)   // player1 が退出しました
        {
            int_NowWaiting_Player1 = 0;  // これ以降、常に待機フラグが OFFになる
        }
        if (logon_player2 == false)
        {
            int_NowWaiting_Player2 = 0;
        }
        if (logon_player3 == false)
        {
            int_NowWaiting_Player3 = 0;
        }
        if (logon_player4 == false)
        {
            int_NowWaiting_Player4 = 0;
        }

        int_WaitingPlayers_All = int_NowWaiting_Player1 + int_NowWaiting_Player2 + int_NowWaiting_Player3 + int_NowWaiting_Player4;   // 現在待機中の総人数 を更新
        //Debug.Log("総参加人数（SankaNinzu） ： " + SankaNinzu);

        //Debug.Log("int_NowWaiting_Player1 ： " + int_NowWaiting_Player1);
        //Debug.Log("int_NowWaiting_Player2 ： " + int_NowWaiting_Player2);
        //Debug.Log("int_NowWaiting_Player3 ： " + int_NowWaiting_Player3);
        //Debug.Log("int_NowWaiting_Player4 ： " + int_NowWaiting_Player4);

        //Debug.Log("現在待機中の総人数（int_WaitingPlayers_All） ： " + int_WaitingPlayers_All);
        if (SankaNinzu >= 1 && (Shiai_Kaishi == false))  // 試合開始まえであれば
        {
            //AppearPanel_Ikemasu();  // いけます ボタン(パネル) 参加人数2人以上になったら表示する
            photonView.RPC("AppearPanel_Ikemasu", RpcTarget.All);
        }
        if (int_WaitingPlayers_All >= 1)  // 待機人数が1人以上であれば
        {
            AppearNinja_Button();
        }
        else                             // 全員待機前ならば
        {
            CloseNinja_Button();
        }
    }

    public void AllPlayerWaiting_Forcibly()
    {
        //Debug.Log("強制的に全員の待機フラグを 1 （待機中）にします");
        int_WaitingPlayers_All = SankaNinzu;
    }


    public void ToShareNinzu_2()   // 参加人数 2名 は居る → 全員に共有
    {
        //Debug.Log("ToShare 参加人数 2名 は居る");
        photonView.RPC("ShareNinzu_2", RpcTarget.All);
    }

    public void ToShareNinzu_3()   // 参加人数 3名 は居る → 全員に共有
    {
        //Debug.Log("ToShare 参加人数 3名 は居る");
        photonView.RPC("ShareNinzu_3", RpcTarget.All);
    }

    public void ToShareNinzu_4()   // 参加人数 4名 は居る → 全員に共有
    {
        //Debug.Log("ToShare 参加人数 4名 は居る");
        photonView.RPC("ShareNinzu_4", RpcTarget.All);
    }

    [PunRPC]
    public void ShareNinzu_2()   // 参加人数 2名 は居る → 全員に共有
    {
        //Debug.Log("参加人数 2名 は居る → 全員に共有");
        NumLivePlayer = 2;
        SankaNinzu = 2;
    }

    [PunRPC]
    public void ShareNinzu_3()   // 参加人数 3名 は居る → 全員に共有
    {
        //Debug.Log("参加人数 3名 は居る → 全員に共有");
        NumLivePlayer = 3;
        SankaNinzu = 3;
    }

    [PunRPC]
    public void ShareNinzu_4()   // 参加人数 4名 は居る → 全員に共有
    {
        //Debug.Log("参加人数 4名 は居る → 全員に共有");
        NumLivePlayer = 4;
        SankaNinzu = 4;
    }
    #endregion

    #endregion

    #region // 右上の「開始ボタン」（ジャンケンパネル オープン）を自動で押す処理

    public void Countdown_Until_Push_OpenMyJankenPanel_Button()   // ジャンケンパネルが開かれていないならば、カウントダウン開始
    {
        WhoIsTopPlayer();               // 各プレイヤーのX軸位置を比較し、現在の首位と、自分との距離を算出する
        CheckCanUseTaihou();            // 人間大砲が撃てるか確認します
        Update_Life_Players();          // 各プレイヤーのLifeを同期します

        //Debug.Log("Countdown_timer_PanelOpen : " + Countdown_timer_PanelOpen);
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)  // ジャンケンパネルが既に表示されていたら
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;  // ボタンフラグをOFFにする
            Erase_Text_Announcement();
        }
        //Debug.Log("右上の「開始ボタン」を自動で押す:ジャンケンパネルが開かれていない（アクティブでない）ならば、カウントダウン開始");
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg && GameSet_Flg == false)
        {
            //Debug.Log("右上の「開始ボタン」を自動で押す:ジャンケンパネルが開かれていないので、ボタンを おしてね Text");
            Text_Announcement.text = "ボタンを おしてね";   // テキスト表示

            if (Life_MyPlayer <= 0)        // 体力がゼロになっているなら（気絶していたら）
            {
                var sequence30 = DOTween.Sequence();
                sequence30.InsertCallback(10f, () => Auto_Push_OpenMyJankenPanel_Button());
            }
            else                           // 体力が1以上あれば（平常時）
            {          
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
                    //Debug.Log("右上「開始ボタン」の Countdown_timer_PanelOpen が 1～6 以外です");
                    ResetCountdown_timer_PanelOpen_1();
                }
            }
        }
        else
        {
            //Debug.Log("右上「開始ボタン」を自動で押す:既にジャンケンパネルが開かれているようです・・・");
        }
    }

    public void ResetCountdown_timer_PanelOpen_1()
    {
        Countdown_timer_PanelOpen = 1;
    }

    public void Checking_PanelOpen(int timeHyoji)
    {
        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)  // 既にジャンケンパネルが開いていたら
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        //Debug.Log("右上「開始ボタン」のカウントダウン：" + timeHyoji);
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg && GameSet_Flg == false)
        {
            if (timeHyoji == 5 || timeHyoji == 20)
            {
                bottonwo_oshitene_SE();   // ボタンを おしてね のSEを流す
                WhoIsTopPlayer();               // 各プレイヤーのX軸位置を比較し、現在の首位と、自分との距離を算出する
                CheckCanUseTaihou();            // 人間大砲が撃てるか確認します
            }
            Countdown_timer_PanelOpen++;
            Countdown_Until_Push_OpenMyJankenPanel_Button();
        }
        else
        {
            //Debug.Log("右上「開始ボタン」のカウントダウン、ここで中断です・・");
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
            //Debug.Log("右上「開始ボタン」を自動で押す: ボタンを おしてね SE を流します");
            BGM_SE_MSC.bottonwo_oshitene_SE();         // ボタンを おしてね のSEを流す
        }
        else
        {
            //Debug.Log("右上「開始ボタン」を自動で押す → ボタンを おしてね SE を流す条件を満たしていません・・・");
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
            //Debug.Log("右上の「開始ボタン」を自動で押す:ジャンケンパネルが開かれていないので、開始ボタンを おします Auto");
            PushOpenMyJankenPanel_Button();            // 右上の開始ボタンを自動で押す
        }
        else
        {
            //Debug.Log("右上の「開始ボタン」を自動で押す → 開始ボタンを押す 条件を満たしていません・・・");
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
        //Debug.Log("Countdown_timer_Kettei : " + Countdown_timer_Kettei);

        if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)
        {
            Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
            Erase_Text_Announcement();
        }
        else
        {
            Countdown_Push_JankenTe_KetteiButton_Flg = false;
        }
        //Debug.Log("ジャンケンパネルが開かれていて、決定ボタンか押されていないならば、カウントダウン開始します");
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg)
        {
            //Debug.Log("ジャンケンパネルが開かれていて、決定ボタンか押されていないので、カウントダウン開始しました");
            //Debug.Log("カードを選んでね");

            if (Life_MyPlayer <= 0)        // 体力がゼロになっているなら（気絶していたら）
            {
                var sequence = DOTween.Sequence();
                sequence.InsertCallback(2f, () => PushBtn_Omakase());

                var sequence2 = DOTween.Sequence();
                sequence2.InsertCallback(5f, () => Auto_Push_JankenTe_KetteiButton());
            }
            else                           // 体力が1以上あれば（平常時）
            {
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
                    //Debug.Log("「決定ボタン」の Countdown_timer_Kettei が 1～6 以外です");
                    ResetCountdown_timer_Kettei_1();
                }
            }
        }
        else
        {
            //Debug.Log("「決定ボタン」を自動で押す:ジャンケンパネル閉じているようです・・・");
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
        //Debug.Log("決定ボタンのカウントダウン：" + timeHyoji);
        if (Countdown_Push_OpenMyJankenPanel_Button_Flg == false && GameSet_Flg == false && Countdown_Push_JankenTe_KetteiButton_Flg)
        {
            Countdown_timer_Kettei++;
            Countdown_Until_Push_JankenTe_KetteiButton();
        }
        else
        {
            //Debug.Log("「決定ボタン」のカウントダウン、ここで中断です・・");
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
            //Debug.Log("決定ボタンか押されていないので、おまかせボタンを自動で押す Auto");
            PushBtn_Omakase();                      // おまかせボタンを自動で押す

            var sequenc3 = DOTween.Sequence();
            sequenc3.InsertCallback(5f, () => korede_iikana_SE());
        }
        else
        {
            //Debug.Log("「決定ボタン」を自動で押す → おまかせボタンを自動で押す 条件を満たしていません・・・");
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
            //Debug.Log("決定ボタンか押されていないので、これでいいかな？ SEを流す");
            BGM_SE_MSC.korede_iikana_SE();         // これでいいかな？ SEを流す

            var sequence2 = DOTween.Sequence();
            sequence2.InsertCallback(10f, () => Auto_Push_JankenTe_KetteiButton());
        }
        else
        {
            //Debug.Log("「決定ボタン」を自動で押す → これでいいかな？ SEを流す 条件を満たしていません・・・");
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
            //Debug.Log("決定ボタンか押されていないので、決定ボタンを自動で押す Auto");
            JankenTe_Kettei();                     // 決定ボタンを自動で押す
        }
        else
        {
            //Debug.Log("「決定ボタン」を自動で押す → 押す条件を満たしていません・・・");
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
        Pos_Hasshin_Text.text = "";

        //Debug.Log("【JK-01】******************************************************************");
        //Debug.Log("【JK-01】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        //Debug.Log("【JK-01】OpenMyJankenPanel_Button（右上のセット開始ボタン） が押されました。セットを開始します。カードを配ります");
        //Debug.Log("【JK-01】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        //Debug.Log("【JK-01】******************************************************************");
        //Debug.Log("！！【JK-01】各プレイヤーの生存フラグは そのままです（リセットしません）！！");
        //Debug.Log("！！【JK-01】各プレイヤーの待機中フラグは そのままです（リセットしません）！！");

        //Debug.Log("【JK-01】共通ジャンケン パネル（ベース）を表示します");
        ShuffleCardsMSC.AppearJankenCards_Panel();
        //Debug.Log("【JK-01】自分のジャンケン パネル（カード選択画面）を表示します");
        ShuffleCardsMSC.AppearMyJankenPanel();

        //Debug.Log("【JK-01】セット開始したばかりなので、ジャンケン手「決定ボタン」を非表示にします");
        Check_CanAppear_KetteiBtn();     // 【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら決定ボタンを表示する
        Countdown_Until_Push_JankenTe_KetteiButton();     // ジャンケンパネルが開かれていて、決定ボタンか押されていならば、カウントダウン開始
        ShuffleCardsMSC.Distribute_JankenCards();         // じゃんけんカードの配布（一旦非表示にしてから、順番に表示していく）
    }

    [PunRPC]
    public void Janken_ExtraInning()              //【JK-37】ジャンケンカードを配る前の処理（延長戦突入時）
    {
        //Debug.Log("【JK-38】■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■");
        //Debug.Log("【JK-38】（延長戦）決着がつかなかったので延長戦に突入します！ジャンケンカードを配る準備をします。");
        //Debug.Log("【JK-38】■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■**■■");
        //Debug.Log("【JK-38】（延長戦）各プレイヤーの生存フラグは そのままです（リセットしません）");
        //Debug.Log("【JK-39】（延長戦）待機中フラグを全員一律 初期化 0：待機前（初期値） にします");
        //Debug.Log("【JK-40】（延長戦）共通ジャンケン パネル（ベース）を表示します");
        ShuffleCardsMSC.AppearJankenCards_Panel();
        //Debug.Log("【JK-41】（延長戦）自分のジャンケン パネル（カード選択画面）を表示します");
        ShuffleCardsMSC.AppearMyJankenPanel();
        //Debug.Log("【JK-42】（延長戦）フラグに関わらず、全員一律「待機中」画面 を表示します");
        ShuffleCardsMSC.AppearWait_JankenPanel();
        //Debug.Log("【JK-43】（延長戦）延長戦 開始したばかりなので、ジャンケン手「決定ボタン」を非表示にします");
        Check_CanAppear_KetteiBtn();          //【JK-43】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら決定ボタンを表示する
        //Debug.Log("【JK-44】（延長戦）ジャンケン生存者は待機フラグを0（待機まえ）に、敗北者は待機フラグを1（待機中）にし、黒カバーします");
        Check_WaitingFlg_DependOn_alive();    //【JK-44】ジャンケン生存者は待機フラグを0（待機まえ）に、敗北者は待機フラグを1（待機中）にする&& 黒カバー表示【JK-45】
        //Debug.Log("【JK-45】（延長戦）待機中フラグのプレイヤー間での共有が終わりました。");

        ToCheck_Iam_alive();            // ジャンケンで自分が生き残っているかどうかの確認をする
        if (Iam_alive == 1)    // 自分がまだジャンケン生存者であるならば
        {
            //Debug.Log("【JK-46】（延長戦）自分はまだジャンケン生存者です。私の待機フラグは0（待機まえ）です。黒カバーしません。");
            //Debug.Log("【JK-46】（延長戦）カードを選んで、延長戦を闘います！");
            //Debug.Log("【JK-47】（延長戦）「待機中」画面 を非表示にします （→ カード選べるようになる）");
            ShuffleCardsMSC.CloseWait_JankenPanel();        //【JK-47】「待機中」画面 を非表示にする
            Countdown_Until_Push_JankenTe_KetteiButton();     // ジャンケンパネルが開かれていて、決定ボタンか押されていならば、カウントダウン開始
        }
        else                   // 自分がジャンケン敗北者であるならば
        {
            //Debug.Log("【JK-46】（延長戦）自分はジャンケン敗北者...。私の待機フラグは1（待機中）です。黒カバーします。");
            //Debug.Log("【JK-46】（延長戦）もうカードを選ぶこともできません。見守るだけです。");
            //Debug.Log("【JK-47】（延長戦）私は待機中。「待機中」画面 は表示しておきます。（カードは選べない）");
        }
    }

    public void Check_CanAppear_KetteiBtn()       // 【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたらボタンを表示する
    {
        //Debug.Log("【JK-01】まずジャンケン手「決定ボタン」を非表示 → 表示できるか確認し、条件に合っていたら「決定ボタン」を表示する");
        ShuffleCardsMSC.CloseKetteiBtn();         // ボタンを閉じる（消す）
        if ((CanPushBtn_A + CanPushBtn_B + CanPushBtn_C + CanPushBtn_D + CanPushBtn_E + CanPushBtn_StockCard_Up + CanPushBtn_StockCard_Down) >= 5)  // 合計5枚分のじゃんけんカードを押した後ならば
        {
            ShuffleCardsMSC.AppearKetteiBtn();    // 決定ボタンを表示する
            //Debug.Log("【JK-01】条件に合っていたため「決定ボタン」を表示しました");
        }
    }
    #endregion

    #region // マッチング（ルーム入室）後、4人そろってから（あるいは一定数以上が「はじめる」を押したら）試合開始する処理
    public void Push_Ikemasu_Button()  // Ikemasu ボタンを押した時
    {
        if (Flg_IkemasuDone == 0)
        {
            Flg_IkemasuDone = 1;
            Share_Iam_Ikemasu();           // 私「試合開始、いけます！」を全員に向け共有する
            //Debug.Log("全員の Ikemasu を合計します");
            photonView.RPC("Gokei_Ikemasu_PlayersAll", RpcTarget.All);
            CheckStart_GameMatch();        // 試合開始できるか確認する処理
            ClosePanel_Ikemasu();
        }
        else
        {
            Debug.LogError("Push_Ikemasu 処理 ダブってます！！！");
            Error03_Text.text = "Push_Ikemasu処理ダブり";
        }
    }

    public void CheckStart_GameMatch()  // 試合開始できるか確認する処理
    {
        if (Shiai_Kaishi == false)  // 試合開始まえであれば、判定処理を実施する（試合中は判定する必要なし）
        {
            //Debug.Log("現在の参加人数をチェック NinzuCheck");
            NinzuCheck();
            //Debug.Log("Ikemasu を合計します");
            Gokei_Ikemasu_PlayersAll();  // Ikemasu を合計する
            //Debug.Log("試合開始まえなので、試合開始できるか 判定処理を実施します");
            if (SankaNinzu == 4)
            {
                //Debug.Log("4人そろったので、試合を開始します");
                photonView.RPC("Start_GameMatch", RpcTarget.All);
            }
            else if (SankaNinzu == 3)
            {
                if (int_Ikemasu_All >= 3)
                {
                    //Debug.Log("3人いけます！ので、試合を開始します");
                    photonView.RPC("Start_GameMatch", RpcTarget.All);
                }
            }
            else if (SankaNinzu == 2)
            {
                if (int_Ikemasu_All >= 2)
                {
                    //Debug.Log("2人いけます！ので、試合を開始します");
                    photonView.RPC("Start_GameMatch", RpcTarget.All);
                }
            }
            else
            {
                if (int_Ikemasu_All >= 1)
                {
                    ////Debug.Log("まだ一人なので、他の参加者を待ちます");
                    photonView.RPC("Start_GameMatch", RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    public void Start_GameMatch()  // 試合開始する処理
    {
        if (Flg_StartGameMatchDone == 0)
        {
            Flg_StartGameMatchDone = 1;
            Shiai_Kaishi = true;
            text_Game_kaishi_MAE.text = "";
            text_Game_kaishi_CHU.text = "しあい中";
            if (PhotonNetwork.CurrentRoom.IsOpen)          // まだ入室許可が出ていたら
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;  // これ以上、入室できないようにする
            }
            ToShare_InitialSetting();         // スタート位置まで戻ってくださーい
            CloseOpenMyJankenPanel_Button();
            CloseDebug_Buttons();
            ClosePanel_Ikemasu();
            ClosePanel_Intro();
            CloseAisatsu_Panel();
            CloseWinPanel();
            Reset_AllAisatsu();
            ShuffleCardsMSC.Reset_All();
            ShuffleCardsMSC.ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
            AppearStartLogo();
            BGM_SE_MSC.StartRappa_SE();  // ★ 開始のラッパを鳴らす！
            Countdown_Push_OpenMyJankenPanel_Button_Flg = true;

            StartSet_Life_Players();   // 体力をセットします[初期値のセット]

            var sequence = DOTween.Sequence();
            sequence.InsertCallback(3f, () => Start_GameMatch_After3());
        }
        else
        {
            Debug.LogError("Start_GameMatch 処理 ダブってます！！！");
            Error04_Text.text = "Start_GameMatch処理ダブり";
            ClosePanel_Ikemasu();
        }
    }

    public void Start_GameMatch_After3()  // 試合開始してから 3秒後 にする処理
    {
        //Debug.Log("試合開始してから 3秒後 にする処理をします");
        ShuffleCardsMSC.Set_All();
        CloseTaiki_OK_All();
        CloseStartLogo();
        ClosePanel_Ikemasu();
        AppearOpenMyJankenPanel_Button();
        Countdown_Until_Push_OpenMyJankenPanel_Button();
        Ctrl_Check_NowLoginMember();       // 一旦全員のフラグをログアウトにして、ログインしている人から返答をもらう    
    }


    public void Share_Iam_Ikemasu()    // 私「試合開始、いけます！」を全員に向け共有する
    {
        int_Iam_Ikemasu = 1;  // 私は いけます！
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            //Debug.Log("プレイヤー1はいけます！");
            photonView.RPC("Player1_Ikemasu", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            //Debug.Log("プレイヤー2はいけます！");
            photonView.RPC("Player2_Ikemasu", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            //Debug.Log("プレイヤー3はいけます！");
            photonView.RPC("Player3_Ikemasu", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            //Debug.Log("プレイヤー4はいけます！");
            photonView.RPC("Player4_Ikemasu", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Player1_Ikemasu()  // Player1 が いけます！⇒ 全員に情報提供（共有）する
    {
        //Debug.Log("[PunRPC] Player1（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player1 = 1;
    }
    [PunRPC]
    public void Player2_Ikemasu()  // Player2 が いけます！⇒ 全員に情報提供（共有）する
    {
        //Debug.Log("[PunRPC] Player2（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player2 = 1;
    }
    [PunRPC]
    public void Player3_Ikemasu()  // Player3 が いけます！⇒ 全員に情報提供（共有）する
    {
        //Debug.Log("[PunRPC] Player3（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player3 = 1;
    }
    [PunRPC]
    public void Player4_Ikemasu()  // Player4 が いけます！⇒ 全員に情報提供（共有）する
    {
        //Debug.Log("[PunRPC] Player4（" + AcutivePlayerName + "） が いけます！⇒ 全員に情報提供（共有）する");
        int_Ikemasu_Player4 = 1;
    }

    [PunRPC]
    public void Gokei_Ikemasu_PlayersAll()  // Ikemasu を合計する
    {
        int_Ikemasu_All = int_Ikemasu_Player1 + int_Ikemasu_Player2 + int_Ikemasu_Player3 + int_Ikemasu_Player4;    // 現在「試合開始、いけます！」な人の総人数 を更新
        //Debug.Log("「試合開始、いけます！」の総人数（int_Ikemasu_All） ： " + int_Ikemasu_All);
    }

    [PunRPC]
    public void AppearPanel_Ikemasu()
    {
        if (int_Iam_Ikemasu == 1)  // 既に「いけますボタン」を自分が押しているならば
        {
            Share_Iam_Ikemasu();   // （パネルは開かず）私「試合開始、いけます！」を全員に向け共有する（前回居なかった人もいるかも知れないので、改めて通知する）
        }
        else                       // 「いけますボタン」を自分がまだ押していなければ
        {
            Panel_Ikemasu.SetActive(true);
        }
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

    public void On_Off_Aisatsu_Panel()
    {
        if (Aisatsu_Panel.activeSelf) // あいさつパネル ON だったら
        {
            CloseAisatsu_Panel();

        }
        else                         // あいさつパネル OFF だったら
        {
            AppearAisatsu_Panel();
        }
    }
    #endregion


    #region //【JK-05】ジャンケン手 決定ボタン（「これでOK!」）を押した時の処理以降
    public void JankenTe_Kettei()               // 【JK-05】からの処理 ： ジャンケン手 決定ボタン（「これでOK!」）を押した時の処理
    {
        Countdown_Push_JankenTe_KetteiButton_Flg = false;
        CountLivePlayer();       //【JK-26】残留しているプレイヤー人数をカウントする★

        Update_Life_Players();                    // 各プレイヤーのLifeを同期します

        photonView.RPC("Share_Push_KetteiBtn", RpcTarget.All);
        //Debug.Log("【JK-05】ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        ShuffleCardsMSC.CloseMyJankenPanel();   // 不要なパネルを閉じる

        //Debug.Log("【JK-06】私のジャンケン手をみんなに提供（共有）します");
        ToSharePlayerTeNum();                   // 【JK-06】私のジャンケン手をみんなに提供（共有）します

        //Debug.Log("【JK-08】決定ボタンを押したので、他のプレイヤーを待っています");
        ShuffleCardsMSC.AppearWait_JankenPanel();   // 待機中パネルを表示

        //Debug.Log("【JK-10】私は待機中です");
        int_IamNowWaiting = 1;                  // 自分のジャンケン手 決定して待機中 （0：まだ決定してない、1：決定して待機中）

        //Debug.Log("【JK-11】私が待機中ということを、全員（他のプレイヤー）に情報提供（共有）します");
        ToCheck_NowWaiting();                   // ジャンケンで自分が待機中の旨を 情報提供（共有）する

        //Debug.Log("【JK-12】全員の待機フラグを確認します。その上で 勝敗判定（Hantei_Stream）フェーズへ進めるか確認します");
        //Debug.Log("【JK-12】CanDoフラグ に関わらず、2 秒待機後、Check_Can_Hantei_Stream を実行します");
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2f, () => Check_Can_Hantei_Stream());

        PrecheckTaiho_PosX();  // PosX の値を共有し、大砲が撃てるか確認する

        //CloseMyKakejiku();    // 自分はログインしているので、掛け軸外しますよ
        photonView.RPC("CloseMyKakejiku", RpcTarget.All);

        if (Btn_StockCard_Up.interactable == false)  // ストックカードを押していたら、→ ストック非表示
        {
            ShuffleCardsMSC.Stock_Button_Up.SetActive(false);
        }

        if (Btn_StockCard_Down.interactable == false)  // ストックカードを押していたら、→ ストック非表示
        {
            ShuffleCardsMSC.Stock_Button_Down.SetActive(false);
        }
    }

    [PunRPC]
    public void Share_Push_KetteiBtn()               // 【JK-05】ジャンケン手 決定ボタン（「これでOK!」）押下したことを伝える
    {
        //Debug.Log("【JK-05】******************************************************************");
        //Debug.Log("【JK-05】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        //Debug.Log("【JK-05】AcutivePlayerName" + AcutivePlayerName + "が ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        //Debug.Log("【JK-05】PhotonNetwork.NickName" + PhotonNetwork.NickName + "が ジャンケン手 決定ボタン（「これでOK!」）を押しました。ジャンケン手 これで決定します");
        //Debug.Log("【JK-05】■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        //Debug.Log("【JK-05】******************************************************************");
    }

    #region // 【JK-11】待機中フラグ関連の処理
    public void ToCheck_NowWaiting()        // 【JK-11_1】ジャンケンで自分が待機中の旨を 情報提供（共有）する
    {
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Check_NowWaiting();
    }

    public void Check_NowWaiting()          // 【JK-11_2】ジャンケンで自分が待機中の旨を 情報提供（共有）する
    {
        //Debug.Log("【JK-11_2】* ジャンケンで自分が待機中かどうかの確認をします *");
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("AcutivePlayerName  " + AcutivePlayerName);
        //Debug.Log("AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            //Debug.Log("プレイヤー1は待機中です");
            photonView.RPC("Player1_NowWaiting", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            //Debug.Log("プレイヤー2は待機中です");
            photonView.RPC("Player2_NowWaiting", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            //Debug.Log("プレイヤー3は待機中です");
            photonView.RPC("Player3_NowWaiting", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            //Debug.Log("プレイヤー4は待機中です");
            photonView.RPC("Player4_NowWaiting", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Player1_NowWaiting()  // 【JK-11_3】Player1 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("[PunRPC] 【JK-11_3】Player1（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player1 = 1;   // 0：待機前（初期値）、 1：待機中（決定ボタン押下後）
    }
    [PunRPC]
    public void Player2_NowWaiting()  // 【JK-11_3】Player2 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("[PunRPC] 【JK-11_3】Player2（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player2 = 1;
    }
    [PunRPC]
    public void Player3_NowWaiting()  // 【JK-11_3】Player3 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("[PunRPC] 【JK-11_3】Player3（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player3 = 1;
    }
    [PunRPC]
    public void Player4_NowWaiting()  // 【JK-11_3】Player4 が 待機中 ⇒ 全員に情報提供（共有）する
    {
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("[PunRPC] 【JK-11_3】Player4（" + AcutivePlayerName + "） が 待機中 ⇒ 全員に情報提供（共有）する");
        int_NowWaiting_Player4 = 1;
    }

    public void Reset_NowWaiting()    //【JK-38】【JK-204】待機中確認のパラメータ 初期化
    {
        //Debug.Log("【JK-38】【JK-204】全員の待機中フラグを 0（待機まえ）にリセットします");
        int_NowWaiting_Player1 = 0;   // 0：待機前（初期値:決定ボタン押下前）、 1：待機中（決定ボタン押下後）
        int_NowWaiting_Player2 = 0;
        int_NowWaiting_Player3 = 0;
        int_NowWaiting_Player4 = 0;
        int_WaitingPlayers_All = 0;
        int_IamNowWaiting = 0;
    }
    #endregion



    public void Share_TochuTaiseki_ninzu()  // ログアウトした人の人数を 全員に共有する
    {
        //Debug.Log("int_TochuTaiseki ログアウトした人の人数を 全員に共有する");
        if (int_TochuTaiseki == 1)
        {
            photonView.RPC("Reload_TochuTaiseki_ninzu_1", RpcTarget.All);
        }
        else if (int_TochuTaiseki == 2)
        {
            photonView.RPC("Reload_TochuTaiseki_ninzu_2", RpcTarget.All);
        }
        else if (int_TochuTaiseki == 3)
        {
            photonView.RPC("Reload_TochuTaiseki_ninzu_3", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Plus_TochuTaiseki_ninzu()      // ログアウトした人の人数を +1 する
    {
        int_TochuTaiseki++;
        //Debug.Log("int_TochuTaiseki" + int_TochuTaiseki);
    }

    [PunRPC]
    public void Reload_TochuTaiseki_ninzu_1()      // ログアウトした人の人数を更新する
    {
        int_TochuTaiseki = 1;
        //Debug.Log("int_TochuTaiseki を 1 にセットしました");
    }

    [PunRPC]
    public void Reload_TochuTaiseki_ninzu_2()      // ログアウトした人の人数を更新する
    {
        int_TochuTaiseki = 2;
        //Debug.Log("int_TochuTaiseki を 2 にセットしました");
    }

    [PunRPC]
    public void Reload_TochuTaiseki_ninzu_3()      // ログアウトした人の人数を更新する
    {
        int_TochuTaiseki = 3;
        //Debug.Log("int_TochuTaiseki を 3 にセットしました");
    }


    public void PreCheck_Can_Hantei_Stream()   // Check_Can_Hantei_Stream に進む前の前段階のチェック
    {
        //Debug.Log("ログアウトした人が居るので、後処理のため、残った私が判定処理を行います");
        CloseMyKakejiku();    // 自分はログインしているので、掛け軸外しますよ
        //Debug.Log("PreCheck_Can_Hantei_Stream 開始_01");
        if (Shiai_Kaishi)        // 試合中であれば
        {
            //Debug.Log("PreCheck_Can_Hantei_Stream 開始_02");

            if (Flg_PreCheck_Can_Hantei_Stream)
            {
                //Debug.Log("PreCheck_Can_Hantei_Stream 開始_03");
                photonView.RPC("ChangeFlg_PreCheck_Can_Hantei_Stream_False", RpcTarget.All);

                int_TochuTaiseki++;              // ログアウトした人の人数を +1 する
                Share_TochuTaiseki_ninzu();      // ログアウトした人の人数を 全員に共有する
                //Debug.Log("int_TochuTaiseki ： " + int_TochuTaiseki);

                var sequence2 = DOTween.Sequence();
                sequence2.InsertCallback(3.9f, () => shareChangeFlg_PreCheck_Can_Hantei_Stream_True());

                if (ShuffleCardsMSC.JankenCards_Panel.activeSelf)               // ジャンケンパネルが既に表示されていたら
                {
                    //Debug.Log("PreCheck_Can_Hantei_Stream 開始_04");

                    if ((int_IamNowWaiting == 1) && Flg_before_Hantei_Stream)   // 自分のジャンケン手 決定して待機中 （0：まだ決定してない、1：決定して待機中）
                    {                                                           // 勝敗判定（Hantei_Stream） 実行前なら               
                            //Debug.Log("PreCheck_Can_Hantei_Stream 開始_05");

                            Check_NowLoginMember();   // 一旦全員のフラグをログアウトにして、ログインしている人から返答をもらう

                            //Debug.Log("1.5秒後、 Check_Can_Hantei_Stream に移行します");
                            var sequence = DOTween.Sequence();
                            sequence.InsertCallback(1.5f, () => Check_Can_Hantei_Stream());
                    }
                    else
                    {
                        //Debug.Log("勝敗判定（Hantei_Stream） 実行中、もしくは実行後。・・Check_Can_Hantei_Stream には進みません");
                    }
                }
                else
                {
                    //Debug.Log("ジャンケンパネルが非表示です。・・Check_Can_Hantei_Stream には進みません");
                }
            }
            else
            {
                //Debug.Log("Flg_PreCheck_Can_Hantei_Stream が False です。・・Check_Can_Hantei_Stream には進みません");
            }
        }
        else
        {
            //Debug.Log("まだ試合開始前です。・・Check_Can_Hantei_Stream には進みません");
        }

    }

    public void Check_Can_Hantei_Stream()      // 【JK-12】勝敗判定（Hantei_Stream）フェーズへ進めるか確認する： 全員待機中であれば、勝敗判定（Hantei_Stream）に進む。
    {                                          // 一人でも待機まえであれば、何もしない（処理せず全員揃うまで待つ）
        //Debug.Log("ローカルの CanDoフラグ を OFF にします");
        //Debug.Log("ローカルの CanDoフラグ を OFF にしました");
        //Debug.Log("【JK-12】Check_Can_Hantei_Stream()スタート： 勝敗判定（Hantei_Stream）フェーズへ進めるか確認します");
        //Debug.Log("【JK-12】全員待機中であれば、勝敗判定（Hantei_Stream）に進む。/ 一人でも待機まえであれば、何もしない（処理せず全員揃うまで待つ）");
        //Debug.Log("【JK-12】■count_RoundRoop : " + count_RoundRoop + " ラウンド");    // N回目のジャンケンループ
        if (count_RoundRoop == 1)  // 1ラウンド目
        {
            //Debug.Log("ラウンドループ 1回目");
            //Debug.Log(TestRoomControllerSC.allPlayers.Length + ": allPlayers.Length");
            //Debug.Log("現在の参加人数は " + TestRoomControllerSC.int_JoinedPlayerAllNum);
            //Debug.Log("【JK-12】総参加人数 と 現在待機中の総人数 をチェックします");
            NinzuCheck();                          // 【JK-12】総参加人数 と 現在待機中の総人数 （ログアウトしている人の分は「常に待機中」にする）
                                                   //if (int_WaitingPlayers_All >= 4)  // 参加している全員が待機中になっていたら
            if (int_WaitingPlayers_All == SankaNinzu)  // 参加している全員が待機中になっていたら
            {
                //Debug.Log("■■ int_WaitingPlayers_All == 4 ■■");
                //Debug.Log("全員待機中です。最後にOKを出した人が、代表して勝敗判定（Hantei_Stream） を実行し、結果を都度、全員に共有します");
                Hantei_Stream();
            }
            else                                   // 一人でも待機まえである
            {
                //Debug.Log("■■ int_WaitingPlayers_All < SankaNinzu ■■");
                //Debug.Log("【JK-12_2】まだ決定ボタンを 押していない人がいます");
                //Debug.Log("【JK-12_2】全員揃うまで待ちます...");
                //Debug.Log("【JK-12_2】Now Waiting ...");
            }
            Te_RoundAll_ON();             // 全員のジャンケン手  全ラウンド分 表示
        }
        else                      // 2ラウンド目以降
        {
            //Debug.Log("ラウンドループ 2回目以降");
            //Debug.Log("自動的に 勝敗判定（Hantei_Stream）に進みます。ローカル実行してください。");
            Hantei_Stream();      //【JK-21】ジャンケン勝敗判定 実施 ⇒ ジャンケン勝者を1名に絞り込む（2名以上なら Check_Can_Hantei_Stream() に戻る）
        }
    }

    public void Hantei_Stream()  //【JK-21】ジャンケン勝敗判定 実施 ⇒ ジャンケン勝者を1名に絞り込む（2名以上なら Check_Can_Hantei_Stream() に戻る）
    {
        //Debug.Log("【JK-21】勝敗判定（Hantei_Stream） 開始");

        photonView.RPC("ShareStartHantei_Stream", RpcTarget.All);       // 勝敗判定（Hantei_Stream） 実行中の旨を全体共有する

        photonView.RPC("ShareCloseMyJankenPanel", RpcTarget.All);       // 延長戦の人がそのままになってしまうので、Myジャンケンパネルを閉じさせる
        photonView.RPC("Share_ResetFlg_AfterJumpDone", RpcTarget.All);  // AfterJumpDone フラグを 0 にリセットする

        ToCheck_Iam_alive();            // ジャンケンで自分が生き残っているかどうかの確認をする

        //Debug.Log("【JK-21】黒カバー表示確認");
        if (Iam_alive == 1) // 自分がジャンケン生存者であるなら
        {
            //Debug.Log("【JK-22】私は生きています！");
            ShuffleCardsMSC.CloseWait_JankenPanel();        // 「待機中」を非表示にする
        }
        else                // 自分がジャンケン敗北者なら
        {
            //Debug.Log("【JK-22】はぁ、はぁ、敗北者？");
            ShuffleCardsMSC.AppearWait_JankenPanel();       // 「待機中」を表示させる（画面を隠す）
        }

        //Debug.Log("【JK-23】ジャンケン ラウンドループ を 開始します");
        CountLivePlayer();            //【JK-26】残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
        //Debug.Log("NumLivePlayer（ジャンケン生存者）" + NumLivePlayer);

        if (NumLivePlayer >= 2)       // ジャンケン生存者が2人以上残っている場合
        {
            //Debug.Log("今、" + count_RoundRoop + "回目 のラウンドループです。まだ生存者2人以上です。");
            if (count_RoundRoop <= 5)     // 1～5回目 ラウンドループ
            {
                JankenBattle_OneRoop();   //【JK-23】じゃんけん手の勝ち負けを判定 → 生存人数（NumLivePlayer）が減る（⇒その後、Check_Can_Hantei_Stream() に戻る）
            }
            else                          // 6回目以降 ラウンドループ
            {
                //Debug.Log("ラウンドループを5回繰り返しましたが決着つきませんでした。残り1人になるまでやり直します");
                photonView.RPC("PrepareToNextSet", RpcTarget.All);
                //Debug.Log("【JK-37】（延長戦）延長戦に突入します");
                photonView.RPC("Janken_ExtraInning", RpcTarget.All);
            }
        }
        else                   //【JK-27_3】ジャンケン生存者が1人のみの場合
        {
            //Debug.Log("【JK-27_3】決着！ 生存者 1名になりました");    // ここでジャンケンの勝者が 1名 になった
            AfterWinnerDecision();    //【JK-100】この時点で ジャンケン生存者は 1名です。これから勝者の前進ジャンプ処理に移ります。
        }
    }


    [PunRPC]
    public void ShareStartHantei_Stream() // 勝敗判定（Hantei_Stream） 実行中の旨を全体共有する
    {
        Flg_before_Hantei_Stream = false;  // 勝敗判定（Hantei_Stream） 実行前ならtrue、実行始まったらfalse
    }

    [PunRPC]
    public void ShareEndHantei_Stream()  // 勝敗判定（Hantei_Stream） 実行終了の旨を全体共有する
    {
        Flg_before_Hantei_Stream = true;  // 勝敗判定（Hantei_Stream） 実行前ならtrue、実行始まったらfalse
    }

    [PunRPC]
    public void ShareCloseMyJankenPanel()
    {
        if (ShuffleCardsMSC.MyJankenPanel.activeSelf)
        {
            ShuffleCardsMSC.CloseMyJankenPanel();   // 不要なパネルを閉じる
        }
    }

    [PunRPC]
    public void Share_ResetFlg_AfterJumpDone()      // AfterJumpDone フラグを 0 にリセットする
    {
        Flg_AfterJumpDone = 0;
    }

    #region // 全員のジャンケン手  表示/非表示処理
    [PunRPC]
    public void Te_Round1_ON()     // 全員のジャンケン手  ラウンド1 表示
    {
        Img_Player1_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player2_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player3_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player4_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
    }

    [PunRPC]
    public void Te_Round2_ON()     // 全員のジャンケン手  ラウンド2 表示
    {
        Img_Player1_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player2_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player3_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player4_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
    }

    [PunRPC]
    public void Te_Round3_ON()     // 全員のジャンケン手  ラウンド3 表示
    {
        Img_Player1_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player2_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player3_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player4_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
    }

    [PunRPC]
    public void Te_Round4_ON()     // 全員のジャンケン手  ラウンド4 表示
    {
        Img_Player1_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player2_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player3_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player4_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
    }

    [PunRPC]
    public void Te_Round5_ON()     // 全員のジャンケン手  ラウンド5 表示
    {
        Img_Player1_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player2_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player3_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        Img_Player4_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
    }

    public void Te_RoundAll_ON()   // 全員のジャンケン手  全ラウンド分 表示
    {
        photonView.RPC("Te_Round1_ON", RpcTarget.All);
        photonView.RPC("Te_Round2_ON", RpcTarget.All);
        photonView.RPC("Te_Round3_ON", RpcTarget.All);
        photonView.RPC("Te_Round4_ON", RpcTarget.All);
        photonView.RPC("Te_Round5_ON", RpcTarget.All);
    }

    [PunRPC]
    public void Te_Round1_OFF()     // 全員のジャンケン手  ラウンド1 非表示
    {
        Img_Player1_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player2_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player3_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player4_Te1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
    }

    [PunRPC]
    public void Te_Round2_OFF()     // 全員のジャンケン手  ラウンド2 非表示
    {
        Img_Player1_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player2_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player3_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player4_Te2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
    }

    [PunRPC]
    public void Te_Round3_OFF()     // 全員のジャンケン手  ラウンド3 非表示
    {
        Img_Player1_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player2_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player3_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player4_Te3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
    }

    [PunRPC]
    public void Te_Round4_OFF()     // 全員のジャンケン手  ラウンド4 非表示
    {
        Img_Player1_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player2_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player3_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player4_Te4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
    }

    [PunRPC]
    public void Te_Round5_OFF()     // 全員のジャンケン手  ラウンド5 非表示
    {
        Img_Player1_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player2_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player3_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        Img_Player4_Te5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
    }

    public void Te_RoundAll_OFF()   // 全員のジャンケン手  全ラウンド分 非表示
    {
        photonView.RPC("Te_Round1_OFF", RpcTarget.All);
        photonView.RPC("Te_Round2_OFF", RpcTarget.All);
        photonView.RPC("Te_Round3_OFF", RpcTarget.All);
        photonView.RPC("Te_Round4_OFF", RpcTarget.All);
        photonView.RPC("Te_Round5_OFF", RpcTarget.All);
    }

    #endregion


    public void JankenBattle_OneRoop()          //【JK-23-】ジャンケンバトルの１ループ分処理（1ラウンド）
    {
        if (Flg_OneLoopDone == 0)
        {
            Flg_OneLoopDone = 1;
            //Debug.Log("【JK-23-】■count_RoundRoop : " + count_RoundRoop + " 回目（ラウンド）のジャンケンループ ");    // N回目のジャンケンループ
            StartCoroutine("Entrance_MainPart");    // メイン判定処理の前段階（2秒待機後に MainPart へ）
        }
        else
        {
            Debug.LogError("OneLoopDone 処理 ダブってます！！！");
            Error05_Text.text = "OneLoopDone処理ダブり";
        }
    }

    IEnumerator Entrance_MainPart()             // メイン判定処理の前段階（2秒待機後に MainPart へ）
    {
        //Debug.Log("【JK-23-】メイン判定処理の前段階（2秒待機後に MainPart へ）");
        yield return new WaitForSeconds(2.0f);
        //Debug.Log("【JK-23-】2秒 待機しました");
        JankenBattle_MainPart();
    }

    public void JankenBattle_MainPart()   // 【JK-23-】ジャンケンバトルのメイン判定処理
    {
        //Debug.Log("【JK-23-】ジャンケンバトルのメイン判定処理");
        SetKP_counter();               //【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する & 全員のジャンケン手  1ラウンド分ずつ 非表示

        var sequence2 = DOTween.Sequence();
        sequence2.InsertCallback(1.5f, () => JankenBattle_MainPart_02());
    }

    public void JankenBattle_MainPart_02()   // 【JK-23-】ジャンケンバトルのメイン判定処理 その2
    {
        Syohai_Hantei();               //【JK-25】生存者同士の 勝ち負けを判定（負けた人のaliveフラグ を 0 にする） → 人数が減る ＆＆ 移動ステップ数を 勝った手に応じて上書き ＆＆ 負けた人の黒カバー表示
        CountLivePlayer();             //【JK-26】残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
        photonView.RPC("Share_JKAvator_MakeSetting", RpcTarget.All);
        count_RoundRoop++;             // N回目 のループ を 1 進める
        //Debug.Log("ジャンケンバトルのメイン判定処理おわり！ 3秒待って、ラウンドループのはじめに戻ります。");
        var sequence2 = DOTween.Sequence();
        sequence2.InsertCallback(1.4f, () => Share_ResetFlg_OneLoopDone());
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(1.5f, () => Check_Can_Hantei_Stream());
    }

    public void Share_ResetFlg_OneLoopDone()
    {
        Flg_OneLoopDone = 0;
    }

    [PunRPC]
    public void PrepareToNextSet()     // 【JK-28】【JK-201】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット【JK-36】
    {
        //Debug.Log("【JK-28】【JK-201】PrepareToNextSet 次のセットへ移る準備をします");
        ResetMyNumTe_All();               // 【JK-29】MyNumTe 数値を -1 にリセット（int,text）
        Reset_MyRireki_All();             // 【JK-30】MyRireki イメージを null にリセット（Image）
        ToCanPush_All();                  // 【JK-31】じゃんけんカードボタン を押せるようにする(フラグのリセット）（bool）
        ResetPlayerTeNum();               // 【JK-32】Player1 ～ Player4 のじゃんけん手 数値を -1 にリセット（int,text）
        ResetImg_PlayerlayerRireki_All(); // 【JK-33】Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）
        //Debug.Log("【JK-34】じゃんけんカード 手のリセット");
        ShuffleCardsMSC.Reset_All();      // 【JK-34】じゃんけんカード 手のリセット
        //Debug.Log("【JK-35】じゃんけんカード 手のセット");
        ShuffleCardsMSC.Set_All();        // 【JK-35】じゃんけんカード 手のセット
                                          // ResetAlivePlayer();  // 各種カウンター リセット
        count_RoundRoop = 1;              // 【JK-36】ラウンドループカウンター 1に戻す
    }

    public void AfterWinnerDecision()     //【JK-100】この時点で ジャンケン生存者は 1名です。これから勝者の前進ジャンプ処理に移ります。
    {
        //Debug.Log("【JK-100】この時点で ジャンケン生存者は 1名です。これから勝者の前進ジャンプ処理に移ります。");

        if (SankaNinzu == 1)          //【JK-101】参加人数が1人の時（テストプレイ時）
        {
            //Debug.Log("【JK-101】参加人数が1人なので（テストプレイ時）、移動ステップ数を 4 に上書きします");
            original_StepNum = 4;     // 移動ステップ数を 4 に上書き
        }

        //Debug.Log("【JK-102】ジャンケン勝敗 判定おわり");    // 【JK-102】ここでジャンケンの勝者が 1名 決まっている

        //Debug.Log("【JK-103】ジャンケン勝敗の勝利者は？");
        photonView.RPC("WhoIsWinner", RpcTarget.All);

        photonView.RPC("CloseMyKakejiku", RpcTarget.All);

        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2f, () => ClosePanel_beforeJump());
    }

    public void ClosePanel_beforeJump()  // 3秒待ってからジャンケンパネルを閉じる
    {
        //Debug.Log("【JK-104】ジャンケンで自分が勝利者かどうかの確認をします");
        ToCheck_Iam_Winner();         //【JK-104】ジャンケンで自分が勝利者かどうかの確認をする → 勝ってたら右にジャンプ（ぴょーん！）！【JK-110】

        //Debug.Log("【JK-200】不要なパネルを閉じて、デフォルト状態にします");
        photonView.RPC("Close_AllPanel", RpcTarget.All);
    }

    [PunRPC]
    public void Close_AllPanel()
    {
        ShuffleCardsMSC.ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
    }

    public void Check_WaitingFlg_DependOn_alive()  //【JK-44】（延長戦）ジャンケン生存者（aliveフラグが 1 の人）は待機フラグを0に、敗北者は待機フラグを1のままにする && 黒カバー表示
    {
        CountLivePlayer();       //【JK-26】残留しているプレイヤー人数をカウントする★

        if (alivePlayer1 == 1)                     // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player1 = 0;            // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);            // 黒カバー表示
        }

        if (alivePlayer2 == 1)                    // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player2 = 0;           // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);           // 黒カバー表示
        }

        if (alivePlayer3 == 1)                   // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player3 = 0;          // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);          // 黒カバー表示
        }

        if (alivePlayer4 == 1)                  // プレイヤーが生存者であれば
        {
            int_NowWaiting_Player4 = 0;         // 待機フラグを 0 ：待機前（初期値:決定ボタン押下前）   にする
        }
        else
        {
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);          // 黒カバー表示
        }
        //Debug.Log("【JK-45】（延長戦）生き残っている者のみが「待機まえ」になりました。敗北者は待機中（見守り中）です。");
    }

    public void ShareAfterJump()   // 右にジャンプ（ぴょーん！）が完了してからの処理（右上の開始ボタンを押せるように各値をリセット） ⇒ 全員に共有する
    {
        photonView.RPC("AfterJump", RpcTarget.All);
    }

    [PunRPC]
    public void AfterJump()   // 右にジャンプ（ぴょーん！）が完了してからの処理（右上の開始ボタンを押せるように各値をリセット）
    {
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(1.5f, () => CloseSubCamera_Group());

        ShareEndHantei_Stream();  // 勝敗判定（Hantei_Stream） 実行終了の旨を全体共有する

        //Debug.Log("【JK-201】PrepareToNextSet 次のセットへ移る準備 をします");
        PrepareToNextSet();           //【JK-201】次のセットへ移る準備： プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット
        //Debug.Log("【JK-202】PrepareToNextSet 次のセットへ移る準備 終わりました");

        //Debug.Log("【JK-203】全員の aliveフラグ を 1 にします（全員生存）");
        ResetAlivePlayer();           //【JK-203】各種 生存者カウンター リセット
                                      //anzenPoint = 0;

        //Debug.Log("【JK-204】待機中フラグ（確認用パラメータ） を 初期化（0にする）");
        Reset_NowWaiting();          // 待機中フラグ（確認用パラメータ） を 初期化（0にする）

        //Debug.Log("【JK-205】Katakori_stream");
        Katakori_stream();           // 肩こりフラグがONの時のみ実行される（治癒されるまで）

        //Debug.Log("【JK-206】Check_IamHealed");
        Check_IamHealed();           // 完治して体力が全快したか確認（一定値で回復）

        //Debug.Log("【JK-207】Check_LifeZERO");
        Check_LifeZERO();            // 体力がゼロになっているか確認する → セロなら白旗ALL

        //Debug.Log("【JK-208】Update_Life_Players");
        Update_Life_Players();       // 各プレイヤーのLifeを同期します

        if (Life_MyPlayer > 0)        // 体力がゼロより上なら（通常時）
        {
            Redistribute_JankenCards();    // じゃんけんカードの再配布を実施します
        }

        ResetCountdown_timer_PanelOpen_1();
        if (Flg_AfterJumpDone == 0)
        {
            Flg_AfterJumpDone = 1;
            Countdown_Until_Push_OpenMyJankenPanel_Button();   // ジャンケンパネルが開かれていないならば、ボタンを押すようにアナウンスする
        }
        else
        {
            Debug.LogError("AfterJumpDone 処理 ダブってます！！！");
            Error06_Text.text = "AfterJumpDone処理ダブり";
        }
        Flg_FromWin_ToJumpDone = 0;

        PrecheckTaiho_PosX();  // PosX の値を共有し、大砲が撃てるか確認する
    }

    public void PrecheckTaiho_PosX()  // PosX の値を共有し、大砲が撃てるか確認する
    {
        Flg_Update_PosX = true;
        Update_PosX_Players();          // 各プレイヤーのX軸位置を同期します
        WhoIsTopPlayer();               // 各プレイヤーのX軸位置を比較し、現在の首位と、自分との距離を算出する
        CheckCanUseTaihou();            // 人間大砲が撃てるか確認します
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
        //Debug.Log("【START-03】【JK-203】全員の aliveフラグ を 1 にします（全員生存）");
        alivePlayer1 = 1;                    // ジャンケンで残留してれば 1 、負けたら 0
        alivePlayer2 = 1;
        alivePlayer3 = 1;
        alivePlayer4 = 1;
        photonView.RPC("Share_alivePlayer1_alive", RpcTarget.All);
        photonView.RPC("Share_alivePlayer2_alive", RpcTarget.All);
        photonView.RPC("Share_alivePlayer3_alive", RpcTarget.All);
        photonView.RPC("Share_alivePlayer4_alive", RpcTarget.All);
        Iam_alive = 1;
        CloseKachi_White_All();              // すべての白カバーを閉じる（消す）
        CloseImg_CoverBlack_All();           // ジャンケン手の黒カバーをリセット（非表示）
        photonView.RPC("Share_JKAvator_StandSetting", RpcTarget.All);

        CountLivePlayer();                   //【JK-26】残留しているプレイヤー人数をカウントする★
        count_RoundRoop = 1;                 // ラウンドループを1に戻す
    }

    public void CheckAlivePlayer_DependOn_Absent()         // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）
    {
        //Debug.Log("生存カウンターのチェック前");

        if (logon_player1 == false)   // player1 が退出しました
        {
            alivePlayer1 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }
        if (logon_player2 == false)
        {
            alivePlayer2 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }
        if (logon_player3 == false)
        {
            alivePlayer3 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }
        if (logon_player4 == false)
        {
            alivePlayer4 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }

        //Debug.Log("生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）");
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
        //Debug.Log("alivePlayer1 ： " + alivePlayer1);
        //Debug.Log("alivePlayer2 ： " + alivePlayer2);
        //Debug.Log("alivePlayer3 ： " + alivePlayer3);
        //Debug.Log("alivePlayer4 ： " + alivePlayer4);
    }

    public void ToCheck_Iam_alive()            // ジャンケンで自分が生き残っているかどうかの確認をする（Iam_alive の値を取得する）
    {
        NinzuCheck();                          // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        Check_Iam_alive();
    }

    public void Check_Iam_alive()              // ジャンケンで自分が生き残っているかどうかの確認をする（Iam_alive の値を取得する）
    {
        //Debug.Log("* ジャンケンで自分が生き残っているかどうかの確認をします *");
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("AcutivePlayerName  " + AcutivePlayerName);
        //Debug.Log("AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            if (alivePlayer1 == 1) // Player1 が生きている
            {
                //Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                //Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            if (alivePlayer2 == 1) // Player2 が生きている
            {
                //Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                //Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            if (alivePlayer3 == 1) // Player3 が生きている
            {
                //Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                //Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            if (alivePlayer4 == 1) // Player4 が生きている
            {
                //Debug.Log("私は生きています！");
                Iam_alive = 1;
            }
            else
            {
                //Debug.Log("はぁ、はぁ、敗北者？");
                Iam_alive = -1;
            }
        }
    }

    [PunRPC]
    public void WhoIsWinner()                //【JK-103】ジャンケン勝敗の勝利者は？（この時点で ジャンケン生存者は 1名です）
    {
        //Debug.Log("【JK-103】ジャンケン勝敗の勝利者は？（この時点で ジャンケン生存者は 1名です）");
        WinnerNum = -1;                      // 一旦リセット
        CountLivePlayer();       //【JK-26】残留しているプレイヤー人数をカウントする★
        if (alivePlayer1 == 1)
        {
            //Debug.Log("【JK-103】Player1 勝利");  // アバターはまだ確認していないけどね
            WinnerNum = 1;
            Text_WinnerName.text = TestRoomControllerSC.string_PName1;
            photonView.RPC("AppearKachi_White1", RpcTarget.All);
        }
        else if (alivePlayer2 == 1)
        {
            //Debug.Log("【JK-103】Player2 勝利");
            WinnerNum = 2;
            Text_WinnerName.text = TestRoomControllerSC.string_PName2;
            photonView.RPC("AppearKachi_White2", RpcTarget.All);
        }
        else if (alivePlayer3 == 1)
        {
            //Debug.Log("【JK-103】Player3 勝利");
            WinnerNum = 3;
            Text_WinnerName.text = TestRoomControllerSC.string_PName3;
            photonView.RPC("AppearKachi_White3", RpcTarget.All);
        }
        else if (alivePlayer4 == 1)
        {
            //Debug.Log("【JK-103】Player4 勝利");
            WinnerNum = 4;
            Text_WinnerName.text = TestRoomControllerSC.string_PName4;
            photonView.RPC("AppearKachi_White4", RpcTarget.All);
        }
        else
        {
            Debug.LogError("【JK-103】勝利いない？");
        }
        Share_JKAvator_KachiSetting();     // 自分が勝ちならジャンケン手、自分の下アバターを勝ち（Happyモーション）にセットし、それを全プレイヤーで共有する
    }

    public void ToCheck_Iam_Winner()           //【JK-104】ジャンケンで自分が勝利者かどうかの確認をするための準備【JK-106】
    {
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
                                            //Share_AcutivePlayerID(); //              // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
                                            //Check_Iam_Winner();
        photonView.RPC("Check_Iam_Winner", RpcTarget.All);
    }

    [PunRPC]
    public void Check_Iam_Winner()         //【JK-105】ジャンケンで自分が勝利者かどうかの確認をする
    {
        //Debug.Log("【JK-105】ジャンケンで自分が勝利者かどうかの確認をします。自分が勝ってたらジャンプします。");
        WhoAreYou();     // 私の名前（真名）を表示
        //Debug.Log("【JK-105】AcutivePlayerName  " + AcutivePlayerName);
        //Debug.Log("【JK-105】AcutivePlayerID  " + AcutivePlayerID);

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            if (WinnerNum == 1)          // プレイヤー1 が勝利者
            {
                //Debug.Log("【JK-106】P1 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            if (WinnerNum == 2)          // プレイヤー2 が勝利者
            {
                //Debug.Log("【JK-106】P2 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            if (WinnerNum == 3)          // プレイヤー3 が勝利者
            {
                //Debug.Log("【JK-106】P3 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            if (WinnerNum == 4)          // プレイヤー4 が勝利者
            {
                //Debug.Log("【JK-106】P4 自分の勝利！！ 前に進みます！");
                FromWin_ToJump();       //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
            }
        }
    }

    public void WaitTime_2nd()
    {
        //Debug.Log("2秒待ち");
    }

    [PunRPC]
    public void Share_JKAvator_StandSetting()     // ジャンケン手、下アバターをスタンド（デフォルト）にし、それを全プレイヤーで共有する
    {
        //Debug.Log("ジャンケン手、下アバターをスタンド（デフォルト）にし、それを全プレイヤーで共有する");

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            SharePlayerIcon_Player1();   // アバターをスタンドにセット
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            SharePlayerIcon_Player2();   // アバターをスタンドにセット
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            SharePlayerIcon_Player3();   // アバターをスタンドにセット
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            SharePlayerIcon_Player4();   // アバターをスタンドにセット
        }
    }

    #region // ジャンケン手、下アバターを負けにセット
    [PunRPC]
    public void Share_JKAvator_MakeSetting()     // 自分が負けていたらジャンケン手、自分の下アバターを負け（しゃがみモーション）にセットし、それを全プレイヤーで共有する
    {
        ToCheck_Iam_alive();            // ジャンケンで自分が生き残っているかどうかの確認をする
        if (Iam_alive == 1)    // 自分がまだジャンケン生存者であるならば
        {
            //Debug.Log("私はジャンケン生存者です");
        }
        else     // 自分がジャンケン敗北者ならば
        {
            //Debug.Log("ジャンケン手、下アバターを負け（しゃがみモーション）にセットし、それを全プレイヤーで共有する");
            if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
            {
                ShareJKAvator_Make_Player1();  // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
            }

            else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
            {
                ShareJKAvator_Make_Player2();  // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
            }

            else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
            {
                ShareJKAvator_Make_Player3();  // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
            }

            else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
            {
                ShareJKAvator_Make_Player4();  // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
            }
        }
    }

    public void ShareJKAvator_Make_Player1()          // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_MakeP1_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_MakeP1_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_MakeP1_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_MakeP1_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_MakeP1_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー1のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_MakeP1_utako()                     // アバターを 負け うたこ にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_utako;
    }
    [PunRPC]
    public void Set_MakeP1_Unitychan()                 // アバターを 負け Unityちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Unitychan;
    }
    [PunRPC]
    public void Set_MakeP1_Pchan()                     // アバターを 負け Pちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Pchan;
    }
    [PunRPC]
    public void Set_MakeP1_mobuchan()                  // アバターを 負け モブちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_mobuchan;
    }
    [PunRPC]
    public void Set_MakeP1_Zunko()                     // アバターを 負け ずん子ちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Zunko;
    }


    public void ShareJKAvator_Make_Player2()          // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_MakeP2_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_MakeP2_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_MakeP2_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_MakeP2_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_MakeP2_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー2のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_MakeP2_utako()                     // アバターを 負け うたこ にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_utako;
    }
    [PunRPC]
    public void Set_MakeP2_Unitychan()                 // アバターを 負け Unityちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Unitychan;
    }
    [PunRPC]
    public void Set_MakeP2_Pchan()                     // アバターを 負け Pちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Pchan;
    }
    [PunRPC]
    public void Set_MakeP2_mobuchan()                  // アバターを 負け モブちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_mobuchan;
    }
    [PunRPC]
    public void Set_MakeP2_Zunko()                     // アバターを 負け ずん子ちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Zunko;
    }


    public void ShareJKAvator_Make_Player3()          // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_MakeP3_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_MakeP3_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_MakeP3_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_MakeP3_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_MakeP3_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー3のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_MakeP3_utako()                     // アバターを 負け うたこ にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_utako;
    }
    [PunRPC]
    public void Set_MakeP3_Unitychan()                 // アバターを 負け Unityちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Unitychan;
    }
    [PunRPC]
    public void Set_MakeP3_Pchan()                     // アバターを 負け Pちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Pchan;
    }
    [PunRPC]
    public void Set_MakeP3_mobuchan()                  // アバターを 負け モブちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_mobuchan;
    }
    [PunRPC]
    public void Set_MakeP3_Zunko()                     // アバターを 負け ずん子ちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Zunko;
    }


    public void ShareJKAvator_Make_Player4()          // ジャンケン手、下アバターを負けにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_MakeP4_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_MakeP4_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_MakeP4_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_MakeP4_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_MakeP4_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー4のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_MakeP4_utako()                     // アバターを 負け うたこ にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_utako;
    }
    [PunRPC]
    public void Set_MakeP4_Unitychan()                 // アバターを 負け Unityちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Unitychan;
    }
    [PunRPC]
    public void Set_MakeP4_Pchan()                     // アバターを 負け Pちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Pchan;
    }
    [PunRPC]
    public void Set_MakeP4_mobuchan()                  // アバターを 負け モブちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_mobuchan;
    }
    [PunRPC]
    public void Set_MakeP4_Zunko()                     // アバターを 負け ずん子ちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Make_Zunko;
    }

    #endregion


    #region // ジャンケン手、下アバターを勝ちにセット
    [PunRPC]
    public void Share_JKAvator_KachiSetting()     // 自分が勝ちならジャンケン手、自分の下アバターを勝ち（しゃがみモーション）にセットし、それを全プレイヤーで共有する（この時点で ジャンケン生存者は 1名です）
    {
        ToCheck_Iam_alive();            // ジャンケンで自分が生き残っているかどうかの確認をする
        if (Iam_alive != 1)    // 自分がまだジャンケン敗北者であるならば
        {
            //Debug.Log("私はジャンケン敗北者です");
        }
        else     // 自分がジャンケン生存者ならば
        {
            //Debug.Log("ジャンケン手、下アバターを勝ち（Happyモーション）にセットし、それを全プレイヤーで共有する");
            if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
            {
                ShareJKAvator_Kachi_Player1();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
            }

            else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
            {
                ShareJKAvator_Kachi_Player2();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
            }

            else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
            {
                ShareJKAvator_Kachi_Player3();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
            }

            else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
            {
                ShareJKAvator_Kachi_Player4();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
            }
        }
    }

    public void ShareJKAvator_Kachi_Player1()          // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_KachiP1_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_KachiP1_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_KachiP1_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_KachiP1_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_KachiP1_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー1のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_KachiP1_utako()                     // アバターを 勝ち うたこ にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_utako;
    }
    [PunRPC]
    public void Set_KachiP1_Unitychan()                 // アバターを 勝ち Unityちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Unitychan;
    }
    [PunRPC]
    public void Set_KachiP1_Pchan()                     // アバターを 勝ち Pちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Pchan;
    }
    [PunRPC]
    public void Set_KachiP1_mobuchan()                  // アバターを 勝ち モブちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_mobuchan;
    }
    [PunRPC]
    public void Set_KachiP1_Zunko()                     // アバターを 勝ち ずん子ちゃん にセット
    {
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Zunko;
    }


    public void ShareJKAvator_Kachi_Player2()          // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_KachiP2_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_KachiP2_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_KachiP2_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_KachiP2_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_KachiP2_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー2のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_KachiP2_utako()                     // アバターを 勝ち うたこ にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_utako;
    }
    [PunRPC]
    public void Set_KachiP2_Unitychan()                 // アバターを 勝ち Unityちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Unitychan;
    }
    [PunRPC]
    public void Set_KachiP2_Pchan()                     // アバターを 勝ち Pちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Pchan;
    }
    [PunRPC]
    public void Set_KachiP2_mobuchan()                  // アバターを 勝ち モブちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_mobuchan;
    }
    [PunRPC]
    public void Set_KachiP2_Zunko()                     // アバターを 勝ち ずん子ちゃん にセット
    {
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Zunko;
    }


    public void ShareJKAvator_Kachi_Player3()          // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_KachiP3_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_KachiP3_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_KachiP3_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_KachiP3_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_KachiP3_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー3のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_KachiP3_utako()                     // アバターを 勝ち うたこ にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_utako;
    }
    [PunRPC]
    public void Set_KachiP3_Unitychan()                 // アバターを 勝ち Unityちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Unitychan;
    }
    [PunRPC]
    public void Set_KachiP3_Pchan()                     // アバターを 勝ち Pちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Pchan;
    }
    [PunRPC]
    public void Set_KachiP3_mobuchan()                  // アバターを 勝ち モブちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_mobuchan;
    }
    [PunRPC]
    public void Set_KachiP3_Zunko()                     // アバターを 勝ち ずん子ちゃん にセット
    {
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Zunko;
    }


    public void ShareJKAvator_Kachi_Player4()          // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
    {
        if (int_conMyCharaAvatar == 1)                // うたこ
        {
            photonView.RPC("Set_KachiP4_utako", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 2)           // Unityちゃん
        {
            photonView.RPC("Set_KachiP4_Unitychan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 3)           // Pちゃん
        {
            photonView.RPC("Set_KachiP4_Pchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 4)           // モブちゃん
        {
            photonView.RPC("Set_KachiP4_mobuchan", RpcTarget.All);
        }
        else if (int_conMyCharaAvatar == 5)           // ずん子ちゃん
        {
            photonView.RPC("Set_KachiP4_Zunko", RpcTarget.All);
        }
        //Debug.Log("【START-07】プレイヤー4のアイコンをセットしました");
    }
    [PunRPC]
    public void Set_KachiP4_utako()                     // アバターを 勝ち うたこ にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_utako;
    }
    [PunRPC]
    public void Set_KachiP4_Unitychan()                 // アバターを 勝ち Unityちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Unitychan;
    }
    [PunRPC]
    public void Set_KachiP4_Pchan()                     // アバターを 勝ち Pちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Pchan;
    }
    [PunRPC]
    public void Set_KachiP4_mobuchan()                  // アバターを 勝ち モブちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_mobuchan;
    }
    [PunRPC]
    public void Set_KachiP4_Zunko()                     // アバターを 勝ち ずん子ちゃん にセット
    {
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Avator_underJankenTe.gameObject.GetComponent<Image>().sprite = sprite_Avator_Kachi_Zunko;
    }

    #endregion


    public void CountLivePlayer()       //【JK-26】残留しているプレイヤー人数をカウントする
    {
        CheckAlivePlayer_DependOn_Absent();  // 生存カウンターのチェック（欠席している所の aliveフラグ を 0 にする）

        if (logon_player1 == false)   // player1 が退出しました
        {
            alivePlayer1 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }
        if (logon_player2 == false)
        {
            alivePlayer2 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }
        if (logon_player3 == false)
        {
            alivePlayer3 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }
        if (logon_player4 == false)
        {
            alivePlayer4 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        }

        NumLivePlayer = alivePlayer1 + alivePlayer2 + alivePlayer3 + alivePlayer4;
        //Debug.Log("【JK-26】NumLivePlayer 残留プレイヤー数 ： " + NumLivePlayer);
    }

    public void SetKP_counter()         //【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
    {
        //Debug.Log("【JK-24】ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する SetKP_counter()");    // N回目のラウンドループ
        //Debug.Log("【JK-24】count_RoundRoop" + count_RoundRoop + " 回目（ラウンド）のジャンケンループ ");    // N回目のラウンドループ

        if (count_RoundRoop == 1)
        {
            KP1 = int_Player1_Te1;
            KP2 = int_Player2_Te1;
            KP3 = int_Player3_Te1;
            KP4 = int_Player4_Te1;
            Te_RoundAll_ON();             // 全員のジャンケン手  全ラウンド分 表示
        }
        else if (count_RoundRoop == 2)
        {
            KP1 = int_Player1_Te2;
            KP2 = int_Player2_Te2;
            KP3 = int_Player3_Te2;
            KP4 = int_Player4_Te2;
            photonView.RPC("Te_Round1_OFF", RpcTarget.All);
        }
        else if (count_RoundRoop == 3)
        {
            KP1 = int_Player1_Te3;
            KP2 = int_Player2_Te3;
            KP3 = int_Player3_Te3;
            KP4 = int_Player4_Te3;
            photonView.RPC("Te_Round2_OFF", RpcTarget.All);
        }
        else if (count_RoundRoop == 4)
        {
            KP1 = int_Player1_Te4;
            KP2 = int_Player2_Te4;
            KP3 = int_Player3_Te4;
            KP4 = int_Player4_Te4;
            photonView.RPC("Te_Round3_OFF", RpcTarget.All);
        }
        else if (count_RoundRoop == 5)
        {
            KP1 = int_Player1_Te5;
            KP2 = int_Player2_Te5;
            KP3 = int_Player3_Te5;
            KP4 = int_Player4_Te5;
            photonView.RPC("Te_Round4_OFF", RpcTarget.All);
        }
        else
        {
            //Debug.Log("count_RoundRoop ６回 超えました");
            // 再度、ジャンケン手カードの選択をする
        }
        //Debug.Log("KP1 ： " + KP1);
        //Debug.Log("KP2 ： " + KP2);
        //Debug.Log("KP3 ： " + KP3);
        //Debug.Log("KP4 ： " + KP4);
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
        //Debug.Log("NoneGuP1 ： " + NoneGuP1);
        //Debug.Log("NoneGuP2 ： " + NoneGuP2);
        //Debug.Log("NoneGuP3 ： " + NoneGuP3);
        //Debug.Log("NoneGuP4 ： " + NoneGuP4);
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
        //Debug.Log("NoneChokiP1 ： " + NoneChokiP1);
        //Debug.Log("NoneChokiP2 ： " + NoneChokiP2);
        //Debug.Log("NoneChokiP3 ： " + NoneChokiP3);
        //Debug.Log("NoneChokiP4 ： " + NoneChokiP4);
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
        //Debug.Log("NonePaP1 ： " + NonePaP1);
        //Debug.Log("NonePaP2 ： " + NonePaP2);
        //Debug.Log("NonePaP3 ： " + NonePaP3);
        //Debug.Log("NonePaP4 ： " + NonePaP4);
    }


    public void Check_King_Existence()    //【JK-25】NoneKing の判定を返す
    {
        bool NoneKingP1 = true;
        bool NoneKingP2 = true;
        bool NoneKingP3 = true;
        bool NoneKingP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 13) // 王さま ではない
            {
                NoneKingP1 = true; // 王さま 無し
            }
            else
            {
                NoneKingP1 = false; // 王さま 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneKingP1 = true; // 王さま無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 13) // 王さま ではない
            {
                NoneKingP2 = true; // 王さま 無し
            }
            else
            {
                NoneKingP2 = false; // 王さま 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneKingP2 = true; // 王さま無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 13) // 王さま ではない
            {
                NoneKingP3 = true; // 王さま 無し
            }
            else
            {
                NoneKingP3 = false; // 王さま 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneKingP3 = true; // 王さま無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 13) // 王さま ではない
            {
                NoneKingP4 = true; // 王さま 無し
            }
            else
            {
                NoneKingP4 = false; // 王さま 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneKingP4 = true; // 王さま無しON
        }

        if (NoneKingP1 && NoneKingP2 && NoneKingP3 && NoneKingP4)
        {
            NoneKing = true; // 王さま無しON
        }
        else
        {
            NoneKing = false; // 王さま無しOFF
        }
        //Debug.Log("NoneKingP1 ： " + NoneKingP1);
        //Debug.Log("NoneKingP2 ： " + NoneKingP2);
        //Debug.Log("NoneKingP3 ： " + NoneKingP3);
        //Debug.Log("NoneKingP4 ： " + NoneKingP4);
    }

    public void Check_Dorei_Existence()    //【JK-25】NoneDorei の判定を返す
    {
        bool NoneDoreiP1 = true;
        bool NoneDoreiP2 = true;
        bool NoneDoreiP3 = true;
        bool NoneDoreiP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 23) // どれい ではない
            {
                NoneDoreiP1 = true; // どれい 無し
            }
            else
            {
                NoneDoreiP1 = false; // どれい 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneDoreiP1 = true; // どれい無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 23) // どれい ではない
            {
                NoneDoreiP2 = true; // どれい 無し
            }
            else
            {
                NoneDoreiP2 = false; // どれい 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneDoreiP2 = true; // どれい無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 23) // どれい ではない
            {
                NoneDoreiP3 = true; // どれい 無し
            }
            else
            {
                NoneDoreiP3 = false; // どれい 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneDoreiP3 = true; // どれい無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 23) // どれい ではない
            {
                NoneDoreiP4 = true; // どれい 無し
            }
            else
            {
                NoneDoreiP4 = false; // どれい 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneDoreiP4 = true; // どれい無しON
        }

        if (NoneDoreiP1 && NoneDoreiP2 && NoneDoreiP3 && NoneDoreiP4)
        {
            NoneDorei = true; // どれい無しON
        }
        else
        {
            NoneDorei = false; // どれい無しOFF
        }
        //Debug.Log("NoneDoreiP1 ： " + NoneDoreiP1);
        //Debug.Log("NoneDoreiP2 ： " + NoneDoreiP2);
        //Debug.Log("NoneDoreiP3 ： " + NoneDoreiP3);
        //Debug.Log("NoneDoreiP4 ： " + NoneDoreiP4);
    }

    public void Check_Muteki_Existence()    //【JK-25】NoneMuteki の判定を返す
    {
        bool NoneMutekiP1 = true;
        bool NoneMutekiP2 = true;
        bool NoneMutekiP3 = true;
        bool NoneMutekiP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 601) // むてき ではない
            {
                NoneMutekiP1 = true; // むてき 無し
            }
            else
            {
                NoneMutekiP1 = false; // むてき 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneMutekiP1 = true; // むてき無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 601) // むてき ではない
            {
                NoneMutekiP2 = true; // むてき 無し
            }
            else
            {
                NoneMutekiP2 = false; // むてき 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneMutekiP2 = true; // むてき無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 601) // むてき ではない
            {
                NoneMutekiP3 = true; // むてき 無し
            }
            else
            {
                NoneMutekiP3 = false; // むてき 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneMutekiP3 = true; // むてき無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 601) // むてき ではない
            {
                NoneMutekiP4 = true; // むてき 無し
            }
            else
            {
                NoneMutekiP4 = false; // むてき 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneMutekiP4 = true; // むてき無しON
        }

        if (NoneMutekiP1 && NoneMutekiP2 && NoneMutekiP3 && NoneMutekiP4)
        {
            NoneMuteki = true; // むてき無しON
        }
        else
        {
            NoneMuteki = false; // むてき無しOFF
        }
        //Debug.Log("NoneMutekiP1 ： " + NoneMutekiP1);
        //Debug.Log("NoneMutekiP2 ： " + NoneMutekiP2);
        //Debug.Log("NoneMutekiP3 ： " + NoneMutekiP3);
        //Debug.Log("NoneMutekiP4 ： " + NoneMutekiP4);
    }

    public void Check_Wall_Existence()    //【JK-25】NoneWall の判定を返す
    {
        bool NoneWallP1 = true;
        bool NoneWallP2 = true;
        bool NoneWallP3 = true;
        bool NoneWallP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 88) // 壁 ではない
            {
                NoneWallP1 = true; // 壁 無し
            }
            else
            {
                NoneWallP1 = false; // 壁 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneWallP1 = true; // 壁無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 88) // 壁 ではない
            {
                NoneWallP2 = true; // 壁 無し
            }
            else
            {
                NoneWallP2 = false; // 壁 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneWallP2 = true; // 壁無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 88) // 壁 ではない
            {
                NoneWallP3 = true; // 壁 無し
            }
            else
            {
                NoneWallP3 = false; // 壁 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneWallP3 = true; // 壁無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 88) // 壁 ではない
            {
                NoneWallP4 = true; // 壁 無し
            }
            else
            {
                NoneWallP4 = false; // 壁 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneWallP4 = true; // 壁無しON
        }

        if (NoneWallP1 && NoneWallP2 && NoneWallP3 && NoneWallP4)
        {
            NoneWall = true; // 壁無しON
        }
        else
        {
            NoneWall = false; // 壁無しOFF
        }
        //Debug.Log("NoneWallP1 ： " + NoneWallP1);
        //Debug.Log("NoneWallP2 ： " + NoneWallP2);
        //Debug.Log("NoneWallP3 ： " + NoneWallP3);
        //Debug.Log("NoneWallP4 ： " + NoneWallP4);
    }

    public void Check_WFlag_Existence()    //【JK-25】NoneWFlag の判定を返す
    {
        bool NoneWFlagP1 = true;
        bool NoneWFlagP2 = true;
        bool NoneWFlagP3 = true;
        bool NoneWFlagP4 = true;

        int_WFlagP1 = 0;
        int_WFlagP2 = 0;
        int_WFlagP3 = 0;
        int_WFlagP4 = 0;
        NumWFlag_AllPlayer = 0;  // 白旗が場に何本あるか？

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 46) // 白旗 ではない
            {
                NoneWFlagP1 = true; // 白旗 無し
            }
            else
            {
                NoneWFlagP1 = false; // 白旗 有り
                int_WFlagP1 = 1;
            }
        }
        else  // Player1 が脱落後
        {
            NoneWFlagP1 = true; // 白旗無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 46) // 白旗 ではない
            {
                NoneWFlagP2 = true; // 白旗 無し
            }
            else
            {
                NoneWFlagP2 = false; // 白旗 有り
                int_WFlagP2 = 1;
            }
        }
        else  // Player2 が脱落後
        {
            NoneWFlagP2 = true; // 白旗無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 46) // 白旗 ではない
            {
                NoneWFlagP3 = true; // 白旗 無し
            }
            else
            {
                NoneWFlagP3 = false; // 白旗 有り
                int_WFlagP3 = 1;
            }
        }
        else  // Player3 が脱落後
        {
            NoneWFlagP3 = true; // 白旗無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 46) // 白旗 ではない
            {
                NoneWFlagP4 = true; // 白旗 無し
            }
            else
            {
                NoneWFlagP4 = false; // 白旗 有り
                int_WFlagP4 = 1;
            }
        }
        else  // Player4 が脱落後
        {
            NoneWFlagP4 = true; // 白旗無しON
        }

        if (NoneWFlagP1 && NoneWFlagP2 && NoneWFlagP3 && NoneWFlagP4)
        {
            NoneWFlag = true; // 白旗無しON
        }
        else
        {
            NoneWFlag = false; // 白旗無しOFF
        }
        //Debug.Log("NoneWFlagP1 ： " + NoneWFlagP1);
        //Debug.Log("NoneWFlagP2 ： " + NoneWFlagP2);
        //Debug.Log("NoneWFlagP3 ： " + NoneWFlagP3);
        //Debug.Log("NoneWFlagP4 ： " + NoneWFlagP4);

        CountLivePlayer();       //【JK-26】残留しているプレイヤー人数をカウントする★
        NumWFlag_AllPlayer = int_WFlagP1 + int_WFlagP2 + int_WFlagP3 + int_WFlagP4;  // 白旗が場に何本あるか？
    }

    public void Syohai_Hantei()         //【JK-25】生存者同士の 勝ち負けを判定（負けた人のaliveフラグ を 0 にする） → 人数が減る ＆＆ 移動ステップ数を 勝った手に応じて上書き ＆＆ 負けた人の黒カバー表示
    {
        //Debug.Log("");
        //Debug.Log("");
        //Debug.Log("【JK-25】Syohai_Hantei スタート");
        //Debug.Log("");
        //Debug.Log("");
        CountLivePlayer();       //【JK-26】残留しているプレイヤー人数をカウントする★
        Check_Gu_Existence();                //【JK-25】 N回目の すべてのプレイヤーの手 の中に グー(0)     があるか ： NoneGu     を取得
        Check_Choki_Existence();             //【JK-25】 N回目の すべてのプレイヤーの手 の中に チョキ(1)   があるか ： NoneChoki  を取得
        Check_Pa_Existence();                //【JK-25】 N回目の すべてのプレイヤーの手 の中に パー(2)     があるか ： NonePa     を取得
        Check_King_Existence();              //【JK-25】 N回目の すべてのプレイヤーの手 の中に 王さま(13)  があるか ： NoneKing   を取得
        Check_Dorei_Existence();             //【JK-25】 N回目の すべてのプレイヤーの手 の中に どれい(23)  があるか ： NoneDorei  を取得
        Check_Muteki_Existence();            //【JK-25】 N回目の すべてのプレイヤーの手 の中に むてき(601) があるか ： NoneMuteki を取得
        Check_Wall_Existence();              //【JK-25】 N回目の すべてのプレイヤーの手 の中に 壁(88)      があるか ： NoneWall   を取得
        Check_WFlag_Existence();             //【JK-25】 N回目の すべてのプレイヤーの手 の中に 白旗(46)    があるか ： NoneWFlag  を取得

        if (!NoneWFlag) // WFlagあり → 白旗一人負け or 全員白旗ならあいこ→ 残りプレイヤーの判定
        {
            CountLivePlayer();               //【JK-26】残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
            NumWFlag_AllPlayer = int_WFlagP1 + int_WFlagP2 + int_WFlagP3 + int_WFlagP4;  // 白旗が場に何本あるか？
            if (NumLivePlayer == NumWFlag_AllPlayer)
            {
                Aiko();                      //    ⇒  全員白旗なので あいこ
            }
            else
            {
                Lose_WFlag();                //    ⇒  白旗 の人のみ 脱落
            }
        }

        if (!NoneKing && !NoneDorei)     // Kingあり Doreiあり → どれい 一人勝ち
        {
            Lose_Gu();      //    ⇒  ぐー   脱落
            Lose_Choki();   //    ⇒  ちょき 脱落
            Lose_Pa();      //    ⇒  ぱー   脱落
            Lose_King();    //    ⇒  王さま 脱落
            Win_Dorei();    //    ⇒  どれい かち
            Lose_Muteki();  //    ⇒  むてき 脱落
            Lose_Wall();    //    ⇒  壁     脱落
        }

        else if (!NoneKing && NoneDorei) // Kingあり Dorei無し → 王さま 一人勝ち
        {
            Lose_Gu();      //    ⇒  ぐー   脱落
            Lose_Choki();   //    ⇒  ちょき 脱落
            Lose_Pa();      //    ⇒  ぱー   脱落
            Win_King();     //    ⇒  王さま かち
            Lose_Dorei();   //    ⇒  どれい 脱落
            Lose_Muteki();  //    ⇒  むてき 脱落
            Lose_Wall();    //    ⇒  壁     脱落
        }

        else if (NoneKing && !NoneDorei) // King無し Doreiあり → どれい 一人負け → 今まで通りの判定（市民同士のみ）
        {
            if (NoneWall && NoneMuteki && NonePa && NoneChoki && NoneGu)  // 市民カードが存在していないなら
            {
                Aiko();                            //    ⇒  全員どれいなので あいこ
            }
            else                                   // 市民が存在するなら
            {
                Lose_Dorei();                      //    ⇒  どれい の人のみ 脱落
                Shimin_Syohai_Hantei_Part();       // 市民同士のみ ノーマル勝敗判定パート
            }
        }

        else if (NoneKing && NoneDorei)        // King無し Dorei無し → 今まで通りの判定（市民同士のみ）
        {
            Shimin_Syohai_Hantei_Part();       // 市民同士のみ ノーマル勝敗判定パート
        }
    }

    public void Shimin_Syohai_Hantei_Part()    // 市民同士のみ 勝敗判定パート
    {
        if (!NoneMuteki && NoneWall)           // Mutekiあり Wall無し → むてき 一人勝ち
        {
            Lose_Gu();     //    ⇒  ぐー   脱落
            Lose_Choki();  //    ⇒  ちょき 脱落
            Lose_Pa();     //    ⇒  ぱー   脱落
            Win_Muteki();  //    ⇒  むてき かち
        }

        else if (!NoneMuteki && !NoneWall)    // Mutekiあり Wallあり → Muteki も Wall もあいこ /それ以外は負け確定      ※ここではまだ勝者は確定しない！！
        {
            Lose_Gu();     //    ⇒  ぐー   脱落
            Lose_Choki();  //    ⇒  ちょき 脱落
            Lose_Pa();     //    ⇒  ぱー   脱落
            Aiko();        //    ⇒  Muteki も Wall もあいこ
        }

        else if (NoneMuteki && !NoneWall)    // Mutekiなし Wallあり → Wall はあいこ残り /それ以外は通常通り勝ち負け判定へ   ※ここでは（基本的に）まだ勝者は確定しない！！
        {
            Normal_GuChoPa_Hantei();         // 通常通りの勝ち負け判定      ※ここでは（基本的に）まだ勝者は確定しない！！  （ただし、この時点で残り一名の場合は壁でも勝利する）
            Win_Wall();    //    ⇒  壁     あいこ残り
        }

        else if (NoneMuteki && NoneWall)     // Mutekiなし Wallなし → 通常通り勝ち負け判定へ
        {
            Normal_GuChoPa_Hantei();         // 通常通りの勝ち負け判定（ぐーちょきぱーのみ）
        }
    }

    public void Normal_GuChoPa_Hantei()  // 通常通りの勝ち負け判定（ぐーちょきぱーのみ）
    {
        if (NoneGu)           //【JK-25】（全員 Gu 無し）ちょき か ぱー
        {
            //Debug.Log("【JK-25】(NoneGu) （全員 Gu 無し）ちょき か ぱー です");
            if (NoneChoki)    //（全員 Choki 無し）ぱー のみ
            {
                //Debug.Log("【JK-25】(NoneGu) ⇒ ぱー のみ あいこ");
                Aiko();       //    ⇒ ぱー のみ
            }
            else if (NonePa)  // (全員 Pa 無し) ちょき のみ
            {
                //Debug.Log("【JK-25】(NoneGu) ⇒ ちょき のみ あいこ");
                Aiko();       //    ⇒ ちょき のみ
            }
            else              // ちょき と ぱー
            {
                //Debug.Log("【JK-25】(NoneGu) ちょき と ぱー");
                Win_Choki();
                Lose_Pa();    //    ⇒  ぱー の人のみ 脱落
            }
        }
        else if (NoneChoki)   //【JK-25】(全員 Choki 無し) ぐー か ぱー （↓これ以降、ぐー は必ずある）
        {
            //Debug.Log("【JK-25】(NoneChoki) (全員 Choki 無し) ぐー か ぱー です");
            if (NoneGu)       // (全員 Gu 無し) ぱー のみ
            {
                //Debug.Log("【JK-25】(NoneChoki) ⇒  ぱー のみ あいこ");
                Aiko();       //    ⇒  ぱー のみ
            }
            else if (NonePa)  // (全員 Pa 無し) ぐー のみ
            {
                //Debug.Log("【JK-25】(NoneChoki) ⇒  ぐー のみ あいこ");
                Aiko();       //    ⇒  ぐー のみ
            }
            else              // ぐー と ぱー
            {
                //Debug.Log("【JK-25】(NoneChoki) ぐー と ぱー");
                Win_Pa();
                Lose_Gu();    //    ⇒  ぐー の人のみ 脱落
            }
        }
        else if (NonePa)      //【JK-25】(全員 Pa 無し)  ぐー か ちょき （↓これ以降、ぐー と ちょき は必ずある）
        {
            //Debug.Log("【JK-25】(NonePa) (全員 Pa 無し)  ぐー か ちょき です");
            if (NoneGu)          // (全員 Gu 無し) ちょき のみ
            {
                //Debug.Log("【JK-25】(NonePa) ⇒  ちょき のみ あいこ");
                Aiko();          //    ⇒  ちょき のみ
            }
            else if (NoneChoki)  //（全員 Choki 無し）ぐー のみ
            {
                //Debug.Log("【JK-25】(NonePa) ⇒  ぐー のみ あいこ");
                Aiko();          //    ⇒  ぐー のみ
            }
            else                 // ぐー と ちょき
            {
                //Debug.Log("【JK-25】(NonePa) ぐー と ちょき");
                Win_Gu();
                Lose_Choki();    //    ⇒  ちょき の人のみ 脱落
            }
        }
        else                     //【JK-25】 ぐー か ちょき か ぱー（↓これ以降、ぐー と ちょき と ぱー は必ずある）
        {
            //Debug.Log("【JK-25】ぐー ちょき ぱー 全部 で あいこ");
            Aiko();              // ぐー ちょき ぱー 全部
        }
    }

    public void Aiko()              //【JK-25】残っている人、全員残留
    {
        // 残っている人、全員残留
        //Debug.Log("【JK-25】あいこ です");
        photonView.RPC("ShareInfo_Aiko", RpcTarget.All);
    }

    [PunRPC]
    public void ShareInfo_Aiko()    //【JK-25】あいこを 全員に共有
    {
        //Debug.Log("【JK-25】あいこ です");
    }

    #region                   // Janken_Win_Part
    public void Win_Gu()            //【JK-25】ぐー の人のみ 残留（移動ステップ数を 3 に上書き）
    {
        // ぐー の人のみ 残留
        //Debug.Log("【JK-25】ぐー の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Gu", RpcTarget.All);    //【JK-25】 ぐー の人のみ 残留（Win_Gu）を 全員に共有
        original_StepNum = 3;       // 移動ステップ数を 3 に上書き
        photonView.RPC("ShareStepNum_3", RpcTarget.All);    // 移動ステップ数を 3 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Gu()  //【JK-25】 ぐー の人のみ 残留（Win_Gu）を 全員に共有
    {
        //Debug.Log("【JK-25】ぐー の人のみ 残留（Win_Gu）");
    }

    public void Win_Choki()         //【JK-25】ちょき の人のみ 残留（移動ステップ数を 6 に上書き）
    {
        // ちょき の人のみ 残留
        //Debug.Log("【JK-25】ちょき の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Choki", RpcTarget.All);    //【JK-25】 ちょき の人のみ 残留（Win_Choki）を 全員に共有
        original_StepNum = 6;      // 移動ステップ数を 6 に上書き
        photonView.RPC("ShareStepNum_6", RpcTarget.All);    // 移動ステップ数を 6 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Choki()   //【JK-25】 ちょき の人のみ 残留（Win_Choki）を 全員に共有
    {
        //Debug.Log("【JK-25】ちょき の人のみ 残留");
    }

    public void Win_Pa()               //【JK-25】ぱー の人のみ 残留（移動ステップ数を 6 に上書き）
    {
        // ぱー の人のみ 残留
        //Debug.Log("【JK-25】ぱー の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Pa", RpcTarget.All);    //【JK-25】 ぱー の人のみ 残留（Win_Pa）を 全員に共有
        original_StepNum = 6;     // 移動ステップ数を 6 に上書き
        photonView.RPC("ShareStepNum_6", RpcTarget.All);    // 移動ステップ数を 6 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Pa()   //【JK-25】 ぱー の人のみ 残留（Win_Pa）を 全員に共有
    {
        //Debug.Log("【JK-25】ぱー の人のみ 残留");
    }

    public void Win_King()               //【JK-25】王さま の人のみ 残留（移動ステップ数を 4 に上書き）
    {
        // 王さま の人のみ 残留
        //Debug.Log("【JK-25】王さま の人のみ 残留");
        photonView.RPC("ShareInfo_Win_King", RpcTarget.All);    //【JK-25】 王さま の人のみ 残留（Win_King）を 全員に共有
        original_StepNum = 4;     // 移動ステップ数を 4 に上書き
        photonView.RPC("ShareStepNum_4", RpcTarget.All);    // 移動ステップ数を 4 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_King()   //【JK-25】 王さま の人のみ 残留（Win_King）を 全員に共有
    {
        //Debug.Log("【JK-25】王さま の人のみ 残留");
    }

    public void Win_Dorei()               //【JK-25】どれい の人のみ 残留（移動ステップ数を 15 に上書き）
    {
        // どれい の人のみ 残留
        //Debug.Log("【JK-25】どれい の人のみ 残留");
        photonView.RPC("ShareInfo_Win_Dorei", RpcTarget.All);    //【JK-25】 どれい の人のみ 残留（Win_Dorei）を 全員に共有
        original_StepNum = 15;     // 移動ステップ数を 15 に上書き
        photonView.RPC("ShareStepNum_15", RpcTarget.All);    // 移動ステップ数を 15 に上書き → 全員に共有
    }
    [PunRPC]
    public void ShareInfo_Win_Dorei()   //【JK-25】 どれい の人のみ 残留（Win_Dorei）を 全員に共有
    {
        //Debug.Log("【JK-25】どれい の人のみ 残留");
    }

    public void Win_Muteki()               //【JK-25】むてき の人のみ 残留（移動ステップ数を 15 に上書き）
    {
        //Debug.Log("【JK-25】むてき の人のみ 残留");
        original_StepNum = 5;     // 移動ステップ数を 5 に上書き
        photonView.RPC("ShareStepNum_5", RpcTarget.All);    // 移動ステップ数を 5 に上書き → 全員に共有
    }

    public void Win_Wall()               //【JK-25】壁 の人のみ 残留（移動ステップ数を 15 に上書き）
    {
        //Debug.Log("【JK-25】壁 の人のみ 残留");
        original_StepNum = 1;     // 移動ステップ数を 5 に上書き
        photonView.RPC("ShareStepNum_1", RpcTarget.All);    // 移動ステップ数を 5 に上書き → 全員に共有
    }

    #endregion

    #region                   // Janken_Lose_Part
    public void Lose_Gu()     //【JK-25】ぐー の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】ぐー（0） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 0)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 0)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 0)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 0)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_Choki()  //【JK-25】ちょき の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】ちょき（1） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 1)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 1)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 1)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 1)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_Pa()     //【JK-25】ぱー の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】ぱー（2） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 2)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 2)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 2)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 2)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_King()     //【JK-25】王さま の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】王さま（13） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 13)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 13)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 13)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 13)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_Dorei()     //【JK-25】どれい の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】どれい（23） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 23)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 23)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 23)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 23)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_Muteki()     //【JK-25】むてき の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】むてき（601） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 601)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 601)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 601)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 601)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_Wall()     //【JK-25】壁 の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】壁（88） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 88)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 88)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 88)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 88)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }

    public void Lose_WFlag()     //【JK-25】白旗 の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる
    {
        //Debug.Log("【JK-25】白旗（46） の人のみ 脱落  ：aliveフラグ を 0 にする  ＆ 黒カバーを表示させる");
        if (KP1 == 46)
        {
            alivePlayer1 = 0;
            photonView.RPC("Share_alivePlayer1_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P1", RpcTarget.All);
        }
        if (KP2 == 46)
        {
            alivePlayer2 = 0;
            photonView.RPC("Share_alivePlayer2_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P2", RpcTarget.All);
        }
        if (KP3 == 46)
        {
            alivePlayer3 = 0;
            photonView.RPC("Share_alivePlayer3_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P3", RpcTarget.All);
        }
        if (KP4 == 46)
        {
            alivePlayer4 = 0;
            photonView.RPC("Share_alivePlayer4_dead", RpcTarget.All);
            photonView.RPC("AppearImg_CoverBlack_P4", RpcTarget.All);
        }
    }
    #endregion


    #region                   //【JK-25】 移動ステップ数 の共有
    [PunRPC]
    public void ShareStepNum_1()   //【JK-25】 移動ステップ数を 1 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 1;     // 移動ステップ数を 1 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 1 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_3()   //【JK-25】 移動ステップ数を 3 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 3;     // 移動ステップ数を 3 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 3 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_4()   //【JK-25】 移動ステップ数を 4 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 4;     // 移動ステップ数を 4 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 4 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_5()   //【JK-25】 移動ステップ数を 4 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 5;     // 移動ステップ数を 5 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 5 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_6()   //【JK-25】 移動ステップ数を 6 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 6;     // 移動ステップ数を 6 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 6 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_8()   //【JK-25】 移動ステップ数を 8 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 8;     // 移動ステップ数を 8 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 8 に上書きしました");
    }

    [PunRPC]
    public void ShareStepNum_15()   //【JK-25】 移動ステップ数を 15 に上書き → 全員に共有
    {
        WhoAreYou();
        original_StepNum = 15;     // 移動ステップ数を 15 に上書き
        //Debug.Log("移動ステップ数（original_StepNum）を 15 に上書きしました");
    }
    #endregion

    #region // alivePlayer (ジャンケン生存判定フラグ) 関連
    [PunRPC]
    public void Share_alivePlayer1_alive()
    {
        alivePlayer1 = 1;
    }

    [PunRPC]
    public void Share_alivePlayer2_alive()
    {
        alivePlayer2 = 1;
    }

    [PunRPC]
    public void Share_alivePlayer3_alive()
    {
        alivePlayer3 = 1;
    }

    [PunRPC]
    public void Share_alivePlayer4_alive()
    {
        alivePlayer4 = 1;
    }


    [PunRPC]
    public void Share_alivePlayer1_dead()
    {
        alivePlayer1 = 0;
    }

    [PunRPC]
    public void Share_alivePlayer2_dead()
    {
        alivePlayer2 = 0;
    }

    [PunRPC]
    public void Share_alivePlayer3_dead()
    {
        alivePlayer3 = 0;
    }

    [PunRPC]
    public void Share_alivePlayer4_dead()
    {
        alivePlayer4 = 0;
    }
    #endregion



    #region // 黒カバーの表示・非表示
    [PunRPC]
    public void AppearImg_CoverBlack_P1() // 黒カバー 表示させる
    {
        Img_CoverBlack_P1.enabled = true;
        Make_Black1.SetActive(true);
    }

    [PunRPC]
    public void AppearImg_CoverBlack_P2() // 黒カバー 表示させる
    {
        Img_CoverBlack_P2.enabled = true;
        Make_Black2.SetActive(true);
    }

    [PunRPC]
    public void AppearImg_CoverBlack_P3() // 黒カバー 表示させる
    {
        Img_CoverBlack_P3.enabled = true;
        Make_Black3.SetActive(true);
    }

    [PunRPC]
    public void AppearImg_CoverBlack_P4() // 黒カバー 表示させる
    {
        Img_CoverBlack_P4.enabled = true;
        Make_Black4.SetActive(true);
    }

    [PunRPC]
    public void CloseImg_CoverBlack_All()  // すべての黒カバーを閉じる（消す）
    {
        CloseImg_CoverBlack_P1();
        CloseImg_CoverBlack_P2();
        CloseImg_CoverBlack_P3();
        CloseImg_CoverBlack_P4();
    }

    [PunRPC]
    public void CloseImg_CoverBlack_P1() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P1.enabled = false;
        Make_Black1.SetActive(false);
    }

    [PunRPC]
    public void CloseImg_CoverBlack_P2() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P2.enabled = false;
        Make_Black2.SetActive(false);
    }

    [PunRPC]
    public void CloseImg_CoverBlack_P3() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P3.enabled = false;
        Make_Black3.SetActive(false);
    }

    [PunRPC]
    public void CloseImg_CoverBlack_P4() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P4.enabled = false;
        Make_Black4.SetActive(false);
    }
    #endregion


    #region // 白カバーの表示・非表示
    [PunRPC]
    public void AppearKachi_White1()
    {
        Kachi_White1.SetActive(true);
    }

    [PunRPC]
    public void AppearKachi_White2()
    {
        Kachi_White2.SetActive(true);
    }

    [PunRPC]
    public void AppearKachi_White3()
    {
        Kachi_White3.SetActive(true);
    }

    [PunRPC]
    public void AppearKachi_White4()
    {
        Kachi_White4.SetActive(true);
    }

    public void CloseKachi_White_All()  // すべての白カバーを閉じる（消す）
    {
        CloseKachi_White1();
        CloseKachi_White2();
        CloseKachi_White3();
        CloseKachi_White4();
    }

    [PunRPC]
    public void CloseKachi_White1()
    {
        Kachi_White1.SetActive(false);
    }

    [PunRPC]
    public void CloseKachi_White2()
    {
        Kachi_White2.SetActive(false);
    }

    [PunRPC]
    public void CloseKachi_White3()
    {
        Kachi_White3.SetActive(false);
    }

    [PunRPC]
    public void CloseKachi_White4()
    {
        Kachi_White4.SetActive(false);
    }
    #endregion

    #region // 掛け軸の表示・非表示
    [PunRPC]
    public void CloseMyKakejiku()    // 自分はログインしているので、掛け軸外しますよ
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("CloseLogout_Kakejiku1", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("CloseLogout_Kakejiku2", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("CloseLogout_Kakejiku3", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("CloseLogout_Kakejiku4", RpcTarget.All);
        }
    }

    public void CheckLogout_Kakejiku()   // ログアウトしてたら掛け軸表示、alivePlayerフラグ を0 。ログインしていたら掛け軸を取る
    {
        if (logon_player1 == false)      // player1 が退出しました
        {
            alivePlayer1 = 0;            // これ以降、常に alivePlayerフラグ が 0 になる
            int_NowWaiting_Player1 =0;  // これ以降、常に待機フラグが OFFになる
            AppearLogout_Kakejiku1();    // 「ログアウト中」を表示させる
        }
        else
        {
            CloseLogout_Kakejiku1();
        }

        if (logon_player2 == false)
        {
            alivePlayer2 = 0;            // これ以降、常に alivePlayerフラグ が 0 になる
            int_NowWaiting_Player2 =0;  // これ以降、常に待機フラグが OFFになる
            AppearLogout_Kakejiku2();
        }
        else
        {
            CloseLogout_Kakejiku2();
        }

        if (logon_player3 == false)
        {
            alivePlayer3 = 0;            // これ以降、常に alivePlayerフラグ が 0 になる
            int_NowWaiting_Player3 =0;  // これ以降、常に待機フラグが OFFになる
            AppearLogout_Kakejiku3();
        }
        else
        {
            CloseLogout_Kakejiku3();
        }

        if (logon_player4 == false)
        {
            alivePlayer4 = 0;            // これ以降、常に alivePlayerフラグ が 0 になる
            int_NowWaiting_Player4 =0;  // これ以降、常に待機フラグが OFFになる
            AppearLogout_Kakejiku4();
        }
        else
        {
            CloseLogout_Kakejiku4();
        }
    }

    [PunRPC]
    public void AppearLogout_Kakejiku_All()  // すべての掛け軸を開く
    {
        AppearLogout_Kakejiku1();
        AppearLogout_Kakejiku2();
        AppearLogout_Kakejiku3();
        AppearLogout_Kakejiku4();
    }

    [PunRPC]
    public void AppearLogout_Kakejiku1()
    {
        Logout_Kakejiku1.SetActive(true);
    }

    [PunRPC]
    public void AppearLogout_Kakejiku2()
    {
        Logout_Kakejiku2.SetActive(true);
    }

    [PunRPC]
    public void AppearLogout_Kakejiku3()
    {
        Logout_Kakejiku3.SetActive(true);
    }

    [PunRPC]
    public void AppearLogout_Kakejiku4()
    {
        Logout_Kakejiku4.SetActive(true);
    }

    public void CloseLogout_Kakejiku_All()  // すべての掛け軸を閉じる（消す）
    {
        CloseLogout_Kakejiku1();
        CloseLogout_Kakejiku2();
        CloseLogout_Kakejiku3();
        CloseLogout_Kakejiku4();
    }

    [PunRPC]
    public void CloseLogout_Kakejiku1()
    {
        Logout_Kakejiku1.SetActive(false);
    }

    [PunRPC]
    public void CloseLogout_Kakejiku2()
    {
        Logout_Kakejiku2.SetActive(false);
    }

    [PunRPC]
    public void CloseLogout_Kakejiku3()
    {
        Logout_Kakejiku3.SetActive(false);
    }

    [PunRPC]
    public void CloseLogout_Kakejiku4()
    {
        Logout_Kakejiku4.SetActive(false);
    }


    [PunRPC]
    public void Still_Login_Player1()  // player1 がまだ入出中です & 掛け軸を閉じます
    {
        logon_player1 = true;
        CloseLogout_Kakejiku1();    // 掛け軸を閉じる 
    }

    [PunRPC]
    public void Still_Login_Player2()  // player2 がまだ入出中です & 掛け軸を閉じます
    {
        logon_player2 = true;
        CloseLogout_Kakejiku2();    // 掛け軸を閉じる
    }

    [PunRPC]
    public void Still_Login_Player3()  // player3 がまだ入出中です & 掛け軸を閉じます
    {
        logon_player3 = true;
        CloseLogout_Kakejiku3();    // 掛け軸を閉じる
    }

    [PunRPC]
    public void Still_Login_Player4()  // player4 がまだ入出中です & 掛け軸を閉じます
    {
        logon_player4 = true;
        CloseLogout_Kakejiku4();    // 掛け軸を閉じる
    }
    #endregion

    #endregion


    public void ToSharePlayerTeNum()  // 【JK-06】私のジャンケン手をみんなに提供（共有）します
    {
        NinzuCheck();                       // 【START-10】【JK-12】現時点の参加人数を更新し、総参加人数 と 現在待機中の総人数 を確認します
        SharePlayerTeNum();
    }

    public void SharePlayerTeNum()    // 【JK-06】現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）【JK-08】
    {
        //Debug.Log("【JK-06】************ データ共有 SharePlayerTeNum  スタート **********");
        WhoAreYou();     // 私の名前（真名）を表示

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1)
        {
            //Debug.Log("【JK-07】今からプレイヤー1＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player1();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2)
        {
            //Debug.Log("【JK-07】今からプレイヤー2＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player2();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3)
        {
            //Debug.Log("【JK-07】今からプレイヤー3＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player3();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4)
        {
            //Debug.Log("【JK-07】今からプレイヤー4＝私（" + AcutivePlayerName + "）のジャンケン手をみんなに提供（共有）します");
            SharePlayerTeNum_Player4();
        }

        //Debug.Log("【JK-08】************ データ共有 SharePlayerTeNum おわり **********");
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

        //Debug.Log("【JK-32】PlayerTeNum を すべて「-1」に リセットしました");
    }

    public void Reset_MyRireki_All()  // 【JK-30】MyRireki イメージを null にリセット（Image）
    {
        //Debug.Log("【JK-30】MyRireki イメージを null にリセット（Image）します");
        MyTeImg_1.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_2.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_3.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_4.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_5.gameObject.GetComponent<Image>().sprite = null;

        MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
    }

    public void ResetImg_PlayerlayerRireki_All()  // 【JK-33】Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）
    {
        //Debug.Log("【JK-33】Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）");

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

        //Debug.Log("【JK-33】ジャンケンカード裏 sprite_CardUra（Image）をセットします");

        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_CardUra;

        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_CardUra;

        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_CardUra;

        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_CardUra;
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

    public void ToSharePlayerTeNum_Player1_1_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_King()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player1_Te1 = 13;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "13";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player1_Te1 = 23;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "23";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player1_Te1 = 601;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "601";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player1_Te1 = 88;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "88";
    }

    public void ToSharePlayerTeNum_Player1_1_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player1_Te1 = 46;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_2
    public void ToSharePlayerTeNum_Player1_2_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player1_Te2 = 0;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player1_Te2 = 1;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player1_Te2 = 2;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "2";
    }

    public void ToSharePlayerTeNum_Player1_2_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_King()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player1_Te2 = 13;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "13";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player1_Te2 = 23;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "23";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player1_Te2 = 601;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "601";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player1_Te2 = 88;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "88";
    }

    public void ToSharePlayerTeNum_Player1_2_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player1_Te2 = 46;

        // Image の反映
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer1_Te2.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_3
    public void ToSharePlayerTeNum_Player1_3_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player1_Te3 = 0;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player1_Te3 = 1;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player1_Te3 = 2;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "2";
    }

    public void ToSharePlayerTeNum_Player1_3_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_King()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player1_Te3 = 13;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "13";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player1_Te3 = 23;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "23";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player1_Te3 = 601;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "601";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player1_Te3 = 88;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "88";
    }

    public void ToSharePlayerTeNum_Player1_3_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player1_Te3 = 46;

        // Image の反映
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer1_Te3.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_4
    public void ToSharePlayerTeNum_Player1_4_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player1_Te4 = 0;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player1_Te4 = 1;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player1_Te4 = 2;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "2";
    }

    public void ToSharePlayerTeNum_Player1_4_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_King()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player1_Te4 = 13;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "13";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player1_Te4 = 23;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "23";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player1_Te4 = 601;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "601";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player1_Te4 = 88;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "88";
    }

    public void ToSharePlayerTeNum_Player1_4_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player1_Te4 = 46;

        // Image の反映
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer1_Te4.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player1_5
    public void ToSharePlayerTeNum_Player1_5_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player1_Te5 = 0;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player1_Te5 = 1;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player1_Te5 = 2;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "2";
    }

    public void ToSharePlayerTeNum_Player1_5_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_King()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player1_Te5 = 13;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "13";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player1_Te5 = 23;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "23";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player1_Te5 = 601;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "601";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player1_Te5 = 88;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "88";
    }

    public void ToSharePlayerTeNum_Player1_5_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player1_Te5 = 46;

        // Image の反映
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer1_Te5.text = "46";
    }

    #endregion
    #endregion

    #region// ●【JK-07】Num_Player2
    #region// 【JK-07】PlayerTeNum_Player2_1
    public void ToSharePlayerTeNum_Player2_1_is_Gu()  //【JK-07】
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

    public void ToSharePlayerTeNum_Player2_1_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_King()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player2_Te1 = 13;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "13";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player2_Te1 = 23;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "23";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player2_Te1 = 601;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "601";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player2_Te1 = 88;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "88";
    }

    public void ToSharePlayerTeNum_Player2_1_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player2_Te1 = 46;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player2_2
    public void ToSharePlayerTeNum_Player2_2_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player2_Te2 = 0;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player2_Te2 = 1;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player2_Te2 = 2;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "2";
    }

    public void ToSharePlayerTeNum_Player2_2_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_King()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player2_Te2 = 13;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "13";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player2_Te2 = 23;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "23";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player2_Te2 = 601;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "601";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player2_Te2 = 88;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "88";
    }

    public void ToSharePlayerTeNum_Player2_2_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player2_Te2 = 46;

        // Image の反映
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer2_Te2.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player2_3
    public void ToSharePlayerTeNum_Player2_3_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player2_Te3 = 0;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player2_Te3 = 1;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player2_Te3 = 2;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "2";
    }

    public void ToSharePlayerTeNum_Player2_3_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_King()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player2_Te3 = 13;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "13";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player2_Te3 = 23;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "23";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player2_Te3 = 601;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "601";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player2_Te3 = 88;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "88";
    }

    public void ToSharePlayerTeNum_Player2_3_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player2_Te3 = 46;

        // Image の反映
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer2_Te3.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player2_4
    public void ToSharePlayerTeNum_Player2_4_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player2_Te4 = 0;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player2_Te4 = 1;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player2_Te4 = 2;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "2";
    }

    public void ToSharePlayerTeNum_Player2_4_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_King()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player2_Te4 = 13;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "13";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player2_Te4 = 23;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "23";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player2_Te4 = 601;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "601";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player2_Te4 = 88;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "88";
    }

    public void ToSharePlayerTeNum_Player2_4_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player2_Te4 = 46;

        // Image の反映
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer2_Te4.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player2_5
    public void ToSharePlayerTeNum_Player2_5_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player2_Te5 = 0;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player2_Te5 = 1;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player2_Te5 = 2;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "2";
    }

    public void ToSharePlayerTeNum_Player2_5_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_King()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player2_Te5 = 13;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "13";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player2_Te5 = 23;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "23";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player2_Te5 = 601;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "601";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player2_Te5 = 88;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "88";
    }

    public void ToSharePlayerTeNum_Player2_5_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player2_Te5 = 46;

        // Image の反映
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer2_Te5.text = "46";
    }

    #endregion
    #endregion

    #region// ●【JK-07】Num_Player3
    #region// 【JK-07】PlayerTeNum_Player3_1
    public void ToSharePlayerTeNum_Player3_1_is_Gu()  //【JK-07】
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

    public void ToSharePlayerTeNum_Player3_1_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_King()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player3_Te1 = 13;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "13";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player3_Te1 = 23;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "23";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player3_Te1 = 601;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "601";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player3_Te1 = 88;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "88";
    }

    public void ToSharePlayerTeNum_Player3_1_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player3_Te1 = 46;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_2
    public void ToSharePlayerTeNum_Player3_2_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player3_Te2 = 0;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player3_Te2 = 1;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player3_Te2 = 2;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "2";
    }

    public void ToSharePlayerTeNum_Player3_2_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_King()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player3_Te2 = 13;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "13";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player3_Te2 = 23;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "23";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player3_Te2 = 601;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "601";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player3_Te2 = 88;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "88";
    }

    public void ToSharePlayerTeNum_Player3_2_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player3_Te2 = 46;

        // Image の反映
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer3_Te2.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_3
    public void ToSharePlayerTeNum_Player3_3_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player3_Te3 = 0;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player3_Te3 = 1;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player3_Te3 = 2;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "2";
    }

    public void ToSharePlayerTeNum_Player3_3_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_King()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player3_Te3 = 13;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "13";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player3_Te3 = 23;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "23";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player3_Te3 = 601;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "601";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player3_Te3 = 88;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "88";
    }

    public void ToSharePlayerTeNum_Player3_3_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player3_Te3 = 46;

        // Image の反映
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer3_Te3.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_4
    public void ToSharePlayerTeNum_Player3_4_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player3_Te4 = 0;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player3_Te4 = 1;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player3_Te4 = 2;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "2";
    }

    public void ToSharePlayerTeNum_Player3_4_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_King()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player3_Te4 = 13;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "13";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player3_Te4 = 23;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "23";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player3_Te4 = 601;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "601";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player3_Te4 = 88;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "88";
    }

    public void ToSharePlayerTeNum_Player3_4_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player3_Te4 = 46;

        // Image の反映
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer3_Te4.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player3_5
    public void ToSharePlayerTeNum_Player3_5_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player3_Te5 = 0;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player3_Te5 = 1;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player3_Te5 = 2;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "2";
    }

    public void ToSharePlayerTeNum_Player3_5_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_King()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player3_Te5 = 13;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "13";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player3_Te5 = 23;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "23";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player3_Te5 = 601;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "601";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player3_Te5 = 88;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "88";
    }

    public void ToSharePlayerTeNum_Player3_5_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player3_Te5 = 46;

        // Image の反映
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer3_Te5.text = "46";
    }

    #endregion
    #endregion

    #region// ●【JK-07】Num_Player4
    #region// 【JK-07】PlayerTeNum_Player4_1
    public void ToSharePlayerTeNum_Player4_1_is_Gu()  //【JK-07】
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

    public void ToSharePlayerTeNum_Player4_1_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_King()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player4_Te1 = 13;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "13";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player4_Te1 = 23;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "23";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player4_Te1 = 601;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "601";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player4_Te1 = 88;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "88";
    }

    public void ToSharePlayerTeNum_Player4_1_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player4_Te1 = 46;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_2
    public void ToSharePlayerTeNum_Player4_2_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player4_Te2 = 0;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player4_Te2 = 1;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player4_Te2 = 2;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "2";
    }

    public void ToSharePlayerTeNum_Player4_2_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_King()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player4_Te2 = 13;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "13";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player4_Te2 = 23;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "23";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player4_Te2 = 601;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "601";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player4_Te2 = 88;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "88";
    }

    public void ToSharePlayerTeNum_Player4_2_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player4_Te2 = 46;

        // Image の反映
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer4_Te2.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_3
    public void ToSharePlayerTeNum_Player4_3_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player4_Te3 = 0;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player4_Te3 = 1;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player4_Te3 = 2;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "2";
    }

    public void ToSharePlayerTeNum_Player4_3_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_King()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player4_Te3 = 13;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "13";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player4_Te3 = 23;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "23";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player4_Te3 = 601;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "601";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player4_Te3 = 88;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "88";
    }

    public void ToSharePlayerTeNum_Player4_3_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player4_Te3 = 46;

        // Image の反映
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer4_Te3.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_4
    public void ToSharePlayerTeNum_Player4_4_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player4_Te4 = 0;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player4_Te4 = 1;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player4_Te4 = 2;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "2";
    }

    public void ToSharePlayerTeNum_Player4_4_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_King()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player4_Te4 = 13;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "13";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player4_Te4 = 23;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "23";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player4_Te4 = 601;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "601";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player4_Te4 = 88;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "88";
    }

    public void ToSharePlayerTeNum_Player4_4_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player4_Te4 = 46;

        // Image の反映
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer4_Te4.text = "46";
    }

    #endregion

    #region// 【JK-07】PlayerTeNum_Player4_5
    public void ToSharePlayerTeNum_Player4_5_is_Gu()  //【JK-07】
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Gu()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player4_Te5 = 0;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Choki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player4_Te5 = 1;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Pa()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player4_Te5 = 2;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "2";
    }

    public void ToSharePlayerTeNum_Player4_5_is_King()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_King", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_King()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（13：王さま）
    {
        // int の反映
        int_Player4_Te5 = 13;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_King;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "13";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Dorei()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Dorei", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Dorei()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（23：どれい）
    {
        // int の反映
        int_Player4_Te5 = 23;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Dorei;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "23";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Muteki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Muteki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Muteki()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（601：Muteki）
    {
        // int の反映
        int_Player4_Te5 = 601;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Muteki;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "601";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Wall()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Wall", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Wall()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（88：Wall）
    {
        // int の反映
        int_Player4_Te5 = 88;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Wall;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "88";
    }

    public void ToSharePlayerTeNum_Player4_5_is_WFlag()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_WFlag", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_WFlag()  //【JK-07】現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（46：WFlag）
    {
        // int の反映
        int_Player4_Te5 = 46;

        // Image の反映
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_WFlag;

        // Text の反映
        Text_JankenPlayer4_Te5.text = "46";
    }

    #endregion
    #endregion

    #region// ジャンケン手のセットに関する処理
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
        else if (Int_MyJanken_Te1 == 13)
        {
            ToSharePlayerTeNum_Player1_1_is_King();
        }
        else if (Int_MyJanken_Te1 == 23)
        {
            ToSharePlayerTeNum_Player1_1_is_Dorei();
        }
        else if (Int_MyJanken_Te1 == 601)
        {
            ToSharePlayerTeNum_Player1_1_is_Muteki();
        }
        else if (Int_MyJanken_Te1 == 88)
        {
            ToSharePlayerTeNum_Player1_1_is_Wall();
        }
        else if (Int_MyJanken_Te1 == 46)
        {
            ToSharePlayerTeNum_Player1_1_is_WFlag();
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
        else if (Int_MyJanken_Te2 == 13)
        {
            ToSharePlayerTeNum_Player1_2_is_King();
        }
        else if (Int_MyJanken_Te2 == 23)
        {
            ToSharePlayerTeNum_Player1_2_is_Dorei();
        }
        else if (Int_MyJanken_Te2 == 601)
        {
            ToSharePlayerTeNum_Player1_2_is_Muteki();
        }
        else if (Int_MyJanken_Te2 == 88)
        {
            ToSharePlayerTeNum_Player1_2_is_Wall();
        }
        else if (Int_MyJanken_Te2 == 46)
        {
            ToSharePlayerTeNum_Player1_2_is_WFlag();
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
        else if (Int_MyJanken_Te3 == 13)
        {
            ToSharePlayerTeNum_Player1_3_is_King();
        }
        else if (Int_MyJanken_Te3 == 23)
        {
            ToSharePlayerTeNum_Player1_3_is_Dorei();
        }
        else if (Int_MyJanken_Te3 == 601)
        {
            ToSharePlayerTeNum_Player1_3_is_Muteki();
        }
        else if (Int_MyJanken_Te3 == 88)
        {
            ToSharePlayerTeNum_Player1_3_is_Wall();
        }
        else if (Int_MyJanken_Te3 == 46)
        {
            ToSharePlayerTeNum_Player1_3_is_WFlag();
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
        else if (Int_MyJanken_Te4 == 13)
        {
            ToSharePlayerTeNum_Player1_4_is_King();
        }
        else if (Int_MyJanken_Te4 == 23)
        {
            ToSharePlayerTeNum_Player1_4_is_Dorei();
        }
        else if (Int_MyJanken_Te4 == 601)
        {
            ToSharePlayerTeNum_Player1_4_is_Muteki();
        }
        else if (Int_MyJanken_Te4 == 88)
        {
            ToSharePlayerTeNum_Player1_4_is_Wall();
        }
        else if (Int_MyJanken_Te4 == 46)
        {
            ToSharePlayerTeNum_Player1_4_is_WFlag();
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
        else if (Int_MyJanken_Te5 == 13)
        {
            ToSharePlayerTeNum_Player1_5_is_King();
        }
        else if (Int_MyJanken_Te5 == 23)
        {
            ToSharePlayerTeNum_Player1_5_is_Dorei();
        }
        else if (Int_MyJanken_Te5 == 601)
        {
            ToSharePlayerTeNum_Player1_5_is_Muteki();
        }
        else if (Int_MyJanken_Te5 == 88)
        {
            ToSharePlayerTeNum_Player1_5_is_Wall();
        }
        else if (Int_MyJanken_Te5 == 46)
        {
            ToSharePlayerTeNum_Player1_5_is_WFlag();
        }
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
        else if (Int_MyJanken_Te1 == 13)
        {
            ToSharePlayerTeNum_Player2_1_is_King();
        }
        else if (Int_MyJanken_Te1 == 23)
        {
            ToSharePlayerTeNum_Player2_1_is_Dorei();
        }
        else if (Int_MyJanken_Te1 == 601)
        {
            ToSharePlayerTeNum_Player2_1_is_Muteki();
        }
        else if (Int_MyJanken_Te1 == 88)
        {
            ToSharePlayerTeNum_Player2_1_is_Wall();
        }
        else if (Int_MyJanken_Te1 == 46)
        {
            ToSharePlayerTeNum_Player2_1_is_WFlag();
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
        else if (Int_MyJanken_Te2 == 13)
        {
            ToSharePlayerTeNum_Player2_2_is_King();
        }
        else if (Int_MyJanken_Te2 == 23)
        {
            ToSharePlayerTeNum_Player2_2_is_Dorei();
        }
        else if (Int_MyJanken_Te2 == 601)
        {
            ToSharePlayerTeNum_Player2_2_is_Muteki();
        }
        else if (Int_MyJanken_Te2 == 88)
        {
            ToSharePlayerTeNum_Player2_2_is_Wall();
        }
        else if (Int_MyJanken_Te2 == 46)
        {
            ToSharePlayerTeNum_Player2_2_is_WFlag();
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
        else if (Int_MyJanken_Te3 == 13)
        {
            ToSharePlayerTeNum_Player2_3_is_King();
        }
        else if (Int_MyJanken_Te3 == 23)
        {
            ToSharePlayerTeNum_Player2_3_is_Dorei();
        }
        else if (Int_MyJanken_Te3 == 601)
        {
            ToSharePlayerTeNum_Player2_3_is_Muteki();
        }
        else if (Int_MyJanken_Te3 == 88)
        {
            ToSharePlayerTeNum_Player2_3_is_Wall();
        }
        else if (Int_MyJanken_Te3 == 46)
        {
            ToSharePlayerTeNum_Player2_3_is_WFlag();
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
        else if (Int_MyJanken_Te4 == 13)
        {
            ToSharePlayerTeNum_Player2_4_is_King();
        }
        else if (Int_MyJanken_Te4 == 23)
        {
            ToSharePlayerTeNum_Player2_4_is_Dorei();
        }
        else if (Int_MyJanken_Te4 == 601)
        {
            ToSharePlayerTeNum_Player2_4_is_Muteki();
        }
        else if (Int_MyJanken_Te4 == 88)
        {
            ToSharePlayerTeNum_Player2_4_is_Wall();
        }
        else if (Int_MyJanken_Te4 == 46)
        {
            ToSharePlayerTeNum_Player2_4_is_WFlag();
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
        else if (Int_MyJanken_Te5 == 13)
        {
            ToSharePlayerTeNum_Player2_5_is_King();
        }
        else if (Int_MyJanken_Te5 == 23)
        {
            ToSharePlayerTeNum_Player2_5_is_Dorei();
        }
        else if (Int_MyJanken_Te5 == 601)
        {
            ToSharePlayerTeNum_Player2_5_is_Muteki();
        }
        else if (Int_MyJanken_Te5 == 88)
        {
            ToSharePlayerTeNum_Player2_5_is_Wall();
        }
        else if (Int_MyJanken_Te5 == 46)
        {
            ToSharePlayerTeNum_Player2_5_is_WFlag();
        }
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
        else if (Int_MyJanken_Te1 == 13)
        {
            ToSharePlayerTeNum_Player3_1_is_King();
        }
        else if (Int_MyJanken_Te1 == 23)
        {
            ToSharePlayerTeNum_Player3_1_is_Dorei();
        }
        else if (Int_MyJanken_Te1 == 601)
        {
            ToSharePlayerTeNum_Player3_1_is_Muteki();
        }
        else if (Int_MyJanken_Te1 == 88)
        {
            ToSharePlayerTeNum_Player3_1_is_Wall();
        }
        else if (Int_MyJanken_Te1 == 46)
        {
            ToSharePlayerTeNum_Player3_1_is_WFlag();
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
        else if (Int_MyJanken_Te2 == 13)
        {
            ToSharePlayerTeNum_Player3_2_is_King();
        }
        else if (Int_MyJanken_Te2 == 23)
        {
            ToSharePlayerTeNum_Player3_2_is_Dorei();
        }
        else if (Int_MyJanken_Te2 == 601)
        {
            ToSharePlayerTeNum_Player3_2_is_Muteki();
        }
        else if (Int_MyJanken_Te2 == 88)
        {
            ToSharePlayerTeNum_Player3_2_is_Wall();
        }
        else if (Int_MyJanken_Te2 == 46)
        {
            ToSharePlayerTeNum_Player3_2_is_WFlag();
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
        else if (Int_MyJanken_Te3 == 13)
        {
            ToSharePlayerTeNum_Player3_3_is_King();
        }
        else if (Int_MyJanken_Te3 == 23)
        {
            ToSharePlayerTeNum_Player3_3_is_Dorei();
        }
        else if (Int_MyJanken_Te3 == 601)
        {
            ToSharePlayerTeNum_Player3_3_is_Muteki();
        }
        else if (Int_MyJanken_Te3 == 88)
        {
            ToSharePlayerTeNum_Player3_3_is_Wall();
        }
        else if (Int_MyJanken_Te3 == 46)
        {
            ToSharePlayerTeNum_Player3_3_is_WFlag();
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
        else if (Int_MyJanken_Te4 == 13)
        {
            ToSharePlayerTeNum_Player3_4_is_King();
        }
        else if (Int_MyJanken_Te4 == 23)
        {
            ToSharePlayerTeNum_Player3_4_is_Dorei();
        }
        else if (Int_MyJanken_Te4 == 601)
        {
            ToSharePlayerTeNum_Player3_4_is_Muteki();
        }
        else if (Int_MyJanken_Te4 == 88)
        {
            ToSharePlayerTeNum_Player3_4_is_Wall();
        }
        else if (Int_MyJanken_Te4 == 46)
        {
            ToSharePlayerTeNum_Player3_4_is_WFlag();
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
        else if (Int_MyJanken_Te5 == 13)
        {
            ToSharePlayerTeNum_Player3_5_is_King();
        }
        else if (Int_MyJanken_Te5 == 23)
        {
            ToSharePlayerTeNum_Player3_5_is_Dorei();
        }
        else if (Int_MyJanken_Te5 == 601)
        {
            ToSharePlayerTeNum_Player3_5_is_Muteki();
        }
        else if (Int_MyJanken_Te5 == 88)
        {
            ToSharePlayerTeNum_Player3_5_is_Wall();
        }
        else if (Int_MyJanken_Te5 == 46)
        {
            ToSharePlayerTeNum_Player3_5_is_WFlag();
        }
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
        else if (Int_MyJanken_Te1 == 13)
        {
            ToSharePlayerTeNum_Player4_1_is_King();
        }
        else if (Int_MyJanken_Te1 == 23)
        {
            ToSharePlayerTeNum_Player4_1_is_Dorei();
        }
        else if (Int_MyJanken_Te1 == 601)
        {
            ToSharePlayerTeNum_Player4_1_is_Muteki();
        }
        else if (Int_MyJanken_Te1 == 88)
        {
            ToSharePlayerTeNum_Player4_1_is_Wall();
        }
        else if (Int_MyJanken_Te1 == 46)
        {
            ToSharePlayerTeNum_Player4_1_is_WFlag();
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
        else if (Int_MyJanken_Te2 == 13)
        {
            ToSharePlayerTeNum_Player4_2_is_King();
        }
        else if (Int_MyJanken_Te2 == 23)
        {
            ToSharePlayerTeNum_Player4_2_is_Dorei();
        }
        else if (Int_MyJanken_Te2 == 601)
        {
            ToSharePlayerTeNum_Player4_2_is_Muteki();
        }
        else if (Int_MyJanken_Te2 == 88)
        {
            ToSharePlayerTeNum_Player4_2_is_Wall();
        }
        else if (Int_MyJanken_Te2 == 46)
        {
            ToSharePlayerTeNum_Player4_2_is_WFlag();
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
        else if (Int_MyJanken_Te3 == 13)
        {
            ToSharePlayerTeNum_Player4_3_is_King();
        }
        else if (Int_MyJanken_Te3 == 23)
        {
            ToSharePlayerTeNum_Player4_3_is_Dorei();
        }
        else if (Int_MyJanken_Te3 == 601)
        {
            ToSharePlayerTeNum_Player4_3_is_Muteki();
        }
        else if (Int_MyJanken_Te3 == 88)
        {
            ToSharePlayerTeNum_Player4_3_is_Wall();
        }
        else if (Int_MyJanken_Te3 == 46)
        {
            ToSharePlayerTeNum_Player4_3_is_WFlag();
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
        else if (Int_MyJanken_Te4 == 13)
        {
            ToSharePlayerTeNum_Player4_4_is_King();
        }
        else if (Int_MyJanken_Te4 == 23)
        {
            ToSharePlayerTeNum_Player4_4_is_Dorei();
        }
        else if (Int_MyJanken_Te4 == 601)
        {
            ToSharePlayerTeNum_Player4_4_is_Muteki();
        }
        else if (Int_MyJanken_Te4 == 88)
        {
            ToSharePlayerTeNum_Player4_4_is_Wall();
        }
        else if (Int_MyJanken_Te4 == 46)
        {
            ToSharePlayerTeNum_Player4_4_is_WFlag();
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
        else if (Int_MyJanken_Te5 == 13)
        {
            ToSharePlayerTeNum_Player4_5_is_King();
        }
        else if (Int_MyJanken_Te5 == 23)
        {
            ToSharePlayerTeNum_Player4_5_is_Dorei();
        }
        else if (Int_MyJanken_Te5 == 601)
        {
            ToSharePlayerTeNum_Player4_5_is_Muteki();
        }
        else if (Int_MyJanken_Te5 == 88)
        {
            ToSharePlayerTeNum_Player4_5_is_Wall();
        }
        else if (Int_MyJanken_Te5 == 46)
        {
            ToSharePlayerTeNum_Player4_5_is_WFlag();
        }
    }

    public void SelectGu()     // 【JK-03】手をグーにセット
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "0";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "0";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "0";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "0";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "0";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手をグーにセット");
        PlayerTeNumSet(0);
        //Debug.Log("【JK-04】手をグーにセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectChoki()  // 【JK-03】手をチョキにセット
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "1";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "1";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "1";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "1";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "1";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手をチョキにセット");
        PlayerTeNumSet(1);
        //Debug.Log("【JK-04】手をチョキにセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectPa()     // 【JK-03】手をパーにセット
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "2";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "2";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "2";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "2";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "2";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手をパーにセット");
        PlayerTeNumSet(2);
        //Debug.Log("【JK-04】手をパーにセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectKing()     // 【JK-03】手を王さまにセット 1,2,3,4,5
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_King;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "13";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_King;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "13";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_King;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "13";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_King;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "13";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_King;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "13";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手を王さまにセット");
        PlayerTeNumSet(13);
        //Debug.Log("【JK-04】手を王さまにセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectDorei()     // 【JK-03】手をどれいにセット  1,2,3,4,5
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "23";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "23";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "23";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "23";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "23";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手をどれいにセット");
        PlayerTeNumSet(23);
        //Debug.Log("【JK-04】手をどれいにセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectMuteki()     // 【JK-03】手をむてきにセット  1,2,3,4,5
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "601";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "601";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "601";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "601";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "601";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手をむてきにセット");
        PlayerTeNumSet(601);
        //Debug.Log("【JK-04】手をむてきにセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectWall()     // 【JK-03】手を壁にセット  1,2,3,4,5
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Wall;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "88";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Wall;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "88";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Wall;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "88";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Wall;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "88";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Wall;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "88";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手を壁にセット");
        PlayerTeNumSet(88);
        //Debug.Log("【JK-04】手を壁にセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }

    public void SelectWFlag()     // 【JK-03】手を白旗にセット  1,2,3,4,5
    {
        //Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            MyTeImg_1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_1.text = "46";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            MyTeImg_2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_2.text = "46";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            MyTeImg_3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_3.text = "46";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            MyTeImg_4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_4.text = "46";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            MyTeImg_5.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            MyNumTeText_5.text = "46";
        }
        else
        {
            //Debug.Log("count_a 6以上");
        }
        //Debug.Log("【JK-03】手を白旗にセット");
        PlayerTeNumSet(46);
        //Debug.Log("【JK-04】手を白旗にセットend");

        count_a++;
        //Debug.Log(count_a + ": count_a");
    }


    public void PlayerTeNumSet(int PTN)  // 【JK-04】私のジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）です。それをセットします。
    {
        //Debug.Log("【JK-04】************ ********** *********** **********");
        //Debug.Log(PTN + ": PTN");

        //Debug.Log("【JK-04】現在自分がジャンケンカードボタン押したよ");
        if (Int_MyJanken_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            //Debug.Log("【JK-04】Int_MyJanken_Te1 代入前" + Int_MyJanken_Te1);
            Int_MyJanken_Te1 = PTN;
            //Debug.Log("【JK-04】Int_MyJanken_Te1 代入後" + Int_MyJanken_Te1);
            //Debug.Log("【JK-04】自分_1 手のセットOK");
        }
        else if (Int_MyJanken_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te2 = PTN;
            //Debug.Log("【JK-04】自分_2 手のセットOK");
        }
        else if (Int_MyJanken_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te3 = PTN;
            //Debug.Log("【JK-04】自分_3 手のセットOK");
        }
        else if (Int_MyJanken_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te4 = PTN;
            //Debug.Log("【JK-04】自分_4 手のセットOK");
        }
        else if (Int_MyJanken_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te5 = PTN;
            //Debug.Log("【JK-04】自分_5 手のセットOK");
        }
        else
        {
            //Debug.Log("【JK-04】現在自分の 5こすべて手が決まったよ");
        }
    }

    #endregion

    #region// 【JK-02】ジャンケンカードボタン 押した時の処理（フラグを処理済みにする）
    public void Push_Btn_A() // 【JK-02】ジャンケンカードボタン押したよ
    {
        //Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_A ==0)
        {
            //Debug.Log("【JK-02】RndCreateCard_A ： " + ShuffleCardsMSC.RndCreateCard_A);
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
            else if (ShuffleCardsMSC.RndCreateCard_A == 13) //王さま
            {
                SelectKing();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 23) //どれい
            {
                SelectDorei();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 601) //むてき
            {
                SelectMuteki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 88) //壁
            {
                SelectWall();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 46) //白旗
            {
                SelectWFlag();
            }

            else
            {
                //Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_A.interactable = false;
        CanPushBtn_A = 1;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
        BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
    }

    public void Push_Btn_B() // 【JK-02】ジャンケンカードボタン押したよ
    {
        //Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_B == 0)
        {
            //Debug.Log("【JK-02】RndCreateCard_B ： " + ShuffleCardsMSC.RndCreateCard_B);
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
            else if (ShuffleCardsMSC.RndCreateCard_B == 13) //王さま
            {
                SelectKing();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 23) //どれい
            {
                SelectDorei();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 601) //むてき
            {
                SelectMuteki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 88) //壁
            {
                SelectWall();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 46) //白旗
            {
                SelectWFlag();
            }

            else
            {
                //Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_B.interactable = false;
        CanPushBtn_B = 1;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
        BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
    }

    public void Push_Btn_C() // 【JK-02】ジャンケンカードボタン押したよ
    {
        //Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_C == 0)
        {
            //Debug.Log("【JK-02】RndCreateCard_C ： " + ShuffleCardsMSC.RndCreateCard_C);
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
            else if (ShuffleCardsMSC.RndCreateCard_C == 13) //王さま
            {
                SelectKing();
            }
            else if (ShuffleCardsMSC.RndCreateCard_C == 23) //どれい
            {
                SelectDorei();
            }
            else if (ShuffleCardsMSC.RndCreateCard_C == 601) //むてき
            {
                SelectMuteki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_C == 88) //壁
            {
                SelectWall();
            }
            else if (ShuffleCardsMSC.RndCreateCard_C == 46) //白旗
            {
                SelectWFlag();
            }

            else
            {
                //Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_C.interactable = false;
        CanPushBtn_C = 1;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
        BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
    }

    public void Push_Btn_D() // 【JK-02】ジャンケンカードボタン押したよ
    {
        //Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_D == 0)
        {
            //Debug.Log("【JK-02】RndCreateCard_D ： " + ShuffleCardsMSC.RndCreateCard_D);
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
            else if (ShuffleCardsMSC.RndCreateCard_D == 13) //王さま
            {
                SelectKing();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 23) //どれい
            {
                SelectDorei();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 601) //むてき
            {
                SelectMuteki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 88) //壁
            {
                SelectWall();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 46) //白旗
            {
                SelectWFlag();
            }

            else
            {
                //Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_D.interactable = false;
        CanPushBtn_D = 1;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
        BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
    }

    public void Push_Btn_E() // 【JK-02】ジャンケンカードボタン押したよ
    {
        //Debug.Log("【JK-02】ジャンケンカードを1枚 押下しました");
        if (CanPushBtn_E == 0)
        {
            //Debug.Log("【JK-02】RndCreateCard_E ： " + ShuffleCardsMSC.RndCreateCard_E);
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
            else if (ShuffleCardsMSC.RndCreateCard_E == 13) //王さま
            {
                SelectKing();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 23) //どれい
            {
                SelectDorei();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 601) //むてき
            {
                SelectMuteki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 88) //壁
            {
                SelectWall();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 46) //白旗
            {
                SelectWFlag();
            }

            else
            {
                //Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_E.interactable = false;
        CanPushBtn_E = 1;
        Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
        BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
    }

    public void Push_StockCard_Up() // StockCard_Up ボタン押したよ
    {
        //Debug.Log("StockCard_Up ボタン を1枚 押下しました");
        if (ShuffleCardsMSC.int_StockCard_Up >= 0)   // アイテムがジャンケンカードならば
        {
            if (ShuffleCardsMSC.MyJankenPanel.activeSelf)   // Myジャンケンパネルが開かれていたら
            {
                if ((CanPushBtn_A + CanPushBtn_B + CanPushBtn_C + CanPushBtn_D + CanPushBtn_E + CanPushBtn_StockCard_Up + CanPushBtn_StockCard_Down) < 5)  // 合計5枚分のじゃんけんカードをまだ押していないならば
                {
                    if (CanPushBtn_StockCard_Up == 0)
                    {
                        //Debug.Log("int_StockCard_Up ： " + ShuffleCardsMSC.int_StockCard_Up);
                        if (ShuffleCardsMSC.int_StockCard_Up == 0) //グー
                        {
                            SelectGu();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 1) //チョキ
                        {
                            SelectChoki();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 2) //パー
                        {
                            SelectPa();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 13) //王さま
                        {
                            SelectKing();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 23) //どれい
                        {
                            SelectDorei();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 601) //むてき
                        {
                            SelectMuteki();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 88) //壁
                        {
                            SelectWall();
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Up == 46) //白旗
                        {
                            SelectWFlag();
                        }

                        else
                        {
                            //Debug.Log("ランダム値の見直しが必要！！");
                        }
                    }
                    Btn_StockCard_Up.interactable = false;
                    CanPushBtn_StockCard_Up = 1;
                    Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
                    BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
                }
            }
        }
        else
        {
            //Debug.Log("ジャンケンカード以外のアイテムです");
        }
    }

    public void Push_StockCard_Down() // StockCard_Down ボタン押したよ
    {
        //Debug.Log("StockCard_Down ボタン を1枚 押下しました");
        if (ShuffleCardsMSC.int_StockCard_Down >= 0)   // ジャンケンカード以外のアイテムならば
        {
            if (ShuffleCardsMSC.MyJankenPanel.activeSelf)   // Myジャンケンパネルが開かれていたら
        {
            if ((CanPushBtn_A + CanPushBtn_B + CanPushBtn_C + CanPushBtn_D + CanPushBtn_E + CanPushBtn_StockCard_Up + CanPushBtn_StockCard_Down) < 5)  // 合計5枚分のじゃんけんカードをまだ押していないならば
            {
                if (CanPushBtn_StockCard_Down == 0)
                {
                    //Debug.Log("int_StockCard_Down ： " + ShuffleCardsMSC.int_StockCard_Down);
                    if (ShuffleCardsMSC.int_StockCard_Down == 0) //グー
                    {
                        SelectGu();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 1) //チョキ
                    {
                        SelectChoki();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 2) //パー
                    {
                        SelectPa();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 13) //王さま
                    {
                        SelectKing();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 23) //どれい
                    {
                        SelectDorei();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 601) //むてき
                    {
                        SelectMuteki();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 88) //壁
                    {
                        SelectWall();
                    }
                    else if (ShuffleCardsMSC.int_StockCard_Down == 46) //白旗
                    {
                        SelectWFlag();
                    }

                    else
                    {
                        //Debug.Log("ランダム値の見直しが必要！！");
                    }
                }
                Btn_StockCard_Down.interactable = false;
                CanPushBtn_StockCard_Down = 1;
                Check_CanAppear_KetteiBtn();  // ジャンケン手「決定ボタン」を表示できるか確認
                BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
            }
        }
        }
        else
        {
            //Debug.Log("ジャンケンカード以外のアイテムです");
        }
    }

    public void PushBtn_Omakase() // 【JK-02】おまかせボタン押したよ
    {
        //Debug.Log("【JK-02】おまかせボタン押したよ");
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
        BGM_SE_MSC.Card_Mekuri_SE();         // じゃんけんカードめくる音
    }

    #endregion


    #region// 【JK-02】ジャンケンカードボタン を押せるようにする(フラグのリセット）

    public void ToCanPush_All()   //【JK-02】 【JK-31】じゃんけんカードボタン を押せるようにする(フラグのリセット）（bool）
    {
        //Debug.Log("【JK-02】【JK-31】すべてのジャンケンカードボタン を押せるようにします(フラグのリセット）");
        ToCanPush_A();
        ToCanPush_B();
        ToCanPush_C();
        ToCanPush_D();
        ToCanPush_E();
        ToCanPushBtn_StockCard_Up();
        ToCanPushBtn_StockCard_Down();
        ToCanPush_Omakase();
    }

    public void ToCanPush_A() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_A.interactable = true;
        CanPushBtn_A = 0;
    }

    public void ToCanPush_B() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_B.interactable = true;
        CanPushBtn_B = 0;
    }

    public void ToCanPush_C() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_C.interactable = true;
        CanPushBtn_C = 0;
    }

    public void ToCanPush_D() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_D.interactable = true;
        CanPushBtn_D = 0;
    }

    public void ToCanPush_E() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_E.interactable = true;
        CanPushBtn_E = 0;
    }

    public void ToCanPushBtn_StockCard_Up() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_StockCard_Up.interactable = true;
        CanPushBtn_StockCard_Up = 0;
    }

    public void ToCanPushBtn_StockCard_Down() // 【JK-02】ジャンケンカードボタン押せるようにするよ
    {
        Btn_StockCard_Down.interactable = true;
        CanPushBtn_StockCard_Down = 0;
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

        //Debug.Log("【JK-04】【JK-29】自分のジャンケン手をリセットしました");
    }
    #endregion


    public void FromWin_ToJump()     //【JK-106】ジャンケンに勝ったのでジャンプで移動する その一連の処理
    {
        if (Flg_FromWin_ToJumpDone == 0)
        {
            Flg_FromWin_ToJumpDone = 1;
            //Debug.Log("ジャンケン勝者の位置に応じて、SubCamera を移動します");
            SetPosX_SubCamera_AccordingTo_Winner();  // ジャンケン勝者の位置に応じて、SubCamera を移動する

            //Debug.Log("【JK-107】FromWin_ToJump （ジャンプ移動）処理に入ります");
            Set_StepNum();               //【JK-108】ジャンプする回数を設定する（変数上書き） 
            bridge_JumpToRight();        //【JK-109】右方向へ 指定された回数 ぴょん と跳ねながら移動する
        }
        else
        {
            Debug.LogError("FromWin_ToJumpDone 処理 ダブってます！！！");
            Error06_Text.text = "FromWin_ToJumpDone処理ダブり";
        }
    }

    public void Set_StepNum()        //【JK-108】ジャンプする回数を設定する（変数上書き） 
    {
        //Debug.Log("【JK-108】ジャンプする回数を設定（変数上書き）します");
        //Debug.Log("PlayerSC.MoveForward_StepNum :" + PlayerSC.MoveForward_StepNum);
        //Debug.Log("original_StepNum :" + original_StepNum);
        PlayerSC.MoveForward_StepNum = original_StepNum;   // ジャンプして移動するステップ数（の元となる変数）に上書きする
        //Debug.Log("PlayerSC.MoveForward_StepNum :" + PlayerSC.MoveForward_StepNum);
    }

    public void bridge_JumpToRight()  //【JK-109】右方向へ 指定された回数 ぴょん と跳ねながら移動する
    {
        //Debug.Log("【JK-109】bridge_JumpToRight（ジャンプ移動） を実行します");
        //Debug.Log("【JK-109】処理前に " + JumpMaeTaiki + "秒 待機します");
        //Debug.Log("【JK-109】JumpMaeTaiki : " + JumpMaeTaiki);

        var sequence = DOTween.Sequence();
        sequence.InsertCallback(JumpMaeTaiki, () => PlayerSC.JumpRight());
    }

    public void Check_KageDistance()               //  MyKage と MyPlayer の距離を求める（Y軸の初期位置）
    {
        KageDistance = myPlayer.transform.position.y - MyKage.transform.position.y;
    }

    public void MoveTo_MyKagePos()                 //  MyKage の位置へ移動する（Y軸位置微調整）
    {
        myPlayer.transform.position = new Vector3(myPlayer.transform.position.x, MyKage.transform.position.y + KageDistance, myPlayer.transform.position.z);
    }

    public void bridge_GetDamage()
    {
        //Debug.Log("bridge_GetDamage を実行します");
        PlayerSC.receivedDammage();
        BGM_SE_MSC.korede_iikana_SE();         // これでいいかな？ SEを流す

    }

    #region // サブカメラの処理一連
    public void SetPosX_SubCamera_AccordingTo_Winner()  // ジャンケン勝者の位置に応じて、SubCamera を移動する
    {
        //Debug.Log("PosX_Winner : " + PosX_Winner);
        PosX_Winner = MyKage.transform.position.x;
        //Debug.Log("PosX_Winner : " + PosX_Winner);
        share_SubCamera_Position();                // サブカメラの位置を移動して共有する ＆＆ サブカメラの表示ON

        var sequence = DOTween.Sequence();
        sequence.InsertCallback(JumpMaeTaiki, () => ShareSubCamera_GoRight_byStepNum());
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

    [PunRPC]
    public void MoveTo_cafe_kanban_035()   // SubCamera を 035 の位置に移動する  -28.4
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_035.transform.position.x, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_025()   // SubCamera を 025 の位置に移動する
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_025.transform.position.x, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_015()   // SubCamera を 015 の位置に移動する  -8.4
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_015.transform.position.x, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_005()   // SubCamera を 005 の位置に移動する  1.43
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_005.transform.position.x, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void MoveTo_cafe_kanban_0_5()   // SubCamera を -5 の位置に移動する   10.5
    {
        SubCamera.transform.position = new Vector3(cafe_kanban_0_5.transform.position.x, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void AppearSubCamera_Group()
    {
        //Debug.Log("SubCamera サブカメラ 表示します");
        SubCamera_Group.SetActive(true);
        SubCamera.SetActive(true);
        ToLeft_SubCamera = false;
        ToRight_SubCamera = false;
    }

    public void CloseSubCamera_Group()
    {
        //Debug.Log("SubCamera サブカメラ 非表示");
        SubCamera_Group.SetActive(false);
        SubCamera.SetActive(false);
        ToLeft_SubCamera = false;
        ToRight_SubCamera = false;
    }

    public void On_Off_SubCamera_Group()
    {
        ToLeft_SubCamera = false;
        ToRight_SubCamera = false;
        if (SubCamera_Group.activeSelf) // サブカメラ ON だったら
        {
            CloseSubCamera_Group();
        }
        else                           // サブカメラ OFF だったら
        {
            AppearSubCamera_Group();
        }
    }

    public void Right_PushDown()          //      右ボタンを押している間
    {
        ToRight_SubCamera = true;
        ToLeft_SubCamera = false;
    }

    public void Right_PushUp()            //      右ボタンを押すのをやめた時
    {
        ToRight_SubCamera = false;
        ToLeft_SubCamera = false;
    }

    public void Left_PushDown()         //      左ボタンを押している間

    {
        ToLeft_SubCamera = true;
        ToRight_SubCamera = false;
    }

    public void Left_PushUp()          //      左ボタンを押すのをやめた時
    {
        ToRight_SubCamera = false;
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

    [PunRPC]
    public void SubCamera_GoRight_4m()
    {
        SubCamera.transform.DOMove(new Vector3(4, 0, 0), 4).SetRelative(true); //現在の位置から（4,0,0）だけ移動
    }

    [PunRPC]
    public void SubCamera_GoRight_10m_First()
    {
        SubCamera.transform.DOMove(new Vector3(10, 0, 0), 2).SetRelative(true); //現在の位置から（10,0,0）だけ移動
    }

    public void ShareSubCamera_GoRight_10m_slow()
    {
        photonView.RPC("SubCamera_GoRight_10m_slow", RpcTarget.All);  // ゆっくり10ｍ右に移動させる
    }

    [PunRPC]
    public void SubCamera_GoRight_10m_slow()
    {
        SubCamera.transform.DOMove(new Vector3(10, 0, 0), 10).SetRelative(true); //現在の位置から（10,0,0）だけ移動
    }

    public void ShareSubCamera_GoRight_byStepNum() // original_StepNum の数だけ、サブカメラを右に移動させる
    {
        photonView.RPC("SubCamera_GoRight_byStepNum", RpcTarget.All);  // original_StepNum の数だけ右に移動させる
    }

    [PunRPC]
    public void SubCamera_GoRight_byStepNum()
    {
        SubCamera.transform.DOMove(new Vector3(original_StepNum, 0, 0), original_StepNum).SetRelative(true); //現在の位置から original_StepNum分 だけ移動
    }

    public void share_SubCamera_Position()                // サブカメラの位置を移動して共有する ＆＆ サブカメラの表示ON
    {
        //Debug.Log("サブカメラの位置を移動して共有する");

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("SubCamera_MoveTo_PosX_Player1", RpcTarget.All);  // サブカメラの位置を移動して共有する
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("SubCamera_MoveTo_PosX_Player2", RpcTarget.All);  // サブカメラの位置を移動して共有する
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("SubCamera_MoveTo_PosX_Player3", RpcTarget.All);  // サブカメラの位置を移動して共有する
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("SubCamera_MoveTo_PosX_Player4", RpcTarget.All);  // サブカメラの位置を移動して共有する
        }

        photonView.RPC("AppearSubCamera_Group", RpcTarget.All);  // 全員にサブカメラを一斉に開かせる
    }


    [PunRPC]
    public void SubCamera_MoveTo_PosX_Player1()   // SubCamera を PosX_Player1 の位置に移動する
    {
        SubCamera.transform.position = new Vector3(PosX_Player1, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void SubCamera_MoveTo_PosX_Player2()   // SubCamera を PosX_Player2 の位置に移動する
    {
        SubCamera.transform.position = new Vector3(PosX_Player2, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void SubCamera_MoveTo_PosX_Player3()   // SubCamera を PosX_Player3 の位置に移動する
    {
        SubCamera.transform.position = new Vector3(PosX_Player3, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void SubCamera_MoveTo_PosX_Player4()   // SubCamera を PosX_Player4 の位置に移動する
    {
        SubCamera.transform.position = new Vector3(PosX_Player4, cafe_kanban_035.transform.position.y, SubCamera.transform.position.z);
    }

    [PunRPC]
    public void CloseSubCamera_Group_AfterWait1sec()
    {
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(1.0f, () => CloseSubCamera_Group());  // サブカメラを閉じる
    }

    #endregion

    #region // PosXの同期

    public float PosX_P1
    {
        get { return PosX_Player1; }
        set { PosX_Player1 = value; RequestOwner(); }
    }

    public float PosX_P2
    {
        get { return PosX_Player2; }
        set { PosX_Player2 = value; RequestOwner(); }
    }

    public float PosX_P3
    {
        get { return PosX_Player3; }
        set { PosX_Player3 = value; RequestOwner(); }
    }

    public float PosX_P4
    {
        get { return PosX_Player4; }
        set { PosX_Player4 = value; RequestOwner(); }
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

    [PunRPC]
    public void Update_PosX_Players()   // 各プレイヤーのX軸位置を同期します
    {
        Flg_Update_PosX = true;
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("PosX_Koshin_Player1", RpcTarget.All);
            PosX_Player1 = myPlayer.transform.position.x - StartMark1.transform.position.x;
            realPosX_Player1 = myPlayer.transform.position.x;
            PosX_MyPlayer = PosX_Player1;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("PosX_Koshin_Player2", RpcTarget.All);
            PosX_Player2 = myPlayer.transform.position.x - StartMark2.transform.position.x;
            realPosX_Player2 = myPlayer.transform.position.x;
            PosX_MyPlayer = PosX_Player2;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("PosX_Koshin_Player3", RpcTarget.All);
            PosX_Player3 = myPlayer.transform.position.x - StartMark3.transform.position.x;
            realPosX_Player3 = myPlayer.transform.position.x;
            PosX_MyPlayer = PosX_Player3;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("PosX_Koshin_Player4", RpcTarget.All);
            PosX_Player4 = myPlayer.transform.position.x - StartMark4.transform.position.x;
            realPosX_Player4 = myPlayer.transform.position.x;
            PosX_MyPlayer = PosX_Player4;
        }

        checkPosX_Koshin_Atai();        // PosXの数値はいくつ？
    }

    public void WhoIsTopPlayer()        // 各プレイヤーのX軸位置を比較し、現在の首位と、自分との距離を算出する
    {
        PosX_TopPlayer = Mathf.Max(PosX_Player1, PosX_Player2, PosX_Player3, PosX_Player4);
        X_dis_betweenTop = PosX_TopPlayer - PosX_MyPlayer;
    }

    public void WhoIsBottomPlayer()     // 各プレイヤーのX軸位置を比較し、最下位の位置を特定する
    {
        PosX_BottomPlayer = Mathf.Min(PosX_Player1, PosX_Player2, PosX_Player3, PosX_Player4);
    }

    [PunRPC]
    public void PosX_Koshin_Player1()   // 誰のX軸座標を更新するの？
    {
        PosX_Koshin_PlayerNum = 1;
    }

    [PunRPC]
    public void PosX_Koshin_Player2()   // 誰のX軸座標を更新するの？
    {
        PosX_Koshin_PlayerNum = 2;
    }

    [PunRPC]
    public void PosX_Koshin_Player3()   // 誰のX軸座標を更新するの？
    {
        PosX_Koshin_PlayerNum = 3;
    }

    [PunRPC]
    public void PosX_Koshin_Player4()   // 誰のX軸座標を更新するの？
    {
        PosX_Koshin_PlayerNum = 4;
    }

    public static int GetPointDigit(int PosX_MyPlayer_Moto, int Shutoku_Keta)   // 数値の中から指定した桁の値を取り出す
    {
        return (int)(PosX_MyPlayer_Moto / Mathf.Pow(10, Shutoku_Keta - 1)) % 10;
    }

    public void checkPosX_Koshin_Atai()  // PosXの数値はいくつ？
    {
        int PosX_MyPlayer_Moto = Mathf.FloorToInt(PosX_MyPlayer);
        PosX_Koshin_Atai_10keta = GetPointDigit(PosX_MyPlayer_Moto, 2);  // 十桁の値
        PosX_Koshin_Atai_1keta = GetPointDigit(PosX_MyPlayer_Moto, 1);   // 一桁の値
        checkPosX_Koshin_Atai_10keta();
        checkPosX_Koshin_Atai_1keta();
        photonView.RPC("totalPosX_Koshin_Atai", RpcTarget.All);
    }

    [PunRPC]
    public void totalPosX_Koshin_Atai()
    {
        PosX_Koshin_Atai = PosX_Koshin_Atai_10keta * 10 + PosX_Koshin_Atai_1keta;

        switch (PosX_Koshin_PlayerNum)
        {
            case 1: //
                PosX_Player1 = PosX_Koshin_Atai;
                break;
            case 2: //
                PosX_Player2 = PosX_Koshin_Atai;
                break;
            case 3: //
                PosX_Player3 = PosX_Koshin_Atai;
                break;
            case 4: //
                PosX_Player4 = PosX_Koshin_Atai;
                break;
            default:
                // その他処理
                break;
        }
    }


    public void checkPosX_Koshin_Atai_10keta()
    {

        switch (PosX_Koshin_Atai_10keta)
        {
            case 1: //
                photonView.RPC("PosX_Koshin_Atai_10keta_1", RpcTarget.All);
                break;
            case 2: //
                photonView.RPC("PosX_Koshin_Atai_10keta_2", RpcTarget.All);
                break;
            case 3: //
                photonView.RPC("PosX_Koshin_Atai_10keta_3", RpcTarget.All);
                break;
            case 4: //
                photonView.RPC("PosX_Koshin_Atai_10keta_4", RpcTarget.All);
                break;
            case 5: //
                photonView.RPC("PosX_Koshin_Atai_10keta_5", RpcTarget.All);
                break;
            case 6: //
                photonView.RPC("PosX_Koshin_Atai_10keta_6", RpcTarget.All);
                break;
            case 7: //
                photonView.RPC("PosX_Koshin_Atai_10keta_7", RpcTarget.All);
                break;
            case 8: //
                photonView.RPC("PosX_Koshin_Atai_10keta_8", RpcTarget.All);
                break;
            case 9: //
                photonView.RPC("PosX_Koshin_Atai_10keta_9", RpcTarget.All);
                break;
            case 0: //
                photonView.RPC("PosX_Koshin_Atai_10keta_0", RpcTarget.All);
                break;
            default:
                // その他処理
                break;
        }
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_1()
    {
        PosX_Koshin_Atai_10keta = 1;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_2()
    {
        PosX_Koshin_Atai_10keta = 2;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_3()
    {
        PosX_Koshin_Atai_10keta = 3;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_4()
    {
        PosX_Koshin_Atai_10keta = 4;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_5()
    {
        PosX_Koshin_Atai_10keta = 5;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_6()
    {
        PosX_Koshin_Atai_10keta = 6;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_7()
    {
        PosX_Koshin_Atai_10keta = 7;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_8()
    {
        PosX_Koshin_Atai_10keta = 8;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_9()
    {
        PosX_Koshin_Atai_10keta = 9;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_10keta_0()
    {
        PosX_Koshin_Atai_10keta = 0;
    }

    public void checkPosX_Koshin_Atai_1keta()
    {

        switch (PosX_Koshin_Atai_1keta)
        {
            case 1: //
                photonView.RPC("PosX_Koshin_Atai_1keta_1", RpcTarget.All);
                break;
            case 2: //
                photonView.RPC("PosX_Koshin_Atai_1keta_2", RpcTarget.All);
                break;
            case 3: //
                photonView.RPC("PosX_Koshin_Atai_1keta_3", RpcTarget.All);
                break;
            case 4: //
                photonView.RPC("PosX_Koshin_Atai_1keta_4", RpcTarget.All);
                break;
            case 5: //
                photonView.RPC("PosX_Koshin_Atai_1keta_5", RpcTarget.All);
                break;
            case 6: //
                photonView.RPC("PosX_Koshin_Atai_1keta_6", RpcTarget.All);
                break;
            case 7: //
                photonView.RPC("PosX_Koshin_Atai_1keta_7", RpcTarget.All);
                break;
            case 8: //
                photonView.RPC("PosX_Koshin_Atai_1keta_8", RpcTarget.All);
                break;
            case 9: //
                photonView.RPC("PosX_Koshin_Atai_1keta_9", RpcTarget.All);
                break;
            case 0: //
                photonView.RPC("PosX_Koshin_Atai_1keta_0", RpcTarget.All);
                break;
            default:
                // その他処理
                break;
        }
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_1()
    {
        PosX_Koshin_Atai_1keta = 1;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_2()
    {
        PosX_Koshin_Atai_1keta = 2;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_3()
    {
        PosX_Koshin_Atai_1keta = 3;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_4()
    {
        PosX_Koshin_Atai_1keta = 4;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_5()
    {
        PosX_Koshin_Atai_1keta = 5;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_6()
    {
        PosX_Koshin_Atai_1keta = 6;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_7()
    {
        PosX_Koshin_Atai_1keta = 7;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_8()
    {
        PosX_Koshin_Atai_1keta = 8;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_9()
    {
        PosX_Koshin_Atai_1keta = 9;
    }

    [PunRPC]
    public void PosX_Koshin_Atai_1keta_0()
    {
        PosX_Koshin_Atai_1keta = 0;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        if (stream.IsWriting)        //データの送信
        {

        }
        else                       //データの受信
        {

        }
        */
    }

    #endregion


    public void BackTo_TitleScene() // タイトル画面へ戻ります
    {
        BGM_SE_MSC.firstRead_Selectjanken = 0;
        BGM_SE_MSC.firstRead_TestRoomController = 0;
        Logout_InTheMiddle();       // 途中退席した人の処理
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Launcher");
    }

    public void BackTo_LobbyScene() // ロビー画面へ戻ります
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }



    #region// 大砲関連

    public void CheckCanUseTaihou()     // 人間大砲が撃てるか確認します
    {
        //Debug.Log("CheckCanUseTaihou 人間大砲が撃てるか確認します");
        //Debug.Log("X_dis_betweenTop : " + X_dis_betweenTop);
        //Debug.Log("(X_dis30po/2)-1 : " + ((X_dis30po / 2) - 1));

        if (X_dis_betweenTop >= ((X_dis30po / 2) - 1))      // 首位との差が 14 以上開いていたら
        {
            Flg_CanUseTaihou = true;
            Button_TaihouFire.SetActive(true);
        }
        else
        {
            Flg_CanUseTaihou = false;
            Button_TaihouFire.SetActive(false);
        }
    }

    public void CanUse_Taihou()
    {
        Flg_CanUseTaihou = true;
        Button_TaihouFire.SetActive(true);
    }

    public void CannotUse_Taihou()
    {
        Flg_CanUseTaihou = false;
        Button_TaihouFire.SetActive(false);
    }

    public void ShareTaihouFireStream()
    {
        WhoIsTaihouFlyer();                     // 大砲で飛ぶのは誰？
        Taihou.transform.position = myPlayer.transform.position;  // 大砲本体の位置をMyキャラの位置に移動する
        Flg_CanUseTaihou = false;
        Button_TaihouFire.SetActive(false);
        myPlayer.SetActive(false);              // 自分のプレイヤーキャラを一時的に非表示にする
        Countdown_timer_PanelOpen = -1;         // ボタンを おしてね のカウントダウンを止める
        share_SubCamera_Position();             // サブカメラの位置を移動して共有する ＆＆ サブカメラの表示ON
        photonView.RPC("TaihouFire", RpcTarget.All);

        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3f, () => bridge_Fly_byTaihou());
    }

    public void bridge_Fly_byTaihou()   // 大砲によってキャラが飛ぶ
    {
        myPlayer.SetActive(true);       // 自分のプレイヤーキャラを再表示する
        PlayerSC.Fly_byTaihou();        // 大砲によってキャラが飛ぶ
    }

    [PunRPC]
    public void TaihouFire()
    {
        Taihou.SetActive(true);          // 大砲本体を出現させる     
        BGM_SE_MSC.TaihouFire_SE();      // 大砲のカウントダウン開始
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3f, () => Taihou_Bakuhatsu_Play());
    }

    public void Taihou_Bakuhatsu_Play()   // 人間大砲の爆発エフェクトを再生 ＆＆ 画面揺れ ＆＆ 爆発音
    {
        Taihou_Bakuhatsu.Play();  // 爆発エフェクト
        if (Flg_Taihou_punch)
        {
            var vecPunch = new Vector3(0.05f, 0.05f, 0.05f);
            SubCamera_Group.transform.DOPunchScale(vecPunch, duration: 2, vibrato: int_vib, flo_ran);
        }
        else
        {
            var vecPunch = new Vector3(0.01f, 0.02f, 0.01f);
            SubCamera_Group.transform.DOPunchScale(vecPunch, duration: 2, vibrato: int_vib, flo_ran);
        }
        photonView.RPC("SubCamera_GoRight_10m_First", RpcTarget.All);  // サブカメラを素早く10ｍ右に移動させる
    }

    public void AfterFly_byTaihou()       // 人間大砲ヲ撃って、着地した後の処理
    {
        MoveTo_MyKagePos();   // MyKage の位置へ移動する（Y軸位置微調整）
        Judge_GOAL();         // ゴールラインに到達したか判定する
        ResetCountdown_timer_PanelOpen_1();             // ボタンを おしてね のカウントダウンを再開する
        Countdown_Until_Push_OpenMyJankenPanel_Button();   // ジャンケンパネルが開かれていないならば、カウントダウン開始
        photonView.RPC("CloseSubCamera_Group_AfterWait1sec", RpcTarget.All);   // 全員一斉にサブカメラを閉じる

        PrecheckTaiho_PosX();  // PosX の値を共有し、大砲が撃てるか確認する
    }


    public void WhoIsTaihouFlyer()                // 大砲で飛んでいるのは誰？
    {
        //Debug.Log("大砲で飛んでいるのは誰？");
        WinnerNum = -1;                      // 一旦リセット

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            WinnerNum = 1;
            photonView.RPC("ShareWinnerName_P1", RpcTarget.All);
            ShareJKAvator_Kachi_Player1();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            WinnerNum = 2;
            photonView.RPC("ShareWinnerName_P2", RpcTarget.All);
            ShareJKAvator_Kachi_Player2();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            WinnerNum = 3;
            photonView.RPC("ShareWinnerName_P3", RpcTarget.All);
            ShareJKAvator_Kachi_Player3();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            WinnerNum = 4;
            photonView.RPC("ShareWinnerName_P4", RpcTarget.All);
            ShareJKAvator_Kachi_Player4();  // ジャンケン手、下アバターを勝ちにし、それを全プレイヤーで共有する
        }

        SetMyAvator_ForChamp();         // チャンプ のアバターを ゴールパネル（表彰台）にセットします。  
    }

#endregion


    [PunRPC]
    public void ShareWinnerName_P1()
    {
        Text_WinnerName.text = TestRoomControllerSC.string_PName1;
    }

    [PunRPC]
    public void ShareWinnerName_P2()
    {
        Text_WinnerName.text = TestRoomControllerSC.string_PName2;
    }

    [PunRPC]
    public void ShareWinnerName_P3()
    {
        Text_WinnerName.text = TestRoomControllerSC.string_PName3;
    }

    [PunRPC]
    public void ShareWinnerName_P4()
    {
        Text_WinnerName.text = TestRoomControllerSC.string_PName4;
    }

    public void AppearPanel_SyokaiTaihou()
    {
        Panel_SyokaiTaihou.SetActive(true);
    }

    public void ClosePanel_SyokaiTaihou()
    {
        Panel_SyokaiTaihou.SetActive(false);
    }


    public void WhoAreYou()    // 私の名前（真名）を表示
    {
        //Debug.Log("私の名前(真名)は・・・");
        //Debug.Log("PhotonNetwork.NickName ランチャー：" + PhotonNetwork.NickName);
    }

    #region// じゃんけんカードの再配布
    public void Redistribute_JankenCards()    // じゃんけんカードの再配布を実施します
    {
        ResetMyNumTe_All();               // 【JK-29】MyNumTe 数値を -1 にリセット（int,text）
        Reset_MyRireki_All();             // 【JK-30】MyRireki イメージを null にリセット（Image）
        ToCanPush_All();                  // 【JK-31】じゃんけんカードボタン を押せるようにする(フラグのリセット）（bool）
        ShuffleCardsMSC.Reset_All();      // 【JK-34】じゃんけんカード 手のリセット
        ShuffleCardsMSC.Set_All();        // 【JK-35】じゃんけんカード 手のセット
    }

    public void PushRedistributeButton()     // じゃんけんカードの再配布ボタンを押下
    {
        if (Gold_MyPlayer >= 10)
        {
            Set_m10_calculation_Gold();  // ゴールド -10
            calculate_Gold_Players();    // 所持金（ゴールド）をマイナス/プラスします
            ResetCountdown_timer_Kettei_1();  // 自動カウントダウンを一時停止
            Redistribute_JankenCards();    // じゃんけんカードの再配布を実施します
            ShuffleCardsMSC.Distribute_JankenCards();         // じゃんけんカードの配布（一旦非表示にしてから、順番に表示していく）
        }
        else  // ゴールドが足りないよ
        {
            // ぽわわ～ん。。。
            BGM_SE_MSC.gold_fusoku_SE();
            Text_Gold_fusoku.text = "ゴールドが 不足しています...";
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(3f, () => Erase_Text_Gold_fusoku());
        }
    }
    #endregion

    #region// 忍者（相手の手札を見る）
    public void PushNinjaButton()  // Myジャンケンパネルを一旦閉じて、現時点で決まっている手札を見る。5秒後、再度Myジャンケンパネルを表示させる
    {
        if (Gold_MyPlayer >= 10)
        {
            Set_m10_calculation_Gold();  // ゴールド -10
            calculate_Gold_Players();    // 所持金（ゴールド）をマイナス/プラスします
            AppearSyoji_Panel();
            ResetCountdown_timer_Kettei_1();
            bridgeCloseMyJankenPanel();
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(5f, () => bridgeAppearMyJankenPanel());
        }
        else  // ゴールドが足りないよ
        {
            // ぽわわ～ん。。。
            BGM_SE_MSC.gold_fusoku_SE();
            Text_Gold_fusoku.text = "ゴールドが 不足しています...";
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(3f, () => Erase_Text_Gold_fusoku());
        }
    }

    public void Erase_Text_Gold_fusoku()
    {
        Text_Gold_fusoku.text = "";
    }

    public void bridgeAppearMyJankenPanel()
    {
        ShuffleCardsMSC.AppearMyJankenPanel();
        CloseSyoji_Panel();
    }

    public void bridgeCloseMyJankenPanel()
    {
        ShuffleCardsMSC.CloseMyJankenPanel();
    }

    public void AppearSyoji_Panel()
    {
        Syoji_Panel.SetActive(true);
    }

    public void CloseSyoji_Panel()
    {
        Syoji_Panel.SetActive(false);
    }

    #endregion

    #region// 金たらい
    public void FallTarai_stream()
    {
        if (Tarai_to_SetWFlag)    // たらいが落ちると、確定で白旗一枚
        {
            Tarai_to_SetWFlag = false;
            Vector3 PosMyHead = new Vector3(PlayerSC.MyTenjo.transform.position.x, PlayerSC.MyHead.transform.position.y, PlayerSC.MyTenjo.transform.position.z);

            Update_PosX_Players();                                  // 各プレイヤーのX軸位置を同期します
            Tarai.transform.DORotate(new Vector3(0f, 0f, 0), 0f);   // たらいを上向きにセットする
            share_tarai_Position();                                 // たらいの位置を移動して共有する（MyTenjo）
            photonView.RPC("FallTarai", RpcTarget.All);             // たらいを表示 → 4秒後に非表示
            if (Sum_AnzenYoshi_Card <= 0)                           // 安全ヨシカードが1枚も無かったら
            {
                Tarai.transform
                    .DOMove(PosMyHead, 0.5f)                            // たらいを下（キャラの頭の位置）に移動する（落下させる） (PosMyHead)
                                                                        //.SetRelative()     // 今いる位置を基準にする  
                    .OnComplete(() =>                                   // ジャンプが終了したら、以下の操作をする
                    {
                    //Tarai.transform.DORotate(new Vector3(0f, 0f,180), 0.5f);
                    Tarai_Guwan.Play();                             // tarai当たった時のエフェクト
                    BGM_SE_MSC.Tarai_Guwan_SE();
                        int minus_Life = UnityEngine.Random.Range(1, 11);
                        if (minus_Life <= 7)
                        {
                            Set_minus_01_calculation_Life();                // Life -1
                        }
                        else
                        {
                            Set_minus_02_calculation_Life();                // Life -2
                        }
                        calculate_Life_Players();                       // 現在の体力 にマイナス/プラスします
                        ResetCountdown_timer_Kettei_1();                // 自動カウントダウンを一時停止
                        Update_Life_Players();                          // 各プレイヤーのLifeを同期します
                        PlayerSC.anim.SetBool("damage", true);
                        var sequence3 = DOTween.Sequence();
                        sequence3.InsertCallback(1.5f, () => Tarai_Fukki());

                        //Debug.Log("たらいを回転させる");
                        Tarai.transform.DORotate(new Vector3(0f, 0f, 180), 0.3f);
                        var sequence = DOTween.Sequence();
                        sequence.InsertCallback(0.3f, () => FallTarai_After03());
                        Tarai.transform.DOJump(new Vector3(PlayerSC.ToTarai.transform.position.x, PlayerSC.ToTarai.transform.position.y, PlayerSC.ToTarai.transform.position.z), 0.5f, 1, 0.3f);
                    });
            }
            else   // 安全ヨシカードが1枚以上有れば
            {
                Tarai.transform  
                    .DOMove(PosMyHead + new Vector3(0f, 0.2f, 10), 0.5f)                            // たらいを下（キャラの頭の位置）に移動する（落下させる） (PosMyHead)
                                                        //.SetRelative()     // 今いる位置を基準にする  
                    .OnComplete(() =>                                   // ジャンプが終了したら、以下の操作をする
                    {
                        //Tarai.transform.DORotate(new Vector3(0f, 0f,180), 0.5f);
                        AppearBarrier();
                        Tarai_Guwan.Play();                             // tarai当たった時のエフェクト
                        BGM_SE_MSC.barrier_SE();
                        ResetCountdown_timer_Kettei_1();                // 自動カウントダウンを一時停止
                        //Debug.Log("たらいを回転させる");
                        Tarai.transform.DORotate(new Vector3(0f, 0f, 180), 0.3f);
                        Tarai.transform.DOMove(new Vector3(PlayerSC.ToTarai.transform.position.x, PlayerSC.ToTarai.transform.position.y + 10f, PlayerSC.ToTarai.transform.position.z), 1.0f);
                        Sum_AnzenYoshi_Card--;
                        if (ShuffleCardsMSC.int_StockCard_Up == -1)   // ジャンケンカード以外のアイテムならば
                        {
                            Btn_StockCard_Up.interactable = false;
                            ShuffleCardsMSC.Stock_Button_Up.SetActive(false);
                            ShuffleCardsMSC.int_StockCard_Up = -2;
                        }
                        else if (ShuffleCardsMSC.int_StockCard_Down == -1)   // ジャンケンカード以外のアイテムならば
                        {
                            Btn_StockCard_Down.interactable = false;
                            ShuffleCardsMSC.Stock_Button_Down.SetActive(false);
                            ShuffleCardsMSC.int_StockCard_Down = -2;
                        }
                        var sequence = DOTween.Sequence();
                        sequence.InsertCallback(1f, () => CloseBarrier());
                    });
            }
        }
        else
        {

        }
    }

    public void FallTarai_After03()
    {
        Tarai.transform.DOMove(new Vector3(PlayerSC.ToTarai.transform.position.x, PlayerSC.ToTarai.transform.position.y - 0.01f, PlayerSC.ToTarai.transform.position.z), 5.3f);

    }

    public void Tarai_Fukki()
    {
        PlayerSC.anim.SetBool("damage", false);
    }

    [PunRPC]
    public void FallTarai()                                // たらいを表示、(落下)、非表示
    {
        AppearTarai();                                     // 全員にたらいを一斉に表示ONさせる
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(4f, () => CloseTarai());   // 全員にたらいを一斉に非表示ONさせる
    }

    [PunRPC]
    public void FallSara()                                // たらいを表示、落下、非表示
    {
        AppearSara();                                     // 全員にたらいを一斉に表示ONさせる
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(4f, () => CloseSara());   // 全員にたらいを一斉に非表示ONさせる
    }

    public void share_tarai_Position()                // たらいの位置をジャンパーの位置に移動して全員に共有する
    {
        //Debug.Log("たらいの位置を移動して共有する");
        Tarai.transform.position = PlayerSC.MyTenjo.transform.position;
        Tarai_Guwan.transform.position = new Vector3(PlayerSC.MyTenjo.transform.position.x, PlayerSC.MyHead.transform.position.y, PlayerSC.MyTenjo.transform.position.z);

    }

    public void AppearTarai()
    {
        Tarai.SetActive(true);
    }

    public void CloseTarai()
    {
        Tarai.SetActive(false);
    }

    public void AppearSara()
    {
        Sara.SetActive(true);
    }

    public void CloseSara()
    {
        Sara.SetActive(false);
    }

    [PunRPC]
    public void Tarai_MoveTo_PosX_Player1()   // Tarai を realPosX_Player1 の位置に移動する
    {
        //Debug.Log("Tarai を realPosX_Player1 の位置に移動する");
        Sara.transform.position = new Vector3(PosX_Player1, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Sara_Guwan.transform.position = new Vector3(PosX_Player1, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);

        Tarai.transform.position = PlayerSC.MyTenjo.transform.position;
        Tarai_Guwan.transform.position = new Vector3(PlayerSC.MyTenjo.transform.position.x, PlayerSC.MyHead.transform.position.y, PlayerSC.MyTenjo.transform.position.z);
    }

    [PunRPC]
    public void Tarai_MoveTo_PosX_Player2()   // Tarai を realPosX_Player2 の位置に移動する
    {
        Tarai.transform.position = new Vector3(realPosX_Player2, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Tarai_Guwan.transform.position = new Vector3(realPosX_Player2, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);
        Sara.transform.position = new Vector3(PosX_Player2, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Sara_Guwan.transform.position = new Vector3(PosX_Player2, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);
    }

    [PunRPC]
    public void Tarai_MoveTo_PosX_Player3()   // Tarai を realPosX_Player3 の位置に移動する
    {
        Tarai.transform.position = new Vector3(realPosX_Player3, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Tarai_Guwan.transform.position = new Vector3(realPosX_Player3, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);
        Sara.transform.position = new Vector3(PosX_Player3, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Sara_Guwan.transform.position = new Vector3(PosX_Player3, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);
    }

    [PunRPC]
    public void Tarai_MoveTo_PosX_Player4()   // Tarai を realPosX_Player4 の位置に移動する
    {
        Tarai.transform.position = new Vector3(realPosX_Player4, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Tarai_Guwan.transform.position = new Vector3(realPosX_Player4, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);
        Sara.transform.position = new Vector3(PosX_Player4, PosY_taraiSet + flo_sky_taraiSet, Tarai.transform.position.z);
        Sara_Guwan.transform.position = new Vector3(PosX_Player4, PosY_taraiSet + 0.8f, Tarai_Guwan.transform.position.z);
    }
    #endregion

    #region  // ゴール処理一連
    public void Judge_GOAL()   // ゴールラインに到達したか判定する
    {
        if (myPlayer.transform.position.x >= GoalCorn_Head.transform.position.x)  // ゴールにたどり着いた！
        {
            //Debug.Log("GOOOOOALLL！！！！");
            Check_Champ_Avator();
            photonView.RPC("ShareGameSet", RpcTarget.All);
            GoalFlg = true;
        }
        else  // まだゴールまで来ていない
        {
            photonView.RPC("Share_JKAvator_StandSetting", RpcTarget.All);
        }
    }

    public void Check_Champ_Avator()
    {
        //Debug.Log("チャンピョンのアバターをセットします。");
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            if (WinnerNum == 1)          // プレイヤー1 が勝利者
            {
                //Debug.Log("P1 私がチャンプだ！！！！");
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            if (WinnerNum == 2)          // プレイヤー2 が勝利者
            {
                //Debug.Log("P2 私がチャンプだ！！！！");
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            if (WinnerNum == 3)          // プレイヤー3 が勝利者
            {
                //Debug.Log("P3 私がチャンプだ！！！！");
            }
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            if (WinnerNum == 4)          // プレイヤー4 が勝利者
            {
                //Debug.Log("P4 私がチャンプだ！！！！");
            }
        }
        SetMyAvator_ForChamp();  // チャンプ のアバターを ゴールパネル（表彰台）にセットします。
    }


    public void SetMyAvator_ForChamp()  // チャンプ のアバターを ゴールパネル（表彰台）にセットします。
    {
        photonView.RPC("CloseWinner_avator_All", RpcTarget.All);

        //Debug.Log("チャンプ のアバターを ゴールパネル（表彰台）にセットします。");
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
        else if (int_conMyCharaAvatar == 5) // ずん子ちゃん
        {
            photonView.RPC("AppearWinner_avator_5", RpcTarget.All);
        }
        //Debug.Log("チャンプ のアバターを ゴールパネル（表彰台）にセットしました。");
    }

    [PunRPC]
    public void ShareGameSet()          // GOAL して GameSet した旨を全員に共有する
    {
        //Debug.Log("GOOOOOALLL！！！！");
        Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
        GameSet_Flg = true;
        Erase_Text_Announcement();
        AppearGameSet_LOGO();
        CloseOpenMyJankenPanel_Button();
        CloseDebug_Buttons();
        ClosePanel_Kizetsu();      // 気絶パネルを非表示にする
        BGM_SE_MSC.Stop_BGM();
        BGM_SE_MSC.whistle_SE();
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3f, () => AfterGameSet());
    }

    public void AfterGameSet()               // 試合終了後の処理
    {
        Countdown_Push_OpenMyJankenPanel_Button_Flg = false;
        GameSet_Flg = true;
        Erase_Text_Announcement();
        CloseGoal_Iwai_All();
        int iwai = UnityEngine.Random.Range(1, 4);
        if (iwai == 1)
        {
            AppearGoal_Iwai_1();
        }
        else if (iwai == 2)
        {
            AppearGoal_Iwai_2();
        }
        else
        {
            AppearGoal_Iwai_3();
        }
        AppearWinPanel();
        BGM_SE_MSC.Fanfare_solo_SE();
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(5f, () => BGM_SE_MSC.Fanfare_Roop_BGM());

        var sequence30 = DOTween.Sequence();
        sequence30.InsertCallback(25f, () => AutoLogout_AfterGoal());
    }

    public void AutoLogout_AfterGoal()          // 試合終了後に自動ログアウトする処理 （30秒後）
    {
        AppearPanel_AutoLogout();
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(10f, () => BackTo_TitleScene());    // タイトル画面へ戻ります            
    }

    public void AppearPanel_AutoLogout()
    {
        Panel_AutoLogout.SetActive(true);
    }

    public void ClosePanel_AutoLogout()
    {
        Panel_AutoLogout.SetActive(false);
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


    public void AppearGoal_Iwai_1()
    {
        Goal_Iwai_1.SetActive(true);
    }

    public void CloseGoal_Iwai_1()
    {
        Goal_Iwai_1.SetActive(false);
    }

    public void AppearGoal_Iwai_2()
    {
        Goal_Iwai_2.SetActive(true);
    }

    public void CloseGoal_Iwai_2()
    {
        Goal_Iwai_2.SetActive(false);
    }

    public void AppearGoal_Iwai_3()
    {
        Goal_Iwai_3.SetActive(true);
    }

    public void CloseGoal_Iwai_3()
    {
        Goal_Iwai_3.SetActive(false);
    }

    public void CloseGoal_Iwai_All()
    {
        CloseGoal_Iwai_1();
        CloseGoal_Iwai_2();
        CloseGoal_Iwai_3();
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

    [PunRPC]
    public void AppearWinner_avator_5()
    {
        Winner_avator_5.SetActive(true);

    }

    public void CloseWinner_avator_5()
    {
        Winner_avator_5.SetActive(false);
    }

    [PunRPC]
    public void CloseWinner_avator_All()
    {
        CloseWinner_avator_1();
        CloseWinner_avator_2();
        CloseWinner_avator_3();
        CloseWinner_avator_4();
        CloseWinner_avator_5();
    }
    #endregion


    #region  // イントロ（ルール説明）
    public void AppearPanel_Intro()
    {
        Panel_Intro.SetActive(true);
        CloseImage_Group_Introp();
        CloseText_Group_Intro();
        AppearText_Group_Intro();
    }

    public void ClosePanel_Intro()
    {
        Panel_Intro.SetActive(false);
    }

    public void AppearImage_Group_Introp()
    {
        Image_Group_Introp.SetActive(true);
    }

    public void CloseImage_Group_Introp()
    {
        Image_Group_Introp.SetActive(false);
    }

    public void AppearText_Group_Intro()
    {
        Text_Group_Intro.SetActive(true);
    }

    public void CloseText_Group_Intro()
    {
        Text_Group_Intro.SetActive(false);
    }

    public void SwitchPanel_Intro()
    {
        if (Text_Group_Intro.activeSelf) // Text_Group_Intro ON だったら
        {
            CloseImage_Group_Introp();
            CloseText_Group_Intro();
            AppearImage_Group_Introp();
        }
        else                            // Text_Group_Intro OFF だったら
        {
            CloseImage_Group_Introp();
            CloseText_Group_Intro();
            AppearText_Group_Intro();
        }
    }

    public void On_Off_Panel_Intro()
    {
        if (Panel_Intro.activeSelf) // Panel_Intro ON だったら
        {
            ClosePanel_Intro();
        }
        else                         // Panel_Intro OFF だったら
        {
            AppearPanel_Intro();
        }
    }
    #endregion

    public void AppearPanel_ToTitle()
    {
        Panel_ToTitle.SetActive(true);
    }

    public void ClosePanel_ToTitle()
    {
        Panel_ToTitle.SetActive(false);
    }

    public void AppearNinja_Button()
    {
        Ninja_Button.SetActive(true);
    }

    public void CloseNinja_Button()
    {
        Ninja_Button.SetActive(false);
    }


    #region// ログアウト関連


    public void Logout_InTheMiddle()  // これから途中退席する人の処理
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("Logout_Player1", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("Logout_Player2", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("Logout_Player3", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("Logout_Player4", RpcTarget.All);
        }
    }


    [PunRPC]
    public void Logout_Player1()  // player1 が退出しました
    {
        logon_player1 = false;
        alivePlayer1 = 0;            // これ以降、常に alivePlayerフラグ が 0 になる
        int_NowWaiting_Player1 =0;  // これ以降、常に待機フラグが OFFになる
        AppearLogout_Kakejiku1();    // 掛け軸を表示する 
    }

    [PunRPC]
    public void Logout_Player2()  // player2 が退出しました
    {
        logon_player2 = false;
        alivePlayer2 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        int_NowWaiting_Player2 =0;  // これ以降、常に待機フラグが OFFになる
        AppearLogout_Kakejiku2();    // 掛け軸を表示する
    }

    [PunRPC]
    public void Logout_Player3()  // player3 が退出しました
    {
        logon_player3 = false;
        alivePlayer3 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        int_NowWaiting_Player3 =0;  // これ以降、常に待機フラグが OFFになる
        AppearLogout_Kakejiku3();    // 掛け軸を表示する
    }

    [PunRPC]
    public void Logout_Player4()  // player4 が退出しました
    {
        logon_player4 = false;
        alivePlayer4 = 0;         // これ以降、常に alivePlayerフラグ が 0 になる
        int_NowWaiting_Player4 =0;  // これ以降、常に待機フラグが OFFになる
        AppearLogout_Kakejiku4();    // 掛け軸を表示する
    }

    public void Ctrl_Check_NowLoginMember()   // 一旦全員のフラグをログアウトにして、ログインしている人から返答をもらう              
    {
        //Debug.Log("全員に対してログイン状況を確認します");

        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Check_NowLoginMember();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.1f, () => Check_NowLoginMember());
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.2f, () => Check_NowLoginMember());
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.3f, () => Check_NowLoginMember());
        }
    }

    public void Check_NowLoginMember()   // 一旦全員のフラグをログアウトにして、ログインしている人から返答をもらう
    {
        if (Flg_before_Check_NowLoginMember)  // 実行前ならtrue、実行始まったらfalse
        {
            photonView.RPC("ChangeFlg_False_Check_NowLoginMember", RpcTarget.All);

            //Debug.Log("一旦全部のログオンフラグをfalseにする");
            photonView.RPC("ChangeFlg_Logout_Player1", RpcTarget.All);   // player1 が退出しました
            photonView.RPC("ChangeFlg_Logout_Player2", RpcTarget.All);   // player2 が退出しました
            photonView.RPC("ChangeFlg_Logout_Player3", RpcTarget.All);   // player3 が退出しました
            photonView.RPC("ChangeFlg_Logout_Player4", RpcTarget.All);   // player4 が退出しました
                                                                         //logon_player1 = false;
                                                                         //logon_player2 = false;
                                                                         //logon_player3 = false;
                                                                         //logon_player4 = false;
            photonView.RPC("AppearLogout_Kakejiku_All", RpcTarget.All);   // すべての掛け軸を開く

            //Debug.Log("全員に対して死活監視を実行 & 自分の掛け軸を閉じます");
            photonView.RPC("Response_AliveMonitoring", RpcTarget.All);  // 全員に対して死活監視を実行 → ログイン中ならば返答が返ってくる

            var sequence = DOTween.Sequence();
            sequence.InsertCallback(3.9f, () => shareChangeFlg_True_Check_NowLoginMember());
        }
    }

    public void shareChangeFlg_True_Check_NowLoginMember()   // 実行前ならtrue、実行始まったらfalse
    {
        photonView.RPC("ChangeFlg_True_Check_NowLoginMember", RpcTarget.All);
    }

    [PunRPC]
    public void ChangeFlg_True_Check_NowLoginMember()   // 実行前ならtrue、実行始まったらfalse
    {
        Flg_before_Check_NowLoginMember = true;
    }

    [PunRPC]
    public void ChangeFlg_False_Check_NowLoginMember()   // 実行前ならtrue、実行始まったらfalse
    {
        Flg_before_Check_NowLoginMember = false;
    }

    [PunRPC]
    public void Response_AliveMonitoring()  // 死活監視の返答  & 掛け軸を閉じます
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("Still_Login_Player1", RpcTarget.All);   // 私はまだログイン中です！  & 掛け軸を閉じます
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("Still_Login_Player2", RpcTarget.All);   // 私はまだログイン中です！ & 掛け軸を閉じます
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("Still_Login_Player3", RpcTarget.All);   // 私はまだログイン中です！ & 掛け軸を閉じます
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("Still_Login_Player4", RpcTarget.All);   // 私はまだログイン中です！ & 掛け軸を閉じます
        }
    }

    [PunRPC]
    public void ChangeFlg_Logout_Player1()  // player1 が退出しました
    {
        logon_player1 = false;
    }

    [PunRPC]
    public void ChangeFlg_Logout_Player2()  // player2 が退出しました
    {
        logon_player2 = false;
    }

    [PunRPC]
    public void ChangeFlg_Logout_Player3()  // player3 が退出しました
    {
        logon_player3 = false;
    }

    [PunRPC]
    public void ChangeFlg_Logout_Player4()  // player4 が退出しました
    {
        logon_player4 = false;
    }

    #endregion


    #region// 所持金（ゴールド）
    public void Update_Gold_Players()   // 各プレイヤーの所持金（ゴールド）を同期します
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Gold_MyPlayer = Gold_Player1;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Gold_MyPlayer = Gold_Player2;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Gold_MyPlayer = Gold_Player3;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            Gold_MyPlayer = Gold_Player4;
        }
    }

    public void Set_p05_calculation_Gold() // ゴールド +5
    {
        int_calculation_Gold = 5;
    }

    public void Set_p10_calculation_Gold() // ゴールド +10
    {
        int_calculation_Gold = 10;
    }

    public void Set_p20_calculation_Gold() // ゴールド +20
    {
        int_calculation_Gold = 20;
    }

    public void Set_m05_calculation_Gold() // ゴールド -5
    {
        int_calculation_Gold = -5;
    }

    public void Set_m10_calculation_Gold()  // ゴールド -10
    {
        int_calculation_Gold = -10;
    }

    public void Set_m20_calculation_Gold()  // ゴールド -20
    {
        int_calculation_Gold = -20;
    }

    public void calculate_Gold_Players()   // 所持金（ゴールド）をマイナス/プラスします
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Gold_Player1 = Gold_Player1 + int_calculation_Gold;
            Gold_MyPlayer = Gold_Player1;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Gold_Player2 = Gold_Player2 + int_calculation_Gold;
            Gold_MyPlayer = Gold_Player2;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Gold_Player3 = Gold_Player3 + int_calculation_Gold;
            Gold_MyPlayer = Gold_Player3;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            Gold_Player4 = Gold_Player4 + int_calculation_Gold;
            Gold_MyPlayer = Gold_Player4;
        }
    }

    public void Stream_Gold_Plus10()  // 所持金（ゴールド）を+10する 一連の処理
    {
        Set_p10_calculation_Gold();  // ゴールド +10
        calculate_Gold_Players();    // 所持金（ゴールド）をマイナス/プラスします          
        Appear_text_Gold_Plus10();   // text_Gold_Plus10 を開く
        BGM_SE_MSC.CoinGet_SE();     // コインゲット
    }

    public void Appear_text_Gold_Plus10()  // text_Gold_Plus10 を開く
    {
        text_Gold_Plus10.text = "+10G";
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3.5f, () => Erase_text_Gold_Plus10());
    }

    public void Erase_text_Gold_Plus10()  // text_Gold_Plus10 を空欄にする（消しゴムで消すかのように）
    {
        text_Gold_Plus10.text = "";
    }
    #endregion



    #region// エンカウントするアイテムカード関連
    #region// アイテムカードの裏面Up
    public void Stream_Encounter_ItemCard_UraUp()  // アイテムカードの裏面Up 一連の処理  （現在は Gold+10 のみ）
    {
        AppearEncounter_ItemCard_UraUp();
        Encounter_ItemCard_UraUp.transform.position = Center_Mark.transform.position;         // プレイヤー位置を Center_Mark に移動
        Encounter_ItemCard_UraUp.transform.DOLocalMove(new Vector3(0, 500, 0), 1.5f);              //ローカル座標で上方向に移動

        RandomChange_ItemCard_Omote();   // アイテムカードの図柄を ランダムで変更する

        var sequence = DOTween.Sequence();
        sequence.InsertCallback(1.5f, () => CloseEncounter_ItemCard_UraUp());

        var sequence2 = DOTween.Sequence();
        sequence2.InsertCallback(1.5f, () => Stream_Encounter_ItemCard_Down());     // アイテムカードの裏面Down 一連の処理
    }

    public void AppearEncounter_ItemCard_UraUp()
    {
        Encounter_ItemCard_UraUp.SetActive(true);
    }

    public void CloseEncounter_ItemCard_UraUp()
    {
        Encounter_ItemCard_UraUp.SetActive(false);
    }

    #endregion

    #region// アイテムカードの裏面Down
    public void Stream_Encounter_ItemCard_Down()  // アイテムカードの裏面Down 一連の処理
    {
        AppearEncounter_ItemCard_Down();
        Encounter_ItemCard_Down.transform.position = Upperr_Mark.transform.position;         // プレイヤー位置を Upperr_Mark に移動
        Encounter_ItemCard_Down.transform.DOLocalMove(Center_Mark.transform.position, 1);    // プレイヤー位置を Center_Mark に移動
        CardReverseMSC = Encounter_ItemCard_Down.GetComponent<CardReverse>();
        CardReverseMSC.StartCardOpen();                                                      // カードをクルンと回す
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(3.5f, () => CloseEncounter_ItemCard_Down());
    }


    public void AppearEncounter_ItemCard_Down()
    {
        Encounter_ItemCard_Down.SetActive(true);
    }
    
    public void CloseEncounter_ItemCard_Down()
    {
        Encounter_ItemCard_Down.SetActive(false);
    }

    public void RandomChange_ItemCard_Omote()   // アイテムカードの図柄を ランダムで変更する
    {
        int RandomChange_ItemCard = UnityEngine.Random.Range(1, 11);

        if(RandomChange_ItemCard <= 2)
        {
            ChangeTo_Fatigue_Card();   // 図柄を 疲労カード にする
        }
        else if (RandomChange_ItemCard >= 3 && RandomChange_ItemCard <= 4)
        {
            ChangeTo_Gold_Card();      // 図柄を Goldカード にする
        }
        else if (RandomChange_ItemCard >= 5 && RandomChange_ItemCard <= 7)
        {
            ChangeTo_Muteki_Card();    // 図柄を むてきチョキカード にする
        }
        else
        {
            ChangeTo_AnzenYoshi_Card();   // 図柄を AnzenYoshiカード にする
        }
    }


    public void ChangeTo_Fatigue_Card()   // 図柄を 疲労カード にする
    {
        ItemCard_Omote.sprite = Fatigue_Card;
        text_Item_Setsumei.text = "疲労で 肩こり";
        Katakori_to_SetWFlag = true;  // 肩こりのせいで白旗0～3枚
    }

    public void ChangeTo_Gold_Card()   // 図柄を Goldカード にする
    {
        ItemCard_Omote.sprite = Gold_Card;
        text_Item_Setsumei.text = "ゴールド +10G";

        if (GameSet_Flg == false)     // 試合中であれば
        {
            var sequence2 = DOTween.Sequence();
            sequence2.InsertCallback(3f, () => Stream_Gold_Plus10());     // 所持金（ゴールド）を+10する 一連の処理
        }
    }

    public void ChangeTo_Muteki_Card()   // 図柄を むてきチョキカード にする
    {
        ItemCard_Omote.sprite = ShuffleCardsMSC.sprite_Muteki;
        if (ShuffleCardsMSC.Stock_Button_Up.activeSelf == false || ShuffleCardsMSC.Stock_Button_Down.activeSelf == false)  // どちらか一方のストックが開いていたら
        {
            text_Item_Setsumei.text = "むてきチョキ をストック";
            //Debug.Log("むてきチョキ をストック");
            if (ShuffleCardsMSC.Stock_Button_Up.activeSelf == false)  // StockU が非表示（未セット）ならば
            {
                //Debug.Log("StockU が非表示（未セット）ならば");
                ShuffleCardsMSC.StockUp_Set_MutekiCard();     // StockU に むてきカード をセットします  
                ShuffleCardsMSC.Stock_Button_Up.SetActive(true);
            }
            else
            {
                //Debug.Log("StockU が アクティブ ならば");
                ShuffleCardsMSC.StockDown_Set_MutekiCard();     // StockDown に むてきカード をセットします  
                ShuffleCardsMSC.Stock_Button_Down.SetActive(true);
            }
        }
        else
        {
            text_Item_Setsumei.text = "アイテムがいっぱいです";
        }
    }

    public void ChangeTo_AnzenYoshi_Card()   // 図柄を AnzenYoshiカード にする
    {
        ItemCard_Omote.sprite = AnzenYoshi_Card;
        if (ShuffleCardsMSC.Stock_Button_Up.activeSelf == false || ShuffleCardsMSC.Stock_Button_Down.activeSelf == false)  // どちらか一方のストックが開いていたら
        {
            text_Item_Setsumei.text = "安全ヨシ！";
            Sum_AnzenYoshi_Card++;  // Sum_AnzenYoshi_Card の枚数をプラス1します

            if (ShuffleCardsMSC.Stock_Button_Up.activeSelf == false)  // StockU が非表示（未セット）ならば
            {
                //Debug.Log("StockU が非表示（未セット）ならば");
                //Debug.Log("StockU に AnzenYoshi_Card をセットします  ");
                ShuffleCardsMSC.Stock_Button_Up.gameObject.GetComponent<Image>().sprite = AnzenYoshi_Card;
                ShuffleCardsMSC.Stock_Button_Up.SetActive(true);
                ShuffleCardsMSC.int_StockCard_Up = -1;
            }
            else
            {
                //Debug.Log("StockU が アクティブ ならば");
                //Debug.Log("StockDown に AnzenYoshi_Card をセットします  ");
                ShuffleCardsMSC.Stock_Button_Down.gameObject.GetComponent<Image>().sprite = AnzenYoshi_Card;
                ShuffleCardsMSC.Stock_Button_Down.SetActive(true);
                ShuffleCardsMSC.int_StockCard_Down = -1;
            }
        }
        else
        {
            text_Item_Setsumei.text = "アイテムがいっぱいです";
        }
    }

    #endregion

    #region// 肩こり
    public void Katakori_stream()  // 肩こりフラグがONの時のみ実行される（治癒されるまで）
    {
        if (Katakori_to_SetWFlag)  // 肩こりフラグONの時 → 治るか確認
        {
            if (Katakori_hajimari_Flg == false)  // 肩こり発症して2ターン目以降である
            {
                if (HariQ_Button.activeSelf == false)  // 表示されていなければ
                {
                    HariQ_Button.SetActive(true);     //表示する
                }
                int Katakori_cure = UnityEngine.Random.Range(1, 11);       // 肩こりの自然治癒率
                if (Katakori_cure <= 2)
                {
                    CureKatakori();  // 肩こりが治癒しました
                }
            }
            else                     // 肩こり発症して1ターン目
            {
                Katakori_hajimari_Flg = false;       // フラグを肩こり発症して2ターン目以降の扱いにする
                if (GameSet_Flg == false)            // 試合中であれば
                {
                    var sequence = DOTween.Sequence();
                    sequence.InsertCallback(3f, () => Katakori_stream_After3());  // 肩こりフラグがONの時のみ実行される（治癒されるまで）
                }
            }
        }
    }

    public void Katakori_stream_After3()  // 肩こりフラグがONの時のみ実行される（治癒されるまで）
    {
        HariQ_Button.SetActive(true);     //表示する
        Katakori_Mark.SetActive(true);    //表示する    
        BGM_SE_MSC.Tired_SE();
    }

    public void CureKatakori()  // 肩こりが治癒しました
    {
        BGM_SE_MSC.cure_SE();  // ぴゅいーん（回復音）
        Text_Katakori_cure.text = "肩こりが治りました";
        Katakori_Mark.SetActive(false);   //非表示にする
        Katakori_to_SetWFlag = false;    // 肩こりが治癒しました（フラグOFF）
        var sequence = DOTween.Sequence();
        sequence.InsertCallback(2f, () => CureKatakori2());
    }

    public void CureKatakori2()  // 肩こりが治癒しました2
    {
        HariQ_Button.SetActive(false);    //非表示にする
        //Debug.Log("肩こりが治癒しました");
        Text_Katakori_cure.text = "";
        Katakori_hajimari_Flg = true;
        Redistribute_JankenCards();    // じゃんけんカードの再配布
    }

    #endregion


    #region// はりきゅう
    public void PushHariQButton()  // 肩こりフラグをOFFにする
    {
        if (Gold_MyPlayer >= 10)
        {
            Set_m10_calculation_Gold();  // ゴールド -10
            calculate_Gold_Players();    // 所持金（ゴールド）をマイナス/プラスします
            ResetCountdown_timer_Kettei_1();
            CureKatakori();  // 肩こりが治癒しました
        }
        else  // ゴールドが足りないよ
        {
            // ぽわわ～ん。。。
            BGM_SE_MSC.gold_fusoku_SE();
            Text_Gold_fusoku_HariQ.text = "ゴールドが 不足しています...";
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(3f, () => Erase_Text_Gold_fusoku_HariQ());
        }
    }

    public void Erase_Text_Gold_fusoku_HariQ()
    {
        Text_Gold_fusoku_HariQ.text = "";
    }
    #endregion

    #region // 安全ヨシ
    public void AppearBarrier()
    {
        //Debug.Log("AppearBarrier！");
        PlayerSC.Barrier.SetActive(true);
    }

    public void CloseBarrier()
    {
        //Debug.Log("CloseBarrier！");
        PlayerSC.Barrier.SetActive(false);
    }
    #endregion

    #endregion

    public void PlayCancel_SE()     // キャンセル音
    {
        BGM_SE_MSC.cancel_SE();     // キャンセル音
    }


    #region// 体力（Life）

    public void StartSet_Life_Players()   // 体力をセットします[初期値のセット]
    {
        //Debug.Log("体力をセットします[初期値のセット]");

        int_calculation_Life = 0;
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Life_Player1 = int_Default_Life;
            Life_MyPlayer = Life_Player1;
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.1f, () => Update_Life_Players());
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Life_Player2 = int_Default_Life;
            Life_MyPlayer = Life_Player2;
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.2f, () => Update_Life_Players());
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Life_Player3 = int_Default_Life;
            Life_MyPlayer = Life_Player3;
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.3f, () => Update_Life_Players());
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            Life_Player4 = int_Default_Life;
            Life_MyPlayer = Life_Player4;
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.4f, () => Update_Life_Players());
        }
    }

    public void Set_Zero_calculation_Life() // Life +-0
    {
        int_calculation_Life = 0;
    }

    public void Set_plus_01_calculation_Life() // Life +1
    {
        int_calculation_Life = 1;
    }

    public void Set_minus_01_calculation_Life() // Life -1
    {
        int_calculation_Life = -1;
    }

    public void Set_minus_02_calculation_Life() // Life -2
    {
        int_calculation_Life = -2;
    }

    public void calculate_Life_Players()   // 現在の体力 にマイナス/プラスします
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            Life_Player1 = Life_Player1 + int_calculation_Life;
            Life_MyPlayer = Life_Player1;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            Life_Player2 = Life_Player2 + int_calculation_Life;
            Life_MyPlayer = Life_Player2;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            Life_Player3 = Life_Player3 + int_calculation_Life;
            Life_MyPlayer = Life_Player3;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            Life_Player4 = Life_Player4 + int_calculation_Life;
            Life_MyPlayer = Life_Player4;
        }
    }


    [PunRPC]
    public void Update_Life_Players()   // 各プレイヤーのLifeを同期します
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("Life_Koshin_Player1", RpcTarget.All);         // 誰のLifeを更新するの？
            Life_MyPlayer = Life_Player1;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("Life_Koshin_Player2", RpcTarget.All);         // 誰のLifeを更新するの？
            Life_MyPlayer = Life_Player2;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("Life_Koshin_Player3", RpcTarget.All);         // 誰のLifeを更新するの？
            Life_MyPlayer = Life_Player3;
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("Life_Koshin_Player4", RpcTarget.All);         // 誰のLifeを更新するの？
            Life_MyPlayer = Life_Player4;
        }

        Life_Koshin_Atai = Life_MyPlayer;
        checkLife_Koshin_Atai();                                   // Lifeの数値はいくつ？ → その値を全員に共有
        photonView.RPC("totalLife_Koshin_Atai", RpcTarget.All);    // 該当プレイヤーのLifeを指定された値に更新する
        Check_DamageUmu();                                         // ダメージの有無を確認する
        Set_LifeHeart();                                           // 各プレイヤーの体力ハートをセットする
        Check_LifeZERO();                                          // 体力がゼロになっているか確認する → セロなら白旗ALL
    }

    [PunRPC]
    public void Life_Koshin_Player1()   // 誰のLifeを更新するの？
    {
        Life_Koshin_PlayerNum = 1;
    }

    [PunRPC]
    public void Life_Koshin_Player2()   // 誰のLifeを更新するの？
    {
        Life_Koshin_PlayerNum = 2;
    }

    [PunRPC]
    public void Life_Koshin_Player3()   // 誰のLifeを更新するの？
    {
        Life_Koshin_PlayerNum = 3;
    }

    [PunRPC]
    public void Life_Koshin_Player4()   // 誰のLifeを更新するの？
    {
        Life_Koshin_PlayerNum = 4;
    }


    public void checkLife_Koshin_Atai()        // Lifeの数値はいくつ？ → その値を全員に共有
    {
        switch (Life_Koshin_Atai)
        {
            case 1: //
                photonView.RPC("Life_Koshin_Atai_1", RpcTarget.All);
                break;
            case 2: //
                photonView.RPC("Life_Koshin_Atai_2", RpcTarget.All);
                break;
            case 3: //
                photonView.RPC("Life_Koshin_Atai_3", RpcTarget.All);
                break;
            case 4: //
                photonView.RPC("Life_Koshin_Atai_4", RpcTarget.All);
                break;
            case 5: //
                photonView.RPC("Life_Koshin_Atai_5", RpcTarget.All);
                break;
            case 6: //
                photonView.RPC("Life_Koshin_Atai_6", RpcTarget.All);
                break;
            case 7: //
                photonView.RPC("Life_Koshin_Atai_7", RpcTarget.All);
                break;
            case 8: //
                photonView.RPC("Life_Koshin_Atai_8", RpcTarget.All);
                break;
            case 9: //
                photonView.RPC("Life_Koshin_Atai_9", RpcTarget.All);
                break;
            case 0: //
                photonView.RPC("Life_Koshin_Atai_0", RpcTarget.All);
                break;
            default:
                // その他処理
                break;
        }
    }

    [PunRPC]
    public void Life_Koshin_Atai_1()
    {
        Life_Koshin_Atai = 1;
    }

    [PunRPC]
    public void Life_Koshin_Atai_2()
    {
        Life_Koshin_Atai = 2;
    }

    [PunRPC]
    public void Life_Koshin_Atai_3()
    {
        Life_Koshin_Atai = 3;
    }

    [PunRPC]
    public void Life_Koshin_Atai_4()
    {
        Life_Koshin_Atai = 4;
    }

    [PunRPC]
    public void Life_Koshin_Atai_5()
    {
        Life_Koshin_Atai = 5;
    }

    [PunRPC]
    public void Life_Koshin_Atai_6()
    {
        Life_Koshin_Atai = 6;
    }

    [PunRPC]
    public void Life_Koshin_Atai_7()
    {
        Life_Koshin_Atai = 7;
    }

    [PunRPC]
    public void Life_Koshin_Atai_8()
    {
        Life_Koshin_Atai = 8;
    }

    [PunRPC]
    public void Life_Koshin_Atai_9()
    {
        Life_Koshin_Atai = 9;
    }

    [PunRPC]
    public void Life_Koshin_Atai_0()
    {
        Life_Koshin_Atai = 0;
    }


    [PunRPC]
    public void totalLife_Koshin_Atai()          // 該当プレイヤーのLifeを指定された値に更新する
    {
        switch (Life_Koshin_PlayerNum)
        {
            case 1: //
                Life_Player1 = Life_Koshin_Atai;
                break;
            case 2: //
                Life_Player2 = Life_Koshin_Atai;
                break;
            case 3: //
                Life_Player3 = Life_Koshin_Atai;
                break;
            case 4: //
                Life_Player4 = Life_Koshin_Atai;
                break;
            default:
                // その他処理
                break;
        }
    }


    #region// 体力 ハートの調整

    public void Set_LifeHeart()  // 各プレイヤーの体力ハートをセットする
    {
        text_Life_P1.text = (Life_Player1).ToString();
        text_Life_P2.text = (Life_Player2).ToString();
        text_Life_P3.text = (Life_Player3).ToString();
        text_Life_P4.text = (Life_Player4).ToString();

        Set_LifeHeart_P1();  // プレイヤーP1の体力ハートをセットする
        Set_LifeHeart_P2();  // プレイヤーP2の体力ハートをセットする
        Set_LifeHeart_P3();  // プレイヤーP3の体力ハートをセットする
        Set_LifeHeart_P4();  // プレイヤーP4の体力ハートをセットする
        //Check_DamageUmu();  // ダメージの有無を確認する
    }

    #region// 体力 ハートの調整_P1
    public void Set_LifeHeart_P1()  // プレイヤーP1の体力ハートをセットする
    {
        switch (Life_Player1)
        {
            case 1: //
                Set_LifeHeart_P1_to1();
                break;

            case 2: //
                Set_LifeHeart_P1_to2();
                break;

            case 3: //
                Set_LifeHeart_P1_to3();
                break;

            case 4: //
                Set_LifeHeart_P1_to4();
                break;

            case 5: //
                Set_LifeHeart_P1_to5();
                break;

            case 6: //
                Set_LifeHeart_P1_to6();
                break;

            case 7: //
                Set_LifeHeart_P1_to7();
                break;

            case 8: //
                Set_LifeHeart_P1_to8();
                break;

            case 0: //
                Set_LifeHeart_P1_to0();
                break;

            default:
                // その他処理
                break;
        }
    }


    [PunRPC]
    public void Set_LifeHeart_P1_to1()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(false);
        Heart3_P1.SetActive(false);
        Heart4_P1.SetActive(false);
        Heart5_P1.SetActive(false);
        Heart6_P1.SetActive(false);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to2()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(false);
        Heart4_P1.SetActive(false);
        Heart5_P1.SetActive(false);
        Heart6_P1.SetActive(false);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to3()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(true);
        Heart4_P1.SetActive(false);
        Heart5_P1.SetActive(false);
        Heart6_P1.SetActive(false);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to4()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(true);
        Heart4_P1.SetActive(true);
        Heart5_P1.SetActive(false);
        Heart6_P1.SetActive(false);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to5()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(true);
        Heart4_P1.SetActive(true);
        Heart5_P1.SetActive(true);
        Heart6_P1.SetActive(false);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to6()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(true);
        Heart4_P1.SetActive(true);
        Heart5_P1.SetActive(true);
        Heart6_P1.SetActive(true);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to7()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(true);
        Heart4_P1.SetActive(true);
        Heart5_P1.SetActive(true);
        Heart6_P1.SetActive(true);
        Heart7_P1.SetActive(true);
        Heart8_P1.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to8()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P1.SetActive(true);
        Heart2_P1.SetActive(true);
        Heart3_P1.SetActive(true);
        Heart4_P1.SetActive(true);
        Heart5_P1.SetActive(true);
        Heart6_P1.SetActive(true);
        Heart7_P1.SetActive(true);
        Heart8_P1.SetActive(true);
    }

    [PunRPC]
    public void Set_LifeHeart_P1_to0()  // 各プレイヤーの体力ハートを0にする
    {
        Heart1_P1.SetActive(false);
        Heart2_P1.SetActive(false);
        Heart3_P1.SetActive(false);
        Heart4_P1.SetActive(false);
        Heart5_P1.SetActive(false);
        Heart6_P1.SetActive(false);
        Heart7_P1.SetActive(false);
        Heart8_P1.SetActive(false);
    }
    #endregion


    #region// 体力 ハートの調整_P2
    public void Set_LifeHeart_P2()  // プレイヤーP2の体力ハートをセットする
    {
        switch (Life_Player2)
        {
            case 1: //
                Set_LifeHeart_P2_to1();
                break;

            case 2: //
                Set_LifeHeart_P2_to2();
                break;

            case 3: //
                Set_LifeHeart_P2_to3();
                break;

            case 4: //
                Set_LifeHeart_P2_to4();
                break;

            case 5: //
                Set_LifeHeart_P2_to5();
                break;

            case 6: //
                Set_LifeHeart_P2_to6();
                break;

            case 7: //
                Set_LifeHeart_P2_to7();
                break;

            case 8: //
                Set_LifeHeart_P2_to8();
                break;

            case 0: //
                Set_LifeHeart_P2_to0();
                break;

            default:
                // その他処理
                break;
        }
    }


    [PunRPC]
    public void Set_LifeHeart_P2_to1()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(false);
        Heart3_P2.SetActive(false);
        Heart4_P2.SetActive(false);
        Heart5_P2.SetActive(false);
        Heart6_P2.SetActive(false);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to2()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(false);
        Heart4_P2.SetActive(false);
        Heart5_P2.SetActive(false);
        Heart6_P2.SetActive(false);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to3()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(true);
        Heart4_P2.SetActive(false);
        Heart5_P2.SetActive(false);
        Heart6_P2.SetActive(false);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to4()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(true);
        Heart4_P2.SetActive(true);
        Heart5_P2.SetActive(false);
        Heart6_P2.SetActive(false);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to5()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(true);
        Heart4_P2.SetActive(true);
        Heart5_P2.SetActive(true);
        Heart6_P2.SetActive(false);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to6()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(true);
        Heart4_P2.SetActive(true);
        Heart5_P2.SetActive(true);
        Heart6_P2.SetActive(true);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to7()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(true);
        Heart4_P2.SetActive(true);
        Heart5_P2.SetActive(true);
        Heart6_P2.SetActive(true);
        Heart7_P2.SetActive(true);
        Heart8_P2.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to8()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P2.SetActive(true);
        Heart2_P2.SetActive(true);
        Heart3_P2.SetActive(true);
        Heart4_P2.SetActive(true);
        Heart5_P2.SetActive(true);
        Heart6_P2.SetActive(true);
        Heart7_P2.SetActive(true);
        Heart8_P2.SetActive(true);
    }

    [PunRPC]
    public void Set_LifeHeart_P2_to0()  // 各プレイヤーの体力ハートを0にする
    {
        Heart1_P2.SetActive(false);
        Heart2_P2.SetActive(false);
        Heart3_P2.SetActive(false);
        Heart4_P2.SetActive(false);
        Heart5_P2.SetActive(false);
        Heart6_P2.SetActive(false);
        Heart7_P2.SetActive(false);
        Heart8_P2.SetActive(false);
    }
    #endregion


    #region// 体力 ハートの調整_P3
    public void Set_LifeHeart_P3()  // プレイヤーP3の体力ハートをセットする
    {
        switch (Life_Player3)
        {
            case 1: //
                Set_LifeHeart_P3_to1();
                break;

            case 2: //
                Set_LifeHeart_P3_to2();
                break;

            case 3: //
                Set_LifeHeart_P3_to3();
                break;

            case 4: //
                Set_LifeHeart_P3_to4();
                break;

            case 5: //
                Set_LifeHeart_P3_to5();
                break;

            case 6: //
                Set_LifeHeart_P3_to6();
                break;

            case 7: //
                Set_LifeHeart_P3_to7();
                break;

            case 8: //
                Set_LifeHeart_P3_to8();
                break;

            case 0: //
                Set_LifeHeart_P3_to0();
                break;

            default:
                // その他処理
                break;
        }
    }


    [PunRPC]
    public void Set_LifeHeart_P3_to1()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(false);
        Heart3_P3.SetActive(false);
        Heart4_P3.SetActive(false);
        Heart5_P3.SetActive(false);
        Heart6_P3.SetActive(false);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to2()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(false);
        Heart4_P3.SetActive(false);
        Heart5_P3.SetActive(false);
        Heart6_P3.SetActive(false);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to3()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(true);
        Heart4_P3.SetActive(false);
        Heart5_P3.SetActive(false);
        Heart6_P3.SetActive(false);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to4()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(true);
        Heart4_P3.SetActive(true);
        Heart5_P3.SetActive(false);
        Heart6_P3.SetActive(false);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to5()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(true);
        Heart4_P3.SetActive(true);
        Heart5_P3.SetActive(true);
        Heart6_P3.SetActive(false);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to6()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(true);
        Heart4_P3.SetActive(true);
        Heart5_P3.SetActive(true);
        Heart6_P3.SetActive(true);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to7()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(true);
        Heart4_P3.SetActive(true);
        Heart5_P3.SetActive(true);
        Heart6_P3.SetActive(true);
        Heart7_P3.SetActive(true);
        Heart8_P3.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to8()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P3.SetActive(true);
        Heart2_P3.SetActive(true);
        Heart3_P3.SetActive(true);
        Heart4_P3.SetActive(true);
        Heart5_P3.SetActive(true);
        Heart6_P3.SetActive(true);
        Heart7_P3.SetActive(true);
        Heart8_P3.SetActive(true);
    }

    [PunRPC]
    public void Set_LifeHeart_P3_to0()  // 各プレイヤーの体力ハートを0にする
    {
        Heart1_P3.SetActive(false);
        Heart2_P3.SetActive(false);
        Heart3_P3.SetActive(false);
        Heart4_P3.SetActive(false);
        Heart5_P3.SetActive(false);
        Heart6_P3.SetActive(false);
        Heart7_P3.SetActive(false);
        Heart8_P3.SetActive(false);
    }
    #endregion


    #region// 体力 ハートの調整_P4
    public void Set_LifeHeart_P4()  // プレイヤーP4の体力ハートをセットする
    {
        switch (Life_Player4)
        {
            case 1: //
                Set_LifeHeart_P4_to1();
                break;

            case 2: //
                Set_LifeHeart_P4_to2();
                break;

            case 3: //
                Set_LifeHeart_P4_to3();
                break;

            case 4: //
                Set_LifeHeart_P4_to4();
                break;

            case 5: //
                Set_LifeHeart_P4_to5();
                break;

            case 6: //
                Set_LifeHeart_P4_to6();
                break;

            case 7: //
                Set_LifeHeart_P4_to7();
                break;

            case 8: //
                Set_LifeHeart_P4_to8();
                break;

            case 0: //
                Set_LifeHeart_P4_to0();
                break;

            default:
                // その他処理
                break;
        }
    }


    [PunRPC]
    public void Set_LifeHeart_P4_to1()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(false);
        Heart3_P4.SetActive(false);
        Heart4_P4.SetActive(false);
        Heart5_P4.SetActive(false);
        Heart6_P4.SetActive(false);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to2()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(false);
        Heart4_P4.SetActive(false);
        Heart5_P4.SetActive(false);
        Heart6_P4.SetActive(false);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to3()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(true);
        Heart4_P4.SetActive(false);
        Heart5_P4.SetActive(false);
        Heart6_P4.SetActive(false);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to4()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(true);
        Heart4_P4.SetActive(true);
        Heart5_P4.SetActive(false);
        Heart6_P4.SetActive(false);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to5()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(true);
        Heart4_P4.SetActive(true);
        Heart5_P4.SetActive(true);
        Heart6_P4.SetActive(false);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to6()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(true);
        Heart4_P4.SetActive(true);
        Heart5_P4.SetActive(true);
        Heart6_P4.SetActive(true);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to7()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(true);
        Heart4_P4.SetActive(true);
        Heart5_P4.SetActive(true);
        Heart6_P4.SetActive(true);
        Heart7_P4.SetActive(true);
        Heart8_P4.SetActive(false);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to8()  // 各プレイヤーの体力ハートをセットする
    {
        Heart1_P4.SetActive(true);
        Heart2_P4.SetActive(true);
        Heart3_P4.SetActive(true);
        Heart4_P4.SetActive(true);
        Heart5_P4.SetActive(true);
        Heart6_P4.SetActive(true);
        Heart7_P4.SetActive(true);
        Heart8_P4.SetActive(true);
    }

    [PunRPC]
    public void Set_LifeHeart_P4_to0()  // 各プレイヤーの体力ハートを0にする
    {
        Heart1_P4.SetActive(false);
        Heart2_P4.SetActive(false);
        Heart3_P4.SetActive(false);
        Heart4_P4.SetActive(false);
        Heart5_P4.SetActive(false);
        Heart6_P4.SetActive(false);
        Heart7_P4.SetActive(false);
        Heart8_P4.SetActive(false);
    }
    #endregion

    public void Check_DamageUmu()  // ダメージの有無を確認する
    {
        if(int_calculation_Life < 0)      // int_calculation_Life がマイナスであれば
        {
            Direction_Heart_Damage();  // 被ダメージ時、ハートを白く点滅させる
        }
        Set_Zero_calculation_Life();      // int_calculation_Life +-0
    }

    public void Direction_Heart_Damage()  // 被ダメージ時、ハートを白く点滅させる
    {
        if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
        {
            photonView.RPC("PosX_Koshin_Player1", RpcTarget.All);
            Direction_Heart_Damage_P1();
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
        {
            photonView.RPC("PosX_Koshin_Player2", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
        {
            photonView.RPC("PosX_Koshin_Player3", RpcTarget.All);
        }

        else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
        {
            photonView.RPC("PosX_Koshin_Player4", RpcTarget.All);
        }
    }

    public void Direction_Heart_Damage_P1()  // 被ダメージ時、ハートを白く点滅させる
    {
        //Debug.Log("被ダメージ時、ハートを白く点滅させる");
        AppearHeart_Damage_P1();

        var sequence = DOTween.Sequence();
        sequence.InsertCallback(0.2f, () => CloseHeart_Damage_P1());

        var sequence2 = DOTween.Sequence();
        sequence2.InsertCallback(0.4f, () => AppearHeart_Damage_P1());

        var sequence3 = DOTween.Sequence();
        sequence3.InsertCallback(0.6f, () => CloseHeart_Damage_P1());

        var sequence4 = DOTween.Sequence();
        sequence4.InsertCallback(0.8f, () => AppearHeart_Damage_P1());

        var sequence5 = DOTween.Sequence();
        sequence5.InsertCallback(1.0f, () => CloseHeart_Damage_P1());
    }

    public void AppearHeart_Damage_P1()
    {
        Heart_Damage_P1.SetActive(true);       
    }

    public void CloseHeart_Damage_P1()
    {
        Heart_Damage_P1.SetActive(false);        
    }

    #region// LifeZERO（体力ゼロの時の処理＆回復）

    public void Check_LifeZERO()  // 体力がゼロになっているか確認する → セロなら白旗ALL
    {
        //Debug.Log("Check_LifeZERO スタート");
        if (Shiai_Kaishi)        // 試合中であれば
        {
            //Debug.Log("試合中です");
            if (Life_MyPlayer <= 0)        // 体力がゼロになっているなら
            {
                LoseTurns();       // 体力がゼロになっているため、ジャンケンカードを白旗のみにします（一回休み）
            }
        }
    }

    public void LoseTurns()       // 体力がゼロになっているため、ジャンケンカードを白旗のみにします（一回休み）
    {
        //Debug.Log("LoseTurns スタート");

        PlayerSC.anim.SetBool("down", true);
        //Debug.Log("anim down スタート");

        ShuffleCardsMSC.SetAll_WFlagCard();    // すべて白旗カード でセットします（このターンは負け確定）  

        //Debug.Log("SetAll_WFlagCard スタート");

        if (Level_MyHealing <= 0)
        {
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.5f, () => AppearPanel_Kizetsu());
        }       
    }

    public void Check_IamHealed()  // 完治して体力が全快したか確認（一定値で回復）
    {
        if (Shiai_Kaishi)        // 試合中であれば
        {
            if (Life_MyPlayer <= 0)        // 体力がゼロになっているなら
            {
                PlayerSC.anim.SetBool("down", true);
                int HealingPoint = UnityEngine.Random.Range(3, 10);  // 治療レベルにプラスする値（治療の進行具合）
                Level_MyHealing = Level_MyHealing + HealingPoint;
                if (Level_MyHealing >= 10)      // 治療レベルが10に達しました！
                {
                    // 自分の体力を8にする（全快しました！）
                    if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
                    {
                        Life_MyPlayer = int_Default_Life;  // MAX ＝ 8
                        Life_Player1 = Life_MyPlayer;
                    }

                    else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
                    {
                        Life_MyPlayer = int_Default_Life;
                        Life_Player2 = Life_MyPlayer;
                    }

                    else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
                    {
                        Life_MyPlayer = int_Default_Life;
                        Life_Player3 = Life_MyPlayer;
                    }

                    else if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
                    {
                        Life_MyPlayer = int_Default_Life;
                        Life_Player4 = Life_MyPlayer;
                    }
                    Update_Life_Players();          // 各プレイヤーのLifeを同期します
                    Level_MyHealing = 0;
                    PlayerSC.anim.SetBool("down", false);
                    BGM_SE_MSC.cure_SE();  // ぴゅいーん（回復音）
                    Text_Katakori_cure.text = "体力が回復しました";
                    ClosePanel_Kizetsu();      // 気絶パネルを非表示にする

                    Katakori_Mark.SetActive(false);   //非表示にする
                    Katakori_to_SetWFlag = false;    // 肩こりが治癒しました（フラグOFF）
                    HariQ_Button.SetActive(false);    //非表示にする
                    Katakori_hajimari_Flg = true;

                    var sequence = DOTween.Sequence();
                    sequence.InsertCallback(4f, () => IamHealed2());
                }
            }
        }
    }

    public void IamHealed2()  // 体力が回復しました2
    {
        Text_Katakori_cure.text = "";
    }

    public void AppearPanel_Kizetsu()
    {
        Panel_Kizetsu.SetActive(true);        
    }

    public void ClosePanel_Kizetsu()
    {
        Panel_Kizetsu.SetActive(false);        
    }
    #endregion

    #endregion

    #endregion

    // End

}