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

    private bool isIntroducted = true;//�ж��Ƿ�����̳̣�������Ĭ�Ͻ���
    private float startTime;//��ʼ�������ֵ�ʱ��
    private float nowTime;//��ǰʱ�䣬�����ж�����Ԥ����

    [SerializeField] private GameObject tringle;
    [SerializeField] private GameObject circle;

    //ͼ�γ��ֵ�λ��
    [SerializeField] private Transform patchBornPos;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //�̳̽�������ʼ�������֣����濪ʼʱ��
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

    private void CheckTime()//���ܵļ��ʱ��㣬����Ԥ����
    {
        //���ݲ�ͬʱ��㣬��isOnPathΪfalse����Effect�ṩ���룬�ɷֶ�����ʵ��Ч��
        //���ɴ���Instantiate(Ԥ����,patchBornPos.position,Quaternion.identity);
    }
}
