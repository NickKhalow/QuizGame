#nullable enable
using System;
using System.Collections.Generic;


namespace Misc
{
    public class ObjectPool<T>
    {
        private readonly Stack<T> objects;
        private readonly Func<T> constructor;
        private readonly Action<T> postPush;
        private readonly Action<T> prePop;


        public ObjectPool(
            int prewarmCount,
            Func<T> constructor,
            Action<T> postPush,
            Action<T> prePop)
        {
            this.objects = new Stack<T>(prewarmCount);
            this.constructor = constructor;
            this.postPush = postPush;
            this.prePop = prePop;
        }


        public T Pop()
        {
            var obj = objects.TryPop(out var result) ? result : constructor();
            prePop(obj);
            return obj;
        }


        public void Push(T value)
        {
            objects.Push(value);
            postPush(value);
        }
    }
}