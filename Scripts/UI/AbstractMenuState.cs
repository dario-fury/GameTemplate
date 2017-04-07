﻿using Promises;
using UnityEngine;

public abstract class AbstractMenuState : AbstractState
{
    private readonly string _resourcePath;

    protected MenuScreen Screen;

    protected abstract AbstractMenuState BackButtonState { get; }

    protected AbstractMenuState(string uiScreenResourcePath)
    {
        _resourcePath = uiScreenResourcePath;
    }

    public override IPromise OnEnter()
    {
        GameManager.EventSystem.enabled = false;

        return ResourceExtensions.LoadAsync(_resourcePath)
            .ThenDo<GameObject>(HandleResourceLoaded)
            .Then(GameManager.LoadingScreen.AnimateOff)
            .Then(() => Screen.Animator.AnimateOn())
            .ThenDo(() =>
            {
                Screen.ButtonPressedEvent += HandleButtonPressed;
                Screen.BackButtonPressedEvent += HandleBackButtonPressed;
                GameManager.EventSystem.enabled = true;
            });
    }

    public override IPromise OnExit()
    {
        GameManager.EventSystem.enabled = false;
        Screen.CanFireEvents = false;
        Screen.ButtonPressedEvent -= HandleButtonPressed;
        Screen.BackButtonPressedEvent -= HandleBackButtonPressed;

        return Screen.Animator.AnimateOff();
    }

    public override void CleanUp()
    {
        Object.Destroy(Screen.gameObject);

        if (!(NextState is AbstractMenuState))
            GameManager.EventSystem.enabled = true;
    }

    protected virtual void HandleResourceLoaded(GameObject loadedObject)
    {
        Screen = GameManager.Canvas.InstantiateBehindLoadingScreen(loadedObject).GetComponent<MenuScreen>();
    }

    private void HandleBackButtonPressed()
    {
        NextState = BackButtonState;
    }

    protected virtual void HandleButtonPressed(int i)
    {
    }
}