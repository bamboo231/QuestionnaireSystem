<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticPage.aspx.cs" Inherits="QuestionnaireSystem.StatisticPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #tbQstnir{
            text-align:center;
            width:90%;
            margin:12px;
        }
        #PlhDym
        {
            margin:10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="position-absolute top-0 end-0">
        <asp:Label ID="IsVoting" class="position-absolute top-0 end-0" runat="server" Text="已結束"></asp:Label>
        <br />
        <asp:Label ID="Period" runat="server" Text="2021/9/9~2022/5/5"></asp:Label>
        <br />
    </div>
    <div class="container-all">

        <table id="tbQstnir">
            <tr>
                <td>
                    <asp:Label ID="Caption" Style="font-size: 22px; text-align: center; line-height: 80px;" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <%--統計--%>
        <div class="container-fluid">
            <asp:PlaceHolder ID="PlhDym" runat="server"></asp:PlaceHolder>
        </div>
        <asp:Label ID="NAStatistic" runat="server" Text="(查無資料)" Visible="false"></asp:Label>

    </div>

    <input id="currentID" name="currentID" runat="server" type="hidden" />

    <asp:Image runat="server"></asp:Image>
    <script>
</script>
</asp:Content>
