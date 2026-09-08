using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;

namespace CSharpTasks.Tasks.Task2;

public static class CountServer
{
    private static int _count;

    private static readonly ReaderWriterLockSlim Lock =
        new(LockRecursionPolicy.NoRecursion);

    public static int GetCount()
    {
        Lock.EnterReadLock();

        try
        {
            return _count;
        }
        finally
        {
            Lock.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        Lock.EnterWriteLock();

        try
        {
            _count += value;
        }
        finally
        {
            Lock.ExitWriteLock();
        }
    }

    internal static void Reset()
    {
        Lock.EnterWriteLock();

        try
        {
            _count = 0;
        }
        finally
        {
            Lock.ExitWriteLock();
        }
    }
}