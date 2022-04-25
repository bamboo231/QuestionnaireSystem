<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="MyQuestionnaire.aspx.cs" Inherits="QuestionnaireSystem.admin.MyQuestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="srchArea">
        問卷標題
            <asp:TextBox ID="srchQuestionnaire" runat="server"></asp:TextBox>
        <br />
        開始/結束
            <asp:TextBox ID="srchBeginDateText" runat="server"></asp:TextBox>
           <%-- <asp:Calendar ID="srchBeginDateCalendar" runat="server"></asp:Calendar>--%>
            <asp:TextBox ID="srchEndDateText" runat="server"></asp:TextBox>
  <%--      <asp:Calendar ID="srchEendDate" runat="server"></asp:Calendar>--%>
        <asp:Button ID="srchBotton" runat="server" Text="搜尋" />
        <br />
    </div>
    <div id="ListArea">
        <%--問卷總列表--%>
        <div id="QuestionnaireList">
            <asp:Repeater ID="RptrQtnir" runat="server">
                <HeaderTemplate>
                    <table border="1" width="90%" id="table2">
                        <tr>
                            <td bgcolor="#000000"><font color="#FFFFFF">- </font></td>
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
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                        <td><%# Eval("QuestionnaireID")%></td>
                        <td><a href="page.aspx?QuestionnaireID=link" target="_blank"><%# Eval("Caption")%></a></td>
                        <td><%# Eval("VoidStatus")%></td>
                        <td><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                        <td><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                        <td><a href="page.aspx?QuestionnaireID=link" target="_blank">前往</a></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td bgcolor="#CECECF">
                            <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                        <td bgcolor="#CECECF"><%# Eval("QuestionnaireID")%></td>
                        <td bgcolor="#CECECF"><a href="page.aspx?QuestionnaireID=link" target="_blank"><%# Eval("Caption")%></a></td>
                        <td bgcolor="#CECECF"><%# Eval("VoidStatus")%></td>
                        <td bgcolor="#CECECF"><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                        <td bgcolor="#CECECF"><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                        <td bgcolor="#CECECF"><a href="/admin/page.aspx?QuestionnaireID=<%# Eval("QuestionnaireID")%>" target="_blank">前往</a></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


        </div>
    </div>
</asp:Content>
