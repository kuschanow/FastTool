using FastTool.WPF.Windows.MainWindow.Calculator;
using FastTool.WPF.Windows.MainWindow.Settings;
using FastTool.WPF.Windows.MainWindow.Time;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Calculator
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, Page> pages = new()
        {
            { "basePage", new CalcBasePage() },
            { "transPage", new CalcTransformPage() },
            { "graphPage", new CalcGraphPage() },
            { "formulsPage", new CalcFormulsPage() },
            { "boardPage", new CalcBoardPage() },
        };

        private Page currPage;

        public Page CurrPage
        {
            get => currPage;
            set
            {
                currPage = value;
                OnPropertyChanged();
            }
        }

        public CalculatorViewModel() => currPage = pages["basePage"];

        public ICommand ChangePage => new RelayCommand(ChangePageExecute);

        private void ChangePageExecute(object obj) => CurrPage = pages[(string)obj];

        #region PropertyChanged
#nullable disable
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
#nullable enable
        #endregion
    }
}
