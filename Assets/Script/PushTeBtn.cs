using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using say;　// 対象のスクリプトの情報を取得
using BEFOOL.PhotonTest;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PushTeBtn : MonoBehaviour
{
    public Button Btn_A;
    public Button Btn_B;
    public Button Btn_C;
    public Button Btn_D;
    public Button Btn_E;

    public int count_a = 1;

    public Sprite sprite_Gu;
    public Sprite sprite_Choki;
    public Sprite sprite_Pa;

    public Image te1_1;
    public Image te1_2;
    public Image te1_3;
    public Image te1_4;
    public Image te1_5;

    public int _MyJanken_Te1 = -1;
    public int _MyJanken_Te2 = -1;
    public int _MyJanken_Te3 = -1;
    public int _MyJanken_Te4 = -1;
    public int _MyJanken_Te5 = -1;

    public int MyJanken_Te1 = -1;
    public int MyJanken_Te2 = -1;
    public int MyJanken_Te3 = -1;
    public int MyJanken_Te4 = -1;
    public int MyJanken_Te5 = -1;

    public int Player1_Te1 = -1;
    public int Player1_Te2 = -1;
    public int Player1_Te3 = -1;
    public int Player1_Te4 = -1;
    public int Player1_Te5 = -1;
    /*
    public GameObject NumTe1_1 = null; // Textオブジェクト
    public GameObject NumTe1_2 = null; // Textオブジェクト
    public GameObject NumTe1_3 = null; // Textオブジェクト
    public GameObject NumTe1_4 = null; // Textオブジェクト
    public GameObject NumTe1_5 = null; // Textオブジェクト
    */
    public Text NumTe1_1;
    public Text NumTe1_2;
    public Text NumTe1_3;
    public Text NumTe1_4;
    public Text NumTe1_5;

    public bool CanPushBtn_A = true;
    public bool CanPushBtn_B = true;
    public bool CanPushBtn_C = true;
    public bool CanPushBtn_D = true;
    public bool CanPushBtn_E = true;

    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム

    public SelectJanken SelectJankenMSC; //スクリプト名 + このページ上でのニックネーム


    // Start is called before the first frame update
    void Start()
    {
        /*
        Btn_A = GetComponent<Button>();
        Btn_B = GetComponent<Button>();
        Btn_C = GetComponent<Button>();
        Btn_D = GetComponent<Button>();
        Btn_E = GetComponent<Button>();
        
        ToCanPush_A();
        ToCanPush_B();
        ToCanPush_C();
        ToCanPush_D();
        ToCanPush_E();
        */
        ToCanPush_All();

        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
        //SelectJankenMSC.MyPlayID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SharePlayerTeNum_Player1()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        SelectJankenMSC.Player1_Te1 = MyJanken_Te1;
        SelectJankenMSC.Player1_Te2 = MyJanken_Te2;
        SelectJankenMSC.Player1_Te3 = MyJanken_Te3;
        SelectJankenMSC.Player1_Te4 = MyJanken_Te4;
        SelectJankenMSC.Player1_Te5 = MyJanken_Te5;

    }


    public void PushJankenCard()
    {
        Debug.Log("処理実施前確認");

        Debug.Log("処理実施後確認");
    }


    public void SelectGu()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            te1_1.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            NumTe1_1.text = "0";
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            NumTe1_2.text = "0";
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            NumTe1_3.text = "0";
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            NumTe1_4.text = "0";
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            NumTe1_5.text = "0";
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
            //Text NumTe = NumTe1_1.GetComponent<Text>();
            //NumTe.text = "1";
            NumTe1_1.text = "1";
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            //NumTe1_2.gameObject.GetComponent<Text>().text = "-1";
            NumTe1_2.text = "1";
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            NumTe1_3.text = "1";
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            NumTe1_4.text = "1";
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            NumTe1_5.text = "1";
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
            NumTe1_1.text = "2";
        }
        else if (count_a == 2)
        {
            te1_2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            NumTe1_2.text = "2";
        }
        else if (count_a == 3)
        {
            te1_3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            NumTe1_3.text = "2";
        }
        else if (count_a == 4)
        {
            te1_4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            NumTe1_4.text = "2";
        }
        else if (count_a == 5)
        {
            te1_5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            NumTe1_5.text = "2";
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
                _MyJanken_Te1 = MyJanken_Te1;
                Debug.Log("MyJanken_Te1 代入後" + MyJanken_Te1);
                Debug.Log("プレイヤー1_1 手のセットOK");
            }
            else if (MyJanken_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                MyJanken_Te2 = PTN;
                _MyJanken_Te2 = MyJanken_Te2;
                Debug.Log("プレイヤー1_2 手のセットOK");
            }
            else if (MyJanken_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                MyJanken_Te3 = PTN;
                _MyJanken_Te3 = MyJanken_Te3;
                Debug.Log("プレイヤー1_3 手のセットOK");
            }
            else if (MyJanken_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                MyJanken_Te4 = PTN;
                _MyJanken_Te4 = MyJanken_Te4;
                Debug.Log("プレイヤー1_4 手のセットOK");
            }
            else if (MyJanken_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
            {
                MyJanken_Te5 = PTN;
                _MyJanken_Te5 = MyJanken_Te5;
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

        NumTe1_1.text = "-1";
        NumTe1_2.text = "-1";
        NumTe1_3.text = "-1";
        NumTe1_4.text = "-1";
        NumTe1_5.text = "-1";    
    }
    #endregion

    // End

}
