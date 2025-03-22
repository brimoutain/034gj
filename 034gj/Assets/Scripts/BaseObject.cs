using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using System.Runtime.CompilerServices;

public class BaseObject : MonoBehaviour
{
    //图形出现的位置
    [SerializeField] private Transform codeBornPos;
    //获取粒子系统
    [SerializeField] private ParticleSystem particle;
    //随机选择曲线
    private int pathNumber;
    //小预制体
    [SerializeField] private GameObject smallPrefab;
    //力的强度
    [SerializeField] private float forceStrength;
    //判断是否在路径上
    public bool isOnPath;

    public void Awake()
    {
        transform.position = codeBornPos.position;
        //随机生成一个数决定路径
        pathNumber = UnityEngine.Random.Range(1, 4);
        isOnPath = true;
    }

    public void Update()
    {
        //路径
        if(isOnPath) 
            ObjectDrop(pathNumber);
    }

    //物体下落的路径
    public void ObjectDrop(int path)
    {
        switch (path)
        {
            case 1:
                //这里等待曲线
                break;
            case 2:
                //同
                break;
            case 3:
                //同
                break;
            default:
                return;
        }
    }

    public void Effect(int grade)
    {
        switch (grade)
        {
            //教程小三角形/正确小零件错过判定时机的特效(缓动变色)
            case 1:
                //等待

                break;
            //教程三角形 / 待加工零件判定成功的特效（金色小三角形散射 + 光晕）
            case 2:
                particle.emission.SetBurst(5, particle.emission.GetBurst(0));
                particle.startColor = new Color(1, 1, 1, 1);//等待金色
                //光晕
                break;
            //教程小圆形/错误小零件被判定的特效（碎裂成小三角形+红色光晕）
            case 3:
                BreakEffect();
                //等待光晕
                break;
            //教程三角形/待加工零件判定失败的破碎特效（碎裂成小三角形+光晕）
            case 4:
                BreakEffect();
                //等待光晕
                break;
            default:
                return;
        }
    }

    //碎裂效果
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
        //待摧毁obj，考虑用什么函数
    }

    //缓动变色
    public void SlowdownAndChangeColor()
    {
        //缓动，待改变数值
        transform.DOMove(new Vector3(5, 0, 0), 2f).SetEase(Ease.InOutQuad);
        //变色
        GetComponent<SpriteRenderer>().material.color = Color.red;
    }

    //抖动效果
    public void ShakeEffect()
    {

    }

    //光晕效果
    public void HaloEffect()
    {

    }

}
