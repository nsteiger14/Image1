<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Query1.aspx.vb" Inherits="_Default" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta name="SKYPE_TOOLBAR" content ="SKYPE_TOOLBAR_PARSER_COMPATIBLE"/>
<head runat="server">
    <title>Herend products</title>
    <style type="text/css">
        .style1
        {
            height: 39px;
        }
        .style2
        {
            height: 154px;
            width: 295px;
        }
        .style3
        {
            width: 20%;
        }
    </style>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function imgBook_onclick() {
           //s1 = "http:Thumb1.aspx?ImageID=" & Cstr(kaz1) & "&imageFull=9"
            //s1 = "http:Thumb1.aspx?ImageID=165452&imageFull=9"
            //Textbox5.Text = s1
            //javascript: window.open(s1);
            //            javascript: window.open("http:Default.aspx");
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <table cellpadding="0" style="height: 430px" width="1050">
                <tr>
                    <td colspan="3" style="height: 21px; background-color: #990000;" valign="top">
                        <asp:Label ID="Label1" runat="server" Text="Form" ForeColor="#FF9900" Font-Names="verdana"
                            Font-Size="X-Small"></asp:Label>
                        <asp:TextBox ID="TextForm" runat="server" Font-Names="verdana" 
                            Font-Size="X-Small"></asp:TextBox>&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Decor" ForeColor="#FF9900" Font-Names="verdana"
                            Font-Size="X-Small"></asp:Label>
                        <asp:TextBox ID="TextDeko" runat="server" Font-Names="verdana" Font-Size="X-Small"></asp:TextBox>&nbsp;<asp:Label 
                            ID="Label3" runat="server" Text="Name" ForeColor="#FF9900" Font-Names="verdana"
                            Font-Size="X-Small"></asp:Label>
&nbsp;<asp:TextBox ID="TextName" runat="server" Width="257px" Font-Names="verdana" Font-Size="X-Small"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="Query" Width="69px" Font-Names="verdana"
                            Font-Size="X-Small" />
                        <asp:TextBox ID="TextKaz" runat="server" Width="86px" Font-Names="verdana" 
                            Font-Size="X-Small"></asp:TextBox>
                        <asp:LoginStatus ID="LoginStatus1" runat="server" ForeColor="#FF9900" 
                            Font-Bold="True" Font-Names="verdana" Font-Size="XX-Small" 
                            LogoutAction="Redirect" LogoutPageUrl="~/Default.aspx" />
                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="verdana" Font-Size="X-Small"
                            ForeColor="#FF9900" NavigateUrl="~/ChangePassword.aspx">Change password</asp:HyperLink>
                        <asp:TextBox ID="LastQuery" runat="server" Font-Names="verdana" 
                            Font-Size="X-Small" ReadOnly="True" 
                            Width="42px" Visible="False"></asp:TextBox>
                        <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
                        <asp:Button ID="ButtonNextkep" runat="server" Font-Names="verdana" Font-Size="X-Small" Text="Next" Visible="False" />
                        <asp:Button ID="ButtonDownload" runat="server" Font-Names="verdana" Font-Size="X-Small" Text="Download" />
                        <asp:Button ID="ButtonAlldown" runat="server" Text="Download all" Font-Names="Verdana" Font-Size="X-Small" />
                        <br />
                        <asp:CheckBox ID="CheckByMinta" runat="server" Font-Names="verdana" 
                            Font-Size="X-Small" ForeColor="#FF9900" Text="By decor" TextAlign="Left" />
                        <asp:Label ID="LabelCust" runat="server" Font-Names="verdana" 
                            Font-Size="X-Small" ForeColor="#FF9900" Text="Customer"></asp:Label>
                        <asp:TextBox ID="TextVevo" runat="server" Font-Names="verdana" 
                            Font-Size="X-Small" BackColor="#FF9900" BorderStyle="None" 
                            ForeColor="#990000"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" ForeColor="#990000" Text="Q"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="X-Small" 
                            ForeColor="#FF9900" Text="Name language"></asp:Label>
                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                            Checked="True" Font-Names="verdana" Font-Size="X-Small" ForeColor="White" 
                            GroupName="a1" Text="English" ValidationGroup="1" />
                        <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" 
                            Font-Names="Verdana" Font-Size="X-Small" ForeColor="White" GroupName="a1" 
                            Text="German" ValidationGroup="1" />
                        <asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="True" 
                            Font-Names="Verdana" Font-Size="X-Small" ForeColor="White" GroupName="a1" 
                            Text="French" ValidationGroup="1" />
                        <asp:RadioButton ID="RadioButton4" runat="server" AutoPostBack="True" 
                            Font-Names="Verdana" Font-Size="X-Small" ForeColor="White" GroupName="a1" 
                            Text="Hungarian" ValidationGroup="1" />
                        <asp:TextBox ID="TextLang" runat="server" Font-Names="verdana" 
                            Font-Size="XX-Small" Visible="False" Width="17px" Wrap="False">E</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style3">
                        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowSorting="True"
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                            DataKeyNames="suma1,forma,telso" Width="300px" Font-Names="verdana" Font-Size="X-Small"
                            EmptyDataText="0 row" AllowPaging="True" PageSize="25">
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="forma" HeaderText="Form" SortExpression="forma">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="60px" Font-Underline="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="name_en" HeaderText="Name" 
                                    SortExpression="name_en" />
                                <asp:BoundField DataField="a2" HeaderText="Dec." SortExpression="a2">
                                <ItemStyle HorizontalAlign="Right" Width="15px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="a1" HeaderText="Pics" ReadOnly="True" 
                                    SortExpression="a1">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="25px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="suma1" HeaderText="suma1" SortExpression="suma1" 
                                    Visible="False" />
                                <asp:BoundField DataField="telso" HeaderText="telso" SortExpression="telso" 
                                    Visible="False" />
                            </Columns>
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetLista1" 
                            TypeName="KepekTableAdapters.getlista1adapter">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="TextForm" Name="t1" PropertyName="Text" Type="String"
                                    DefaultValue=" " />
                                <asp:ControlParameter ControlID="TextDeko" Name="d1" PropertyName="Text" Type="String"
                                    DefaultValue=" " />
                                <asp:ControlParameter ControlID="TextName" DefaultValue="" Name="n1" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="TextLang" DefaultValue="" Name="l1" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="TextVevo" DefaultValue="" Name="vevokod" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="CheckByMinta" DefaultValue="0" Name="byminta" 
                                    PropertyName="Checked" Type="Boolean" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        &nbsp;&nbsp;
                    </td>
                    <td valign="top" class="style2">
                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" DataSourceID="Obj3" Font-Names="Verdana" 
                            Font-Size="X-Small" AllowSorting="True" DataKeyNames="suma1,dekor" 
                            Width="125px" PageSize="25" ShowFooter="True">
                            <Columns>
                                <asp:BoundField DataField="dekor" HeaderText="Decor" SortExpression="dekor">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="100px" Font-Underline="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="name_en" HeaderText="Name" 
                                    SortExpression="name_en" Visible="False" />
                                <asp:BoundField DataField="a1" HeaderText="Pics" ReadOnly="True" 
                                    SortExpression="a1">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="25px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="suma1" HeaderText="suma1" SortExpression="suma1" 
                                    Visible="False" />
                            </Columns>
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <FooterStyle BackColor="#990000" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="Obj3" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetLista2" 
                            TypeName="KepekTableAdapters.getlista2adapter" CacheDuration="1">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="TextBox2" DefaultValue="" Name="t1" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="TextDeko" DefaultValue="" Name="d1" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="TextName" DefaultValue="" Name="n1" 
                                    PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="TextLang" Name="l1" PropertyName="Text" 
                                    Type="String" />
                                <asp:ControlParameter ControlID="TextVevo" Name="vevokod" PropertyName="Text" 
                                    Type="String" />
                                <asp:ControlParameter ControlID="CheckByMinta" DefaultValue="0" Name="byminta" 
                                    PropertyName="Checked" Type="Boolean" />
                                <asp:ControlParameter ControlID="TextForm" Name="t2" PropertyName="Text" 
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td style="width: 233px; height: 145px" valign="top">
                        <asp:DataList ID="dlImages" runat="server" BackColor="#FFFBD6" DataKeyField="k_az"
                            OnItemDataBound="dlImages_ItemDataBound" RepeatColumns="5" RepeatDirection="Horizontal"
                            SelectedItemStyle-BackColor="#FFCC66" Width="760px" Font-Names="verdana" 
                            Font-Size="X-Small" style="table-layout:fixed">
                            <ItemTemplate>
                                <table cellpadding="0" style="height: 5px; font-size: x-small; font-family: Verdana;">
                                    <tr>
                                        <td valign="top">
                                        </td>
                                        <asp:Label ID="LabelX1"  width= "152px" runat="server" CommandName="Select" 
                                            Style="color: #990000" Text='<%# DataBinder.Eval(Container.DataItem, "exc") %>' BackColor="Red" ForeColor="Yellow"></asp:Label>
                                        <asp:LinkButton ID="Linkbutton1"  width= "152px" runat="server" CommandName="Select" OnClick="Linkbutton1_Click"
                                            Style="color: #990000" Text='<%# DataBinder.Eval(Container.DataItem, "termek") %>'></asp:LinkButton><br>
                                        <span style="font-size: 0.15em">
                                            <br>
                                        </span>
                                        <img id="imgThumbnail" runat="server" alt="image" border="0" src="" />&nbsp; 
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <SelectedItemTemplate>
                                <table cellpadding="0" style="height: 5px; font-size: x-small; font-family: Verdana;">
                                    <tr>
                                        <td valign="top">
                                        </td>
                                        <asp:Label ID="LabelX2"  width= "152px" runat="server" CommandName="Select" 
                                            Style="color: #990000" Text='<%# DataBinder.Eval(Container.DataItem, "exc") %> ' BackColor="Red" ForeColor="Yellow"></asp:Label>
                                        <asp:LinkButton ID="Linkbutton1" width= "152px" runat="server" CommandName="Select" OnClick="Linkbutton1_Click"
                                            Style="color: #990000" Text='<%# DataBinder.Eval(Container.DataItem, "termek") %>'></asp:LinkButton><br>
                                        <span style="font-size: 0.15em">
                                            <br>
                                        </span>
                                        <img id="imgThumbnail" runat="server" alt="image" border="0" src="" />&nbsp; 
                                    </tr>
                                </table>
                            </SelectedItemTemplate>
                            <SelectedItemStyle BackColor="#FFCC66" />
                            <SeparatorStyle BorderStyle="None" />
                        </asp:DataList><asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                            CellPadding="2" DataSourceID="ObjectDataSource2" ForeColor="#333333" 
                            GridLines="None" Font-Names="verdana" Font-Size="XX-Small" Width="760px">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:BoundField DataField="maktx" HeaderText="Name" HtmlEncode="False" SortExpression="maktx">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="320px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="suly" HeaderText="Weight (gr)" HtmlEncode="False" SortExpression="suly">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="magassag" HeaderText="Height (mm)" HtmlEncode="False"
                                    SortExpression="magassag">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hosszusag" HeaderText="Length (mm)" HtmlEncode="False"
                                    SortExpression="hosszusag">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="szelesseg" HeaderText="Width (mm)" HtmlEncode="False"
                                    SortExpression="szelesseg">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="atmero" HeaderText="Diameter (mm)" HtmlEncode="False"
                                    SortExpression="atmero">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="talpatmero" HeaderText="Base dm. (mm)" SortExpression="talpatmero">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="urtartalom" HeaderText="Volume (cm3)" HtmlEncode="False"
                                    SortExpression="urtartalom">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <RowStyle BackColor="#E3EAEB" Font-Names="verdana" Font-Size="XX-Small" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" Font-Names="verdana" Font-Size="XX-Small" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Names="verdana" Font-Size="XX-Small" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetProdData" TypeName="KepekTableAdapters.getprodnameTableAdapter">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="TextKaz" Name="k_az" PropertyName="Text" Type="Int32" />
                                <asp:ControlParameter ControlID="TextLang" Name="l1" PropertyName="Text" 
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        &nbsp;<asp:HyperLink ID="HyperLinkNormal" runat="server" Font-Size="X-Small" 
                            Target="_blank">Normal image</asp:HyperLink>
                        &nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLinkBest" runat="server" Font-Size="X-Small" 
                            Target="_blank">Best image</asp:HyperLink>
                        <br />
                        <table border="0" width="100%" style="height: 21px">
                            <tr>
                                <td id="tdSelectedImage" runat="server" align="left" class="style1" colspan="1" 
                                    valign="top">
                                    <img id="imgBook" runat="server" alt="book" border="0" visible="false" src=""
                                        onclick="return imgBook_onclick()" title="" />&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
            </table>
            <asp:TextBox ID="TextBox2" runat="server" Width="101px" Visible="False"></asp:TextBox>
            <asp:TextBox
                ID="TextBox1" runat="server" Width="87px" Visible="False"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tx1" runat="server" Visible="False" 
                Width="20px"></asp:TextBox>
            <asp:TextBox ID="Tx2" runat="server" Visible="False" Width="20px"></asp:TextBox>
            <asp:TextBox ID="Tx3" runat="server" Visible="False" Width="20px"></asp:TextBox>
            <asp:TextBox ID="Td1" runat="server" Visible="False" Width="20px"></asp:TextBox>
            <asp:TextBox ID="Td2" runat="server" Visible="False" Width="20px"></asp:TextBox>
            <asp:TextBox ID="Td3" runat="server" Visible="False" Width="20px"></asp:TextBox>
            <asp:TextBox ID="Td4" runat="server" Visible="False" Width="20px"></asp:TextBox>
            <br />
    </form>
</body>
</html>
