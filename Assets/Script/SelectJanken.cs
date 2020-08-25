using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using say;　// 対象のスクリプトの情報を取得
using BEFOOL.PhotonTest;
using DG.Tweening;
//using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SelectJanken : MonoBehaviour, IPunObservable
{
    [SerializeField]
    public int count_a = 1;
    public int NumLivePlayer = 4; // 残りのプレイヤー人数
    public int countHanteiTurn = 1; // ジャンケン勝ち負け判定のループ回数

    public Sprite sprite_Gu;
    public Sprite sprite_Choki;
    public Sprite sprite_Pa;

    public Image MyTeImg_1;
    public Image MyTeImg_2;
    public Image MyTeImg_3;
    public Image MyTeImg_4;
    public Image MyTeImg_5;

    public Image Img_CoverBlack_P1;
    public Image Img_CoverBlack_P2;
    public Image Img_CoverBlack_P3;
    public Image Img_CoverBlack_P4;

    public Image Img_Player1_Te1;
    public Image Img_Player1_Te2;
    public Image Img_Player1_Te3;
    public Image Img_Player1_Te4;
    public Image Img_Player1_Te5;

    public Image Img_Player2_Te1;
    public Image Img_Player2_Te2;
    public Image Img_Player2_Te3;
    public Image Img_Player2_Te4;
    public Image Img_Player2_Te5;

    public Image Img_Player3_Te1;
    public Image Img_Player3_Te2;
    public Image Img_Player3_Te3;
    public Image Img_Player3_Te4;
    public Image Img_Player3_Te5;

    public Image Img_Player4_Te1;
    public Image Img_Player4_Te2;
    public Image Img_Player4_Te3;
    public Image Img_Player4_Te4;
    public Image Img_Player4_Te5;


    public Text MyNumTeText_1;
    public Text MyNumTeText_2;
    public Text MyNumTeText_3;
    public Text MyNumTeText_4;
    public Text MyNumTeText_5;

    public Text Text_JankenPlayer1_Te1;
    public Text Text_JankenPlayer1_Te2;
    public Text Text_JankenPlayer1_Te3;
    public Text Text_JankenPlayer1_Te4;
    public Text Text_JankenPlayer1_Te5;

    public Text Text_JankenPlayer2_Te1;
    public Text Text_JankenPlayer2_Te2;
    public Text Text_JankenPlayer2_Te3;
    public Text Text_JankenPlayer2_Te4;
    public Text Text_JankenPlayer2_Te5;

    public Text Text_JankenPlayer3_Te1;
    public Text Text_JankenPlayer3_Te2;
    public Text Text_JankenPlayer3_Te3;
    public Text Text_JankenPlayer3_Te4;
    public Text Text_JankenPlayer3_Te5;

    public Text Text_JankenPlayer4_Te1;
    public Text Text_JankenPlayer4_Te2;
    public Text Text_JankenPlayer4_Te3;
    public Text Text_JankenPlayer4_Te4;
    public Text Text_JankenPlayer4_Te5;


    public int Int_MyJanken_Te1 = -1;
    public int Int_MyJanken_Te2 = -1;
    public int Int_MyJanken_Te3 = -1;
    public int Int_MyJanken_Te4 = -1;
    public int Int_MyJanken_Te5 = -1;

    public int int_Player1_Te1 = -1;
    public int int_Player1_Te2 = -1;
    public int int_Player1_Te3 = -1;
    public int int_Player1_Te4 = -1;
    public int int_Player1_Te5 = -1;

    public int int_Player2_Te1 = -1;
    public int int_Player2_Te2 = -1;
    public int int_Player2_Te3 = -1;
    public int int_Player2_Te4 = -1;
    public int int_Player2_Te5 = -1;

    public int int_Player3_Te1 = -1;
    public int int_Player3_Te2 = -1;
    public int int_Player3_Te3 = -1;
    public int int_Player3_Te4 = -1;
    public int int_Player3_Te5 = -1;

    public int int_Player4_Te1 = -1;
    public int int_Player4_Te2 = -1;
    public int int_Player4_Te3 = -1;
    public int int_Player4_Te4 = -1;
    public int int_Player4_Te5 = -1;

    public int KP1 = -1; // ジャンケン勝敗判定に使う、仮の値 （0：グー、1：チョキ、2：パー）
    public int KP2 = -1;
    public int KP3 = -1;
    public int KP4 = -1;

    public int alivePlayer1 = 1; // ジャンケンで残留してれば 1 、負けたら -1
    public int alivePlayer2 = 1;
    public int alivePlayer3 = 1;
    public int alivePlayer4 = 1;

    public bool NoneGu = false;
    public bool NoneChoki = false;
    public bool NonePa = false;

    public string PresentPlayerID;
    private PhotonView photonView = null;

    public GameObject ShuffleCardsManager;  //ヒエラルキー上のオブジェクト名
    ShuffleCards ShuffleCardsMSC; //スクリプト名 + このページ上でのニックネーム

    public GameObject TestRoomController;  //ヒエラルキー上のオブジェクト名
    TestRoomController TestRoomControllerSC;

    //public GameObject PushTeBtnManager; //ヒエラルキー上のオブジェクト名
    //PushTeBtn PushTeBtnMSC;
    //public PushTeBtn PushTeBtnMSC;

    public GameObject myPlayer;

    public int rap1 = 0;
    public int receiveRap1 = 0;

    public string senderName = "anonymous";
    public string senderID = "2434";
    public string MyName = "me";
    public string MyID = "0000";

    public bool isSelected_A = false;
    public bool isSelected_B = false;
    public bool isSelected_C = false;
    public bool isSelected_D = false;
    public bool isSelected_E = false;

    public Button Btn_A;
    public Button Btn_B;
    public Button Btn_C;
    public Button Btn_D;
    public Button Btn_E;

    public bool CanPushBtn_A = true;
    public bool CanPushBtn_B = true;
    public bool CanPushBtn_C = true;
    public bool CanPushBtn_D = true;
    public bool CanPushBtn_E = true;

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
        Debug.Log("SelectJanken 出席確認1");
    }

    void Start()
    {
        //var customProperties = photonView.Owner.CustomProperties;
        Debug.Log("SelectJanken 出席確認2");
        count_a = 1;
        ShuffleCardsMSC = ShuffleCardsManager.GetComponent<ShuffleCards>();
        TestRoomControllerSC = TestRoomController.GetComponent<TestRoomController>();
        myPlayer = GameObject.FindGameObjectWithTag("MyPlayer");
        ResetAlivePlayer();  // 各種カウンター リセット
    }

    #region// Hantei_Group  ジャンケン勝敗 判定 一連のグループ

    public void Hantei_Stream()
    {
        Debug.Log("ジャンケン勝敗 判定開始");
        ResetAlivePlayer();  // 各種カウンター リセット
        while (NumLivePlayer > 1) // ジャンケンで残留している人数が 2名以上であるならば
        {
            SetKP_counter();    // ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
            Syohai_Hantei();    // N回目 のループ における 残留プレイヤー同士の じゃんけん手の勝ち負けを判定 → 人数が減る
            CountLivePlayer();  // 残留しているプレイヤー人数をカウントする ： NumLivePlayer を取得
            countHanteiTurn++;  // N回目 のループ を 1 進める
            var sequence = DOTween.Sequence();
            sequence.InsertCallback(2f, () => WaitTime_2nd());
            LoopOver6thCheck();
        }
        Debug.Log("ジャンケン勝敗 判定おわり");
        WhoIsWinner();  // ジャンケン勝敗の勝利者は？
    }

    public void LoopOver6thCheck() // ループが6回目突入か？ → 突入ならジャンケンカード選択へ
    {
        if (countHanteiTurn == 6)
        {
            Debug.Log("ループが6回目突入 → ジャンケンカード選択へ");
        }
        if (countHanteiTurn >= 7)
        {
            Debug.Log("！！！ ループが7回目突入 → 要スクリプト見直し ！！！");
            NumLivePlayer = 0;
        }
    }

    public void WhoIsWinner()  // ジャンケン勝敗の勝利者は？
    {
        if (alivePlayer1 == 1)
        {
            Debug.Log("Player1 勝利");
        }
        else if (alivePlayer2 == 1)
        {
            Debug.Log("Player2 勝利");
        }
        else if (alivePlayer3 == 1)
        {
            Debug.Log("Player3 勝利");
        }
        else if (alivePlayer4 == 1)
        {
            Debug.Log("Player4 勝利");
        }
        else
        {
            Debug.Log("勝利いない？");
        }
    }

    public void WaitTime_2nd()
    {
        Debug.Log("2秒待ち");
    }

    public void ResetAlivePlayer()  // 各種カウンター リセット
    {
        alivePlayer1 = 1; // ジャンケンで残留してれば 1 、負けたら 0
        alivePlayer2 = 1;
        alivePlayer3 = 1;
        alivePlayer4 = 1;

        CloseImg_CoverBlack_All(); // ジャンケン手の黒カバーをリセット（非表示）
        
        countHanteiTurn = 1;
    }

    public void CountLivePlayer()    // 残留しているプレイヤー人数をカウントする
    {
        NumLivePlayer = alivePlayer1 + alivePlayer2 + alivePlayer3 + alivePlayer4;
        Debug.Log("NumLivePlayer 残留プレイヤー数 ： " + NumLivePlayer);
    }

    public void SetKP_counter() // ジャンケン勝ち負け判定のループ回数 に伴い、KP に一時的（仮の）値を代入する
    {
        Debug.Log("countHanteiTurn" + countHanteiTurn);
        if (countHanteiTurn == 1)
        {
            KP1 = int_Player1_Te1;
            KP2 = int_Player2_Te1;
            KP3 = int_Player3_Te1;
            KP4 = int_Player4_Te1;
        }
        else if (countHanteiTurn == 2)
        {
            KP1 = int_Player1_Te2;
            KP2 = int_Player2_Te2;
            KP3 = int_Player3_Te2;
            KP4 = int_Player4_Te2;
        }
        else if (countHanteiTurn == 3)
        {
            KP1 = int_Player1_Te3;
            KP2 = int_Player2_Te3;
            KP3 = int_Player3_Te3;
            KP4 = int_Player4_Te3;
        }
        else if (countHanteiTurn == 4)
        {
            KP1 = int_Player1_Te4;
            KP2 = int_Player2_Te4;
            KP3 = int_Player3_Te4;
            KP4 = int_Player4_Te4;
        }
        else if (countHanteiTurn == 5)
        {
            KP1 = int_Player1_Te5;
            KP2 = int_Player2_Te5;
            KP3 = int_Player3_Te5;
            KP4 = int_Player4_Te5;
        }
        else
        {
            Debug.Log("countHanteiTurn ６回 超えました");
            // 再度、ジャンケン手カードの選択をする
        }
        Debug.Log("KP1 ： " + KP1);
        Debug.Log("KP2 ： " + KP2);
        Debug.Log("KP3 ： " + KP3);
        Debug.Log("KP4 ： " + KP4);
    }

    public void Check_Gu_Existence() // NoneGu の判定を返す
    {
        bool NoneGuP1 = true;
        bool NoneGuP2 = true;
        bool NoneGuP3 = true;
        bool NoneGuP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 0) // グー ではない
            {
                NoneGuP1 = true; // グー 無し
            }
            else
            {
                NoneGuP1 = false; // グー 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneGuP1 = true; // グー無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 0) // グー ではない
            {
                NoneGuP2 = true; // グー 無し
            }
            else
            {
                NoneGuP2 = false; // グー 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneGuP2 = true; // グー無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 0) // グー ではない
            {
                NoneGuP3 = true; // グー 無し
            }
            else
            {
                NoneGuP3 = false; // グー 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneGuP3 = true; // グー無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 0) // グー ではない
            {
                NoneGuP4 = true; // グー 無し
            }
            else
            {
                NoneGuP4 = false; // グー 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneGuP4 = true; // グー無しON
        }

        if (NoneGuP1 && NoneGuP2 && NoneGuP3 && NoneGuP4)
        {
            NoneGu = true; // グー無しON
        }
        else
        {
            NoneGu = false; // グー無しOFF
        }
        Debug.Log("NoneGuP1 ： " + NoneGuP1);
        Debug.Log("NoneGuP2 ： " + NoneGuP2);
        Debug.Log("NoneGuP3 ： " + NoneGuP3);
        Debug.Log("NoneGuP4 ： " + NoneGuP4);
    }

    public void Check_Choki_Existence() // NoneChoki の判定を返す
    {
        bool NoneChokiP1 = true;
        bool NoneChokiP2 = true;
        bool NoneChokiP3 = true;
        bool NoneChokiP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 1) // チョキ ではない
            {
                NoneChokiP1 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP1 = false; // チョキ 有り
            }
        }
        else  // Player1 が脱落後
        {
            NoneChokiP1 = true; // チョキ無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 1) // チョキ ではない
            {
                NoneChokiP2 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP2 = false; // チョキ 有り
            }
        }
        else  // Player2 が脱落後
        {
            NoneChokiP2 = true; // チョキ無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 1) // チョキ ではない
            {
                NoneChokiP3 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP3 = false; // チョキ 有り
            }
        }
        else  // Player3 が脱落後
        {
            NoneChokiP3 = true; // チョキ無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 1) // チョキ ではない
            {
                NoneChokiP4 = true; // チョキ 無し
            }
            else
            {
                NoneChokiP4 = false; // チョキ 有り
            }
        }
        else  // Player4 が脱落後
        {
            NoneChokiP4 = true; // チョキ無しON
        }

        if (NoneChokiP1 && NoneChokiP2 && NoneChokiP3 && NoneChokiP4)
        {
            NoneChoki = true; // チョキ無しON
        }
        else
        {
            NoneChoki = false; // チョキ無しOFF
        }
        Debug.Log("NoneChokiP1 ： " + NoneChokiP1);
        Debug.Log("NoneChokiP2 ： " + NoneChokiP2);
        Debug.Log("NoneChokiP3 ： " + NoneChokiP3);
        Debug.Log("NoneChokiP4 ： " + NoneChokiP4);
    }

    public void Check_Pa_Existence() // NonePa の判定を返す
    {
        bool NonePaP1 = true;
        bool NonePaP2 = true;
        bool NonePaP3 = true;
        bool NonePaP4 = true;

        if (alivePlayer1 == 1) // Player1 が生きている
        {
            if (KP1 != 2) // パー ではない
            {
                NonePaP1 = true; // パー 無し
            }
            else
            {
                NonePaP1 = false; // パー 有り
            }
        }
        else  // Player1 が脱落後
        {
            NonePaP1 = true; // パー無しON
        }

        if (alivePlayer2 == 1) // Player2 が生きている
        {
            if (KP2 != 2) // パー ではない
            {
                NonePaP2 = true; // パー 無し
            }
            else
            {
                NonePaP2 = false; // パー 有り
            }
        }
        else  // Player2 が脱落後
        {
            NonePaP2 = true; // パー無しON
        }

        if (alivePlayer3 == 1) // Player3 が生きている
        {
            if (KP3 != 2) // パー ではない
            {
                NonePaP3 = true; // パー 無し
            }
            else
            {
                NonePaP3 = false; // パー 有り
            }
        }
        else  // Player3 が脱落後
        {
            NonePaP3 = true; // パー無しON
        }

        if (alivePlayer4 == 1) // Player4 が生きている
        {
            if (KP4 != 2) // パー ではない
            {
                NonePaP4 = true; // パー 無し
            }
            else
            {
                NonePaP4 = false; // パー 有り
            }
        }
        else  // Player4 が脱落後
        {
            NonePaP4 = true; // パー無しON
        }

        if (NonePaP1 && NonePaP2 && NonePaP3 && NonePaP4)
        {
            NonePa = true; // パー無しON
        }
        else
        {
            NonePa = false; // パー無しOFF
        }
        Debug.Log("NonePaP1 ： " + NonePaP1);
        Debug.Log("NonePaP2 ： " + NonePaP2);
        Debug.Log("NonePaP3 ： " + NonePaP3);
        Debug.Log("NonePaP4 ： " + NonePaP4);
    }

    public void Syohai_Hantei()  // N回目 のループ における 残留プレイヤー同士の じゃんけん手の勝ち負けを判定 → 人数が減る
    {
        Debug.Log("Syohai_Hantei スタート");
        Check_Gu_Existence();     // N回目の すべてのプレイヤーの手 の中に グー(0)   があるか ： NoneGu を取得
        Check_Choki_Existence();  // N回目の すべてのプレイヤーの手 の中に チョキ(1) があるか ： NoneChoki を取得
        Check_Pa_Existence();     // N回目の すべてのプレイヤーの手 の中に パー(2)   があるか ： NonePa を取得

        if (NoneGu) // 全員 Gu 無し (ちょき か ぱー)
        {
            Debug.Log("(NoneGu) です");
            if (NoneChoki) // 全員 Choki 無し (ぱー のみ)
            {
                Aiko(); // ぱー のみ
            }
            else // ちょき か ぱー
            {
                Win_Choki();
                Lose_Pa();    // ぱー の人のみ 脱落
            }
        }
        else if (NoneChoki) //(全員 Choki 無し) ぐー か ぱー （↓これ以降、ぐー は必ずある）
        {
            Debug.Log("(NoneChoki) です");
            if (NonePa) //(全員 Pa 無し) ぐー のみ
            {
                Aiko(); // ぐー のみ
            }
            else // ぐー か ぱー
            {
                Win_Pa();
                Lose_Gu();  // ぐー の人のみ 脱落
            }
        }
        else if (NonePa) //(全員 Pa 無し)  ぐー か ちょき （↓これ以降、ぐー と ちょき は必ずある）
        {
            Debug.Log("(NonePa) です");
            if (NoneGu) //(全員 Gu 無し) ちょき のみ
            {
                Aiko(); // ちょき のみ
            }
            else // ぐー か ちょき
            {
                Win_Gu();
                Lose_Choki();  // ちょき の人のみ 脱落
            }
        }
        else // ぐー か ちょき か ぱー（↓これ以降、ぐー と ちょき と ぱー は必ずある）
        {
            Aiko(); // ぐー ちょき ぱー 全部
        }
    }

    public void Aiko()
    {
        // 残っている人、全員残留
        Debug.Log("あいこ です");
    }

    public void Win_Gu()
    {
        // ぐー の人のみ 残留
    }

    public void Win_Choki()
    {
        // ちょき の人のみ 残留
    }

    public void Win_Pa()
    {
        // ぱー の人のみ 残留
    }

    public void Lose_Gu()   // ぐー の人のみ 脱落
    {
        if (KP1 == 0)
        {
            alivePlayer1 = 0;
            AppearImg_CoverBlack_P1();
        }
        if (KP2 == 0)
        {
            alivePlayer2 = 0;
            AppearImg_CoverBlack_P2();
        }
        if (KP3 == 0)
        {
            alivePlayer3 = 0;
            AppearImg_CoverBlack_P3();
        }
        if (KP4 == 0)
        {
            alivePlayer4 = 0;
            AppearImg_CoverBlack_P4();
        }
    }

    public void Lose_Choki()  // ちょき の人のみ 脱落
    {
        if (KP1 == 1)
        {
            alivePlayer1 = 0;
            AppearImg_CoverBlack_P1();
        }
        if (KP2 == 1)
        {
            alivePlayer2 = 0;
            AppearImg_CoverBlack_P2();
        }
        if (KP3 == 1)
        {
            alivePlayer3 = 0;
            AppearImg_CoverBlack_P3();
        }
        if (KP4 == 1)
        {
            alivePlayer4 = 0;
            AppearImg_CoverBlack_P4();
        }
    }

    public void Lose_Pa()  // ぱー の人のみ 脱落
    {
        if (KP1 == 2)
        {
            alivePlayer1 = 0;
            AppearImg_CoverBlack_P1();
        }
        if (KP2 == 2)
        {
            alivePlayer2 = 0;
            AppearImg_CoverBlack_P2();
        }
        if (KP3 == 2)
        {
            alivePlayer3 = 0;
            AppearImg_CoverBlack_P3();
        }
        if (KP4 == 2)
        {
            alivePlayer4 = 0;
            AppearImg_CoverBlack_P4();
        }
    }


    public void AppearImg_CoverBlack_P1() // 黒カバー 表示させる
    {
        Img_CoverBlack_P1.enabled = true;
    }

    public void AppearImg_CoverBlack_P2() // 黒カバー 表示させる
    {
        Img_CoverBlack_P2.enabled = true;
    }

    public void AppearImg_CoverBlack_P3() // 黒カバー 表示させる
    {
        Img_CoverBlack_P3.enabled = true;
    }

    public void AppearImg_CoverBlack_P4() // 黒カバー 表示させる
    {
        Img_CoverBlack_P4.enabled = true;
    }


    public void CloseImg_CoverBlack_All()
    {
        CloseImg_CoverBlack_P1();
        CloseImg_CoverBlack_P2();
        CloseImg_CoverBlack_P3();
        CloseImg_CoverBlack_P4();
    }

    public void CloseImg_CoverBlack_P1() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P1.enabled = false;
    }

    public void CloseImg_CoverBlack_P2() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P2.enabled = false;
    }

    public void CloseImg_CoverBlack_P3() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P3.enabled = false;
    }

    public void CloseImg_CoverBlack_P4() // 黒カバー 非表示にする（デフォルトに戻す）
    {
        Img_CoverBlack_P4.enabled = false;
    }

    #endregion


    public void OnMouseDown()
    {
        // photonView.RPC("SelectJankenCard", RpcTarget.All);
    }

    public void MyNameIs(Player player)
    {
        Debug.Log("私の名前は 「" + player.NickName + " 」でござる");
    }

    public void MyPlayID() // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    {
        // TestRoomControllerSC.PNameCheck();
        photonView.RPC("PlayerIDCheck", RpcTarget.All);
    }

    /// <summary>
    /// 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
    /// </summary>
    /// <param name="mi">現プレイヤー名とプレイヤーID 取得、共有</param>
    [PunRPC]
    public void PlayerIDCheck(PhotonMessageInfo mi)
    {
        Debug.Log("[PunRPC] PlayerIDCheck");
        //string senderName = "anonymous";
        //string senderID = "2434";
        if (mi.Sender != null)
        {
            senderName = mi.Sender.NickName;
            senderID = mi.Sender.UserId;
            MyName = senderName;
            MyID = senderID;
        }
        Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("MyName  " + MyName);  // 今ボタン押した人
        Debug.Log("MyID  " + MyID);  // 今ボタン押した人
    }

    public void ToSharePlayerTeNum()
    {
        TestRoomControllerSC.PNameCheck(); // プレイヤー名が埋まっていなかったら入れる
        MyPlayID();  // 現在操作している人のプレイヤー名とプレイヤーIDを取得し、共有する
        //photonView.RPC("SharePlayerTeNum", RpcTarget.All);
        SharePlayerTeNum();
    }

    //[PunRPC]
    public void SharePlayerTeNum()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Debug.Log("************ データ共有 SharePlayerTeNum **********");
        Debug.Log(TestRoomControllerSC.string_PID1 + ": TestRoomControllerSC.string_PID1");
        Debug.Log(TestRoomControllerSC.string_PID2 + ": TestRoomControllerSC.string_PID2");
        Debug.Log(TestRoomControllerSC.string_PID3 + ": TestRoomControllerSC.string_PID3");
        Debug.Log(TestRoomControllerSC.string_PID4 + ": TestRoomControllerSC.string_PID4");
        Debug.Log("senderName  " + senderName);  // 今ボタン押した人
        Debug.Log("senderID  " + senderID);  // 今ボタン押した人
        Debug.Log("MyName  " + MyName);  // 今ボタン押した人
        Debug.Log("MyID  " + MyID);  // 今ボタン押した人

        //Debug.Log("PhotonNetwork.player.ID  " + PhotonNetwork.player.ID);  // 今ボタン押した人


        if (senderID == TestRoomControllerSC.string_PID1)
        {
            Debug.Log("現在プレイヤー1がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            SharePlayerTeNum_Player1();
        }

        if (senderID == TestRoomControllerSC.string_PID2)
        {
            Debug.Log("現在プレイヤー2がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            SharePlayerTeNum_Player2();
        }

        if (senderID == TestRoomControllerSC.string_PID3)
        {
            Debug.Log("現在プレイヤー3がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            SharePlayerTeNum_Player3();
        }

        if (senderID == TestRoomControllerSC.string_PID4)
        {
            Debug.Log("現在プレイヤー4がボタン押したよ");
            Debug.Log("MyName  " + MyName);  // 今ボタン押した人
            SharePlayerTeNum_Player4();
        }

        Debug.Log("************ データ共有 SharePlayerTeNum おわり **********");
    }


    public void ResetPlayerTeNum() // Player1 ～ Player4 のじゃんけん手 数値を -1 にリセット（int,text）
    {
        int_Player1_Te1 = -1;
        int_Player1_Te2 = -1;
        int_Player1_Te3 = -1;
        int_Player1_Te4 = -1;
        int_Player1_Te5 = -1;

        int_Player2_Te1 = -1;
        int_Player2_Te2 = -1;
        int_Player2_Te3 = -1;
        int_Player2_Te4 = -1;
        int_Player2_Te5 = -1;

        int_Player3_Te1 = -1;
        int_Player3_Te2 = -1;
        int_Player3_Te3 = -1;
        int_Player3_Te4 = -1;
        int_Player3_Te5 = -1;

        int_Player4_Te1 = -1;
        int_Player4_Te2 = -1;
        int_Player4_Te3 = -1;
        int_Player4_Te4 = -1;
        int_Player4_Te5 = -1;

        Text_JankenPlayer1_Te1.text = "-1";
        Text_JankenPlayer1_Te2.text = "-1";
        Text_JankenPlayer1_Te3.text = "-1";
        Text_JankenPlayer1_Te4.text = "-1";
        Text_JankenPlayer1_Te5.text = "-1";

        Text_JankenPlayer2_Te1.text = "-1";
        Text_JankenPlayer2_Te2.text = "-1";
        Text_JankenPlayer2_Te3.text = "-1";
        Text_JankenPlayer2_Te4.text = "-1";
        Text_JankenPlayer2_Te5.text = "-1";

        Text_JankenPlayer3_Te1.text = "-1";
        Text_JankenPlayer3_Te2.text = "-1";
        Text_JankenPlayer3_Te3.text = "-1";
        Text_JankenPlayer3_Te4.text = "-1";
        Text_JankenPlayer3_Te5.text = "-1";

        Text_JankenPlayer4_Te1.text = "-1";
        Text_JankenPlayer4_Te2.text = "-1";
        Text_JankenPlayer4_Te3.text = "-1";
        Text_JankenPlayer4_Te4.text = "-1";
        Text_JankenPlayer4_Te5.text = "-1";

        Debug.Log("PlayerTeNum を すべて「」リセットしました");
    }

    [PunRPC]
    public void CheckPlayerTeNum()
    {
        Debug.Log("************ CheckPlayerTeNum *********** **********");

        Debug.Log("***  Player1  ***********");
        Debug.Log("int_Player1_Te1 " + int_Player1_Te1);
        Debug.Log("int_Player1_Te2 " + int_Player1_Te2);
        Debug.Log("int_Player1_Te3 " + int_Player1_Te3);
        Debug.Log("int_Player1_Te4 " + int_Player1_Te4);
        Debug.Log("int_Player1_Te5 " + int_Player1_Te5);

        Debug.Log("***  Player2  ***********");
        Debug.Log("int_Player2_Te1 " + int_Player2_Te1);
        Debug.Log("int_Player2_Te2 " + int_Player2_Te2);
        Debug.Log("int_Player2_Te3 " + int_Player2_Te3);
        Debug.Log("int_Player2_Te4 " + int_Player2_Te4);
        Debug.Log("int_Player2_Te5 " + int_Player2_Te5);

        Debug.Log("***  Player3  ***********");
        Debug.Log("int_Player3_Te1 " + int_Player3_Te1);
        Debug.Log("int_Player3_Te2 " + int_Player3_Te2);
        Debug.Log("int_Player3_Te3 " + int_Player3_Te3);
        Debug.Log("int_Player3_Te4 " + int_Player3_Te4);
        Debug.Log("int_Player3_Te5 " + int_Player3_Te5);

        Debug.Log("***  Player4  ***********");
        Debug.Log("int_Player4_Te1 " + int_Player4_Te1);
        Debug.Log("int_Player4_Te2 " + int_Player4_Te2);
        Debug.Log("int_Player4_Te3 " + int_Player4_Te3);
        Debug.Log("int_Player4_Te4 " + int_Player4_Te4);
        Debug.Log("int_Player4_Te5 " + int_Player4_Te5);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
    // オーナーの場合
    if (stream.IsWriting)
    {
        stream.SendNext(int_Player1_Te1);
        stream.SendNext(int_Player2_Te1);
        if (TestRoomControllerSC.allPlayers.Length >= 3)
        {
            stream.SendNext(int_Player3_Te1);
        }
        if (TestRoomControllerSC.allPlayers.Length >= 4)
        {
            stream.SendNext(int_Player4_Te1);
        }
    }
    // オーナー以外の場合
    else
    {
        this.receiveint_Player1_Te1 = (int)stream.ReceiveNext();
        SelectJankenMSC.int_Player1_Te1 = receiveint_Player1_Te1;
        this.receiveint_Player2_Te1 = (int)stream.ReceiveNext();
        SelectJankenMSC.int_Player2_Te1 = receiveint_Player2_Te1;
        if (TestRoomControllerSC.allPlayers.Length >= 3)
        {
            this.receiveint_Player3_Te1 = (int)stream.ReceiveNext();
            SelectJankenMSC.int_Player3_Te1 = receiveint_Player3_Te1;
        }
        if (TestRoomControllerSC.allPlayers.Length >= 4)
        {
            this.receiveint_Player4_Te1 = (int)stream.ReceiveNext();
            SelectJankenMSC.int_Player4_Te1 = receiveint_Player4_Te1;
        }
    }
    */
    }


    private void RequestOwner()
    {
        if (this.photonView.IsMine == false)
        {
            if (this.photonView.OwnershipTransfer != OwnershipOption.Request)
                Debug.LogError("OwnershipTransferをRequestに変更してください。");
            else
                this.photonView.RequestOwnership();
        }
    }

    public void Reset_MyRireki_All() // Image
    {
        MyTeImg_1.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_2.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_3.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_4.gameObject.GetComponent<Image>().sprite = null;
        MyTeImg_5.gameObject.GetComponent<Image>().sprite = null;
    }

    public void ResetImg_PlayerlayerRireki_All() // Image
    {
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = null;

        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = null;

        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = null;

        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = null;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = null;
    }

    public void GetMyJankenNum()
    {
        //ReInt_MyJanken_Te4 = PushTeBtnMSC.Int_MyJanken_Te1;
    }

    #region// Num_Player1
    #region// PlayerTeNum_Player1_1
    public void ToSharePlayerTeNum_Player1_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Gu()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player1_Te1 = 0;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Choki()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player1_Te1 = 1;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_1_is_Pa()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player1_Te1 = 2;

        // Image の反映
        Img_Player1_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer1_Te1.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player1_2
    public void ToSharePlayerTeNum_Player1_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Gu()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te2 = 0;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Choki()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te2 = 1;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_2_is_Pa()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te2 = 2;
        Img_Player1_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te2.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player1_3
    public void ToSharePlayerTeNum_Player1_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Gu()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te3 = 0;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Choki()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te3 = 1;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_3_is_Pa()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te3 = 2;
        Img_Player1_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te3.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player1_4
    public void ToSharePlayerTeNum_Player1_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Gu()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te4 = 0;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Choki()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te4 = 1;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_4_is_Pa()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te4 = 2;
        Img_Player1_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te4.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player1_5
    public void ToSharePlayerTeNum_Player1_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Gu()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player1_Te5 = 0;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer1_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Choki()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player1_Te5 = 1;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer1_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player1_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player1_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player1_5_is_Pa()  //現在プレイしているのが「プレイヤー1」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player1_Te5 = 2;
        Img_Player1_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer1_Te5.text = "2";
    }
    #endregion
    #endregion

    #region// Num_Player2
    #region// PlayerTeNum_Player2_1
    public void ToSharePlayerTeNum_Player2_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Gu()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player2_Te1 = 0;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Choki()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player2_Te1 = 1;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_1_is_Pa()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player2_Te1 = 2;

        // Image の反映
        Img_Player2_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer2_Te1.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_2
    public void ToSharePlayerTeNum_Player2_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Gu()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te2 = 0;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Choki()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te2 = 1;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_2_is_Pa()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te2 = 2;
        Img_Player2_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te2.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_3
    public void ToSharePlayerTeNum_Player2_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Gu()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te3 = 0;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Choki()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te3 = 1;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_3_is_Pa()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te3 = 2;
        Img_Player2_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te3.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_4
    public void ToSharePlayerTeNum_Player2_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Gu()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te4 = 0;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Choki()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te4 = 1;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_4_is_Pa()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te4 = 2;
        Img_Player2_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te4.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player2_5
    public void ToSharePlayerTeNum_Player2_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Gu()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player2_Te5 = 0;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer2_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Choki()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player2_Te5 = 1;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer2_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player2_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player2_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player2_5_is_Pa()  //現在プレイしているのが「プレイヤー2」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player2_Te5 = 2;
        Img_Player2_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer2_Te5.text = "2";
    }
    #endregion
    #endregion

    #region// Num_Player3
    #region// PlayerTeNum_Player3_1
    public void ToSharePlayerTeNum_Player3_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Gu()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player3_Te1 = 0;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Choki()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player3_Te1 = 1;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_1_is_Pa()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player3_Te1 = 2;

        // Image の反映
        Img_Player3_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer3_Te1.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player3_2
    public void ToSharePlayerTeNum_Player3_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Gu()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te2 = 0;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Choki()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te2 = 1;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_2_is_Pa()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te2 = 2;
        Img_Player3_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te2.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player3_3
    public void ToSharePlayerTeNum_Player3_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Gu()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te3 = 0;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Choki()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te3 = 1;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_3_is_Pa()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te3 = 2;
        Img_Player3_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te3.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player3_4
    public void ToSharePlayerTeNum_Player3_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Gu()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te4 = 0;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Choki()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te4 = 1;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_4_is_Pa()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te4 = 2;
        Img_Player3_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te4.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player3_5
    public void ToSharePlayerTeNum_Player3_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Gu()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player3_Te5 = 0;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer3_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Choki()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player3_Te5 = 1;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer3_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player3_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player3_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player3_5_is_Pa()  //現在プレイしているのが「プレイヤー3」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player3_Te5 = 2;
        Img_Player3_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer3_Te5.text = "2";
    }
    #endregion
    #endregion

    #region// Num_Player4
    #region// PlayerTeNum_Player4_1
    public void ToSharePlayerTeNum_Player4_1_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Gu()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        // int の反映
        int_Player4_Te1 = 0;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Gu;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Choki()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        // int の反映
        int_Player4_Te1 = 1;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Choki;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_1_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_1_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_1_is_Pa()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        // int の反映
        int_Player4_Te1 = 2;

        // Image の反映
        Img_Player4_Te1.gameObject.GetComponent<Image>().sprite = sprite_Pa;

        // Text の反映
        Text_JankenPlayer4_Te1.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player4_2
    public void ToSharePlayerTeNum_Player4_2_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Gu()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te2 = 0;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te2.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Choki()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te2 = 1;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te2.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_2_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_2_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_2_is_Pa()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te2 = 2;
        Img_Player4_Te2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te2.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player4_3
    public void ToSharePlayerTeNum_Player4_3_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Gu()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te3 = 0;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te3.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Choki()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te3 = 1;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te3.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_3_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_3_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_3_is_Pa()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te3 = 2;
        Img_Player4_Te3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te3.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player4_4
    public void ToSharePlayerTeNum_Player4_4_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Gu()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te4 = 0;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te4.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Choki()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te4 = 1;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te4.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_4_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_4_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_4_is_Pa()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te4 = 2;
        Img_Player4_Te4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te4.text = "2";
    }
    #endregion

    #region// PlayerTeNum_Player4_5
    public void ToSharePlayerTeNum_Player4_5_is_Gu()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Gu", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Gu()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（0：グー）
    {
        int_Player4_Te5 = 0;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
        Text_JankenPlayer4_Te5.text = "0";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Choki()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Choki", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Choki()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（1:チョキ）
    {
        int_Player4_Te5 = 1;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
        Text_JankenPlayer4_Te5.text = "1";
    }

    public void ToSharePlayerTeNum_Player4_5_is_Pa()
    {
        photonView.RPC("SharePlayerTeNum_Player4_5_is_Pa", RpcTarget.All);
    }
    [PunRPC]
    public void SharePlayerTeNum_Player4_5_is_Pa()  //現在プレイしているのが「プレイヤー4」 + そのジャンケンの手は「PTN」（2：パー）
    {
        int_Player4_Te5 = 2;
        Img_Player4_Te5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
        Text_JankenPlayer4_Te5.text = "2";
    }
    #endregion
    #endregion


    public void SharePlayerTeNum_Player1()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player1_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player1_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player1_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player1_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player1_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player1_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player1_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player1_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player1_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player1_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player1_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player1_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player1_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player1_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player1_5_is_Pa();
        }
        /*
        // int の反映
        int_Player1_Te1 = Int_MyJanken_Te1;
        int_Player1_Te2 = Int_MyJanken_Te2;
        int_Player1_Te3 = Int_MyJanken_Te3;
        int_Player1_Te4 = Int_MyJanken_Te4;
        int_Player1_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player1_Te1.sprite = MyTeImg_1.sprite;
        Img_Player1_Te2.sprite = MyTeImg_2.sprite;
        Img_Player1_Te3.sprite = MyTeImg_3.sprite;
        Img_Player1_Te4.sprite = MyTeImg_4.sprite;
        Img_Player1_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer1_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer1_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer1_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer1_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer1_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SharePlayerTeNum_Player2()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player2_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player2_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player2_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player2_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player2_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player2_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player2_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player2_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player2_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player2_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player2_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player2_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player2_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player2_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player2_5_is_Pa();
        }
        /*
        // int の反映
        int_Player2_Te1 = Int_MyJanken_Te1;
        int_Player2_Te2 = Int_MyJanken_Te2;
        int_Player2_Te3 = Int_MyJanken_Te3;
        int_Player2_Te4 = Int_MyJanken_Te4;
        int_Player2_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player2_Te1.sprite = MyTeImg_1.sprite;
        Img_Player2_Te2.sprite = MyTeImg_2.sprite;
        Img_Player2_Te3.sprite = MyTeImg_3.sprite;
        Img_Player2_Te4.sprite = MyTeImg_4.sprite;
        Img_Player2_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer2_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer2_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer2_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer2_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer2_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SharePlayerTeNum_Player3()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player3_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player3_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player3_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player3_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player3_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player3_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player3_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player3_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player3_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player3_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player3_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player3_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player3_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player3_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player3_5_is_Pa();
        }
        /*
        // int の反映
        int_Player3_Te1 = Int_MyJanken_Te1;
        int_Player3_Te2 = Int_MyJanken_Te2;
        int_Player3_Te3 = Int_MyJanken_Te3;
        int_Player3_Te4 = Int_MyJanken_Te4;
        int_Player3_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player3_Te1.sprite = MyTeImg_1.sprite;
        Img_Player3_Te2.sprite = MyTeImg_2.sprite;
        Img_Player3_Te3.sprite = MyTeImg_3.sprite;
        Img_Player3_Te4.sprite = MyTeImg_4.sprite;
        Img_Player3_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer3_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer3_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer3_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer3_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer3_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SharePlayerTeNum_Player4()  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        if (Int_MyJanken_Te1 == 0)
        {
            ToSharePlayerTeNum_Player4_1_is_Gu();
        }
        else if (Int_MyJanken_Te1 == 1)
        {
            ToSharePlayerTeNum_Player4_1_is_Choki();
        }
        else if (Int_MyJanken_Te1 == 2)
        {
            ToSharePlayerTeNum_Player4_1_is_Pa();
        }

        if (Int_MyJanken_Te2 == 0)
        {
            ToSharePlayerTeNum_Player4_2_is_Gu();
        }
        else if (Int_MyJanken_Te2 == 1)
        {
            ToSharePlayerTeNum_Player4_2_is_Choki();
        }
        else if (Int_MyJanken_Te2 == 2)
        {
            ToSharePlayerTeNum_Player4_2_is_Pa();
        }

        if (Int_MyJanken_Te3 == 0)
        {
            ToSharePlayerTeNum_Player4_3_is_Gu();
        }
        else if (Int_MyJanken_Te3 == 1)
        {
            ToSharePlayerTeNum_Player4_3_is_Choki();
        }
        else if (Int_MyJanken_Te3 == 2)
        {
            ToSharePlayerTeNum_Player4_3_is_Pa();
        }

        if (Int_MyJanken_Te4 == 0)
        {
            ToSharePlayerTeNum_Player4_4_is_Gu();
        }
        else if (Int_MyJanken_Te4 == 1)
        {
            ToSharePlayerTeNum_Player4_4_is_Choki();
        }
        else if (Int_MyJanken_Te4 == 2)
        {
            ToSharePlayerTeNum_Player4_4_is_Pa();
        }

        if (Int_MyJanken_Te5 == 0)
        {
            ToSharePlayerTeNum_Player4_5_is_Gu();
        }
        else if (Int_MyJanken_Te5 == 1)
        {
            ToSharePlayerTeNum_Player4_5_is_Choki();
        }
        else if (Int_MyJanken_Te5 == 2)
        {
            ToSharePlayerTeNum_Player4_5_is_Pa();
        }
        /*
        // int の反映
        int_Player4_Te1 = Int_MyJanken_Te1;
        int_Player4_Te2 = Int_MyJanken_Te2;
        int_Player4_Te3 = Int_MyJanken_Te3;
        int_Player4_Te4 = Int_MyJanken_Te4;
        int_Player4_Te5 = Int_MyJanken_Te5;
        // Image の反映
        Img_Player4_Te1.sprite = MyTeImg_1.sprite;
        Img_Player4_Te2.sprite = MyTeImg_2.sprite;
        Img_Player4_Te3.sprite = MyTeImg_3.sprite;
        Img_Player4_Te4.sprite = MyTeImg_4.sprite;
        Img_Player4_Te5.sprite = MyTeImg_5.sprite;
        // Text の反映
        Text_JankenPlayer4_Te1.text = Int_MyJanken_Te1.ToString();
        Text_JankenPlayer4_Te2.text = Int_MyJanken_Te2.ToString();
        Text_JankenPlayer4_Te3.text = Int_MyJanken_Te3.ToString();
        Text_JankenPlayer4_Te4.text = Int_MyJanken_Te4.ToString();
        Text_JankenPlayer4_Te5.text = Int_MyJanken_Te5.ToString();
        */
    }

    public void SelectGu()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_1.text = "0";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_2.text = "0";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_3.text = "0";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_4.text = "0";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Gu;
            MyNumTeText_5.text = "0";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(0); //手をグーにセット
        Debug.Log("手をグーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 0);
        PlayerTeNumSet(0);
        Debug.Log("手をグーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectChoki()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_1.text = "1";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_2.text = "1";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_3.text = "1";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_4.text = "1";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Choki;
            MyNumTeText_5.text = "1";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(1); //手をチョキにセット
        Debug.Log("手をチョキにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All, 1);
        PlayerTeNumSet(1);
        Debug.Log("手をチョキにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void SelectPa()
    {
        Debug.Log(count_a + ": count_a");

        if (count_a == 1)
        {
            MyTeImg_1.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_1.text = "2";
        }
        else if (count_a == 2)
        {
            MyTeImg_2.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_2.text = "2";
        }
        else if (count_a == 3)
        {
            MyTeImg_3.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_3.text = "2";
        }
        else if (count_a == 4)
        {
            MyTeImg_4.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_4.text = "2";
        }
        else if (count_a == 5)
        {
            MyTeImg_5.gameObject.GetComponent<Image>().sprite = sprite_Pa;
            MyNumTeText_5.text = "2";
        }
        else
        {
            Debug.Log("count_a 6以上");
        }
        //PlayerTeNumSet(2); //手をパーにセット
        Debug.Log("手をパーにセット");
        //photonView.RPC("PlayerTeNumSet", RpcTarget.All,2);
        PlayerTeNumSet(2);
        Debug.Log("手をパーにセットend");

        count_a++;
        Debug.Log(count_a + ": count_a");
    }

    public void PlayerTeNumSet(int PTN)  //現在プレイしているのが「プレイヤーX」 + そのジャンケンの手は「PTN」（0：グー、1：チョキ、2：パー）
    {
        Debug.Log("************ ********** *********** **********");
        Debug.Log(PTN + ": PTN");

        Debug.Log("現在プレイヤー1がボタン押したよ");
        if (Int_MyJanken_Te1 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Debug.Log("Int_MyJanken_Te1 代入前" + Int_MyJanken_Te1);
            Int_MyJanken_Te1 = PTN;
            Debug.Log("Int_MyJanken_Te1 代入後" + Int_MyJanken_Te1);
            Debug.Log("プレイヤー1_1 手のセットOK");
        }
        else if (Int_MyJanken_Te2 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te2 = PTN;
            Debug.Log("プレイヤー1_2 手のセットOK");
        }
        else if (Int_MyJanken_Te3 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te3 = PTN;
            Debug.Log("プレイヤー1_3 手のセットOK");
        }
        else if (Int_MyJanken_Te4 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te4 = PTN;
            Debug.Log("プレイヤー1_4 手のセットOK");
        }
        else if (Int_MyJanken_Te5 == -1) //手がまだ決まっていなければ（デフォルト値ならば）
        {
            Int_MyJanken_Te5 = PTN;
            Debug.Log("プレイヤー1_5 手のセットOK");
        }
        else
        {
            Debug.Log("現在プレイヤー1の 5こすべて手が決まったよ");
        }
    }


    #region// じゃんけんボタン 押した時の処理（フラグを処理済みにする）

    public void Push_Btn_A() // ボタン押したよ
    {
        if (CanPushBtn_A)
        {
            Debug.Log("RndCreateCard_A ： " + ShuffleCardsMSC.RndCreateCard_A);
            if (ShuffleCardsMSC.RndCreateCard_A == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_A == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_A.interactable = false;
        CanPushBtn_A = false;
    }

    public void Push_Btn_B() // ボタン押したよ
    {
        if (CanPushBtn_B)
        {
            Debug.Log("RndCreateCard_B ： " + ShuffleCardsMSC.RndCreateCard_B);
            if (ShuffleCardsMSC.RndCreateCard_B == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_B == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_B.interactable = false;
        CanPushBtn_B = false;
    }

    public void Push_Btn_C() // ボタン押したよ
    {
        if (CanPushBtn_C)
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
        Btn_C.interactable = false;
        CanPushBtn_C = false;
    }

    public void Push_Btn_D() // ボタン押したよ
    {
        if (CanPushBtn_D)
        {
            Debug.Log("RndCreateCard_D ： " + ShuffleCardsMSC.RndCreateCard_D);
            if (ShuffleCardsMSC.RndCreateCard_D == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_D == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_D.interactable = false;
        CanPushBtn_D = false;
    }

    public void Push_Btn_E() // ボタン押したよ
    {
        if (CanPushBtn_E)
        {
            Debug.Log("RndCreateCard_E ： " + ShuffleCardsMSC.RndCreateCard_E);
            if (ShuffleCardsMSC.RndCreateCard_E == 0) //グー
            {
                SelectGu();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 1) //チョキ
            {
                SelectChoki();
            }
            else if (ShuffleCardsMSC.RndCreateCard_E == 2) //パー
            {
                SelectPa();
            }
            else
            {
                Debug.Log("ランダム値の見直しが必要！！");
            }
        }
        Btn_E.interactable = false;
        CanPushBtn_E = false;
    }

    #endregion


    #region// じゃんけんボタン ボタン押せるようにする(フラグのリセット）

    public void ToCanPush_All()
    {
        ToCanPush_A();
        ToCanPush_B();
        ToCanPush_C();
        ToCanPush_D();
        ToCanPush_E();
    }

    public void ToCanPush_A() // ボタン押せるようにするよ
    {
        Btn_A.interactable = true;
        CanPushBtn_A = true;
    }

    public void ToCanPush_B() // ボタン押せるようにするよ
    {
        Btn_B.interactable = true;
        CanPushBtn_B = true;
    }

    public void ToCanPush_C() // ボタン押せるようにするよ
    {
        Btn_C.interactable = true;
        CanPushBtn_C = true;
    }

    public void ToCanPush_D() // ボタン押せるようにするよ
    {
        Btn_D.interactable = true;
        CanPushBtn_D = true;
    }

    public void ToCanPush_E() // ボタン押せるようにするよ
    {
        Btn_E.interactable = true;
        CanPushBtn_E = true;
    }

    #endregion

    #region// じゃんけん手ナンバー リセット

    public void ResetMyNumTe_All()  // 数値を -1 にリセット（int,text）
    {
        count_a = 1;

        MyNumTeText_1.text = "-1";
        MyNumTeText_2.text = "-1";
        MyNumTeText_3.text = "-1";
        MyNumTeText_4.text = "-1";
        MyNumTeText_5.text = "-1";

        Int_MyJanken_Te1 = -1;
        Int_MyJanken_Te2 = -1;
        Int_MyJanken_Te3 = -1;
        Int_MyJanken_Te4 = -1;
        Int_MyJanken_Te5 = -1;
    }
    #endregion

    public void ToNextTurn() // 次のターンへ移る プレイヤー1～4の履歴リセット ＆ MyJanken手 もリセット
    {
        Debug.Log("ToNextTurn() // 次のターンへ移る");
        ResetMyNumTe_All();      // MyNumTe 数値を -1 にリセット（int,text）
        Reset_MyRireki_All();  // MyRireki イメージを null にリセット（Image）
        ToCanPush_All();       // じゃんけんボタン ボタン押せるようにする(フラグのリセット）（bool）
        ResetPlayerTeNum();    // Player1 ～ Player4 のじゃんけん手 数値を -1 にリセット（int,text）
        ResetImg_PlayerlayerRireki_All(); // Player1 ～ Player4 のじゃんけん手 履歴イメージを null にリセット（Image）
        ResetAlivePlayer();  // 各種カウンター リセット
    }

    // End

}