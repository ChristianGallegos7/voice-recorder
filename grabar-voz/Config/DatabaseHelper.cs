using System.Data.SQLite;

namespace grabar_voz.Config
{
    class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=grabar_voz.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Clientes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Identificacion TEXT NOT NULL,
                        Nombre TEXT NOT NULL,
                        Observacion TEXT,
                        Fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                    );";
                var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void SaveClient(string identificacion, string nombre, string observacion)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO Clientes (Identificacion, Nombre, Observacion)
                    VALUES (@Identificacion, @Nombre, @Observacion);";
                var command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Identificacion", identificacion);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Observacion", observacion);
                command.ExecuteNonQuery();
            }
        }
    }
}
