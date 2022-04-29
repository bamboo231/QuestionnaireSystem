<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FillOutPage.aspx.cs" Inherits="QuestionnaireSystem.FillOutPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="plhDoneDetail" runat="server" Visible="false">

        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="姓名"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="doneName" runat="server" Text="data" ></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="手機"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="donePhone" runat="server" Text="data" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="doneEmail" runat="server" Text="data" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="年齡"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="doneAge" runat="server" Text="data"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Content>
