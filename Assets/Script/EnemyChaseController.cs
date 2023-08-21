using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseController : StateMachineBehaviour
{
    private Transform _player; // �v���C���[��Transform�R���|�[�l���g
    [SerializeField] float _chaseSpeed = 0;
    [SerializeField] float _chaseRadius = 10f; // �ǂ�������͈�

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �v���C���[�I�u�W�F�N�g���������� _player �ϐ��ɑ��
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���g�̈ʒu�ƃv���C���[�̈ʒu�̋������v�Z
        float distanceToPlayer = Vector3.Distance(animator.transform.position, _player.position);

        // ���͈͓̔��ɋ߂Â�����ǂ�������
        if (distanceToPlayer <= _chaseRadius)
        {
            // �v���C���[�̕���������
            Vector3 direction = (_player.position - animator.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);

            // �v���C���[�Ɍ������Ĉړ�����
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, _player.position, Time.deltaTime * _chaseSpeed);
        }

        // �X�y�[�X�L�[�������ꂽ��Ăъ񂹃X�e�[�g�ɑJ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Call", true);
        }

        if (distanceToPlayer > _chaseRadius)
        {
            animator.SetBool("Chase", false); // �͈͊O�ɏo���ꍇ�AChase�X�e�[�g�ɑJ�ڂ���t���O������
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("Chase", false); // �X�e�[�g�I�����Ƀt���O�����Z�b�g
    }
}
