using Microsoft.Data.Sqlite;

namespace Skovorodki;

public class SkovorodkiDatabase : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly SqliteCommand _command;

    public SkovorodkiDatabase(string dbName)
    {
        _connection = new SqliteConnection($"Data source = {dbName}");
        _command = _connection.CreateCommand();

        // Открываем соединение с базой
        _connection.Open();

        // Инициализировать все таблицы в базе
        CreateIfNotExists();
    }
    
    public void CreateIfNotExists()
    {

        _command.CommandText =
            @"
                 CREATE TABLE IF NOT EXISTS Produsers(
                     id     INTEGER PRIMARY KEY AUTOINCREMENT,
                     name   TEXT NOT NULL
                 );
                 CREATE TABLE IF NOT EXISTS Skovorodki(
                     id             INTEGER PRIMARY KEY AUTOINCREMENT,
                     produser_id    INTEGER,
                     model          TEXT NOT NULL,
                     price          TEXT NOT NULL,
                     FOREIGN KEY(produser_id) REFERENCES Produsers(id)
                 );   
     ";
        _command.ExecuteNonQuery();
    }
    
    public void AddProdusers(List<string> Produsers)
    {
        foreach (var pr in Produsers)
        {
            _command.CommandText =
                        $"INSERT INTO Produsers(name) " +
                        $"VALUES ('{pr}')";
            _command.ExecuteNonQuery();
        }
    }
    
    public void AddSkovorodki(List<Skovorodki> skovorodki,List<string> produsers )
    {
        foreach (var sk in skovorodki)
        {
            
            
            _command.CommandText =
                $"INSERT INTO Skovorodki(produser_id,model,price) " +
                $"VALUES ('{FindIdentity(sk,produsers)}','{sk.Name}','{sk.Price}')";
            _command.ExecuteNonQuery();
        }
    }

    public int FindIdentity(Skovorodki sk, List<string> produsers)
    {
        int result = 0;

        for (int i = 0; i < produsers.Count; i++)
        {
            if (sk.Brand == produsers[i])
            {
                result = i + 1;
                break;
            } ;
           
        }

        return result;
    }
    public void Dispose()
    {
        _connection.Dispose();
        _command.Dispose();
    }
}