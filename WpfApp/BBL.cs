using DSystemAPI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfApp;

//Business Logic Layer
public class BBL
{
    private readonly string _httpClient = "https://localhost:7199/api/v1/task";// URL da API ASP.NET

    #region CRUD
    /*
    ========================
    Register
    ------------------------
    Acessa a API e realiza o cadastro.
    ========================
    */
    public async Task Register(TaskViewModel task)
    {
        try
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var response = await client.PostAsync(_httpClient, ToRequest(task));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Tarefa criada com sucesso!\n{task.Title}");
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

    /*
    ========================
    Update
    ------------------------
    Acessa a API e realiza o cadastro.
    ========================
    */
    public async Task Update(int id, TaskViewModel task)
    {
        try
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var response = await client.PutAsync(_httpClient + "/" + id.ToString(), ToRequest(task));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Tarefa atualizada com sucesso!\n{task.Title}");
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar a tarefa. {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro: {ex.Message}");
        }
    }

    /*
    ========================
    Delete
    ------------------------
    Acessa a API e deleta o cadastro.
    ========================
    */
    public async Task Delete(int id)
    {
        try
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var response = await client.DeleteAsync(_httpClient + "/" + id.ToString());
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Tarefa atualizada com sucesso!\n{id}");
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar a tarefa. {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro: {ex.Message}");
        }
    }

    /*
    ========================
    GetData
    ------------------------
    Acessa a API para buscar as tarefas.
    ========================
    */
    public async Task<string> GetData()
    {
        try
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var response = await client.GetAsync(_httpClient);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return json;
                }
                else
                {
                    MessageBox.Show("Erro");
                    return "";
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro: {ex.Message}");
            return "";
        }
    }
#endregion

    /*
    ========================
    GetDataTable
    ------------------------
    Acessa a API para buscar as tarefas e transforma em tabela.
    ========================
    */
    public async Task<IEnumerable<TaskItem>> GetDataTable(int kind, string searchText)
    {
        string json = await GetData();
        DataTable newTable = CreateDataTable(json, kind, searchText);
        List<TaskItem> taskItems = ConvertDataTableToList(newTable);
        return taskItems;
    }

    /*
    ========================
    GetDataRow
    ------------------------
    Acessa a API para buscar as tarefas e transforma em linha.
    ========================
    */
    public async Task<DataRow> GetDataRow(int id)
    {
        string json = await GetData();
        dynamic tasks = JsonConvert.DeserializeObject(json);
        DataTable newTable = new DataTable();
        newTable.Columns.Add("Id", typeof(int));
        newTable.Columns.Add("Título", typeof(string));
        newTable.Columns.Add("Descrição", typeof(string));
        newTable.Columns.Add("Data de criação", typeof(DateTime));
        newTable.Columns.Add("Data de conclusão", typeof(DateTime)); // Nullable
        newTable.Columns.Add("Status", typeof(int));

        foreach (var task in tasks)
        {
            if (task.id == id)
            {
                DataRow newRow = newTable.NewRow();
                newRow["Id"] = (int)task.id;
                newRow["Título"] = (string)task.title;
                newRow["Descrição"] = (string)task.description;
                newRow["Data de criação"] = (DateTime)task.dateCreated;
                newRow["Data de conclusão"] = task.dateConclusion == null ? DBNull.Value : (DateTime?)task.dateConclusion;
                newRow["Status"] = task.status;
                return newRow;
            }
        }
        return null;
    }

    /*
    ========================
    ToRequest
    ------------------------
    Prepara o TaskViewModel para solicitar o cadastro object-json.
    ========================
    */
    private static StringContent ToRequest(TaskViewModel task)
    {
        var json = JsonConvert.SerializeObject(task);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        return data;
    }

    #region Table
    /*
    ========================
    CreateDataTable
    ------------------------
    Gera a tabela a ser mostrada.
    ========================
    */
    private DataTable CreateDataTable(string json, int kind, string searchText)
    {
        dynamic tasks = JsonConvert.DeserializeObject(json);
        DataTable newTable = new DataTable();
        newTable.Columns.Add("Id", typeof(int));
        newTable.Columns.Add("Título", typeof(string));
        newTable.Columns.Add("Descrição", typeof(string));
        newTable.Columns.Add("Data de criação", typeof(DateTime));
        newTable.Columns.Add("Data de conclusão", typeof(DateTime)); // Nullable
        newTable.Columns.Add("Status", typeof(string));

        foreach (var task in tasks)
        {
            if (kind <= 0)
            {
                if (searchText == string.Empty)
                {
                    DataRow newRow = newTable.NewRow();
                    newRow["Id"] = (int)task.id;
                    newRow["Título"] = (string)task.title;
                    newRow["Descrição"] = (string)task.description;
                    newRow["Data de criação"] = (DateTime)task.dateCreated;
                    newRow["Data de conclusão"] = task.dateConclusion == null ? DBNull.Value : (DateTime?)task.dateConclusion;
                    newRow["Status"] = (DSystemAPI.Model.TaskModel.StatusE)task.status;
                    newTable.Rows.Add(newRow);
                }
                else
                {
                    if (task.title == searchText)
                    {
                        DataRow newRow = newTable.NewRow();
                        newRow["Id"] = (int)task.id;
                        newRow["Título"] = (string)task.title;
                        newRow["Descrição"] = (string)task.description;
                        newRow["Data de criação"] = (DateTime)task.dateCreated;
                        newRow["Data de conclusão"] = task.dateConclusion == null ? DBNull.Value : (DateTime?)task.dateConclusion;
                        newRow["Status"] = (DSystemAPI.Model.TaskModel.StatusE)task.status;
                        newTable.Rows.Add(newRow);
                    }
                }
            }
            else
            {
                if (task.status == kind - 1)
                {
                    if (searchText == string.Empty)
                    {
                        DataRow newRow = newTable.NewRow();
                        newRow["Id"] = (int)task.id;
                        newRow["Título"] = (string)task.title;
                        newRow["Descrição"] = (string)task.description;
                        newRow["Data de criação"] = (DateTime)task.dateCreated;
                        newRow["Data de conclusão"] = task.dateConclusion == null ? DBNull.Value : (DateTime?)task.dateConclusion;
                        newRow["Status"] = (DSystemAPI.Model.TaskModel.StatusE)task.status;
                        newTable.Rows.Add(newRow);
                    }
                    else
                    {
                        if (task.title == searchText)
                        {
                            DataRow newRow = newTable.NewRow();
                            newRow["Id"] = (int)task.id;
                            newRow["Título"] = (string)task.title;
                            newRow["Descrição"] = (string)task.description;
                            newRow["Data de criação"] = (DateTime)task.dateCreated;
                            newRow["Data de conclusão"] = task.dateConclusion == null ? DBNull.Value : (DateTime?)task.dateConclusion;
                            newRow["Status"] = (DSystemAPI.Model.TaskModel.StatusE)task.status;
                            newTable.Rows.Add(newRow);
                        }
                    }

                }
            }
        }

        return newTable;
    }

    /*
    ========================
    ConvertDataTableToList
    ------------------------
    Apenas converte DataTable para Lista.
    ========================
    */
    private List<TaskItem> ConvertDataTableToList(DataTable dataTable)
    {
        return dataTable.AsEnumerable().Select(row => new TaskItem
        {
            Id = row.Field<int>("Id"),
            Title = row.Field<string>("Título"),
            Description = row.Field<string>("Descrição"),
            DateCreated = row.Field<DateTime>("Data de criação"),
            DateConclusion = row.Field<DateTime?>("Data de conclusão"),
            Status = row.Field<string>("Status")
        }).ToList();
    }
    #endregion
}
