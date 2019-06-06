using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_BD.Repository
{
    class Repository: IRepository
    {
        static string domain = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        static string path = domain + "\\Instruments.mdf";       

        string connStr = @"Data Source = (localdb)\MSSQLLocalDB; AttachDbFilename=" + path + "; Integrated Security = True; database = Instruments ";

        public IEnumerable<String> GetFullTree()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                
                conn.Open();
                
                string sql = "SELECT name, lev,id FROM tree ORDER BY left_key";
                
                SqlCommand command = new SqlCommand(sql, conn);
                
                SqlDataReader reader = command.ExecuteReader();

                IList<String> list = new List<String>();
                
                while (reader.Read())
                {
                    list.Add(new String('-', (int)reader[1]) + reader[0].ToString() + "\t | " + reader[2].ToString());
                }
                reader.Close();
                conn.Close();
                return list;
            }
            catch { return new List<String>(); }
        }

        public IEnumerable<String> GetDaughter(int numb)
        {
            int right = 0, left = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                
                conn.Open();
                string sql = $"SELECT left_key,right_key FROM tree  where id='{numb}'";
                SqlCommand command = new SqlCommand(sql, conn);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    left = (int)reader[0];
                    right = (int)reader[1];
                }
                reader.Close(); 

                string sqlDaughter = $"SELECT id, name, lev FROM tree  WHERE left_key >= {left} AND right_key <= {right} ORDER BY left_key";
                SqlCommand commandDaughter = new SqlCommand(sqlDaughter, conn);
                IList<String> list = new List<String>();
                SqlDataReader readerDaughter = commandDaughter.ExecuteReader();
                while (readerDaughter.Read())
                {
                    list.Add(new String('-', (int)readerDaughter[2]) + readerDaughter[1].ToString() + "\t | " + readerDaughter[0].ToString());
                }
                reader.Close();
                conn.Close();
                return list;
            }
            catch { return new List<String>(); }
        }

        public IEnumerable<String> GetParent(int numb)
        {
            int right = 0, left = 0;
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                
                conn.Open();
                string sql = $"SELECT left_key,right_key FROM tree where id='{numb}'";
                SqlCommand command = new SqlCommand(sql, conn);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    left = (int)reader[0];
                    right = (int)reader[1];
                }
                reader.Close();
                IList<String> list = new List<String>();
                string sqlDaughter = $"SELECT id, name, lev FROM tree WHERE left_key <= {left} AND right_key >= {right} ORDER BY left_key";
                SqlCommand commandDaughter = new SqlCommand(sqlDaughter, conn);
                
                SqlDataReader readerDaughter = commandDaughter.ExecuteReader();
                while (readerDaughter.Read())
                {
                    list.Add(new String('-', (int)readerDaughter[2]) + readerDaughter[1].ToString() + "\t | " + readerDaughter[0].ToString());
                }
                reader.Close();
                conn.Close();
                return list;
            }
            catch { return new List<String>(); }
        }

        public IEnumerable<String> GetBranchTwoElements(int a, int b)
        {
            int right1 = 0, left1 = 0;
            int right2 = 0, left2 = 0;

            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                
                conn.Open();
                string sql = $"SELECT left_key,right_key FROM tree where id='{a}'";
                SqlCommand command = new SqlCommand(sql, conn);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    left1 = (int)reader[0];
                    right1 = (int)reader[1];
                }
                reader.Close(); 

                SqlConnection conn1 = new SqlConnection(connStr);
                
                conn1.Open();
                string sql1 = $"SELECT left_key,right_key FROM tree where id='{b}'";
                SqlCommand command1 = new SqlCommand(sql1, conn1);
                
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    left2 = (int)reader1[0];
                    right2 = (int)reader1[1];
                }
                reader1.Close();
                IList<String> list = new List<String>();

                SqlConnection conn2 = new SqlConnection(connStr);
                conn2.Open();
                string sqlDaughter = $"SELECT id, name, lev FROM tree WHERE left_key <= {left1} " +
                    $"and right_key >= {right1} ORDER BY left_key";
                SqlCommand commandDaughter = new SqlCommand(sqlDaughter, conn2);
                
                SqlDataReader readerDaughter = commandDaughter.ExecuteReader();
                while (readerDaughter.Read())
                {
                    //list.Add(new String('-', (int)readerDaughter[2]) + readerDaughter[1].ToString() + "\t | " + readerDaughter[0].ToString());
                }
                reader.Close();

                string sqlDaughter1 = $"SELECT id, name, lev FROM tree WHERE left_key <= {left2} " +
                    $"and right_key >= {right2} ORDER BY left_key";
                SqlConnection conn3 = new SqlConnection(connStr);
                conn3.Open();
                SqlCommand commandDaughter1 = new SqlCommand(sqlDaughter1, conn3);
                
                SqlDataReader readerDaughter1 = commandDaughter1.ExecuteReader();
                int q = 0;
                while (readerDaughter1.Read())
                {
                   
                    if (q == 0)
                    { q++; }
                    else
                        list.Add(new String('-', (int)readerDaughter1[2]) + readerDaughter1[1].ToString() + "\t | " + readerDaughter1[0].ToString());

                }
                conn.Close();
                return list;
            }
            catch { return new List<String>(); }
        }

    }

}