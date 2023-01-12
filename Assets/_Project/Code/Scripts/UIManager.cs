using System;
using System.Collections;
using System.Collections.Generic;
using LifeIsTheGame.TechnicalTest;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectionPanelRT, gameplayPanelRT;
    [SerializeField]
    private Button animation1Button, animation2Button, animation3Button, selectButton, backButton;

    private IPlayer _player;

    private void Start()
    {
        _player = GameManager.instance.player;
        InitializeListeners();
        StartSelection();
    }

    private void InitializeListeners()
    {
        animation1Button.onClick.AddListener(OnAnimation1Button);
        animation2Button.onClick.AddListener(OnAnimation2Button);
        animation3Button.onClick.AddListener(OnAnimation3Button);
        selectButton.onClick.AddListener(StartGameplay);
        backButton.onClick.AddListener(StartSelection);
    }

    private void OnAnimation1Button()
    {
        _player.playerAnimator.SetAnimation(PlayerAnimator.AnimationState.HouseDancing);
    }

    private void OnAnimation2Button()
    {
        _player.playerAnimator.SetAnimation(PlayerAnimator.AnimationState.MacarenaDance);
    }

    private void OnAnimation3Button()
    {
        _player.playerAnimator.SetAnimation(PlayerAnimator.AnimationState.WaveHipHopDance);
    }

    private void StartGameplay()
    {
        selectionPanelRT.gameObject.SetActive(false);
        gameplayPanelRT.gameObject.SetActive(true);

        _player.playerController.EnableController(true);
        _player.playerCameraController.SetFPSCamera();
    }

    private void StartSelection()
    {
        selectionPanelRT.gameObject.SetActive(true);
        gameplayPanelRT.gameObject.SetActive(false);

        _player.playerController.EnableController(false);
        _player.playerCameraController.SetSelectionCamera();
    }
}