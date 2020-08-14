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
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SelectJanken : MonoBehaviour, IPunObservable
{
    [SerializeField]
    public int count_a = 1;

    public Sprite sprite_Gu;
    public Sprite sprite_Choki;
    public Sprite sprite_Pa;

    public Image te1_1;
    public Image te1_2;
    public Image te1_3;
    public Image te1_4;
    public Image te1_5;

    public Text JankenTe_TextP1_1;
    public Text JankenTe_TextP1_2;
    public Text JankenTe_TextP1_3;
    public Text JankenTe_TextP1_4;
    public Text JankenTe_TextP1_5;

    public Text JankenTe_TextP2_1;
    public Text JankenTe_TextP2_2;
    public Text JankenTe_TextP2_3;
    public Text JankenTe_TextP2_4;
    public Text JankenTe_TextP2_5;

    public Text JankenTe_TextP3_1;
    public Text JankenTe_TextP3_2;
    public Text JankenTe_TextP3_3;
    public Text JankenTe_TextP3_4;
    public Text JankenTe_TextP3_5;

    public Text JankenTe_TextP4_1;
    public Text JankenTe_TextP4_2;
    public Text JankenTe_TextP4_3;
    public Text JankenTe_TextP4_4;
    public Text JankenTe_TextP4_5;

    public Text MyNumTe1_1;
    public Text MyNumTe1_2;
    public Text MyNumTe1_3;
    public Text MyNumTe1_4;
    public Text MyNumTe1_5;

    public int ReMyJanken_Te1 = -1;
    public int ReMyJanken_Te2 = -1;
    public int ReMyJanken_Te3 = -1;
    public int ReMyJanken_Te4 = -1;
    public int ReMyJanken_Te5 = -1;

    public int _Player1_Te1 = -1;
    public int _Player1_Te2 = -1;
    public int _Player1_Te3 = -1;
    public int _Player1_Te4 = -1;
    public int _Player1_Te5 = -1;

    public int _Player2_Te1 = -1;
    public int _Player2_Te2 = -1;
    public int _Player2_Te3 = -1;
    public int _Player2_Te4 = -1;
    public int _Player2_Te5 = -1;

    public int _Player3_Te1 = -1;
    public int _Player3_Te2 = -1;
    public int _Player3_Te3 = -1;
    public int _Player3_Te4 = -1;
    public int _Player3_Te5 = -1;

    public int _Player4_Te1 = -1;
    public int _Player4_Te2 = -1;
    public int _Player4_Te3 = -1;
    public int _Player4_Te4 = -1;
    public int _Player4_Te5 = -1;


    public int Player1_Te1 = -1;
    public int Player1_Te2 = -1;
    public int Player1_Te3 = -1;
    public int Player1_Te4 = -1;
    public int Player1_Te5 = -1;

    public int Player2_Te1 = -1;
    public int Player2_Te2 = -1;
    public int Player2_Te3 = -1;
    public int Player2_Te4 = -1;
    public int Player2_Te5 = -1;

    public int Player3_Te1 = -1;
    public int Player3_Te2 = -1;
    public int Player3_Te3 = -1;
    public int Player3_Te4 = -1;
    public int Player3_Te5 = -1;

    public int Player4_Te1 = -1;
    //    {
    //      get { return _Player4_Te1; }
    //    set { _Player4_Te1 = value; RequestOwner(); }
    //}
    public int Player4_Te2 = -1;
    public int Player4_Te3 = -1;
    public int Player4_Te4 = -1;
    public int Player4_Te5 = -1;

    public int receivePlayer1_Te1 = -5;
    public int receivePlayer1_Te2 = -5;
    public int receivePlayer1_Te3 = -5;
    public int receivePlayer1_Te4 = -5;
    public int receivePlayer1_Te5 = -5;

    public int receivePlayer2_Te1 = -5;
    public int receivePlayer2_Te2 = -5;
    public int receivePlayer2_Te3 = -5;
    public int receivePlayer2_Te4 = -5;
    public int receivePlayer2_Te5 = -5;

    public int receivePlayer3_Te1 = -5;
    public int receivePlayer3_Te2 = -5;
    public int receivePlayer3_Te3 = -5;
    public int receivePlayer3_Te4 = -5;
    public int receivePlayer3_Te5 = -5;

    public int receivePlayer4_Te1 = -5;
    public int receivePlayer4_Te2 = -5;
    public int receivePlayer4_Te3 = -5;
    public int receivePlayer4_Te4 = -5;
    public int receivePlayer4_Te5 = -5;

    public int MyJanken_Te1 = -1;
    public int MyJanken_Te2 = -1;
    public int MyJanken_Te3 = -1;
    public int MyJanken_Te4 = -1;
    public int MyJanken_Te5 = -1;

    public string PresentPlayerID;
    private PhotonView photonView = null;

    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム

    public GameObject TestRoomController;  //ヒエラルキー上のオブジェクト名
    TestRoomController TestRoomControllerSC;

    public GameObject PushTeBtnManager; //ヒエラルキー上のオブジェクト名
    PushTeBtn PushTeBtnMSC;
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

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
        Debug.Log("SelectJanken 出席確認1");
    }

    void Start()
    {
        //var customProperties = photonView.Owner.CustomProperties;
        Debug.Log("SelectJanken 出席確認2");
        /*
        Btn_A = GetComponent<Button>();
        Btn_B = GetComponent<Button>();
        Btn_C = GetComponent<Button>();
        Btn_D = GetComponent<Button>();
        Btn_E = GetComponent<Button>();
        */
        count_a = 1;
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
        TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
        //PushTeBtnMSC = PushTeBtnManager.GetComponent<PushTeBtn>();
        //PushTeBtnMSC = PushTeBtnManager.GetComponent<PushTeBtn>();
        ResetPlayerTeNum();
        // photonView.RPC("CheckPlayerTeNum", RpcTarget.All);
        //   if (PhotonNetwork.LocalPlayer.CustomProperties["Score"] is int score)
        //   {
        //       Debug.Log("score :" +score);
        //   }
        //   if (PhotonNetwork.LocalPlayer.CustomProperties["Te11"] is int te11)
        //   {
        //       Debug.Log("te11 : " +te11);
        //   }
        myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");
        // SelectJankenMSC = myPlayer.GetComponent<SelectJanken>();
        // PushTeBtnMSC.Push_Btn_A();
        //GetMyJankenNum();
        //ShuffleCardsMSC.Reset_All();
        //ToCanPush_All();
    }

    public void OnMouseDown()
    {
        // photonView.RPC("SelectJankenCard", RpcTarget.All);
    }

    public void MyNameIs(Player player)
    {
        Debug.Log("私の名前は 「" + player.NickName + " 」でござる");
    }

    public void MyPlayID()
    {
        // TestRoomControllerSC.PNameCheck();
        photonView.RPC("PlayerIDCheck", RpcTarget.All);
    }

    /// <summary>
    /// 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    /// </summary>
    /// <param name="mi">現プレイヤー名とプレイヤーID 取得、共有</param>
    [PunRPC]
    public void PlayerIDCheck(PhotonMessageInfo mi)
    {
        Debug.Log("[PunRPC] PlayerIDCheck");
        //string senderName = "anonymous";
        //string senderID = "2434";
        if (mi.Sender != null)
        {
            senderName = mi.Sender.NickName;
            senderID = mi.Sender.UserId;
            MyName = senderName;
            MyID = senderID;
        }
        /*
        Debug.Log("私の名前は 「" + PhotonNetwork.LocalPlayer.UserId + " 」でござる"); // accho1 で固定
        Debug.Log("私の名前は 「" + PhotonNetwork.LocalPlayer.NickName + " 」でござる"); // accho1 で固定
        Debug.Log("NickName  " + PhotonNetwork.NickName); //accho1 で固定
        Debug.Log("PlayerList  " + PhotonNetwork.PlayerList);  //accho1 で固定
        */
        Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("MyName  " + MyName);  // 今ボタン押した人
        Debug.Log("MyID  " + MyID);  // 今ボタン押した人
    }

    /*
    public void ToSelectJankenCard()
    {
        if (MyID == senderID)
        {
            Debug.Log("○○ MyID == senderID");
            photonView.RPC("SelectJankenCard", RpcTarget.All);
        }
        else
        {
            Debug.Log("×× MyID != senderID");
        }
    }
    */

    /*
[PunRPC]
public void SelectJankenCard()
{
    MyPlayID();
    Debug.Log("◎●◎◎●◎ [PunRPC] SelectJankenCard が起動しました ◎●◎◎●◎");

    Debug.Log("処理実施前確認");
    photonView.RPC("CheckPlayerTeNum", RpcTarget.All);
    PresentPlayerID = ""; //プレイヤーID初期化
    Debug.Log("PresentPlayerID 初期化確認 " + PresentPlayerID);
    Debug.Log("LocalPlayer.UserId : " + PhotonNetwork.LocalPlayer.UserId);

    Debug.Log(TestRoomControllerSC.PID1 + ": PID1");
    Debug.Log(TestRoomControllerSC.PID2 + ": PID2");
    Debug.Log(TestRoomControllerSC.PID3 + ": PID3");
    Debug.Log(TestRoomControllerSC.PID4 + ": PID4");

    Debug.Log("PresentPlayerIDをセット");
    PresentPlayerID = PhotonNetwork.LocalPlayer.UserId;
    Debug.Log("PresentPlayerID セット確認 " + PresentPlayerID);

    Debug.Log("senderName  " + senderName);
    Debug.Log("senderID  " + senderID);

    if (senderID == TestRoomControllerSC.PID1)
    {
        Debug.Log("現在プレイヤー1がボタン押したよ");
    }
    else if (senderID == TestRoomControllerSC.PID2)
    {
        Debug.Log("現在プレイヤー2がボタン押したよ");
    }
    else if (senderID == TestRoomControllerSC.PID3)
    {
        Debug.Log("現在プレイヤー3がボタン押したよ");
    }
    else if (senderID == TestRoomControllerSC.PID4)
    {
        Debug.Log("現在プレイヤー4がボタン押したよ");
    }
    else
    {
        Debug.Log("ID の条件、どれにも当てはまってない");
    }


    Debug.Log("RndCreateCard_C ： " + ShuffleCardsMSC.RndCreateCard_C);
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


    Debug.Log("処理実施後確認");
    photonView.RPC("CheckPlayerTeNum", RpcTarget.All);
    Debug.Log("●●●●●● [PunRPC] SelectJankenCard の起動終わり ●●●●●●");
}
*/

    /*
    public void SelectGu()
    {
        Debug.Log("今グー押したのは" + senderID);
        Debug.Log("今グー押したのは" + senderName);
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(0); //手をグーにセット
        Debug.Log("手をグーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 0);
        PlayerTeNumSet(0);
        Debug.Log("手をグーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectChoki()
    {
        Debug.Log("今チョキ押したのは" + senderID);
        Debug.Log("今チョキ押したのは" + senderName);
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(1); //手をチョキにセット
        Debug.Log("手をチョキにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 1);
        PlayerTeNumSet(1);
        Debug.Log("手をチョキにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectPa()
    {
        Debug.Log("今パー押したのは" + senderID);
        Debug.Log("今パー押したのは" + senderName);
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(2); //手をパーにセット
        Debug.Log("手をパーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All,2);
        PlayerTeNumSet(2);
        Debug.Log("手をパーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }
    */

    /*
    //[PunRPC]
    public void PlayerTeNumSet(int PTN)  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Debug.Log("************ ********** *********** **********");
        Debug.Log(PTN + ": PTN");
        Debug.Log(TestRoomControllerSC.PID1 + ": TestRoomControllerSC.PID1");
        Debug.Log(TestRoomControllerSC.PID2 + ": TestRoomControllerSC.PID2");
        Debug.Log(TestRoomControllerSC.PID3 + ": TestRoomControllerSC.PID3");
        Debug.Log(TestRoomControllerSC.PID4 + ": TestRoomControllerSC.PID4");
        Debug.Log(senderID + ": senderID");
        
        if (senderID == TestRoomControllerSC.PID1)
        { 
            Debug.Log("現在プレイヤー1がボタン押したよ");
            if (Player1_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Debug.Log("Player1_Te1 代入前" + Player1_Te1);
                Player1_Te1 = PTN;
                _Player1_Te1 = Player1_Te1;
                Debug.Log("Player1_Te1 代入後" + Player1_Te1);
                Debug.Log("プレイヤー1_1 手のセットOK");
            }
            else if (Player1_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te2 = PTN;
                _Player1_Te2 = Player1_Te2;
                Debug.Log("プレイヤー1_2 手のセットOK");
            }
            else if (Player1_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te3 = PTN;
                _Player1_Te3 = Player1_Te3;
                Debug.Log("プレイヤー1_3 手のセットOK");
            }
            else if (Player1_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te4 = PTN;
                _Player1_Te4 = Player1_Te4;
                Debug.Log("プレイヤー1_4 手のセットOK");
            }
            else if (Player1_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te5 = PTN;
                _Player1_Te5 = Player1_Te5;
                Debug.Log("プレイヤー1_5 手のセットOK");
            }
            else
            {
                Debug.Log("現在プレイヤー1の 5こすべて手が決まったよ");
            }
        }

        else if (senderID == TestRoomControllerSC.PID2)
        {
            Debug.Log("現在プレイヤー2がボタン押したよ");
            if (Player2_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te1 = PTN;
                _Player2_Te1 = Player2_Te1;
            }
            else if (Player2_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te2 = PTN;
                _Player2_Te2 = Player2_Te2;
            }
            else if (Player2_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te3 = PTN;
                _Player2_Te3 = Player2_Te3;
            }
            else if (Player2_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te4 = PTN;
                _Player2_Te4 = Player2_Te4;
            }
            else if (Player2_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te5 = PTN;
                _Player2_Te5 = Player2_Te5;
            }
            else
            {
                Debug.Log("現在プレイヤー2 の5こすべて手が決まったよ");
            }
        }

        else if (senderID == TestRoomControllerSC.PID3)
        {
            Debug.Log("現在プレイヤー3がボタン押したよ");
            if (Player3_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te1 = PTN;
                _Player3_Te1 = Player3_Te1;
            }
            else if (Player3_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te2 = PTN;
                _Player3_Te2 = Player3_Te2;
            }
            else if (Player3_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te3 = PTN;
                _Player3_Te3 = Player3_Te3;
            }
            else if (Player3_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te4 = PTN;
                _Player3_Te4 = Player3_Te4;
            }
            else if (Player3_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te5 = PTN;
                _Player3_Te5 = Player3_Te5;
            }
            else
            {
                Debug.Log("現在プレイヤー3 の5こすべて手が決まったよ");
            }
        }

        else if (senderID == TestRoomControllerSC.PID4)
        {
            Debug.Log("現在プレイヤー4がボタン押したよ");
            if (Player4_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te1 = PTN;
                _Player4_Te1 = Player4_Te1;
            }
            else if (Player4_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te2 = PTN;
                _Player4_Te2 = Player4_Te2;
            }
            else if (Player4_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te3 = PTN;
                _Player4_Te3 = Player4_Te3;
            }
            else if (Player4_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te4 = PTN;
                _Player4_Te4 = Player4_Te4;
            }
            else if (Player4_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te5 = PTN;
                _Player4_Te5 = Player4_Te5;
            }
            else
            {
                Debug.Log("現在プレイヤー4 の5こすべて手が決まったよ");
            }
        }

        else
        {
            Debug.Log("現在プレイヤー の条件、どれにも当てはまってない");
        }
    }
    */

    public void ToSharePlayerTeNum()
    {
        photonView.RPC("SharePlayerTeNum", RpcTarget.All);
    }

    [PunRPC]
    public void SharePlayerTeNum()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Debug.Log("************ データ共有 SharePlayerTeNum **********");
        Debug.Log(TestRoomControllerSC.PID1 + ": TestRoomControllerSC.PID1");
        Debug.Log(TestRoomControllerSC.PID2 + ": TestRoomControllerSC.PID2");
        Debug.Log(TestRoomControllerSC.PID3 + ": TestRoomControllerSC.PID3");
        Debug.Log(TestRoomControllerSC.PID4 + ": TestRoomControllerSC.PID4");
        Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("MyName  " + MyName);  // 今ボタン押した人
        Debug.Log("MyID  " + MyID);  // 今ボタン押した人


        if (senderID == TestRoomControllerSC.PID1)
        {
            Debug.Log("現在プレイヤー1がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            //PushTeBtnMSC.SharePlayerTeNum_Player1();
            /*
            Player1_Te1 = PushTeBtnMSC.MyJanken_Te1;
            Player1_Te2 = PushTeBtnMSC.MyJanken_Te2;
            Player1_Te3 = PushTeBtnMSC.MyJanken_Te3;
            Player1_Te4 = PushTeBtnMSC.MyJanken_Te4;
            Player1_Te5 = PushTeBtnMSC.MyJanken_Te5;
            */
            SharePlayerTeNum_Player1();
            JankenTe_TextP1_1.text = Player1_Te1.ToString();
            JankenTe_TextP1_2.text = Player1_Te2.ToString();
            JankenTe_TextP1_3.text = Player1_Te3.ToString();
            JankenTe_TextP1_4.text = Player1_Te4.ToString();
            JankenTe_TextP1_5.text = Player1_Te5.ToString();

        }

        if (senderID == TestRoomControllerSC.PID2)
        {
            Debug.Log("現在プレイヤー2がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            if (Player2_Te1 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te1 = _Player2_Te1;
            }
            if (Player2_Te2 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te2 = _Player2_Te2;
            }
            if (Player2_Te3 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te3 = _Player2_Te3;
            }
            if (Player2_Te4 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te4 = _Player2_Te4;
            }
            if (Player2_Te5 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te5 = _Player2_Te5;
            }
            else
            {
                Debug.Log("現在プレイヤー2 の5こすべて手が決まったよ");
            }
        }

        if (senderID == TestRoomControllerSC.PID3)
        {
            Debug.Log("現在プレイヤー3がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            if (Player3_Te1 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te1 = _Player3_Te1;
            }
            if (Player3_Te2 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te2 = _Player3_Te2;
            }
            if (Player3_Te3 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te3 = _Player3_Te3;
            }
            if (Player3_Te4 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te4 = _Player3_Te4;
            }
            if (Player3_Te5 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te5 = _Player3_Te5;
            }
            else
            {
                Debug.Log("現在プレイヤー3 の5こすべて手が決まったよ");
            }
        }

        if (senderID == TestRoomControllerSC.PID4)
        {
            Debug.Log("現在プレイヤー4がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            if (Player4_Te1 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te1 = _Player4_Te1;
            }
            if (Player4_Te2 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te2 = _Player4_Te2;
            }
            if (Player4_Te3 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te3 = _Player4_Te3;
            }
            if (Player4_Te4 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te4 = _Player4_Te4;
            }
            if (Player4_Te5 != -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te5 = _Player4_Te5;
            }
            else
            {
                Debug.Log("現在プレイヤー4 の5こすべて手が決まったよ");
            }
        }

        Debug.Log("************ データ共有 SharePlayerTeNum おわり **********");
    }


    public void ResetPlayerTeNum()
    {
        Player1_Te1 = -1;
        Player1_Te2 = -1;
        Player1_Te3 = -1;
        Player1_Te4 = -1;
        Player1_Te5 = -1;

        Player2_Te1 = -1;
        Player2_Te2 = -1;
        Player2_Te3 = -1;
        Player2_Te4 = -1;
        Player2_Te5 = -1;

        Player3_Te1 = -1;
        Player3_Te2 = -1;
        Player3_Te3 = -1;
        Player3_Te4 = -1;
        Player3_Te5 = -1;

        Player4_Te1 = -1;
        Player4_Te2 = -1;
        Player4_Te3 = -1;
        Player4_Te4 = -1;
        Player4_Te5 = -1;

        Debug.Log("PlayerTeNum を すべて「」リセットしました");
    }

    [PunRPC]
    public void CheckPlayerTeNum()
    {
        Debug.Log("************ CheckPlayerTeNum *********** **********");

        Debug.Log("***  Player1  ***********");
        Debug.Log("Player1_Te1 " + Player1_Te1);
        Debug.Log("Player1_Te2 " + Player1_Te2);
        Debug.Log("Player1_Te3 " + Player1_Te3);
        Debug.Log("Player1_Te4 " + Player1_Te4);
        Debug.Log("Player1_Te5 " + Player1_Te5);

        Debug.Log("***  Player2  ***********");
        Debug.Log("Player2_Te1 " + Player2_Te1);
        Debug.Log("Player2_Te2 " + Player2_Te2);
        Debug.Log("Player2_Te3 " + Player2_Te3);
        Debug.Log("Player2_Te4 " + Player2_Te4);
        Debug.Log("Player2_Te5 " + Player2_Te5);

        Debug.Log("***  Player3  ***********");
        Debug.Log("Player3_Te1 " + Player3_Te1);
        Debug.Log("Player3_Te2 " + Player3_Te2);
        Debug.Log("Player3_Te3 " + Player3_Te3);
        Debug.Log("Player3_Te4 " + Player3_Te4);
        Debug.Log("Player3_Te5 " + Player3_Te5);

        Debug.Log("***  Player4  ***********");
        Debug.Log("Player4_Te1 " + Player4_Te1);
        Debug.Log("Player4_Te2 " + Player4_Te2);
        Debug.Log("Player4_Te3 " + Player4_Te3);
        Debug.Log("Player4_Te4 " + Player4_Te4);
        Debug.Log("Player4_Te5 " + Player4_Te5);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
    // オーナーの場合
    if (stream.IsWriting)
    {
        stream.SendNext(Player1_Te1);
        stream.SendNext(Player2_Te1);
        if (TestRoomControllerSC.allPlayers.Length >= 3)
        {
            stream.SendNext(Player3_Te1);
        }
        if (TestRoomControllerSC.allPlayers.Length >= 4)
        {
            stream.SendNext(Player4_Te1);
        }
    }

    // オーナー以外の場合
    else
    {
        this.receivePlayer1_Te1 = (int)stream.ReceiveNext();
        SelectJankenMSC.Player1_Te1 = receivePlayer1_Te1;
        this.receivePlayer2_Te1 = (int)stream.ReceiveNext();
        SelectJankenMSC.Player2_Te1 = receivePlayer2_Te1;
        if (TestRoomControllerSC.allPlayers.Length >= 3)
        {
            this.receivePlayer3_Te1 = (int)stream.ReceiveNext();
            SelectJankenMSC.Player3_Te1 = receivePlayer3_Te1;
        }
        if (TestRoomControllerSC.allPlayers.Length >= 4)
        {
            this.receivePlayer4_Te1 = (int)stream.ReceiveNext();
            SelectJankenMSC.Player4_Te1 = receivePlayer4_Te1;
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

    public void Reset_Rireki_All()
    {
        te1_1.gameObject.GetComponent<Image>().sprite = null;
        te1_2.gameObject.GetComponent<Image>().sprite = null;
        te1_3.gameObject.GetComponent<Image>().sprite = null;
        te1_4.gameObject.GetComponent<Image>().sprite = null;
        te1_5.gameObject.GetComponent<Image>().sprite = null;
    }

    /*
    #region// じゃんけんボタン 押せるかどうかのフラグ

    public void Push_Btn_A() // ボタン押したよ
    {
        Btn_A.interactable = false;
    }

    public void ToCanPush_A() // ボタン押せるようにするよ
    {
        Btn_A.interactable = true;
    }

    public void Push_Btn_B() // ボタン押したよ
    {
        Btn_B.interactable = false;
    }

    public void ToCanPush_B() // ボタン押せるようにするよ
    {
        Btn_B.interactable = true;
    }

    public void Push_Btn_C() // ボタン押したよ
    {
        Btn_C.interactable = false;
    }

    public void ToCanPush_C() // ボタン押せるようにするよ
    {
        Btn_C.interactable = true;
    }

    public void Push_Btn_D() // ボタン押したよ
    {
        Btn_D.interactable = false;
    }

    public void ToCanPush_D() // ボタン押せるようにするよ
    {
        Btn_D.interactable = true;
    }

    public void Push_Btn_E() // ボタン押したよ
    {
        Btn_E.interactable = false;
    }

    public void ToCanPush_E() // ボタン押せるようにするよ
    {
        Btn_E.interactable = true;
    }

    #endregion
    */

    public void GetMyJankenNum()
    {
        ReMyJanken_Te4 = PushTeBtnMSC.MyJanken_Te1;
    }


    public void SharePlayerTeNum_Player1()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Player1_Te1 = MyJanken_Te1;
        Player1_Te2 = MyJanken_Te2;
        Player1_Te3 = MyJanken_Te3;
        Player1_Te4 = MyJanken_Te4;
        Player1_Te5 = MyJanken_Te5;
    }

    public void SelectGu()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTe1_1.text = "0";
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTe1_2.text = "0";
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTe1_3.text = "0";
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTe1_4.text = "0";
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTe1_5.text = "0";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(0); //手をグーにセット
        Debug.Log("手をグーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 0);
        PlayerTeNumSet(0);
        Debug.Log("手をグーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectChoki()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            //Text NumTe = MyNumTe1_1.GetComponent<Text>();
            //NumTe.text = "1";
            MyNumTe1_1.text = "1";
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            //MyNumTe1_2.gameObject.GetComponent<Text>().text = "-1";
            MyNumTe1_2.text = "1";
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTe1_3.text = "1";
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTe1_4.text = "1";
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTe1_5.text = "1";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(1); //手をチョキにセット
        Debug.Log("手をチョキにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 1);
        PlayerTeNumSet(1);
        Debug.Log("手をチョキにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectPa()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTe1_1.text = "2";
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTe1_2.text = "2";
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTe1_3.text = "2";
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTe1_4.text = "2";
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTe1_5.text = "2";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(2); //手をパーにセット
        Debug.Log("手をパーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All,2);
        PlayerTeNumSet(2);
        Debug.Log("手をパーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void PlayerTeNumSet(int PTN)  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Debug.Log("************ ********** *********** **********");
        Debug.Log(PTN + ": PTN");

        Debug.Log("現在プレイヤー1がボタン押したよ");
        if (MyJanken_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Debug.Log("MyJanken_Te1 代入前" + MyJanken_Te1);
            MyJanken_Te1 = PTN;
            Debug.Log("MyJanken_Te1 代入後" + MyJanken_Te1);
            Debug.Log("プレイヤー1_1 手のセットOK");
        }
        else if (MyJanken_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            MyJanken_Te2 = PTN;
            Debug.Log("プレイヤー1_2 手のセットOK");
        }
        else if (MyJanken_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            MyJanken_Te3 = PTN;
            Debug.Log("プレイヤー1_3 手のセットOK");
        }
        else if (MyJanken_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            MyJanken_Te4 = PTN;
            Debug.Log("プレイヤー1_4 手のセットOK");
        }
        else if (MyJanken_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            MyJanken_Te5 = PTN;
            Debug.Log("プレイヤー1_5 手のセットOK");
        }
        else
        {
            Debug.Log("現在プレイヤー1の 5こすべて手が決まったよ");
        }
    }


    #region// じゃんけんボタン 押した時の処理（フラグを処理済みにする）

    public void Push_Btn_A() // ボタン押したよ
    {
        if (CanPushBtn_A)
        {
            Debug.Log("RndCreateCard_A ： " + ShuffleCardsMSC.RndCreateCard_A);
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
    }

    public void Push_Btn_B() // ボタン押したよ
    {
        if (CanPushBtn_B)
        {
            Debug.Log("RndCreateCard_B ： " + ShuffleCardsMSC.RndCreateCard_B);
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
    }

    public void Push_Btn_C() // ボタン押したよ
    {
        if (CanPushBtn_C)
        {
            Debug.Log("RndCreateCard_C ： " + ShuffleCardsMSC.RndCreateCard_C);
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
    }

    public void Push_Btn_D() // ボタン押したよ
    {
        if (CanPushBtn_D)
        {
            Debug.Log("RndCreateCard_D ： " + ShuffleCardsMSC.RndCreateCard_D);
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
    }

    public void Push_Btn_E() // ボタン押したよ
    {
        if (CanPushBtn_E)
        {
            Debug.Log("RndCreateCard_E ： " + ShuffleCardsMSC.RndCreateCard_E);
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
    }

    #endregion


    #region// じゃんけんボタン ボタン押せるようにする(フラグのリセット）

    public void ToCanPush_All()
    {
        ToCanPush_A();
        ToCanPush_B();
        ToCanPush_C();
        ToCanPush_D();
        ToCanPush_E();
    }

    public void ToCanPush_A() // ボタン押せるようにするよ
    {
        Btn_A.interactable = true;
        CanPushBtn_A = true;
    }

    public void ToCanPush_B() // ボタン押せるようにするよ
    {
        Btn_B.interactable = true;
        CanPushBtn_B = true;
    }

    public void ToCanPush_C() // ボタン押せるようにするよ
    {
        Btn_C.interactable = true;
        CanPushBtn_C = true;
    }

    public void ToCanPush_D() // ボタン押せるようにするよ
    {
        Btn_D.interactable = true;
        CanPushBtn_D = true;
    }

    public void ToCanPush_E() // ボタン押せるようにするよ
    {
        Btn_E.interactable = true;
        CanPushBtn_E = true;
    }

    #endregion

    #region// じゃんけん手ナンバー リセット

    public void ResetTeNum_All()
    {
        count_a = 1;

        MyNumTe1_1.text = "-1";
        MyNumTe1_2.text = "-1";
        MyNumTe1_3.text = "-1";
        MyNumTe1_4.text = "-1";
        MyNumTe1_5.text = "-1";
    }
    #endregion

    // End

}