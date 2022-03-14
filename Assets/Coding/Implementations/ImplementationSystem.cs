using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ImplementationSystem : MonoBehaviour
{
    private static Dictionary<string, Type> implementations = new Dictionary<string, Type>();

    public static ImplementationSystem Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        LoadImplementations();
    }

    public static void LoadImplementations()
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                object[] attrs = type.GetCustomAttributes(typeof(GameImplementation), true);
                if (attrs.Length > 0)
                {
                    foreach (Attribute attr in attrs)
                    {
                        if (attr as GameImplementation != null)
                        {
                            implementations[(attr as GameImplementation).Name] = type;
                            continue;
                        }
                    }
                }
            }
        }
    }

    public static Type GetImplementation(string name)
    {
        if (implementations.ContainsKey(name)) return implementations[name];
        throw new KeyNotFoundException("GameImplementation " + name + " was not found by Implementations!");
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class GameImplementation : Attribute
{
    public string Name { get; private set; }

    public GameImplementation(string name)
    {
        Name = name;

    }
}
