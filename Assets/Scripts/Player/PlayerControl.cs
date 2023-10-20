using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, ITriggerCheckable
{
    // ���e��Prefab��ݒ�
    [SerializeField] GameObject BombPrefab;


    // �v���C���[�̈ړ����͂����m�����ۂɑ�������l
    float X = 0;
    float Z = 0;
    // �v���C���[�̈ړ����x
    float _playerMoveSpeed = 7.0f;

    // �v���C���[�̍s��
    // ���݃X�e�[�W�ɐݒu���Ă��锚�e�̐�
    int _bombPlaceCount;
    // �v���C���[���C�₵�Ă��邩
    bool _playerFainting = false;
    // �v���C���[������Ă��邩
    bool _playerDead = false;

    // �v���C���[�̃p���[�A�b�v
    // ���e������
    int _bombMaxCount = 1;
    // �Η�
    int _fireCount = 2;
    // �p���[�{����
    bool _powerBomb = false;

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
        // �ړ��֘A�̏���
        MovePlayer();
        // ���e�֘A�̏���
        BombMovement();

    }

    // �v���C���[�̈ړ��֘A�̏���
    void MovePlayer()
    {
        // �L�[���͂���l���擾
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");
        // ���͂��ꂽ�����Ɉړ�����
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
        // �{����ݒu����
        if (Input.GetButtonDown("Bomb_Place") && _bombMaxCount > _bombPlaceCount)
        {
            // ���e��Prefab�𐶐�
            GameObject bomb = Instantiate(BombPrefab, transform.position, Quaternion.identity);
            bomb.layer = LayerMask.NameToLayer("InitialBomb");
            // �������ݒu���Ă���{���̃J�E���g�𑝂₷
            ++_bombPlaceCount;



        }
        if (Input.GetButtonDown("Bomb_PickUp"))
        {

        }

        //kick bomb
        if (Input.GetButtonDown("Bomb_Kick"))
        {
            Debug.Log("1111111111111111");
            Debug.Log(BombOnFoot.StateMachine.CurrentItemState);

            //if bomb is on foot and bomb is idle, change state to bomb.OnKickState
            if ((BombOnFoot) && (BombOnFoot.StateMachine.CurrentItemState is ItemIdleState))
            {
                Debug.Log("22222222222");

                BombOnFoot.SetKickStatus(true);
                //Debug.Log(BombOnFoot.StateMachine.CurrentItemState);
            }

        }


    }
}
