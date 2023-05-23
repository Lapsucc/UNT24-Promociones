using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private Image progressBar;
    public static string scene;
    public static string sceneInfo;

    private void Start()
    {
        StartCoroutine(LoadScene(scene));
    }

    private IEnumerator LoadScene(string scene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        while (op.progress < 1)
        {
            progressBar.fillAmount = op.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
