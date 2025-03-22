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
    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        //��ʼ��Ϸʱ�����ŵ�һ��������һ������������Ư������������������ƶ�����ָ��λ��
        //��һ����������ʱ���ŵڶ�������������͸������0�𽥱�ɼ� ���� ������������������������������������������ƶ����������ʱ�жϵڶ������������ŵ��ĸ�����
        //�ڶ�����������ʱ�жϵ��������������ŵ��ĸ������������ɵ�ǰ͸�����𽥱�Ϊ0����������ƽ�Ƶ��ж����м�
        //���ĸ���������ʱ����Tutorial()������̳�
    }
    public void Tutorial1()
    {
        //�̳̿�ʼʱ����ʼ����һ��ѭ�����н��ĵ�����
        //ÿһ�������ζ���һ��
        //ÿ������ǰ100ms�����η���С���Σ���Ļ�Ϸ���ʼ�ȼ�������һ��С������
        //��������ʱС���Ǻ������غϣ�������ʱ��ҵ����������ɢ������Σ�ֹͣ����С���ǲ�����Tutorial2(),δ����򲥷�С������Miss()��������һ������ѭ��
    }
    public void Tutorial2()
    {
        //����������Tutorial1()������
        //ÿһ�������ζ���һ��
        //ÿ������ǰ100ms�����η���С���Σ���ʼ�ȼ�������һ��СԲ��
        //��һ�������У�������ʱ���δ�����СԲ��Miss()���ظ���һ������ �� ����������κ�СԲ��BreakUp()��������Ļ˲������ɫ������Ʈ����һ�������Σ����ַ����ֺͼ����������ڶ�������
        //�ڶ��������У�Miss����ʹ������BreakUp()�������������ģ� �� СԲ�α�������BreakUp()�������ĸ�����
        //�����������У����ִӷ����ֵ�СԲ�ε�·��ָ�������ʹ������BreakUp()��Miss()���ظ����������� �� ��������ײ��СԲ��ʱСԲ��BreakUp()��������ĸ�����
        //���ĸ������У���ΪƮ��С�����Σ�����ʱ����򡰳ɹ���������С������ʧ��������ɢ�����Σ������������ģ���ͬ�ڶ������ģ��� Miss()���ظ����ĸ����� �� С�����α���������Ļ˲������ɫ���ظ����ĸ�����
        //����������У�Miss����ʹ������BreakUp()���ظ���������ģ� �� СԲ�α�������BreakUp()�������������ģ�ͬ���ĸ����ģ�
        //�����������У�Miss()���ظ����������� �� ����С��������Ļ˲������ɫ���ظ����������� �� ����򡰳ɹ���������С������ʧ��������ɢ�����Σ�Tutorial2()������isIntroduced��Ϊtrue
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
