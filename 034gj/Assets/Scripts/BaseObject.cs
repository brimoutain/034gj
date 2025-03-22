using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using System.Runtime.CompilerServices;

public class BaseObject : MonoBehaviour
{
    //ͼ�γ��ֵ�λ��
    [SerializeField] private Transform codeBornPos;
    //��ȡ����ϵͳ
    [SerializeField] private ParticleSystem particle;
    //���ѡ������
    private int pathNumber;
    //СԤ����
    [SerializeField] private GameObject smallPrefab;
    //����ǿ��
    [SerializeField] private float forceStrength;
    //�ж��Ƿ���·����
    public bool isOnPath;

    public void Awake()
    {
        transform.position = codeBornPos.position;
        //�������һ��������·��
        pathNumber = UnityEngine.Random.Range(1, 4);
        isOnPath = true;
    }

    public void Update()
    {
        //·��
        if(isOnPath) 
            ObjectDrop(pathNumber);
    }

    //���������·��
    public void ObjectDrop(int path)
    {
        switch (path)
        {
            case 1:
                //����ȴ�����
                break;
            case 2:
                //ͬ
                break;
            case 3:
                //ͬ
                break;
            default:
                return;
        }
    }

    public void Effect(int grade)
    {
        switch (grade)
        {
            //�̳�С������/��ȷС�������ж�ʱ������Ч(������ɫ)
            case 1:
                //�ȴ�

                break;
            //�̳������� / ���ӹ�����ж��ɹ�����Ч����ɫС������ɢ�� + ���Σ�
            case 2:
                particle.emission.SetBurst(5, particle.emission.GetBurst(0));
                particle.startColor = new Color(1, 1, 1, 1);//�ȴ���ɫ
                //����
                break;
            //�̳�СԲ��/����С������ж�����Ч�����ѳ�С������+��ɫ���Σ�
            case 3:
                BreakEffect();
                //�ȴ�����
                break;
            //�̳�������/���ӹ�����ж�ʧ�ܵ�������Ч�����ѳ�С������+���Σ�
            case 4:
                BreakEffect();
                //�ȴ�����
                break;
            default:
                return;
        }
    }

    //����Ч��
    public void BreakEffect()
    {
        GameObject obj;
        for (int i = 0; i < 10; i++)
        {
            obj = Instantiate(smallPrefab, transform.position, Quaternion.identity);
            Vector2 randomDirection = new Vector2(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f)
            ).normalized;
            obj.GetComponent<Rigidbody2D>().AddForce(randomDirection * forceStrength, ForceMode2D.Impulse);
        }
        //���ݻ�obj��������ʲô����
    }

    //������ɫ
    public void SlowdownAndChangeColor()
    {
        //���������ı���ֵ
        transform.DOMove(new Vector3(5, 0, 0), 2f).SetEase(Ease.InOutQuad);
        //��ɫ
        GetComponent<SpriteRenderer>().material.color = Color.red;
    }

    //����Ч��
    public void ShakeEffect()
    {

    }

    //����Ч��
    public void HaloEffect()
    {

    }

}
