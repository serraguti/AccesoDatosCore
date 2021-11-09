using System;
using System.Data.SqlClient;

namespace AccesoDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void LeerRegistros()
        {
            String cadenaconexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            //DECLARAMOS LOS OBJETOS A UTILIZAR
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            SqlDataReader reader;
            //CREAMOS LA CONSULTA
            String consulta = "SELECT * FROM DEPT";
            //INDICAMOS AL COMANDO TRES PROPIEDADES
            //CONEXION A UTILIZAR
            com.Connection = cn;
            //LA CONSULTA A REALIZAR
            com.CommandText = consulta;
            //TIPO DE CONSULTA
            com.CommandType = System.Data.CommandType.Text;
            //ENTRAR SALIR, ABRIMOS CONEXION
            cn.Open();
            //EJECUTAMOS LA CONSULTA, AL SER UNA CONSULTA SELECT
            //UTILIZAMOS EL METODO ExecuteReader QUE DEVUELVE UN LECTOR
            reader = com.ExecuteReader();
            //EL LECTOR TIENE UN METODO Read() QUE DEVUELVE
            //BOOLEAN Y LEERA LOS DATOS.
            //CADA VEZ QUE EJECUTAMOS Read() LEE UNA FILA
            reader.Read();
            //PARA RECUPERAR LOS DATOS SE UTILIZA ["columna"]
            String nombre = reader["DNOMBRE"].ToString();
            Console.WriteLine(nombre);
            //CERRAMOS EL LECTOR Y LA CONEXION
            reader.Close();
            cn.Close();
        }
    }
}
