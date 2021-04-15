using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    GameObject panel;

    void OnEnable()
    {
        panel = transform.GetChild(0).gameObject;
        AnimateOpen();
        SoundManager.instance.PlaySound(2);
    }

    void AnimateOpen()
    {
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    void AnimateClose()
    {
        panel.transform.localScale = Vector3.one;
        panel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InSine);
    }

    public void RetryClicked()
    {
        AnimateClose();
        Invoke("RestartScene", 0.5f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}
