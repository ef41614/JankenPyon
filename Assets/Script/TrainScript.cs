using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainScript : MonoBehaviour
{
    public GameObject[] Train;

    int number = 1;

    void Start()
    {
        Debug.Log("TrainScript 出席確認");
        number = Random.Range(0, Train.Length);
        Instantiate(Train[number], transform.position, transform.rotation);
    }
}