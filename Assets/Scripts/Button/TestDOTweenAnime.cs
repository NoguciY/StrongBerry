using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TestDOTweenAnime : MonoBehaviour
{
    private Tween _tween;
    private Text _text;

    [SerializeField]
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        //PlayMoveAnimation();
        PlayMoveAnimationTo();
        PlayScaleAnimation();
        //PlayColorAnimation();
    }

    //�ړ��A�j��
    public void PlayMoveAnimation()
    {
        //���W��(100, 100, 0)��1�b�ňړ�
        transform.DOMove(new Vector3(100f, 100f, 0f), 1f);
    }

    //�ړ��A�j���Q
    public void PlayMoveAnimationTo()
    {
        //���W(-200, 0, 0)������W(100, 100, 0)��1�b�ňړ�
        DOTween.To(
            () => new Vector3(-200f, 0f, 0f), 
            pos => transform.localPosition = pos, 
            new Vector3(100f, 100f, 0f), 1f)
            .Play();
    }

    //�ړ��A�j�� Sequence
    //������Tween��A����������s���삵����ł���
    public void PlayMoveAnimationSequence()
    {
        Sequence sequence = DOTween.Sequence();

        //Append:���݂�Sequence��ǉ�
        sequence.Append(_image.DOFade(0f, 0.5f));

        //Join:���݂�Sequence�̍Ō���Ɠ����ʒu�ɒǉ�
        sequence.Join(_image.transform.DOLocalMoveX(0f, 0.5f));

        //Insert:�w��ʒu�ɒǉ�
        sequence.Insert(0.5f, _image.DOFade(1f, 0.5f));

        //�R�[���o�b�N:Append,Join,Insert�\
        sequence.AppendCallback(() =>
        {
            _image.color = Color.red;
        });

        //�C���^�[�o��:Append,Insert�\
        sequence.AppendInterval(0.5f);

        //Tween�Ɠ����ݒ胁�\�b�h���g�p�ł���
        sequence.OnComplete(() =>
        {
            _image.enabled = false;
        });

        sequence.Play();
    }

        //�e�L�X�g�̓��e���A�j���[�V����������
        public void PlayTextAnimation()
    {
        //0����10�܂�1�b�Ńe�L�X�g���J�E���g�A�b�v������
        DOTween.To(
            () => 0,
            num => _text.text = num.ToString(),
            10,
            1f)
            .Play();
    }

    //�g�k�A�j��
    public void PlayScaleAnimation()
    {
        //2�{�̑傫����1�b�Ŋg��
        transform.DOScale(new Vector3(2f, 2f, 1f), 1f).SetEase(Ease.OutQuart).Play();
    }

    //�ϐF�A�j��
    public void PlayColorAnimation()
    {
        //�����F����ԁA�Ԃ��珉���F�𖳌����[�v����
        _image.DOColor(Color.red, 1f).SetLoops(-1, LoopType.Yoyo).Play();
    }

    //�ϐF�A�j���̃��[�v��C�ӂ̃^�C�~���O�Ŏ~�߂�
    public void StopColorAnimation()
    {
        if (_tween != null)
        {
            _tween.Kill();
        }
    }

    //��]�A�j��
    public void PlayRotateAnimation()
    {
        //1�b�Ŕ����v���Ɉ��
        transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.LocalAxisAdd).Play();
    }
}
