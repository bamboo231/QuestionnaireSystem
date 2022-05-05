<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="EditQuestionnaire.aspx.cs" Inherits="QuestionnaireSystem.admin.EditQuestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <asp:Button runat="server" ID="bookmark1" class="nav-link active" aria-selected="true" data-bs-toggle="tab" role="tab" Text="問卷" OnClick="bookmark1_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark2" class="nav-link" aria-selected="false" Text="問題" OnClick="bookmark2_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark3" class="nav-link" aria-selected="false" Text="填寫資料" OnClick="bookmark3_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark4" class="nav-link" aria-selected="false" Text="統計" OnClick="bookmark4_Click"></asp:Button>
        </div>
    </nav>
    <div id="nav-tabContent">
        <%--問卷固定問題頁--%>
        <asp:PlaceHolder ID="plhbookmark1" runat="server">

            <div>問卷名稱<asp:TextBox ID="textCaption" runat="server"></asp:TextBox></div>
            <br />
            <div>描述內容<asp:TextBox ID="textQuestionnaireContent" runat="server"></asp:TextBox></div>
            <br />
            <div>開始時間<asp:TextBox ID="textStartDate" runat="server" TextMode="Date"></asp:TextBox></div>
            <br />
            <div>結束時間<asp:TextBox ID="textEndDate" runat="server" TextMode="Date"></asp:TextBox></div>
            <br />
            <asp:CheckBox ID="ChkBxVoidStatus" runat="server" Text="已啟用" /><br />
            <asp:Button ID="btnBasicCancel" Style="margin-left: 40%;" runat="server" Text="取消" OnClick="btnBasicCancel_Click" />
            <asp:Button ID="btnBasicSummit" Style="float: right;" runat="server" Text="送出" OnClick="btnBasicSummit_Click" />
            <br />
        </asp:PlaceHolder>
        <%--撰寫問卷問題頁--%>
        <asp:PlaceHolder ID="plhbookmark2" runat="server" Visible="false">
            <%--撰寫問題--%>
            <div id="CommonDropDownList">
                種類
            </div>
            <div>
                問題<asp:TextBox ID="setQuest" runat="server"></asp:TextBox>
                <asp:DropDownList ID="setQuestForm" runat="server">
                    <asp:ListItem Value="1" Text="文字方塊"></asp:ListItem>
                    <asp:ListItem Value="2" Text="數字"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Email"></asp:ListItem>
                    <asp:ListItem Value="4" Text="日期"></asp:ListItem>
                    <asp:ListItem Value="5" Text="單選方塊"></asp:ListItem>
                    <asp:ListItem Value="6" Text="複選方塊"></asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox ID="IsRequired" runat="server" Text="必填" Checked="false" />
            </div>
            <div>
                回答<asp:TextBox ID="textSelectItem" runat="server"></asp:TextBox>(多個答案以,分隔)
            <asp:Button ID="btnAddToQuest" runat="server" Text="加入" OnClick="btnAddToQuest_Click" />
            </div>
            <br />
            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="/images/deleteIcon.png" Width="20px" OnClick="imgBtnDelete_Click" />
            <%--問題列表--%>
            <asp:Repeater ID="RptrQuest" runat="server">
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
                            <asp:Label ID="lblText" runat="server" Text='<%# Eval("QuestOrder")%>' Style="display: none;"></asp:Label>
                            <asp:CheckBox ID="A" runat="server"></asp:CheckBox>
                        </td>
                        <td><%# Eval("QuestOrder")%></td>
                        <td><%# Eval("QuestContent")%></td>
                        <td><%# Eval("strAnswerForm")%></td>
                        <td>
                            <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>'></asp:CheckBox>
                        </td>
                        <td>
                            <a href="EditQuestionnaire.aspx?UpdateOrder=<%# Eval("QuestOrder")%>&Targetplh=2">編輯</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td bgcolor="#CECECF">
                            <asp:Label ID="lblText" runat="server" Text='<%# Eval("QuestOrder")%>' Style="display: none;"></asp:Label>
                            <asp:CheckBox ID="A" runat="server"></asp:CheckBox>
                        </td>
                        <td bgcolor="#CECECF"><%# Eval("QuestOrder")%></td>
                        <td bgcolor="#CECECF"><%# Eval("QuestContent")%></td>
                        <td bgcolor="#CECECF"><%# Eval("strAnswerForm")%></td>
                        <td bgcolor="#CECECF">
                            <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>'></asp:CheckBox>
                        </td>
                        <td bgcolor="#CECECF">
                            <a href="EditQuestionnaire.aspx?UpdateOrder=<%# Eval("QuestOrder")%>&Targetplh=2">編輯</a>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


            <%--取消/送出問題--%>
            <asp:Button ID="btnQuestCancel" runat="server" Text="取消" OnClick="btnQuestCancel_Click" />
            <asp:Button ID="btnQuestSummit" runat="server" Text="送出" OnClick="btnQuestSummit_Click" />
        </asp:PlaceHolder>

        <%--填寫資料--%>
        <asp:PlaceHolder ID="plhbookmark3" runat="server" Visible="false">
            <div>
                <asp:Button ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click" />
            </div>
            <%--繳回的列表--%>
            <asp:PlaceHolder ID="plhdoneList" runat="server">
                <asp:Repeater ID="RptrAnswerList" runat="server">
                    <HeaderTemplate>
                        <table border="1" width="90%" id="table2">
                            <tr>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>#</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>姓名</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>填寫時間</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>觀看細節</b></font></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("BasicAnswerID")%></td>
                            <td><%# Eval("Nickname")%></td>
                            <td><%# Eval("AnswerDate", "{0:yyyy/MM/dd HH:mm:ss}")%></td>
                            <td><a href="EditQuestionnaire.aspx?QstnirID=<%# Eval("QuestionnaireID")%>&BsicAnsID=<%# Eval("BasicAnswerID")%>&Targetplh=3">前往</a></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td bgcolor="#CECECF"><%# Eval("BasicAnswerID")%></td>
                            <td bgcolor="#CECECF"><%# Eval("Nickname")%></td>
                            <td bgcolor="#CECECF"><%# Eval("AnswerDate", "{0:yyyy/MM/dd HH:mm:ss}")%></td>
                            <td bgcolor="#CECECF"><a href="EditQuestionnaire.aspx?QstnirID=<%# Eval("QuestionnaireID")%>&BsicAnsID=<%# Eval("BasicAnswerID")%>&Targetplh=3">前往</a></td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div id="NoneDATA" runat="server" visible="false">(查無資料)</div>
                (分頁待補)

            </asp:PlaceHolder>

            <%--繳回的詳細內容--%>
            <asp:PlaceHolder ID="plhDoneDetail" runat="server" Visible="false">

                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="姓名"></asp:Label></td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="手機"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="doneName" runat="server" Text='<%# Eval("Nickname")%>' Enabled="false"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="donePhone" runat="server" Text='<%# Eval("Phone")%>' Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label></td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="年齡"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="doneEmail" runat="server" Text='<%# Eval("email")%>' Enabled="false"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="doneAge" runat="server" Text='<%# Eval("Age")%>' Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="填寫時間"></asp:Label>
                            <asp:Label ID="AnswerDate" runat="server" Text='<%# Eval("AnswerDate", "{0:yyyy/MM/dd HH:mm:ss}")%>'></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="plhDynDetail" runat="server"></asp:PlaceHolder>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
        <%--統計--%>
        <asp:PlaceHolder ID="plhbookmark4" runat="server" Visible="false"></asp:PlaceHolder>
        <asp:Label ID="NAStatistic" runat="server" Text="(查無資料)" Visible="false"></asp:Label>
    </div>


    <script>
        $(document).ready(function () {
            //取得常用問題
            GetCommon();

            $("#CommonDropDownList").on('click', "option[class*=mdl-menu__item]", function () {
                var parentDiv = $(this).closest("div");
                var item = parentDiv.find("select.setQuestType");
                var selection = { "QuestContent": item.val() };

                $.ajax({
                    url: "/API/CommonHandler.ashx?Action=ChangeSelect",
                    method: "POST",
                    data: selection,
                    dataType: "text",
                    success: function (jsonText) {
                        console.log(jsonText);
                        jsonText = jsonText.replace("\\", "");
                        var jsonObj = JSON.parse(jsonText);
                        for (var item of jsonObj) {
                            var txtQuestContent = `${item.QuestContent}`;
                            var strAnswerForm = `${item.AnswerForm}`;
                            var intAnswerForm = Number(strAnswerForm);
                            var isRequired = item.Required;
                            var txtQuestSelectItem = `${item.SelectItem}`;

                            $("#ContentPlaceHolder1_setQuest").val(txtQuestContent);//問題題目描述
                            $("#ContentPlaceHolder1_IsRequired").prop('checked', isRequired);//是否必填

                            if (item.SelectItem != null)
                                $("#ContentPlaceHolder1_textSelectItem").val(txtQuestSelectItem);

                            var osel = $("#ContentPlaceHolder1_setQuestForm"); //得到select的ID
                            var opts = osel.val(intAnswerForm).selected = true;//得到陣列option
                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。")
                    }
                });

            });


        });
        function GetCommon() {
            $.ajax({
                url: "/API/CommonHandler.ashx",
                method: "GET",
                dataType: "JSON",
                success: function (jsonText) {
                    var rowsText = "";
                    rowsText = `<select id="setQuestType" class="setQuestType" name="setQuestType" width="60%">`;
                    for (var item of jsonText) {
                        rowsText += `<option class="mdl-menu__item " value="${item.QuestContent}">${item.QuestContent}</option> `;
                    }
                    rowsText += `</select >`;

                    $("#CommonDropDownList").append(rowsText);

                    console.log(jsonText);
                },
                error: function (msg) {
                    console.log(msg);
                    alert("常用問題載入失敗，請聯絡管理員。");
                }
            });

        }

    </script>
</asp:Content>
