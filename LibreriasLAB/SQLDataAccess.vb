
Imports System.Data.SqlClient


Public Class SQLDataAccess
    Private Shared conexion As New SqlConnection
    Private Shared comando As New SqlCommand

    Public Function Conectar() As String
        Try
            conexion.ConnectionString = “Server=tcp:hads.database.windows.net,1433;Initial Catalog=Amigos;Persist Security Info=False;User ID=vadillo@ehu.es@hads;Password=curso19-20;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            conexion.Open()
            Return "Conexión establecida"
        Catch ex As Exception
            Return "ERROR DE CONEXIÓN: " + ex.Message
        End Try
    End Function

    Public Function Registrar(ByVal email As String, ByVal nombre As String, ByVal apellidos As String, ByVal rol As String, ByVal pass As String, ByVal numConfirm As Integer) As Boolean
        Dim st = "insert into Usuarios values ('" & email & "', '" & nombre & "', '" & apellidos & "',  '" & numConfirm & "', 0, '" & rol & "','" & pass & "','0')"
        comando = New SqlCommand(st, conexion)
        comando.ExecuteNonQuery()
        Return True
    End Function

    Public Function Login(ByVal email As String, ByVal pass As String) As Boolean
        Dim st = "select count(*) from Usuarios where email='" & email & "' and pass='" & pass & "'  and confirmado=1"
        comando = New SqlCommand(st, conexion)
        If comando.ExecuteScalar() = 1 Then
            Return True
        End If
        Return False
    End Function


    Public Function VerificarRegistro(ByVal email As String, ByVal numConfir As Integer) As Boolean
        Dim st = "select count(*) from Usuarios where email='" & email & "' and numconfir='" & numConfir & "' and confirmado=0"
        comando = New SqlCommand(st, conexion)
        If comando.ExecuteScalar() = 1 Then
            st = "update Usuarios set confirmado = 1 where email= '" & email & "' "
            comando = New SqlCommand(st, conexion)
            comando.ExecuteNonQuery()
            Return True
        End If
        Return False
    End Function


    Public Function CambiarPass(ByVal email As String, ByVal pass As String) As Boolean
        Dim st = "select count(*) from Usuarios where email='" & email & "' "
        comando = New SqlCommand(st, conexion)
        If comando.ExecuteScalar() = 1 Then
            st = "update Usuarios set pass = '" & pass & "' where email= '" & email & "' "
            comando = New SqlCommand(st, conexion)
            comando.ExecuteNonQuery()
            Return True
        End If
        Return False
    End Function


    Public Function CambiarCodPass(ByVal email As String, ByVal codpass As Integer) As Boolean

        Dim st = "select count(*) from Usuarios where email='" & email & "' "
        comando = New SqlCommand(st, conexion)
        If comando.ExecuteScalar() = 1 Then
            st = "update Usuarios set codpass = " & codpass & " where email= '" & email & "' "
            comando = New SqlCommand(st, conexion)
            comando.ExecuteNonQuery()
            Return True

        End If
        Return False
    End Function



    Public Function VerificarCodPass(ByVal email As String, ByVal codpass As Integer) As Boolean

        Dim st = "select count(*) from Usuarios where email='" & email & "' and codpass='" & codpass & "' "
        comando = New SqlCommand(st, conexion)
        If comando.ExecuteScalar() = 1 Then
            Return True
        End If
        Return False
    End Function


End Class
