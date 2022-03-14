using System;
using System.Collections.Generic;
using UnityEngine;


    public interface IGameImplementation
    {
        Action UpdateFunction { get; set; }
        Action DeathFunction { get; set; }
    }

public class ObjectiveGameImplementation : MonoBehaviour
{
    public IGameImplementation implementation;
    public string implementationName = "DefaultImplementation";

    public virtual void Start()
    {
        if (implementation == null) InstantiateImplementation();
    }

    public virtual void Update()
    {
        implementation.UpdateFunction?.Invoke();
    }

    public virtual void OnDestroy()
    {
        implementation.DeathFunction?.Invoke();
    }

    public void InstantiateImplementation(params object[] parameters)
    {
        if (implementationName == "DefaultImplementation") implementationName = (GetType().GetCustomAttributes(typeof(DefaultGameImplementation), true)[0] as DefaultGameImplementation).Name;
        implementation = Activator.CreateInstance(ImplementationSystem.GetImplementation(implementationName), parameters) as IGameImplementation;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DefaultGameImplementation : Attribute
{
    public string Name { get; private set; }

    public DefaultGameImplementation(string name)
    {
        Name = name;

    }
}
