using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using DSystemAPI;
using DSystemAPI.Model;
using DSystemAPI.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly HttpClient _httpClient;

        /*
        ========================
        Window1
        ------------------------
        Inicializador.
        ========================
        */
        public Window1()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7199/") }; // URL da API ASP.NET
        }

        /*
        ========================
        BtnCreate_Click
        ------------------------
        Cria cadastro no banco de dados.
        ========================
        */
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Cadastrar();
        }

        private async Task Cadastrar()
        {
            try
            {
                var newTask = new TaskViewModel
                {
                    Title = tbTitle.Text,
                    Description = tbDesc.Text,
                    DateCreated = DateTime.Now,
                    DateConclusion = dpData.SelectedDate,
                    Status = TaskModel.StatusE.Pendente
                };

                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    var response = await client.PostAsync("https://localhost:7199/api/v1/task", ToRequest(newTask));
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Tarefa criada com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show($"Erro ao criar a tarefa. {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private static StringContent ToRequest(TaskViewModel task)
        {
            var json = JsonSerializer.Serialize(task);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }
    }
}
