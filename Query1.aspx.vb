Option Explicit On
Option Strict On

Imports System.Configuration.ConfigurationManager
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics




Partial Class _Default
    Inherits System.Web.UI.Page
    Dim _rowcount As Integer = 0
    Dim _sumpic As Integer = 0
    Dim _rowcount3 As Integer = 0
    Dim kaz1 As Integer = 0



    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim senderGridView As GridView = CType(sender, GridView)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='pointer';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"
            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + CStr(e.Row.RowIndex))
            Dim product As Kepek.getlista1tableRow = CType(CType(e.Row.DataItem, System.Data.DataRowView).Row, Kepek.getlista1tableRow)
            If Not product.Isa1Null() Then _sumpic += product.a1

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' Display the summary data in the appropriate cells
            e.Row.Cells(0).Text = _rowcount.ToString + " rows"
            e.Row.Cells(3).Text = CStr(GridView1.DataKeys(0).Value)
            ' e.Row.Cells(2).Text = _sumpic.ToString
        End If
        If e.Row.RowType = DataControlRowType.Header Then
            If (GridView1.SortExpression = "forma" Or GridView1.SortExpression = "") Then
                e.Row.Cells(0).ForeColor = System.Drawing.Color.Yellow
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                If GridView1.SortExpression = "name_en" Then e.Row.Cells(1).ForeColor = System.Drawing.Color.Yellow
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                If GridView1.SortExpression = "a2" Then e.Row.Cells(2).ForeColor = System.Drawing.Color.Yellow
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                If GridView1.SortExpression = "a1" Then e.Row.Cells(3).ForeColor = System.Drawing.Color.Yellow
            End If
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        TextBox2.Text = Left(CStr(GridView1.SelectedDataKey.Item(1)), 12)
        TextBox1.Text = ""
        GridView3.Visible = True
        GridView3.SelectedIndex = -1

        Dim dbConn As OleDbConnection = Nothing
        Dim dc As OleDbCommand
        Dim dr As OleDbDataReader
        Dim strConnection As String
        Dim strSQL As String
        Dim img As System.Web.UI.HtmlControls.HtmlImage
        Dim ix, iy As Integer
        Dim row As GridViewRow = GridView1.Rows(GridView1.SelectedIndex)
        Dim p1, p2, p3 As String
        Dim maxegybe As Integer

        maxegybe = 50

        p1 = TextBox2.Text
        p2 = TextBox1.Text

        ix = CInt(row.Cells(3).Text)
        iy = CInt(row.Cells(2).Text)

        If (True) Then
            Try
                'get the connection string from web.config and open a connection 
                'to the database
                strConnection =
                  ConnectionStrings("dbConnectionString").ConnectionString
                dbConn = New OleDb.OleDbConnection(strConnection)
                dbConn.Open()

                strSQL = "select normal,best,alldown from webfullimage where vevokod='" & TextVevo.Text & "'"
                dc = New OleDbCommand(strSQL, dbConn)
                dr = dc.ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    GlobalVariables.shownormalimage = CBool(dr(0).ToString)
                    GlobalVariables.shownormalimage = True
                    GlobalVariables.showfullimage = CBool(dr(1).ToString)
                    GlobalVariables.allowalldownload = CBool(dr(2).ToString)
                End If
                'build the query string and get the data from the database
                p3 = CStr(GridView1.SelectedDataKey.Item(2))
                GlobalVariables.alap1 = CStr(GridView1.SelectedDataKey.Item(1))
                GlobalVariables.alap2 = ""
                GlobalVariables.alap2 = p3
                If CheckByMinta.Checked Then
                    If ix <= maxegybe Then p3 = ""
                    strSQL = "exec dbo.getlista3 '" + p1 + "','" + p3 + "','" + TextForm.Text + "','" + TextName.Text + "','" + TextLang.Text + "','" + TextVevo.Text + "',1,'" + TextForm.Text + "'"
                Else
                    If ix > maxegybe Then p2 = p3
                    strSQL = "exec dbo.getlista3 '" + p1 + "','" + TextDeko.Text + "','" + p2 + "','','','" + TextVevo.Text + "',0,'" + TextForm.Text + "'"
                End If

                GlobalVariables.strSQL1 = strSQL
                dc = New OleDbCommand(strSQL, dbConn)
                ReDim GlobalVariables.azarray(999)
                GlobalVariables.ix = 0
                dr = dc.ExecuteReader()

                'set the source of the data for the repeater control and bind it
                dlImages.DataSource = dr
                dlImages.SelectedIndex = -1
                dlImages.DataBind()
                'get a reference to the image used for the bar in the row
                img = CType(FindControl("imgBook"), HtmlImage)

                'ha nincs semmi kiválasztva, akkor nem látható
                img.Src = ""
                img.Visible = False
                HyperLinkNormal.Visible = False
                ButtonDownload.Visible = False
                HyperLinkBest.Visible = False
                TextKaz.Text = ""
                kaz1 = 0
                ButtonNextkep.Visible = True
                ButtonAlldown.Visible = GlobalVariables.allowalldownload

            Finally
                'clean up
                If (Not IsNothing(dbConn)) Then
                    dbConn.Close()
                End If
            End Try
        End If
        If ix > maxegybe Or iy = 1 Then
            GridView3.SelectedIndex = 0
        End If
    End Sub

    Protected Sub dlImages_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlImages.ItemDataBound
        Dim img As System.Web.UI.HtmlControls.HtmlImage
        Dim k_az As Integer
        Dim i As Integer
        Dim vanilyen As Boolean

        'make sure this is an item in the data list (not header etc.)
        If ((e.Item.ItemType = ListItemType.Item) Or
            (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            'get a reference to the image used for the bar in the row
            img = CType(e.Item.FindControl("imgThumbnail"),
                        System.Web.UI.HtmlControls.HtmlImage)

            'set the source to the page that generates the thumbnail image
            k_az = CInt(CType(e.Item.DataItem, Data.Common.DbDataRecord)("k_az"))
            img.Src = "Thumb0.aspx?ImageID=" &
                      CStr(CType(e.Item.DataItem, Data.Common.DbDataRecord)("k_az"))
            img.Alt = "123.jpg"
            For i = 0 To GlobalVariables.ix
                If GlobalVariables.azarray(i) = k_az Then vanilyen = True
                If vanilyen Then Exit For
            Next i
            If Not vanilyen And GlobalVariables.ix < 1000 Then
                GlobalVariables.ix = GlobalVariables.ix + 1
                GlobalVariables.azarray(GlobalVariables.ix) = k_az
            End If
        End If
    End Sub

    Protected Sub dlImages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlImages.SelectedIndexChanged
        Dim kaz As Integer = CInt(dlImages.DataKeys(dlImages.SelectedItem.ItemIndex))
        Dim img As System.Web.UI.HtmlControls.HtmlImage
        Dim dbConn As OleDbConnection = Nothing
        Dim strConnection As String

        Dim index As Integer = dlImages.SelectedItem.ItemIndex
        Dim lbl As LinkButton = DirectCast(dlImages.Items(index).FindControl("LinkButton1"), LinkButton)
        GlobalVariables.Termek = lbl.Text
        GlobalVariables.Vevokod = TextVevo.Text
        GlobalVariables.k_az = kaz

        TextKaz.Text = CStr(kaz)
        TextKaz.Visible = Debugger.IsAttached
        TextVevo.ReadOnly = Not Debugger.IsAttached
        Try
            'get the connection string from web.config and open a connection to the database
            strConnection = ConfigurationManager.
            ConnectionStrings("dbConnectionString").ConnectionString
            dbConn = New OleDb.OleDbConnection(strConnection)
            dbConn.Open()
        Finally
            'clean up
            If (Not IsNothing(dbConn)) Then
                dbConn.Close()
            End If
        End Try

        'get a reference to the image used for the bar in the row
        img = CType(FindControl("imgBook"), HtmlImage)

        'set the source to the page that generates the thumbnail image
        img.Src = "Thumb1.aspx?ImageID=" &
                  CStr(kaz) & "&imageFull=0"
        img.Visible = True
        img.Alt = "124.jpg"
        'HyperLinkNormal.Visible = shownormalimage
        HyperLinkNormal.Visible = True 'mindenki látja
        ButtonDownload.Visible = True
        HyperLinkNormal.NavigateUrl = "Thumb1.aspx?ImageID=" &
                  CStr(kaz) & "&imageFull=1"
        HyperLinkBest.Visible = GlobalVariables.showfullimage
        HyperLinkBest.NavigateUrl = "Thumb2.aspx?ImageID=" &
                  CStr(kaz)
    End Sub


    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        Dim img As System.Web.UI.HtmlControls.HtmlImage
        TextBox1.Text = ""
        TextBox2.Text = ""
        GridView3.Visible = False

        dlImages.DataSource = Nothing
        dlImages.SelectedIndex = -1
        dlImages.DataBind()
        'get a reference to the image used for the bar in the row
        img = CType(FindControl("imgBook"), HtmlImage)

        'set the source to the page that generates the thumbnail image
        img.Src = ""
        img.Visible = False
        HyperLinkNormal.Visible = False
        ButtonDownload.Visible = False
        HyperLinkBest.Visible = False
        TextKaz.Text = ""
        TextKaz.Visible = Debugger.IsAttached
        kaz1 = 0
    End Sub

    Protected Sub ObjectDataSource1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub GridView1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBinding
        LastQuery.Text = CStr(Now)
        TextKaz.Text = ""
        TextKaz.Visible = Debugger.IsAttached
        kaz1 = 0
        GridView1.SelectedIndex = -1
        TextBox1.Text = ""
        TextBox2.Text = ""
        ButtonNextkep.Visible = False
        ButtonDownload.Visible = False
        ButtonAlldown.Visible = False
    End Sub

    Protected Sub TextDeko_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextDeko.TextChanged
        TextDeko.Text = TextDeko.Text.ToUpper
    End Sub

    Protected Sub ObjectDataSource1_Load1(sender As Object, e As System.EventArgs) Handles ObjectDataSource1.Load
        LastQuery.Text = CStr(Now)
        TextKaz.Text = ""
        TextKaz.Visible = Debugger.IsAttached
        kaz1 = 0
    End Sub

    Protected Sub Linkbutton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView3.SelectedIndexChanged
        TextBox1.Text = CStr(GridView3.SelectedDataKey.Item(1))
        Dim dbConn As OleDbConnection = Nothing
        Dim dc As OleDbCommand
        Dim dr As OleDbDataReader
        Dim strConnection As String
        Dim strSQL As String
        Dim img As System.Web.UI.HtmlControls.HtmlImage
        Dim p1, p2 As String

        p1 = TextBox2.Text
        p2 = TextBox1.Text
        If (True) Then
            Try
                'get the connection string from web.config and open a connection 
                'to the database
                strConnection =
                  ConnectionStrings("dbConnectionString").ConnectionString
                dbConn = New OleDb.OleDbConnection(strConnection)
                dbConn.Open()

                'build the query string and get the data from the database
                GlobalVariables.alap2 = CStr(GridView3.SelectedDataKey.Item(1))
                If CheckByMinta.Checked Then
                    strSQL = "exec dbo.getlista3 '" + p1 + "','" + p2 + "','" + TextForm.Text + "','" + TextName.Text + "','" + TextLang.Text + "','" + TextVevo.Text + "',1,null"
                Else
                    strSQL = "exec dbo.getlista3 '" + p1 + "','" + TextDeko.Text + "','" + p2 + "','','','" + TextVevo.Text + "',0,null"
                End If

                GlobalVariables.strSQL1 = strSQL
                dc = New OleDbCommand(strSQL, dbConn)
                ReDim GlobalVariables.azarray(999)
                GlobalVariables.ix = 0
                dr = dc.ExecuteReader()

                'set the source of the data for the repeater control and bind it
                dlImages.DataSource = dr
                dlImages.SelectedIndex = -1
                dlImages.DataBind()
                'dlImages_SelectedIndexChanged(dlImages, EventArgs.Empty)

                'get a reference to the image used for the bar in the row
                img = CType(FindControl("imgBook"), HtmlImage)

                'ha nincs semmi kiválasztva, akkor nem látható
                img.Src = ""
                img.Visible = False
                HyperLinkNormal.Visible = False
                HyperLinkBest.Visible = False
                TextKaz.Text = ""
                TextKaz.Visible = Debugger.IsAttached
                kaz1 = 0

            Finally
                'clean up
                If (Not IsNothing(dbConn)) Then
                    dbConn.Close()
                End If
            End Try
        End If
    End Sub

    Protected Sub GridView3_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        Dim ix As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onmouseover") = "this.style.cursor='pointer';this.style.textDecoration='underline';"
            e.Row.Attributes("onmouseout") = "this.style.textDecoration='none';"
            e.Row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(Me.GridView3, "Select$" + CStr(e.Row.RowIndex))
            Dim product As Kepek.getlista2tableRow = CType(CType(e.Row.DataItem, System.Data.DataRowView).Row, Kepek.getlista2tableRow)
            ' Increment the running totals (if they're not NULL!)
            _rowcount += 1
            If Not product.Isa1Null() Then _sumpic += product.a1

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            ' Display the summary data in the appropriate cells
            e.Row.Cells(0).Text = _rowcount3.ToString + " rows"
            'e.Row.Cells(1).Text = _sumpic.ToString
            Dim Row As GridViewRow = GridView3.Rows(0)
            If CheckByMinta.Checked Then ix = 2 Else ix = 1
            e.Row.Cells(ix).Text = CStr(GridView3.DataKeys(0).Value)
        End If
    End Sub

    Protected Sub Obj3_Selected(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles Obj3.Selected
        Dim dt As DataTable = CType(e.ReturnValue, DataTable)
        _rowcount3 = dt.Rows.Count
        Label4.Text = CStr(_rowcount3)
    End Sub

    Protected Sub ObjectDataSource1_Selected(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles ObjectDataSource1.Selected
        Dim dt As DataTable = CType(e.ReturnValue, DataTable)
        _rowcount = dt.Rows.Count
        Label4.Text = CStr(_rowcount3)
    End Sub

    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged, RadioButton4.CheckedChanged
        Dim s As String
        s = "E"
        If RadioButton2.Checked Then s = "D"
        If RadioButton3.Checked Then s = "F"
        If RadioButton4.Checked Then s = "H"
        TextLang.Text = s
        If TextName.Text <> "" Then TextName.Text = ""
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim dbConn As OleDbConnection = Nothing
        TextKaz.Visible = Debugger.IsAttached
        TextVevo.ReadOnly = Not Debugger.IsAttached
        If TextVevo.Text = "" Then TextVevo.Text = UCase(Membership.GetUser().UserName)

    End Sub

    Protected Sub TextForm_TextChanged(sender As Object, e As System.EventArgs) Handles TextForm.TextChanged
        'TextBox2.Text = ""
        GridView1.PageIndex = 0

    End Sub


    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckByMinta.CheckedChanged
        If CheckByMinta.Checked Then
            GridView1.Columns(1).Visible = False
            GridView1.Width = 150
            GridView1.Columns(0).ItemStyle.Width = 100
            GridView1.Columns(0).HeaderText = "Decor"
            GridView1.Columns(2).HeaderText = "For."
            GridView3.Width = 285
            GridView3.Columns(1).Visible = True
            GridView3.Columns(0).ItemStyle.Width = 60
            GridView3.Columns(0).HeaderText = "Form"
        Else
            GridView1.Columns(1).Visible = True
            GridView1.Width = 300
            GridView1.Columns(0).ItemStyle.Width = 60
            GridView1.Columns(0).HeaderText = "Form"
            GridView1.Columns(2).HeaderText = "Dec."
            GridView3.Width = 125
            GridView3.Columns(1).Visible = False
            GridView3.Columns(0).ItemStyle.Width = 100
            GridView3.Columns(0).HeaderText = "Decor"
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonNextkep.Click

        If dlImages.SelectedIndex >= dlImages.Items.Count - 1 Then
            dlImages.SelectedIndex = 0
        Else
            dlImages.SelectedIndex = dlImages.SelectedIndex + 1
        End If
        dlImages_SelectedIndexChanged(dlImages, EventArgs.Empty)
        'Response.Write("<script> window.open('" + HyperLinkNormal.NavigateUrl + "','_blank'); </script>")
    End Sub
    Protected Sub Button2_Click1(sender As Object, e As EventArgs) Handles ButtonDownload.Click
        DownloadKep()
    End Sub

    Protected Sub DownloadKep()
        Dim dbConn As OleDbConnection = Nothing
        Dim dc As OleDbCommand
        Dim imageData() As Byte
        Dim strConnection As String
        Dim strSQL As String
        'to the database
        strConnection = ConfigurationManager.
                  ConnectionStrings("dbConnectionString").ConnectionString
        dbConn = New OleDb.OleDbConnection(strConnection)
        dbConn.Open()
        'build the query string and get the data from the database
        strSQL = "select kep as thu from kep2"
        strSQL = strSQL & " where k_az=" & CStr(GlobalVariables.k_az)
        dc = New OleDbCommand(strSQL, dbConn)
        imageData = CType(dc.ExecuteScalar(), Byte())
        dbConn.Close()
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "image/jpeg"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + GlobalVariables.Termek + ".jpg")
        Response.BinaryWrite(imageData)
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub Button2_Click2(sender As Object, e As EventArgs) Handles ButtonAlldown.Click
        'Dim archive = Server.MapPath("~/archive.zip")
        'Dim temp = Server.MapPath("~/temp")
        Dim zipalap = TextVevo.Text
        Dim dbConn As OleDbConnection = Nothing
        Dim dc As OleDbCommand
        Dim dr As OleDbDataReader
        Dim strConnection As String
        Dim strSQL As String
        Dim most As String
        Dim termek, filename As String
        Dim ix As Integer
        Dim imageData() As Byte
        'to the database
        strConnection = ConfigurationManager.
                  ConnectionStrings("dbConnectionString").ConnectionString
        dbConn = New OleDb.OleDbConnection(strConnection)
        dbConn.Open()
        'build the query string and get the data from the database
        strSQL = "select * from most1"
        dc = New OleDbCommand(strSQL, dbConn)
        most = CType(dc.ExecuteScalar(), String)
        zipalap = zipalap + "_" + most
        Dim archive = Server.MapPath("~/zip/" + zipalap + ".zip")
        Dim temp = Server.MapPath("~/temp/" + zipalap)

        If Not IO.Directory.Exists(temp) Then
            IO.Directory.CreateDirectory(temp)
        End If
        If IO.File.Exists(archive) Then
            IO.File.Delete(archive)
        End If
        dc = New OleDbCommand(GlobalVariables.strSQL1, dbConn)
        dr = dc.ExecuteReader()
        ix = 0
        termek = GlobalVariables.alap1 + " " + GlobalVariables.alap2
        termek = RTrim(termek)
        While dr.Read()
            ix = ix + 1
            strSQL = "select kep as thu from kep2 where k_az=" & dr(0).ToString
            dc = New OleDbCommand(strSQL, dbConn)
            imageData = CType(dc.ExecuteScalar(), Byte())
            filename = termek + " " + Right("0000" + ix.ToString, 4)
            IO.File.WriteAllBytes(temp + "\" + filename + ".jpg", imageData)

        End While
        dbConn.Close()
        IO.Compression.ZipFile.CreateFromDirectory(temp, archive)
        If IO.Directory.Exists(temp) Then
            DeleteFilesAndFoldersRecursively(temp)
        End If
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/x-zip-compressed"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + termek + ".zip")
        Response.TransmitFile(archive)
        Response.Flush()
        Response.End()
    End Sub

    Public Shared Sub DeleteFilesAndFoldersRecursively(ByVal target_dir As String)
        For Each file As String In IO.Directory.GetFiles(target_dir)
            IO.File.Delete(file)
        Next
        For Each subDir As String In IO.Directory.GetDirectories(target_dir)
            DeleteFilesAndFoldersRecursively(subDir)
        Next
        System.Threading.Thread.Sleep(1)
        IO.Directory.Delete(target_dir)
    End Sub

End Class
