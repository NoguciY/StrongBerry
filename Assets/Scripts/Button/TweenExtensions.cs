using DG.Tweening;
using Unity.VisualScripting;

//Tween��null�`�F�b�N���s���g�����\�b�h
public static class TweenExtensions
{
    public static void SafeKill(this Tween tween,bool complete = false)
    {
        if (tween != null)
        {
            tween.Kill(complete);
        }
    }

    public static void SafePause(this Tween tween)
    {
        if (tween != null)
        {
            tween.Pause();
        }
    }

    public static void SafeRestart(this Tween tween)
    {
        if (tween != null)
        {
            tween.Restart();
        }
    }
}
