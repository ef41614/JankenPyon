using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

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

    public GameObject PlayStartButton;
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

        firstPush = false; //初期化
        if (PhotonNetwork.IsConnected)
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
    }

    void Start()
    {
        firstPush = false; //初期化
        Reset_AvatarAll();
        ClosePlayStartButton();
    }


    #region Public Methods
    //ログインボタンを押したときに実行される
    public void Connect()
    {
        Debug.Log("CLauncherScript で「Play」が押されました。");
        Debug.Log(" firstPush ： " + firstPush);
        Debug.Log(" PhotonNetwork.IsConnected ： " + PhotonNetwork.IsConnected);
        if (!firstPush)
        {
            Debug.Log("Connect 処理中");
            Debug.Log("PhotonNetwork.NickName Play：" + PhotonNetwork.NickName);
            if (string.IsNullOrWhiteSpace(PhotonNetwork.NickName))
            {
                Debug.Log("名前が空欄です");
            }
            else
            {
                if (!PhotonNetwork.IsConnected)
                {
                    Debug.Log("Photonに接続をします");
                    //Photonに接続できていなければ
                    PhotonNetwork.ConnectUsingSettings();   //Photonに接続する
                    Debug.Log("Photonに接続しました。");
                }
                else
                {
                    Debug.Log("Photonに接続できませんで した。");
                }
            }
            firstPush = true; //ボタン押下済みフラグ
        }
        else
        {
            Debug.Log("Connect 処理に失敗しました");
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

    public static int get_int_MyCharaAvatar()
    {
        return int_MyCharaAvatar;
    }

    public void Select_Utako_Avatar()
    {
        Reset_AvatarAll();
        Utako_Button.GetComponent< Image > ().color = Color.green;
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

    public void Reset_AvatarAll()
    {
        Utako_Button.GetComponent<Image>().color = Color.gray;
        Unitychan_Button.GetComponent<Image>().color = Color.gray;
        Pchan_Button.GetComponent<Image>().color = Color.gray;
        Mobuchan_Button.GetComponent<Image>().color = Color.gray;
    }

    //●表示させる
    public void AppearPlayStartButton()
    {
        PlayStartButton.SetActive(true);
    }

    //●非表示にする
    public void ClosePlayStartButton()
    {
        PlayStartButton.SetActive(false);
        Debug.Log("ClosePlayStartButton");
    }

    #endregion

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
}