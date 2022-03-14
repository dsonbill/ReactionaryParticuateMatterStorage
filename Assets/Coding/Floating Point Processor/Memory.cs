using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;

namespace FloatingPointProcessor
{
    public class Memory : MonoBehaviour
    {
        public Stack Stack;
        public Bucket Bucket;

        public void InitializeStack()
        {
            Stack = new Stack();
        }

        public void InitializeBucket()
        {
            Bucket = new Bucket();
        }
    }


    public class Stack
    {
        Stack<double> Data = new Stack<double>();

        public void Push(double x)
        {
            Data.Push(x);
        }

        public double Pop()
        {
            return Data.Pop();
        }

        public int Length()
        {
            return Data.Count;
        }
    }

    public class Bucket
    {
        KeyedList<double> Data = new KeyedList<double>();

        public void Write(int key, double x)
        {
            Data.Write(key, x);
        }

        public double Read(int key)
        {
            return Data[key];
        }

        public void Delete(int key)
        {
            Data.Remove(key);
        }

        public void Compress()
        {
            Data.Compress();
        }
    }

}