using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : StateMachineBehaviour
{
    private Vector3 randomDestination; // ランダムな目的地
    private Vector3 initialPosition; // 初期位置
    private float moveRadius = 10f; // 移動範囲の半径
    [SerializeField] private float moveTimer = 0f; // 移動の経過時間
    [SerializeField]private float moveDuration = 3f; // 方向転換の間隔
    [SerializeField]private float _walkSpeed;

    // その他の変数や初期化処理を追加する場合はここに記述

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 初期位置を設定
        initialPosition = animator.transform.position;

        // ランダムな目的地を設定
        SetRandomDestination();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 目的地に向かって移動
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, randomDestination, _walkSpeed * Time.deltaTime);

        // 進んでいる方向に向きを変える
        Vector3 direction = randomDestination - animator.transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        // 方向転換の間隔が経過したか、目的地に近づいた場合は新しい目的地を設定
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration || Vector3.Distance(animator.transform.position, randomDestination) < 1f)
        {
            SetRandomDestination();
            moveTimer = 0f;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 状態終了時の処理が必要な場合はここに記述
    }

    private void SetRandomDestination()
    {
        // ランダムな方向に移動する目的地を設定
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 direction3D = new Vector3(randomDirection.x, 0f, randomDirection.y);
        randomDestination = initialPosition + direction3D * Random.Range(0f, moveRadius);
    }
}
