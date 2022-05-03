<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticPage.aspx.cs" Inherits="QuestionnaireSystem.StatisticPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-all">
        <!-- 上區塊 -->
        <div class="container-fluid">
            <asp:PlaceHolder ID="PlhDym" runat="server"></asp:PlaceHolder>
        </div>
        <%--統計--%>
    </div>

    <input id="currentID" name="currentID" runat="server" type="hidden" />

    <script><asp:Image runat="server"></asp:Image>
        //ajax

        //function GetStatistic() {
        //    var id = parentDiv.find("input.currentID");
        //    var postData = { "currentID": id.val()};
        //    $.ajax({
        //        url: "/API/PBoardHandler.ashx?Action=StatisticDATA",
        //        method: "POST",
        //        data: postData,
        //        dataType: "JSON",
        //        success: function (jsonText) {


        //            var rowsText = "";
        //            for (var item of jsonText) {
        //                rowsText += `
        //                <tr>
        //                      <div>
        //                          <input name="PboardID" class="PboardID" value="${item.PboardID}" type="hidden" />
        //                          <input name="PboardName" class="PboardName rounded-3  text-white" value="${item.Pname}" />
        //                          <input name="Porder" class="Porder" value="${item.Porder}"  type="hidden" />
        //                          <input type="button" id="btnPBMoveUp" class="btn-outline-primary btn-sm btnPBMoveUp rounded-pill" name="btnPBMoveUp" value="上移" />
        //                          <input type="button" id="btnPBMoveDown" class="btn-outline-primary btn-sm btnPBMoveDown rounded-pill" name="btnPBMoveDown" value="下移" />

        //                          <input type="button" id="btnPBName" class="btn-outline-success btn-sm btnPBName rounded-pill" name="btnPBName" value="儲存" />
        //                      </div>
        //                    </td>
        //                </tr>
        //                `;
        //            }
        //            $("#PBDataList_Admin1").append("<table>" + rowsText + "</table>");
        //            $("#PBDataList_Admin2").append("<table>" + rowsText + "</table>");

        //            console.log(jsonText);
        //        },
        //        error: function (msg) {
        //            console.log(msg);
        //            alert("母版塊載入失敗，請聯絡管理員。");
        //        }
        //    });

        //}

    </script>
</asp:Content>
