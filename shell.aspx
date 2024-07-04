<%@ Page Title="Shell | STT - GAT Link" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="shell.aspx.cs" Inherits="shell" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>


    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Shell </h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Shell</li>
            </ol>
        </div>
    </div>

    <!-- CORPO -->
    <div id="main-wrapper">

        <!-- FORMULARIO -->
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-white ui-sortable-handle" style="opacity: 1;">

                    <!-- corpo -->
                    <div class="panel-body" style="display: block;">

                        <!-- adm -->
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">GAT Link Administrador:</label>

                                <div class="col-sm-8">
                                    <div class="m-b-sm">
                                        <asp:TextBox ID="txtadm" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <asp:LinkButton ID="cmdsalvar_shell_adm" OnClick="cmdsalvar_shell_adm_Click" runat="server">
                                <button type="button" class="btn btn-info"><i class="fa fa-save"></i>
                                </button>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <!-- FRONT END -->
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">GAT Link Front End:</label>
                                <div class="col-sm-8">
                                    <div class="m-b-sm">
                                        <asp:TextBox ID="txtfe" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="cmdsalvar_shell_fe" OnClick="cmdsalvar_shell_fe_Click" runat="server">
                                        <button type="button" class="btn btn-info"><i class="fa fa-save"></i>
                                        </button>
                                    </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>


    </div>

</asp:Content>

