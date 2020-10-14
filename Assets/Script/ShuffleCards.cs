using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using BEFOOL.PhotonTest;
using Random = UnityEngine.Random;

namespace say
{
    public class ShuffleCards : MonoBehaviourPunCallbacks
    {

        public int count_selected = 1;

        public Sprite sprite_Gu;
        public Sprite sprite_Choki;
        public Sprite sprite_Pa;
        public Sprite sprite_King;
        public Sprite sprite_Dorei;

        public Image Te_A;
        public Image Te_B;
        public Image Te_C;
        public Image Te_D;
        public Image Te_E;

        public int RndCreateCard_A = 0;
        public int RndCreateCard_B = 0;
        public int RndCreateCard_C = 0;
        public int RndCreateCard_D = 0;
        public int RndCreateCard_E = 0;
        public GameObject Button_A;
        public GameObject Button_B;
        public GameObject Button_C;
        public GameObject Button_D;
        public GameObject Button_E;
        public GameObject KetteiBtn;  // ジャンケン手 決定ボタン

        public GameObject JankenCards_Panel;
        public GameObject MyJankenPanel;
        public GameObject Wait_JankenPanel;

        public bool isSet_A = false;
        public bool isSet_B = false;
        public bool isSet_C = false;
        public bool isSet_D = false;
        public bool isSet_E = false;

        public GameObject SelectJankenManager; //ヒエラルキー上のオブジェクト名
        SelectJanken SelectJankenMSC;//スクリプト名 + このページ上でのニックネーム

        public GameObject TestRoomController;  //ヒエラルキー上のオブジェクト名
        TestRoomController TestRoomControllerSC;

        public int FirstChancePush_Flg = 0;  // 最初に押した人に判定させるフラグ ：0ならばまだ判定前なのでできる
        int KingDorei_SetChance = 0;         // 0～3  ローカル   王さま-どれい-セットチャンス
        public int KingChancePlayer = 0;     // 0～4  パブリック 王さまチャンス → どのプレイヤーがカードをセットするか
        public int DoreiChancePlayer = 0;    // 0～4  パブリック どれいチャンス → どのプレイヤーがカードをセットするか
        int RndSet_CardPos_King;             // A～E どの位置にカードをセットするか
        int RndSet_CardPos_Dorei;            // A～E どの位置にカードをセットするか

        private void Awake()
        {
            Debug.Log("ShuffleCards Awake 出席確認");
            ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
            SelectJankenMSC = SelectJankenManager.GetComponent<SelectJanken>();
            TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
        }

        //☆################☆################  Start  ################☆################☆

        void Start()
        {
            Debug.Log("ShuffleCards Start 出席確認");
            AppearButton_A();
            AppearButton_B();
            AppearButton_C();
            AppearButton_D();
            AppearButton_E();

            count_selected = 1;

            Reset_All();
            Set_All();
            ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
        }


        //####################################  Update  ###################################

        void Update()
        {


        }

        //####################################  other  ####################################

        #region// じゃんけんカード 手のセット
        /*
        public void Share_Done_FirstChancePush()  // 王さま-どれい-セットチャンス 判定したら 0→1 [ 共有する ]
        {
            photonView.RPC("Done_FirstChancePush", RpcTarget.All);
        }

        [PunRPC]
        public void Done_FirstChancePush()  // 王さま-どれい-セットチャンス 判定したら 0→1 にする
        {
            FirstChancePush_Flg = 1;
        }

        public void Share_Reset_FirstChancePush_Flg()  // 王さま-どれい-セットチャンス リセットして 1→0 にする [ 共有する ]
        {
            photonView.RPC("Reset_FirstChancePush_Flg", RpcTarget.All);
        }

        [PunRPC]
        public void Reset_FirstChancePush_Flg()  // 王さま-どれい-セットチャンス リセットして 1→0 にする
        {
            FirstChancePush_Flg = 0;
        }
        */

        public void Set_All()
        {
            KingChancePlayer = UnityEngine.Random.Range(1, 5);          // 1～4  パブリック 王さまチャンス → どのプレイヤーがカードをセットするか
            DoreiChancePlayer = UnityEngine.Random.Range(1, 5);
            int safe = 0;
            while (KingChancePlayer == DoreiChancePlayer || safe >20)   // ChancePlayer が被らないようにします
            {
                DoreiChancePlayer = UnityEngine.Random.Range(1, 5);
                safe++;
            }

            RndSet_CardPos_King = UnityEngine.Random.Range(1, 6);        // A～E どの位置にカードをセットするか
            RndSet_CardPos_Dorei = UnityEngine.Random.Range(1, 6);

            Debug.Log("参加人数でセットチャンス調整" + SelectJankenMSC.SankaNinzu);          
            if (FirstChancePush_Flg == 0)                                // 王さま-どれい-セットチャンス 判定まえ ならば
            {
                KingDorei_SetChance = UnityEngine.Random.Range(SelectJankenMSC.SankaNinzu*-1, 5);   // -3～4  ローカル   王さま-どれい-セットチャンス
                SelectJankenMSC.Share_Done_FirstChancePush();            // 王さま-どれい-セットチャンス 判定したら FirstChancePush_Flg を 0→1 [ 共有する ]
            }

            A_Set();
            B_Set();
            C_Set();
            D_Set();
            E_Set();

            Debug.Log("ここまでか 通常ジャンケン手のセット");
            Debug.Log("ここから 王さま-どれい カードのセット処理");

            Debug.Log("KingChancePlayer ： " + KingChancePlayer);
            Debug.Log("DoreiChancePlayer ： " + DoreiChancePlayer);
            Debug.Log("RndSet_CardPos_King ： " + RndSet_CardPos_King);
            Debug.Log("RndSet_CardPos_Dorei ： " + RndSet_CardPos_Dorei);
            Debug.Log("KingDorei_SetChance ： " + KingDorei_SetChance);

            if (KingDorei_SetChance <= 0)     // 王さまチャンス0、どれいチャンス0： 共に 0
            {
                Debug.Log("0 なので何もしません");
            }
            if (KingDorei_SetChance == 1)     // 王さまチャンス0、どれいチャンス1： 片方 1
            {
                Debug.Log("1 なので どれいチャンス1");
                Check_ChancePlayer_Dorei();   // どれいカード をセット するプレイヤーを確認します
            }
            if (KingDorei_SetChance == 2)     // 王さまチャンス1、どれいチャンス0： 片方 1
            {
                Debug.Log("2 なので 王さまチャンス1");
                Check_ChancePlayer_King();    // 王さまカード をセット するプレイヤーを確認します
            }
            if (KingDorei_SetChance >= 3)     // 王さまチャンス1、どれいチャンス1： 両方 1（ペア成立）
            {
                Debug.Log("3 なので 王さまチャンス1、どれいチャンス1： 両方 1");
                Check_ChancePlayer_Dorei();   // どれいカード をセット するプレイヤーを確認します
                Check_ChancePlayer_King();    // 王さまカード をセット するプレイヤーを確認します
            }
        }

        public void Check_ChancePlayer_King()  // 王さまカード をセット するプレイヤーを確認します
        {
            if (KingChancePlayer == 1)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
                {
                    Debug.Log("プレイヤー1 に 王さまカードセットします");
                    Set_KingCard();  // 王さまカード をセットします
                }
            }
            if (KingChancePlayer == 2)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
                {
                    Debug.Log("プレイヤー2 に 王さまカードセットします");
                    Set_KingCard();  // 王さまカード をセットします
                }
            }
            if (KingChancePlayer == 3)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
                {
                    Debug.Log("プレイヤー3 に 王さまカードセットします");
                    Set_KingCard();  // 王さまカード をセットします
                }
            }
            if (KingChancePlayer == 4)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
                {
                    Debug.Log("プレイヤー4 に 王さまカードセットします");
                    Set_KingCard();  // 王さまカード をセットします
                }
            }
        }

        public void Check_ChancePlayer_Dorei()   // どれいカード をセット するプレイヤーを確認します
        {
            if (DoreiChancePlayer == 1)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName1) // 自身がプレイヤー1 であるなら
                {
                    Debug.Log("プレイヤー1 に どれいカードセットします");
                    Set_DoreiCard();  // どれいカード をセットします
                }
            }
            if (DoreiChancePlayer == 2)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName2) // 自身がプレイヤー2 であるなら
                {
                    Debug.Log("プレイヤー2 に どれいカードセットします");
                    Set_DoreiCard();  // どれいカード をセットします
                }
            }
            if (DoreiChancePlayer == 3)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName3) // 自身がプレイヤー3 であるなら
                {
                    Debug.Log("プレイヤー3 に どれいカードセットします");
                    Set_DoreiCard();  // どれいカード をセットします
                }
            }
            if (DoreiChancePlayer == 4)
            {
                if (PhotonNetwork.NickName == TestRoomControllerSC.string_PName4) // 自身がプレイヤー4 であるなら
                {
                    Debug.Log("プレイヤー4 に どれいカードセットします");
                    Set_DoreiCard();  // どれいカード をセットします
                }
            }
        }

        public void  Set_DoreiCard()  // どれいカード をセットします  A,B,C,D,E
        {
            Debug.Log("どれいカード をセットします");
            if (RndSet_CardPos_Dorei == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
                RndCreateCard_A = 23;  // どれいの番号をセット
            }
            else if (RndSet_CardPos_Dorei == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
                RndCreateCard_B = 23;  // どれいの番号をセット
            }
            else if (RndSet_CardPos_Dorei == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
                RndCreateCard_C = 23;  // どれいの番号をセット
            }
            else if (RndSet_CardPos_Dorei == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
                RndCreateCard_D = 23;  // どれいの番号をセット
            }
            else if (RndSet_CardPos_Dorei == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_Dorei;
                RndCreateCard_E = 23;  // どれいの番号をセット
            }
        }

        public void Set_KingCard()  // 王さまカード をセットします A,B,C,D,E
        {
            Debug.Log("王さまカード をセットします");
            if (RndSet_CardPos_King == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_King;
                RndCreateCard_A = 13;  // 王さまの番号をセット
            }
            else if (RndSet_CardPos_King == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_King;
                RndCreateCard_B = 13;  // 王さまの番号をセット
            }
            else if (RndSet_CardPos_King == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_King;
                RndCreateCard_C = 13;  // 王さまの番号をセット
            }
            else if (RndSet_CardPos_King == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_King;
                RndCreateCard_D = 13;  // 王さまの番号をセット
            }
            else if (RndSet_CardPos_King == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_King;
                RndCreateCard_E = 13;  // 王さまの番号をセット
            }
        }

        public void A_Set()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);

            if (isSet_A == false) // まだセットしていなければ
            {
                RndCreateCard_A = Random.Range(0, 3);
                AppearButton_A();
                Debug.Log(RndCreateCard_A + ": RndCreateCard_A");

                if (RndCreateCard_A == 0)  //グー
                {
                    Button_A.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_A == 1)  //チョキ
                {
                    Button_A.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_A == 2) //パー
                {
                    Button_A.gameObject.GetComponent<Image>().sprite = sprite_Pa;
                }
                else
                {
                    Debug.Log("ジャンケン ERROR");
                }

                isSet_A = true;

            }
            else
            {
                Debug.Log("カードセット済み");
            }
        }

        public void B_Set()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);

            if (isSet_B == false) // まだセットしていなければ
            {
                RndCreateCard_B = Random.Range(0, 3);
                AppearButton_B();
                Debug.Log(RndCreateCard_B + ": RndCreateCard_B");

                if (RndCreateCard_B == 0)  //グー
                {
                    Button_B.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_B == 1)  //チョキ
                {
                    Button_B.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_B == 2) //パー
                {
                    Button_B.gameObject.GetComponent<Image>().sprite = sprite_Pa;
                }
                else
                {
                    Debug.Log("ジャンケン ERROR");
                }

                isSet_B = true;
            }
            else
            {
                Debug.Log("カードセット済み");
            }
        }

        public void C_Set()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);

            if (isSet_C == false) // まだセットしていなければ
            {
                RndCreateCard_C = Random.Range(0, 3);
                AppearButton_C();
                Debug.Log(RndCreateCard_C + ": RndCreateCard_C");

                if (RndCreateCard_C == 0)  //グー
                {
                    Button_C.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_C == 1)  //チョキ
                {
                    Button_C.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_C == 2) //パー
                {
                    Button_C.gameObject.GetComponent<Image>().sprite = sprite_Pa;
                }
                else
                {
                    Debug.Log("ジャンケン ERROR");
                }

                isSet_C = true;
            }
            else
            {
                Debug.Log("カードセット済み");
            }
        }

        public void D_Set()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);

            if (isSet_D == false) // まだセットしていなければ
            {
                RndCreateCard_D = Random.Range(0, 3);
                AppearButton_D();
                Debug.Log(RndCreateCard_D + ": RndCreateCard_D");

                if (RndCreateCard_D == 0)  //グー
                {
                    Button_D.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_D == 1)  //チョキ
                {
                    Button_D.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_D == 2) //パー
                {
                    Button_D.gameObject.GetComponent<Image>().sprite = sprite_Pa;
                }
                else
                {
                    Debug.Log("ジャンケン ERROR");
                }

                isSet_D = true;
            }
            else
            {
                Debug.Log("カードセット済み");
            }
        }

        public void E_Set()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);

            if (isSet_E == false) // まだセットしていなければ
            {
                RndCreateCard_E = Random.Range(0, 3);
                AppearButton_E();
                Debug.Log(RndCreateCard_E + ": RndCreateCard_E");

                if (RndCreateCard_E == 0)  //グー
                {
                    Button_E.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_E == 1)  //チョキ
                {
                    Button_E.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_E == 2) //パー
                {
                    Button_E.gameObject.GetComponent<Image>().sprite = sprite_Pa;
                }
                else
                {
                    Debug.Log("ジャンケン ERROR");
                }

                isSet_E = true;
            }
            else
            {
                Debug.Log("カードセット済み");
            }
        }


        #endregion

        #region// じゃんけんカード 手のリセット

        public void Reset_All()
        {
            A_Reset();
            B_Reset();
            C_Reset();
            D_Reset();
            E_Reset();
        }

        public void A_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_A = -1;
            Debug.Log(RndCreateCard_A + ": RndCreateCard_A");
            //Button_A.gameObject.GetComponent<Image>().sprite = null;
            isSet_A = false;
        }

        public void B_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_B = -1;
            Debug.Log(RndCreateCard_B + ": RndCreateCard_B");
            //Button_B.gameObject.GetComponent<Image>().sprite = null;
            isSet_B = false;
        }

        public void C_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_C = -1;
            Debug.Log(RndCreateCard_C + ": RndCreateCard_C");
            //Button_C.gameObject.GetComponent<Image>().sprite = null;
            isSet_C = false;
        }

        public void D_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_D = -1;
            Debug.Log(RndCreateCard_D + ": RndCreateCard_D");
            //Button_D.gameObject.GetComponent<Image>().sprite = null;
            isSet_D = false;
        }

        public void E_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_E = -1;
            Debug.Log(RndCreateCard_E + ": RndCreateCard_E");
            //Button_E.gameObject.GetComponent<Image>().sprite = null;
            isSet_E = false;
        }
        #endregion


        #region// じゃんけんカードボタン 表示・非表示の設定

        public void ClosePanel_To_Defalt()   // 不要なパネルを閉じて、デフォルト状態にする
        {
            CloseJankenCards_Panel();    //●非表示にする
            CloseMyJankenPanel();        //●非表示にする
            CloseWait_JankenPanel();     //●非表示にする
        }

        //●表示させる
        public void AppearButton_A()
        {
            Button_A.SetActive(true);
        }

        //●非表示にする
        public void CloseButton_A()
        {
            Button_A.SetActive(false);
        }

        //●表示させる
        public void AppearButton_B()
        {
            Button_B.SetActive(true);
        }

        //●非表示にする
        public void CloseButton_B()
        {
            Button_B.SetActive(false);
        }

        //●表示させる
        public void AppearButton_C()
        {
            Button_C.SetActive(true);
        }

        //●非表示にする
        public void CloseButton_C()
        {
            Button_C.SetActive(false);
        }

        //●表示させる
        public void AppearButton_D()
        {
            Button_D.SetActive(true);
        }

        //●非表示にする
        public void CloseButton_D()
        {
            Button_D.SetActive(false);
        }

        //●表示させる
        public void AppearButton_E()
        {
            Button_E.SetActive(true);
        }

        //●非表示にする
        public void CloseButton_E()
        {
            Button_E.SetActive(false);
        }

        public void AppearKetteiBtn()        //●表示させる
        {
            KetteiBtn.SetActive(true);
        }
        public void CloseKetteiBtn()        //●非表示にする
        {
            KetteiBtn.SetActive(false);
        }

        public void AppearJankenCards_Panel()   //●表示させる
        {
            JankenCards_Panel.SetActive(true);
        }
        public void CloseJankenCards_Panel()    //●非表示にする
        {
            JankenCards_Panel.SetActive(false);
        }

        public void AppearMyJankenPanel()       //●表示させる
        {
            MyJankenPanel.SetActive(true);
        }
        public void CloseMyJankenPanel()        //●非表示にする
        {
            MyJankenPanel.SetActive(false);
        }

        public void AppearWait_JankenPanel()       //●表示させる
        {
            Wait_JankenPanel.SetActive(true);
        }
        public void CloseWait_JankenPanel()        //●非表示にする
        {
            Wait_JankenPanel.SetActive(false);
        }
        #endregion

        //#################################################################################

    }
    // End
}