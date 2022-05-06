<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="QuestionnaireSystem.Index" %>

<%@ Register Src="~/ShareControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div id="srchArea" width="60%">
            問卷標題
            <asp:TextBox ID="srchKey" runat="server"></asp:TextBox>
            <br />
            開始/結束
            <asp:TextBox ID="srchBeginDateText" runat="server" TextMode="Date"></asp:TextBox>
            <asp:TextBox ID="srchEndDateText" runat="server" TextMode="Date"></asp:TextBox>
            <asp:Button ID="srchButton" runat="server" Text="搜尋" OnClick="srchButton_Click" />
            <br />
        </div>
        <br />
        <div id="ListArea">
            <%--問卷總列表--%>
            <div id="QuestionnaireList">
                <asp:Repeater ID="RptrQtnir" runat="server">
                    <HeaderTemplate>
                        <table border="1" width="60%" id="table2">
                            <tr>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>#</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>問卷</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>狀態</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>開始時間</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>結束時間</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>觀看統計</b></font></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("QuestionnaireID")%></td>
                            <td><a href="FillOutPage.aspx?QnirID=<%# Eval("QuestionnaireID")%>" target="_blank"><%# Eval("Caption")%></a></td>
                            <td><%# Eval("VoteStatus")%></td>
                            <td><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                            <td><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                            <td><a href="StatisticPage.aspx?QnirID=<%# Eval("QuestionnaireID")%>" target="_blank">前往</a></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td bgcolor="#CECECF"><%# Eval("QuestionnaireID")%></td>
                            <td bgcolor="#CECECF"><a href="FillOutPage.aspx?QnirID=<%# Eval("QuestionnaireID")%>" target="_blank"><%# Eval("Caption")%></a></td>
                            <td bgcolor="#CECECF"><%# Eval("VoteStatus")%></td>
                            <td bgcolor="#CECECF"><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                            <td bgcolor="#CECECF"><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                            <td bgcolor="#CECECF"><a href="StatisticPage.aspx?QnirID=<%# Eval("QuestionnaireID")%>" target="_blank">前往</a></td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Label ID="NoData" runat="server" Text="(查無資料)" Visible="false"></asp:Label>
                <uc1:ucPager runat="server" id="ucPager"  PageSize="10"/>

            </div>
        </div>
    </div>
</asp:Content>
