Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class ClaseConexion
    Public Connection As SqlConnection = New SqlConnection("Data Source=HP\SQLSERVER2012;Initial Catalog=Formulario;Integrated Security=True")
    Private cmb As SqlCommandBuilder
    Public ds As DataSet = New DataSet
    Public da As SqlDataAdapter
    Public cmd As SqlCommand

    Public Sub conectar()
        Try
            Connection.Open()
            MessageBox.Show("Conexíon realizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error al realizar la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Connection.Close()
        End Try
    End Sub

    Public Sub Query(ByVal SQL As String, ByVal Table As String)
        Try
            ds.Tables.Clear()
            da = New SqlDataAdapter(SQL, Connection)
            cmb = New SqlCommandBuilder(da)
            da.Fill(ds, Table)
        Catch ex As Exception
            MessageBox.Show("Error al mostrar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function Operaciones(ByVal SQL)
        Try
            Connection.Open()
            cmd = New SqlCommand(SQL, Connection)
            Dim i As Integer = cmd.ExecuteNonQuery()
            If (i > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class