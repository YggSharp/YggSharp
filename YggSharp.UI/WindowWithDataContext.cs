using Avalonia.Controls;

namespace YggSharp.UI;

public class WindowWithDataContext<T> : Window
{
    protected T CastedDataContext => (T) DataContext!;
} 