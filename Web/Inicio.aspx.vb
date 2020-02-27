
Public Class Inicio
    Inherits System.Web.UI.Page
    Dim ln As New LibreriasLAB.LogicaDeNegocio

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ln.Conectar()
    End Sub


    Protected Sub Login(sender As Object, e As EventArgs) Handles loginButton.Click

        If (ln.Login(email.Text, password.Text)) Then
            conection.Text = "LOGEADO CORRECTAMENTE"
        Else
            conection.Text = "Login incorrecto"
        End If

    End Sub


End Class