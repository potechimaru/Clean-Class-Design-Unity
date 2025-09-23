using UnityEngine;
using DG.Tweening;
using UniRx;
using VContainer;
using System.Collections.Generic;

public class Chicken : MonoBehaviour
{
    [SerializeField] private GameObject swordIcon;
    [SerializeField] private GameObject grayBack;

    [SerializeField] private List<RectTransform> shopPanels;

    [SerializeField] private RectTransform _nextButtomLeft;
    [SerializeField] private RectTransform _nextButtomRight;

    private CanvasGroup swordIconCanvasGroup;
    private CanvasGroup grayBackCanvasGroup;
    private List<CanvasGroup> shopPanelCanvasGroups = new List<CanvasGroup>();
    private CanvasGroup nextButtomLeftCanvasGroup;
    private CanvasGroup nextButtomRightCanvasGroup;

    private bool isShowing = false;
    private bool playerInRange = false;
    private bool shopOpen = false;

    [Inject] private PlayerMVCFacade _facade;
    [Inject] private AttackUpgradeButton _attackUpgradeButton;
    [Inject] private ShieldUpgradeButton _shieldUpgradeButton;
    [Inject] private HPUpgradeButton _hpUpgradeButton;
    [Inject] private BeamUpgradeButton _beamUpgradeButton;
    [Inject] private GetLowHealButton _getLowHealButton;
    [Inject] private GetHighHealButton _getHighHealButton;
    [Inject] private GetFullHealButton _getFullHealButton;
    [Inject] private GetRemedyButton _getRemedyButton;
    [Inject] private GetPowerBuffButton _getPowerBuffButton;
    [Inject] private GetSpeedBuffButton _getSpeedBuffButton;

    private CompositeDisposable disposables = new();

    private void Awake()
    {
        // SwordIcon ������
        if (swordIcon != null)
        {
            swordIcon.SetActive(false);
            swordIconCanvasGroup = swordIcon.GetComponent<CanvasGroup>() ?? swordIcon.AddComponent<CanvasGroup>();
            swordIconCanvasGroup.alpha = 0f;
            swordIcon.transform.localScale = Vector3.zero;
        }

        // GrayBack ������
        if (grayBack != null)
        {
            grayBack.SetActive(false);
            grayBackCanvasGroup = grayBack.GetComponent<CanvasGroup>() ?? grayBack.AddComponent<CanvasGroup>();
            grayBackCanvasGroup.alpha = 0f;
        }

        // ���� ShopPanel ������
        foreach (var panel in shopPanels)
        {
            if (panel == null) continue;
            panel.gameObject.SetActive(false);
            var cg = panel.GetComponent<CanvasGroup>() ?? panel.gameObject.AddComponent<CanvasGroup>();
            cg.alpha = 0f;
            panel.localScale = Vector3.zero;
            shopPanelCanvasGroups.Add(cg);
        }

        // �{�^��������
        if (_nextButtomLeft != null)
        {
            nextButtomLeftCanvasGroup = _nextButtomLeft.GetComponent<CanvasGroup>() ?? _nextButtomLeft.gameObject.AddComponent<CanvasGroup>();
            nextButtomLeftCanvasGroup.alpha = 0f;
        }

        if (_nextButtomRight != null)
        {
            nextButtomRightCanvasGroup = _nextButtomRight.GetComponent<CanvasGroup>() ?? _nextButtomRight.gameObject.AddComponent<CanvasGroup>();
            nextButtomRightCanvasGroup.alpha = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShowing)
        {
            ShowSwordIcon();
            playerInRange = true;

            _facade.SubmitStream
                .Where(_ => playerInRange)
                .Subscribe(_ => ToggleShop())
                .AddTo(disposables);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideSwordIcon();
            playerInRange = false;
            disposables.Clear();

            if (shopOpen)
                CloseShop();
        }
    }

    private void ShowSwordIcon()
    {
        isShowing = true;
        swordIcon.SetActive(true);

        swordIcon.transform.localScale = Vector3.zero;
        swordIcon.transform.DOScale(0.008f, 0.4f).SetEase(Ease.OutBack);
        swordIconCanvasGroup.DOFade(1f, 0.4f);
    }

    private void HideSwordIcon()
    {
        isShowing = false;

        swordIcon.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack);
        swordIconCanvasGroup.DOFade(0f, 0.3f).OnComplete(() =>
        {
            swordIcon.SetActive(false);
        });
    }

    private void ToggleShop()
    {
        if (shopOpen)
            CloseShop();
        else
            OpenShop();
    }

    private void OpenShop()
    {
        if (grayBack == null || shopPanels.Count == 0 || shopOpen) return;
        _attackUpgradeButton.LevelViewChange();
        _shieldUpgradeButton.LevelViewChange();
        _hpUpgradeButton.LevelViewChange();
        _beamUpgradeButton.LevelViewChange();
        _getLowHealButton.CostViewChange();
        _getHighHealButton.CostViewChange();
        _getFullHealButton.CostViewChange();
        //_getRemedyButton.CostViewChange();
        _getPowerBuffButton.CostViewChange();
        _getSpeedBuffButton.CostViewChange();

        shopOpen = true;

        grayBack.SetActive(true);
        grayBackCanvasGroup.alpha = 0f;
        grayBackCanvasGroup.DOFade(1f, 0.3f);

        // ���ׂĂ� shopPanel ���A�j���[�V�����\��
        foreach (var panel in shopPanels)
        {
            panel.gameObject.SetActive(true);
            panel.localScale = Vector3.zero;
        }
        foreach (var cg in shopPanelCanvasGroups)
        {
            cg.alpha = 0f;
        }
        foreach (var panel in shopPanels)
        {
            panel.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
        }
        foreach (var cg in shopPanelCanvasGroups)
        {
            cg.DOFade(1f, 0.3f);
        }

        _nextButtomLeft.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
        nextButtomLeftCanvasGroup.DOFade(1f, 0.4f);

        _nextButtomRight.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
        nextButtomRightCanvasGroup.DOFade(1f, 0.4f);
    }

    private void CloseShop()
    {
        if (!shopOpen) return;

        shopOpen = false;

        grayBackCanvasGroup.DOFade(0f, 0.3f)
            .OnComplete(() => grayBack.SetActive(false));

        // ���ׂĂ� shopPanel ���A�j���[�V������\��
        for (int i = 0; i < shopPanels.Count; i++)
        {
            var panel = shopPanels[i];
            var cg = shopPanelCanvasGroups[i];
            cg.DOFade(0f, 0.3f);
            panel.DOScale(0f, 0.3f).SetEase(Ease.InBack)
                .OnComplete(() => panel.gameObject.SetActive(false));
        }

        nextButtomLeftCanvasGroup.DOFade(0f, 0.3f);
        _nextButtomLeft.DOScale(0f, 0.3f).SetEase(Ease.InBack);

        nextButtomRightCanvasGroup.DOFade(0f, 0.3f);
        _nextButtomRight.DOScale(0f, 0.3f).SetEase(Ease.InBack);
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
