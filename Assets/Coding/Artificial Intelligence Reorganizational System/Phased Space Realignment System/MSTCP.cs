using System;
using UnityEngine;

//Magical Systemless Ticking Clock Protocol
//Written by Enigmia

public class MSTCP : MonoBehaviour
{

    //+17 11 2;
    //+31 71 14;
    //+5  5;
    //+7 8 9;

    //-6 0 1;
    //-5 8 0;
    //-1 5;
    //-5 5 5;

    // --[732]--

    // Start is called before the first frame update
    //+3048962717284943
    //=3048962593828154
    //+3048962593813362
    //=3048962593862091
    //-87923146
    //=8192346
    //=4792813
    //=112027
    //1225
    //+546
    //*580
    //=601 + 601

    //1202 <-- Found at a later, dare I say... time.

    //DD: 4445445 -> 1.8143      

    //4430 4400 5360 1136
    //4430 4400 5360 4310 <---
    //11.25 667 85304 <---
    //1125 1136 85304
    //667


    //And the ratio of a line to a circle is....
    // 1 to 3.14159
    // And the remainder is...
    // 26535


    //26 is our key, our guide. So we look for 26 all the time.
    //The other piece, the link, is that we were just one off....
    // 3.14159 was seen nearly before, in our time machine (digital universal clock, Space+MSTCP)
    // 314760 was the number seen. So we look for a 76 in Pi. 3.14159... the last two!
    // 9 minus 2 (first part of the key) is 7, and 5 is one off (the link) from 6, which also gives us a two-six
    // This gives us a closer-than-possibly-expected connection between our universal clock and Pi, and says that we are on the right
    // track.

    //Our next step is to reduce the digital universal clock's output by exactly pi,
    // to output our actual ratio of time, and produce the clock's infinitely variable output.
    // This gives us our actual tick rate, and the precision required to find the time.

    // 5639412

    public float Exact(float y, float z)
    {
        //float A1 = (71 + 17 + 5 | 8 | 4) * Time.deltaTime * y;
        //float A2 = (11 + 31 - 14 + 7 - 5 | 9) * Time.deltaTime;

        int exact = 17112 + 317114 + 55 + 789;

        int B1 = (int)(exact * y);
        int B2 = (int)(exact / z);

        return (B1 + B2) << (int)z - 4792813;
    }

    // Update is called once per frame
    public float Contactless(float i)
    {
        //float six = Pr(Time.deltaTime * i) * 8192346;
        

        int adjustment = -601 - 580 - 15 - 555;
        float six = Pr(adjustment * i) * 8192346;

        //return six - 1225;
        return six;
        

        //Math.Acos(IWNBA <= TUDY) Math.Sin(PFDTOP + YWM);

        //;
        ////MOT T PORTALK
        //;
        //wALL;
        ////
        //43;
        ////
        //pARTICLE RESULTER;
        ////
        //13;
        //26;
        //10;
        //0111WHTCUP;
        //911;
        //26;
        //06
    }

    public float Pr(float x)
    {
        float u = x / 001001001f * 873129f + Mathf.Sin(1234056.78910f) / Mathf.Cos(6612633) / Mathf.Atan(1135126789016327891) * Mathf.Lerp(1123, 156, Time.deltaTime);
        float z = x / Mathf.Acos(Mathf.Sin(304896259342962091 / 1405927));

        float c = u + z;

        return c;
    }
}