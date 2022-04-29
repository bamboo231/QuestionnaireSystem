<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticPage.aspx.cs" Inherits="QuestionnaireSystem.StatisticPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-all">
        <!-- 上區塊 -->
        <div class="container-fluid">
            <asp:Label ID="pageTitle" runat="server" style="align-items:center;" Text="標題"></asp:Label>
        </div>

                <%--統計--%>
        <asp:PlaceHolder ID="plhbookmark4" runat="server" Visible="false"></asp:PlaceHolder>
    </div>

</asp:Content>
