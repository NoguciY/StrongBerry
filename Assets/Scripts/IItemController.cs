using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�v���C���[�ɐG�ꂽ�炱�̃N���X��GetComponent����
//���̃N���X�̃��\�b�h�����s���邱�ƂŁA�y�ɂ�����
//�������A���s�����������̈������قȂ�ꍇ�͂ǂ����邩�H
//�����A�C�e���ł��u�R�C���v�Ɓu�`�F�b�N�|�C���g�v��ExcuteItemAbility()�̈������قȂ�
//�����Ƃ́A�O����󂯎�肽���l(�f�[�^)
//�v���C���[����̃f�[�^���󂯎�肽������A�v���C���[�̃R���|�[�l���g�������ɂ���΂���

public interface IItemController
{
    //�v���C���[�ƏՓ˂������̏���
    void ExcuteItemAbility(PlayerManager playerManager);
}
