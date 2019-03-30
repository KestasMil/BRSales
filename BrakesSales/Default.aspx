<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BrakesSales.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales</title>
    <link rel="stylesheet" href="Scripts/tablesorter-master/dist/css/theme.blue.css" />
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script src="Scripts/tablesorter-master/dist/js/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="Scripts/tablesorter-master/dist/js/jquery.tablesorter.widgets.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#GridView1").tablesorter({
                theme: 'blue',
                widgets: ["zebra", "filter"]
            });
        });
    </script>
    <style type="text/css">
        .info_row {
            height: 20px;
        }

            .info_row div {
                float: left;
            }

        .label {
            color: #1C5E55;
            font-weight: bold;
            margin-right: 10px;
        }

        .content {
            color: #1C5E55;
        }

        .info_section {
            background-color: #E3EAEB;
            display: inline-block;
            padding: 20px;
            border: 3px solid #1c5e55;
            border-radius: 20px;
        }
    </style>
    <form id="form1" runat="server">
        <div>
            <br />
            <div class="info_section">
                <div class="info_row">
                    <div class="label">Date Updated:</div>
                    <div class="content" id="Con1" runat="server">Some Content</div>
                </div>
                <div class="info_row">
                    <div class="label">Products on sale:</div>
                    <div class="content" id="Con2" runat="server">Some Content</div>
                </div>
                <div class="info_row">
                    <div class="label">New products today:</div>
                    <div class="content" id="Con3" runat="server">Some Content</div>
                </div>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                <Columns>
                    <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description 1" SortExpression="Description" />
                    <asp:BoundField DataField="CompareTheMarket" HeaderText="Description 2" SortExpression="CompareTheMarket" />
                    <asp:BoundField DataField="Temp" HeaderText="Temp" SortExpression="Temp" />
                    <asp:BoundField DataField="SalePrice" DataFormatString="{0:c}" HeaderText="Price" SortExpression="SalePrice" />
                    <asp:BoundField DataField="BBE_Date" HeaderText="BBE Date" SortExpression="BBE_Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="ReducedPrice" HeaderText="Reduced Price" SortExpression="ReducedPrice">
                        <HeaderStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Weighted" HeaderText="Weighted" SortExpression="Weighted" />
                    <asp:BoundField DataField="IsNewProduct" HeaderText="New Item" SortExpression="IsNewProduct">
                        <HeaderStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LinkToProduct" DataFormatString="&lt;a href={0} target=&quot;_blank&quot;&gt;Order&lt;/a&gt;" HeaderText="Order" HtmlEncode="False" SortExpression="LinkToProduct" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetProducts" TypeName="BrakesSales.Index"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
