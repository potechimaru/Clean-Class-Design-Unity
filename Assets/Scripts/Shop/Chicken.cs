using UnityEngine;
using DG.Tweening;
using UniRx;
using VContainer;

public class Chicken : MonoBehaviour
{
    [SerializeField] private GameObject swordIcon;
    [SerializeField] private GameObject grayBack;     // Grayバック
    [SerializeField] private RectTransform shopPanel; // メニュー本体（にゅいん用）

    private CanvasGroup swordIconCanvasGroup;
    private CanvasGroup grayBackCanvasGroup;
    private CanvasGroup shopPanelCanvasGroup;

    private bool isShowing = false;
    private bool playerInRange = false;
    private bool shopOpen = false;

    [Inject] private PlayerMVCFacade _facade;

    [Inject] private AttackUpgradeButton _attackUpgradeButton;
    [Inject] private ShieldUpgradeButton _shieldUpgradeButton;  

    private CompositeDisposable disposables = new();

    private void Awake()
    {
        // SwordIcon 初期化
        if (swordIcon != null)
        {
            swordIcon.SetActive(false);
            swordIconCanvasGroup = swordIcon.GetComponent<CanvasGroup>();
            if (swordIconCanvasGroup == null)
                swordIconCanvasGroup = swordIcon.AddComponent<CanvasGroup>();

            swordIconCanvasGroup.alpha = 0f;
            swordIcon.transform.localScale = Vector3.zero;
        }

        // GrayBack 初期化
        if (grayBack != null)
        {
            grayBack.SetActive(false);
            grayBackCanvasGroup = grayBack.GetComponent<CanvasGroup>();
            if (grayBackCanvasGroup == null)
                grayBackCanvasGroup = grayBack.AddComponent<CanvasGroup>();
            grayBackCanvasGroup.alpha = 0f;
        }

        // ShopPanel 初期化
        if (shopPanel != null)
        {
            shopPanel.gameObject.SetActive(false);
            shopPanelCanvasGroup = shopPanel.GetComponent<CanvasGroup>();
            if (shopPanelCanvasGroup == null)
                shopPanelCanvasGroup = shopPanel.gameObject.AddComponent<CanvasGroup>();
            shopPanelCanvasGroup.alpha = 0f;
            shopPanel.localScale = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShowing)
        {
            ShowSwordIcon();
            playerInRange = true;

            // Space キー監視
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
        if (grayBack == null || shopPanel == null || shopOpen) return;
        _attackUpgradeButton.LevelViewChange();
        _shieldUpgradeButton.LevelViewChange();

        shopOpen = true;

        // GrayBack表示＆フェード
        grayBack.SetActive(true);
        grayBackCanvasGroup.alpha = 0f;
        grayBackCanvasGroup.DOFade(1f, 0.3f);

        // ShopPanel表示＆にゅいん!!
        shopPanel.gameObject.SetActive(true);
        shopPanel.localScale = Vector3.zero;
        shopPanelCanvasGroup.alpha = 0f;

        shopPanel.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
        shopPanelCanvasGroup.DOFade(1f, 0.3f);

        //Debug.Log("Shop menu opened!");
    }

    private void CloseShop()
    {
        if (!shopOpen) return;

        shopOpen = false;

        // GrayBackフェードアウト
        grayBackCanvasGroup.DOFade(0f, 0.3f)
            .OnComplete(() => grayBack.SetActive(false));

        // ShopPanel縮小＋フェードアウト
        shopPanelCanvasGroup.DOFade(0f, 0.3f);
        shopPanel.DOScale(0f, 0.3f).SetEase(Ease.InBack)
            .OnComplete(() => shopPanel.gameObject.SetActive(false));

        //Debug.Log("Shop menu closed!");
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
