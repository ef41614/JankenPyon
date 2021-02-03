using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using DG.Tweening;


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

        public Player[] allPlayers;

        public int int_JoinedPlayerAllNum = 0;

        public GameObject Text_MyHeadName;
        public Text Text_PName1;
        public Text Text_PName2;
        public Text Text_PName3;
        public Text Text_PName4;

        public GameObject SelectJankenManager; //ヒエラルキー上のオブジェクト名
        SelectJanken SelectJankenMSC;//スクリプト名 + このページ上でのニックネーム

        public GameObject BGM_SE_Manager;
        BGM_SE_Manager BGM_SE_MSC;

        float span = 0.1f;
        private float currentTime = 0f;

        void Start()
        {
            BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
            BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();

            //EntryCheck();
            Debug.Log("TestRoomController 出席確認");
            allPlayers = PhotonNetwork.PlayerList; // プレイヤーの配列（自身を含む）
            //Debug.Log(allPlayers.Length + ": allPlayers.Length");
            SelectJankenMSC = SelectJankenManager.GetComponent<SelectJanken>();
            /*
            Text_PName1 = Text_PName1.GetComponent<Text>();
            Text_PName2 = Text_PName2.GetComponent<Text>();
            Text_PName3 = Text_PName3.GetComponent<Text>();
            Text_PName4 = Text_PName4.GetComponent<Text>();
            */
            UpdateMemberList();
            string_PName1 = allPlayers[0].NickName;
            //Debug.Log(string_PName1 + ": string_PName1");
            string_PID1 = allPlayers[0].UserId;
            //Debug.Log(string_PID1 + ": string_PID1");
            SelectJankenMSC.Share_AcutivePlayerID();
            EntryCheck();
            PNameCheck();
            Set_PNameTextAll();
            //ToSet_MyHeadName();
        }

        void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime > span)
            {
                //Debug.Log(allPlayers.Length + ": allPlayers.Length");
                //Debug.Log(int_JoinedPlayerAllNum + ": int_JoinedPlayerAllNum");
                currentTime = 0f;
            }
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
            SelectJankenMSC.ToShareStageNo();           // ステージ情報共有（ログインしてきた他プレイヤーに、既に決定している StageNo を共有する）
        }

        public void LoginCheck(Player player)
        {
            //Debug.Log("LoginCheck（ログイン時チェック）を実施します");

            UpdateMemberList();
            //Debug.Log(allPlayers.Length + ": allPlayers.Length");
            //Debug.Log(player.NickName + "が参加しました");
            UpdateMemberList();
            //Debug.Log(allPlayers.Length + ": allPlayers.Length");
            UpdateMemberList();
            //Debug.Log(allPlayers.Length + ": allPlayers.Length");
            //Debug.Log(player.NickName + "が参加しました2");

            PNameCheck();

            //Debug.Log(player.NickName + "が参加しました3");
            //Debug.Log(allPlayers.Length + ": allPlayers.Length");

            //Debug.Log("プレイヤー名テキストをセットします");
            Set_PNameTextAll();
            //Debug.Log("プレイヤー名テキストをセットしました");

            //Debug.Log(" allPlayers.Length は "+ allPlayers.Length + " です");

            //Debug.Log(" allPlayers.Length チェック 2");
            if (allPlayers.Length >= 2)  // 参加人数 2名 は居る
            {
                //Debug.Log(" allPlayers.Length は 2以上です");
                SelectJankenMSC.ToShareNinzu_2();
            }

            //Debug.Log(" allPlayers.Length チェック 3");
            if (allPlayers.Length >= 3)  // 参加人数 3名 は居る
            {
                //Debug.Log(" allPlayers.Length は 3以上です");
                SelectJankenMSC.ToShareNinzu_3();
            }

            //Debug.Log(" allPlayers.Length チェック 4");
            if (allPlayers.Length >= 4)  // 参加人数 4名 は居る
            {
                //Debug.Log(" allPlayers.Length は 4以上です");
                SelectJankenMSC.ToShareNinzu_4();
            }
        }

        public void PNameCheck() // 現時点の参加人数を更新する（プレイヤー名が埋まっていなかったら入れる）
        {
            //Debug.Log("「PNameCheck」現時点の参加人数を更新します（プレイヤー名が埋まっていなかったら入れる）");
            if (string_PID4 != "5")
            {

                string_PName1 = allPlayers[0].NickName;
                string_PID1 = allPlayers[0].UserId;

                int_JoinedPlayerAllNum = 1;

                if (allPlayers.Length >= 2)
                {
                    string_PName2 = allPlayers[1].NickName;
                    string_PID2 = allPlayers[1].UserId;
                    int_JoinedPlayerAllNum = 2;
                }

                if (allPlayers.Length >= 3)
                {
                    string_PName3 = allPlayers[2].NickName;
                    string_PID3 = allPlayers[2].UserId;
                    int_JoinedPlayerAllNum = 3;
                }

                if (allPlayers.Length >= 4)
                {
                    string_PName4 = allPlayers[3].NickName;
                    string_PID4 = allPlayers[3].UserId;
                    int_JoinedPlayerAllNum = 4;
                }

            }
            else
            {
                //Debug.Log("string_PID4 まで 既に埋まっています");
            }
            Set_PNameTextAll();
        }

        public void EntryCheck()
        {
            //Debug.Log(" ＝＝＝ EntryCheck スタート！ ＝＝＝ ");
            SelectJankenMSC.Share_AcutivePlayerID();

            PresentMemberCheck();
            //Debug.Log(" ＝＝＝ EntryCheck 終わり！ ＝＝＝ ");
        }

        public void PresentMemberCheck()
        {
            //Debug.Log("allPlayers[0].NickName" + allPlayers[0].NickName);
            if (allPlayers.Length >= 2)  // 参加人数 2名 は居る
            {
                //Debug.Log("allPlayers[1].NickName" + allPlayers[1].NickName);
            }
            if (allPlayers.Length >= 3)  // 参加人数 3名 は居る
            {
                //Debug.Log("allPlayers[2].NickName" + allPlayers[2].NickName);
            }
            if (allPlayers.Length >= 4)  // 参加人数 4名 は居る
            {
                //Debug.Log("allPlayers[3].NickName" + allPlayers[3].NickName);
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
                //Debug.Log(player.NickName + "が退出しました");
                SelectJankenMSC.Ctrl_Check_NowLoginMember();     // 一旦全員のフラグをログアウトにして、ログインしている人から返答をもらう        
                var sequence = DOTween.Sequence();
                sequence.InsertCallback(3f, () => SelectJankenMSC.PreCheck_Can_Hantei_Stream());   //  Check_Can_Hantei_Stream に進む前の前段階のチェック
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
            int_JoinedPlayerAllNum = allPlayers.Length;
        }

        public void MyNameIs(Player player)
        {
            //Debug.Log("私の名前は 「" + player.NickName + " 」でござる");
            string_MyName = player.NickName;
            string_MyName = PhotonNetwork.NickName;

            Text_MyHeadName = GameObject.FindWithTag("MyHeadName");
            Text_MyHeadName.GetComponent<Text>().text = string_MyName;
        }

        public void Set_PNameTextAll()
        {
            Text_PName1.text = string_PName1;
            Text_PName2.text = string_PName2;
            Text_PName3.text = string_PName3;
            Text_PName4.text = string_PName4;
        }
    }
}