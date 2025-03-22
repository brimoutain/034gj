using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;

public class BaseObject : MonoBehaviour
{
    //获取粒子系统
    [SerializeField] private ParticleSystem particle;
    //小预制体
    [SerializeField] private GameObject smallPrefab;
    //力的强度
    [SerializeField] private float forceStrength;
    //光晕存在时间，需要调试
    [SerializeField] private float haloTime;
    //用于q弹效果
    private bool isShaked = false;
    private float shakeTime;

    

    public void Update()
    {       
        if (isShaked)
        {
            shakeTime += Time.deltaTime;
        }
    }

    public void Effect(int grade)
    {
        switch (grade)
        {
            //教程小三角形/正确小零件错过判定时机的特效(缓动变色)
            case 1:
                SlowDownAndChangeColor();
                break;
            //教程三角形 / 待加工零件判定成功的特效（金色小三角形散射 + 光晕）
            case 2:
                //particle.emission.SetBurst(0,new ParticleSystem.Burst(0.0f, 50));
                //particle.Play();
                HaloEffect(3);
                break;
            //教程小圆形/错误小零件被判定的特效（碎裂成小三角形+红色光晕）
            case 3:
                BreakEffect();
                //等待光晕
                break;
            //教程三角形/错误零件错过判定时间的破碎特效（碎裂成小三角形+光晕）
            case 4:
                BreakEffect();
                //等待光晕
                break;
            default:
                return;
        }
    }

    #region DifferentEffect
    //碎裂效果
    public void BreakEffect()
    {
        //等待填具体数字
        for(int i= 0; i < 10; i++)
        {
            StartCoroutine(BornAndDestroy());
        }
    }

    IEnumerator BornAndDestroy()
    {
        GameObject instance = Instantiate(smallPrefab, transform.position, Quaternion.identity);
        Vector2 randomDirection = new Vector2(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f)
            ).normalized;
        instance.GetComponent<Rigidbody2D>().AddForce(randomDirection * forceStrength, ForceMode2D.Impulse);
        // 等待0.3秒，需要测试具体效果
        yield return new WaitForSeconds(.3f);

        Destroy(instance);
    }

    IEnumerator Halo(float haloScale)
    {
        GameObject[] obj = new GameObject[10];
        for(int i =0; i < 10; i++)
        {
            obj[i] = BornHalo(haloScale);
            //需要调试
            yield return new WaitForSeconds(.1f);
        }
        
        for(int i = 0; i < 10; i++)
        {
            Destroy(obj[i]);
        }
    }

    public GameObject BornHalo(float haloScale)
    {
        GameObject obj;
        obj = Instantiate(smallPrefab,transform.position, Quaternion.identity);
        Color objColor = obj.GetComponent<SpriteRenderer>().color;

        
        //光圈扩大，需要调试具体大小
        obj.transform.DOScale(haloScale, haloTime);
        //渐渐消失，不知道能不能用
        obj.GetComponent<SpriteRenderer>().DOColor(new Color(objColor.r,objColor.g,objColor.b,0), haloTime);
        return obj;
    }

    //缓动变色
    public void SlowDownAndChangeColor()
    {
        //缓动，待改变数值
        transform.DOMove(new Vector3(5, 0, 0), 2f).SetEase(Ease.InOutQuad);
        //变色，颜色需要调试
        GetComponent<SpriteRenderer>().DOColor(Color.red, .5f);
    }

    //抖动效果，需要update实现
    public void ShakeEffect()
    {
        isShaked = true;
        float a = 1 + .2f * Mathf.Pow((float)System.Math.E,- 10 * shakeTime) * Mathf.Sin(8*Mathf.PI*shakeTime);
        float h = 1 - .2f * Mathf.Pow((float)System.Math.E, -10 * shakeTime) * Mathf.Sin(8 * Mathf.PI * shakeTime);
        transform.localScale = new Vector3(a,h,1);
    }

    //光晕效果
    public void HaloEffect(float haloScale)
    {
        StartCoroutine(Halo(haloScale));
    }

    #endregion
}
