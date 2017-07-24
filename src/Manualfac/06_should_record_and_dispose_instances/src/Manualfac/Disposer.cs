using System;
using System.Collections.Generic;
using System.Linq;

namespace Manualfac
{
    class Disposer : Disposable
    {
        #region Please implements the following methods
        List<Object> items = new List<object>();

        /*
         * The disposer is used for disposing all disposable items added when it is disposed.
         */

        public void AddItemsToDispose(object item)
        {
            items.Add(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var disposableItems = this.items.Select(i => i as IDisposable).Where(i => i != null).ToList();
                foreach (IDisposable disposableItem in disposableItems)
                {
                    disposableItem.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}