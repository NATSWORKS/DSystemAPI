using Newtonsoft.Json;
using System.Data;
using System.Windows;
using DSystemAPI.ViewModel;
using System.Threading.Tasks;
using DSystemAPI.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Buttons
        /*
        ========================
        BtnAddTarefa_Click
        ------------------------
        Abre a janela para criar tarefa.
        ========================
        */
        private void btnAddTarefa_Click(object sender, RoutedEventArgs e)
        {
            RegisterFrm window1 = new RegisterFrm();
            window1.Show();
        }

        /*
        ========================
        btnSearch_Click
        ------------------------
        Chama as funçoes para gerar a tablea de acordo com os parametros dados.
        ========================
        */
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var bbl = new BBL();
            dtTarefas.ItemsSource = await bbl.GetDataTable(ddKind.SelectedIndex, tbSearch.Text);
            TableFormat();
        }

        private void btnAddTarefa_Copy_Click(object sender, RoutedEventArgs e)
        {
            UpdateFrm window2 = new UpdateFrm();
            window2.Show();
        }
        #endregion

        /*
        ========================
        TableFormat
        ------------------------
        Formata os nomes das colunas.
        ========================
        */
        private void TableFormat()
        {
            dtTarefas.Columns[0].Header = "Id";
            dtTarefas.Columns[1].Header = "Título";
            dtTarefas.Columns[2].Header = "Descrição";
            dtTarefas.Columns[3].Header = "Data de criação";
            dtTarefas.Columns[4].Header = "Data de conclusão";
            dtTarefas.Columns[5].Header = "Status";
        }
    }

    //Classe TaskItem
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateConclusion { get; set; }
        public string Status { get; set; }
    }
}