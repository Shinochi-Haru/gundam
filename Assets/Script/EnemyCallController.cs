using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallController : StateMachineBehaviour
{
    [SerializeField] private float callRadius = 10f; // 呼び出し範囲

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 自身の位置
        Vector3 ownPosition = animator.transform.position;

        // 他の敵を取得
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // 自身と同じ敵はスキップ
            if (enemy == animator.gameObject)
            {
                continue;
            }

            // 呼び寄せる敵の位置
            Vector3 enemyPosition = enemy.transform.position;

            // 自身と呼び寄せる敵の距離を計算
            float distance = Vector3.Distance(ownPosition, enemyPosition);

            // 呼び寄せ範囲内にいる場合、呼び寄せ処理を行う（ここではログを出力）
            if (distance <= callRadius)
            {
                Debug.Log("敵を呼び寄せる処理を行います");
                Vector3 direction = (enemyPosition - ownPosition).normalized;
                float moveSpeed = 5f; // 移動速度を設定
                animator.transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        // スペースキーが押されたら呼び寄せステートに遷移
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Call", true);
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Call", false); // ステート終了時にフラグをリセット
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
