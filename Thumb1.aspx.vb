Option Explicit On
Option Strict Off

Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO


Partial Class Thumb1
    Inherits System.Web.UI.Page

    'constants used to create URLs to this page
    Public Const PAGE_NAME As String = "Thumb1.aspx"
    Public Const QS_IMAGE_ID As String = "ImageID"
    Public Const QS_IMAGE_FULL As String = "ImageFull"

    Public Sub ResizeImage(scaleFactor As Double, fromStream As Stream, toStream As Stream)
        Dim image__1 As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        Dim thumbnailBitmap As Bitmap
        Dim thumbnailGraph As Graphics
        Dim imageRectangle As Rectangle
        image__1 = Image.FromStream(fromStream)
        newWidth = CInt(image__1.Width * scaleFactor)
        newHeight = CInt(image__1.Height * scaleFactor)
        thumbnailBitmap = New Bitmap(newWidth, newHeight)

        thumbnailGraph = Graphics.FromImage(thumbnailBitmap)
        'thumbnailGraph.CompositingQuality = Graphics.CompositingQuality.HighQuality
        'thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality
        'thumbnailGraph.InterpolationMode = HighQualityBicubic

        imageRectangle = New Rectangle(0, 0, newWidth, newHeight)
        thumbnailGraph.DrawImage(image__1, imageRectangle)

        thumbnailBitmap.Save(toStream, image__1.RawFormat)

        thumbnailGraph.Dispose()
        thumbnailBitmap.Dispose()
        image__1.Dispose()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, _
                          ByVal e As System.EventArgs) Handles Me.Load
        'height of the thumbnail created from the original image
        Const THUMBNAIL_WIDTH As Integer = 760
        Dim dbConn As OleDbConnection = Nothing
        Dim dc As OleDbCommand
        Dim imageData() As Byte
        Dim strConnection As String
        Dim strSQL As String
        Dim ms As MemoryStream
        Dim ms1 As MemoryStream
        Dim fullsizeImage As Image
        Dim imageID As String
        Dim imageFull As String
        Dim scalefact As Double
        Dim nevhez As String

        If (Not Page.IsPostBack) Then
            Try

                'get the ID of the image to retrieve from the database
                imageID = Request.QueryString(QS_IMAGE_ID)
                imageFull = Request.QueryString(QS_IMAGE_FULL)
                If imageID < "0" Or imageID <> GlobalVariables.k_az Then Exit Sub

                'get the connection string from web.config and open a connection 
                'to the database
                strConnection = ConfigurationManager. _
                  ConnectionStrings("dbConnectionString").ConnectionString
                dbConn = New OleDb.OleDbConnection(strConnection)
                dbConn.Open()
                'build the query string and get the data from the database
                strSQL = "select kep as thu from kep2"
                strSQL = strSQL & " where k_az=" & imageID

                dc = New OleDbCommand(strSQL, dbConn)
                imageData = CType(dc.ExecuteScalar(), Byte())

                'create an image from the byte array
                ms = New MemoryStream(imageData)
                ms1 = New MemoryStream
                fullsizeImage = System.Drawing.Image.FromStream(ms)
                nevhez = ""
                If (fullsizeImage.Width > THUMBNAIL_WIDTH) And (imageFull <> "1") Then
                    scalefact = THUMBNAIL_WIDTH / fullsizeImage.Width
                    If CDbl(imageFull) = 9 Then scalefact = 1

                    ResizeImage(scalefact, ms, ms1)
                    nevhez = " small"
                Else
                    'write thumbnail to the response object
                    fullsizeImage.Save(ms1, ImageFormat.Jpeg)
                    nevhez = " normal"
                End If

                Response.ContentType = "image/jpeg"
                nevhez = GlobalVariables.Termek & nevhez
                nevhez = nevhez & ".jpg"
                Response.AddHeader("content-disposition", "filename=" & nevhez)
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
