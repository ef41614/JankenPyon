using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using Random = UnityEngine.Random;

namespace say
{
    public class ShuffleCards : MonoBehaviourPunCallbacks
    {

        public int count_selected = 1;

        public Sprite sprite_Gu;
        public Sprite sprite_Choki;
        public Sprite sprite_Pa;

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

        public GameObject MyJankenPanel;


        /*
        public Button Btn_A;
        public Button Btn_B;
        public Button Btn_C;
        public Button Btn_D;
        public Button Btn_E;
        */
        public bool isSet_A = false;
        public bool isSet_B = false;
        public bool isSet_C = false;
        public bool isSet_D = false;
        public bool isSet_E = false;

        //☆################☆################  Start  ################☆################☆

        void Start()
        {
            Debug.Log("ShuffleCards 出席確認");
            AppearButton_A();
            AppearButton_B();
            AppearButton_C();
            AppearButton_D();
            AppearButton_E();
            /*
            Btn_A.interactable = true;
            Btn_B.interactable = true;
            Btn_C.interactable = true;
            Btn_D.interactable = true;
            Btn_E.interactable = true;

            Btn_A = GetComponent<Button>();
            Btn_B = GetComponent<Button>();
            Btn_C = GetComponent<Button>();
            Btn_D = GetComponent<Button>();
            Btn_E = GetComponent<Button>();
            */
            count_selected = 1;
            /*
            CreateCard_A();
            CreateCard_B();
            CreateCard_C();
            CreateCard_D();
            CreateCard_E();
            
            A_Set();
            B_Set();
            C_Set();
            D_Set();
            E_Set();
            */
            Reset_All();
            Set_All();
        }


        //####################################  Update  ###################################

        void Update()
        {


        }

        //####################################  other  ####################################

        //じゃんけんカードを一枚ランダムで生成する
        /*
        public void CreateCard_A()
        {
            RndCreateCard_A = Random.Range(0, 3);
            AppearButton_A();

            if (RndCreateCard_A == 0)  //グー
            {
                SetGu();
            }
            else if (RndCreateCard_A == 1)  //チョキ
            {
                SetChoki();
            }
            else if (RndCreateCard_A == 2) //パー
            {
                SetPa();
            }
            else
            {
                Debug.Log("ジャンケン ERROR");
            }
        }

        public void CreateCard_B()
        {
            RndCreateCard_B = Random.Range(0, 3);
            AppearButton_B();

            if (RndCreateCard_B == 0)  //グー
            {
                SetGu();
            }
            else if (RndCreateCard_B == 1)  //チョキ
            {
                SetChoki();
            }
            else if (RndCreateCard_B == 2) //パー
            {
                SetPa();
            }
            else
            {
                Debug.Log("ジャンケン ERROR");
            }
        }


        public void CreateCard_C()
        {
            RndCreateCard_C = Random.Range(0, 3);
            AppearButton_C();

            if (RndCreateCard_C == 0)  //グー
            {
                SetGu();
            }
            else if (RndCreateCard_C == 1)  //チョキ
            {
                SetChoki();
            }
            else if (RndCreateCard_C == 2) //パー
            {
                SetPa();
            }
            else
            {
                Debug.Log("ジャンケン ERROR");
            }
        }

        public void CreateCard_D()
        {
            RndCreateCard_D = Random.Range(0, 3);
            AppearButton_D();

            if (RndCreateCard_D == 0)  //グー
            {
                SetGu();
            }
            else if (RndCreateCard_D == 1)  //チョキ
            {
                SetChoki();
            }
            else if (RndCreateCard_D == 2) //パー
            {
                SetPa();
            }
            else
            {
                Debug.Log("ジャンケン ERROR");
            }
        }

        public void CreateCard_E()
        {
            RndCreateCard_E = Random.Range(0, 3);
            AppearButton_E();

            if (RndCreateCard_E == 0)  //グー
            {
                SetGu();
            }
            else if (RndCreateCard_E == 1)  //チョキ
            {
                SetChoki();
            }
            else if (RndCreateCard_E == 2) //パー
            {
                SetPa();
            }
            else
            {
                Debug.Log("ジャンケン ERROR");
            }
        }
        */

        /*
        public void SetGu()
        {
            Debug.Log("今グーがセットされました" + PhotonNetwork.NickName);
            Debug.Log(count_selected + ": count_selected");
            if (count_selected <= 2)
            {
                Te_C.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            }
            else
            {
                Debug.Log("count_selected 3以上");
            }
            count_selected++;
            Debug.Log(count_selected + ": count_selected");
        }

        public void SetChoki()
        {
            Debug.Log("今チョキがセットされました" + PhotonNetwork.NickName);
            Debug.Log(count_selected + ": count_selected");
            if (count_selected <= 2)
            {
                Te_C.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            }
            else
            {
                Debug.Log("count_selected 3以上");
            }
            count_selected++;
            Debug.Log(count_selected + ": count_selected");
        }

        public void SetPa()
        {
            Debug.Log("今パーがセットされました" + PhotonNetwork.NickName);
            Debug.Log(count_selected + ": count_selected");
            if (count_selected <= 2)
            {
                Te_C.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            }
            else
            {
                Debug.Log("count_selected 3以上");
            }
            count_selected++;
            Debug.Log(count_selected + ": count_selected");
        }
        */

        public void JankenTe_Kettei()
        {
            CloseMyJankenPanel();
            /*
            count_selected = 1;
            CloseButton_A();
            count_selected = 1;
            CloseButton_C();
            count_selected = 1;
            CloseButton_B();
            count_selected = 1;
            CloseButton_D();
            count_selected = 1;
            CloseButton_E();
            */
        }


        #region// じゃんけんカード 手のセット

        public void Set_All()
        {
            A_Set();
            B_Set();
            C_Set();
            D_Set();
            E_Set();
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
                    Te_A.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_A == 1)  //チョキ
                {
                    Te_A.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_A == 2) //パー
                {
                    Te_A.gameObject.GetComponent<Image>().sprite = sprite_Pa;
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
                    Te_B.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_B == 1)  //チョキ
                {
                    Te_B.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_B == 2) //パー
                {
                    Te_B.gameObject.GetComponent<Image>().sprite = sprite_Pa;
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
                    Te_C.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_C == 1)  //チョキ
                {
                    Te_C.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_C == 2) //パー
                {
                    Te_C.gameObject.GetComponent<Image>().sprite = sprite_Pa;
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
                    Te_D.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_D == 1)  //チョキ
                {
                    Te_D.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_D == 2) //パー
                {
                    Te_D.gameObject.GetComponent<Image>().sprite = sprite_Pa;
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
                    Te_E.gameObject.GetComponent<Image>().sprite = sprite_Gu;
                }
                else if (RndCreateCard_E == 1)  //チョキ
                {
                    Te_E.gameObject.GetComponent<Image>().sprite = sprite_Choki;
                }
                else if (RndCreateCard_E == 2) //パー
                {
                    Te_E.gameObject.GetComponent<Image>().sprite = sprite_Pa;
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
            //Te_A.gameObject.GetComponent<Image>().sprite = null;
            isSet_A = false;
        }

        public void B_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_B = -1;
            Debug.Log(RndCreateCard_B + ": RndCreateCard_B");
            //Te_B.gameObject.GetComponent<Image>().sprite = null;
            isSet_B = false;
        }

        public void C_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_C = -1;
            Debug.Log(RndCreateCard_C + ": RndCreateCard_C");
            //Te_C.gameObject.GetComponent<Image>().sprite = null;
            isSet_C = false;
        }

        public void D_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_D = -1;
            Debug.Log(RndCreateCard_D + ": RndCreateCard_D");
            //Te_D.gameObject.GetComponent<Image>().sprite = null;
            isSet_D = false;
        }

        public void E_Reset()
        {
            Debug.Log("PhotonNetwork.NickName : " + PhotonNetwork.NickName);
            RndCreateCard_E = -1;
            Debug.Log(RndCreateCard_E + ": RndCreateCard_E");
            //Te_E.gameObject.GetComponent<Image>().sprite = null;
            isSet_E = false;
        }
        #endregion


        #region// じゃんけんカードボタン 表示・非表示の設定

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

        //●表示させる
        public void AppearMyJankenPanel()
        {
            MyJankenPanel.SetActive(true);
        }

        //●非表示にする
        public void CloseMyJankenPanel()
        {
            MyJankenPanel.SetActive(false);
        }
        #endregion

        //#################################################################################

    }
    // End
}