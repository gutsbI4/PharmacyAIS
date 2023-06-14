using ReactiveUI;

namespace PharmacyAIS.Models;

public class FilterModel<T> : ReactiveObject
{
    private bool _isChecked;

    public bool isChecked
    {
        get => _isChecked;
        set => this.RaiseAndSetIfChanged(ref _isChecked, value);
    }
    public T Value { get; set; }
}