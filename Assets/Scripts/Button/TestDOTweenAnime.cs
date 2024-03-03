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

    //移動アニメ
    public void PlayMoveAnimation()
    {
        //座標を(100, 100, 0)へ1秒で移動
        transform.DOMove(new Vector3(100f, 100f, 0f), 1f);
    }

    //移動アニメ２
    public void PlayMoveAnimationTo()
    {
        //座標(-200, 0, 0)から座標(100, 100, 0)へ1秒で移動
        DOTween.To(
            () => new Vector3(-200f, 0f, 0f), 
            pos => transform.localPosition = pos, 
            new Vector3(100f, 100f, 0f), 1f)
            .Play();
    }

    //移動アニメ Sequence
    //複数のTweenを連結したり並行動作したりできる
    public void PlayMoveAnimationSequence()
    {
        Sequence sequence = DOTween.Sequence();

        //Append:現在のSequenceを追加
        sequence.Append(_image.DOFade(0f, 0.5f));

        //Join:現在のSequenceの最後尾と同じ位置に追加
        sequence.Join(_image.transform.DOLocalMoveX(0f, 0.5f));

        //Insert:指定位置に追加
        sequence.Insert(0.5f, _image.DOFade(1f, 0.5f));

        //コールバック:Append,Join,Insert可能
        sequence.AppendCallback(() =>
        {
            _image.color = Color.red;
        });

        //インターバル:Append,Insert可能
        sequence.AppendInterval(0.5f);

        //Tweenと同じ設定メソッドを使用できる
        sequence.OnComplete(() =>
        {
            _image.enabled = false;
        });

        sequence.Play();
    }

        //テキストの内容をアニメーションさせる
        public void PlayTextAnimation()
    {
        //0から10まで1秒でテキストをカウントアップさせる
        DOTween.To(
            () => 0,
            num => _text.text = num.ToString(),
            10,
            1f)
            .Play();
    }

    //拡縮アニメ
    public void PlayScaleAnimation()
    {
        //2倍の大きさに1秒で拡大
        transform.DOScale(new Vector3(2f, 2f, 1f), 1f).SetEase(Ease.OutQuart).Play();
    }

    //変色アニメ
    public void PlayColorAnimation()
    {
        //初期色から赤、赤から初期色を無限ループする
        _image.DOColor(Color.red, 1f).SetLoops(-1, LoopType.Yoyo).Play();
    }

    //変色アニメのループを任意のタイミングで止める
    public void StopColorAnimation()
    {
        if (_tween != null)
        {
            _tween.Kill();
        }
    }

    //回転アニメ
    public void PlayRotateAnimation()
    {
        //1秒で反時計回りに一周
        transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.LocalAxisAdd).Play();
    }
}
