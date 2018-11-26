Option Explicit On
Option Strict Off

Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Partial Class Thumb0
    Inherits System.Web.UI.Page

    'constants used to create URLs to this page
    Public Const PAGE_NAME As String = "Thumb0.aspx"
    Public Const QS_IMAGE_ID As String = "ImageID"

    Private Sub Page_Load(ByVal sender As Object, _
                          ByVal e As System.EventArgs) Handles Me.Load
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
        Dim i As Integer
        Dim vanilyen As Boolean

        If (Not Page.IsPostBack) Then
            Try

                'get the ID of the image to retrieve from the database
                imageID = Request.QueryString(QS_IMAGE_ID)
                If imageID < "0" Then Exit Sub
                vanilyen = False
                For i = 0 To GlobalVariables.ix
                    If GlobalVariables.azarray(i) = imageID Then vanilyen = True
                    If vanilyen Then Exit For
                Next i
                If Not vanilyen Then Exit Sub
                'get the connection string from web.config and open a connection 
                'to the database
                strConnection = ConfigurationManager. _
                  ConnectionStrings("dbConnectionString").ConnectionString
                dbConn = New OleDb.OleDbConnection(strConnection)
                dbConn.Open()
                'build the query string and get the data from the database
                strSQL = "select thu from thu2"
                strSQL = strSQL & " where k_az=" & imageID

                dc = New OleDbCommand(strSQL, dbConn)
                imageData = CType(dc.ExecuteScalar(), Byte())

                'create an image from the byte array
                ms = New MemoryStream(imageData)
                ms1 = New MemoryStream
                fullsizeImage = System.Drawing.Image.FromStream(ms)
                fullsizeImage.Save(ms1, ImageFormat.Jpeg)

                Response.ContentType = "image/jpeg"
                Response.BinaryWrite(ms1.ToArray())

            Finally
                'clean up
                If (Not IsNothing(dbConn)) Then
                    dbConn.Close()
                End If
            End Try
        End If
    End Sub  'Page_Load  

End Class
