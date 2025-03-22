using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : BaseObject
{
    private int patchID;
    //判断是否在路径上
    public bool isOnPath;
    //随机选择曲线
    private int pathNumber;

    public void Awake()
    {
        //随机生成一个数决定路径
        pathNumber = UnityEngine.Random.Range(1, 4);
        isOnPath = true;
    }
    private void Start()
    {
        //路径
        if (isOnPath)
            ObjectDrop(pathNumber);
    }

    //补丁下落的路径
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
}
