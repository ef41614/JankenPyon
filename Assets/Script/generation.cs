using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//↓↓Button変数を使用するときにこの記述が必要です！↓↓
using UnityEngine.UI;

public class generation : MonoBehaviour
{

    //これはbuttonコンポーネントを登録するための変数
    public Button btn1;
    public GameObject prefab;
    public GameObject parentobject, btn2;
    public GameObject obj1;
    int PrefabNumber = 0;
    //削除ボタンにアタッチされているdeleteスクリプトを登録するための変数
    delete script;

    // Use this for initialization
    void Start()
    {

        //Canvasの子としてプレハブを生成したいので親にCanvasを登録
        parentobject = GameObject.Find("Canvas");

        //prefab1というゲームオブジェクトの変数にResourcesファイル内にあるprefab1を登録

        //prefab = (GameObject)Resources.Load(prefabName);
        PrefabNumber = Random.Range(0, 3);

        CreateCard();

        //obj1としてprefab1をインスタンスとして生成、ここでヒエラルキーに載ります
        obj1 = Instantiate(prefab) as GameObject;

        //生成したインスタンスをparentobjectの子、つまりCanvasの子として登録します
        obj1.transform.SetParent(parentobject.transform, false);

        //自分のButtonのコンポーネントを取得
        btn1 = GetComponent<Button>();

        //btn2に削除ボタンをGameObjectとして登録
        btn2 = GameObject.Find("削除ボタン");

        //削除ボタンのコンポーネントであるdeleteスクリプトを登録
        script = btn2.GetComponent<delete>();

        //最初は生成ボタンは無効に
        btn1.interactable = false;
    }

    //じゃんけんカードを一枚ランダムで生成する
    public void CreateCard()
    {
        PrefabNumber = Random.Range(0, 3);

        if (PrefabNumber == 0)
        {
            prefab = (GameObject)Resources.Load("prefab_Gu");
        }
        else if (PrefabNumber == 1)
        {
            prefab = (GameObject)Resources.Load("prefab_Choki");
        }
        else if (PrefabNumber == 2)
        {
            prefab = (GameObject)Resources.Load("prefab_Pa");
        }
        else
        {
            Debug.Log("ジャンケン ERROR");
        }
    }

    //ボタンが押されるとこの関数が実行される
    public void OnClick()
    {
        CreateCard();

        //自分のボタンが押されたので自分のボタンを無効にします
        btn1.interactable = false;

        //削除ボタンを有効にする
        script.btn2.interactable = true;

        //上記と同じ
        obj1 = Instantiate(prefab) as GameObject;
        obj1.transform.SetParent(parentobject.transform, false);

    }
}