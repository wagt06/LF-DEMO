using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LIP;
using Xamarin.Forms;
using SQLite;

[assembly: Dependency(typeof(SqliteService))]
namespace LIP
{
    class SqliteService : ISQLite
    {
            public SqliteService() { }

  
         SQLite.SQLiteConnection ISQLite.GetConnection()
        {
            try
            {
                var sqliteFilename = "SQLiteEx.db3";
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine(documentsPath, sqliteFilename);
                Console.WriteLine(path);
                if (!File.Exists(path)) File.Create(path);

                //var plat = new sql.Platform.XamarinAndroid.SQLitePlatformAndroid();
                var conn = new SQLiteConnection(path);
                // Return the database connection
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
      
        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            while (bytesRead >= 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }

    }
}

