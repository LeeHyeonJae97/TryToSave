using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Vector3 Pos;
    private static int[] targetExps = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200 };
    //private static int[] targetExps = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

    public ConsumePointManager consumePointManager;
    public Joystick joyStick;
    public Transform model;
    public Rigidbody rb;

    public GameObject gainText;
    private Vector3 gainTextOrgPos;
    public float floatDuration;
    private Coroutine corGain;

    [SerializeField] private Stat[] stats;
    public Stat[] Stats { get { return stats; } }
    private Dictionary<string, Stat> statDic = new Dictionary<string, Stat>();

    public Image healthBar;
    private int curHp;
    public int CurHp
    {
        get { return curHp; }

        set
        {
            float maxHp = statDic["MaxHp"].Value;

            curHp = (int)Mathf.Clamp(value, 0, maxHp);

            float ratio = curHp / maxHp;
            healthBar.fillAmount = ratio;

            // 남은 체력에 따라 체력 바 색깔이 상이
            if (ratio <= 0) Debug.Log("GAME OVER");
            else if (ratio < 0.25f) healthBar.color = new Color32(238, 59, 53, 255);
            else if (ratio < 0.5f) healthBar.color = new Color32(236, 239, 53, 255);
            else healthBar.color = new Color32(112, 238, 53, 255);
        }
    }

    public Image fuelBar;
    private float curFuel;
    public float CurFuel
    {
        get { return curFuel; }

        set
        {
            float maxFuel = statDic["MaxFuel"].Value;

            curFuel = Mathf.Clamp(value, 0, maxFuel);

            float ratio = curFuel / maxFuel;
            fuelBar.fillAmount = ratio;
        }
    }

    public Text levelText;
    private int level;
    public int Level
    {
        get { return level; }

        set
        {
            level = value;
            levelText.text = string.Format("Level {0}", level + 1);
        }
    }

    public Image expBar;
    private int exp;
    public int Exp
    {
        get { return exp; }

        set
        {
            exp = value;

            if (exp >= targetExps[Level])
            {
                Exp -= targetExps[Level];
                LevelUp();
            }

            expBar.fillAmount = (float)exp / targetExps[Level];
        }
    }

    public int Point { get; set; }

    [HideInInspector] public Weapon[] weapons;

    private void Awake()
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i].Level = 0;
            statDic.Add(Stats[i].statName, Stats[i]);
        }
        //maxHp.Level = moveSpeed.Level = size.Level = maxFuel.Level = fuelEfficiency.Level = 0;

        gainTextOrgPos = gainText.GetComponent<RectTransform>().anchoredPosition3D;

        CurHp = (int)statDic["MaxHp"].Value;
        CurFuel = statDic["MaxFuel"].Value;
        Exp = 0;
        Level = 0;
        Point = 0;
        Pos = model.position;
        weapons = new Weapon[5];

        joyStick.move = Move;
    }

    private void FixedUpdate()
    {
        // Player의 위치를 참조하기 쉽게 하기 위해 static 변수에 현재 위치값을 저장
        Pos = model.position;

        // 충돌로 인해 속도가 생기지 않도록 조정
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Move(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            model.forward = dir;

            // 연료가 부족한 경우 속도 급감
            float speed = curFuel > 0 ? statDic["MoveSpeed"].Value : 2.5f;
            rb.MovePosition(rb.position + dir * Time.deltaTime * speed);

            // 플레이어의 연비에 따라 연료 소모량이 상이
            CurFuel -= 1 / statDic["FuelEfficiency"].Value;
        }
    }

    // 살릴까? 버릴까??
    private void LevelUp()
    {
        Level += 1;
        Point += 1;

        consumePointManager.SetActive(true);
    }

    // 최초에 시작 레벨이 1보다 높은 경우 게임 시작하면서 호출해버린다.
    public void LevelUp(int amount)
    {
        Level += amount;
        Point += amount;

        consumePointManager.SetActive(true);
    }

    public void Gain(string msg)
    {
        if (corGain != null) StopCoroutine(corGain);
        corGain = StartCoroutine(CorGain(msg));
    }

    private IEnumerator CorGain(string msg)
    {
        float curDuration = 0;
        gainText.gameObject.SetActive(true);
        gainText.GetComponent<RectTransform>().anchoredPosition3D = gainTextOrgPos;
        gainText.GetComponent<Text>().text = msg;
        
        // 텍스트가 위로 떠오르는 효과
        while (curDuration < floatDuration)
        {
            gainText.transform.Translate(-Vector3.forward * Time.deltaTime * 3);
            curDuration += Time.deltaTime;

            yield return null;
        }

        gainText.SetActive(false);
        corGain = null;
    }
}
