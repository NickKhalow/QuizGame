namespace View
{
    public interface IView<T>
    {
        void Render(T value);
    }
}