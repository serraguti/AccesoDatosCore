using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class RepositoryDepartamentos
    {
        //QUE OBJETOS NECESITA LA CLASE PARA ACCEDER A DATOS??
        //CadenaConexion, Connection, Command, DataReader
        //LA CLASE Program UTILIZARA DICHOS OBJETOS PARA INSERTAR??
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;
        private String cadenaconexion;

        //ESTOS OBJETOS DEBO DE INSTANCIARLOS...CUANDO?
        public RepositoryDepartamentos()
        {
            //INSTANCIAMOS TODAS LAS HERRAMIENTAS DE LA CLASE
            this.cadenaconexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=azure";
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();
            //SOLAMENTE LE DIREMOS UNA VEZ AL COMANDO SU CONEXION Y SU TIPO DE CONSULTAS
            this.com.Connection = this.cn;
            this.com.CommandType = System.Data.CommandType.Text;
        }

        //CREAMOS UN METODO PARA INSERTAR, ¿QUE NECESITAMOS QUE NOS DEN?
        public int InsertarDepartamento(int num, String nom, String localidad)
        {
            String sqlinsert = "INSERT INTO DEPT VALUES (@NUMERO, @NOMBRE, @LOCALIDAD)";
            SqlParameter pamnumero = new SqlParameter("@NUMERO", num);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", nom);
            SqlParameter pamlocalidad = new SqlParameter("@LOCALIDAD", localidad);
            //AÑADIMOS LOS PARAMETROS AL COMMAND
            this.com.Parameters.Add(pamnumero);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamlocalidad);
            //INDICAMOS AL COMANDO SU CONSULTA SQL
            this.com.CommandText = sqlinsert;
            //ABRIMOS LA CONEXION
            this.cn.Open();
            //EJECUTAMOS LA ACCION
            int result = this.com.ExecuteNonQuery();
            //CERRAMOS LA CONEXION
            this.cn.Close();
            //LIBERAMOS LOS PARAMETROS
            this.com.Parameters.Clear();
            return result;
        }

        //METODO PARA ELIMINAR
        public int EliminarDepartamento(int deptno)
        {
            String sqldelete = "DELETE FROM DEPT WHERE DEPT_NO=@DEPTNO";
            //INDICAMOS AL COMMAND SU CONSULTA
            this.com.CommandText = sqldelete;
            //CREAMOS EL PARAMETRO
            SqlParameter pamdeptno = new SqlParameter("@DEPTNO", deptno);
            this.com.Parameters.Add(pamdeptno);
            //ABRIMOS CONEXION
            this.cn.Open();
            //EJECUTAMOS
            int result = this.com.ExecuteNonQuery();
            //CERRAMOS CONEXION
            this.cn.Close();
            //LIBERAMOS PARAMETROS
            this.com.Parameters.Clear();
            return result;
        }

        //METODO PARA MODIFICAR
        public int ModificarDepartamento(int deptno, String nombre, String localidad)
        {
            String sqlupdate = "UPDATE DEPT SET DNOMBRE=@DNOMBRE, LOC=@LOCALIDAD "
                + " WHERE DEPT_NO=@DEPTNO";
            this.com.CommandText = sqlupdate;
            SqlParameter pamnum = new SqlParameter("@DEPTNO", deptno);
            SqlParameter pamnom = new SqlParameter("@DNOMBRE", nombre);
            SqlParameter pamloc = new SqlParameter("@LOCALIDAD", localidad);
            this.com.Parameters.Add(pamnum);
            this.com.Parameters.Add(pamnom);
            this.com.Parameters.Add(pamloc);
            this.cn.Open();
            int result = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return result;
        }

        //METODO PARA BUSCAR POR ID
        //QUE DEVOLVEMOS AL BUSCAR CON UN SELECT?? UN DEPARTAMENTO
        public Departamento BuscarDepartamento(int id)
        {
            String sqlselect = "SELECT * FROM DEPT WHERE DEPT_NO=@DEPTNO";
            this.com.CommandText = sqlselect;
            SqlParameter pamnum = new SqlParameter("@DEPTNO", id);
            this.com.Parameters.Add(pamnum);
            this.cn.Open();
            //EJECUTAMOS LA CONSULTA SELECT CON EL Reader
            this.reader = this.com.ExecuteReader();
            //SOLO EXISTE UN REGISTRO, PREGUNTAMOS SI TENEMOS DATOS...
            if (this.reader.Read())
            {
                //CREAMOS UN DEPARTAMENTO PARA DEVOLVER LOS DATOS
                Departamento departamento = new Departamento();
                //GUARDAMOS LOS DATOS DEL reader EN NUESTRO OBJETO
                departamento.Numero = int.Parse(this.reader["DEPT_NO"].ToString());
                departamento.Nombre = this.reader["DNOMBRE"].ToString();
                departamento.Localidad = this.reader["LOC"].ToString();
                //LIBERAMOS EL LECTOR
                this.reader.Close();
                this.cn.Close();
                this.com.Parameters.Clear();
                //DEVOLVEMOS EL OBJETO
                return departamento;
            }
            else
            {
                //QUE DEVOLVEMOS SI NO HAY DEPARTAMENTO, ES DECIR, 
                //TENEMOS QUE DEVOLVER UN OBJETO VACIO
                return null;
            }
        }

        //QUE DEVOLVEMOS ESTA VEZ EN NUESTRO SELECT¿? VARIOS DEPARTAMENTOS
        public List<Departamento> GetDepartamentos()
        {
            String sql = "SELECT * FROM DEPT";
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            //DICHOS DATOS, NECESITAMOS ALMACENARLOS EN UNA COLECCION
            //INSTANCIAMOS UNA COLECCION
            List<Departamento> departamentos = new List<Departamento>();
            //EXTRAEMOS MULTIPLES DATOS
            while (this.reader.Read())
            {
                //CREAMOS UN OBJETO POR CADA FILA DEL READER
                Departamento departamento = new Departamento();
                //ASIGNAMOS LOS VALORES
                departamento.Numero = (int)this.reader["DEPT_NO"];
                departamento.Nombre = this.reader["DNOMBRE"].ToString();
                departamento.Localidad = this.reader["LOC"].ToString();
                //AÑADIMOS CADA OBJETO A LA COLECCION
                departamentos.Add(departamento);
            }
            this.reader.Close();
            this.cn.Close();
            return departamentos;
        }
    }
}
