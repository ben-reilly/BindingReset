using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;

namespace BindingReset.ViewModels
{
    public enum OptionType { NONE, A, B }

    public class RowViewModel : ReactiveObject
    {
        public List<OptionType> LocalOptions { get; } = new() { OptionType.A, OptionType.B };

        private OptionType selectedOption = OptionType.NONE;
        public OptionType SelectedOption
        {
            get => selectedOption;
            set => this.RaiseAndSetIfChanged(ref selectedOption, value);
        }
    }

    public class HostViewModel : ViewModelBase
    {
        public List<RowViewModel> Rows { get; }

        public HostViewModel(List<RowViewModel> rows)
        {
            Rows = rows;
        }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        public List<OptionType> ParentOptions { get; } = new() { OptionType.A, OptionType.B };

        private ViewModelBase? currentViewModel;
        public ViewModelBase? CurrentViewModel
        {
            get => currentViewModel;
            set => this.RaiseAndSetIfChanged(ref currentViewModel, value);
        }

        public ReactiveCommand<Unit, Unit> ToggleViewModel { get; }

        public List<RowViewModel> RowData { get; }

        public MainWindowViewModel()
        {
            RowData = new List<RowViewModel>()
            {
                new RowViewModel()
            };

            var viewModel = new HostViewModel(RowData);

            ToggleViewModel = ReactiveCommand.Create(() =>
            {
                CurrentViewModel = CurrentViewModel == viewModel ? null : viewModel;
            });

            CurrentViewModel = viewModel;
        }
    }
}
