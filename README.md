# **DESAFIO ETAPA 2**

## 📝 **Instruções**

---

### Configuração Local
Caminho para alteração do servidor (API -> SRV): DSystemAPI/DSystemAPI/Structure/ConnectionContext.cs
Caminho para alteração de conexão (WPF -> API): DSystemAPI/WpfApp/BBL.cs

---

### Comandos e Testes
Comandos para rodar a aplicação e os testes.

Na tela principal encontra-se os botões de adicionar e atualizar as tarefas
Na caixa de texto é possivel pesquisar tarefas por nome, caso um filtro não seja escolhido a pesquisa sera em em todos os status de tarefa
No filtro é possivel selecionar apênas Pendentes, Em progresso ou Concluidas

Na tela de cadastro é possivel inserir todos os dados basicos para criar a tarefa (já com status pendente)
Essa etapa tem as devidas seguranças para efetuar o cadastro corretamente e mensagem de erro para o usuário

Na tela de atualização é possivel alterar uma tarefa por id, basta digitar o id no devido campo e automaticamente os outros campos vão atualizar com os dados
Nessa tela também é possivel deletar a tarefa
Essa etapa tem as devidas seguranças para efetuar uma atualização corretamente e mensagem de erro para o usuário

Uso da API:
PostAsync(context, request): Cadastrar nova tarefa
GetAsync(context): Obter lista de tarefas
PutAsync(context + id): Atualizar tarefa
DeleteAsync(context + id): Deletar tarefa

---

### Banco de dados
Informações sobre o banco de dados usado.
Configurações padrão da minha máquina:
Data Source=DESKTOP-QVNBIP4
Database=master
User ID=sa
Password=102030
