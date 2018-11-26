
Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Login1.Focus()
        ReDim GlobalVariables.azarray(999)
        GlobalVariables.ix = 0
    End Sub
End Class
