<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ManifestList.aspx.cs" Inherits="TransitCamp.Admin.ManifestList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <section class="d-flex align-items-center my-5 mt-lg-6 mb-lg-5">
            <div class="container">
                <h3 class="text-center text-md-center mb-4 mt-md-0">Manifest Lists</h3>
                <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="upSide" runat="server">
                    <ContentTemplate>
                        <div id="divRptrADNo" runat="server" class="mr-2 w-25 bg-white row float-left form-bg-image d-flex align-items-center mb-4 mb-lg-0 bg-white shadow-soft border rounded border-light">
                            <div class="">
                                <h1 class="mb-0 h5 text-center text-md-center mt-3 mb-1">Manifest No</h1>
                                <input name="myinput" type="text" id="myUlInput" onkeyup="myFunctionPriority()" class="form-control" placeholder="Search">
                                <div class="mt-1 mb-1"></div>
                                <div class="mb-1 background-search" style="height: 120px  !important">
                                    <ul id="myUL">
                                        <asp:Repeater runat="server" ID="rptManifest">
                                            <ItemTemplate>
                                                <li class="li-list float-left"><a onclick="CallMethod()" class="search-list float-left" id="<%# Eval("MenifestNo") %>">
                                                    <asp:HiddenField runat="server" ID="hf" Value='<%# Eval("MenifestNo") %>' />
                                                    <div class="ml-3 pl-2 float-left"><%# Eval("MenifestNo") %></div>
                                                </a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="table-container mt-1">
                        <div class="table-wrp">
                            <div class="table-responsive table-grid float-left w-100">
                                <asp:UpdatePanel runat="server" ID="upGrd">
                                    <ContentTemplate>
                                        <asp:GridView runat="server" ID="grdManifest" CssClass="table table-striped border-none table-grid-one rounded" Visible="true" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Armny No">
                                                    <ItemTemplate>
                                                        <%#Eval("ArmyNo") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank">
                                                    <ItemTemplate>
                                                        <%#Eval("Rank") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <%#Eval("Name") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <%#Eval("UnitName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FMN">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AD NO">
                                                    <ItemTemplate>
                                                        <%#Eval("ADNO") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ac Used In Calendar YR">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data Found
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJavaScripts" runat="server">
    <script type='text/javascript'>
        var id;
        $("#myUL").children('li').click(function () {
            id = $(this).children('a').attr('id');
            CallMethod();
        });
        function CallMethod() {
            $.ajax({
                type: "POST",
                url: "Admin/manifestlist.aspx/aClick",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'ManifestNo':'" + id + "'}",
                success: function (data) {
                    if (data != null) {
                        var removerow = $("#ContentPlaceHolder1_grdManifest > tr");
                        if (removerow != null) {
                            $("#ContentPlaceHolder1_grdManifest tr").remove();
                        }
                        $("#ContentPlaceHolder1_grdManifest").append("<tr><td>Name</td><td>ICard</td><td>Unit</td><td>Rank</td><td>Category</td><td>Transport No</td><td>City</td></tr>");

                        for (var i = 0; i < data.length; i++) {
                            $("#ContentPlaceHolder1_grdManifest").append("<tr><td>" + data[i].Name + "</td><td>" + data[i].ICard + "</td><td>" + data[i].UnitName + "</td><td>" + data[i].Rank + "</td><td>" + data[i].CategoryName + "</td><td>" + data[i].TransportTypeName + "</td><td>" + data[i].CityName + "</td></tr>");
                        }
                    }
                },
                failure: function (data) {
                    alert("fail");
                }
            });
        }
    </script>
    <script>
        function myFunctionPriority() {
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById("myUlInput");
            filter = input.value.toUpperCase();
            ul = document.getElementById("myUL");
            li = ul.getElementsByTagName("li");
            for (i = 0; i < li.length; i++) {
                a = li[i].getElementsByTagName("a")[0];
                txtValue = a.textContent || a.innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }
            }
        }
    </script>
</asp:Content>
