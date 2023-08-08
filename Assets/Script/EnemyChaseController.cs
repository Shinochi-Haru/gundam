using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyChaseController : StateMachineBehaviour
{
    private Transform _player; // �v���C���[��Transform�R���|�[�l���g
    [SerializeField] float _chaseSpeed = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �v���C���[�I�u�W�F�N�g����������_player�ϐ��ɑ��
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        // �v���C���[�̕���������
        Vector3 direction = (_player.position - animator.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �v���C���[�Ɍ������Ĉړ�����
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, _player.position, Time.deltaTime * _chaseSpeed);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
