using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Linq;
using UnityEngine.UI;

class LoadingView: View
{
    public Button submitButton;
    public InputField input;
    public Button homeButton;
    public Text messageText;

    public event Action<string> SubmitButtonPressed;
    public void Init()
    {
        submitButton.onClick.AddListener(OnClick_SubmitButton);
        homeButton.onClick.AddListener(OnClick_HomeButton);
    }

    private void OnClick_SubmitButton()
    {
        SubmitButtonPressed.Invoke(input.text);
        messageText.text = "";
    }

    private void OnClick_HomeButton()
    {
        ClosePanel();
    }

    public void OutputMessage(string message)
    {
        messageText.text = message;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
}
