using UnityEngine;
using System.Threading;
using Omnigen.Generators;

public class WavepointGenerator : MonoBehaviour
{
    public bool running;
    System.Random r = new System.Random();
    
    public Graphing Graphing;

    public MegaSystem MegaSystem;

    public double lastPosition;

    float max;

    int count;

    public float latestNovelty;

    private void Update()
    {
        if (!running)
        {
            running = true;
            //WaveProperties properties = GetWaveProperties();
            //Task.Run(() => CalculateWave(properties.Singularity, properties.Bailout, properties.Interval, properties.WaveFactor));
            new Thread(() =>
            {
                lastPosition = -1;
                CalculateWave(1, 4, 10f, r.Next(2, 10000));
                //CalculateWave(7, 6, 10f, 64);
            }).Start();

        }
    }

    void CalculateWave(double daysBeforeZeroPoint, double daysAfterZeroPoint, double timeInterval, int waveFactor)
    {
        WaveGenerator wave = new WaveGenerator(daysBeforeZeroPoint, daysAfterZeroPoint, timeInterval, waveFactor);
        wave.OnWavePointGenerated += OnWavePointGenerated;
        wave.OnWaveGenerationComplete += OnWaveGenerationComplete;
        wave.Generate();
    }

    void OnWavePointGenerated(object sender, WavePointGeneratedEventArgs args)
    {

        string[] parsed = args.Output.Replace(" ", "").Split(",".ToCharArray()[0]);

        if (lastPosition == -1)
        {
            lastPosition = double.Parse(parsed[0]);
        }

        double average = 0;
        for (int i = 1; i < parsed.Length; i++)
        {
            average += double.Parse(parsed[i]);
        }
        average /= 4;


        if (count == 512 - 4)
        {
            count = 0;
            max = 0;
        }
        count++;

        float data = (float)average * 10000000;

        if (data > max)
        {
            max = data;
        }

        float dump = ScaleBetweenNumber(data, 0, max, 0, 200);

        Graphing.AddValue(dump);
        MegaSystem.LevelOfNovelty = dump;
        latestNovelty = dump;

        Thread.Sleep((int)((lastPosition - double.Parse(parsed[0])) * 1000));
        lastPosition = double.Parse(parsed[0]);
    }

    void OnWaveGenerationComplete(object sender, WaveGenerationCompleteEventArgs args)
    {
        if (!args.Successful)
        {
            Debug.Log("Wave Generation Error");
        }
        running = false;
    }

    float ScaleBetweenNumber(float measurement, float measMin, float measMax, float scaleMin, float scaleMax)
    {
        return ((measurement - measMin) / (measMax - measMin)) * (scaleMax - scaleMin) + scaleMin;
    }
}
