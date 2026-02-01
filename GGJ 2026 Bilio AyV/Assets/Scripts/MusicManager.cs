using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioMixer mixer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetVolumenGeneral(float valor)
    {
        mixer.SetFloat("Master", LinearAVolumen(valor));
    }

    public void SetVolumenMusica(float valor)
    {
        mixer.SetFloat("Music", LinearAVolumen(valor));
    }

    public void SetVolumenSFX(float valor)
    {
        mixer.SetFloat("SFX", LinearAVolumen(valor));
    }

    private float LinearAVolumen(float valor)
    {
        return Mathf.Log10(Mathf.Clamp(valor, 0.0001f, 1f)) * 20f;
    }
}
