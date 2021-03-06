﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkFinder.Common.Extensions
{
    public static class TaskExtensions
    {
        // Adapted from https://stackoverflow.com/a/22078975
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
            if (completedTask != task) throw new TimeoutException();
            timeoutCancellationTokenSource.Cancel();
            return await task; // Very important in order to propagate exceptions
        }
    }
}
