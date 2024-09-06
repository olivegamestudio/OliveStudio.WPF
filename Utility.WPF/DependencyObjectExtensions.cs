using System.Windows;
using System.Windows.Media;

namespace Utility;

public static class DependencyObjectExtensions
{
    /// <summary>
    /// Finds the first parent of a specified type for a given dependency object.
    /// </summary>
    /// <typeparam name="TDependencyType">The type of the parent to find.</typeparam>
    /// <param name="dependencyObject">The dependency object to start the search from.</param>
    /// <returns>The first parent of the specified type, or null if no such parent exists.</returns>
    public static TDependencyType? FindParentOfType<TDependencyType>(this DependencyObject dependencyObject)
    {
        if (dependencyObject is null)
        {
            return default;
        }

        if (dependencyObject is TDependencyType type)
        {
            return type;
        }

        return FindParentOfType<TDependencyType>(VisualTreeHelper.GetParent(dependencyObject));
    }

    /// <summary>
    /// Finds all visual children of a specified type within a given dependency object.
    /// </summary>
    /// <typeparam name="TDependencyType">The type of visual children to find.</typeparam>
    /// <param name="dependencyObject">The parent dependency object to search within.</param>
    /// <returns>A list of visual children of the specified type.</returns>
    public static List<TDependencyType> FindVisualChildrenOfType<TDependencyType>(this DependencyObject dependencyObject) where TDependencyType : DependencyObject
    {
        List<TDependencyType> children = new();

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
            if (child is TDependencyType typedChild)
            {
                children.Add(typedChild);
            }

            List<TDependencyType> childItems = FindVisualChildrenOfType<TDependencyType>(child);
            if (childItems.Count > 0)
            {
                children.AddRange(childItems);
            }
        }

        return children;
    }
}
