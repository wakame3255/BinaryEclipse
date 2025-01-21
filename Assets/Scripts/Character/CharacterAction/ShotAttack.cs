using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _shotCoolTime;

    //���������e�̃��X�g
    private List<List<BaseBullet>> _bulletList = new List<List<BaseBullet>>();

    private int _cacheShotCount;

    private bool _isCoolingDown = false;

    private void FixedUpdate()
    {
        foreach (List<BaseBullet> bulletList in _bulletList)
        {
            foreach (BaseBullet bullet in bulletList)
            {
                if (bullet.gameObject.activeSelf)
                {
                    bullet.MoveBullet();
                }
            }
        }
    }

    public void DoAttack(Vector3 TargerPosition)
    {
        if (_isCoolingDown) return;

        MyExtensionClass.CheckArgumentNull(TargerPosition, nameof(TargerPosition));

        int random = Random.Range(0, _bulletList.Count);
        List<BaseBullet> baseBullet = GetShotBullet(random, _cacheShotCount);
        print(_cacheShotCount);

        foreach (BaseBullet bullet in baseBullet)
        {
            bullet.GenerateBullet(transform.position, TargerPosition);
        }

        StartCoolDownTimerAsync();
    }

    /// <summary>
    /// ���\�[�X�̏�������
    /// </summary>
    /// <param name="bulletFactorys">�e�̍H��</param>
    public void SetResource(BaseBulletFactory[] bulletFactorys)
    {
        MyExtensionClass.CheckArgumentNull(bulletFactorys, nameof(bulletFactorys));

        for (int i = 0; i < bulletFactorys.Length; i++)
        {
            _cacheShotCount = bulletFactorys[i].ShotCount;
            print(bulletFactorys[i].name);

            _bulletList.Add(new List<BaseBullet>());
            _bulletList[i] = bulletFactorys[i].GetGenerateBullet();
        }
    }

    /// <summary>
    /// �e���擾����
    /// </summary>
    /// <param name="index">�擾�������e�̃C���f�N�X</param>
    /// <returns>�����Ƃ��\�Ȓe</returns>
    private List<BaseBullet> GetShotBullet(int index, int shotCount)
    {
        List<BaseBullet> baseBullet = new List<BaseBullet>();

        for (int i = 0; i < shotCount; i++)
        {
           baseBullet.Add(_bulletList[index][i]);
        }
        
        return baseBullet;
    }

    private async void StartCoolDownTimerAsync()
    {
        _isCoolingDown = true;
        await Task.Delay(_shotCoolTime * 1000);
        _isCoolingDown = false;
    }
}
