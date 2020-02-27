Public Class LogicaDeNegocio
    Dim da As New LibreriasLAB.SQLDataAccess

    Public Function Login(ByVal email As String, ByVal pass As String) As Boolean
        Return da.Login(email, pass)
    End Function

    Public Sub Conectar()
        da.Conectar()

    End Sub

    Public Function Registrar(ByVal email As String, ByVal nombre As String, ByVal apellidos As String, ByVal rol As String, ByVal pass As String) As String

        Dim numconfir = CLng(Rnd() * 9000000) + 1000000
        Try
            da.Registrar(email, nombre, apellidos, rol, pass, numconfir)
        Catch ex As Exception
            Return ex.Message
        End Try

        Dim gCorreo As New GestionCorreo
        Dim mensaje As String
        mensaje = "
        <html>
        <head>
        </head>
        <body>
        <h1> HADS 19-20 G17</h1>
        <p>Gracias por registrarte en nuestra web, para confirmar tu registro
        haz click en el siguiente enlace 
        <a href= 'http://localhost:56315/confirmar.aspx/?email=" & email & "&numconfir=" & numconfir & "'>Confirmar registro</a>
        </body>
        </html>"

        gCorreo.SendEmail(email, "Registro", mensaje)

        Return ("Registrado correctamente")
    End Function

    Public Function VerificarRegistro(ByVal email As String, ByVal numconfir As Integer) As Boolean
        Try
            da.VerificarRegistro(email, numconfir)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Public Function SolicitarCambioPass(ByVal email As String) As Boolean
        Dim codpass = CLng(Rnd() * 900000) + 100000


        Try
            da.CambiarCodPass(email, codpass)
        Catch ex As Exception
            Return False
        End Try

        Dim gCorreo As New GestionCorreo
        Dim mensaje As String
        mensaje = "
        <html>
        <head>
        </head>
        <body>
        <h1> HADS 19-20 G17</h1>
        <p>Aquí su código: </p>
        <p> " & codpass & " </p>
        <p> Por favor, introduzca este código para verificar su cambio de contraseña</p>
        <a href= 'http://localhost:56315/ModifyPass.aspx'>cambio de contraseña</a>
        </body>
        </html>"

        gCorreo.SendEmail(email, "Cambio contraseña", mensaje)

        Return True
    End Function

    Public Function VerificarCodPass(ByVal email As String, ByVal codpass As Integer) As String
        Return da.VerificarCodPass(email, codpass)
    End Function



    Public Function CambiarPass(ByVal email As String, ByVal pass As String) As Boolean
        Return (da.CambiarPass(email, pass) And da.CambiarCodPass(email, 0))
    End Function















End Class
