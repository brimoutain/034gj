using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : BaseObject
{
    private int patchID;
    //�ж��Ƿ���·����
    public bool isOnPath;
    //���ѡ������
    private int pathNumber;

    public void Awake()
    {
        //�������һ��������·��
        pathNumber = UnityEngine.Random.Range(1, 4);
        isOnPath = true;
    }
    private void Start()
    {
        //·��
        if (isOnPath)
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
}
