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
        Text joinedMembersText;
        string PN1;
        string PN2;
        string PN3;
        string PN4;

        string PID1;
        string PID2;
        string PID3;
        string PID4;
        public Player[] allPlayers;

        public Text PName1;
        public Text PName2;
        public Text PName3;
        public Text PName4;

        public int ken1_1;
        public int ken1_2;

        public int ken2_1;
        public int ken2_2;

        public int ken3_1;
        public int ken3_2;

        void Start()
        {
            Debug.Log("TestRoomController 出席確認");
            allPlayers = PhotonNetwork.PlayerList; // プレイヤーの配列（自身を含む）
            Debug.Log(allPlayers.Length + ": allPlayers.Length");

            PName1 = PName1.GetComponent<Text>();
            PName2 = PName2.GetComponent<Text>();
            PName3 = PName3.GetComponent<Text>();
            PName4 = PName4.GetComponent<Text>();

            UpdateMemberList();
            PN1 = allPlayers[0].NickName;
            Debug.Log(PN1 + ": PN1");
            PID1 = allPlayers[0].UserId;
            Debug.Log(PID1 + ": PID1");
        }

        // <summary>
        // リモートプレイヤーが入室した際にコールされる
        // </summary>
        // 他プレイヤーが参加した時に呼ばれるコールバック
        public override void OnPlayerEnteredRoom(Player player)
        {
            LoginCheck(player);
        }

        public void LoginCheck(Player player)
        {
            {
                Debug.Log(allPlayers.Length + ": allPlayers.Length");
                Debug.Log(player.NickName + "が参加しました");
                UpdateMemberList();
                Debug.Log(allPlayers.Length + ": allPlayers.Length");
                UpdateMemberList();
                Debug.Log(allPlayers.Length + ": allPlayers.Length");
                Debug.Log(player.NickName + "が参加しました2");

                PN1 = allPlayers[0].NickName;
                Debug.Log(PN1 + ": PN1");
                PID1 = allPlayers[0].UserId;
                Debug.Log(PID1 + ": PID1");

                Debug.Log(allPlayers.Length + ": allPlayers.Length");

                PN2 = allPlayers[1].NickName;
                Debug.Log(PN2 + ": PN2");
                PID2 = allPlayers[1].UserId;
                Debug.Log(PID2 + ": PID2");
                Debug.Log(allPlayers.Length + ": allPlayers.Length");

                if (allPlayers.Length >= 3)
                {
                    PN3 = allPlayers[2].NickName;
                    Debug.Log(PN3 + ": PN3");
                    Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    PID3 = allPlayers[2].UserId;
                    Debug.Log(PID3 + ": PID3");
                }

                if (allPlayers.Length >= 4)
                {
                    PN4 = allPlayers[3].NickName;
                    Debug.Log(PN4 + ": PN4");
                    Debug.Log(allPlayers.Length + ": allPlayers.Length");
                    PID4 = allPlayers[3].UserId;
                    Debug.Log(PID4 + ": PID4");
                }

                //PN4 = allPlayers[3].NickName;
                Debug.Log(PN3 + ": PN3");
                Debug.Log(PN4 + ": PN4");
                Debug.Log(player.NickName + "が参加しました3");
                Debug.Log(allPlayers.Length + ": allPlayers.Length");

                PName1.text = PN1;
                PName2.text = PN2;
                PName3.text = PN3;
                PName4.text = PN4;
            }
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

    }
}
