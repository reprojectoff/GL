using UnityEngine;

public class flashligthSimple : MonoBehaviour
{
    [SerializeField] private Light lightFonarick;
    [SerializeField] private AudioSource asF;
    [SerializeField] private bool Vkl;
    private void Start()
    {
        lightFonarick.enabled = Vkl;
    }
    void Update()
    {
        FlashLightOnOF();
    }
    public void FlashLightOnOF()
    {
        if (Input.GetKeyDown(KeyCode.F))
		{
            asF.Play();
            Vkl = !Vkl;
            lightFonarick.enabled = Vkl;
		}
    }
}
