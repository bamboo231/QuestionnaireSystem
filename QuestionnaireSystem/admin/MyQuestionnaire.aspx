<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="MyQuestionnaire.aspx.cs" Inherits="QuestionnaireSystem.admin.MyQuestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="srchArea">
        問卷標題
            <asp:TextBox ID="srchKey" runat="server"></asp:TextBox>
        <br />
        開始/結束
            <asp:TextBox ID="srchBeginDateText" runat="server" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="srchEndDateText" runat="server" TextMode="Date"></asp:TextBox>
        <asp:Button ID="srchButton" runat="server" Text="搜尋" OnClick="srchButton_Click" />
        <br />
    </div>
    <div>
        <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="/images/deleteIcon.png" Width="20px" OnClick="ImageDelete_Click" />
        <asp:ImageButton ID="ImageAdd" runat="server" ImageUrl="/images/addIcon.png" Width="20px" OnClick="ImageAdd_Click" />
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
                            <asp:TextBox ID="tbxTableName" runat="server" Text='<%# Eval("QuestionnaireID")%>' Style="display: none;" />
                            <asp:CheckBox ID="ChkBxQnir" runat="server" value='<%# Eval("QuestionnaireID")%>' />
                        </td>
                        <td><%# Eval("QuestionnaireID")%></td>
                        <td><a href="EditQuestionnaire.aspx?QstnirID=<%# Eval("QuestionnaireID")%>&Targetplh=1" target="_blank"><%# Eval("Caption")%></a></td>
                        <td><%# Eval("VoidStatus")%></td>
                        <td><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                        <td><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                        <td><a href="EditQuestionnaire.aspx?QstnirID=<%# Eval("QuestionnaireID")%>&Targetplh=4" target="_blank">前往</a></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td bgcolor="#CECECF">
                            <asp:TextBox ID="tbxTableName" runat="server" Text='<%# Eval("QuestionnaireID")%>' Style="display: none;" />
                            <asp:CheckBox ID="ChkBxQnir" runat="server" value='<%# Eval("QuestionnaireID")%>' />
                        </td>
                        <td bgcolor="#CECECF"><%# Eval("QuestionnaireID")%></td>
                        <td bgcolor="#CECECF"><a href="EditQuestionnaire.aspx?QstnirID=<%# Eval("QuestionnaireID")%>&Targetplh=1" target="_blank"><%# Eval("Caption")%></a></td>
                        <td bgcolor="#CECECF"><%# Eval("VoidStatus")%></td>
                        <td bgcolor="#CECECF"><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                        <td bgcolor="#CECECF"><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                        <td bgcolor="#CECECF"><a href="EditQuestionnaire.aspx?QstnirID=<%# Eval("QuestionnaireID")%>&Targetplh=4" target="_blank">前往</a></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="Label1" runat="server" Text="(查無資料)" Visible="false"></asp:Label>

        </div>
    </div>
</asp:Content>
