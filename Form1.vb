Imports System.Data.SqlClient

Public Class Form1

    Dim Connection As ClaseConexion = New ClaseConexion()
    Dim generoPersona As Integer
    Dim adapter As SqlDataAdapter
    Dim Table As DataTable
    Dim data As DataSet

    Public Sub fillGenero()

        adapter = New System.Data.SqlClient.SqlDataAdapter("select * from genero", Connection.Connection)
        data = New DataSet
        adapter.Fill(data)
        cmb_genero.DataSource = data.Tables(0)
        cmb_genero.DisplayMember = "genero"
        cmb_genero.ValueMember = "id_genero"

    End Sub
    Public Sub fillEdad()

        adapter = New System.Data.SqlClient.SqlDataAdapter("select * from edad", Connection.Connection)
        data = New DataSet
        adapter.Fill(data)
        cmb_edad.DataSource = data.Tables(0)
        cmb_edad.DisplayMember = "edad"
        cmb_edad.ValueMember = "id_edad"

    End Sub

    Public Sub ShowData()
        Connection.Query("Consulta_Datos", "Datos")
        dgv_tabla.DataSource = Connection.ds.Tables("Datos")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_agregar.Click

        Dim insertar As String = "Insertar_datos " + txt_id.Text +
            ",'" + txt_nombre.Text +
            "'," + cmb_edad.SelectedValue.ToString +
            "," + cmb_genero.SelectedValue.ToString

        Try
            If Connection.Operaciones(insertar) Then
                MessageBox.Show("Registro insertado correctamente", "Éxito", MessageBoxButtons.OK)
                ShowData()
                Connection.Connection.Close()
            Else
                MessageBox.Show("Error al insertar registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Connection.Connection.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ConsultarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles menu_buscar.Click
        Dim ID As String = "Ingrese ID"
        ID = InputBox("Ingresa el ID de la persona que deseas buscar", "Búsqueda por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Try
                Connection.Query("Buscar_Datos " + ID, "Datos")
                dgv_tabla.DataSource = Connection.ds.Tables("Datos")
            Catch ex As Exception
                MessageBox.Show("Error al realizar la búsqueda", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Connection.conectar()
        ShowData()
        fillGenero()
        fillEdad()

    End Sub

    Private Sub menu_modificar_Click(sender As Object, e As EventArgs) Handles menu_modificar.Click


    End Sub

    Private Sub menu_eliminar_Click(sender As Object, e As EventArgs) Handles menu_eliminar.Click
        Dim ID As String = "Ingrese ID"
        ID = InputBox("Ingresa el ID de la persona que deseas Eliminar", "Eliminación por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim insertar As String = "Eliminar_Datos " + ID.ToString

            Try
                If Connection.Operaciones(insertar) Then
                    MessageBox.Show("Registro eliminado correctamente", "Éxito", MessageBoxButtons.OK)
                    ShowData()
                    Connection.Connection.Close()
                Else
                    MessageBox.Show("Error al eliminar registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Connection.Connection.Close()
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ConsultarToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ConsultarToolStripMenuItem.Click
        ShowData()
    End Sub

    Private Sub NombreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NombreToolStripMenuItem.Click
        Dim SQL As String = "Actualizar_nombre "
        UpdateData(SQL)
    End Sub

    Private Sub EdadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EdadToolStripMenuItem.Click
        Dim SQL As String = "Actualizar_Edad "
        Dim ID As String = "Ingrese ID"
        Dim Dato As String = "Por favor, ingresa el nuevo dato."

inicio:
        ID = InputBox("Ingresa el ID de la persona que deseas actualizar", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo fin
        End If

        Dato = InputBox("Ingresa el dato de la persona", "Actualización", Dato)
        If Dato = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            If Dato = "22" Then
                Dato = 1
            ElseIf Dato = "23" Then
                Dato = 2
            End If

            Dim update As String = SQL + ID.ToString + "," + Dato.ToString
            If Connection.Operaciones(update) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                ShowData()
                Connection.Connection.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Connection.Connection.Close()
            End If
        End If
fin:

    End Sub

    Private Sub GeneroToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeneroToolStripMenuItem.Click
        Dim SQL As String = "Actualiza_Genero "
        UpdateData(SQL)

    End Sub

    Public Sub UpdateData(ByVal name_sp As String)
        Dim ID As String = "Ingrese ID"
        Dim Dato As String = "Por favor, ingresa el nuevo dato."

inicio:
        ID = InputBox("Ingresa el ID de la persona que deseas actualizar", "Actualización por ID", ID)
        If ID = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            GoTo fin
        End If

        Dato = InputBox("Ingresa el dato de la persona", "Actualización", Dato)
        If Dato = "" Then
            MessageBox.Show("Casilla vacía", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else

            If Dato = "masculino" Or Dato = "Masculino" Or Dato = "MASCULINO" Then
                Dato = 2
            ElseIf Dato = "femenino" Or Dato = "Femenino" Or Dato = "FEMENINO" Then
                Dato = 1
            End If

            Dim update As String = name_sp + ID.ToString + "," + Dato.ToString
            If Connection.Operaciones(update) Then
                MessageBox.Show("Registro actualizado correctamente", "Éxito", MessageBoxButtons.OK)
                ShowData()
                Connection.Connection.Close()
            Else
                MessageBox.Show("Error al actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Connection.Connection.Close()
            End If
        End If
fin:
    End Sub

End Class
