using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    #region Component
    private AudioSource audioPlayer;
    #endregion

    #region Intrudction
    GameObject iTringle;

    Transform outPoint;
    Transform stopPoint;

    [SerializeField] private float flyduration;//飘上去所用时间
    bool isFinished = false;
    [SerializeField] private float clearduration;//标题显现所用时间

    public GameObject title;

    public float moveSpeed = 2f;     // 移动速度
    public float changeInterval = 3f; // 方向变化间隔

    private Vector2 randomDirection;
    #endregion

    #region ExcelReader

    public List<CodeBlockExcelReader.ExcelData> codeBlockData;
    public List<PatchExcelReader.ExcelData> patchData;
    public string codeBlockType;
    public float codeJudgmentTime;
    public string codeBlockID;
    public string patchType;
    public float patchJudgmentTime;
    public string patchID;
    public int codeBlockCurrentLine = 1;
    public int patchCurrentLine = 1;

    bool end = false;
    #endregion


    private bool isIntroducted = true;//判断是否结束教程，这里先默认结束
    private float startTime;//开始播放音乐的时间
    private float nowTime;//当前时间，用于判断生成预制体

    [SerializeField] private GameObject tringle;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject codeBlockTringle;
    [SerializeField] private GameObject codeBlockCircle;

    //图形出现的位置
    [SerializeField] private Transform patchBornPos;

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        outPoint.position = new Vector3(0, -5.5f, 0);
        stopPoint.position = new Vector3(0,-3.75f,0);
    }
    void Start()//读取Excel数据，开始游戏
    {
        LoadCodeBlockFromFile("Assets/Resources/2.xlsx");
        LoadPatchFromFile("Assets/Resources/3.xlsx");
        AudioClip audioClip = Resources.Load<AudioClip>("Assets/Resources/");
        //这段用于测试特效
        GameObject game = Instantiate(tringle, outPoint);
        game.GetComponent<BaseObject>().Effect(2);
        //
        StartGame();
    }
    public void StartGame()
    {
        FirstAnim();
        #region 1
        //开始游戏时，播放第一个动画：一个三角形向上漂浮（过程中随机左右移动）到指定位置
        //第一个动画结束时播放第二个动画（标题透明度由0逐渐变可见 ）和
        //第三个动画：三角形悬浮（过程中随机上下左右移动），被点击时中断第二个动画，播放第四个动画
        //第二个动画结束时中断第三个动画，播放第四个动画（标题由当前透明度逐渐变为0）且三角形平移到判定区中间
        //第四个动画结束时调用Tutorial()，进入教程
        #endregion
    }

    //随机移动的方向
    //IEnumerator ChangeDirection()
    //{
    //    while (true)
    //    {
    //        // 生成随机方向（单位向量）
    //        randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
    //        yield return new WaitForSeconds(changeInterval);
    //    }
    //}

    //手动添加路径点
    private void FirstAnim()
    {
        iTringle = Instantiate(tringle, outPoint.position, Quaternion.identity);
        iTringle.transform.DOPath(
           new[] {
                iTringle.transform.position,
                new Vector3(iTringle.transform.position.x + 2f,iTringle.transform.position.y + 5f, 0),
                new Vector3(iTringle.transform.position.x - 2f, iTringle.transform.position.y + 10f, 0),
                // 添加更多路径点
           }, flyduration, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() => SecondAnim());
    }
    private void SecondAnim()
    {
        title.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), clearduration);
        isFinished = true;
    }

    public void Tutorial1()
    {
        //教程开始时，开始播放一段循环的有节拍的音乐
        audioPlayer.Play();
        //每一拍三角形抖动一次
        //每第四拍前100ms三角形发出小光晕，屏幕上方开始匀加速下落一个小三角形
        //（第四拍时小三角和三角重合）第四拍时玩家点击则三角形散发大光晕，停止落下小三角并进入Tutorial2()
        //,未点击则播放小三角形Miss()并进入下一个四拍循环
    }
    public void Tutorial2()
    {
        //音乐是沿用Tutorial1()的音乐
        //每一拍三角形抖动一次
        //每第四拍前100ms三角形发出小光晕，开始匀加速下落一个小圆形
        //第一个四拍中，第四拍时玩家未点击则小圆形Miss()，重复第一个四拍 ； 点击则三角形和小圆形BreakUp()，
        //整个屏幕瞬间变红褪色，重新飘上来一个三角形，出现粉碎轮和检查区，进入第二个四拍
        //第二个四拍中，Miss或点击使三角形BreakUp()则进入第三个四拍， ； 小圆形被粉碎轮BreakUp()则教程结束
        //第三个四拍中，出现从粉碎轮到小圆形的路径指引，点击使三角形BreakUp()或Miss()则重复第三个四拍 ；
        //粉碎轮碰撞到小圆形时小圆形BreakUp()，教程结束
        //isIntroduced设为true
    }

    public void SongStart()//开始播放音频，补丁箱、漏洞代码仓、稳定代码仓、调试台从屏幕外平滑移动至指定位置
    {
        //这里是正式音频
        audioPlayer.enabled = true;
        //SlowMove(补丁箱,初位置，指定位置，所用时间);
        //SlowMove(漏洞代码仓，初位置，指定位置，所用时间);
        //SlowMove(稳定代码仓，初位置，指定位置，所用时间);
        //SlowMove(调试台，初位置，指定位置，所用时间);
    }
    public void End()
    {
        //音频片段切换为结算音乐
        //补丁箱，漏洞代码仓，调试台，检查区，杀毒软件全部淡化，保留稳定代码仓
        //屏幕中心出现计数条（由十个灰色三角形组成）
        //若干个（数量为修复的代码块数）三角形从稳定代码仓飞向计数条的指定位置（第a个三角形飞到a/n长度，n为代码块总数）
        //每有一个三角形飞到计数条则成绩+1，当成绩小于总数90%时，每多10%就使计数条中一个三角形变蓝 ； 当成绩大于90%时每多1%就使计数条中一个三角形变金
    }

    private void Update()
    {      
        if (audioPlayer.enabled)
        {
            nowTime += Time.deltaTime;
            CheckTime();
        }
        //检测第四段动画1.第二段播放完，2.鼠标点击
        ForthAnim();
        //补丁和代码块全部打完进入结算
        if (codeBlockCurrentLine >= codeBlockData.Count || patchCurrentLine >= patchData.Count)
        {
            end = true;
        }

    }

    private void ForthAnim()
    {
        if (isFinished && (Input.GetMouseButtonDown(0) || title.GetComponent<Image>().color.a == 1))
        {
            //第四段动画,都用的clearduration，有需要可改
            title.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), flyduration);
            iTringle.transform.DOMove(outPoint.position, flyduration).OnComplete(() => Tutorial1());
        }
    }

    private void CheckTime()//可能的检查时间点，生成预制体
    {
        //根据不同时间点，令isOnPath为false，给Effect提供输入，可分二函数实现效果

        //Instantiate(tringle,patchBornPos.position,Quaternion.identity);
        //需要弹出特效，两个位置

        //生成漏洞代码块
        //判断漏洞代码块清除情况

        //判断单个音符准确率
        //呼唤特效
    }

    #region ExcelReaderFuction
    public void LoadCodeBlockFromFile(string Path)//游戏开始时从指定路径+文件名读取Excel
    {
        codeBlockData = CodeBlockExcelReader.ReadExcel(Path);
        if (codeBlockData == null || codeBlockData.Count == 0)
        {
            Debug.LogError("NO_Block_DATA_FOUND");
        }
    }
    public void ReadBlock(string Path)//读取一个codeBlock的类型，判定时间和id
    {
        var bdata = codeBlockData[codeBlockCurrentLine];
        codeBlockID = bdata.codeBlockID;
        codeBlockType = bdata.codeBlockType;
        codeJudgmentTime = float.Parse(bdata.codeJudgmentTime);
        codeBlockCurrentLine++;
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
    #endregion
}
