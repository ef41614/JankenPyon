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

        //☆################☆################  Start  ################☆################☆

        void Start()
        {
            Debug.Log("ShuffleCards 出席確認");
            AppearButton_A();
            AppearButton_B();
            AppearButton_C();
            AppearButton_D();
            AppearButton_E();
            count_selected = 1;
            CreateCard_A();
            CreateCard_B();
            CreateCard_C();
            CreateCard_D();
            CreateCard_E();
        }


        //####################################  Update  ###################################

        void Update()
        {


        }

        //####################################  other  ####################################

        //じゃんけんカードを一枚ランダムで生成する
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

        public void JankenTe_Kettei()
        {
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

        //#################################################################################

    }
    // End
}