using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMoveController : StateMachineBehaviour
{
    private Vector3 randomDestination; // �����_���ȖړI�n
    private Vector3 initialPosition; // �����ʒu
    private float moveRadius = 10f; // �ړ��͈͂̔��a
    [SerializeField] private float moveTimer = 0f; // �ړ��̌o�ߎ���
    [SerializeField]private float moveDuration = 3f; // �����]���̊Ԋu
    [SerializeField]private float _walkSpeed;
    [SerializeField] private Transform _player; // �v���C���[��Transform�R���|�[�l���g
    [SerializeField] float _chaseRadius = 10f; // �ǂ�������͈�

    // ���̑��̕ϐ��⏉����������ǉ�����ꍇ�͂����ɋL�q

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �����ʒu��ݒ�
        initialPosition = animator.transform.position;

        // �����_���ȖړI�n��ݒ�
        SetRandomDestination();

        // �v���C���[�I�u�W�F�N�g���������� _player �ϐ��ɑ��
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ړI�n�Ɍ������Ĉړ�
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, randomDestination, _walkSpeed * Time.deltaTime);

        // �i��ł�������Ɍ�����ς���
        Vector3 direction = randomDestination - animator.transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        // �����]���̊Ԋu���o�߂������A�ړI�n�ɋ߂Â����ꍇ�͐V�����ړI�n��ݒ�
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration || Vector3.Distance(animator.transform.position, randomDestination) < 1f)
        {
            SetRandomDestination();
            moveTimer = 0f;
        }
        // ���g�̈ʒu�ƃv���C���[�̈ʒu�̋������v�Z
        float distanceToPlayer = Vector3.Distance(animator.transform.position, _player.position);

        if (distanceToPlayer <= _chaseRadius)
        {
            animator.SetBool("Chase", true); // ���͈͓̔��ɂ���ꍇ�AChase�X�e�[�g�ɑJ�ڂ���t���O��ݒ�
        }
        else
        {
            animator.SetBool("Chase", false); // �͈͊O�ɂ���ꍇ�AChase�X�e�[�g�ɑJ�ڂ���t���O������
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }

    private void SetRandomDestination()
    {
        // �����_���ȕ����Ɉړ�����ړI�n��ݒ�
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 direction3D = new Vector3(randomDirection.x, 0f, randomDirection.y);
        randomDestination = initialPosition + direction3D * Random.Range(0f, moveRadius);
    }
}
