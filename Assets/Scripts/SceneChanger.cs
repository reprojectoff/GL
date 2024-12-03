using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] private int scene;
    [SerializeField] private float transitionTime;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeScene(scene);
        }
    }
    public void ChangeScene(int scene)
    {
        StartCoroutine(LoadLevel(scene));
    }
    IEnumerator LoadLevel(int scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }
}
