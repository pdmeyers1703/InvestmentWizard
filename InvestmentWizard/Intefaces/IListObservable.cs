namespace InvestmentWizard
{
    using System.Collections.Generic;

    /// <summary>
    /// Observer event
    /// </summary>
    /// <param name="list"></param>
    public delegate void ListChangedEventHandler<T>(IList<T> list);

    /// <summary>
    /// Model for a readable 2D list
    /// </summary>
    public interface IListObservable<T>
    {
        /// <summary>
        /// Registers observer to model
        /// </summary>
        /// <param name="listUpdateObserver">observer to montitor when list changes</param>
        void RegisterObserver(ListChangedEventHandler<T> listChangedObserver);

        /// <summary>
        /// Forces model to update
        /// </summary>
        void Update();
    }
}
