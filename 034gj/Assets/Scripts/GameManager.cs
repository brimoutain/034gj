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
    void Start()//��ȡExcel���ݣ���ʼ��Ϸ
    {
        LoadCodeBlockFromFile("Assets/Resources/Block.xlsx");
        LoadPatchFromFile("Assets/Resources/Patch.xlsx");
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
        //ÿ������ǰ1000ms�����η���С���Σ���Ļ�Ϸ���ʼ�ȼ�������һ��С������
        //��������ʱС���Ǻ������غϣ�������ʱ��ҵ����������ɢ������Σ�ֹͣ����С���ǲ�����Tutorial2(),δ����򲥷�С������Miss()��������һ������ѭ��
    }
    public void Tutorial2()
    {
        //����������Tutorial1()������
        //ÿһ�������ζ���һ��
        //ÿ������ǰ1000ms�����η���С���Σ���ʼ�ȼ�������һ��СԲ��
        //��һ�������У�������ʱ���δ�����СԲ��Miss()���ظ���һ������ �� ����������κ�СԲ��BreakUp()��������Ļ˲������ɫ������Ʈ����һ�������Σ����ַ����ֺͼ����������ڶ�������
        //�ڶ��������У�Miss����ʹ������BreakUp()�������������ģ� �� СԲ�α�������BreakUp()�������ĸ�����
        //�����������У����ִӷ����ֵ�СԲ�ε�·��ָ�������ʹ������BreakUp()��Miss()���ظ����������� �� ��������ײ��СԲ��ʱСԲ��BreakUp()��������ĸ�����
        //���ĸ������У���ΪƮ��С�����Σ�����ʱ����򡰳ɹ���������С������ʧ��������ɢ�����Σ������������ģ���ͬ�ڶ������ģ��� Miss()���ظ����ĸ����� �� С�����α���������Ļ˲������ɫ���ظ����ĸ�����
        //����������У�Miss����ʹ������BreakUp()���ظ���������ģ� �� СԲ�α�������BreakUp()�������������ģ�ͬ���ĸ����ģ�
        //�����������У�Miss()���ظ����������� �� ����С��������Ļ˲������ɫ���ظ����������� �� ����򡰳ɹ���������С������ʧ��������ɢ�����Σ�Tutorial2()������isIntroduced��Ϊtrue
    }






    public void SongStart()//��ʼ������Ƶ�������䡢©������֡��ȶ�����֡�����̨����Ļ��ƽ���ƶ���ָ��λ��
    {
        audioPlayer.enabled = true;
        //SlowMove(������,��λ�ã�ָ��λ�ã�����ʱ��);
        //SlowMove(©������֣���λ�ã�ָ��λ�ã�����ʱ��);
        //SlowMove(�ȶ�����֣���λ�ã�ָ��λ�ã�����ʱ��);
        //SlowMove(����̨����λ�ã�ָ��λ�ã�����ʱ��);
    }

    private void Update()
    {
        //��ʽ��ʼʱ��ÿ֡��ʱ��ʵʱ���£�ÿ֡���ʱ���
        if (audioPlayer.enabled)
        {
            Timer();
            CheckTime(nowTime);
        }
    }
    public float Timer()//��ʱ�����ص�ǰʱ��
    {
        nowTime += Time.deltaTime;
        return nowTime;
    }

    private void CheckTime(float nowTime)//���ʱ���
    {
        if(nowTime==codeJudgmentTime-4000f) //���ʱ���,��Ϊ������ж���ǰ4000ms�򵯳�һ�������
        {
            //����(bdata.blockType)

        }
        if (nowTime == patchJudgmentTime - 1000f) //���ʱ���,��Ϊ�����ж���ǰ1000ms�򵯳�һ������
        {
            //����(pdata.patchType)
        }


        //���ݲ�ͬʱ��㣬��isOnPathΪfalse����Effect�ṩ���룬�ɷֶ�����ʵ��Ч��
        //���ɴ���Instantiate(Ԥ����,patchBornPos.position,Quaternion.identity);
    }
    public void LoadCodeBlockFromFile(string Path)//��Ϸ��ʼʱ��ָ��·��+�ļ�����ȡExcel
    {
        codeBlockData = CodeBlockExcelReader.ReadExcel(Path);
        if ( codeBlockData == null || codeBlockData.Count == 0)
        {
            Debug.LogError("NO_Block_DATA_FOUND");
        }
    }
    public void ReadBlock(string Path)//��ȡһ��codeBlock�����ͣ��ж�ʱ���id
    {
        var bdata = codeBlockData[blockCurrentLine];
        codeBlockID = bdata.codeBlockID;
        codeBlockType = bdata.codeBlockType;
        codeJudgmentTime = float.Parse(bdata.codeJudgmentTime);
        blockCurrentLine++;
    }

    public void LoadPatchFromFile(string Path)//��Ϸ��ʼʱ��ָ��·��+�ļ�����ȡExcel
    {
        patchData = PatchExcelReader.ReadExcel(Path);
        if (codeBlockData == null || codeBlockData.Count == 0)
        {
            Debug.LogError("NO_Patch_DATA_FOUND");
        }
    }
    public void ReadPatch(string Path)//��ȡһ��patch���ͣ��ж�ʱ���id
    {
        var pdata = patchData[patchCurrentLine];
        patchID = pdata.patchID;
        patchType = pdata.patchType;
        patchJudgmentTime = float.Parse(pdata.patchJudgmentTime);
        patchJudgmentTime++;
    }
}
