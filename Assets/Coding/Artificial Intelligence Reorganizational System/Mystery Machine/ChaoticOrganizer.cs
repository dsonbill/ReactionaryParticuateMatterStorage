using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Written by Lucifer & Alterous

public class ChaoticOrganizer : MonoBehaviour
{
    public class Payload
    {
        public readonly Char Designation;
        public readonly int Information;

        public readonly int Link;
        public readonly int Code;
        public readonly string Target;

        public readonly int Latch;

        public readonly bool Locked;

        public readonly string Solver;

        public readonly string Dump;

        public Payload(Char designation, int information, int link, int code, string target, int latch, bool locked, string solver, string crap)
        {
            Designation = designation;
            Information = information;

            Link = link;
            Code = code;
            Target = target;

            Latch = latch;

            Locked = locked;

            Solver = solver;

            Dump = crap;
        }
    }

    //float min = float.MaxValue;
    //float max = 0;
    //
    //void Update()
    //{
    //    float current = RandomFunction(GenerateNewRandom(0, int.MaxValue));
    //    if (min > current)
    //    {
    //        min = current;
    //        Debug.Log("Min: " + min);
    //    }
    //    if (max < current)
    //    {
    //        max = current;
    //        Debug.Log("Max: " + max);
    //    }
    //    
    //}

    static public ChaoticOrganizer Instance;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    System.Random r = new System.Random();
    public int GenerateNewRandom(int min, int max)
    {
        return r.Next(min, max);
    }

    float RandomFunction(double randomness)
    {
        double obfuscated = randomness;

        double Beginning = 1;
        double End = double.PositiveInfinity;

        double key;


        key = 1 / obfuscated;
        key = key * obfuscated; 

        float Proof = (float)Math.Sqrt(obfuscated) * 4.7912f;
        Proof = (Proof * (float)obfuscated) + 2;
        Proof = Proof / (float)obfuscated;
        Proof = Proof * 40;
        Proof = Proof - 360;
        Proof = Proof / 40;
        Proof = Proof * (float)obfuscated;
        Proof = Proof / ((float)obfuscated - 2);
        //Proof = (float)Math.Pow(Proof, 4.7912f) / (float)obfuscated;
        //Proof = -MathF.Pow(Proof, (float)obfuscated) / 4.7912f;
        Proof = Proof / ((float)obfuscated + 2);
        Proof = Proof * (float)obfuscated;
        Proof = Proof / 40;
        Proof = Proof + 360;
        Proof = Proof * 40;
        Proof = Proof / (float)obfuscated;
        Proof = (Proof * (float)obfuscated) + 2;
        Proof = (float)Math.Sqrt(Proof) * 4.7912f;

        return Proof;
    }

    string ReplacementSystem(string input, char replacement, int start, int end)
    {
        string output = input;
        for (int i = start; i <= end; i++)
        {
            output = output.Replace((char)i, replacement);
        }
        return output;
    }


    float ScaleBetweenNumber(float measurement, float measMin, float measMax, float scaleMin, float scaleMax)
    {
        return ((measurement - measMin) / (measMax - measMin)) * (scaleMax - scaleMin) + scaleMin;
    }

    public Payload Pump()
    {
        int mom = GenerateNewRandom(23, 26);

        int rand = GenerateNewRandom(0, int.MaxValue);

        int rand1 = GenerateNewRandom(1, 4652);
        int rand2 = GenerateNewRandom(2, 28465);
        int rand3 = GenerateNewRandom(3, 79135);
        int rand4 = GenerateNewRandom(4, 465);
        int rand5 = GenerateNewRandom(5, 748195263);
        int rand6 = GenerateNewRandom(6, 748159623);
        int rand7 = GenerateNewRandom(7, 326951487);
        int rand8 = GenerateNewRandom(8, 784951623);

        int rand9 = GenerateNewRandom(4424, 4445413);
        int rand10 = GenerateNewRandom(44454144, 518181518);
        int rand11 = GenerateNewRandom(12345, 678910);

        int rand12 = GenerateNewRandom(666, 1074147);




        int perfectSecret = GenerateNewRandom(777, 072789);
        int imperfectMasters = GenerateNewRandom(1114, 1115);


        int rand99 = GenerateNewRandom(99, 75238);

        int rand203 = GenerateNewRandom(2, 8675309);

        int rand777 = GenerateNewRandom(0, 45132);

        int MarsArgo = GenerateNewRandom(4, 1979);
        int Poppy = GenerateNewRandom(336, 915);

        int everyone = GenerateNewRandom(0, 987564321);

        int bill = GenerateNewRandom(4, 7541329);

        int sistersquid = GenerateNewRandom(1, 4768);
        int faux = GenerateNewRandom(0, 3421);

        int streams = GenerateNewRandom(4, 8492);
        int parallel = GenerateNewRandom(3, 8429);

        int j = GenerateNewRandom(4, 4652);

        int william = GenerateNewRandom(1, 0135);

        int order = GenerateNewRandom(1, 777);
        int perfect = GenerateNewRandom(0, 666);

        int specialCode = GenerateNewRandom(7, 31246);

        int HEARTBEAT = GenerateNewRandom(0, 4826);

        int ChristsCode = GenerateNewRandom(2, 123456789);

        int anti = GenerateNewRandom(2, 6);

        int lucky = GenerateNewRandom(777, 31427);

        int adam = GenerateNewRandom(1, 67932);

        int BASAL_GANGLIA = GenerateNewRandom(2, 4861);

        //int down = GenerateNewRandom(9, 75321);

        int latch = GenerateNewRandom(0, 4513);

        int mommy_type_lorena = GenerateNewRandom(4, 123432);

        int fishy = GenerateNewRandom(3, 4651);

        int T = GenerateNewRandom(4, 8531);

        int TheFour = GenerateNewRandom(0, 4682);

        int mirror = GenerateNewRandom(4, 44123);

        int arrow = GenerateNewRandom(19, 77236);

        int thelink = GenerateNewRandom(4, 1324);

        int dim = GenerateNewRandom(7, 14267);

        int reducer = GenerateNewRandom(0, 85426);

        int right = GenerateNewRandom(4, 15896);

        //int redo = GenerateNewRandom(5, 87412);

        int eli = GenerateNewRandom(56, 2212);

        int wall = GenerateNewRandom(14, 369);

        int colin1 = GenerateNewRandom(4, 9874123);

        int colin2 = GenerateNewRandom(3, 1473856);

        int colin3 = GenerateNewRandom(2, 1478963);

        int Dani = GenerateNewRandom(10, 3482);

        int MottAlot = GenerateNewRandom(44, 217);

        //int media_code = GenerateNewRandom(0, 5555555);

        //int l0 =  GenerateNewRandom(0, 703521);

        //int a =  GenerateNewRandom(1,38114957);

        //int s =  GenerateNewRandom(268635, 4012897);



        //int Unresolved = GenerateNewRandom(404, 667);

        //int OnTheLetter = GenerateNewRandom(1, 4310);

        string output = "";
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(mom), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand1), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand2), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand3), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand4), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand5), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand6), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand7), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand8), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand9), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand10), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand11), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand12), 500, 2500, 0, 24) + 65)).ToString();




        output += ((char)((int)ScaleBetweenNumber(RandomFunction(perfectSecret), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(imperfectMasters), 500, 2500, 0, 24) + 65)).ToString();


        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand99), 500, 2500, 0, 24) + 65)).ToString();


        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand203), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(rand777), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(MarsArgo), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(Poppy), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(everyone), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(bill), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(sistersquid), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(faux), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(streams), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(parallel), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(j), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(william), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(order), 500, 2500, 0, 24) + 65)).ToString();
        output += ((char)((int)ScaleBetweenNumber(RandomFunction(perfect), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(specialCode), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(HEARTBEAT), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(ChristsCode), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(anti), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(lucky), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(adam), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(BASAL_GANGLIA), 500, 2500, 0, 24) + 65)).ToString();

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(down), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(latch), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(mommy_type_lorena), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(fishy), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(T), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(TheFour), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(mirror), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(arrow), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(thelink), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(dim), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(reducer), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(right), 500, 2500, 0, 24) + 65)).ToString();

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(redo), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(eli), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(wall), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(colin1), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(colin2), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(colin3), 500, 2500, 0, 24) + 65)).ToString();

        output += ((char)((int)ScaleBetweenNumber(RandomFunction(MottAlot), 500, 2500, 0, 24) + 65)).ToString();


        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(Dani), 500, 2500, 0, 24) + 65)).ToString(); <-- Make sure to add later!

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(media_code), 500, 2500, 0, 24) + 65)).ToString();

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(l0), 500, 2500, 0, 24) + 65)).ToString();

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(a), 500, 2500, 0, 24) + 65)).ToString();

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(s), 500, 2500, 0, 24) + 65)).ToString();



        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(Unresolved), 500, 2500, 0, 24) + 65)).ToString();

        //output += ((char)((int)ScaleBetweenNumber(RandomFunction(OnTheLetter), 500, 2500, 0, 24) + 65)).ToString();


        //Memory System
        char space = " ".ToCharArray()[0];

        string theory = output.Replace((char)66, space).Replace((char)65, space);

        char[] encoded = theory.ToCharArray();
        //S POKOCL     D OC          H
        // U    LOHO M     C   HD          H        
        // P    PHGOCN     D   PC          G
        // U    PKLQCM     C   JD          E                 CC
        string locked = encoded[9].ToString();
        string link = encoded[17].ToString();
        string code = encoded[21].ToString() + encoded[22].ToString();
        string target = encoded[33].ToString();
        string solver = encoded[51].ToString() + encoded[52].ToString() + encoded[53].ToString();

        int spacialLatch = 0;
        for (int i = 0; i < encoded.Length; i++)
        {
            if (encoded[i] == space)
            {
                spacialLatch++;
            }
        }

        spacialLatch -= 40;

        bool lockedLink = false;
        if (locked.ToCharArray()[0] == space)
        {
            lockedLink = true;
        }



        StringBuilder sb = new StringBuilder(theory);
        sb[15] = " ".ToCharArray()[0];
        sb[21] = " ".ToCharArray()[0];
        sb[22] = " ".ToCharArray()[0];
        sb[32] = " ".ToCharArray()[0];
        theory = sb.ToString();


        theory = theory.Replace(" ", "");



        //Console.Write(theory);
        //return theory;



        //string x = theory.Remove(0, 1).TrimStart().TrimEnd();
        //if (x.Contains(" "))
        //{
        //    x = x.Replace(" ", "%");
        //    x = ReplacementSystem(x, " ".ToCharArray()[0], 65, 90);
        //    Console.Write(x);
        //}
        //else
        //{
        //    for (int v = 0; v < x.Length + 5; v++)
        //    {
        //        Console.Write(" ");
        //    }
        //}


        char[] arr = theory.ToCharArray();
        int x = 0;
        bool check = false;
        foreach (Char c in arr)
        {
            if (!check)
            {
                check = true;
                continue;
            }
            x += c - 65;
        }

        arr = link.ToCharArray();
        int l = 0;
        foreach (Char c in arr)
        {
            if (c == space)
            {
                continue;
            }
            l += c - 65;
        }

        arr = code.ToCharArray();
        int co = 0;
        foreach (Char c in arr)
        {
            if (c == space)
            {
                continue;
            }
            co += c - 65;
        }

        return new Payload(arr[0], x, l, co, target, spacialLatch, lockedLink, solver, theory);



        //S POKOCL     D OC          H

        //return new Payload(new char(), 15);



        //if (!baseCode.Contains(theory.Remove(1)))
        //{
        //    baseCode += theory.Remove(1);
        //}
        //Console.Clear();
        //Console.Write(baseCode);
        //OTPSRVMUHGQNIKLJDFCE
        //VNMOURSQTLJHCPKGI DEF
        //UFJMTVRNOWHQPSLKGI D


        //Thread.Sleep(4000);
        //The Secret

        //Two Pieces - Analog-DC System and the CLIPPER Service
    }
}