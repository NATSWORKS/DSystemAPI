using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using DSystemAPI;
using DSystemAPI.Model;
using DSystemAPI.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for RegisterFrm.xaml
    /// </summary>
    //Tela de cadastro de tarefas
    public partial class RegisterFrm : Window
    {
        /*
        ========================
        Window1
        ------------------------
        Inicializador.
        ========================
        */
        public RegisterFrm()
        {
            InitializeComponent();
        }

        /*
        ========================
        BtnCreate_Click
        ------------------------
        Envia os inputs para o BBL como TaskViewModel.
        ========================
        */
        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(CheckContent() == true)
            {
                var bbl = new BBL();
                var newTask = new TaskViewModel
                {
                    Title = tbTitle.Text,
                    Description = tbDesc.Text,
                    DateCreated = DateTime.Now,
                    DateConclusion = dpData.SelectedDate,
                    Status = TaskModel.StatusE.Pendente
                };
                await bbl.Register(newTask);
            }
        }

        /*
        ========================
        ChekContent
        ------------------------
        Checagem dos inputs para cadastro.
        ========================
        */
        private bool CheckContent()
        {
            if(tbTitle.Text == string.Empty)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Content = "Erro: O Titulo é obrigatório.";
                return false;
            }
            if (dpData.SelectedDate != null)
            {
                if (dpData.SelectedDate < DateTime.Now)
                {
                    txtError.Visibility = Visibility.Visible;
                    txtError.Content = "Erro: A data de conclusão é menor que a data atual.";
                    return false;
                }
            }
            txtError.Visibility = Visibility.Hidden;
            return true;
        }
    }
}
