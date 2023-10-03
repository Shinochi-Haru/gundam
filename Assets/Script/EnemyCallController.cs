using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallController : StateMachineBehaviour
{
    [SerializeField] private float callRadius = 10f; // �Ăяo���͈�

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���g�̈ʒu
        Vector3 ownPosition = animator.transform.position;

        // ���̓G���擾
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // ���g�Ɠ����G�̓X�L�b�v
            if (enemy == animator.gameObject)
            {
                continue;
            }

            // �Ăъ񂹂�G�̈ʒu
            Vector3 enemyPosition = enemy.transform.position;

            // ���g�ƌĂъ񂹂�G�̋������v�Z
            float distance = Vector3.Distance(ownPosition, enemyPosition);

            // �Ăъ񂹔͈͓��ɂ���ꍇ�A�Ăъ񂹏������s���i�����ł̓��O���o�́j
            if (distance <= callRadius)
            {
                Debug.Log("�G���Ăъ񂹂鏈�����s���܂�");
                Vector3 direction = (enemyPosition - ownPosition).normalized;
                float moveSpeed = 5f; // �ړ����x��ݒ�
                animator.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        // �X�y�[�X�L�[�������ꂽ��Ăъ񂹃X�e�[�g�ɑJ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Call", true);
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Call", false); // �X�e�[�g�I�����Ƀt���O�����Z�b�g
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
