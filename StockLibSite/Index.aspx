<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Refresh" runat="server" Text="刷新" />
        </div>
        <div>
            <asp:Repeater ID="Result" runat="server">
                <HeaderTemplate>

                </HeaderTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>

                </FooterTemplate>
            </asp:Repeater>

        </div>
    </form>
</body>
</html>
