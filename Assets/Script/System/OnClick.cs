using UnityEngine;

public class OnClick : MonoBehaviour
{
    /// <summary> リザルト画面からネクストステージボタンを押した際に一度だけ呼びされる </summary>
    public void NextStageRelay()
    {
        GameDataManager.Instance.InGameManager.NextStage();
    }
}
