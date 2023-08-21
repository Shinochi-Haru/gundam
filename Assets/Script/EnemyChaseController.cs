using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseController : StateMachineBehaviour
{
    private Transform _player; // プレイヤーのTransformコンポーネント
    [SerializeField] float _chaseSpeed = 0;
    [SerializeField] float _chaseRadius = 10f; // 追いかける範囲

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // プレイヤーオブジェクトを検索して _player 変数に代入
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 自身の位置とプレイヤーの位置の距離を計算
        float distanceToPlayer = Vector3.Distance(animator.transform.position, _player.position);

        // 一定の範囲内に近づいたら追いかける
        if (distanceToPlayer <= _chaseRadius)
        {
            // プレイヤーの方向を向く
            Vector3 direction = (_player.position - animator.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);

            // プレイヤーに向かって移動する
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, _player.position, Time.deltaTime * _chaseSpeed);
        }

        // スペースキーが押されたら呼び寄せステートに遷移
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Call", true);
        }

        if (distanceToPlayer > _chaseRadius)
        {
            animator.SetBool("Chase", false); // 範囲外に出た場合、Chaseステートに遷移するフラグを解除
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("Chase", false); // ステート終了時にフラグをリセット
    }
}
