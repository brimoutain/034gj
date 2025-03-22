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

    [SerializeField] private float flyduration;//Ʈ��ȥ����ʱ��
    bool isFinished = false;
    [SerializeField] private float clearduration;//������������ʱ��

    public GameObject title;

    public float moveSpeed = 2f;     // �ƶ��ٶ�
    public float changeInterval = 3f; // ����仯���

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


    private bool isIntroducted = true;//�ж��Ƿ�����̳̣�������Ĭ�Ͻ���
    private float startTime;//��ʼ�������ֵ�ʱ��
    private float nowTime;//��ǰʱ�䣬�����ж�����Ԥ����

    [SerializeField] private GameObject tringle;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject codeBlockTringle;
    [SerializeField] private GameObject codeBlockCircle;

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
        outPoint.position = new Vector3(0, -5.5f, 0);
        stopPoint.position = new Vector3(0,-3.75f,0);
    }
    void Start()//��ȡExcel���ݣ���ʼ��Ϸ
    {
        LoadCodeBlockFromFile("Assets/Resources/2.xlsx");
        LoadPatchFromFile("Assets/Resources/3.xlsx");
        AudioClip audioClip = Resources.Load<AudioClip>("Assets/Resources/");
        //������ڲ�����Ч
        GameObject game = Instantiate(tringle, outPoint);
        game.GetComponent<BaseObject>().Effect(2);
        //
        StartGame();
    }
    public void StartGame()
    {
        FirstAnim();
        #region 1
        //��ʼ��Ϸʱ�����ŵ�һ��������һ������������Ư������������������ƶ�����ָ��λ��
        //��һ����������ʱ���ŵڶ�������������͸������0�𽥱�ɼ� ����
        //������������������������������������������ƶ����������ʱ�жϵڶ������������ŵ��ĸ�����
        //�ڶ�����������ʱ�жϵ��������������ŵ��ĸ������������ɵ�ǰ͸�����𽥱�Ϊ0����������ƽ�Ƶ��ж����м�
        //���ĸ���������ʱ����Tutorial()������̳�
        #endregion
    }

    //����ƶ��ķ���
    //IEnumerator ChangeDirection()
    //{
    //    while (true)
    //    {
    //        // ����������򣨵�λ������
    //        randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
    //        yield return new WaitForSeconds(changeInterval);
    //    }
    //}

    //�ֶ�����·����
    private void FirstAnim()
    {
        iTringle = Instantiate(tringle, outPoint.position, Quaternion.identity);
        iTringle.transform.DOPath(
           new[] {
                iTringle.transform.position,
                new Vector3(iTringle.transform.position.x + 2f,iTringle.transform.position.y + 5f, 0),
                new Vector3(iTringle.transform.position.x - 2f, iTringle.transform.position.y + 10f, 0),
                // ���Ӹ���·����
           }, flyduration, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() => SecondAnim());
    }
    private void SecondAnim()
    {
        title.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), clearduration);
        isFinished = true;
    }

    public void Tutorial1()
    {
        //�̳̿�ʼʱ����ʼ����һ��ѭ�����н��ĵ�����
        audioPlayer.Play();
        //ÿһ�������ζ���һ��
        //ÿ������ǰ100ms�����η���С���Σ���Ļ�Ϸ���ʼ�ȼ�������һ��С������
        //��������ʱС���Ǻ������غϣ�������ʱ��ҵ����������ɢ������Σ�ֹͣ����С���ǲ�����Tutorial2()
        //,δ����򲥷�С������Miss()��������һ������ѭ��
    }
    public void Tutorial2()
    {
        //����������Tutorial1()������
        //ÿһ�������ζ���һ��
        //ÿ������ǰ100ms�����η���С���Σ���ʼ�ȼ�������һ��СԲ��
        //��һ�������У�������ʱ���δ�����СԲ��Miss()���ظ���һ������ �� ����������κ�СԲ��BreakUp()��
        //������Ļ˲������ɫ������Ʈ����һ�������Σ����ַ����ֺͼ����������ڶ�������
        //�ڶ��������У�Miss����ʹ������BreakUp()�������������ģ� �� СԲ�α�������BreakUp()��̳̽���
        //�����������У����ִӷ����ֵ�СԲ�ε�·��ָ�������ʹ������BreakUp()��Miss()���ظ����������� ��
        //��������ײ��СԲ��ʱСԲ��BreakUp()���̳̽���
        //isIntroduced��Ϊtrue
    }

    public void SongStart()//��ʼ������Ƶ�������䡢©������֡��ȶ�����֡�����̨����Ļ��ƽ���ƶ���ָ��λ��
    {
        //��������ʽ��Ƶ
        audioPlayer.enabled = true;
        //SlowMove(������,��λ�ã�ָ��λ�ã�����ʱ��);
        //SlowMove(©������֣���λ�ã�ָ��λ�ã�����ʱ��);
        //SlowMove(�ȶ�����֣���λ�ã�ָ��λ�ã�����ʱ��);
        //SlowMove(����̨����λ�ã�ָ��λ�ã�����ʱ��);
    }
    public void End()
    {
        //��ƵƬ���л�Ϊ��������
        //�����䣬©������֣�����̨���������ɱ������ȫ�������������ȶ������
        //��Ļ���ĳ��ּ���������ʮ����ɫ��������ɣ�
        //���ɸ�������Ϊ�޸��Ĵ�������������δ��ȶ�����ַ����������ָ��λ�ã���a�������ηɵ�a/n���ȣ�nΪ�����������
        //ÿ��һ�������ηɵ���������ɼ�+1�����ɼ�С������90%ʱ��ÿ��10%��ʹ��������һ�������α��� �� ���ɼ�����90%ʱÿ��1%��ʹ��������һ�������α��
    }

    private void Update()
    {      
        if (audioPlayer.enabled)
        {
            nowTime += Time.deltaTime;
            CheckTime();
        }
        //�����Ķζ���1.�ڶ��β����꣬2.�����
        ForthAnim();
        //�����ʹ����ȫ������������
        if (codeBlockCurrentLine >= codeBlockData.Count || patchCurrentLine >= patchData.Count)
        {
            end = true;
        }

    }

    private void ForthAnim()
    {
        if (isFinished && (Input.GetMouseButtonDown(0) || title.GetComponent<Image>().color.a == 1))
        {
            //���Ķζ���,���õ�clearduration������Ҫ�ɸ�
            title.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), flyduration);
            iTringle.transform.DOMove(outPoint.position, flyduration).OnComplete(() => Tutorial1());
        }
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

        //Instantiate(tringle,patchBornPos.position,Quaternion.identity);
        //��Ҫ������Ч������λ��

        //����©�������
        //�ж�©�������������

        //�жϵ�������׼ȷ��
        //������Ч
    }

    #region ExcelReaderFuction
    public void LoadCodeBlockFromFile(string Path)//��Ϸ��ʼʱ��ָ��·��+�ļ�����ȡExcel
    {
        codeBlockData = CodeBlockExcelReader.ReadExcel(Path);
        if (codeBlockData == null || codeBlockData.Count == 0)
        {
            Debug.LogError("NO_Block_DATA_FOUND");
        }
    }
    public void ReadBlock(string Path)//��ȡһ��codeBlock�����ͣ��ж�ʱ���id
    {
        var bdata = codeBlockData[codeBlockCurrentLine];
        codeBlockID = bdata.codeBlockID;
        codeBlockType = bdata.codeBlockType;
        codeJudgmentTime = float.Parse(bdata.codeJudgmentTime);
        codeBlockCurrentLine++;
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
    #endregion
}
