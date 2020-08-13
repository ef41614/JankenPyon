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
        string PN1;
        string PN2;
        string PN3;
        string PN4;

        public string PID1 = "1";
        public string PID2 = "2";
        public string PID3 = "3";
        public string PID4 = "4";

        public string PlayerID_1;
        public string PlayerID_2;
        public string PlayerID_3;
        public string PlayerID_4;
        public Player[] allPlayers;

        public Text PName1;
        public Text PName2;
        public Text PName3;
        public Text PName4;

        public SelectJanken SelectJankenMSC; //スクリプト名 + このページ上でのニックネーム

        void Start()
        {
            //EntryCheck();
            Debug.Log("TestRoomController 出席確認");
            allPlayers = PhotonNetwork.PlayerList; // プレイヤーの配列（自身を含む）
            Debug.Log(allPlayers.Length + ": allPlayers.Length");

            /*
            PName1 = PName1.GetComponent<Text>();
            PName2 = PName2.GetComponent<Text>();
            PName3 = PName3.GetComponent<Text>();
            PName4 = PName4.GetComponent<Text>();
            */
            UpdateMemberList();
            PN1 = allPlayers[0].NickName;
            Debug.Log(PN1 + ": PN1");
            PID1 = allPlayers[0].UserId;
            Debug.Log(PID1 + ": PID1");
            SelectJankenMSC.MyPlayID();
            EntryCheck();
            PNameCheck();
            SetPNameTextAll();
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
            SetPNameTextAll();
        }

        public void LoginCheck(Player player)
        {
            {
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
                PName1.text = PN1;
                PName2.text = PN2;
                PName3.text = PN3;
                PName4.text = PN4;
                */
                SetPNameTextAll();
            }
        }

        public void PNameCheck() // プレイヤー名が埋まっていなかったら入れる
        {
            if (PID4 != "5")
            {
                Debug.Log("PID4 まだ埋まっていない");
                PN1 = allPlayers[0].NickName;
                PID1 = allPlayers[0].UserId;
                Debug.Log(PID1 + ": PID1");
                Debug.Log(allPlayers.Length + ": allPlayers.Length");

                if (allPlayers.Length >= 2)
                {
                    PN2 = allPlayers[1].NickName;
                    PID2 = allPlayers[1].UserId;
                    Debug.Log(PID2 + ": PID2");
                    Debug.Log(allPlayers.Length + ": allPlayers.Length");
                }

                if (allPlayers.Length >= 3)
                {
                    PN3 = allPlayers[2].NickName;
                    Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    PID3 = allPlayers[2].UserId;
                    Debug.Log(PID3 + ": PID3");
                }

                if (allPlayers.Length >= 4)
                {
                    PN4 = allPlayers[3].NickName;
                    Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    PID4 = allPlayers[3].UserId;
                    Debug.Log(PID4 + ": PID4");
                }

                //PN4 = allPlayers[3].NickName;
                Debug.Log(PN1 + ": PN1");
                Debug.Log(PN2 + ": PN2");
                Debug.Log(PN3 + ": PN3");
                Debug.Log(PN4 + ": PN4");
            }
            else
            {
                Debug.Log("PID4 まで 既に埋まっています");
            }
            SetPNameTextAll();
        }

        public void EntryCheck()
        {
            Debug.Log(" ＝＝＝ EntryCheck スタート！ ＝＝＝ ");
            SelectJankenMSC.MyPlayID();

            if (PlayerID_1 == "")
            {
                PlayerID_1 = SelectJankenMSC.senderID;
            }
            else if (PlayerID_2 == "")
            {
                PlayerID_2 = SelectJankenMSC.senderID;
            }
            else if (PlayerID_3 == "")
            {
                PlayerID_3 = SelectJankenMSC.senderID;
            }
            else if (PlayerID_4 == "")
            {
                PlayerID_4 = SelectJankenMSC.senderID;
            }
            else
            {
                Debug.Log(" EntryCheck 不要です");
            }

            PresentMemberCheck();
            Debug.Log(" ＝＝＝ EntryCheck 終わり！ ＝＝＝ ");
        }

        public void PresentMemberCheck()
        {

            Debug.Log(PlayerID_1 + ": PlayerID_1");
            Debug.Log(PlayerID_2 + ": PlayerID_2");
            Debug.Log(PlayerID_3 + ": PlayerID_3");
            Debug.Log(PlayerID_4 + ": PlayerID_4");

            Debug.Log("allPlayers[0].NickName" + allPlayers[0].NickName);
            Debug.Log("allPlayers[0].UserId" + allPlayers[0].UserId);
            if (allPlayers.Length >= 2)
            {
                Debug.Log("allPlayers[1].NickName" + allPlayers[1].NickName);
                Debug.Log("allPlayers[1].UserId" + allPlayers[1].UserId);
            }
            if (allPlayers.Length >= 3)
            {
                Debug.Log("allPlayers[2].NickName" + allPlayers[2].NickName);
                Debug.Log("allPlayers[2].UserId" + allPlayers[2].UserId);
            }
            if (allPlayers.Length >= 4)
            {
                Debug.Log("allPlayers[3].NickName" + allPlayers[3].NickName);
                Debug.Log("allPlayers[3].UserId" + allPlayers[3].UserId);
            }
            SetPNameTextAll();
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
        }

        public void SetPNameTextAll()
        {
            /*
            PName1.text = PlayerID_1;
            PName2.text = PlayerID_1;
            PName3.text = PlayerID_1;
            PName4.text = PlayerID_1;
            */
            PName1.text = PN1;
            PName2.text = PN2;
            PName3.text = PN3;
            PName4.text = PN4;
        }
    }
}
