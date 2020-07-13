using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using say;　// 対象のスクリプトの情報を取得

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

    public int ken1_1;
    public int ken1_2;

    public int ken2_1;
    public int ken2_2;

    public int ken3_1;
    public int ken3_2;

    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム

    void Start()
    {
        Debug.Log("SelectJanken 出席確認");
        count_a = 1;
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
    }

    public void SelectJankenCard()
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

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

}