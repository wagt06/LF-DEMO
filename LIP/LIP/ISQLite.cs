using System;
using System.Collections.Generic;
using System.Text;


namespace LIP
{
    interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
