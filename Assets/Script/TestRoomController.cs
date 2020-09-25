using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;

namespace BEFOOL.PhotonTest
{
    public class TestRoomController : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        Text joinedMembersText = null;

        public string string_MyName;
        public string string_PName1;
        public string string_PName2;
        public string string_PName3;
        public string string_PName4;

        public string string_PID1 = "";
        public string string_PID2 = "";
        public string string_PID3 = "";
        public string string_PID4 = "";

        //public string string_PlayerID_1;
        //public string string_PlayerID_2;
        //public string string_PlayerID_3;
        //public string string_PlayerID_4;
        public Player[] allPlayers;

        public int int_JoinedPlayerAllNum = 0;

        public GameObject Text_MyHeadName;
        public Text Text_PName1;
        public Text Text_PName2;
        public Text Text_PName3;
        public Text Text_PName4;

        public GameObject SelectJankenManager; //ヒエラルキー上のオブジェクト名
        SelectJanken SelectJankenMSC;//スクリプト名 + このページ上でのニックネーム
        //public SelectJanken SelectJankenMSC; //スクリプト名 + このページ上でのニックネーム

        void Start()
        {
            //EntryCheck();
            Debug.Log("TestRoomController 出席確認");
            allPlayers = PhotonNetwork.PlayerList; // プレイヤーの配列（自身を含む）
            Debug.Log(allPlayers.Length + ": allPlayers.Length");
            SelectJankenMSC = SelectJankenManager.GetComponent<SelectJanken>();
            /*
            Text_PName1 = Text_PName1.GetComponent<Text>();
            Text_PName2 = Text_PName2.GetComponent<Text>();
            Text_PName3 = Text_PName3.GetComponent<Text>();
            Text_PName4 = Text_PName4.GetComponent<Text>();
            */
            UpdateMemberList();
            string_PName1 = allPlayers[0].NickName;
            Debug.Log(string_PName1 + ": string_PName1");
            string_PID1 = allPlayers[0].UserId;
            Debug.Log(string_PID1 + ": string_PID1");
            SelectJankenMSC.Share_AcutivePlayerID();
            EntryCheck();
            PNameCheck();
            Set_PNameTextAll();
            //ToSet_MyHeadName();
        }

        // <summary>
        // リモートプレイヤーが入室した際にコールされる
        // </summary>
        // 他プレイヤーが参加した時に呼ばれるコールバック
        public override void OnPlayerEnteredRoom(Player player)
        {
            LoginCheck(player);
            EntryCheck();
            PNameCheck();
            Set_PNameTextAll();
            SelectJankenMSC.ToShare_InitialSetting();   // プレイヤー名 横の顔アイコンをセットして共有する
            //ToSet_MyHeadName();                       // Myプレイヤー名をキャラの頭上に表示
        }

        public void LoginCheck(Player player)
        {
            Debug.Log("LoginCheck（ログイン時チェック）を実施します");

            UpdateMemberList();
            Debug.Log(allPlayers.Length + ": allPlayers.Length");
            Debug.Log(player.NickName + "が参加しました");
            UpdateMemberList();
            Debug.Log(allPlayers.Length + ": allPlayers.Length");
            UpdateMemberList();
            Debug.Log(allPlayers.Length + ": allPlayers.Length");
            Debug.Log(player.NickName + "が参加しました2");

            PNameCheck();

            Debug.Log(player.NickName + "が参加しました3");
            Debug.Log(allPlayers.Length + ": allPlayers.Length");
            /*
            Text_PName1.text = string_PName1;
            Text_PName2.text = string_PName2;
            Text_PName3.text = string_PName3;
            Text_PName4.text = string_PName4;
            */
            Debug.Log("プレイヤー名テキストをセットします");
            Set_PNameTextAll();
            Debug.Log("プレイヤー名テキストをセットしました");

            Debug.Log(" allPlayers.Length は "+ allPlayers.Length + " です");

            Debug.Log(" allPlayers.Length チェック 2");
            if (allPlayers.Length >= 2)  // 参加人数 2名 は居る
            {
                Debug.Log(" allPlayers.Length は 2以上です");
                SelectJankenMSC.ToShareNinzu_2();
            }

            Debug.Log(" allPlayers.Length チェック 3");
            if (allPlayers.Length >= 3)  // 参加人数 3名 は居る
            {
                Debug.Log(" allPlayers.Length は 3以上です");
                SelectJankenMSC.ToShareNinzu_3();
            }

            Debug.Log(" allPlayers.Length チェック 4");
            if (allPlayers.Length >= 4)  // 参加人数 4名 は居る
            {
                Debug.Log(" allPlayers.Length は 4以上です");
                SelectJankenMSC.ToShareNinzu_4();
            }
        }
        /*
        [PunRPC]
        public void ShareNinzu_2()   // // 参加人数 2名 は居る → 全員に共有
        {
            Debug.Log("参加人数 2名 は居る");
            SelectJankenMSC.NumLivePlayer = 2;
            SelectJankenMSC.SankaNinzu = 2;
        }

        [PunRPC]
        public void ShareNinzu_3()   // // 参加人数 3名 は居る → 全員に共有
        {
            Debug.Log("参加人数 3名 は居る");
            SelectJankenMSC.NumLivePlayer = 3;
            SelectJankenMSC.SankaNinzu = 3;
        }

        [PunRPC]
        public void ShareNinzu_4()   // // 参加人数 4名 は居る → 全員に共有
        {
            Debug.Log("参加人数 4名 は居る");
            SelectJankenMSC.NumLivePlayer = 4;
            SelectJankenMSC.SankaNinzu = 4;
        }
        */

        public void PNameCheck() // 現時点の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        {
            Debug.Log("「PNameCheck」現時点の参加人数を更新します（プレイヤー名が埋まっていなかったら入れる）");
            if (string_PID4 != "5")
            {
                //Debug.Log("string_PID4 まだ埋まっていない");
                string_PName1 = allPlayers[0].NickName;
                string_PID1 = allPlayers[0].UserId;
                //Debug.Log(string_PID1 + ": string_PID1");
                //Debug.Log(allPlayers.Length + ": allPlayers.Length");
                int_JoinedPlayerAllNum = 1;

                if (allPlayers.Length >= 2)
                {
                    string_PName2 = allPlayers[1].NickName;
                    string_PID2 = allPlayers[1].UserId;
                    //Debug.Log(string_PID2 + ": string_PID2");
                    //Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    int_JoinedPlayerAllNum = 2;
                }

                if (allPlayers.Length >= 3)
                {
                    string_PName3 = allPlayers[2].NickName;
                    //Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    string_PID3 = allPlayers[2].UserId;
                    //Debug.Log(string_PID3 + ": string_PID3");
                    int_JoinedPlayerAllNum = 3;
                }

                if (allPlayers.Length >= 4)
                {
                    string_PName4 = allPlayers[3].NickName;
                    //Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    string_PID4 = allPlayers[3].UserId;
                    //Debug.Log(string_PID4 + ": string_PID4");
                    int_JoinedPlayerAllNum = 4;
                }

                //string_PName4 = allPlayers[3].NickName;
                //Debug.Log(string_PName1 + ": string_PName1");
                //Debug.Log(string_PName2 + ": string_PName2");
                //Debug.Log(string_PName3 + ": string_PName3");
                //Debug.Log(string_PName4 + ": string_PName4");

                //Debug.Log(allPlayers.Length + ": allPlayers.Length");
                //Debug.Log("現在の参加人数は " + int_JoinedPlayerAllNum);
            }
            else
            {
                Debug.Log("string_PID4 まで 既に埋まっています");
            }
            Set_PNameTextAll();
        }

        public void EntryCheck()
        {
            Debug.Log(" ＝＝＝ EntryCheck スタート！ ＝＝＝ ");
            SelectJankenMSC.Share_AcutivePlayerID();

            /*
            if (string_PlayerID_1 == "")
            {
                string_PlayerID_1 = SelectJankenMSC.senderID;
            }
            else if (string_PlayerID_2 == "")
            {
                string_PlayerID_2 = SelectJankenMSC.senderID;
            }
            else if (string_PlayerID_3 == "")
            {
                string_PlayerID_3 = SelectJankenMSC.senderID;
            }
            else if (string_PlayerID_4 == "")
            {
                string_PlayerID_4 = SelectJankenMSC.senderID;
            }
            else
            {
                Debug.Log(" EntryCheck 不要です");
            }
            */

            PresentMemberCheck();
            Debug.Log(" ＝＝＝ EntryCheck 終わり！ ＝＝＝ ");
        }

        public void PresentMemberCheck()
        {
            /*
            Debug.Log(string_PlayerID_1 + ": string_PlayerID_1");
            Debug.Log(string_PlayerID_2 + ": string_PlayerID_2");
            Debug.Log(string_PlayerID_3 + ": string_PlayerID_3");
            Debug.Log(string_PlayerID_4 + ": string_PlayerID_4");
            */
            Debug.Log("allPlayers[0].NickName" + allPlayers[0].NickName);
            //Debug.Log("allPlayers[0].UserId" + allPlayers[0].UserId);
            if (allPlayers.Length >= 2)  // 参加人数 2名 は居る
            {
                Debug.Log("allPlayers[1].NickName" + allPlayers[1].NickName);
                //Debug.Log("allPlayers[1].UserId" + allPlayers[1].UserId);
            }
            if (allPlayers.Length >= 3)  // 参加人数 3名 は居る
            {
                Debug.Log("allPlayers[2].NickName" + allPlayers[2].NickName);
                //Debug.Log("allPlayers[2].UserId" + allPlayers[2].UserId);
            }
            if (allPlayers.Length >= 4)  // 参加人数 4名 は居る
            {
                Debug.Log("allPlayers[3].NickName" + allPlayers[3].NickName);
                //Debug.Log("allPlayers[3].UserId" + allPlayers[3].UserId);
            }
            Set_PNameTextAll(); // プレイヤー名1～4 をセット
        }

        // <summary>
        // リモートプレイヤーが退室した際にコールされる
        // </summary>
        // 他プレイヤーが退出した時に呼ばれるコールバック
        public override void OnPlayerLeftRoom(Player player)
        {
            {
                Debug.Log(player.NickName + "が退出しました");
                UpdateMemberList();
                UpdateMemberList();
            }
        }

        public void UpdateMemberList()
        {
            allPlayers = PhotonNetwork.PlayerList; // プレイヤーの配列（自身を含む）
            joinedMembersText.text = "";
            foreach (var p in PhotonNetwork.PlayerList)
            {
                joinedMembersText.text += p.NickName + "\n";
            }
        }

        public void MyNameIs(Player player)
        {
            Debug.Log("私の名前は 「" + player.NickName + " 」でござる");
            string_MyName = player.NickName;
            string_MyName = PhotonNetwork.NickName;

            Text_MyHeadName = GameObject.FindWithTag("MyHeadName");
            // Text_MyHeadName = GameObject.FindWithTag("MyHeadName");
            Text_MyHeadName.GetComponent<Text>().text = string_MyName;
        }

        /*
        public void ToSet_MyHeadName() // Myプレイヤー名をキャラの頭上に表示
        {
            photonView.RPC("Set_MyHeadName", RpcTarget.All);
        }

        [PunRPC]
        public void Set_MyHeadName() // Myプレイヤー名をキャラの頭上に表示
        {
            Debug.Log(" Myプレイヤー名をキャラの頭上に表示します");
            Debug.Log("私の名前は 「" + PhotonNetwork.NickName + " 」でござる");
            string_MyName = PhotonNetwork.NickName;
            Debug.Log("string_MyName : " + string_MyName);

            Text_MyHeadName = GameObject.FindWithTag("MyHeadName");
            // Text_MyHeadName = GameObject.FindWithTag("MyHeadName");
            Text_MyHeadName.GetComponent<Text>().text = string_MyName;
        }
        */

        public void Set_PNameTextAll()
        {
            /*
            Text_PName1.text = string_PlayerID_1;
            Text_PName2.text = string_PlayerID_1;
            Text_PName3.text = string_PlayerID_1;
            Text_PName4.text = string_PlayerID_1;
            */
            Text_PName1.text = string_PName1;
            Text_PName2.text = string_PName2;
            Text_PName3.text = string_PName3;
            Text_PName4.text = string_PName4;
        }
    }
}