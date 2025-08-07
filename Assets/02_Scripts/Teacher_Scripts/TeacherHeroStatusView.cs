using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TeacherHeroStatusView : ViewBase
{
    // enum을 통해 Image 컴포넌트 바인딩
    public enum Images
    {
        HpBar,
        MpBar,
    }

    // enum을 통해 TMP 컴포넌트 바인딩
    public enum TMPs
    {
        HeroNameText
    }

    // enum을 통해 RectTransform 바인딩
    public enum Rts
    {
        InteractionGuide
    }

    [SerializeField] RectTransform _canvasRt;
    [SerializeField] Camera _uiCamera;

    private void Awake()
    {
        // 자식 게임오브젝트 컴포넌트 바인딩
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(TMPs));
        Bind<RectTransform>(typeof(Rts));
    }


    public void SetHpBar(float currentHp, float maxHp)
    {
        SetImageFillAmount((int)Images.HpBar, currentHp / maxHp);
    }
    public void SetHeroNameText(string heroName)
    {
        SetTMP((int)TMPs.HeroNameText, heroName);
    }

    public void SetInteractionGuide(Vector3 worldPos, bool isActive)
    {
        RectTransform interactionGuideRt = Get<RectTransform>((int)Rts.InteractionGuide);

        interactionGuideRt.gameObject.SetActive(isActive);
        if (isActive == false) return;

        // 월드 좌표 -> 스크린 좌표 변환
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);

        // 스크린 좌표 -> 캔버스 로컬 좌표 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRt, screenPoint, _uiCamera, out Vector2 localPoint);

        interactionGuideRt.anchoredPosition = localPoint;
    }
}
