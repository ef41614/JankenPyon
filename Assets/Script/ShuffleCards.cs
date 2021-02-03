using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using BEFOOL.PhotonTest;
using DG.Tweening;
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
        public Sprite sprite_Muteki;
        public Sprite sprite_Wall;
        public Sprite sprite_WFlag;

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
        public int int_StockCard_Up = 0;
        public int int_StockCard_Down = 0;

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

        public GameObject BGM_SE_Manager;
        BGM_SE_Manager BGM_SE_MSC;

        int RndSet_CardPos_King;             // A～E どの位置にカードをセットするか
        int RndSet_CardPos_Dorei;            // A～E どの位置にカードをセットするか
        int RndSet_CardPos_Muteki;           // A～E どの位置にカードをセットするか
        int RndSet_CardPos_Wall;             // A～E どの位置にカードをセットするか
        int RndSet_CardPos_WFlag;            // A～E どの位置にカードをセットするか
        int RndSet_CardPos_WFlag2;           // A～E どの位置にカードをセットするか
        int RndSet_CardPos_WFlag3;           // A～E どの位置にカードをセットするか

        //public bool Tarai_to_SetWFlag = false;  // たらいが落ちると、確定で白旗一枚

        public GameObject Stock_Card_Up;      // ストックカード Up枠
        public GameObject Stock_Button_Up;
        public GameObject Stock_Card_Down;      // ストックカード Down枠
        public GameObject Stock_Button_Down;


        private void Awake()
        {
            Debug.Log("ShuffleCards Awake 出席確認");
            ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
            SelectJankenMSC = SelectJankenManager.GetComponent<SelectJanken>();
            TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
            BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
            BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();
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
            //Set_All();
            ClosePanel_To_Defalt();   // 不要なパネルを閉じて、デフォルト状態にする
            Stock_Button_Up.SetActive(false);
            Stock_Button_Down.SetActive(false);
        }


        //####################################  Update  ###################################

        void Update()
        {


        }

        //####################################  other  ####################################

        #region// じゃんけんカード 手のセット

        public void Set_All()
        {

            RndSet_CardPos_King = UnityEngine.Random.Range(1, 6);        // A～E どの位置にカードをセットするか
            RndSet_CardPos_Dorei = UnityEngine.Random.Range(1, 6);
            RndSet_CardPos_Muteki = UnityEngine.Random.Range(1, 6);
            RndSet_CardPos_Wall = UnityEngine.Random.Range(1, 6);
            RndSet_CardPos_WFlag = UnityEngine.Random.Range(1, 6);
            RndSet_CardPos_WFlag2 = UnityEngine.Random.Range(1, 6);
            RndSet_CardPos_WFlag3 = UnityEngine.Random.Range(1, 6);

            A_Set();    // ぐー、ちょき、ぱー のみセット
            B_Set();
            C_Set();
            D_Set();
            E_Set();

            Debug.Log("ここまでが 通常ジャンケン手のセット");
            Debug.Log("ここから 特殊カードのセット処理");

            int RndGo_King = UnityEngine.Random.Range(1, 101);
            int RndGo_Dorei = UnityEngine.Random.Range(1, 101);
            int RndGo_Muteki = UnityEngine.Random.Range(1, 101);
            int RndGo_Wall = UnityEngine.Random.Range(1, 101);
            int RndGo_WFlag = UnityEngine.Random.Range(1, 101);

            int RndGo_ReverseOrder = UnityEngine.Random.Range(1, 11);  // 正規順でセットするか、逆順でセットするか

            if (RndGo_ReverseOrder <= 5)  // 従来通りのカードセット順
            {
                if (RndGo_King >= 85)
                {
                    Set_KingCard();              // 王様カード をセットします  A,B,C,D,E
                }
                if (RndGo_Dorei >= 85)
                {
                    Set_DoreiCard();             // どれいカード をセットします  A,B,C,D,E
                }
                if (RndGo_Muteki >= 85)
                {
                    Set_MutekiCard();            // むてきカード をセットします  A,B,C,D,E
                }
                if (RndGo_Wall >= 85)
                {
                    Set_WallCard();              // 壁カード をセットします  A,B,C,D,E
                }
                if (RndGo_WFlag >= 85)
                {
                    Set_WFlagCard();             // 白旗カード をセットします  A,B,C,D,E
                }
            }
            else                          // 従来とは逆のカードセット順
            {
                if (RndGo_WFlag >= 85)
                {
                    Set_WFlagCard();             // 白旗カード をセットします  A,B,C,D,E
                }
                if (RndGo_Wall >= 85)
                {
                    Set_WallCard();              // 壁カード をセットします  A,B,C,D,E
                }
                if (RndGo_Muteki >= 85)
                {
                    Set_MutekiCard();            // むてきカード をセットします  A,B,C,D,E
                }
                if (RndGo_Dorei >= 85)
                {
                    Set_DoreiCard();             // どれいカード をセットします  A,B,C,D,E
                }
                if (RndGo_King >= 85)
                {
                    Set_KingCard();              // 王様カード をセットします  A,B,C,D,E
                }
            }

            Debug.Log("たらいが落ちるフラグによる判定スタート");

            if (SelectJankenMSC.Tarai_to_SetWFlag)            // たらいが落ちるフラグON
            {
                //SelectJankenMSC.Tarai_to_SetWFlag = true;  // たらいが落ちると、確定で白旗一枚
                Set_WFlagCard();             // 白旗カード をセットします  A,B,C,D,E              
            }
            if (SelectJankenMSC.Katakori_to_SetWFlag)            // 肩こりフラグON
            {
                int Katakori_degree = UnityEngine.Random.Range(0, 11);       // 肩こりの程度
                if (Katakori_degree <= 0)
                {
                }

                if (Katakori_degree >= 1)
                {
                    Set_WFlagCard();             // 白旗カード をセットします  A,B,C,D,E              
                }

                if (Katakori_degree >= 4)
                {
                    Set_WFlagCard2();             // 白旗カード をセットします  A,B,C,D,E              
                }

                if (Katakori_degree >= 9)
                {
                    Set_WFlagCard3();             // 白旗カード をセットします  A,B,C,D,E              
                }
            }

            Debug.Log("たらいが落ちるフラグによる判定おわり");

            //SelectJankenMSC.Check_LifeZERO();  // 体力がゼロになっているか確認する → セロなら白旗ALL
            //Debug.Log("Check_LifeZERO おわり");

        }


        public void Set_DoreiCard()  // どれいカード をセットします  A,B,C,D,E
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

        public void Set_MutekiCard()  // むてきカード をセットします  A,B,C,D,E
        {
            Debug.Log("むてきカード をセットします");
            if (RndSet_CardPos_Muteki == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
                RndCreateCard_A = 601;  // むてきの番号をセット
            }
            else if (RndSet_CardPos_Muteki == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
                RndCreateCard_B = 601;  // むてきの番号をセット
            }
            else if (RndSet_CardPos_Muteki == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
                RndCreateCard_C = 601;  // むてきの番号をセット
            }
            else if (RndSet_CardPos_Muteki == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
                RndCreateCard_D = 601;  // むてきの番号をセット
            }
            else if (RndSet_CardPos_Muteki == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
                RndCreateCard_E = 601;  // むてきの番号をセット
            }
        }

        public void Set_WallCard()  // 壁カード をセットします  A,B,C,D,E
        {
            Debug.Log("壁カード をセットします");
            if (RndSet_CardPos_Wall == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_Wall;
                RndCreateCard_A = 88;  // 壁の番号をセット
            }
            else if (RndSet_CardPos_Wall == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_Wall;
                RndCreateCard_B = 88;  // 壁の番号をセット
            }
            else if (RndSet_CardPos_Wall == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_Wall;
                RndCreateCard_C = 88;  // 壁の番号をセット
            }
            else if (RndSet_CardPos_Wall == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_Wall;
                RndCreateCard_D = 88;  // 壁の番号をセット
            }
            else if (RndSet_CardPos_Wall == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_Wall;
                RndCreateCard_E = 88;  // 壁の番号をセット
            }
        }

        public void Set_WFlagCard()  // 白旗カード をセットします  A,B,C,D,E
        {
            Debug.Log("白旗カード をセットします");
            if (RndSet_CardPos_WFlag == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_A = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_B = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_C = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_D = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_E = 46;  // 白旗の番号をセット
            }
        }

        public void Set_WFlagCard2()  // 白旗カード をセットします  A,B,C,D,E
        {
            Debug.Log("白旗カード をセットします");
            if (RndSet_CardPos_WFlag2 == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_A = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag2 == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_B = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag2 == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_C = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag2 == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_D = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag2 == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_E = 46;  // 白旗の番号をセット
            }
        }

        public void Set_WFlagCard3()  // 白旗カード をセットします  A,B,C,D,E
        {
            Debug.Log("白旗カード をセットします");
            if (RndSet_CardPos_WFlag3 == 1)
            {
                Button_A.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_A = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag3 == 2)
            {
                Button_B.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_B = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag3 == 3)
            {
                Button_C.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_C = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag3 == 4)
            {
                Button_D.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_D = 46;  // 白旗の番号をセット
            }
            else if (RndSet_CardPos_WFlag3 == 5)
            {
                Button_E.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
                RndCreateCard_E = 46;  // 白旗の番号をセット
            }
        }

        public void SetAll_WFlagCard()  // すべて白旗カード でセットします（このターンは負け確定）
        {
            Debug.Log("すべて白旗カード でセットします");

            Button_A.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            RndCreateCard_A = 46;  // 白旗の番号をセット

            Button_B.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            RndCreateCard_B = 46;  // 白旗の番号をセット

            Button_C.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            RndCreateCard_C = 46;  // 白旗の番号をセット

            Button_D.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            RndCreateCard_D = 46;  // 白旗の番号をセット

            Button_E.gameObject.GetComponent<Image>().sprite = sprite_WFlag;
            RndCreateCard_E = 46;  // 白旗の番号をセット
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

        public void Distribute_JankenCards()  // じゃんけんカードの配布（一旦非表示にしてから、順番に表示していく）
        {
            CloseButton_A();
            CloseButton_B();
            CloseButton_C();
            CloseButton_D();
            CloseButton_E();

            //BGM_SE_MSC.Distribute_JankenCards_18_SE();
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(0.38f, () => BGM_SE_MSC.Distribute_JankenCards_18_SE());

            var sequenceA = DOTween.Sequence();
            sequenceA.InsertCallback(0.5f, () => AppearButton_A());

            var sequenceB = DOTween.Sequence();
            sequenceB.InsertCallback(0.7f, () => AppearButton_B());

            var sequenceC = DOTween.Sequence();
            sequenceC.InsertCallback(0.9f, () => AppearButton_C());

            var sequenceD = DOTween.Sequence();
            sequenceD.InsertCallback(1.1f, () => AppearButton_D());

            var sequenceE = DOTween.Sequence();
            sequenceE.InsertCallback(1.3f, () => AppearButton_E());
        }

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

        #region// カードストック機能 stock
        public void StockUp_Set_MutekiCard()  // StockU に むてきカード をセットします  
        {
            Debug.Log("StockU に むてきカード をセットします  ");
            Stock_Button_Up.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            int_StockCard_Up = 601;
        }

        public void StockDown_Set_MutekiCard()  // StockDown に むてきカード をセットします  
        {
            Debug.Log("StockDown に むてきカード をセットします  ");
            Stock_Button_Down.gameObject.GetComponent<Image>().sprite = sprite_Muteki;
            int_StockCard_Down = 601;
        }
        #endregion

        //#################################################################################

    }
    // End
}
