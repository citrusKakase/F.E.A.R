using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 锁门触发器 : MonoBehaviour
{
    public bool 上锁 = false;

    public int 开锁所需的道具ID = 0;   //默认空手开门，即无锁

    private bool 已开 = false;
    private ItemManager 背包管理器;
    public Text 旁白系统;
    public float 旋转速度;
    public Transform open;
    public Transform close;
    Transform 目标;

    [Header("这里填开门所用的道具名称")]
    [SerializeField]
    private string 所需的道具的名称;

    public AudioClip 开门音效, 关门音效;

    private AudioSource 音频源;

    private void Awake()
    {
        音频源 = GetComponent<AudioSource>();
    }

    public void Start()
    {
        背包管理器 = GameObject.FindWithTag("Player").GetComponent<ItemManager>();
        目标 = close;
    }

    public void 被交互()
    {
        Debug.Log(this.name + "被交互");

        //上锁的门需要对应钥匙才能打开
        if (上锁)
        {
            //开门需要手持对应道具
            if (背包管理器.当前手持的道具ID == 开锁所需的道具ID && 背包管理器.消耗道具(开锁所需的道具ID))
            {
                上锁 = !上锁;
                旁白系统.SendMessage("ShowDialog", "用" + 所需的道具的名称 + "打开了门");
                开或关门();
            }
            else
            {
                旁白系统.SendMessage("ShowDialog", "锁住了，打不开");
            }
        }
        else
        {
            开或关门();
        }

    }

    private void 开或关门()
    {
        //    音频源.Play();
        if (已开)
        {
            音频源.clip = 关门音效;
            音频源.Play();
            目标 = close;
            已开 = !已开;
        }
        else
        {
            音频源.clip = 开门音效;
            音频源.Play();
            目标 = open;
            已开 = !已开;
        }
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, 目标.rotation, Time.deltaTime * 旋转速度);
    }
}
