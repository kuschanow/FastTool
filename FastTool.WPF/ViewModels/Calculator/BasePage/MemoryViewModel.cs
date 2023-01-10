#nullable disable

using System.Collections.ObjectModel;

namespace FastTool.WPF.ViewModels.Calculator;

public class MemoryViewModel : ListItemBase
{
    public MemoryViewModel(ObservableCollection<ValueViewModel> values) : base(values) { }
}
