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


    public List<CodeBlockExcelReader.ExcelData> codeBlockData;
    public List<PatchExcelReader.ExcelData> patchData;
    public string codeBlockType;
    public float codeJudgmentTime;
    public string codeBlockID;
    public string patchType;
    public float patchJudgmentTime;
    public string patchID;
    public int blockCurrentLine=1;
    public int patchCurrentLine=1;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    void Start()//读取Excel数据，开始游戏
    {
        LoadCodeBlockFromFile("Assets/Resources/Block.xlsx");
        LoadPatchFromFile("Assets/Resources/Patch.xlsx");
        StartGame();
    }
    public void StartGame()
    {
        //开始游戏时，播放第一个动画：一个三角形向上漂浮（过程中随机左右移动）到指定位置
        //第一个动画结束时播放第二个动画（标题透明度由0逐渐变可见 ）和 第三个动画：三角形悬浮（过程中随机上下左右移动），被点击时中断第二个动画，播放第四个动画
        //第二个动画结束时中断第三个动画，播放第四个动画（标题由当前透明度逐渐变为0）且三角形平移到判定区中间
        //第四个动画结束时调用Tutorial()，进入教程
    }
    public void Tutorial1()
    {
        //教程开始时，开始播放一段循环的有节拍的音乐
        //每一拍三角形抖动一次
        //每第四拍前1000ms三角形发出小光晕，屏幕上方开始匀加速下落一个小三角形
        //（第四拍时小三角和三角重合）第四拍时玩家点击则三角形散发大光晕，停止落下小三角并进入Tutorial2(),未点击则播放小三角形Miss()并进入下一个四拍循环
    }
    public void Tutorial2()
    {
        //音乐是沿用Tutorial1()的音乐
        //每一拍三角形抖动一次
        //每第四拍前1000ms三角形发出小光晕，开始匀加速下落一个小圆形
        //第一个四拍中，第四拍时玩家未点击则小圆形Miss()，重复第一个四拍 ； 点击则三角形和小圆形BreakUp()，整个屏幕瞬间变红褪色，重新飘上来一个三角形，出现粉碎轮和检查区，进入第二个四拍
        //第二个四拍中，Miss或点击使三角形BreakUp()则进入第三个四拍， ； 小圆形被粉碎轮BreakUp()则进入第四个四拍
        //第三个四拍中，出现从粉碎轮到小圆形的路径指引，点击使三角形BreakUp()或Miss()则重复第三个四拍 ； 粉碎轮碰撞到小圆形时小圆形BreakUp()，进入第四个四拍
        //第四个四拍中，改为飘落小三角形，四拍时点击则“成功（）”，小三角消失，大三角散发光晕，进入第五个四拍，（同第二个四拍）； Miss()则重复第四个四拍 ； 小三角形被粉碎则屏幕瞬间变红褪色，重复第四个四拍
        //第五个四拍中，Miss或点击使三角形BreakUp()则重复第五个四拍， ； 小圆形被粉碎轮BreakUp()则进入第六个四拍（同第四个四拍）
        //第六个四拍中，Miss()则重复第六个四拍 ； 粉碎小三角则屏幕瞬间变红褪色，重复第六个四拍 ； 点击则“成功（）”，小三角消失，大三角散发光晕，Tutorial2()结束，isIntroduced设为true
    }






    public void SongStart()//开始播放音频，补丁箱、漏洞代码仓、稳定代码仓、调试台从屏幕外平滑移动至指定位置
    {
        audioPlayer.enabled = true;
        //SlowMove(补丁箱,初位置，指定位置，所用时间);
        //SlowMove(漏洞代码仓，初位置，指定位置，所用时间);
        //SlowMove(稳定代码仓，初位置，指定位置，所用时间);
        //SlowMove(调试台，初位置，指定位置，所用时间);
    }

    private void Update()
    {
        //正式开始时，每帧计时器实时更新，每帧检查时间点
        if (audioPlayer.enabled)
        {
            Timer();
            CheckTime(nowTime);
        }
    }
    public float Timer()//计时器返回当前时间
    {
        nowTime += Time.deltaTime;
        return nowTime;
    }

    private void CheckTime(float nowTime)//检查时间点
    {
        if(nowTime==codeJudgmentTime-4000f) //检查时间点,若为代码块判定点前4000ms则弹出一个代码块
        {
            //弹出(bdata.blockType)

        }
        if (nowTime == patchJudgmentTime - 1000f) //检查时间点,若为补丁判定点前1000ms则弹出一个补丁
        {
            //弹出(pdata.patchType)
        }


        //根据不同时间点，令isOnPath为false，给Effect提供输入，可分二函数实现效果
        //生成代码Instantiate(预制体,patchBornPos.position,Quaternion.identity);
    }
    public void LoadCodeBlockFromFile(string Path)//游戏开始时从指定路径+文件名读取Excel
    {
        codeBlockData = CodeBlockExcelReader.ReadExcel(Path);
        if ( codeBlockData == null || codeBlockData.Count == 0)
        {
            Debug.LogError("NO_Block_DATA_FOUND");
        }
    }
    public void ReadBlock(string Path)//读取一个codeBlock的类型，判定时间和id
    {
        var bdata = codeBlockData[blockCurrentLine];
        codeBlockID = bdata.codeBlockID;
        codeBlockType = bdata.codeBlockType;
        codeJudgmentTime = float.Parse(bdata.codeJudgmentTime);
        blockCurrentLine++;
    }

    public void LoadPatchFromFile(string Path)//游戏开始时从指定路径+文件名读取Excel
    {
        patchData = PatchExcelReader.ReadExcel(Path);
        if (codeBlockData == null || codeBlockData.Count == 0)
        {
            Debug.LogError("NO_Patch_DATA_FOUND");
        }
    }
    public void ReadPatch(string Path)//读取一个patch类型，判定时间和id
    {
        var pdata = patchData[patchCurrentLine];
        patchID = pdata.patchID;
        patchType = pdata.patchType;
        patchJudgmentTime = float.Parse(pdata.patchJudgmentTime);
        patchJudgmentTime++;
    }
}
