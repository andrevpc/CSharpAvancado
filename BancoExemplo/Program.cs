using System;
using System.Data;
using System.Data.SqlClient;

SqlConnectionStringBuilder stringConnectionBuilder = new SqlConnectionStringBuilder();
stringConnectionBuilder.DataSource = @"CT-C-0013J\SQLEXPRESS01"; // Nome do servidor
stringConnectionBuilder.InitialCatalog = "example"; // Nome do banco
stringConnectionBuilder.IntegratedSecurity = true;
string stringConnection = stringConnectionBuilder.ConnectionString;

SqlConnection conn = new SqlConnection(stringConnection);
conn.Open();

//SqlCommand comm = new SqlCommand("insert Cliente values ('Pamella', '123', CONVERT(DATETIME, '27/03/2023'));");
//comm.Connection = conn;
//comm.ExecuteNonQuery();

//conn.Close();

string nome = Console.ReadLine();
string senha = Console.ReadLine();

SqlCommand comm = new SqlCommand($"select * from Cliente where Nome = '{nome}' and --Senha = '{senha}'");
comm.Connection = conn;
var reader = comm.ExecuteReader();

DataTable dt = new DataTable();
dt.Load(reader);

if (dt.Rows.Count > 0)
    Console.WriteLine($"Usuário {dt.Rows[0].ItemArray[0]} Logado");
else
    Console.WriteLine("Conta inexistente");

conn.Close();