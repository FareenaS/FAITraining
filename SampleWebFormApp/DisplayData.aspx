<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayData.aspx.cs" Inherits="SampleWebFormApp.DisplayData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rpProducts" runat="server" OnItemCommand="rpProducts_ItemCommand">
                <HeaderTemplate>
                    <div>
                        <h1>List of Products with Us!!!</h1>
                        <hr />
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="width:65%; display:inline-block">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image Width="250px" Height="200px" ImageUrl='<%#Eval("Image") %>' runat="server" />
                                </td>
                                <td style="vertical-align:top">
                                    <div style="border:2px solid lightBlue">

                                    <h2>
                                        <asp:Label ID="lblTitle" Text='<%#Eval("ProductName") %>' runat="server" />
                                    </h2>
                                    <hr /> 
                                    <div>
                                        Rs. <asp:Label ID="lblPrice" Text='<%#Eval("Price") %>' runat="server" />
                                        <p>

    <asp:Button Text="Add to Cart" CommandName="addToCart" CommandArgument='<%#Eval("ProductId") %>'  runat="server" />
                                        </p>
                                    </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
                
            </asp:Repeater>
        </div>

        <div style="width: 70%">
            <hr />
            <asp:DataList runat="server" Width="1613px" ID="grdCart" RepeatColumns="4" >
                <ItemTemplate>
                    <div style="padding:5px; margin:3px; border:2px double blue">
                        <h2><%#Eval("ProductName") %></h2>
                        <hr />
                        <div>
                            <p>
                                <asp:Image Width="50px" Height="100px" ImageUrl='<%#Eval("Image") %>' runat="server" />
                            </p>
                            <p>Price: <%#Eval("Price")%></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
