using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using Game.Utilities;
using Game.Interfaces;
using Game.Core;

namespace Game.UI
{
    public class RewardPopupController : MonoBehaviour
    {
        [Header("Reward Pop-up Elements")]
        public GameObject ui_panel_reward_popup;
        public Image ui_image_shine;
        public Image ui_image_reward_icon;
        public TextMeshProUGUI ui_text_reward_popup; 

        private void Start()
        {
            if (ui_panel_reward_popup != null)
                ui_panel_reward_popup.SetActive(false);
        }

        public void ShowRewardPopup(Sprite rewardSprite, string rewardName, int amount, Action onAnimationComplete)
        {
            var audioService = ServiceLocator.Get<IAudioService>();
            if (audioService != null) audioService.PlayWin();

            ui_panel_reward_popup.SetActive(true);
            ui_image_reward_icon.sprite = rewardSprite;
            ui_text_reward_popup.text = RewardFormatter.FormatReward(amount, rewardName);

            ui_image_reward_icon.transform.localScale = Vector3.zero;
            ui_text_reward_popup.transform.localScale = Vector3.zero;
            ui_image_shine.transform.localRotation = Quaternion.identity;

            ui_image_shine.transform.DORotate(new Vector3(0, 0, -360), AnimationConstants.SHINE_ROTATION_DURATION, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);

            Sequence seq = DOTween.Sequence();

            seq.Append(ui_image_reward_icon.transform.DOScale(Vector3.one * 1.5f, AnimationConstants.POPUP_REVEAL_DURATION).SetEase(Ease.OutBack));
            seq.Join(ui_text_reward_popup.transform.DOScale(Vector3.one, AnimationConstants.POPUP_REVEAL_DURATION).SetEase(Ease.OutBack));

            seq.AppendInterval(AnimationConstants.POPUP_DISPLAY_DELAY);

            seq.Append(ui_image_reward_icon.transform.DOScale(Vector3.zero, AnimationConstants.POPUP_HIDE_DURATION).SetEase(Ease.InBack));
            seq.Join(ui_text_reward_popup.transform.DOScale(Vector3.zero, AnimationConstants.POPUP_HIDE_DURATION).SetEase(Ease.InBack));

            seq.OnComplete(() =>
            {
                ui_image_shine.transform.DOKill();
                ui_panel_reward_popup.SetActive(false);
                onAnimationComplete?.Invoke();
            });
        }
    }
}
