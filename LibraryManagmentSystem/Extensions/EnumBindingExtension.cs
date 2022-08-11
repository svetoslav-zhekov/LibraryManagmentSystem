namespace LibraryManagmentSystem.Extensions;

/// <summary>
/// EnumBindingExtension, helps with binding Enums to ItemsSource of Pickers etc.
/// </summary>

[ContentProperty(nameof(EnumType))]
public class EnumBindingExtension : IMarkupExtension
{
    public Type EnumType { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        if (EnumType == null || !EnumType.IsEnum)
        {
            throw new Exception("Provided type is not of type enum!");
        }
        else
        {
            return Enum.GetValues(EnumType);
        }
    }
}
