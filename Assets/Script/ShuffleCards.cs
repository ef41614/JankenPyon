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

        public Image Te_C;

        public int RndCreateCard_C = 0;
        public GameObject Button_C;

        //☆################☆################  Start  ################☆################☆

        void Start()
        {
            AppearButton_C();
            Debug.Log("ShuffleCards 出席確認");
            count_selected = 1;
            CreateCard_C();
        }


        //####################################  Update  ###################################

        void Update()
        {


        }

        //####################################  other  ####################################

        //じゃんけんカードを一枚ランダムで生成する
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
            CloseButton_C();
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

        //#################################################################################

    }
    // End
}