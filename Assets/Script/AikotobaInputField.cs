using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class AikotobaInputField : MonoBehaviour
{
    #region Private変数定義
    public GameObject BGM_SE_Manager;
    BGM_SE_Manager BGM_SE_MSC;
    //static string playerNamePrefKey = "PlayerName";
    InputField _inputField;
    public GameObject Aikotoba_OK_Button;
    #endregion

    #region MonoBehaviourコールバック
    private void Awake()
    {
        Debug.Log("AikotobaInputField Awake");
        BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
    }
    
    void Start()
    {
        Debug.Log("あいことばをリセットする");
        BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();
        BGM_SE_MSC.Aikotoba = "";  // あいことばをリセットする
        _inputField = this.GetComponent<InputField>();
        CloseAikotoba_OK_Button();
        _inputField.text = "";
    }
    #endregion

    #region Public Method
    public void Set_Aikotoba()
    {
        BGM_SE_MSC.Aikotoba = _inputField.text + " ";     //今回ゲームで利用するプレイヤーの名前を設定
        //PhotonNetwork.NickName = _inputField.text + " ";     //今回ゲームで利用するプレイヤーの名前を設定
        //PlayerPrefs.SetString(playerNamePrefKey, _inputField.text);    //今回の名前をセーブ
        //PlayerPrefs.Save();
        Debug.Log("BGM_SE_MSC.Aikotoba ： " + BGM_SE_MSC.Aikotoba);

        if (BGM_SE_MSC.Aikotoba.Length >= 4 && BGM_SE_MSC.Aikotoba.Length <= 10)  // あいことばの長さが 4 ～ 10 文字ならば
        {
            AppearAikotoba_OK_Button();
        }
        else
        {
            CloseAikotoba_OK_Button();
        }
    }


    public void AppearAikotoba_OK_Button()
    {
        Aikotoba_OK_Button.SetActive(true);
    }

    public void CloseAikotoba_OK_Button()
    {
        Aikotoba_OK_Button.SetActive(false);
    }
    #endregion
}