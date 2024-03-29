using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyMoveController : StateMachineBehaviour
{
    private Vector3 randomDestination; // ランダムな目的地
    private Vector3 initialPosition; // 初期位置
    private float moveRadius = 10f; // 移動範囲の半径
    [SerializeField] private float moveTimer = 0f; // 移動の経過時間
    [SerializeField]private float moveDuration = 3f; // 方向転換の間隔
    [SerializeField]private float _walkSpeed;
    [SerializeField] private Transform _player; // プレイヤーのTransformコンポーネント
    [SerializeField] float _chaseRadius = 10f; // 追いかける範囲

    // その他の変数や初期化処理を追加する場合はここに記述

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 初期位置を設定
        initialPosition = animator.transform.position;

        // ランダムな目的地を設定
        SetRandomDestination();

        // プレイヤーオブジェクトを検索して _player 変数に代入
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
        // 自身の位置とプレイヤーの位置の距離を計算
        float distanceToPlayer = Vector3.Distance(animator.transform.position, _player.position);

        if (distanceToPlayer <= _chaseRadius)
        {
            animator.SetBool("Chase", true); // 一定の範囲内にいる場合、Chaseステートに遷移するフラグを設定
        } 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        //if()
        //{
        //    animator.SetBool("Chase", false); // 範囲外にいる場合、Chaseステートに遷移するフラグを解除
        //}
    }

    private void SetRandomDestination()
    {
        // ランダムな方向に移動する目的地を設定
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 direction3D = new Vector3(randomDirection.x, 0f, randomDirection.y);
        randomDestination = initialPosition + direction3D * Random.Range(0f, moveRadius);
    }
}
