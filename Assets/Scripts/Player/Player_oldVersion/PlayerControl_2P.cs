using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_2P : MonoBehaviour, ITriggerCheckable
{
    // 爆弾のPrefabを設定
    [SerializeField] GameObject BombPrefab;


    // プレイヤーの移動入力を検知した際に代入される値
    float X = 0;
    float Z = 0;
    // プレイヤーの移動速度
    float _playerMoveSpeed = 7.0f;

    // プレイヤーの行動
    // 現在ステージに設置している爆弾の数
    //int _bombPlaceCount;
    // プレイヤーが気絶しているか
   // bool _playerFainting = false;
    // プレイヤーがやられているか
    //bool _playerDead = false;

    // プレイヤーのパワーアップ
    // 爆弾所持数
    //int _bombMaxCount = 1;
    // 火力
   // int _fireCount = 2;
    // パワーボムか
   // bool _powerBomb = false;

    //kick
    public Bomb BombOnFoot { get; set; } = null;
    public void SetBombOnFoot(Bomb bomb)
    {
        BombOnFoot = bomb;
        Debug.Log("BombOnFoot is set to " + BombOnFoot);
    }

    #region ITriggerCheckable implementation
    public bool IsAggroed { get; set; } = false;

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 移動関連の処理
        MovePlayer();
        // 爆弾関連の処理
        BombMovement();

    }

    // プレイヤーの移動関連の処理
    void MovePlayer()
    {
        // キー入力から値を取得
        X = Input.GetAxis("Horizontal_2P");
        Z = Input.GetAxis("Vertical_2P");
        // 入力された方向に移動する
        // Calculate movement direction
        Vector3 move = new Vector3(X, 0.0f, Z);

        // Apply movement vector 
        transform.Translate(move * _playerMoveSpeed * Time.deltaTime, Space.World);

        // Change orientation to face the direction of movement
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), 0.15f);
        }
    }

    void BombMovement()
    {
        // ボムを設置する
        if (Input.GetButtonDown("Bomb_Place_2P"))
        {
            // 爆弾のPrefabを生成
            GameObject bomb = Instantiate(BombPrefab, transform.position, Quaternion.identity);
            bomb.layer = LayerMask.NameToLayer("InitialBomb");
            // 自分が設置しているボムのカウントを増やす
           // ++_bombPlaceCount;

            Debug.Log("Bomb_Place_2P ");

        }
        if (Input.GetButtonDown("Bomb_PickUp"))
        {

        }

        //kick bomb
        if (Input.GetButtonDown("Bomb_Kick_2P"))
        {
            Debug.Log("Bomb_Kick_2P ");
            //if bomb is on foot and bomb is idle, change state to bomb.OnKickState
            if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemIdleState))
            {
                BombOnFoot.SetKickedBy(this.transform);
                BombOnFoot.SetKickStatus(true);
                //Debug.Log(BombOnFoot.StateMachine.CurrentItemState);
            }

        }
    }
}
