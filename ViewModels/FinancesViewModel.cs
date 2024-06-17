using ICommand_Counter.Commands;
using ICommand_Counter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICommand_Counter.ViewModels
{
    public class FinancesViewModel : ViewModelBase
    {
        private ObservableCollection<Finance> _finances;
        private decimal _currentBalance;
        private Finance _selectedFinance;
        private string _buttonText;

        private string _name;
        private decimal _sum;
        private DateTime _date;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Sum
        {
            get => _sum;
            set
            {
                if (_sum != value)
                {
                    _sum = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal CurrentBalance
        {
            get => _currentBalance;
            set
            {
                if (_currentBalance != value)
                {
                    _currentBalance = value;
                    OnPropertyChanged();
                }
            }
        }
        public Finance SelectedFinance
        {
            get => _selectedFinance;
            set
            {
                if (_selectedFinance != value)
                {
                    _selectedFinance = value;
                    if (_selectedFinance != null)
                    {
                        Name = _selectedFinance.Name;
                        Sum = _selectedFinance.Sum;
                        Date = _selectedFinance.Date;
                        ButtonText = "Обновить";
                    }
                    else
                    {
                        ButtonText = "Внести";
                    }
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsFinanceSelected));
                }
            }
        }

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged();
                }
            }
        }

        public FinancesViewModel(decimal balance)
        {
            _finances = new ObservableCollection<Finance>();

            CurrentBalance = balance;
            ButtonText = "Внести";

            AddOrUpdateFinance = new RelayCommand(_ =>
            {
                if (SelectedFinance != null)
                {

                    SelectedFinance.Name = Name;
                    SelectedFinance.Sum = Sum;
                    SelectedFinance.Date = Date;
                }
                else
                {
                    var finance = new Finance { Name = Name, Sum = Sum, Date = Date };
                    _finances.Add(finance);
                    CurrentBalance -= finance.Sum;
                }

                ClearFields();
            });

            DeleteFinance = new RelayCommand(_ =>
            {
                if (SelectedFinance != null)
                {
                    CurrentBalance += SelectedFinance.Sum;
                    _finances.Remove(SelectedFinance);
                    ClearFields();
                }
            }, _ => SelectedFinance != null);
        }

        public ObservableCollection<Finance> Finances
        {
            get => _finances;
            set
            {
                if (_finances != value)
                {
                    _finances = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddOrUpdateFinance { get; }
        public ICommand DeleteFinance { get; }

        public bool IsFinanceSelected => SelectedFinance != null;

        private void ClearFields()
        {
            Name = string.Empty;
            Sum = 0;
            Date = DateTime.Now;
            SelectedFinance = null;
        }
    }
}
