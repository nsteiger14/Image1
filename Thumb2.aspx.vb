Option Explicit On
Option Strict Off

Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Partial Class Thumb2
    Inherits System.Web.UI.Page
    'constants used to create URLs to this page
    Public Const PAGE_NAME As String = "Best image"
    Public Const QS_IMAGE_ID As String = "ImageID"


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'height of the thumbnail created from the original image
        Dim dbConn As OleDbConnection = Nothing
        Dim dc As OleDbCommand
        Dim imageData() As Byte
        Dim strConnection As String
        Dim strSQL As String
        Dim ms As MemoryStream
        Dim ms1 As MemoryStream
        Dim fullsizeImage As Image
        Dim imageID As String
        Dim gepnev As String

        If (Not Page.IsPostBack) Then
            Try
                'get the ID of the image to retrieve from the database
                imageID = Request.QueryString(QS_IMAGE_ID)
                If imageID < "0" Or imageID <> GlobalVariables.k_az Then Exit Sub
                gepnev = ""
                'get the connection string from web.config and open a connection to the database
                strConnection = ConfigurationManager. _
                  ConnectionStrings("dbConnectionString").ConnectionString
                dbConn = New OleDb.OleDbConnection(strConnection)
                dbConn.Open()
                'teljes kép importálása Kepbig.dbo.kepnagy táblába
                strSQL = "exec dbo.getnagykep " & imageID & ",'" & GlobalVariables.Vevokod & "','"
                strSQL = strSQL & System.Web.HttpContext.Current.Request.UserHostAddress & "','"
                strSQL = strSQL & gepnev & "'"
                dc = New OleDbCommand(strSQL, dbConn)
                dc.ExecuteScalar()
                'strSQL = "select kep from Kepbig.dbo.kepnagy where k_az=" & imageID
                strSQL = "select case when k.kep is null then kep2.kep else k.kep end kep from kep2"
                strSQL = strSQL + " left join Kepbig.dbo.kepnagy k on k.k_az=kep2.k_az where kep2.k_az=" & imageID
                dc = New OleDbCommand(strSQL, dbConn)
                imageData = CType(dc.ExecuteScalar(), Byte())

                'create an image from the byte array
                ms = New MemoryStream(imageData)
                ms1 = New MemoryStream
                fullsizeImage = System.Drawing.Image.FromStream(ms)

                'write thumbnail to the response object
                fullsizeImage.Save(ms1, ImageFormat.Jpeg)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "image/jpeg"
                Response.AddHeader("content-disposition", "filename=" & GlobalVariables.Termek & " full.jpg")
                Response.BinaryWrite(ms1.ToArray())

            Finally
                'clean up
                If (Not IsNothing(dbConn)) Then
                    dbConn.Close()
                End If
            End Try
        End If
    End Sub
End Class
