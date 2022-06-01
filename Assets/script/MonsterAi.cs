using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    idle,
    run
}
public class MonsterAi : MonoBehaviour
{
    public EnemyState CurrentState = EnemyState.idle;
    public Animator ani;
    private Transform player;
    public NavMeshAgent agent;
 //   AnimatorStateInfo info;
    private float ��ת���� = 0;
    private bool isFollowAction = false;
    private bool isPatrolAction = true;
    public Vector3 waitPosition;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        double stopdistance = 1.5;
  //    info = ani.GetCurrentAnimatorStateInfo(0);
        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 v = player.position;

        //����׷��
        if (other.gameObject.tag == "Player")
        {
            isFollowAction = true;
            this.gameObject.GetComponent<Animator>().SetBool("run", true);
            isPatrolAction = false;
            transform.LookAt(v);
            agent.speed = 2f;
            agent.isStopped = false;
            agent.SetDestination(player.position);

            //׷������ ����die
            if (distance <= stopdistance)
            {
                this.gameObject.GetComponent<Animator>().SetBool("run", false);
                agent.isStopped = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�ص�Ѳ�ߵ�
        if(other.gameObject.tag == "Player")
        {
            transform.LookAt(waitPosition);
            agent.SetDestination(waitPosition);
            agent.speed = 0.1f;
            isFollowAction = false;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        waitPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Ѳ��״̬
        if (isPatrolAction)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, ��ת����++, 0));
            if (��ת���� >= 360) ��ת���� -= 360;
        }
        else
        {
            if (isFollowAction)
            {

            }
            else if (System.Math.Abs(waitPosition.sqrMagnitude - this.transform.position.sqrMagnitude) <= 1)
            {
                this.gameObject.GetComponent<Animator>().SetBool("run", false);
                agent.isStopped = true;
                isPatrolAction = true;
            }  
        }
        

        /*switch (CurrentState)
        {
            case EnemyState.idle:
                if (distance > stopdistance)
                {
                    CurrentState = EnemyState.run;
                }
                ani.Play("idle");
                transform.LookAt(v);
                agent.isStopped = true;
                break;
            case EnemyState.run:
                if (distance <= stopdistance)
                {
                    CurrentState = EnemyState.idle;
                }
                transform.LookAt(v);
                agent.speed = 1.5f;
                ani.Play("run");
                agent.isStopped = false;
                agent.SetDestination(player.position);
                break;
        }*/
    }
}
