<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="CommonQuestPage.aspx.cs" Inherits="QuestionnaireSystem.admin.CommonQuestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--撰寫問題--%>
    <div>
        問題<asp:TextBox ID="setQuest" runat="server"></asp:TextBox>
        <asp:DropDownList ID="setQuestForm" runat="server">
            <asp:ListItem>文字方塊</asp:ListItem>
            <asp:ListItem>數字</asp:ListItem>
            <asp:ListItem>Email</asp:ListItem>
            <asp:ListItem>日期</asp:ListItem>
            <asp:ListItem>單選方塊</asp:ListItem>
            <asp:ListItem>複選方塊</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="IsRequired" runat="server" Text="必填" />
    </div>
    <div>
        回答<asp:TextBox ID="textSelectItem" runat="server"></asp:TextBox>(多個答案以「;」分隔)
            <asp:Button ID="btnAddToQuest" runat="server" Text="加入" OnClick="btnAddToQuest_Click" />
    </div>
    <br />
    <div>
        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="/images/deleteIcon.png" Width="20px" OnClick="imgBtnDelete_Click" />
    </div>
    <%--問題列表--%>
    <asp:Repeater ID="RptrCommonQuest" runat="server">
        <HeaderTemplate>
            <table border="1" width="90%" id="table2">
                <tr>
                    <td bgcolor="#000000"><font color="#FFFFFF">- </font></td>
                    <td bgcolor="#000000"><font color="#FFFFFF"><b>#</b></font></td>
                    <td bgcolor="#000000"><font color="#FFFFFF"><b>問題</b></font></td>
                    <td bgcolor="#000000"><font color="#FFFFFF"><b>種類</b></font></td>
                    <td bgcolor="#000000"><font color="#FFFFFF"><b>必須</b></font></td>
                    <td bgcolor="#000000"><font color="#FFFFFF">- </font></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="tbxTableName" runat="server" Text='<%# Eval("CommonQuestID")%>' Style="display: none;" />
                    <%--<input type="checkbox" value='<%#Eval("CommonQuestID")%>' id='chkBxQuest<%# Eval("CommonQuestID")%>'/>--%>
                    <asp:CheckBox ID="chkBxQuest" runat="server" />
                </td>
                <td><%# Eval("CommonQuestID")%></td>
                <td><%# Eval("QuestContent")%></td>
                <td><%# Eval("strAnswerForm")%></td>
                <td>
                    <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>' Enabled="false"/>
                </td>
                <td>
                    <a href="CommonQuestPage.aspx?CommonQuestID=<%# Eval("CommonQuestID")%>">編輯</a>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td bgcolor="#CECECF">
                    <asp:TextBox ID="tbxTableName" runat="server" Text='<%# Eval("CommonQuestID")%>' Style="display: none;" />
                    <%--<input type="checkbox" value='<%#Eval("CommonQuestID")%>' id='chkBxQuest<%# Eval("CommonQuestID")%>'/>--%>
                    <asp:CheckBox ID="chkBxQuest" runat="server" Checked="false"/>
                </td>
                <td bgcolor="#CECECF"><%# Eval("CommonQuestID")%></td>
                <td bgcolor="#CECECF"><%# Eval("QuestContent")%></td>
                <td bgcolor="#CECECF"><%# Eval("strAnswerForm")%></td>
                <td bgcolor="#CECECF">
                    <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>' Enabled="false" />
                </td>
                <td bgcolor="#CECECF">
                    <a href="CommonQuestPage.aspx?CommonQuestID=<%# Eval("CommonQuestID")%>">編輯</a>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <%--取消/送出問題--%>
    <div>
        <asp:Button ID="btnEditsOver" runat="server" Text="結束編輯" OnClick="btnEditsOver_Click" />
    </div>
</asp:Content>
