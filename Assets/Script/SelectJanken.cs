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

public class SelectJanken : MonoBehaviourPunCallbacks
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

    [HideInInspector]
    public int Player1_Te1 = -1;
    [HideInInspector]
    public int Player1_Te2 = -1;
    [HideInInspector]
    public int Player1_Te3 = -1;
    [HideInInspector]
    public int Player1_Te4 = -1;
    [HideInInspector]
    public int Player1_Te5 = -1;

    [HideInInspector]
    public int Player2_Te1 = -1;
    [HideInInspector]
    public int Player2_Te2 = -1;
    [HideInInspector]
    public int Player2_Te3 = -1;
    [HideInInspector]
    public int Player2_Te4 = -1;
    [HideInInspector]
    public int Player2_Te5 = -1;

    [HideInInspector]
    public int Player3_Te1 = -1;
    [HideInInspector]
    public int Player3_Te2 = -1;
    [HideInInspector]
    public int Player3_Te3 = -1;
    [HideInInspector]
    public int Player3_Te4 = -1;
    [HideInInspector]
    public int Player3_Te5 = -1;

    [HideInInspector]
    public int Player4_Te1 = -1;
    [HideInInspector]
    public int Player4_Te2 = -1;
    [HideInInspector]
    public int Player4_Te3 = -1;
    [HideInInspector]
    public int Player4_Te4 = -1;
    [HideInInspector]
    public int Player4_Te5 = -1;

    public string PresentPlayerID;

    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム

    public GameObject TestRoomController;  //ヒエラルキー上のオブジェクト名
    TestRoomController TestRoomControllerSC;

    void Start()
    {
        Debug.Log("SelectJanken 出席確認");
        count_a = 1;
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
        TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
        ResetPlayerTeNum();
        CheckPlayerTeNum();
    }

    public void SelectJankenCard()
    {
        CheckPlayerTeNum();
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

        if(PresentPlayerID == TestRoomControllerSC.PID1)
        {
            Debug.Log("現在プレイヤー1がボタン押したよ");
        }
        else if (PresentPlayerID == TestRoomControllerSC.PID2)
        {
            Debug.Log("現在プレイヤー2がボタン押したよ");
        }
        else if (PresentPlayerID == TestRoomControllerSC.PID3)
        {
            Debug.Log("現在プレイヤー3がボタン押したよ");
        }
        else if (PresentPlayerID == TestRoomControllerSC.PID4)
        {
            Debug.Log("現在プレイヤー4がボタン押したよ");
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

        CheckPlayerTeNum();
    }

    public void SelectGu()
    {
        Debug.Log("今グー押したのは" + PhotonNetwork.NickName);
        Debug.Log("今グー押したのは" + PhotonNetwork.PlayerList);
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
        PlayerTeNumSet(0); //手をグーにセット

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectChoki()
    {
        Debug.Log("今チョキ押したのは" + PhotonNetwork.NickName);
        Debug.Log("今チョキ押したのは" + PhotonNetwork.PlayerList);
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
        PlayerTeNumSet(1); //手をチョキにセット

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectPa()
    {
        Debug.Log("今パー押したのは" + PhotonNetwork.NickName);
        Debug.Log("今パー押したのは" + PhotonNetwork.PlayerList);
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
        PlayerTeNumSet(2); //手をパーにセット

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void PlayerTeNumSet(int PTN)  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Debug.Log(PTN + ": PTN");
        if (PresentPlayerID == TestRoomControllerSC.PID1)
        {
            Debug.Log("現在プレイヤー1がボタン押したよ");
            if (Player1_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Debug.Log("Player1_Te1 " + Player1_Te1);
                Player1_Te1 = PTN;
                Debug.Log("Player1_Te1 " + Player1_Te1);
                Debug.Log("プレイヤー1_1 手のセットOK");
            }
            else if (Player1_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te2 = PTN;
                Debug.Log("プレイヤー1_2 手のセットOK");
            }
            else if (Player1_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te3 = PTN;
                Debug.Log("プレイヤー1_3 手のセットOK");
            }
            else if (Player1_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te4 = PTN;
                Debug.Log("プレイヤー1_4 手のセットOK");
            }
            else if (Player1_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player1_Te5 = PTN;
                Debug.Log("プレイヤー1_5 手のセットOK");
            }
            else
            {
                Debug.Log("現在プレイヤー1の 5こすべて手が決まったよ");
            }
        }
    
        else if (PresentPlayerID == TestRoomControllerSC.PID2)
        {
            Debug.Log("現在プレイヤー2がボタン押したよ");
            if (Player2_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te1 = PTN;
            }
            else if (Player2_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te2 = PTN;
            }
            else if (Player2_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te3 = PTN;
            }
            else if (Player2_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te4 = PTN;
            }
            else if (Player2_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player2_Te5 = PTN;
            }
            else
            {
                Debug.Log("現在プレイヤー2 の5こすべて手が決まったよ");
            }
        }

        else if (PresentPlayerID == TestRoomControllerSC.PID3)
        {
            Debug.Log("現在プレイヤー3がボタン押したよ");
            if (Player3_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te1 = PTN;
            }
            else if (Player3_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te2 = PTN;
            }
            else if (Player3_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te3 = PTN;
            }
            else if (Player3_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te4 = PTN;
            }
            else if (Player3_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player3_Te5 = PTN;
            }
            else
            {
                Debug.Log("現在プレイヤー3 の5こすべて手が決まったよ");
            }
        }

        else if (PresentPlayerID == TestRoomControllerSC.PID4)
        {
            Debug.Log("現在プレイヤー4がボタン押したよ");
            if (Player4_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te1 = PTN;
            }
            else if (Player4_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te2 = PTN;
            }
            else if (Player4_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te3 = PTN;
            }
            else if (Player4_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te4 = PTN;
            }
            else if (Player4_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                Player4_Te5 = PTN;
            }
            else
            {
                Debug.Log("現在プレイヤー4 の5こすべて手が決まったよ");
            }
        }
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

        Debug.Log("PlayerTeNum をリセットしました");
    }

    public void CheckPlayerTeNum()
    {
        Debug.Log("***				Player1	***********" );
        Debug.Log("Player1_Te1 " + Player1_Te1);
        Debug.Log("Player1_Te2 " + Player1_Te2);
        Debug.Log("Player1_Te3 " + Player1_Te3);
        Debug.Log("Player1_Te4 " + Player1_Te4);
        Debug.Log("Player1_Te5 " + Player1_Te5);

        Debug.Log("***				Player2	***********" );
        Debug.Log("Player2_Te1 " + Player2_Te1);
        Debug.Log("Player2_Te2 " + Player2_Te2);
        Debug.Log("Player2_Te3 " + Player2_Te3);
        Debug.Log("Player2_Te4 " + Player2_Te4);
        Debug.Log("Player2_Te5 " + Player2_Te5);

        Debug.Log("***				Player3	***********" );
        Debug.Log("Player3_Te1 " + Player3_Te1);
        Debug.Log("Player3_Te2 " + Player3_Te2);
        Debug.Log("Player3_Te3 " + Player3_Te3);
        Debug.Log("Player3_Te4 " + Player3_Te4);
        Debug.Log("Player3_Te5 " + Player3_Te5);

        Debug.Log("***				Player4	***********" );
        Debug.Log("Player4_Te1 " + Player4_Te1);
        Debug.Log("Player4_Te2 " + Player4_Te2);
        Debug.Log("Player4_Te3 " + Player4_Te3);
        Debug.Log("Player4_Te4 " + Player4_Te4);
        Debug.Log("Player4_Te5 " + Player4_Te5);
    }
    // End
}