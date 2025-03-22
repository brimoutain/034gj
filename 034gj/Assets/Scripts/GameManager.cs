using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    #region GetComponent
    private AudioSource audioPlayer;
    #endregion

    private bool isIntroducted = true;//判断是否结束教程，这里先默认结束
    private float startTime;//开始播放音乐的时间
    private float nowTime;//当前时间，用于判断生成预制体

    [SerializeField] private GameObject tringle;
    [SerializeField] private GameObject circle;

    //图形出现的位置
    [SerializeField] private Transform patchBornPos;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //教程结束，开始播放音乐，储存开始时间
        if (isIntroducted)
        {
            audioPlayer.enabled = true;
            startTime = Time.time;
            isIntroducted = false;
        }
        if (audioPlayer.enabled)
        {
            nowTime += Time.deltaTime;
            CheckTime();
        }
    }

    private void CheckTime()//可能的检查时间点，生成预制体
    {
        //根据不同时间点，令isOnPath为false，给Effect提供输入，可分二函数实现效果
        //生成代码Instantiate(预制体,patchBornPos.position,Quaternion.identity);
    }
}
