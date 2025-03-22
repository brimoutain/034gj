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
    //��ȡ����ϵͳ
    [SerializeField] private ParticleSystem particle;
    //СԤ����
    [SerializeField] private GameObject smallPrefab;
    //����ǿ��
    [SerializeField] private float forceStrength;
    //���δ���ʱ�䣬��Ҫ����
    [SerializeField] private float haloTime;
    //����q��Ч��
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
            //�̳�С������/��ȷС�������ж�ʱ������Ч(������ɫ)
            case 1:
                SlowDownAndChangeColor();
                break;
            //�̳������� / ���ӹ�����ж��ɹ�����Ч����ɫС������ɢ�� + ���Σ�
            case 2:
                //particle.emission.SetBurst(0,new ParticleSystem.Burst(0.0f, 50));
                //particle.Play();
                HaloEffect(3);
                break;
            //�̳�СԲ��/����С������ж�����Ч�����ѳ�С������+��ɫ���Σ�
            case 3:
                BreakEffect();
                //�ȴ�����
                break;
            //�̳�������/�����������ж�ʱ���������Ч�����ѳ�С������+���Σ�
            case 4:
                BreakEffect();
                //�ȴ�����
                break;
            default:
                return;
        }
    }

    #region DifferentEffect
    //����Ч��
    public void BreakEffect()
    {
        //�ȴ����������
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
        // �ȴ�0.3�룬��Ҫ���Ծ���Ч��
        yield return new WaitForSeconds(.3f);

        Destroy(instance);
    }

    IEnumerator Halo(float haloScale)
    {
        GameObject[] obj = new GameObject[10];
        for(int i =0; i < 10; i++)
        {
            obj[i] = BornHalo(haloScale);
            //��Ҫ����
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

        
        //��Ȧ������Ҫ���Ծ����С
        obj.transform.DOScale(haloScale, haloTime);
        //������ʧ����֪���ܲ�����
        obj.GetComponent<SpriteRenderer>().DOColor(new Color(objColor.r,objColor.g,objColor.b,0), haloTime);
        return obj;
    }

    //������ɫ
    public void SlowDownAndChangeColor()
    {
        //���������ı���ֵ
        transform.DOMove(new Vector3(5, 0, 0), 2f).SetEase(Ease.InOutQuad);
        //��ɫ����ɫ��Ҫ����
        GetComponent<SpriteRenderer>().DOColor(Color.red, .5f);
    }

    //����Ч������Ҫupdateʵ��
    public void ShakeEffect()
    {
        isShaked = true;
        float a = 1 + .2f * Mathf.Pow((float)System.Math.E,- 10 * shakeTime) * Mathf.Sin(8*Mathf.PI*shakeTime);
        float h = 1 - .2f * Mathf.Pow((float)System.Math.E, -10 * shakeTime) * Mathf.Sin(8 * Mathf.PI * shakeTime);
        transform.localScale = new Vector3(a,h,1);
    }

    //����Ч��
    public void HaloEffect(float haloScale)
    {
        StartCoroutine(Halo(haloScale));
    }

    #endregion
}
