using System;
using System.Data.SqlClient;

namespace AccesoDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            AccionRegistros();
            EliminarDepartamento();
            LeerRegistros();
        }

        static void EliminarDepartamento()
        {
            String cadenaconexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            String sql = "DELETE FROM DEPT WHERE DEPT_NO=@NUMERO";
            Console.WriteLine("Número departamento a eliminar: ");
            int numero = int.Parse(Console.ReadLine());
            SqlParameter pamnumero = new SqlParameter("@NUMERO", numero);
            com.Connection = cn;
            com.CommandText = sql;
            com.CommandType = System.Data.CommandType.Text;
            //INCLUIMOS LOS PARAMETROS DENTRO DEL COMANDO
            com.Parameters.Add(pamnumero);
            cn.Open();
            int eliminados = com.ExecuteNonQuery();
            Console.WriteLine("Departamentos eliminados: " + eliminados);
            //NO IMPORTA EL ORDEN...
            cn.Close();
            //LIBERAMOS LOS PARAMETROS DEL COMANDO
            com.Parameters.Clear();
        }

        static void AccionRegistros()
        {
            String cadenaconexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            SqlConnection cn = new SqlConnection(cadenaconexion);
            SqlCommand com = new SqlCommand();
            String sql = "INSERT INTO DEPT VALUES (66, 'INFORMATICA', 'GIJON')";
            com.Connection = cn;
            com.CommandText = sql;
            com.CommandType = System.Data.CommandType.Text;
            cn.Open();
            //LAS CONSULTAS DE ACCION SE EJECUTAR CON ExecuteNonQuery()
            //Y DEVUELVEN EL NUMERO DE REGISTROS QUE HAN SIDO AFECTADOS
            int insertados = com.ExecuteNonQuery();
            cn.Close();
            Console.WriteLine("Insertados: " + insertados);
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
            //reader.Read();
            //PARA RECUPERAR LOS DATOS SE UTILIZA ["columna"]
            //String nombre = reader["DNOMBRE"].ToString();
            //Console.WriteLine(nombre);
            //LEEMOS TODOS LOS REGISTROS
            while (reader.Read())
            {
                String nombre = reader["DNOMBRE"].ToString();
                String localidad = reader["LOC"].ToString();
                Console.WriteLine(nombre + " - " + localidad);
            }
            //CERRAMOS EL LECTOR Y LA CONEXION
            reader.Close();
            cn.Close();
        }
    }
}
