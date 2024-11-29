using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    //�ێ����Ă���e�H��̃��X�g
    private BaseBulletFactory[] _bulletFactorys;

    //���������e�̃��X�g
    private List<List<BaseBullet>> _bulletList = new List<List<BaseBullet>>();

    public void DoAttack(Vector3 TargerPosition)
    {
        int random = Random.Range(0, _bulletList.Count);
        GetShotBullet(random).GenerateBullet(transform.position, TargerPosition);
        print("ShotAttack");
    }

    /// <summary>
    /// ���\�[�X�̏�������
    /// </summary>
    /// <param name="bulletFactorys">�e�̍H��</param>
    public void SetResource(BaseBulletFactory[] bulletFactorys)
    {
        MyExtensionClass.CheckArgumentNull(bulletFactorys, nameof(bulletFactorys));

        _bulletFactorys = bulletFactorys;

        for (int i = 0; i < _bulletFactorys.Length; i++)
        {
            _bulletList.Add(new List<BaseBullet>());
            _bulletList[i] = _bulletFactorys[i].GetGenerateBullet();
        }
    }

    /// <summary>
    /// �e���擾����
    /// </summary>
    /// <param name="index">�擾�������e�̃C���f�N�X</param>
    /// <returns>�����Ƃ��\�Ȓe</returns>
    private BaseBullet GetShotBullet(int index)
    {
        BaseBullet baseBullet = null;

        foreach (BaseBullet bullet in _bulletList[index])
        {
            if (!bullet.gameObject.activeSelf)
            {
                return bullet;
            }
        }

        return baseBullet;
    }
}
