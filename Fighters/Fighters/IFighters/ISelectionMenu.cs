public interface ISelectionMenu
{
    T SelectFromCollection<T>( IEnumerable<T> collection, string prompt );
}

