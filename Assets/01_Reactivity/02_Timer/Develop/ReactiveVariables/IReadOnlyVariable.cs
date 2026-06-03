using System;

public interface IReadOnlyVariable<T>
{
    event Action<T, T> Changed;
    T Value { get; }
}