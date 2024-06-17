using ICommand_Counter.ViewModels;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICommand_Counter.Views
{
    /// <summary>
    /// Interaction logic for FinancialManagementForm.xaml
    /// </summary>
    public partial class FinancialManagementForm : Window
    {
        public FinancialManagementForm()
        {
            InitializeComponent();
            if (decimal.TryParse(Interaction.InputBox("Введите начальный баланс : ", "Баланс"), out decimal balance)) {
                DataContext = new FinancesViewModel(balance);
            }
            else
            {
                DataContext = new FinancesViewModel(0);
            }
        }
    }
}
