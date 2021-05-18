using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingOverlay;
    [SerializeField]
    [Range(0.01f, 3f)]
    private float fadeTime = 0.5f;

    public static SceneLoader Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(PerformLoadSceneAsync(sceneName));
    }

    private IEnumerator PerformLoadSceneAsync(string sceneName)
    {
        yield return StartCoroutine(FadeIn());

        var operation = SceneManager.LoadSceneAsync(sceneName);
        while(operation.isDone == false)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        yield return StartCoroutine(LerpAlpha(0, 1));
    }

    private IEnumerator FadeOut()
    {
        yield return StartCoroutine(LerpAlpha(1, 0));
    }

    /*
    * O segredo para fazer o check do while funcionar nas duas direcoes
    * Eh tirar vantagem da seguinte propriedade: se a < b, entao -a > -b (multiplica por -1 e inverte a desigualdade)
    *
    * Entao, em um caso de exemplo (generico), na direcao positiva (end > start)
    *     Ex1: start = 1, end = 2, alpha = 1.5 (no meio do caminho)
    *         A gente tem que o sign vai ser +1 (sign eh o sinal da speed, olhe o comentario na linha 75)
    *         A gente tem que o check (alpha*sign < end*sign) = (alpha < end) = (1.5 < 2)
    *         Que eh o check do while que a gente ta procurando (ele vai ser true ate alpha == end)
    * 
    * Agora o exemplo de um caso negativo (end < start)
    *     Ex2: start = 3, end = 1, alpha = 2
    *         A gente tem que o sign vai ser -1
    *         E o check (alpha*sign < end*sign) = (-alpha < -end) = (-2 < -1), que eh verdadeiro
    *         e vai ser verdadeiro ate o alpha == end
    */
    private IEnumerator LerpAlpha(float start, float end)
    {
        float speed = (end - start) / fadeTime;

        //sign retorna -1 para numeros negativos, +1 para numeros positivos e 0 para 0
        float sign = Mathf.Sign(speed);

        loadingOverlay.alpha = start;

        while (loadingOverlay.alpha*sign < end*sign)
        {
            loadingOverlay.alpha += speed * Time.deltaTime;
            yield return null;
        }
        loadingOverlay.alpha = end;
    }
}
