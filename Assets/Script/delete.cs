using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//↓↓Button変数を使用するときにこの記述が必要です！↓↓
using UnityEngine.UI;

public class delete : MonoBehaviour
{

    //これはbuttonコンポーネントを登録するための変数
    public Button btn2;
    //これは生成ボタンを登録するための変数
    GameObject btn1;
    //生成ボタンにアタッチされているgenerationスクリプトを登録するための変数
    generation script;

    // Use this for initialization
    void Start()
    {

        //btn1に生成ボタンをGameObjectとして登録
        btn1 = GameObject.Find("生成ボタン");

        //生成ボタンのコンポーネントであるgenerationスクリプトを登録
        script = btn1.GetComponent<generation>();

        //これは自分のボタンをbtn2として登録
        btn2 = GetComponent<Button>();
    }

    public void OnClick()
    {
        //自分のボタンが押されたので無効に
        btn2.interactable = false;

        //generationで生成したプレハブのobj1を削除
        Destroy(script.obj1);

        //生成ボタンを有効にする
        script.btn1.interactable = true;
    }
}