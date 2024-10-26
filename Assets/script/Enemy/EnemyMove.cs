using System.Collections;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float enemyLifeTime;
    [SerializeField] private Transform player;
    [SerializeField] private float stopDistance = 10f;  // �v���C���[�Ƃ̒�~����
    [SerializeField] private float fleeDistance = 5f;   // �v���C���[�Ƃ̓�������
    [SerializeField] private float approachDistance = 15f;  // �v���C���[�Ɍ������Đi�ދ���
    [SerializeField] private GameObject enemyBulletPrefab; // �e��Prefab
    [SerializeField] private float bulletSpeed = 10f;   // �e�̑��x
    [SerializeField] private float shootCooldown = 2f;   // �e���˂̃N�[���_�E��
    public bool isFrozen = false;  // �������
    private bool canShoot = true;  // �e���ˉ\���ǂ���
    private Vector3 randomDestination;  // �����_���ȖړI�n
    private float randomMoveCooldown = 5f;  // �����_���ړ����X�V����Ԋu
    private float randomMoveTimer;

    [SerializeField] private float maxRange = 30f;  // �G�l�~�[���ړ��ł���ő勗��
    [SerializeField] private float minRange = 5f;   // �G�l�~�[���ړ��ł���ŏ�����

    private ScoreManager1 scoreManager; // �X�R�A���Ǘ�����ScoreManager

    // ���ʉ����Đ����邽�߂�AudioClip��AudioSource
    [SerializeField] private AudioClip hitSE; // �o���b�g�ɓ��������Ƃ��̌��ʉ�
    [SerializeField] private AudioClip shootSE; // �e���ˎ��̌��ʉ�
    private AudioSource audioSource; // ���ʉ����Đ�����AudioSource

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // ScoreManager�̎Q�Ƃ��擾
        scoreManager = FindObjectOfType<ScoreManager1>();

        // AudioSource��ǉ����Ď擾
        audioSource = gameObject.AddComponent<AudioSource>();

        // �ŏ��̃����_���ȖړI�n��ݒ�
        SetRandomDestination();
    }

    void Update()
    {
        if (!isFrozen && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // �v���C���[�ɋ߂Â������Ă��Ȃ��ꍇ�Ƀ����_���ړ�
            if (distanceToPlayer > approachDistance)
            {
                RandomMove();
            }
            else if (distanceToPlayer <= fleeDistance)
            {
                // �v���C���[���瓦����
                Vector3 direction = (transform.position - player.position).normalized;
                MoveAndClampPosition(direction);
            }
            else if (distanceToPlayer <= stopDistance)
            {
                // ��~�����ȓ��Ȃ�U��
                if (canShoot)
                {
                    StartCoroutine(ShootAtPlayer());
                }
            }
            else
            {
                // �v���C���[�Ɍ������Đi��
                Vector3 direction = (player.position - transform.position).normalized;
                MoveAndClampPosition(direction);
            }
        }

        enemyLifeTime -= Time.deltaTime;
        if (enemyLifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    // �����_���ړ��̏���
    private void RandomMove()
    {
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f)
        {
            SetRandomDestination();
        }

        // �����_���ȖړI�n�Ɍ������Ĉړ�
        Vector3 direction = (randomDestination - transform.position).normalized;
        MoveAndClampPosition(direction);

        // �ړI�n�ɓ���������A�V�����ړI�n��ݒ�
        if (Vector3.Distance(transform.position, randomDestination) < 1f)
        {
            SetRandomDestination();
        }
    }

    // �����_���ȖړI�n��ݒ肷��
    private void SetRandomDestination()
    {
        float randomDistance = Random.Range(minRange, maxRange); // �ŏ��͈͂ƍő�͈͂̊Ԃ̋���
        float randomAngle = Random.Range(0f, 2 * Mathf.PI); // �����_���Ȋp�x

        // �ɍ��W����V�����ړI�n��X, Z���W���v�Z
        randomDestination = new Vector3(
            randomDistance * Mathf.Cos(randomAngle),
            transform.position.y, // Y���͌Œ�
            randomDistance * Mathf.Sin(randomAngle)
        );

        randomMoveTimer = randomMoveCooldown;

        ClampPosition(ref randomDestination); // �͈͊O�ɏo�Ȃ��悤�ɖړI�n�𒲐�
    }

    // �G�l�~�[�̈ړ��͈͂𐧌����鏈��
    private void ClampPosition(ref Vector3 position)
    {
        float distanceFromCenter = Vector3.Distance(Vector3.zero, position);

        // �ő�͈͂𒴂��Ă���΁A�ő�͈͓��ɐ���
        if (distanceFromCenter > maxRange)
        {
            position = position.normalized * maxRange;
        }

        // �ŏ��͈͂������ł���΁A�ŏ��͈͂ɐ���
        if (distanceFromCenter < minRange)
        {
            position = position.normalized * minRange;
        }
    }

    // �G�l�~�[�̈ړ��Ɣ͈͐���
    private void MoveAndClampPosition(Vector3 direction)
    {
        transform.Translate(direction * enemySpeed * Time.deltaTime, Space.World);

        // �G�l�~�[�̌��݈ʒu��͈͓��ɐ�������
        Vector3 clampedPosition = transform.position;
        ClampPosition(ref clampedPosition);
        transform.position = clampedPosition;
    }

    private IEnumerator ShootAtPlayer()
    {
        canShoot = false; // �e���˃N�[���_�E���J�n

        // �e�̐����Ɣ���
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        // �e���ˎ��̌��ʉ����Đ�
        PlayShootSE();

        yield return new WaitForSeconds(shootCooldown); // �N�[���_�E����҂�
        canShoot = true; // ���̒e���ˉ\
    }

    // �G�l�~�[���v���C���[�̒e�ɓ��������ꍇ�̏���
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // ���ʉ����Đ�
            PlayHitSE();

            Destroy(gameObject);
        }
    }

    // ���ˎ��̌��ʉ����Đ����郁�\�b�h
    private void PlayShootSE()
    {
        if (shootSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSE); // ���ˎ��̌��ʉ�����x�Đ�
        }
    }

    // ��e���̌��ʉ����Đ����郁�\�b�h
    private void PlayHitSE()
    {
        if (hitSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSE); // ��e���̌��ʉ�����x�Đ�
        }
    }
}
