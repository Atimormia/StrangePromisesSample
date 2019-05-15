﻿using strange.extensions.mediation.impl;
using System;
using System.Collections;
using UnityEngine;

class LoadingMediator: Mediator
{
    [Inject]
    public LoadingView View { get; set; }
    [Inject]
    public StartLoadingSignal LoadingPanelOpenedSignal { get; set; }
    [Inject]
    public FinishLoadingSignal FinishLoadingSignal { get; set; }
    [Inject]
    public OpenLoadingPanelSignal OpenLoadingPanelSignal { get; set; }

    public override void OnRegister()
    {
        View.Init();
        View.SubmitButtonPressed += OnSubmitButtonPressed;
        FinishLoadingSignal.AddListener(LoadingFinished);

        OpenLoadingPanelSignal.AddListener(View.OpenPanel);
    }
    public override void OnRemove()
    {
        View.SubmitButtonPressed -= OnSubmitButtonPressed;
        FinishLoadingSignal.RemoveListener(LoadingFinished);

        OpenLoadingPanelSignal.RemoveListener(View.OpenPanel);
    }

    private void OnSubmitButtonPressed(string url)
    {
        LoadingPanelOpenedSignal.Dispatch(url);
        Debug.Log("LoadingPanelOpenedSignal with "+url);
    }

    private void LoadingFinished(string result, bool successed)
    {
        View.OutputMessage(result);
        if (successed)
        {
            Wait(5);
            View.ClosePanel();
        }
    }
    private void Wait(int seconds)
    {
        DateTime ts = DateTime.Now + TimeSpan.FromSeconds(seconds);

        do { } while (DateTime.Now < ts);
    }
}
