using System;
using WaveLoader;
using UnityEngine;

public class Harmonizer : MonoBehaviour
{
    public AudioSource MusicPlayer;
    public WaveFile WaveFile;

    public Graphing Graph;

    public string WavePath;

    public float[] Sampler;

    void Update()
    {
        if (MusicPlayer.isPlaying)
        {
            Sampler = new float[8];
            MusicPlayer.GetOutputData(Sampler, 0);

            for (int i = 0; i < Sampler.Length; i++)
            {
                Graph.AddValue(Sampler[i] * 100);
            }
        }
    }

    public void LoadFile()
    {
        string loadPath = System.IO.Path.Combine(System.IO.Path.Combine(Environment.CurrentDirectory, "Wave"), WavePath);

        WaveFile = WaveFile.Load(loadPath);
        MusicPlayer.clip = WaveFile.ToAudioClip();

        MusicPlayer.Play();
    }
}
