using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
//using static System.Net.Mime.MediaTypeNames;

public class CLauncherScript : MonoBehaviourPunCallbacks
{
    #region Public変数定義

    //Public変数の定義はココで
    public static int int_MyCharaAvatar = -1;
    //static int int_player1_CharaAvatar = -1;
    //static int int_player2_CharaAvatar = -1;
    //static int int_player3_CharaAvatar = -1;
    //static int int_player4_CharaAvatar = -1;

    public GameObject Utako_Button;
    public GameObject Unitychan_Button;
    public GameObject Pchan_Button;
    public GameObject Mobuchan_Button;
    public GameObject Zunko_Button;
    public GameObject Yuzuru_Button;  // yuzuru用のボタン    

    public GameObject PlayStartButton_Panel;

    public GameObject BGM_SE_Manager;
    BGM_SE_Manager BGM_SE_MSC;

    public GameObject Volume_Panel;
    public GameObject Credit_Panel;
    public GameObject Aikotoba_Panel;
        
    #endregion

    #region Private変数
    //Private変数の定義はココで
    private bool firstPush = false;
    #endregion

    private void Awake()
    {
        Debug.Log("****** CLauncherScript Awake **");
        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);
        BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
        BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();

        firstPush = false; //初期化
        if (PhotonNetwork.IsConnected)  // 2週目以降で既にルームに入室していたら
        {
            Debug.Log("ルームに入っていたので、一旦退出します");
            PhotonNetwork.Disconnect();
            //PhotonNetwork.LeaveRoom();
            Debug.Log("ルームから退出しました");
        }
        else
        {
            Debug.Log("今、ルームには入っていません");
        }
        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);
        //BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
        //Volume_Panel = GameObject.Find("Volume_Panel");
        //Credit_Panel = GameObject.Find("Credit_Panel");
    }

    void Start()
    {
        Debug.Log("CLauncherScript Start 出席確認");

        AppearVolume_Panel();
        //BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();
        BGM_SE_MSC.Dadadadau_BGM();
        BGM_SE_MSC.find_Vol_Panel();
        CloseVolume_Panel();
        if (BGM_SE_MSC.firstMatch == 0)
        {
            AppearVolume_Panel();
        }
        BGM_SE_MSC.firstMatch++;
        //BGM_SE_MSC.CloseVolume_Panel();
        firstPush = false; //初期化
        Reset_AvatarAll();
        ClosePlayStartButton_Panel();
        CloseCredit_Panel();
        CloseAikotoba_Panel();

        if (PhotonNetwork.IsConnected)  // 2週目以降で既にルームに入室していたら
        {
            Debug.Log("ルームに入っていたので、一旦退出します");
            PhotonNetwork.Disconnect();
            //PhotonNetwork.LeaveRoom();
            Debug.Log("ルームから退出しました");
        }
    }


    #region Public Methods
    //ログインボタンを押したときに実行される
    public void Connect()     // 「ランダムマッチ」ボタンを押した時の処理
    {
        BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
        BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();

        Debug.Log("CLauncherScript で「Play」が押されました。");
        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);
        BGM_SE_MSC.Aikotoba = "";  // あいことばをリセットする

        if (!firstPush)
        {
            Debug.Log("Connect 処理中");
            Debug.Log("PhotonNetwork.NickName Play：" + PhotonNetwork.NickName);
            if (string.IsNullOrWhiteSpace(PhotonNetwork.NickName))
            {
                Debug.Log("名前が空欄なので、名前をランダムに決めて入力します");
                int RndPName = UnityEngine.Random.Range(1, 10000);
                PhotonNetwork.NickName = "JKP_" + RndPName;     //今回ゲームで利用するプレイヤーの名前を設定
                Debug.Log("PhotonNetwork.NickName ランチャー：" + PhotonNetwork.NickName);
            }
            else    //名前が正常に入力されていたら
            {

                /*
                if (!PhotonNetwork.IsConnected)             //Photonに接続できていなければ
                {
                    Debug.Log("Photonに接続をします");
                    PhotonNetwork.ConnectUsingSettings();   //Photonに接続する
                    Debug.Log("Photonに接続しました。");
                    Debug.Log("ロビーに移動します。");
                    SceneManager.LoadScene("Mike");
                }
                else
                {
                    Debug.Log("エラーのようです");
                }
                */
            }
            BGM_SE_MSC.Stop_BGM();
            SceneManager.LoadScene("Mike");
            firstPush = true; //ボタン押下済みフラグ
        }
        else
        {
            Debug.Log("Connect 処理に失敗しました");
        }
        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);
    }

    public void Push_Aikotoba_OK()  // あいことば を入力して「OK」を押した時の処理
    {
        Debug.Log("Push_Aikotoba_OK()  // CLauncherScript で あいことば を入力して「OK」を押した時の処理");

        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);     

        if (!firstPush)
        {
            Debug.Log("Push_Aikotoba_OK 処理中");
            Debug.Log("PhotonNetwork.NickName Play：" + PhotonNetwork.NickName);
            if (string.IsNullOrWhiteSpace(PhotonNetwork.NickName))   // プレイヤー名が入力されていなかったら（空欄だったら）
            {
                Debug.Log("名前が空欄なので、名前をランダムに決めて入力します");
                int RndPName = UnityEngine.Random.Range(1, 10000);
                PhotonNetwork.NickName = "JKP_" + RndPName;          // 今回ゲームで利用するプレイヤー名を適当に設定する
                Debug.Log("PhotonNetwork.NickName ランチャー：" + PhotonNetwork.NickName);
            }

            BGM_SE_MSC.Stop_BGM();
            SceneManager.LoadScene("Mike");
            firstPush = true; //ボタン押下済みフラグ
        }
        else
        {
            Debug.Log("Push_Aikotoba_OK 処理に失敗しました");
        }
        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);
    }


    public void Select_CharaAvatar_utako()
    {
        int_MyCharaAvatar = 1;
    }

    public void Select_CharaAvatar_unitychan()
    {
        int_MyCharaAvatar = 2;
    }

    public void Select_CharaAvatar_pchan()
    {
        int_MyCharaAvatar = 3;
    }

    public void Select_CharaAvatar_mobuchan()
    {
        int_MyCharaAvatar = 4;
    }

    public void Select_CharaAvatar_Zunko()
    {
        int_MyCharaAvatar = 5;
    }

    public void Select_CharaAvatar_yuzuru()
    {
        int_MyCharaAvatar = 6;  // yuzuruのアバターIDに設定
    }

    public static int get_int_MyCharaAvatar()
    {
        return int_MyCharaAvatar;
    }

    public void Select_Utako_Avatar()
    {
        Reset_AvatarAll();
        Utako_Button.GetComponent<Image>().color = Color.green;
    }

    public void Select_Unitychan_Avatar()
    {
        Reset_AvatarAll();
        Unitychan_Button.GetComponent<Image>().color = Color.green;
    }

    public void Select_Pchan_Avatar()
    {
        Reset_AvatarAll();
        Pchan_Button.GetComponent<Image>().color = Color.green;
    }

    public void Select_Mobuchan_Avatar()
    {
        Reset_AvatarAll();
        Mobuchan_Button.GetComponent<Image>().color = Color.green;
    }

    public void Select_Zunko_Avatar()
    {
        Reset_AvatarAll();
        Zunko_Button.GetComponent<Image>().color = Color.green;
    }

    public void Select_Yuzuru_Avatar()
    {
        Reset_AvatarAll();
        Yuzuru_Button.GetComponent<Image>().color = Color.green;
    }

    public void Reset_AvatarAll()
    {
        Utako_Button.GetComponent<Image>().color = Color.gray;
        Unitychan_Button.GetComponent<Image>().color = Color.gray;
        Pchan_Button.GetComponent<Image>().color = Color.gray;
        Mobuchan_Button.GetComponent<Image>().color = Color.gray;
        Zunko_Button.GetComponent<Image>().color = Color.gray;
        Yuzuru_Button.GetComponent<Image>().color = Color.gray;  // yuzuruのボタン色をリセット
    }

    //●表示させる
    public void AppearPlayStartButton_Panel()
    {
        PlayStartButton_Panel.SetActive(true);
    }

    //●非表示にする
    public void ClosePlayStartButton_Panel()
    {
        PlayStartButton_Panel.SetActive(false);
        Debug.Log("ClosePlayStartButton_Panel");
    }

    #endregion

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    /*
        #region Photonコールバック
        //ルームに入室前に呼び出される
        public override void OnConnectedToMaster()
        {
            if (firstPush)
            {
                RoomOptions options = new RoomOptions();
                Debug.Log("OnConnectedToMasterが呼ばれました");
                options.PublishUserId = true; // ★お互いにユーザＩＤが見えるようにする。
                options.MaxPlayers = 4; // ★最大人数もきちんと定義しておく。
                                        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
                PhotonNetwork.JoinOrCreateRoom("room", options, TypedLobby.Default);
            }
            else
            {
                Debug.Log("まだスタートボタンを押していません");
            }
        }

        //ルームに入った時に呼ばれる
        public override void OnJoinedRoom()
        {
            Debug.Log("ルームに入りました。");
            //battleシーンをロード
            PhotonNetwork.LoadLevel("Battle");
        }

        #endregion
    */

    public void Vol_Set_0()
    {
        BGM_SE_MSC.Vol_Set_0();
    }

    public void Vol_Set_1()
    {
        BGM_SE_MSC.Vol_Set_1();
    }

    public void Vol_Set_2()
    {
        BGM_SE_MSC.Vol_Set_2();
    }

    public void Vol_Set_3()
    {
        BGM_SE_MSC.Vol_Set_3();
    }

    public void Vol_Set_4()
    {
        BGM_SE_MSC.Vol_Set_4();
    }

    public void AppearVolume_Panel()
    {
        //BGM_SE_MSC.AppearVolume_Panel();
        Volume_Panel.SetActive(true);
    }

    public void CloseVolume_Panel()
    {
        //BGM_SE_MSC.Volume_Panel.SetActive(false);
        Volume_Panel.SetActive(false);
    }

    public void AppearCredit_Panel()
    {
        Credit_Panel.SetActive(true);
    }

    public void CloseCredit_Panel()
    {
        Credit_Panel.SetActive(false);
    }

    public void AppearAikotoba_Panel()
    {
        Aikotoba_Panel.SetActive(true);
    }

    public void CloseAikotoba_Panel()
    {
        Aikotoba_Panel.SetActive(false);
    }
}
