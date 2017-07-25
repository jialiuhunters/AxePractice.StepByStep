using System;
using System.Collections.Generic;
using System.Linq;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods
        Stack<IDisposable> items = new Stack<IDisposable>();

        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */

        public void AddItemsToDispose(object item)
        {
            var disposable = item as IDisposable;
            if (disposable != null)
            {
                items.Push(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (items.Count > 0)
                {
                    var disposable = items.Pop();
                    disposable.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}