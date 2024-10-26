using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB : MonoBehaviour
{
    public GameObject objectToSpawn; // ��������I�u�W�F�N�g
    public float spawnInterval = 2f; // �����Ԋu
    [SerializeField] private float maxRadius = 115f; // �ő唼�a
    [SerializeField] private float minRadius = 20f;  // �ŏ����a

    private float timeSinceLastSpawn;
    private GameObject makeAmmo;
    [SerializeField][Tooltip("��������e�̐�")] private int sum; // �e�̑���
    private int cnt; // �������ꂽ�e�̐�

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0f; // �X�|�[���^�C�}�[�̏�����
        cnt = 0; // �������ꂽ�e�̐���������
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt < sum)
        {
            timeSinceLastSpawn += Time.deltaTime; // �o�ߎ��Ԃ��J�E���g

            // �����Ԋu�𒴂�����e�𐶐�
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnObject(); // �e�𐶐�
                cnt++; // �e�̐����J�E���g
                timeSinceLastSpawn = 0f; // �^�C�}�[�����Z�b�g
            }
        }
    }

    // �e�𐶐�����֐�
    void SpawnObject()
    {
        // �X�e�[�W�̍ő�E�ŏ��͈͓��Ń����_���ȋ������擾
        float distance = Random.Range(minRadius, maxRadius); // �ŏ��ƍő�̊Ԃ̋���

        // �����_���Ȋp�x���擾
        float angle = Random.Range(0f, 2 * Mathf.PI); // 0����2�΂܂ł̃����_���Ȋp�x

        // �ɍ��W����XY���W�֕ϊ����Ĉʒu���v�Z
        Vector3 randomPosition = new Vector3(
            distance * Mathf.Cos(angle),
            0f, // ������0�ɌŒ�i���ʏ�ł̐����j
            distance * Mathf.Sin(angle)
        );

        Vector3 spawnPosition = transform.position + randomPosition; // �X�|�[���ʒu���v�Z

        // �v���t�@�u�̃��[�e�[�V������K�p
        Quaternion spawnRotation = Quaternion.Euler(-90, Random.Range(0, 360f), 0); // ��]���w��i��: Y����]�j

        // �e�𐶐�
        makeAmmo = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

        // Ammo�R���|�[�l���g��ݒ�
        makeAmmo.GetComponent<Ammo>().SetCB(this);
    }

    // �e���j�󂳂ꂽ���ɌĂ΂��֐�
    public void DecreaseCount()
    {
        cnt--; // �e�̃J�E���g�����炷
    }
}
