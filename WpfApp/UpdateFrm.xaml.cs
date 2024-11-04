using DSystemAPI.Model;
using DSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for UpdateFrm.xaml
    /// </summary>
    public partial class UpdateFrm : Window
    {
        bool finderOk = false;//Segurança para atualizar só cadastros existentes
        DataRow newRow;
        public UpdateFrm()
        {
            InitializeComponent();
        }

        #region Buttons
        /*
        ========================
        textChangedEventHandler
        ------------------------
        Atualiza compos.
        ========================
        */
        private async void textChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            var bbl = new BBL();
            newRow = await bbl.GetDataRow(Int32.Parse(tbId.Text));
            if(newRow.Field<String>("Título") != string.Empty || newRow.Field<String>("Título") != null)
            {
                finderOk = true;
                tbTitle.Text = newRow.Field<String>("Título");
                tbDesc.Text = newRow.Field<String>("Descrição");
                dpData.DisplayDate = newRow.Field<DateTime>("Data de conclusão") == null ? dpData.DisplayDate : newRow.Field<DateTime>("Data de conclusão");
                ddStatus.SelectedIndex = newRow.Field<int>("Status");
            }
            else
            {
                finderOk = false;
            }
        }

        /*
        ========================
        btnUpdate_Click
        ------------------------
        Atualiza cadastro.
        ========================
        */
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckContent() == true && finderOk == true)
            {
                var bbl = new BBL();
                var newTask = new TaskViewModel
                {
                    Title = tbTitle.Text,
                    Description = tbDesc.Text,
                    DateCreated = newRow.Field<DateTime>("Data de criação"),
                    DateConclusion = dpData.DisplayDate,
                    Status = (TaskModel.StatusE)ddStatus.SelectedIndex
                };
                await bbl.Update(Int32.Parse(tbId.Text), newTask);
            }
        }

        /*
        ========================
        btnDelete_Click
        ------------------------
        Deleta cadastro.
        ========================
        */
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (finderOk == true)
            {
                var bbl = new BBL();
                await bbl.Delete(Int32.Parse(tbId.Text));
            }
        }
        #endregion

        /*
        ========================
        ChekContent
        ------------------------
        Checagem dos inputs para cadastro.
        ========================
        */
        private bool CheckContent()
        {
            if (tbTitle.Text == string.Empty)
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
