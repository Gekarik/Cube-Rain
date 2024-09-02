using System;

public interface IDestroyable
{
    event Action<IDestroyable> Destroyed;
    void Reset();
}